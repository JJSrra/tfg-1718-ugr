using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class ControladorMovimientos : MonoBehaviour {

	Estados estado;

	// Controlador del Leap
    Controller controller;

	//public bool estaEnCirculo = false;
	//public GameObject circulo;
	//public bool cerrado;
	//public int num_cerrado = 0;

	// Use this for initialization
	void Start () {

		// Inicialización de variables

		estado = GetComponent<Estados>();
        controller = new Controller();
		
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
		
		if(firstHand != null){
			CancelarMovimiento(firstHand);
		}

		//print(firstHand.GrabStrength);

		DetectarMovimiento(frame);
		
		// CerrarMano();

		// if(grab){

		// 	grab = false;
		// }
		
	}

	void DetectarMovimiento(Frame fr){

		controller.EnableGesture(Gesture.GestureType.TYPE_CIRCLE);
		//controller.EnableGesture(Gesture.GestureType.TYPE_KEY_TAP);
		//controller.EnableGesture(Gesture.GestureType.TYPE_SCREEN_TAP);
		controller.EnableGesture(Gesture.GestureType.TYPE_SWIPE);

		GestureList gesturesInFrame = fr.Gestures();

		if (!gesturesInFrame.IsEmpty) {
			foreach (Gesture gesture in gesturesInFrame) {
				switch (gesture.Type) {
					case Gesture.GestureType.TYPECIRCLE:
						//Handle circle gestures
						//print("Detectado círculo!!");
						estado.movimiento = Estados.Movement.Circle;
						break;

					// case Gesture.GestureType.TYPEKEYTAP:
					// 	//Handle key tap gestures
					// 	print("Detectado tap!!");
					// 	break;

					// case Gesture.GestureType.TYPESCREENTAP:
					// 	//Handle screen tap gestures
					// 	print("Detectado screen tap!!");
					// 	break;

					case Gesture.GestureType.TYPESWIPE:
						//Handle swipe gestures
						//print("Detectado swipe!!");
						estado.movimiento = Estados.Movement.Swipe;
						break;
						
					default:
						//Handle unrecognized gestures
						print("Otro gesto detectado");
						break;
				}
			}
		}
	}

	void CancelarMovimiento(Hand hand){
		if(hand.GrabStrength < 0.1 && estado.movimiento != Estados.Movement.Nothing){
			print("Movmiento cambiado a nothing");
			estado.movimiento = Estados.Movement.Nothing;
		}
	}

	// void CerrarMano()
	// {
	// 	if(firstHand.GrabStrength > 0.8){
	// 		cerrado = true;
	// 	}
	// 	if(cerrado && firstHand.GrabStrength <= 0.8){
	// 		cerrado = false;
	// 		num_cerrado = 1;
	// 	}

	// 	if(num_cerrado == 1){
	// 		grab = true;
	// 	}
	// }
}
