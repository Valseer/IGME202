using UnityEngine;
using System.Collections.Generic;

public class shotMove : MonoBehaviour {
	//Defines
	private Vector3 vehiclePos; // position of vehicle
	public float speed= 1f; // speed of vehicle
	public Vector3 velocity;
	private Vector3 direction;
	private Quaternion angle; 
	private float accelRate;
	private Vector3 acceleration;
	private float maxSpeed = 0.135f;
	// Use this for initialization
	void Start () {
		//set the positon to the ships position
		vehiclePos = transform.position;
		//make it not moving
		velocity = new Vector3 (0,0,0);
		// get the direction from the ship
		direction = GameObject.Find ("Spaceship").GetComponent<vehicle>().GetDirection();
		accelRate = 1;
	}
	
	// Update is called once per frame
	void Update () {
		Drive ();
		DestroyAtEdge ();
	}
	//standard drive but with a constant speed increase
	void Drive(){
		acceleration = accelRate * direction* Time.deltaTime;
		velocity += acceleration;
		velocity = Vector3.ClampMagnitude (velocity, maxSpeed);
		vehiclePos += velocity;
		SetTransform ();
	}
	
	void SetTransform(){
		// sets the new position
		transform.position = vehiclePos;
	}

	//destroys the shot at the edge of the screen while also removing it from reference
	void DestroyAtEdge(){
		float x = transform.position.x;
		float y = transform.position.y;
		if (x >= 10.0f || x <= -10.0f ||y >= 5.0f ||y <= -5.0f) {
			GameObject.Find("Spaceship").GetComponent<vehicle>().shots.Remove(gameObject);
			Destroy(gameObject);
		}
	}
}
