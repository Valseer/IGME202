using UnityEngine;
using System.Collections.Generic;

public class asteroidSplit : MonoBehaviour {
	public bool isBig=true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void Split(){
		//find scene manager and get the spawnManager from it
		spawnManager mana=GameObject.Find("Scene Manager").GetComponent<spawnManager>();
		//Check if the asteroid has been broken before
		if(isBig){
			//if not create two new small asteroids and add them to the asteroids List
			for(int i=1; i<3;i++){
				mana.asteroids.Remove(gameObject);
				mana.SpawnAsteroidSmall();
				GameObject tempAster= mana.asteroids[mana.asteroids.Count-1];
				tempAster.GetComponent<asteroidSplit>().isBig=false;
				tempAster.transform.position=gameObject.transform.position;
				mana.asteroids[mana.asteroids.Count-1]=tempAster;
			}
			//Destroy this object
			Destroy(gameObject);
		//if the asteroid has been broken before then remove this object from the List and destory it
		}else{
			mana.asteroids.Remove(gameObject);
			Destroy(gameObject);
		}
	}

	public void Hit(){
		//collision stuff goes here
		Split();
	}
}
