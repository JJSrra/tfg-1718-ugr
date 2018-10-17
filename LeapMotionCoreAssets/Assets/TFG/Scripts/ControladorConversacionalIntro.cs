using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControladorConversacionalIntro : MonoBehaviour {

	public string [] cadenas;
	public Text papiro;
	int contador = 0;

	// Use this for initialization
	void Start () {
		papiro.text = cadenas[contador];
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKeyDown){
			NextString();
		}
	}

	void NextString(){
		contador++;
		if(contador < cadenas.Length){
			papiro.text = cadenas[contador];
		}
		else if(contador == cadenas.Length){
			SceneManager.LoadScene("SelectScene");
		}
	}
}
