using UnityEngine;
using System.Collections;

public class perlinterrain : MonoBehaviour {
	//terrain data from script parents TerrainCollider
	private TerrainData terrainData;

	//width, depth, height of terrain
	public Vector3 worldSize; //default size 200, 50, 200

	//resolution of height map
	public int resolution; //default 128
	// Use this for initialization
	void Start () {
		//instantiated terrain data with instance from TerrainCollider
		terrainData = gameObject.GetComponent<TerrainCollider> ().terrainData;
		terrainData.size = worldSize;
		terrainData.heightmapResolution = resolution;
		float[,] hMap= new float[128,128];
		for (int x=0; x<resolution-1; x++) {

			for(int y=0; y<resolution-1; y++){
				hMap[x,y]=Mathf.PerlinNoise(Random.value,Random.value);
  			}
		}
		terrainData.SetHeights (0, 0, hMap);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
