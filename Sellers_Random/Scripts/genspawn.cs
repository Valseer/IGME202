using UnityEngine;
using System.Collections;

public class genspawn : MonoBehaviour {
	public Transform zomb;
	// Use this for initialization
	void Start () {
		spawnGauss ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void spawnGauss(){
		Transform[] zombs= new Transform[9];
		
		for (int x=0; x<9; x++) {
			Vector3 pos = new Vector3 (100, Terrain.activeTerrain.SampleHeight (new Vector3 (100, 0, 70-(x*5))), 70-(x*5));
			zombs[x]= (Transform)Instantiate (zomb, pos, new Quaternion(0f,90f,0f,0f));
			Vector3 lScale=zombs[x].transform.localScale;
			lScale.x= lScale.x*Random.Range(.5f,2f);
			lScale.y= lScale.y*Random.Range(.5f,2f);
			lScale.z= lScale.z*Random.Range(.5f,2f);
			zombs[x].transform.localScale=lScale;
		}
	}
}
