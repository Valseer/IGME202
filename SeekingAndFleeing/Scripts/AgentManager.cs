using UnityEngine;
using System.Collections;

public class AgentManager : MonoBehaviour {
	public Transform greenDude;
	public Transform pinkDude;
	private float maxSpeed=1f;
	private bool seekOrFlee;//true is seek false is flee
	private string text;

	// Use this for initialization
	void Start () {
		seekOrFlee=true;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.S)){
			seekOrFlee=true;
		}
		if(Input.GetKeyDown (KeyCode.F)){
			seekOrFlee=false;
		}
		if(seekOrFlee){
			greenDude.GetComponent<vehicleMovement>().ApplyForce( Seek(pinkDude.transform.position));
		}
		else{
			greenDude.GetComponent<vehicleMovement>().ApplyForce( Flee(pinkDude.transform.position));
		}
	}

	Vector3 Seek(Vector3 targetPos){
		Vector3 desiredVelocity= targetPos- greenDude.transform.position;
		desiredVelocity= Vector3.ClampMagnitude(desiredVelocity, maxSpeed);
		Vector3 steeringForce= desiredVelocity- greenDude.GetComponent<vehicleMovement>().velocity;
		return steeringForce;
	}

	Vector3 Flee(Vector3 targetPos){
		Vector3 desiredVelocity=  greenDude.transform.position - targetPos;
		desiredVelocity= Vector3.ClampMagnitude(desiredVelocity, maxSpeed);
		Vector3 steeringForce= desiredVelocity - greenDude.GetComponent<vehicleMovement>().velocity;
		return steeringForce;
	}

	void OnGUI(){
		if(seekOrFlee){
			text="Seeking";
		}
		else{
			text="Fleeing";
		}
		GUI.Box(new Rect(0, 0, 100	, 30), text);

	}

}
