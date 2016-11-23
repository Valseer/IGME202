using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class textupdat : MonoBehaviour {

    public Text Instructions;
	// Use this for initialization
	void Start () {
        Instructions = GameObject.Find("CounterText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
