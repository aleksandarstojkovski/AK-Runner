using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuScript : MonoBehaviour
{
    [SerializeField] Scrollbar volumeScrollbar;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Dropdown resolutionDropdown;
    [SerializeField] Dropdown mapsDropdown;
    [SerializeField] Toggle fullscreenToggle;
    [SerializeField] InputField playerName;
    private Resolution[] resolutions;
    private List<string> maps;

    private void Start()
    {
        //Resolutions
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> resOptions = new List<string>();
        Resolution currentResolution = new Resolution();
        currentResolution.width = PlayerPrefs.GetInt(GamePrefs.Keys.SCREEN_WIDTH);
        currentResolution.height = PlayerPrefs.GetInt(GamePrefs.Keys.SCREEN_HEIGHT);
        currentResolution.refreshRate = 60;
        // Debug.Log("Risoluzione: " + currentResolution);

        foreach(Resolution r in resolutions){
            resOptions.Add(r.width + " x " + r.height + " pixel");

            if (r.Equals(currentResolution)) currentResolution = r;
        }

        resolutionDropdown.AddOptions(resOptions);
        resolutionDropdown.value = resOptions.IndexOf(currentResolution.width + " x " + currentResolution.height + " pixel");
        resolutionDropdown.RefreshShownValue();

        //Fullscreen
        if (PlayerPrefs.HasKey(GamePrefs.Keys.SCREEN_IS_FULLSCREEN))
        {
            int val = PlayerPrefs.GetInt(GamePrefs.Keys.SCREEN_IS_FULLSCREEN);
            bool v = Convert.ToBoolean(val);
            fullscreenToggle.isOn = v;            
        }
        else
        {
            fullscreenToggle.isOn = false;
            SetFullscreen(false);
        }

        //Maps
        mapsDropdown.ClearOptions();
        
        maps = new List<string>();
        maps.Add("Desert Map");

        if (PlayerPrefs.GetInt(GamePrefs.Keys.SHOP_STREETMAP_BOUGHT,0)==1)
            maps.Add("Street Map");

        //maps.Add("Test Map");

        mapsDropdown.AddOptions(maps);
        mapsDropdown.value = maps.IndexOf(PlayerPrefs.GetString(GamePrefs.Keys.CURRENT_MAP_NAME) + " Map");

        //Volume
        if (PlayerPrefs.HasKey(GamePrefs.Keys.CURRENT_GAME_VOLUME))
        {
            volumeScrollbar.GetComponent<Scrollbar>().value = PlayerPrefs.GetFloat(GamePrefs.Keys.CURRENT_GAME_VOLUME);
        } else
        {
            PlayerPrefs.SetFloat(GamePrefs.Keys.CURRENT_GAME_VOLUME, 1.0f);
        }

        //Player name
        if (PlayerPrefs.HasKey(GamePrefs.Keys.PLAYER_NAME))
        {
            playerName.placeholder.GetComponent<Text>().text = PlayerPrefs.GetString(GamePrefs.Keys.PLAYER_NAME);
        } else
        {
            PlayerPrefs.SetString(GamePrefs.Keys.PLAYER_NAME, "Unknown");
            playerName.placeholder.GetComponent<Text>().text = PlayerPrefs.GetString(GamePrefs.Keys.PLAYER_NAME);
        }
    }

    public void SetVolume()
    {
        //audioMixer.SetFloat("volume", volumeScrollbar.GetComponent<Scrollbar>().value);
        AudioListener.volume = volumeScrollbar.GetComponent<Scrollbar>().value;
        PlayerPrefs.SetFloat(GamePrefs.Keys.CURRENT_GAME_VOLUME, volumeScrollbar.GetComponent<Scrollbar>().value);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        PlayerPrefs.SetInt(GamePrefs.Keys.SCREEN_IS_FULLSCREEN, Convert.ToInt32(isFullscreen));
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution()
    {
        Resolution resolution = resolutions[resolutionDropdown.value];
        PlayerPrefs.SetInt(GamePrefs.Keys.SCREEN_WIDTH, resolution.width);
        PlayerPrefs.SetInt(GamePrefs.Keys.SCREEN_HEIGHT, resolution.height);

        Screen.SetResolution(PlayerPrefs.GetInt(GamePrefs.Keys.SCREEN_WIDTH), PlayerPrefs.GetInt(GamePrefs.Keys.SCREEN_HEIGHT), fullscreenToggle.isOn);

        // Debug.Log("Indice: " + resolutionDropdown.value);
        // Debug.Log("Risoluzione impostata: " + PlayerPrefs.GetInt(GamePrefs.Keys.SCREEN_WIDTH) + PlayerPrefs.GetInt(GamePrefs.Keys.SCREEN_HEIGHT));
        // Debug.Log("Fullscreen: " + PlayerPrefs.GetInt(GamePrefs.Keys.SCREEN_IS_FULLSCREEN));
    }

    public void SetMap()
    {
        //Debug.Log(mapsDropdown.GetComponent<Dropdown>().captionText.text);
        //Debug.Log(mapsDropdown.GetComponent<Dropdown>().captionText.text.Split(' ').First<string>());
        PlayerPrefs.SetString(GamePrefs.Keys.CURRENT_MAP_NAME, mapsDropdown.GetComponent<Dropdown>().captionText.text.Split(' ').First<string>());
    }

    public void SetPlayerName()
    {
        PlayerPrefs.SetString(GamePrefs.Keys.PLAYER_NAME, playerName.text);
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Messenger.Broadcast(GameEvent.RELOAD_SCORE_CONTROLLER);
    }

}
