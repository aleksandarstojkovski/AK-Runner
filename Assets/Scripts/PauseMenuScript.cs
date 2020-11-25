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

	void Start()
	{
		animatorFunctions.disableOnce = false;
	}

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
			pauseMenuUI.SetActive(false);
			Pause.isPaused = false;
			Messenger.Broadcast(GameEvent.UNPAUSE);
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
			SceneManager.LoadScene("MainMenu");
		}
	}
}
