using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectorColisiones : MonoBehaviour {

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
	}

	void OnTriggerEnter2D(Collider2D other)
	{
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

	void EliminadoUltimoObjeto(){
		if((itemConseguidos*1.0f)/numObjetos == 1f){
			controlador.SendMessage("Pintar3Estrellas");
			PlayerPrefs.SetInt("nivel1", 3);
		}
		else if((itemConseguidos*1.0f)/numObjetos >= 0.5f){
			controlador.SendMessage("Pintar2Estrellas");
			PlayerPrefs.SetInt("nivel1", 2);
		}
		else if((itemConseguidos*1.0f)/numObjetos >= 0.33f){
			controlador.SendMessage("Pintar1Estrella");
			PlayerPrefs.SetInt("nivel1", 1);
		}
		controlador.SendMessage("NextString");
	}

}
