using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leap;

public class DetectorColisionesNivel5 : MonoBehaviour {

	Controller controller;

	bool detectarCircle;

	Hand firstHand = null;

	GameObject objetoColisionado;
	public GameObject controlador;
	private AudioSource audioPlayer;
	public AudioClip getObjectClip;
	public AudioClip crash;

	public int numObjetos = 4;
	public int itemConseguidos = 0;
	public int itemPerdidos = 0;
	public int itemRestantes;
	int i = 0;

	public Text score;

	// Use this for initialization
	void Start () {
		controller = new Controller();
		detectarCircle = true;
		ActivarCircle();

		audioPlayer = GetComponent<AudioSource>();
		score.text = itemConseguidos.ToString() + " / " + numObjetos.ToString();
		itemRestantes = numObjetos - itemConseguidos - itemPerdidos;
	}
	
	// Update is called once per frame
	void Update () {
		// Actualización del frame que capta el Leap

        Frame frame = controller.Frame();
        // do something with the tracking data in the frame...

		HandList hands = frame.Hands;
		Hand firstHand = null;
		if(hands.Count > 0){
			firstHand = hands[0];
		}

		GestureList gesturesInFrame = frame.Gestures();
		if (!gesturesInFrame.IsEmpty) {
			detectarCircle = false;
			controller.EnableGesture(Gesture.GestureType.TYPE_CIRCLE, detectarCircle);
			foreach (Gesture gesture in gesturesInFrame) {
				switch (gesture.Type) {
					case Gesture.GestureType.TYPE_CIRCLE:
						print("Detectado circulo");
						Invoke("ActivarCircle", 0.5f);
						
						//Handle swipe gestures
						break;
				}
			}
		}

		if(firstHand.IsRight){
			this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ManoAbierta");
		}
		else{
			this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ManoIzquierda");
		}
	}

	void ActivarCircle(){
		detectarCircle = true;
		controller.EnableGesture(Gesture.GestureType.TYPE_CIRCLE, detectarCircle);
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.tag == "Enemy" && detectarCircle == false){
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
			Invoke("EliminadoUltimoObjeto", 1f);
		}
	}

	public void PerderPuntuacion(){
		itemPerdidos++;
		itemRestantes = numObjetos - itemConseguidos - itemPerdidos;
		audioPlayer.clip = crash;
		audioPlayer.Play();
		if(itemRestantes == 0){
			Invoke("EliminadoUltimoObjeto", 1f);
		}
	}

	void EliminadoUltimoObjeto(){
		if((itemConseguidos*1.0f)/numObjetos == 1f){
			controlador.SendMessage("Pintar3Estrellas");
			PlayerPrefs.SetInt("nivel5", 3);
		}
		else if((itemConseguidos*1.0f)/numObjetos >= 0.5f){
			controlador.SendMessage("Pintar2Estrellas");
			PlayerPrefs.SetInt("nivel5", 2);
		}
		else if((itemConseguidos*1.0f)/numObjetos >= 0.33f){
			controlador.SendMessage("Pintar1Estrella");
			PlayerPrefs.SetInt("nivel5", 1);
		}
		controlador.SendMessage("NextString");
	}
}
