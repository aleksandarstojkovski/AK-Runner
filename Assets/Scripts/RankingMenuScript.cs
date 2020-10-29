using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RankingMenuScript : MonoBehaviour
{
    public void Quit() {
        SceneManager.LoadScene("MainMenu");
    }
}
