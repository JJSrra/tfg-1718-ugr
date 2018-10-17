using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estados : MonoBehaviour {

	// Variable que controla si el puntero está en contacto con alguno de los objetos interactuables.
	public bool enColision = false;

	// Variable que deteca qué tipo de movimiento se ha realizado.
	public enum Movement {Nothing, Circle, Swipe, Grab};
	public Movement movimiento = Movement.Nothing;

	// Estados reconocidos
	public enum State {Estado1, Estado2, Estado3};
	public State estado = State.Estado1;
	

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		ChangeState();
	}

	void ChangeState(){
		if(enColision && movimiento == Movement.Circle){
			estado = State.Estado2;
		}
		else if(enColision && movimiento == Movement.Nothing){
			estado = State.Estado1;
		}
		else{
			estado = State.Estado3;
		}
	}
}
