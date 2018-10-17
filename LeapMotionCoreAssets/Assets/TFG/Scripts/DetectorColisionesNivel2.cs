using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leap;

public class DetectorColisionesNivel2 : MonoBehaviour {

	Controller controller;
	Hand firstHand = null;

	GameObject objetoColisionado;
	public GameObject controlador;
	private AudioSource audioPlayer;
	public AudioClip getObjectClip;

	public int numObjetos = 5;
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
		if(firstHand.GrabStrength > 0.98){
			this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ManoCerrada");
		}
		else{
			this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ManoAbierta");
		}
	}

	// void OnTriggerEnter2D(Collider2D other)
	// {
	// 	other.gameObject.GetComponent<PolygonCollider2D>().isTrigger = false;
	// 	Destroy(other.gameObject, 0.5f);
	// 	itemConseguidos++;
	// 	score.text = itemConseguidos.ToString() + " / " + numObjetos.ToString();
	// 	audioPlayer.clip = getObjectClip;
	// 	audioPlayer.Play();
	// 	if(itemConseguidos == numObjetos){
	// 		Invoke("EliminadoUltimoObjeto", 1f);
	// 	}
	// }

	/// <summary>
	/// Sent each frame where another object is within a trigger collider
	/// attached to this object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerStay2D(Collider2D other)
	{
		if(firstHand.GrabStrength > 0.98f){
			other.gameObject.GetComponent<PolygonCollider2D>().isTrigger = false;
			Destroy(other.gameObject, 0.5f);
			itemConseguidos++;
			score.text = itemConseguidos.ToString() + " / " + numObjetos.ToString();
			audioPlayer.clip = getObjectClip;
			audioPlayer.Play();
			if(itemConseguidos == numObjetos){
				Invoke("EliminadoUltimoObjeto", 1f);
			}
		}
	}

	void EliminadoUltimoObjeto(){
		if((itemConseguidos*1.0f)/numObjetos == 1f){
			controlador.SendMessage("Pintar3Estrellas");
			PlayerPrefs.SetInt("nivel2", 3);
		}
		else if((itemConseguidos*1.0f)/numObjetos >= 0.5f){
			controlador.SendMessage("Pintar2Estrellas");
			PlayerPrefs.SetInt("nivel2", 2);
		}
		else if((itemConseguidos*1.0f)/numObjetos >= 0.33f){
			controlador.SendMessage("Pintar1Estrella");
			PlayerPrefs.SetInt("nivel2", 1);
		}
		controlador.SendMessage("NextString");
	}

}
