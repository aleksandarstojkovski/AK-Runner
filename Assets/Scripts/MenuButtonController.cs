using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonController : MonoBehaviour {

	// Use this for initialization
	public int index;
	public int maxIndex;
	public AudioSource audioSource;

	void Start () {
		audioSource = GetComponent<AudioSource>();
		index = 0;
		maxIndex = 3;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			if(index < maxIndex){
				index++;
			}else{
				index = 0;
			}
		} else if(Input.GetKeyDown(KeyCode.UpArrow)) {
			if(index > 0){
				index --; 
			} else {
				index = maxIndex;
			}
		}
	}

}
