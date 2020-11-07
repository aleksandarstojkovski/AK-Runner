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
    public bool left = false;
    public bool right = false;
    private bool isGrounded = true;
    public bool dead = false;
    public bool boost = false;

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
    Vector3 targetXPosition;

    public float jumpHeight = 1f;
    public float leftRightForce = 2f;
    public float groundDistance = 0.2f;
    public float speed = 8f;
    public float speedIncrement = 0.2f;
    public float animationSpeedIncrement = 0.007f;
    public float currentScore = 0;
    public float currentCoins = 0;
    public float recordScore = 0;
    public float boostMultiplier = 1;
    public float boostMultiplierDefault = 1;
    public float coinValue = 1f;

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
        animator.SetBool("isLeft", left);
        animator.SetBool("isRight", right);

        targetXPosition = transform.position;
    }

    void updatePlayerMetadata() {
        playerMetadata.update(Mathf.Round(currentCoins), Mathf.Round(currentScore));
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

    void checkDead()
    {
        // Debug.Log("controllo se morto");
        if (dead == true)
        {
            SceneManager.LoadScene("GameOverMenu");
        }
    }

    void processInput()
    {

        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, Ground, QueryTriggerInteraction.Ignore);

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && isGrounded)
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

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
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

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            left = true;
        }
        else
        {
            left = false;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            right = true;
        }
        else
        {
            right = false;
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

        if (left)
        {
            // rigidBody.AddForce(Vector3.left * Mathf.Sqrt(leftRightForce * -2f * Physics.gravity.y), ForceMode.VelocityChange);           
            // transform.Translate(-9f * Time.deltaTime, 0, 0);
            targetXPosition.x += -1;
        }

        if (right)
        {
            // rigidBody.AddForce(Vector3.right * Mathf.Sqrt(leftRightForce * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            // transform.Translate(9f*Time.deltaTime, 0, 0);
            targetXPosition.x += 1;
        }
    }

    void moveForward()
    {
        // move character
        transform.Translate(0, 0, speed * Time.deltaTime* boostMultiplier);
    }

    void graduallyMoveLeftAndRight() {
        targetXPosition.y = transform.position.y;
        targetXPosition.z = transform.position.z;
        transform.position = Vector3.MoveTowards(transform.position, targetXPosition, 5 * Time.deltaTime);
    }

    void updateScore()
    {
        currentScore += 5 * Time.deltaTime* boostMultiplier;
    }

    // Update is called once per frame
    void Update()
    {

        updatePlayerMetadata();

        checkDead();

        playAudio();

        graduallyMoveLeftAndRight();

        updateScore();

        moveForward();

        processInput();

        // TODO: remove
        speed += speedIncrement*Time.deltaTime;
        animator.speed += animationSpeedIncrement*Time.deltaTime;
    }

    void OnTriggerEnter(Collider other) {

        if (other.gameObject.tag == "Player") {
            Destroy(trigger.gameObject);
        }

        if (other.gameObject.tag == "Coin") {
            Destroy(other.gameObject,0.5f);
            currentCoins += coinValue;
            speed += speedIncrement;
            animator.speed += animationSpeedIncrement;
        }

        if (other.gameObject.tag == "Obstacle" && !boost)
        {
            dead = true;
            if (currentScore > recordScore) {
                recordScore = Mathf.Round(currentScore);
                Messenger<float>.Broadcast(GameEvent.NEW_RECORD, recordScore);
            }
            Messenger<PlayerMetadata>.Broadcast(GameEvent.STORE_RANKING, playerMetadata);
        }

        if (other.gameObject.tag == "Boost")
        {
            Destroy(other.gameObject, 0.5f);
            StartCoroutine(Boost());            
        }

    }

    IEnumerator Boost()
    {
        boost = true;
        boostMultiplier *= 5;
        yield return new WaitForSeconds(5);
        boostMultiplier = boostMultiplierDefault;
        // wait another 1s with normal speed, so player can resume normal game
        yield return new WaitForSeconds(1);
        boost = false;
    }

}
