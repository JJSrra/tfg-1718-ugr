using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorColisiones : MonoBehaviour {

	int numObjetos = 5;

	void Start()
	{

	}

	void Update()
	{
		if(numObjetos == 0){
			
		}
	}

	void ColisionDetectada(){
		numObjetos--;
		print(numObjetos);
	}

}
