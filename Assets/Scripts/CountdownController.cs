using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownController : MonoBehaviour
{
    public int countdownTime;
    public Text countdownDisplay;
    List<string> countdownList = new List<string>() { GameEvent.PLAY_COUNTDOWN_1, GameEvent.PLAY_COUNTDOWN_2, GameEvent.PLAY_COUNTDOWN_3 };

    IEnumerator Start()
    {
        
        countdownDisplay.text = "";

        yield return new WaitForSeconds(3f);

        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        while(countdownTime > 0)
        {

            Messenger.Broadcast(countdownList[countdownTime-1], MessengerMode.DONT_REQUIRE_LISTENER);

            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1.1f);

            countdownTime--;
        }

        Messenger.Broadcast(GameEvent.PLAY_COUNTDOWN_GO, MessengerMode.DONT_REQUIRE_LISTENER);

        countdownDisplay.text = "GO!";

        Messenger.Broadcast(GameEvent.BEGIN_GAME, MessengerMode.DONT_REQUIRE_LISTENER);

        yield return new WaitForSeconds(1f);

        countdownDisplay.gameObject.SetActive(false);

    }

}
