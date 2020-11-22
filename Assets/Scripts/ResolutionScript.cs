using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionScript : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.HasKey(GamePrefs.Keys.SCREEN_HEIGHT))
        {
          Screen.SetResolution(PlayerPrefs.GetInt(GamePrefs.Keys.SCREEN_WIDTH), PlayerPrefs.GetInt(GamePrefs.Keys.SCREEN_HEIGHT), Screen.fullScreen, 60);
        } else
        {
            PlayerPrefs.SetInt(GamePrefs.Keys.SCREEN_WIDTH, 1280);
            PlayerPrefs.SetInt(GamePrefs.Keys.SCREEN_HEIGHT, 720);
            
            bool isFullScreen = false; // should be windowed to run in arbitrary resolution
            Debug.Log("Altezza: " + PlayerPrefs.GetInt(GamePrefs.Keys.SCREEN_HEIGHT));
            Debug.Log("Larghezza: " + PlayerPrefs.GetInt(GamePrefs.Keys.SCREEN_WIDTH));

            Screen.SetResolution(1280, 720, isFullScreen, 60);
        }

        // set the desired aspect ratio (the values in this example are
        // hard-coded for 16:9, but you could make them into public
        // variables instead so you can set them at design time)
        float targetaspect = 16.0f / 9.0f;

        // determine the game window's current aspect ratio
        float windowaspect = (float)Screen.width / (float)Screen.height;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetaspect;

        // obtain camera component so we can modify its viewport
        Camera camera = GetComponent<Camera>();

        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            camera.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }
}
