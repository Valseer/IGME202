using UnityEngine;
using System.Collections.Generic;

public class AgentManager : MonoBehaviour {
	public Transform psgModel;
	public Transform humanModel;
	public Transform avoidObject;
	public Material material1;
	public List<GameObject> objs;
	public List<GameObject> agents;
	public GameObject psg;
	public GameObject centriod;
	private float maxX=200;
	private float maxZ=200;
	public Vector3 avgDirection= new Vector3(0,0,0);
	public Vector3 flockCenter= new Vector3(0,0,0);

	// Use this for initialization
	void Start () {
		SpawnAssets();
	}
	
	// Update is called once per frame
	void Update () {
		CheckAssets();
	}

	void SpawnAssets(){
		Transform tempPSG= (Transform) Instantiate(psgModel,RandSpawn(), new Quaternion(0f,0f,0f,0f));
		psg= tempPSG.gameObject;
		for(int i=0; i<8; i++){
			Transform tempHum = (Transform) Instantiate(humanModel,RandSpawn(),new Quaternion(0f,0f,0f,0f));
			tempHum.GetComponent<Flocker>().seekTarget=psg;
			tempHum.GetComponent<Flocker>().avoidTarget= psg;
			agents.Add(tempHum.gameObject);
		}
		for(int j=0; j<8; j++){
			Transform tempObj= (Transform) Instantiate(avoidObject, RandSpawn(), new Quaternion(0,0,0,0));
			objs.Add(tempObj.gameObject);
		}
	}

	Vector3 RandSpawn(){ 
		Vector3 tempVector = new Vector3 (Random.Range (-maxX, maxX), 5f, Random.Range(-maxZ, maxZ));
		tempVector.y= Terrain.activeTerrain.SampleHeight(tempVector)+5f;
		return tempVector;
	}

	void CheckAssets(){
		/*
			look at each human and calculate their forces
			apply each force to the human

			next look at the zombie have it seek the closest human

			finally if there is a human within 6units of the PSG respawn the PSG
		 */
		avgDirection= new Vector3(0,0,0);
		flockCenter= new Vector3(0,0,0);
		for(int i=0; i<agents.Count;i++){
			Vector3 dist= agents[i].transform.position -psg.transform.position;
			if(dist.magnitude<50f){
				psg.transform.position=RandSpawn();
			}
			avgDirection += agents[i].transform.forward;
			flockCenter +=  agents[i].transform.position;
		}
		avgDirection = avgDirection/agents.Count;
		flockCenter = flockCenter/agents.Count;
		flockCenter.y=Terrain.activeTerrain.SampleHeight(flockCenter);
		centriod.transform.position= flockCenter;
		centriod.transform.forward= avgDirection;
	}

	float CalcDist(Vector3 target, Vector3 seeker){
		return (((target.x-seeker.x)*(target.x-seeker.x))+((target.z-seeker.z)*(target.z-seeker.z)));
	}
	
	//standard bounding circle collision detection same as from Collision assignment.
	bool BoundingCircle( GameObject obj1, GameObject obj2){
		//Distance from the center x and y
		//if the square distance is greater than the radius square return true
		if(CalcDist(obj1.transform.position, obj2.transform.position)<= (125f)){
			return true;
		}
		//otherwise return false
		return false;
	}

	void OnRenderObject()
	{
		// Set the material to be used for the first line
		material1.SetPass(0);
		// Draws one line
		GL.Begin(GL.LINES);
		// Begin to draw lines
		GL.Vertex(centriod.transform.position);
		// First endpoint of this line
		Vector3 dir= centriod.transform.forward*50f + centriod.transform.position;
		dir.y=centriod.transform.position.y;
		GL.Vertex(dir);
		// Second endpoint of this line
		GL.End();
	}


}
