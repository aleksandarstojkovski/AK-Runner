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
    public AudioClip countdown3AudioClip;
    public AudioClip countdown2AudioClip;
    public AudioClip countdown1AudioClip;
    public AudioClip countdownGoAudioClip;
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
        Messenger.AddListener(GameEvent.PLAY_COUNTDOWN_3, playCountdown3);
        Messenger.AddListener(GameEvent.PLAY_COUNTDOWN_2, playCountdown2);
        Messenger.AddListener(GameEvent.PLAY_COUNTDOWN_1, playCountdown1);
        Messenger.AddListener(GameEvent.PLAY_COUNTDOWN_GO, playCountdownGo);
        Messenger.AddListener(GameEvent.PAUSE, pause);
        Messenger.AddListener(GameEvent.UNPAUSE, unPause);
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

    void playCountdown3()
    {
        backgroundMusicAudioSource.PlayOneShot(countdown3AudioClip);
    }

    void playCountdown2()
    {
        backgroundMusicAudioSource.PlayOneShot(countdown2AudioClip);
    }
    void playCountdown1()
    {
        backgroundMusicAudioSource.PlayOneShot(countdown1AudioClip);
    }
    void playCountdownGo()
    {
        backgroundMusicAudioSource.PlayOneShot(countdownGoAudioClip);
    }

    void pause() {
        runningAudioSource.enabled = false;
        jumpAndSlideAudioSource.enabled = false;
    }

    void unPause() {
        runningAudioSource.enabled = true;
        jumpAndSlideAudioSource.enabled = true;
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.PAUSE_RUNNING_SOUND, pauseRunning);
        Messenger.RemoveListener(GameEvent.UNPAUSE_RUNNING_SOUND, unPauseRunning);
        Messenger.RemoveListener(GameEvent.PLAY_AND_SCHEDULE_RUNNING_SOUND, playAndScheduleRunning);
        Messenger.RemoveListener(GameEvent.PLAY_SLIDE_SOUND, playSlideSound);
        Messenger.RemoveListener(GameEvent.PLAY_JUMP_SOUND, playJumpSound);
        Messenger.RemoveListener(GameEvent.STOP_RUNNING_SOUND, stopRunningSound);
        Messenger.RemoveListener(GameEvent.PLAY_COIN_SOUND, playCoinSound);
        Messenger.RemoveListener(GameEvent.PLAY_COUNTDOWN_3, playCountdown3);
        Messenger.RemoveListener(GameEvent.PLAY_COUNTDOWN_2, playCountdown2);
        Messenger.RemoveListener(GameEvent.PLAY_COUNTDOWN_1, playCountdown1);
        Messenger.RemoveListener(GameEvent.PLAY_COUNTDOWN_GO, playCountdownGo);
        Messenger<AudioClip>.RemoveListener(GameEvent.PLAY_SETTINGS_SOUND, playSettingsSound);
    }

}
