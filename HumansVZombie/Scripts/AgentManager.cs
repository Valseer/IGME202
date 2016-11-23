using UnityEngine;
using System.Collections.Generic;

public class AgentManager : MonoBehaviour {
	public Transform psgModel;
	public Transform zombieModel;
	public Transform humanModel;
	public List<GameObject> humans;
	public GameObject psg;
	public GameObject zombie;
	public List<GameObject> zombies;
	private float maxX=300;
	private float maxZ=300;

	// Use this for initialization
	void Start () {
		SpawnAssets();
	}
	
	// Update is called once per frame
	void Update () {
		CheckAssets();
	}

	void SpawnAssets(){
		for(int i=0; i<4; i++){
			Transform tempHum = (Transform) Instantiate(humanModel,RandSpawn(),new Quaternion(0f,0f,0f,0f));
			humans.Add(tempHum.gameObject);
		}
		Transform tempZomb = (Transform) Instantiate(zombieModel,RandSpawn(),new Quaternion(0f,0f,0f,0f));
		zombies.Add(tempZomb.gameObject);
		zombies[0].GetComponent<Zombie>().targetSeek=humans[0];
	}

	Vector3 RandSpawn(){ 
		return new Vector3 (Random.Range (-maxX, maxX), 5f, Random.Range(-maxZ, maxZ));
	}

	void CheckAssets(){
		/*
			look at each human and calculate their forces
			apply each force to the human

			next look at the zombie have it seek the closest human

			finally if there is a human within 6units of the PSG respawn the PSG
		 */
		if(humans.Count>0){
			float distZH= 80000f;
			for(int i=0; i<humans.Count; i++){
				for(int j=0; j<zombies.Count; j++){
					humans[i].GetComponent<Human>().targetFlee=zombies[j];
					float tempZHDist = CalcDist(humans[i].transform.position, zombies[j].transform.position);
					humans[i].GetComponent<Human>().distToZomb=tempZHDist;
					if(distZH>tempZHDist){
						distZH=tempZHDist;
						zombies[j].GetComponent<Zombie>().targetSeek=humans[i];
						zombies[j].GetComponent<Zombie>().targetDist=tempZHDist;
					}

					if(BoundingCircle(zombies[j],humans[i])){
						//call the hit command to denote a hit
						GameObject tempHuman= humans[i];
						Infect(tempHuman);
						UpdateSeekTargets();
						humans.Remove(tempHuman);
						Destroy(tempHuman);
						//break out to prevent issues with null exceptions. not pretty but it works :/
						return;
					}
				}
			}
		}
		else{
			return;
		}
	}

	float CalcDist(Vector3 target, Vector3 seeker){
		return (((target.x-seeker.x)*(target.x-seeker.x))+((target.z-seeker.z)*(target.z-seeker.z)));
	}
	
	//standard bounding circle collision detection same as from Collision assignment.
	bool BoundingCircle( GameObject zomb, GameObject hum){
		//Distance from the center x and y
		//if the square distance is greater than the radius square return true
		if(CalcDist(zomb.transform.position, hum.transform.position)<= (125f)){
			return true;
		}
		//otherwise return false
		return false;
	}

	void Infect(GameObject zombieToBe){
		Transform tempZombie = (Transform) Instantiate(zombieModel,zombieToBe.transform.position,new Quaternion(0f,0f,0f,0f));
		zombies.Add(tempZombie.gameObject);
	}

	void UpdateSeekTargets(){

		for(int i=0; i<zombies.Count; i++){
			if(humans.Count>0){
				zombies[i].GetComponent<Zombie>().targetSeek = humans[0];
			}
			else{
				zombies[i].GetComponent<Zombie>().targetSeek = null;
			}
		}
	}

}
