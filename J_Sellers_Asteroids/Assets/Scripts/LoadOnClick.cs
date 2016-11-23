using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour {
	public AudioClip[] clips = new AudioClip[2];
	private AudioSource[] sources= new AudioSource[2];
	private bool played= false;
	public int levelToLoad;
	// Use this for initialization
	void Start () {
		int i = 0;
		//initialize incoming clips as sounds
		while (i<2) {
			sources [i] = gameObject.AddComponent<AudioSource> ();
			i++;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		//on mouse down play a sound
		sources[0].clip=clips[0];
		AudioSource.PlayClipAtPoint(clips[0],Camera.main.transform.position);
		//if the game isClassic ensure it reloads as classic on restart
		if(GameCounter.isClassic==true){
			Application.LoadLevel(2);
		}
		if(levelToLoad==2){
			GameCounter.isClassic=true;
		}
		//otherwise the game stays not classic
		else{
			GameCounter.isClassic=false;
		}
		//load the next level
		Application.LoadLevel(levelToLoad);
	}

	//play a sound on mouse over only if you havent played it yet
	void OnMouseOver(){
		if(!played){
			sources[1].clip=clips[1];
			AudioSource.PlayClipAtPoint(clips[1],Camera.main.transform.position);
		}
		played=true;
	}
	//on exit of the hitbox mark the sound unplayed. didnt use my own collision because it would have been an extra step and was much more complex.
	void OnMouseExit(){
		played=false;
	}
}
