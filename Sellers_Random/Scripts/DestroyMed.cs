using UnityEngine;
using System.Collections;

public class DestroyMed : MonoBehaviour {
	private GameCounter gc;
	public AudioClip[] clips = new AudioClip[1];
	private AudioSource[] sources= new AudioSource[1];
	// Use this for initialization
	void Start ()
	{
		int i = 0;
		while (i<1) {
			sources [i] = gameObject.AddComponent<AudioSource> ();
			i++;
		}
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
		gc = GameObject.Find("CamManager").GetComponent<GameCounter>();
		gc.score+=2; 
		for(int i=0;i<1;i++){
			sources[i].clip=clips[i];
			AudioSource.PlayClipAtPoint(clips[i],Camera.main.transform.position);
		}
		Destroy(gameObject);
	}
}
