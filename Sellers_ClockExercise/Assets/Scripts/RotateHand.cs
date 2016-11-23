using UnityEngine;
using System.Collections;

public class RotateHand : MonoBehaviour {
	public bool rotate= false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		RotateToMouse();
	}

	void RotateToMouse(){
		if (rotate) {
			Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			float angle = Mathf.Atan2 (mouseWorldPos.y, mouseWorldPos.x) * Mathf.Rad2Deg;
			Debug.Log (angle);
			transform.rotation = Quaternion.Euler (0, 0, angle);
		}
	}

}
