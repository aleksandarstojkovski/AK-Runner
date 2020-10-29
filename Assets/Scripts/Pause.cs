using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour
{

    bool Paused = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused == true)
            {

                Paused = false;
            }
            else
            {

                Paused = true;
            }
        }
    }
    public void Resume()
    {

    }

}