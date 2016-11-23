using UnityEngine;
using System.Collections;

public class Zombie : vehicleMovement {
	public float seekWeight= 2f;
	public float maxForce=20f;
	public float boundingForce= 1.5f;
	public Material material1;//right
	public Material material2;//forward
	public Material material3;//desired
	public GameObject targetSeek;
	public float targetDist;
	// Use this for initialization
	public override void Start () {
		base.Start();
		maxSpeed=50f;
	}
	public override void CalcSteeringForces(){
		Vector3 force;
		if(targetSeek!= null){
			force=Persue() * seekWeight;
		}
		else{
			force=Wander();
		}
		float x = transform.position.x;
		float z = transform.position.z;
		
		if (x > 300f) {
			force+=Seek(new Vector3(0,0,0))*boundingForce;
		}
		if (x < -300f ) {
			force+=Seek(new Vector3(0,0,0))*boundingForce;
		}
		if (z > 300f) {
			force+=Seek(new Vector3(0,0,0))*boundingForce;
		}
		if (z < -300f) {
			force+=Seek(new Vector3(0,0,0))*boundingForce;
		}
		Vector3.ClampMagnitude (force, maxForce);
		ApplyForce (force);
	}

	public override Vector3 Persue(){
		Vector3 targetPos=targetSeek.transform.position+(targetSeek.GetComponent<Human>().velocity);
		return Seek(targetPos);
	}

	public override Vector3 Evade(){
		return Seek(transform.position);
	}

	void OnRenderObject()
	{
		// Set the material to be used for the first line
		material1.SetPass(0);
		// Draws one line
		GL.Begin(GL.LINES);
		// Begin to draw lines
		GL.Vertex(transform.position);
		// First endpoint of this line
		GL.Vertex(targetSeek.transform.position);
		// Second endpoint of this line
		GL.End();
		// Finish drawing the line
		// Second line
		// Set another material to draw this second line in a different color
		/*material2.SetPass(0);
		GL.Begin(GL.LINES);
		GL.Vertex(transform.position);
		GL.Vertex(transform.right);
		GL.End();*/

		material3.SetPass(0);
		GL.Begin(GL.LINES);
		GL.Vertex(transform.position);
		GL.Vertex(velocity);
		GL.End();
	}
}
