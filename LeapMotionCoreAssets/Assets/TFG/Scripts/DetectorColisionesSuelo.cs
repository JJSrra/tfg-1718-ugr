using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorColisionesSuelo : MonoBehaviour {

	public GameObject puntero;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Pociones"){
			puntero.SendMessage("PerderPuntuacion");
		}
	}

}
