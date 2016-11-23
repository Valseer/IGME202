using UnityEngine;
using System.Collections.Generic;
[RequireComponent (typeof(CharacterController))]
abstract public class vehicleMovement : MonoBehaviour {
	private Vector3 pos; // position of the vehicle
	public Vector3 direction;
	public Vector3 velocity;
	public Vector3 accel; // acceleration
	public float mass; // mass of the vehicle in relative scale
	public float maxSpeed;
	public float circleRadius;
	public float wanderAngle;
	public float angleChange;
	public float radius;
	// Rand Force for testing
	// Use this for initialization
	public virtual void Start () {
		pos=transform.position;
		wanderAngle=90f;
		maxSpeed = 20f;
		circleRadius= 10f;
		angleChange=20f;
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
		float newY=Terrain.activeTerrain.SampleHeight(transform.position)+5f;
		pos = transform.position;
		pos.y= Terrain.activeTerrain.SampleHeight(transform.position)+5f;
		transform.position= pos;
		velocity += accel*Time.deltaTime;
		velocity.y =0f;
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
		// Calculate the circle center
		Vector3 circleCenter;
		circleCenter = velocity;
		circleCenter.Normalize();
		circleCenter*= circleRadius;
		//
		// Calculate the displacement force
		Vector3 displacement;
		displacement = new Vector3(0,0,-1f);
		displacement *= circleRadius;

		// Change wanderAngle just a bit, so it
		// won't have the same value in the
		// next game frame.
		wanderAngle += (Random.value * angleChange - angleChange*.5f);
		// Randomly change the vector direction
		// by making it change its current angle
		displacement= setAngle(displacement, wanderAngle*Mathf.Deg2Rad);

		Vector3 wanderForce = displacement + circleCenter+ transform.position;
		Debug.DrawLine (transform.position, wanderForce, Color.black);
		Debug.DrawLine (transform.position, velocity+ transform.position, Color.red);
		return wanderForce;
	}
	
	public Vector3 setAngle(Vector3 vector, float value){
		float len = vector.magnitude;
		vector.z = Mathf.Cos(value) * len;
		vector.x = Mathf.Sin(value) * len;
		return vector;
	}

	public Vector3 Align(){
		Vector3 desiredVelocity= GameObject.Find("AgentManager").GetComponent<AgentManager>().avgDirection;
		desiredVelocity.Normalize();
		desiredVelocity *= maxSpeed;
		return desiredVelocity - velocity;
	}

	public Vector3 Cohesion(){
		return GameObject.Find("AgentManager").GetComponent<AgentManager>().flockCenter;
	}

	public Vector3 Separation(GameObject avoid, float safeDist){
		if((avoid.transform.position- transform.position).sqrMagnitude< (safeDist * safeDist)){
			return Flee(avoid.transform.position);
		}
		return new Vector3(0,0,0);
	}

	public Vector3 AvoidObstacle(GameObject obst, float safeDistance){
		Vector3 desiredVelocity= new Vector3(0,0,0);
		Vector3 vTOC= transform.position- obst.transform.position;
		//check if the distance between the objects, minus the radii, is greater than the safe distance
		if((Vector3.Distance(transform.position,obst.transform.position)-(radius+obst.GetComponent<ObjectInfo>().radius))< safeDistance){
			//check if the object is in front
			if(DotProd(transform.forward, vTOC)<0){
				//check if the radii combined is less than the Dot of the center and right vectors
				if((radius+obst.GetComponent<ObjectInfo>().radius)> DotProd(vTOC,transform.right)){
					float vTR= DotProd(vTOC, transform.right);
					//if obj to the right go left
					if(vTR>0){
						desiredVelocity= -transform.right*maxSpeed;
					}
					//if obj is to the left go right
					else{
						desiredVelocity= transform.right*maxSpeed;
					}
					Debug.DrawLine(transform.position, desiredVelocity-velocity, Color.blue);
					return desiredVelocity-velocity;
				}return new Vector3(0,0,0);
			}return new Vector3(0,0,0);
		}return new Vector3(0,0,0);
	}
	float CalcDist(Vector3 target, Vector3 seeker){
		return Mathf.Sqrt((((target.x-seeker.x)*(target.x-seeker.x))+((target.z-seeker.z)*(target.z-seeker.z))));
	}
	float DotProd(Vector3 posA,Vector3 posB){
			float dot=(posA.x * posB.x + posA.y * posB.y);
			return dot;
	}



}
