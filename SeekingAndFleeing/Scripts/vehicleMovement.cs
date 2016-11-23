using UnityEngine;
using System.Collections;

public class vehicleMovement : MonoBehaviour {
	private Vector3 pos; // position of the vehicle
	public Vector3 direction;
	public Vector3 velocity;
	public Vector3 accel; // acceleration
	public float mass; // mass of the vehicle in relative scale
	public float maxSpeed;
	private bool appFrict;
	public GameObject target;
	// Rand Force for testing
	// Use this for initialization
	void Start () {
		pos=transform.position;
		appFrict=false;
		maxSpeed = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		UpdatePos ();
		SetTransform ();
		BounceVehicle ();
	}

	//update the position of the vehicle using focre based movement
	void UpdatePos(){
		pos = transform.position;
		ApplyForce (Seek (target.transform.position));
		velocity += accel*Time.deltaTime;
		pos += velocity* Time.deltaTime;
		accel = accel *0f;
		direction = velocity.normalized;
	}

	public void ApplyForce(Vector3 force){
		accel += force / mass;
	}

	private Vector3 Seek(Vector3 targetPos){
		Vector3 desiredVelocity = targetPos - pos;
		//desiredVelocity = Vector3.ClampMagnitude (desiredVelocity, maxSpeed);
		desiredVelocity.Normalize ();
		desiredVelocity *= maxSpeed;
		Vector3 seekVector = desiredVelocity - velocity;
		return seekVector;
	}

	/// <summary>
	/// Applies the gravity force equaly
	/// </summary>
	/// <param name="force">Force.</param>
	void ApplyGravity(Vector3 force){
		accel += force;
	}
	/// <summary>
	/// Sets the transform component based on the position
	/// </summary>
	void SetTransform(){
		transform.up = direction;
		transform.position = pos;
	}

	void ActivateFriciton(){
		if(Input.GetKeyDown(KeyCode.F)){
			appFrict= !appFrict;
		}
	}
	/// <summary>
	/// Applies the friction.
	/// </summary>
	void ApplyFriction(float coeff){
		if(appFrict){
			Vector3 friction = -velocity;
			friction.Normalize ();
			friction = friction * coeff;
			accel += friction;
		}
	}
	void BounceVehicle(){
		float x = transform.position.x;
		float y = transform.position.y;
		Vector3 wrapPos = pos;
		if (x > 10.0f ) {
			wrapPos.x=10f;
			velocity.x*=-1;
		}
		if (x < -10.0f ) {
			wrapPos.x=-10f;
			velocity.x*=-1;
		}
		if (y > 5.0f) {
			wrapPos.y=5f;
			velocity.y*=-1;
		}
		if (y < -5.0f) {
			wrapPos.y=-5f;
			velocity.y*=-1;
		}
		pos = wrapPos;
	}
}
