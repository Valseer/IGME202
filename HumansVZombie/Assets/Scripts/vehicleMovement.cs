using UnityEngine;
using System.Collections;
[RequireComponent (typeof(CharacterController))]
abstract public class vehicleMovement : MonoBehaviour {
	private Vector3 pos; // position of the vehicle
	public Vector3 direction;
	public Vector3 velocity;
	public Vector3 accel; // acceleration
	public float mass; // mass of the vehicle in relative scale
	public float maxSpeed;
	private float maxX=300;
	private float maxZ=300;
	public float circleRadius;
	public float wanderAngle;
	public float angleChange;
	// Rand Force for testing
	// Use this for initialization
	public virtual void Start () {
		pos=transform.position;
		wanderAngle=20f;
		maxSpeed = 20f;
		circleRadius= 10f;
		angleChange=5f;
		velocity=new Vector3(0,0,0);
	}
	
	// Update is called once per frame
	void Update () {
		UpdatePos();
		CalcSteeringForces();
		gameObject.GetComponent<CharacterController> ().Move (velocity * Time.deltaTime);
		transform.forward = direction;
	}

	//update the position of the vehicle using focre based movement
	public void UpdatePos(){
		pos = transform.position;
		velocity += accel*Time.deltaTime;
		velocity.y = 0;
		pos += velocity* Time.deltaTime;
		accel = accel *0f;
		direction = velocity.normalized;
	}

	public void ApplyForce(Vector3 force){
		accel += force / mass;
	}

	public Vector3 Seek(Vector3 targetPos){
		Vector3 desiredVelocity = targetPos - pos;
		desiredVelocity.Normalize ();
		desiredVelocity *= maxSpeed;
		Vector3 seekVector = desiredVelocity - velocity;
		return seekVector;
	}

	public Vector3 Flee(Vector3 targetPos){
		Vector3 desiredVelocity = pos - targetPos;
		desiredVelocity.Normalize ();
		desiredVelocity *= maxSpeed;
		Vector3 seekVector = desiredVelocity - velocity;
		//seekVector = -seekVector;
		return seekVector;
	}

	void SetTransform(){
		transform.forward = direction;
		//transform.position = pos;
	}

	public abstract void CalcSteeringForces();

	public abstract Vector3 Persue();

	public abstract Vector3 Evade();

	public Vector3 Wander() {
		wanderAngle = 10f;
		// Calculate the circle center
		Vector3 circleCenter;
		circleCenter = transform.position + velocity;
		circleCenter.Normalize();
		circleCenter*= circleRadius;
		//
		// Calculate the displacement force
		Vector3 displacement;
		displacement = new Vector3(0f,0f, 1f);
		displacement*= circleRadius;
		// Change wanderAngle just a bit, so it
		// won't have the same value in the
		// next game frame.
		wanderAngle += Random.Range(-1f, 1f) * angleChange;
		//
		// Randomly change the vector direction
		// by making it change its current angle
		setAngle(displacement, wanderAngle);

		Vector3 wanderForce = displacement + circleCenter;
		return wanderForce;
	}
	
	public void setAngle(Vector3 vector, float value){
		float len = vector.magnitude;
		vector.x = Mathf.Cos(value) * len;
		vector.z = Mathf.Sin(value) * len;
	}

	void ObsticalAvoidance(){
		//Ditto for this :/
	}



}
