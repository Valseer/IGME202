using UnityEngine;
using System.Collections;

public class GameCounter : MonoBehaviour {

    public int snowmanCounter = 0;
    private string text;
    // Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        text = snowmanCounter.ToString(); 
	}
    void OnGUI()
    {
        GUI.color = Color.black;
        GUI.skin.box.fontSize = 20;
        GUI.Label( new Rect(10, 10, 150, 50), "Destroyed: " + text);
    }
}
