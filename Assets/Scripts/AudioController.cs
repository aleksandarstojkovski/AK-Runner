using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public AudioClip[] jumpAudioClips;
    public AudioClip[] slideAudioClips;
    public AudioClip backgroundMusicAudioClip;
    public AudioSource runningAudioSource;
    public AudioClip runningAudioClip;
    public AudioClip coinAudioClip;
    public AudioSource jumpAndSlideAudioSource;
    public AudioSource backgroundMusicAudioSource;
    public static AudioController Instance = null;

    void Awake()
    {
        // singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    }

    void Start()
    {
        Messenger.AddListener(GameEvent.PAUSE_RUNNING_SOUND, pauseRunning);
        Messenger.AddListener(GameEvent.UNPAUSE_RUNNING_SOUND, unPauseRunning);
        Messenger.AddListener(GameEvent.PLAY_AND_SCHEDULE_RUNNING_SOUND, playAndScheduleRunning);
        Messenger.AddListener(GameEvent.PLAY_SLIDE_SOUND, playSlideSound);
        Messenger.AddListener(GameEvent.PLAY_JUMP_SOUND, playJumpSound);
        Messenger.AddListener(GameEvent.STOP_RUNNING_SOUND, stopRunningSound);
        Messenger.AddListener(GameEvent.PLAY_COIN_SOUND, playCoinSound);
        Messenger<AudioClip>.AddListener(GameEvent.PLAY_SETTINGS_SOUND, playSettingsSound);

        backgroundMusicAudioSource.volume = 0.16f;
        runningAudioSource.volume = 0.30f;

        if (!backgroundMusicAudioSource.isPlaying)
        {
            backgroundMusicAudioSource.PlayOneShot(backgroundMusicAudioClip);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void playAndScheduleRunning() {
        runningAudioSource.PlayOneShot(runningAudioClip);
        runningAudioSource.PlayScheduled(AudioSettings.dspTime + runningAudioClip.length);
    }

    void pauseRunning()
    {
        runningAudioSource.Pause();
    }

    void unPauseRunning()
    {
        runningAudioSource.UnPause();
    }

    void playJumpSound()
    {
        jumpAndSlideAudioSource.Stop();
        jumpAndSlideAudioSource.PlayOneShot(jumpAudioClips[Random.Range(0, jumpAudioClips.Length)]);
    }

    void playSlideSound()
    {
        jumpAndSlideAudioSource.Stop();
        jumpAndSlideAudioSource.PlayOneShot(slideAudioClips[Random.Range(0, slideAudioClips.Length)]);
    }

    void stopRunningSound()
    {
        runningAudioSource.Stop();
    }

    void playSettingsSound(AudioClip audioClip)
    {
        backgroundMusicAudioSource.PlayOneShot(audioClip);
    }

    void playCoinSound()
    {
        backgroundMusicAudioSource.PlayOneShot(coinAudioClip);
    }

}
