using UnityEngine;
using System.Collections.Generic;

public class spawnManager : MonoBehaviour {
	public Transform[] asteroid;
	public Transform[] asteroidSmall;
	public GameObject ship;
	public Transform life;
	public List<GameObject> asteroids;
	private List<GameObject> lifeModels;
	private float heightMin = -3.5f;
	private float heightMax = 3.5f;
	public int maxAsteroids=5;
	public int asterCount=0;
	public int lives=3;
	// Use this for initialization
	void Start () {
		asteroids = new List<GameObject>();
		lifeModels= new List<GameObject>();
		StartSpawnAsteroid();
		StartSpawnLives();
	}
	
	// Update is called once per frame
	void Update () {
		SpawnAsteroid();
		asterCount=asteroids.Count;
	}
	//spawns 5 asteroids to start
	private void StartSpawnAsteroid(){
		for(int i=0; i<maxAsteroids;i++){
			SpawnAsteroid();
		}
	}

	//spawns a random asteroid at a random location and random speed
	//adds the asteroid to a List to be tracked
	private void SpawnAsteroid(){
		if(asteroids.Count<maxAsteroids){
			Vector3 pos = SpawnLocation ();
			Transform tempObj=(Transform) Instantiate(randomAster(asteroid), pos, new Quaternion(0f,0f,0f,0f));
			asteroids.Add(tempObj.gameObject);
		}
	}

	//repeats much of SpawnAsteroid() but utalized only when splitting asteroids
	public void SpawnAsteroidSmall(){
			Vector3 pos = SpawnLocation ();
			Transform tempObj=(Transform)Instantiate (randomAster(asteroidSmall), pos, new Quaternion(0f,0f,0f,0f));
			asteroids.Add(tempObj.gameObject);
	}

	//gets a random asteroid from the List
	private Transform randomAster(Transform[] aster){
		float pick= Random.Range(0f,3f);
		Transform tempAster;
		if(pick<1f){
			tempAster=aster[0];
			return tempAster;
		}
		else if(pick<2f){
			tempAster=aster[1];
			return tempAster;
		}
		else{
			tempAster=aster[2];
			return tempAster;
		}
	}

	//"Respawns" the ship upon death
	public void Respawn(){
		ship.GetComponent<vehicle>().Reset();
		GameObject curLife=lifeModels[lives-1];
		lifeModels.Remove(curLife);
		Destroy(curLife);
	}


	//returns a random spawn location on the outer edge of the camera view.
	private Vector3 SpawnLocation(){
		float leftOrRight = Random.value;
		float randomHeight = Random.Range (-5f, 5f);

		if (heightMin < randomHeight && randomHeight< heightMax) {
			//middle
			if (leftOrRight > .5f) {
				//left
				return new Vector3 (Random.Range (-10f, -7f), randomHeight);
			} else {
				//right
				return new Vector3 (Random.Range (7f, 10f), randomHeight);
			}
		} else {
			//not middle
			return new Vector3(Random.Range(-10f,10f),randomHeight);
		}
	}

	//sets the spawn lives and makes them apper on the corner of the screen
	private void StartSpawnLives(){
		for(int i=0; i<lives; i++){
			float xPos= (.8f*i)-12.2f;
			Vector3 pos = new Vector3(xPos,4f,0);
			Transform tempObj=(Transform)Instantiate (life, pos, new Quaternion(0f,0f,0f,0f));
			lifeModels.Add(tempObj.gameObject);
		}
	}
}
