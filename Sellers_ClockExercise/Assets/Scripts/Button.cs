using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnMouseDown(){
		RotateHand hand = GameObject.Find ("ClockHand").GetComponent<RotateHand> ();
		hand.rotate = !hand.rotate;
	}
}
