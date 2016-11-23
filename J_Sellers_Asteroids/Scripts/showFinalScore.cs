using UnityEngine;
using System.Collections;

public class showFinalScore : MonoBehaviour {
	//script to set a text mesh on the game over screne.
	string text;
	// Use this for initialization
	void Start () {
		text=GameCounter.score.ToString();
		gameObject.GetComponent<TextMesh>().text="Score:" + text;
	}
	
	// Update is called once per frame
	void Update () {
	
	}	
}
