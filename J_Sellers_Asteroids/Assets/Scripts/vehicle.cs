using UnityEngine;
using System.Collections.Generic;

public class vehicle : MonoBehaviour {
	//Defines
	private Vector3 vehiclePos; // position of vehicle
	public float speed= 1f; // speed of vehicle
	private Vector3 velocity;
	private Vector3 direction;
	private Quaternion angle; 
	private float zRotation;
	private float maxSpeed= 3f;
	public float accelRate=0f;
	private int shotMax=5;
	public List<GameObject> shots;
	public Transform shot;
	private Vector3 acceleration;
	public AudioClip[] clips = new AudioClip[2];
	private AudioSource[] sources= new AudioSource[2];
	// Use this for initialization
	void Start () {
		vehiclePos = new Vector3 (0, 0, 0);
		velocity = new Vector3 (0, 0, 0);
		direction = new Vector3 (0, 1, 0);
		zRotation = 0;
		acceleration = new Vector3 (0, 0, 0);
		shots = new List<GameObject> ();
		int i=0;
		while (i<2) {
			sources [i] = gameObject.AddComponent<AudioSource> ();
			i++;
		}
	}
	
	// Update is called once per frame
	void Update () {
		Drive ();
		WrapVehicle ();
		SetTransform ();
		RotateDirection ();
		Shoot ();
		//ShotsActive ();
	}
	//standard rotate copied from the collision assignemnt
	void RotateDirection(){
		if(Input.GetKey(KeyCode.A)){
			zRotation+=3;
			angle= Quaternion.Euler(0,0,3);
			direction = angle * direction;

		}
		if(Input.GetKey(KeyCode.D)){
			angle= Quaternion.Euler(0,0,-3);
			direction = angle * direction;
			zRotation-=3;
		}

	}

	//drive has deceleration but isn't floaty like original asteroids
	void Drive(){
		if (Input.GetKey (KeyCode.W)) {
			accelRate+=0.25f;		
			acceleration = accelRate * direction* Time.deltaTime;
			velocity += acceleration;
			velocity = Vector3.ClampMagnitude (velocity, maxSpeed);
			vehiclePos += velocity *Time.deltaTime;
		}
		else {
			velocity = velocity * 0.95f;
			if(velocity.magnitude < 0.005){
				velocity = new Vector3(0, 0, 0);
			}
			vehiclePos += velocity * Time.deltaTime;
		}
	}

	void SetTransform(){
		// sets the new position
		transform.position = vehiclePos;
		transform.rotation = Quaternion.Euler(0,0, zRotation);
	}

	//ensures the vehicle wrapps around the screen
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

	//Resets the position and velocity of the ship. Also plays a sound for fun.
	public void Reset(){
		velocity= Vector3.zero;
		vehiclePos= Vector3.zero;
		sources[1].clip=clips[1];
		AudioSource.PlayClipAtPoint(clips[1],Camera.main.transform.position);
	}

	//Spawns a new shot from the center of the ship. Also plays a sound for fun.
	void Shoot(){
		if (Input.GetKeyDown (KeyCode.Space)&& shots.Count<shotMax) {
			Transform tempShot= (Transform)Instantiate (shot, vehiclePos, transform.rotation);
			shots.Add(tempShot.gameObject);
			sources[0].clip=clips[0];
			AudioSource.PlayClipAtPoint(clips[0],Camera.main.transform.position);
		}
	}
	//basic getters and setters.
	public Vector3 GetDirection(){
		return direction;
	}
	
}
