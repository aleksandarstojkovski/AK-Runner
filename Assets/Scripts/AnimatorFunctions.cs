using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorFunctions : MonoBehaviour
{
	[SerializeField] MenuButtonController menuButtonController;
	public bool disableOnce;

	void PlaySound(AudioClip whichSound){
		if(!disableOnce){
			Messenger<AudioClip>.Broadcast(GameEvent.PLAY_SETTINGS_SOUND,whichSound,MessengerMode.DONT_REQUIRE_LISTENER);
		}else{
			disableOnce = false;
		}
	}
}	
