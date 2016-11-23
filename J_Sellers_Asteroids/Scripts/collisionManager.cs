using UnityEngine;
using System.Collections;

public class collisionManager : MonoBehaviour {
	public GameObject manager;
	private spawnManager scene;
	// Use this for initialization
	void Start () {
		scene=manager.GetComponent<spawnManager>();
	}
	
	// Update is called once per frame
	void Update () {
		CheckCollisions();
	}

	void CheckCollisions(){
		//ship collides with asteroid
		for(int i=0; i<scene.asteroids.Count;i++){
			//get the ship and asteroid in question
			GameObject tempAst= (GameObject)scene.asteroids[i];
			GameObject ship= (GameObject)scene.ship;
			//do they collide?
			if(BoundingCircle(tempAst,ship)){
				//do you have lives left?
				if(scene.lives>1){
					//respawn and decrement lives
					scene.Respawn();
					scene.lives--;
				}
				//otherwise call the game is over
				else{
					gameObject.GetComponent<GameCounter>().GameOver();
				}

			}
		}

		//asteroid collides with shot
		for(int i=0; i<scene.asteroids.Count;i++){
			for(int j=0; j<scene.ship.GetComponent<vehicle>().shots.Count;j++){
				//get the asteroid and shot in question
				GameObject shot= (GameObject)scene.ship.GetComponent<vehicle>().shots[j];
				GameObject tempAst=(GameObject)scene.asteroids[i];
				//did they collide?
				if(BoundingCircle(tempAst,shot)){
					//Increase Score 100 if big 200 if small
					if(tempAst.GetComponent<asteroidSplit>().isBig){
						GameCounter.score+=100;
					}
					else{
						GameCounter.score+=200;
					}
					//call the hit command to denote a hit
					tempAst.GetComponent<asteroidSplit>().Hit();
					//remove the asteroid from reference
					scene.asteroids.Remove(tempAst);
					//remove the shot from reference
					GameObject.Find("Spaceship").GetComponent<vehicle>().shots.Remove(shot);
					//destroy the shot as well
					Destroy(shot);
					//break out to prevent issues with null exceptions. not pretty but it works :/
					return;
				}
			}
		}
	}

	//standard bounding circle collision detection same as from Collision assignment.
	bool BoundingCircle( GameObject colA, GameObject colB){
		SpriteRenderer a = colA.GetComponent<SpriteRenderer> ();
		SpriteRenderer b = colB.GetComponent<SpriteRenderer> ();
		//Distance from the center x and y
		float distX= a.bounds.center.x - b.bounds.center.x;
		float distY= a.bounds.center.y - b.bounds.center.y;
		//Square distance for faster calc
		float squareDist = (distX * distX) + (distY * distY);
		//"radius" from center to outside
		float r1 = a.bounds.max.x - a.bounds.center.x;
		float r2 = b.bounds.max.x - b.bounds.center.x;
		//if the square distance is greater than the radius square return true
		if(squareDist<= ((r1+r2)*(r1+r2))){
			return true;
		}
		//otherwise return false
		return false;
	}
}
