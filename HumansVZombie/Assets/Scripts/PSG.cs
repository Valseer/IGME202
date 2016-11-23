using UnityEngine;
using System.Collections;

public class PSG : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Respawn(){
		transform.position= new Vector3(Random.Range (-300, 300f), 5f, Random.Range(-300f, 300f));
	}

}
