using UnityEngine;
using System.Collections;

public class randMove : MonoBehaviour {
	//Defines
	private Vector3 vehiclePos; // position of vehicle
	public float speed= 1f; // speed of vehicle
	private Vector3 velocity;
	private Quaternion angle; 
	private float maxSpeedAst = 0.035f;
	// Use this for initialization
	void Start () {
		vehiclePos = transform.position;
		//generate a random velocity for the asteroid at spawn.
		velocity = new Vector3 (Random.Range(-maxSpeedAst,maxSpeedAst), Random.Range(-maxSpeedAst,maxSpeedAst), 0);
	}
	
	// Update is called once per frame
	void Update () {
		Drive ();
	}
	//drive copied from vehical but moves at a static pace
	void Drive(){
		vehiclePos += velocity;
		WrapVehicle ();
		SetTransform ();
	}
	
	void SetTransform(){
		// sets the new position
		transform.position = vehiclePos;
		transform.rotation = Quaternion.Euler(0,0, 0);
	}

	//ensures the asteroids loop the screen. not size independent.
	void WrapVehicle(){
		float x = transform.position.x;
		float y = transform.position.y;
		Vector3 wrapPos = vehiclePos;
		if (x >= 10.0f ) {
			wrapPos.x= (-9.9f);
		}
		if (x <= -10.0f ) {
			wrapPos.x=9.9f;
		}
		if (y >= 5.0f) {
			wrapPos.y= (-4.9f);
		}
		if (y <= -5.0f) {
			wrapPos.y= 4.9f;
		}
		vehiclePos = wrapPos;
	}

}
