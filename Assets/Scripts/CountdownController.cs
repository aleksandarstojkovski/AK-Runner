using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownController : MonoBehaviour
{
    public int countdownTime;
    public Text countdownDisplay;
    public PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {

        StartCoroutine(waitHalfSec());

        Messenger.Broadcast(GameEvent.PLAY_COUNTDOWN, MessengerMode.DONT_REQUIRE_LISTENER);

        while(countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        countdownDisplay.text = "GO!";

        Messenger.Broadcast(GameEvent.BEGIN_GAME, MessengerMode.DONT_REQUIRE_LISTENER);

        yield return new WaitForSeconds(1f);

        countdownDisplay.gameObject.SetActive(false);

    }

    IEnumerator waitHalfSec() {
        yield return new WaitForSeconds(0.5f);
    }
}
