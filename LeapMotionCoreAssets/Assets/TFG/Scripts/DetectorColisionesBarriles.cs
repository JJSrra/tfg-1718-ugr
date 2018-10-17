using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorColisionesBarriles : MonoBehaviour {

	public GameObject puntero;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "ObjetosMoviles"){
			if(other.GetComponent<ColorObjeto>().color == this.GetComponent<ColorObjeto>().color){
				//other.gameObject.GetComponent<PolygonCollider2D>().isTrigger = false;
				Destroy(other.gameObject, 0.5f);
				AumentarItem();
			}
		}
	}

	void AumentarItem(){
		puntero.SendMessage("AumentarPuntuacion");
	}
}
