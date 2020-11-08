using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuScript : MonoBehaviour
{
    [SerializeField] Scrollbar volumeScrollbar;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Dropdown resolutionDropdown;
    [SerializeField] Dropdown mapsDropdown;
    private Resolution[] resolutions;
    private List<string> maps;

    private void Start()
    {
        //Player default Map
        if (!PlayerPrefs.HasKey("map"))
        {
            PlayerPrefs.SetString("map", "Desert");
        }

        //Resolutions
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> resOptions = new List<string>();
        Resolution currentResolution = Screen.currentResolution;

        foreach(Resolution r in resolutions){
            resOptions.Add(r.width + " x " + r.height + " pixel");

            if (r.Equals(currentResolution)) currentResolution = r;
        }

        resolutionDropdown.AddOptions(resOptions);
        resolutionDropdown.value = resOptions.IndexOf(currentResolution.width + " x " + currentResolution.height + " pixel");
        resolutionDropdown.RefreshShownValue();

        //Maps
        mapsDropdown.ClearOptions();
        
        maps = new List<string>();
        maps.Add("Desert Map");
        maps.Add("Street Map");
        maps.Add("Test Map");

        mapsDropdown.AddOptions(maps);
        mapsDropdown.value = maps.IndexOf(PlayerPrefs.GetString("map") + " Map");
    }

    public void SetVolume()
    {
        audioMixer.SetFloat("volume", volumeScrollbar.GetComponent<Scrollbar>().value);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.width, Screen.fullScreen);
    }

    public void SetMap()
    {
        //Debug.Log(mapsDropdown.GetComponent<Dropdown>().captionText.text);
        //Debug.Log(mapsDropdown.GetComponent<Dropdown>().captionText.text.Split(' ').First<string>());
        PlayerPrefs.SetString("map", mapsDropdown.GetComponent<Dropdown>().captionText.text.Split(' ').First<string>());
    }
}
