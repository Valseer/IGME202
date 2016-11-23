using UnityEngine;
using System.Collections;

public class objspawn : MonoBehaviour {
	public Transform randObj;
	// Use this for initialization
	void Start () {
		spawnObjs ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void spawnObjs(){
		for (int i=0; i<50; i++) {
			spawnObjRand(Random.Range(5,195),Random.Range(5,195));
		}
	}

	void spawnObjRand(float randX,float randZ){
		Vector3 pos= new Vector3 (randX, Terrain.activeTerrain.SampleHeight(new Vector3(randX,0,randZ)), randZ);
		Instantiate (randObj, pos, Quaternion.identity);
	}
}
