using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectorColisionesNivel4SegundaMano : MonoBehaviour {

	public GameObject puntero;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Pociones"){
			puntero.SendMessage("AumentarPuntuacion");
			Destroy(other.gameObject);
		}
	}

}
