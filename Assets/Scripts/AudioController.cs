using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public AudioClip[] jumpAudioClips;
    public AudioClip[] slideAudioClips;
    public AudioClip[] backgroundMusicAudioClips;
    public AudioSource runningAudioSource;
    public AudioClip runningAudioClip;
    public AudioClip coinAudioClip;
    public AudioClip countdown3AudioClip;
    public AudioClip countdown2AudioClip;
    public AudioClip countdown1AudioClip;
    public AudioClip countdownGoAudioClip;
    public AudioClip[] collisionClip;
    public AudioSource jumpAndSlideAudioSource;
    public AudioSource backgroundMusicAudioSource;
    public AudioSource fireworksAudioSource;
    public AudioClip buttonAudioClip;
    public AudioClip escAudioClip;
    public static AudioController Instance = null;
    float backgroundMusicAudioSourceVolume = 0.6f;
    float runningAudioSourceVolume = 1.0f;
    float jumpAndSlideAudioSourceVolume = 1.0f;

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
        Messenger.AddListener(GameEvent.PLAY_COLLISION_OBJECT, playCollision);
        Messenger.AddListener(GameEvent.PLAY_FIREWORKS, playFireworks);
        Messenger.AddListener(GameEvent.STOP_FIREWORKS, stopFireworks);
        Messenger.AddListener(GameEvent.PLAY_BUTTON_SOUND, playSettingsSound);
        Messenger.AddListener(GameEvent.PLAY_ESC_SOUND, playEscSound);

        backgroundMusicAudioSource.volume = backgroundMusicAudioSourceVolume;
        runningAudioSource.volume = runningAudioSourceVolume;
        jumpAndSlideAudioSource.volume = jumpAndSlideAudioSourceVolume;

    }

    // Update is called once per frame
    void Update()
    {
        if (!backgroundMusicAudioSource.isPlaying)
        {
            backgroundMusicAudioSource.PlayOneShot(backgroundMusicAudioClips[Random.Range(0, backgroundMusicAudioClips.Length)]);
        }
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

    void playSettingsSound()
    {
        backgroundMusicAudioSource.PlayOneShot(buttonAudioClip);
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
        Messenger.RemoveListener(GameEvent.PAUSE, pause);
        Messenger.RemoveListener(GameEvent.UNPAUSE, unPause);
        Messenger.RemoveListener(GameEvent.PLAY_COLLISION_OBJECT, playCollision);
        Messenger.RemoveListener(GameEvent.PLAY_FIREWORKS, playFireworks);
        Messenger.RemoveListener(GameEvent.STOP_FIREWORKS, stopFireworks);
        Messenger.RemoveListener(GameEvent.PLAY_BUTTON_SOUND, playSettingsSound);
        Messenger.RemoveListener(GameEvent.PLAY_ESC_SOUND, playEscSound);
    }

    void playCollision()
    {
        backgroundMusicAudioSource.PlayOneShot(collisionClip[Random.Range(0, collisionClip.Length)]);
    }

    void playFireworks()
    {
        fireworksAudioSource.PlayOneShot(fireworksAudioSource.clip);
    }

    void stopFireworks()
    {
        fireworksAudioSource.Stop();
    }

    void playEscSound()
    {
        fireworksAudioSource.PlayOneShot(escAudioClip);
    }

}
