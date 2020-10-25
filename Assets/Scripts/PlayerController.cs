using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Newtonsoft.Json;

public class PlayerController : MonoBehaviour
{

    public bool jump = false;
    public bool slide = false;
    private bool isGrounded = true;
    public bool dead = false;

    public Animator animator;
    private Rigidbody rigidBody;
    public GameObject trigger;
    public LayerMask Ground;
    public Transform groundChecker;
    public PlayerMetadata playerMetadata;
    public AudioSource runningAudioSource;
    public AudioClip runningAudioClip;
    public AudioSource jumpAndSlideAudioSource;
    public AudioClip[] jumpAudioClips;
    public AudioClip[] slideAudioClips;

    public float jumpHeight = 1f;
    public float groundDistance = 0.2f;
    public float speed = 8f;
    public float speedIncrement = 0.2f;
    public float animationSpeedIncrement = 0.007f;
    public float currentScore = 0;
    public float currentCoins = 0;
    public float recordScore = 0;

    public List<PlayerMetadata> ranking = new List<PlayerMetadata>();

    bool AnimatorIsPlaying(Animator animator, int layerIndex, string stateName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    float AnimatorProgressPercentage(Animator animator, int layerIndex, string stateName)
    {
        return (animator.GetCurrentAnimatorStateInfo(0).normalizedTime / animator.GetCurrentAnimatorStateInfo(0).length) * 100;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        groundChecker = GetComponent<Transform>();
        currentCoins = Mathf.Round(PlayerPrefs.GetFloat("coins", 0));
        recordScore = Mathf.Round(PlayerPrefs.GetFloat("recordScore", 0));
        playerMetadata = new PlayerMetadata(currentCoins, currentScore, "Player1");

        runningAudioSource.PlayOneShot(runningAudioClip);
        runningAudioSource.PlayScheduled(AudioSettings.dspTime + runningAudioClip.length);
        runningAudioSource.volume = 0.1f;

        animator.SetBool("isJump", jump);
        animator.SetBool("isSlide", slide);
    }

    void updatePlayerMetadata() {
        playerMetadata.update(currentCoins, currentScore);
        Messenger<PlayerMetadata>.Broadcast(GameEvent.UPDATE_METADATA, playerMetadata);
    }

    void playAudio() {
        // if jump or slide animation is in progress, stop running audio
        if (( AnimatorIsPlaying(animator,0,"BaseLayer.Jump") || AnimatorIsPlaying(animator, 0, "BaseLayer.Running Slide")) && AnimatorProgressPercentage(animator,0, "BaseLayer.Jump")<80)
        {
            runningAudioSource.Pause();
        }
        else
        {
            runningAudioSource.UnPause();
        }

    }

    // Update is called once per frame
    void Update()
    {

        updatePlayerMetadata();

        playAudio();

        if (dead == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        currentScore += 5 * Time.deltaTime;

        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, Ground, QueryTriggerInteraction.Ignore);

        // move character
        transform.Translate(0, 0, speed*Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jump = true;
            jumpAndSlideAudioSource.Stop();
            jumpAndSlideAudioSource.PlayOneShot(jumpAudioClips[Random.Range(0, jumpAudioClips.Length)]);
            animator.Play("Jump");
        } 
        else
        {
            jump = false;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            slide = true;
            jumpAndSlideAudioSource.Stop();
            jumpAndSlideAudioSource.PlayOneShot(slideAudioClips[Random.Range(0, slideAudioClips.Length)]);
            animator.Play("Running Slide");
        }
        else
        {
            slide = false;
        }

        animator.SetBool("isJump", jump);
        animator.SetBool("isSlide", slide);

        if (jump == true && isGrounded)
        {
            rigidBody.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }

        
        if (slide == true && !isGrounded)
        {
            rigidBody.AddForce(Vector3.down * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }

    }

    void updateJson() {
        ranking.Add(playerMetadata);
        Debug.Log(JsonConvert.SerializeObject(ranking).ToString());
    }

    void OnTriggerEnter(Collider other) {

        if (other.gameObject.tag == "Player") {
            Destroy(trigger.gameObject);
        }

        if (other.gameObject.tag == "Coin") {
            Destroy(other.gameObject,0.5f);
            currentCoins += 1f;
            speed += speedIncrement;
            animator.speed += animationSpeedIncrement;
        }

        if (other.gameObject.tag == "Obstacle")
        {
            dead = true;
            if (currentScore > recordScore) {
                recordScore = currentScore;
                Messenger<float>.Broadcast(GameEvent.NEW_RECORD, recordScore);
            }
            updateJson();
        }

    }

}
