using UnityEngine;
using System.Collections;

public class collision : MonoBehaviour {
	public GameObject[] sprites;
	private bool abOrBc= true; // True is AABB False is BoundingCircle defaults to AABB
	private bool collide= true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		ToggleType ();
		//axis aligned bounding box
		if (abOrBc) {
			collide= AABB (sprites [0], sprites [1]);
		//bounding circles
		} else {
			collide= BoudingCircle(sprites[0], sprites[1]);
		}
		ChangeColor (sprites [0], sprites [1]);
	}

	bool AABB(GameObject colA, GameObject colB){
		SpriteRenderer a = colA.GetComponent<SpriteRenderer> ();
		SpriteRenderer b = colB.GetComponent<SpriteRenderer> ();
		//Top of a is in Bottom of b
		if ((a.bounds.max.y > b.bounds.min.y)) {
			//Right of a is in the Left of b
			if ((a.bounds.max.x > b.bounds.min.x)&&!(a.bounds.max.x > b.bounds.max.x)) {
				return true;
			}
			if ((a.bounds.min.x < b.bounds.max.x)&&!(a.bounds.min.x < b.bounds.min.x)) {
				return true;
			}

		//Bottom of a is in Top of b
		} else if ((a.bounds.min.y < b.bounds.max.y)) {
			//Right of a is in the Left of b and the Right of a is not outside the Right of b return true
			if ((a.bounds.max.x > b.bounds.min.x)&&!(a.bounds.max.x > b.bounds.max.x)) {
				return true;
			}
			//Left of a is in the Right of b and the Left of a is not outside the Left of b retrun true
			if ((a.bounds.min.x < b.bounds.max.x)&&!(a.bounds.min.x < b.bounds.min.x)) {
				return true;
			}
		} 
		//otherwise return false
		return false;
	}

	bool BoudingCircle( GameObject colA, GameObject colB){
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
		if(squareDist<= (r1+r2)*(r1+r2)){
			return true;
		}
		//otherwise return false
		return false;
	}

	void ToggleType(){
		//if you click 1 the collide goes to axis alligned
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			abOrBc= true;
		}
		//if you click 2 the collide goes to bounding circle
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			abOrBc= false;
		}
	}
	void ChangeColor(GameObject colA, GameObject colB){
		//if colliding change to red
		if (collide) {
			colA.GetComponent<SpriteRenderer> ().color = Color.red;
			colB.GetComponent<SpriteRenderer> ().color = Color.red;
		}
		//otherwise set the collors to default
		else {
			colA.GetComponent<SpriteRenderer> ().color = Color.white;
			colB.GetComponent<SpriteRenderer> ().color = Color.white;
		}
	}
}
