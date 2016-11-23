using UnityEngine;
using System.Collections.Generic;

public class Flocker : vehicleMovement {
	public float boundingWeight;
	public float wanderWeight;
	public float obstWeight;
	public float alignWeight;
	public float cohesionWeight;
	public float seekWeight;
	public float separationWeight;
	public float safeDistance;

	public GameObject avoidTarget;

	public GameObject seekTarget;

	public float maxForce= 2f;
	// Use this for initialization
	public override void Start () {
		base.Start();
		radius = 10f;
	}

	public override void CalcSteeringForces(){
		Vector3 force = new Vector3 (0, 0, 0);
		force += Seek(seekTarget.transform.position)*seekWeight;
		force += Align()*alignWeight;
		force += Seek (Cohesion())*cohesionWeight;

		Debug.DrawLine(transform.position, Cohesion()*cohesionWeight, Color.yellow);
		float x = transform.position.x;
		float z = transform.position.z;
		List<GameObject> objs = GameObject.Find ("AgentManager").GetComponent<AgentManager> ().objs;
		List<GameObject> agents = GameObject.Find ("AgentManager").GetComponent<AgentManager> ().agents;
		for(int i=0; i<agents.Count; i++){
			if(agents[i].transform.position != transform.position){
				if((agents[i].transform.position-transform.position).sqrMagnitude< (transform.position-avoidTarget.transform.position).sqrMagnitude){
					avoidTarget=agents[i];
				}
			}
		}
		for(int j=0; j<objs.Count; j++){
			force+= AvoidObstacle(objs[j],safeDistance)*obstWeight;
		}
		force += Separation(avoidTarget, safeDistance) * separationWeight;
		if (x > 200f ||x < -200f ||z > 200f ||z < -200f) {
			force+=Seek(new Vector3(0,0,0))*boundingWeight;
			wanderAngle+=20f;
		}
		Vector3.ClampMagnitude(force, maxForce);
		
		ApplyForce( force);
	}

	public override Vector3 Persue(){
		return Seek(transform.position);
	}
	
	public override Vector3 Evade(){
		return Seek(transform.position);
	}
}
