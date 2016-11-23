using UnityEngine;
using System.Collections;

public class GameCounter : MonoBehaviour {

    public static int score = 0;
    private string text;
	private int lives;
	public static bool isClassic;
    // Use this for initialization
	void Start () {
		isClassic=false;
	}
	
	// Update is called once per frame
	void Update () {
		//updates from static pieces of data
        text = GameCounter.score.ToString(); 
		lives= GameObject.Find("Scene Manager").GetComponentInParent<spawnManager>().lives;
	}
    void OnGUI()
    {
		//creates a GUI to show lives and score of the current game
        GUI.color = Color.white;
        GUI.skin.box.fontSize = 50;
        GUI.Label( new Rect(5, 5, 150, 50), "Lives: " + lives);
		GUI.Label (new Rect(1220,5,150,50),"Score:"+ text);
    }

	public void GameOver(){
		//Loads the GameOver scene
		Application.LoadLevel(3);
	}
}
