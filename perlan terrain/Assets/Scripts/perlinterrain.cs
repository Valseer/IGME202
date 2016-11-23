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
		float perlinX = 1.0f+Time.time;
		float perlinY = 1.0f+Time.time;
		for (int x=0; x<resolution-1; x++) {
			perlinX= 1.0f;
			perlinY+= 0.025f;
			for(int y=0; y<resolution-1; y++){
				perlinX+=0.025f;
				hMap[x,y]=Mathf.PerlinNoise(perlinX,perlinY);
  			}
		}
		terrainData.SetHeights (0, 0, hMap);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
