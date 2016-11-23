using UnityEngine;
using System.Collections;

public class vehicle : MonoBehaviour {
	//Defines
	private Vector3 vehiclePos; // position of vehicle
	public float speed= 1f; // speed of vehicle
	private Vector3 velocity;
	private Vector3 direction;
	private Quaternion angle; 
	private float zRotation;
	private Vector3 accelVector;
	private float maxSpeed= 0.1f;
	public float accelRate=0.1f;
	// Use this for initialization
	void Start () {
		vehiclePos = new Vector3 (0, 0, 0);
		velocity = new Vector3 (0, 0, 0);
		direction = new Vector3 (0, 1, 0);
		zRotation = 0;
		accelVector = new Vector3 (0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		//add speed to both directions
		//vehiclePos.x += speed*Time.deltaTime;
		//vehiclePos.y += speed*Time.deltaTime;
		Drive ();
		RotateDirection ();
	}

	void RotateDirection(){
		if(Input.GetKey(KeyCode.J)){
			zRotation+=3;
			angle= Quaternion.Euler(0,0,3);
			direction = angle * direction;

		}
		if(Input.GetKey(KeyCode.L)){
			angle= Quaternion.Euler(0,0,-3);
			direction = angle * direction;
			zRotation-=3;
		}

	}

	void Drive(){
		if (Input.GetKey (KeyCode.I)) {
			accelRate+=0.1f;
		}
		if (!Input.GetKey (KeyCode.I) && accelRate>=0.05f) {
			accelRate= accelRate*0.9f;

		}
		if (!Input.GetKey (KeyCode.I) && accelRate < 0.05f){
			accelRate=0;
		}
		velocity = accelRate * direction* Time.deltaTime;
		//velocity += accelVector;
		velocity = Vector3.ClampMagnitude (velocity, maxSpeed);
		vehiclePos += velocity;
		WrapVehicle ();
		SetTransform ();
	}

	void SetTransform(){
		// sets the new position
		transform.position = vehiclePos;
		transform.rotation = Quaternion.Euler(0,0, zRotation);
	}

	void WrapVehicle(){
		float x = transform.position.x;
		float y = transform.position.y;
		Vector3 wrapPos = vehiclePos;
		if (transform.position.x >= 10.0f ) {
			wrapPos.x= (-9.9f);
		}
		if (transform.position.x <= -10.0f ) {
			wrapPos.x=9.9f;
		}
		if (transform.position.y >= 5.0f) {
			wrapPos.y= (-4.9f);
		}
		if (transform.position.y <= -5.0f) {
			wrapPos.y= 4.9f;
		}
		vehiclePos = wrapPos;
	}

	
}
