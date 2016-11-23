using UnityEngine;
using System.Collections;

public class ClockNumbers : MonoBehaviour {
	public Transform clockNums;
	// Use this for initialization
	void Start () {
		RotateNumbers ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void RotateNumbers(){
		for (int x=1; x<13; x++) {
			int y=(x*30)+90;
			Vector3 pos= new Vector3 (Mathf.Cos (y+((x-1)*30)), Mathf.Sin (y+((x-1)*30)), -10f);
			GameObject num = (GameObject)Instantiate (clockNums, pos, transform.rotation);
			TextMesh text= num.GetComponentsInChildren<TextMesh>()[0];
			text.text=(13-x).ToString();
		}
	}
}
