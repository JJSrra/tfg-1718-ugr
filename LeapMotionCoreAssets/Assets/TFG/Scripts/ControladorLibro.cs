using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leap;

public class ControladorLibro : MonoBehaviour {

	Controller controller;
	public UnityEngine.UI.Image enemy;
	public Text description;
	int contador = 0;

	bool detectarSwipe;


	// Use this for initialization
	void Start () {
		controller = new Controller();
		detectarSwipe = true;
		ActivarSwipe();
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

		GestureList gesturesInFrame = frame.Gestures();
		if (!gesturesInFrame.IsEmpty) {
			detectarSwipe = false;
			controller.EnableGesture(Gesture.GestureType.TYPE_SWIPE, detectarSwipe);
			foreach (Gesture gesture in gesturesInFrame) {
				switch (gesture.Type) {
					case Gesture.GestureType.TYPESWIPE:
						Invoke("ActivarSwipe", 1.5f);
						SwipeGesture swipeGesture = new SwipeGesture (gesture);
						if(swipeGesture.Direction.x < 0)
							contador += 1;
						else if(swipeGesture.Direction.x > 0)
							contador -= 1;
						contador = (contador+3) % 3;
						//Handle swipe gestures
						print(contador);
						CambiarAPagina(contador);
						break;
						//Vector swipeStart = swipe.StartPosition;
				}
			}
		}
	}

	void CambiarAPagina(int num){
		switch(num){
			case 0:
				enemy.sprite = Resources.Load<Sprite>("Gnomo");
				description.text = "GNOMO\n\nSon muy traviesos y les gusta molestar.";
			break;

			case 1:
				enemy.sprite = Resources.Load<Sprite>("Rana");
				description.text = "RANA\n\nEs verde, mírala que bonica.";
			break;

			case 2:
				enemy.sprite = Resources.Load<Sprite>("Pocion");
				description.text = "POCION\n\nEs venenosa, mejor no bebérsela.";
			break;
		}
		
	}

	void ActivarSwipe(){
		detectarSwipe = true;
		controller.EnableGesture(Gesture.GestureType.TYPE_SWIPE, detectarSwipe);
	}
}
