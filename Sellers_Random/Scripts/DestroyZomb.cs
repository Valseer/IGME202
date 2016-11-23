using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestroyZomb : MonoBehaviour {
    private GameCounter gc;
	public AudioClip[] clips = new AudioClip[2];
	private AudioSource[] sources= new AudioSource[2];
    // Use this for initialization
    void Start ()
    {
		int i = 0;
		while (i<2) {
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
     void OnMouseDown(){
        gc = GameObject.Find("CamManager").GetComponent<GameCounter>();
        gc.score++; 
		for(int i=0;i<2;i++){
			sources[i].clip=clips[i];
			AudioSource.PlayClipAtPoint(clips[i],Camera.main.transform.position);
		}
        Destroy(gameObject);		
	}

}
