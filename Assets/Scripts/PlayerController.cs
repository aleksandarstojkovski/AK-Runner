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

    public float jumpHeight = 1f;
    public float groundDistance = 0.2f;
    public float speed = 7f;
    public float speedIncrement = 0.2f;
    public float animationSpeedIncrement = 0.002f;
    public float currentScore = 0;
    public float currentCoins = 0;
    public float recordScore = 0;

    public List<PlayerMetadata> ranking = new List<PlayerMetadata>();

    bool AnimatorIsPlaying(string stateName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
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
    }

    void updatePlayerMetadata() {
        playerMetadata.update(currentCoins, currentScore);
        Messenger<PlayerMetadata>.Broadcast(GameEvent.UPDATE_METADATA, playerMetadata);
    }

    // Update is called once per frame
    void Update()
    {

        updatePlayerMetadata();

        if (dead == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        currentScore += 5 * Time.deltaTime;

        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, Ground, QueryTriggerInteraction.Ignore);

        // move character
        transform.Translate(0, 0, speed*Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        } 
        else
        {
            jump = false;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            slide = true;
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
