using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class ControladorPosicionMano : MonoBehaviour {

	// Controlador del Leap
    Controller controller;
    //public int IDpuntero;

    void Start ()
    {
		// Inicialización de variables
		
        controller = new Controller();
    }

    void Update ()
    {
		// Actualización del frame que capta el Leap

        Frame frame = controller.Frame();
        // do something with the tracking data in the frame...

		// Detección de la primera mano que aparece en pantalla

		HandList hands = frame.Hands;
		Hand firstHand = hands[0];

        // Posicionamiento del puntero según la posición de la mano.

        // La X la he normalizado porque en pantalla va entre -8 y 8 y en las capturas
        // del Leap va entre -300 y 300. He obtenido ese valor dividiendo 300/8.

        // La Y la he normalizado porque en pantalla va entre -4.2 y 4.2 y en las capturas
        // del Leap va entre 50 y 470. El centro está aproximadamente en 230. Por ello
        // le resto 230 para situarlo con centro en 0 (al igual que en pantalla) y luego
        // divido por ese valor que es el resultado de 180/4.2 (180 es lo que va desde 
        // 230 hasta 50).

        float newX = firstHand.PalmPosition.x / 37.5f;
        float newY = (firstHand.PalmPosition.y -230) / 42.85f;
        transform.position = new Vector3(newX, newY, 0f);

        // if(hands.Count>1 && IDpuntero==2){
        //     Hand secondHand = hands[1];
        //     float newX2 = secondHand.PalmPosition.x /37.5f;
        //     float newY2 = (secondHand.PalmPosition.y -230) / 42.85f;
        //     transform.position = new Vector3(newX2, newY2, 0f);
        // }

        //print(transform.position);
    }
}
