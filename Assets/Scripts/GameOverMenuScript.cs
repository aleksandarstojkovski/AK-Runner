using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenuScript : MonoBehaviour
{
	[SerializeField] MenuButtonController menuButtonController;
	[SerializeField] Animator animator;
	[SerializeField] AnimatorFunctions animatorFunctions;
	[SerializeField] MenuButton menuButton;
	[SerializeField] int thisIndex;

	// Update is called once per frame
	void Update()
	{
		if (menuButtonController.index == thisIndex)
		{
			animator.SetBool("selected", true);
			if (Input.GetKeyDown(KeyCode.Return))
			{
				animator.SetBool("pressed", true);
				CheckButtons();
			}
			else if (animator.GetBool("pressed"))
			{
				animator.SetBool("pressed", false);
				animatorFunctions.disableOnce = true;
			}
		}
		else
		{
			animator.SetBool("selected", false);
		}
	}

	public void CheckButtons()
	{
		Wait(0.6f, () => {
			checkQuitButton();
		});
	}

	public void Wait(float seconds, Action action)
	{
		StartCoroutine(_wait(seconds, action));
	}
	IEnumerator _wait(float time, Action callback)
	{
		yield return new WaitForSeconds(time);
		callback();
	}

	void checkQuitButton()
	{
		if (menuButton.GetComponentInChildren<Text>().text.Contains("retry"))
		{
			while (animator.GetCurrentAnimatorStateInfo(0).IsName("press"))
			{
				// do nothing
			}
			SceneManager.LoadScene("Game"+PlayerPrefs.GetString(GamePrefs.Keys.CURRENT_MAP_NAME));
		}
		if (menuButton.GetComponentInChildren<Text>().text.Contains("quit"))
		{
			while (animator.GetCurrentAnimatorStateInfo(0).IsName("press"))
			{
				// do nothing
			}
			SceneManager.LoadScene("MainMenu");
		}
	}
}
