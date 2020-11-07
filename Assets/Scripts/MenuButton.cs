using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
	[SerializeField] MenuButtonController menuButtonController;
	[SerializeField] Animator animator;
	[SerializeField] AnimatorFunctions animatorFunctions;
	[SerializeField] MenuButton menuButton;
	[SerializeField] int thisIndex;

    // Update is called once per frame
    void Update()
    {
		if(menuButtonController.index == thisIndex)
		{
			animator.SetBool ("selected", true);
			if(Input.GetKeyDown(KeyCode.Return)){
				animator.SetBool ("pressed", true);
				CheckButtons();
			}else if (animator.GetBool ("pressed")){
				animator.SetBool ("pressed", false);
				animatorFunctions.disableOnce = true;
			}
		}else{
			animator.SetBool ("selected", false);
		}
    }

	public void CheckButtons()
	{
		Wait(0.6f, () => {
			//Debug.Log("5 seconds is lost forever");
			checkPlayButton();
			checkRankingButton();
			checkSettingsButton();
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

	void checkPlayButton()
    {
		if (menuButton.GetComponentInChildren<Text>().text.Contains("play"))
		{
			while (animator.GetCurrentAnimatorStateInfo(0).IsName("press"))
			{
				// do something
			}
			SceneManager.LoadScene("GameDesert");
		}
	}

	void checkRankingButton()
    {
		if (menuButton.GetComponentInChildren<Text>().text.Contains("ranking"))
		{
			while (animator.GetCurrentAnimatorStateInfo(0).IsName("press"))
			{
				// do nothing
			}
			SceneManager.LoadScene("Ranking");
		}
	}

	void checkSettingsButton()
    {
		if (menuButton.GetComponentInChildren<Text>().text.Contains("settings"))
		{
			while (animator.GetCurrentAnimatorStateInfo(0).IsName("press"))
			{
				// do nothing
			}
			SceneManager.LoadScene("SettingsMenu");
		}
	}

	void checkQuitButton()
    {
		if (menuButton.GetComponentInChildren<Text>().text.Contains("quit"))
		{
			while (animator.GetCurrentAnimatorStateInfo(0).IsName("press"))
			{
				// do nothing
			}
			Application.Quit();
		}
	}
}
