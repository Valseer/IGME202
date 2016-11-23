﻿using UnityEngine;
using System.Collections;

public class camSwitch : MonoBehaviour {
	// Camera array that holds a reference to every camera in the scene
	public	Camera[] cameras;
	// Current camera 
	private	int	currentCameraIndex;
	// Use this for initialization
	void Start () {
		currentCameraIndex = 0;
		// Turn all cameras off, except the first default one
		for(int	i=1; i < cameras.Length; i++){
			cameras[i].gameObject.SetActive(false);
		}
		// If any cameras were added to the controller, enable the first one
		if(cameras.Length > 0){
			cameras [0].gameObject.SetActive (true);
		}
	}
	// Update is called once per frame
	void Update (){
		// Press the 'C' key to cycle through cameras in the array
		if(Input.GetKeyDown(KeyCode.C))	{
			// Cycle to the next camera
			currentCameraIndex ++;
			// If cameraIndex is in bounds, set this camera active and last one inactive
			if(currentCameraIndex < cameras.Length){
				cameras[currentCameraIndex-1].gameObject.SetActive(false);
				cameras[currentCameraIndex].gameObject.SetActive(true);
			}
			// If last camera, cycle back to first camera
			else{
				cameras[currentCameraIndex-1].gameObject.SetActive(false);
				currentCameraIndex = 0;
				cameras[currentCameraIndex].gameObject.SetActive(true);
			}
		}
	}
	void OnGUI(){
		GUI.color = Color.black;
		GUI.skin.box.fontSize = 20;
		GUI.Label( new Rect(900, 10, 250, 50), "Press the C key to change Camera \nCamera: "+currentCameraIndex +"\n"+ cameras[currentCameraIndex].gameObject.name);
	}


}
