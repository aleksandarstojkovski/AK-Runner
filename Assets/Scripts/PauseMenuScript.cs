using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
	[SerializeField] MenuButtonController menuButtonController;
	[SerializeField] Animator animator;
	[SerializeField] AnimatorFunctions animatorFunctions;
	[SerializeField] MenuButton menuButton;
	[SerializeField] private GameObject pauseMenuUI;

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
				animator.SetBool("selected", false);
				animatorFunctions.disableOnce = true;
			}
		}
		else
		{
			animator.SetBool("selected", false);
			animator.SetBool("pressed", false);
		}
	}

	public void CheckButtons()
	{
		checkResumeButton();
		checkQuitButton();
	}

	void checkResumeButton()
	{
		if (menuButton.GetComponentInChildren<Text>().text.Contains("resume"))
		{
			while (animator.GetCurrentAnimatorStateInfo(0).IsName("press"))
			{
				// do nothing
			}
			Time.timeScale = 1;
			AudioListener.pause = false;
			pauseMenuUI.SetActive(false);
			Pause.isPaused = false;
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
			Time.timeScale = 1;
			AudioListener.pause = false;
			pauseMenuUI.SetActive(false);
			Pause.isPaused = false;
			SceneManager.LoadScene("MainMenu");
		}
	}
}
