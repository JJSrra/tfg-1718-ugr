using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leap;

public class DetectorColisionesNivel4 : MonoBehaviour {

	Controller controller;
	Hand firstHand = null;
	public GameObject manoizquierda;

	GameObject objetoColisionado;
	public GameObject controlador;
	private AudioSource audioPlayer;
	public AudioClip getObjectClip;
	public AudioClip crash;

	public int numObjetos = 7;
	public int itemConseguidos = 0;
	public int itemPerdidos = 0;
	public int itemRestantes;
	int i = 0;

	public Text score;

	public GameObject [] pociones;
	public GameObject gnomo;

	void Start()
	{
		audioPlayer = GetComponent<AudioSource>();
		score.text = itemConseguidos.ToString() + " / " + numObjetos.ToString();
		controller = new Controller();
		itemRestantes = numObjetos - itemConseguidos - itemPerdidos;
	}

	void Update()
	{
		Frame frame = controller.Frame();
		HandList hands = frame.Hands;
		
		if(hands.Count > 0){
			firstHand = hands[0];
		}
		

		if(firstHand.IsRight){
			this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ManoAbierta");
			if(hands.Count > 1){
				manoizquierda.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ManoIzquierda");
			}
			
		}
		else{
			this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ManoIzquierda");
			if(hands.Count > 1){
				manoizquierda.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ManoAbierta");
			}
		}
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Pociones"){
			AumentarPuntuacion();
			Destroy(other.gameObject);
		}
	}

	

	public void AumentarPuntuacion(){
		itemConseguidos++;
		itemRestantes = numObjetos - itemConseguidos - itemPerdidos;
		score.text = itemConseguidos.ToString() + " / " + numObjetos.ToString();
		audioPlayer.clip = getObjectClip;
		audioPlayer.Play();
		if(itemRestantes == 0){
			Invoke("EliminadoUltimoObjeto", 0f);
		}
	}

	public void PerderPuntuacion(){
		itemPerdidos++;
		itemRestantes = numObjetos - itemConseguidos - itemPerdidos;
		audioPlayer.clip = crash;
		audioPlayer.Play();
		if(itemRestantes == 0){
			Invoke("EliminadoUltimoObjeto", 0f);
		}
	}

	void LlamarPociones(){
		InvokeRepeating("CaerPocion", 1f, 2f);
	}

	void CaerPocion(){
		if(itemRestantes > 0){
			i = (i+4) % numObjetos;
			GameObject poc = pociones[i];
			gnomo.transform.position = poc.transform.position;
			gnomo.GetComponent<Animator>().Play("Push");
			string ani = "Fall-Poc"+((i+1).ToString());
			poc.GetComponent<Animator>().Play(ani);
		}
		else{
			CancelInvoke();
		}
	}

	void EliminadoUltimoObjeto(){
		if((itemConseguidos*1.0f)/numObjetos == 1f){
			controlador.SendMessage("Pintar3Estrellas");
			PlayerPrefs.SetInt("nivel4", 3);
		}
		else if((itemConseguidos*1.0f)/numObjetos >= 0.5f){
			controlador.SendMessage("Pintar2Estrellas");
			PlayerPrefs.SetInt("nivel4", 2);
		}
		else if((itemConseguidos*1.0f)/numObjetos >= 0.33f){
			controlador.SendMessage("Pintar1Estrella");
			PlayerPrefs.SetInt("nivel4", 1);
		}
		controlador.SendMessage("NextString");
	}

}
