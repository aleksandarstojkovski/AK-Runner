using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonController : MonoBehaviour {

	// Use this for initialization
	public int index;
	public int maxIndex;

	void Start () {
		index = 0;
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
