using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public int escenaActual = 1;
	public int puntuacion = 0;
	public int maxEscenas = 5;
	int minEscenas = 1;
	public Text scoreText;
	public Image imagenActual;
	public Text textoActual;

	void Start()
	{
		puntuacion = PlayerPrefs.GetInt("nivel1", 0) + 
					 PlayerPrefs.GetInt("nivel2", 0) +
					 PlayerPrefs.GetInt("nivel3", 0) +
					 PlayerPrefs.GetInt("nivel4", 0) +
					 PlayerPrefs.GetInt("nivel5", 0);
		if(scoreText != null){
			string score = puntuacion.ToString() + " / " + (maxEscenas*3).ToString();
			scoreText.text = score;
		}
		if(imagenActual != null){
			imagenActual.sprite = Resources.Load<Sprite>("Habitacion");
		}
	}

	void Update()
	{
		
	}

	public void CargarEscena(string nombre)
	{
		if(nombre == "Scene"){
			SceneManager.LoadScene(nombre+escenaActual.ToString());
		}
		else{
			SceneManager.LoadScene(nombre);
		}
	}

	public void CargarIntro(){
		PlayerPrefs.SetInt("nivel1",0);
		PlayerPrefs.SetInt("nivel2",0);
		PlayerPrefs.SetInt("nivel3",0);
		PlayerPrefs.SetInt("nivel4",0);
		PlayerPrefs.SetInt("nivel5",0);
		SceneManager.LoadScene("IntroScene");
	}

	public void CargarMenu(){
		SceneManager.LoadScene("MenuScene");
	}

	public void PasarAEscena(int num)
	{
		escenaActual += num;
		if (escenaActual < minEscenas){
			escenaActual = minEscenas;
		}
		else if (escenaActual > maxEscenas){
			escenaActual = maxEscenas;
		}

		switch(escenaActual){
			case 1:
				imagenActual.sprite = Resources.Load<Sprite>("Habitacion");
			break;

			case 2:
				imagenActual.sprite = Resources.Load<Sprite>("Bosque");
			break;

			case 3:
				imagenActual.sprite = Resources.Load<Sprite>("Barriles");
			break;

			case 4:
				imagenActual.sprite = Resources.Load<Sprite>("Pociones");
			break;

			case 5:
				imagenActual.sprite = Resources.Load<Sprite>("Castillo");
			break;
		}

		textoActual.text = escenaActual.ToString();
		
	}

	public void Salir(){
		Application.Quit();
	}
}
