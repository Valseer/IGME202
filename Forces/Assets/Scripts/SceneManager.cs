using UnityEngine;
using System.Collections.Generic;

public class SceneManager : MonoBehaviour {

	public Transform greenDude;
	public Transform pinkDude;
	public Transform blueDude;
	public List<GameObject> dudes;
	private float maxX= 10f;
	private float maxY= 5f;
	// Use this for initialization
	void Start () {
		dudes= new List<GameObject>();
		spawn();
	}
	
	// Update is called once per frame
	void Update () {
		ApplyMouse();
	}

	void spawn(){
		Vector3 posG= SpawnLocation ();
		Vector3 posB= SpawnLocation ();
		Vector3 posP= SpawnLocation ();
		Transform green= (Transform)Instantiate (greenDude, posG, new Quaternion(0f,0f,0f,0f));
		Transform blue= (Transform)Instantiate (blueDude, posB, new Quaternion(0f,0f,0f,0f));
		Transform pink= (Transform)Instantiate (pinkDude, posP, new Quaternion(0f,0f,0f,0f));
		green.gameObject.GetComponent<vehicleMovement>().mass=1f;
		blue.gameObject.GetComponent<vehicleMovement>().mass=5f;
		pink.gameObject.GetComponent<vehicleMovement>().mass=20f;
		dudes.Add (blue.gameObject);
		dudes.Add (green.gameObject);
		dudes.Add (pink.gameObject);
	}
	Vector3 SpawnLocation(){
		Vector3 spawnPos= new Vector3(Random.Range(-maxX,maxX),Random.Range(-maxY,maxY));
		return spawnPos;
	}

	void ApplyMouse(){
		if(Input.GetKey(KeyCode.Mouse0)){
			for(int i=0; i<3;i++){
				Vector3 force=(dudes[i].transform.position-(Camera.main.ScreenToWorldPoint(Input.mousePosition))).normalized*5;
				force.z=0;
				dudes[i].GetComponent<vehicleMovement>().ApplyForce(force);
			}
		}
	}
}
