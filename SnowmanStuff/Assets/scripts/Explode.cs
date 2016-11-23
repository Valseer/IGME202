using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Explode : MonoBehaviour {
    public GameObject top;
    public GameObject middle;
    public GameObject bottom;
    public Terrain manger;
    private GameCounter gc;
    public bool exploded = false;

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	} 

    /*
     * 
     */
     void OnMouseDown()
    {
        if (!exploded)
        {
            top.AddComponent<Rigidbody>();
            middle.AddComponent<Rigidbody>();
            bottom.AddComponent<Rigidbody>();
            gc = GameObject.Find("SceneManager").GetComponent<GameCounter>();
            gc.snowmanCounter++;
            exploded= !exploded;
        }
        
        //Destroy(gameObject);
    }


}
