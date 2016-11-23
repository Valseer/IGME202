using UnityEngine;
using System.Collections;

public class zspawn : MonoBehaviour {
	public Transform zomb;

	// Use this for initialization
	void Start () {
		spawnHoard ();

	}
	
	// Update is called once per frame
	void Update () {

	}

	void spawnHoard (){
		for (int x=0; x<100; x++) {
			float seed= Random.value;
			if(seed<0.5f){
				spawnZombie (Random.Range(75,125),Random.Range(100,115));
			}
			else if(seed<0.7f){
				spawnZombie ( Random.Range(75,125),Random.Range(115,130));
			}
			else if(seed<0.85f){
				spawnZombie( Random.Range(75,125),Random.Range(130,145));
			}
			else if (seed<0.95f){
				spawnZombie( Random.Range(75,125),Random.Range(145, 160));
			}
			else{
				spawnZombie( Random.Range(75,125),Random.Range(160,175));
			}
		}
	}

	void spawnZombie(float randX, float randZ){
		Vector3 pos= new Vector3 (randX, Terrain.activeTerrain.SampleHeight(new Vector3(randX,0,randZ)), randZ);
		Instantiate (zomb, pos, new Quaternion(0f,90f,0f,0f));
	}
}
