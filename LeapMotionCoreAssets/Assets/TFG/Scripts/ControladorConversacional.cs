using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorConversacional : MonoBehaviour {

	public string [] cadenas;
	
	public GameObject puntero;
	public GameObject ui;
	public GameObject hud;
	public GameObject panel;
	public Text papiro;
	int contador = 0;

	public Image estrella1;
	public Image estrella2;
	public Image estrella3;

	private AudioSource audioPlayer;
	public AudioClip win;
	public AudioClip star;

	private int numEstrellas = 0;

	// Use this for initialization
	void Start () {
		papiro.text = cadenas[contador];
		puntero.GetComponent<Collider2D>().enabled = false;
		audioPlayer = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKeyDown && !Input.GetKeyDown(KeyCode.P)){
			NextString();
		}

		if(Input.GetKeyDown(KeyCode.P)){
			panel.SetActive(!panel.activeSelf);
			if(panel.activeSelf){
				puntero.GetComponent<Collider2D>().enabled = false;
			}
			else{
				puntero.GetComponent<Collider2D>().enabled = true;
			}
		}
	}

	void NextString(){
		contador++;
		if(contador <= cadenas.Length -2){
			//contador++;
			papiro.text = cadenas[contador];
		}
		else if(contador == cadenas.Length-1){
			//contador++;
			ui.SetActive(false);
			hud.SetActive(true);
			puntero.GetComponent<Collider2D>().enabled = true;
		}
		else if(contador == cadenas.Length){
			papiro.text = cadenas[contador-1];
			ui.SetActive(true);
			hud.SetActive(false);
			puntero.GetComponent<Collider2D>().enabled = false;
		}
		else{
			panel.SetActive(true);
			audioPlayer.clip = win;
			audioPlayer.Play();
			Invoke("MostrarEstrellas", 3.0f);
		}

	}

	void Pintar1Estrella(){
		numEstrellas = 1;
	}

	void Pintar2Estrellas(){
		numEstrellas = 2;
	}

	void Pintar3Estrellas(){
		numEstrellas = 3;
	}

	void MostrarEstrellas(){
		if(numEstrellas == 1){
			estrella1.color = new Color32(255,255,255,255);
			audioPlayer.clip = star;
			audioPlayer.Play();
		}
		else if(numEstrellas == 2){
			estrella1.color = new Color32(255,255,255,255);
			estrella2.color = new Color32(255,255,255,255);
			audioPlayer.clip = star;
			audioPlayer.Play();
		}
		else if(numEstrellas == 3){
			estrella1.color = new Color32(255,255,255,255);
			estrella2.color = new Color32(255,255,255,255);
			estrella3.color = new Color32(255,255,255,255);
			audioPlayer.clip = star;
			audioPlayer.Play();
		}
	}
}
