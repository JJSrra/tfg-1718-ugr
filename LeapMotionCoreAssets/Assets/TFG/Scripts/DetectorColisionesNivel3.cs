using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leap;

public class DetectorColisionesNivel3 : MonoBehaviour {

	Controller controller;
	Hand firstHand = null;

	GameObject objetoColisionado;
	public GameObject controlador;
	private AudioSource audioPlayer;
	public AudioClip getObjectClip;

	public int numObjetos = 4;
	public int itemConseguidos = 0;

	public Text score;

	void Start()
	{
		audioPlayer = GetComponent<AudioSource>();
		score.text = itemConseguidos.ToString() + " / " + numObjetos.ToString();
		controller = new Controller();
	}

	void Update()
	{
		Frame frame = controller.Frame();
		HandList hands = frame.Hands;
		
		if(hands.Count > 0){
			firstHand = hands[0];
		}

		if(firstHand.PinchStrength > 0.5f){
			this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ManoDosDedos");
		}
		else{
			this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ManoAbierta");
		}
	}

	bool cogido = false;
	GameObject target = null;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(!cogido){
			cogido = true;
			target = other.gameObject;
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{		
		if(other.tag == "ObjetosMoviles" && (firstHand.PinchStrength > 0.5f)){
			Vector3 newPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
			target.transform.position = newPos;
		}
		
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject == target){
			target = null;
			cogido = false;
		}
	}

	void EliminadoUltimoObjeto(){
		if((itemConseguidos*1.0f)/numObjetos == 1f){
			controlador.SendMessage("Pintar3Estrellas");
			PlayerPrefs.SetInt("nivel3", 3);
		}
		else if((itemConseguidos*1.0f)/numObjetos >= 0.5f){
			controlador.SendMessage("Pintar2Estrellas");
			PlayerPrefs.SetInt("nivel3", 2);
		}
		else if((itemConseguidos*1.0f)/numObjetos >= 0.33f){
			controlador.SendMessage("Pintar1Estrella");
			PlayerPrefs.SetInt("nivel3", 1);
		}
		controlador.SendMessage("NextString");
	}

	public void AumentarPuntuacion(){
		itemConseguidos++;
		score.text = itemConseguidos.ToString() + " / " + numObjetos.ToString();
		audioPlayer.clip = getObjectClip;
		audioPlayer.Play();
		if(itemConseguidos == numObjetos){
			Invoke("EliminadoUltimoObjeto", 1f);
		}
		target = null;
		cogido = false;
	}

}
