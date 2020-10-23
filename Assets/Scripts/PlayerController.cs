using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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
    public Text currentScoreText;
    public Text currentMoneyText;

    public float jumpHeight = 1f;
    public float groundDistance = 0.1f;
    public float speed = 7f;
    public float speedIncrement = 0.2f;
    public float animationSpeedIncrement = 0.002f;
    public float currentScore = 0;
    public float highScore;
    public float currentMoney;

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
        highScore = Mathf.Round(PlayerPrefs.GetFloat("recordScore"));
        currentMoney = Mathf.Round(PlayerPrefs.GetFloat("coins"));
        //currentScoreText.text = currentScore.ToString();
        //currentMoneyText.text = currentMoney.ToString();
    }

    void updateStatusBar() {
        currentScoreText.text = "Score: " + Mathf.Round (currentScore).ToString();
        PlayerPrefs.SetFloat("currentScore", currentScore);
        currentMoneyText.text = "Coins: " + currentMoney.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        currentScore += 5 * Time.deltaTime;
        
        if (dead == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        updateStatusBar();

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

        // ?
        //trigger = GameObject.FindGameObjectWithTag("Obstacle");

    }

    void OnTriggerEnter(Collider other) {

        if (other.gameObject.tag == "Player") {
            Destroy(trigger.gameObject);
        }

        if (other.gameObject.tag == "Coin") {
            Destroy(other.gameObject,0.5f);
            currentMoney += 5f;
            speed += speedIncrement;
            animator.speed += animationSpeedIncrement;
            PlayerPrefs.SetFloat("coins", currentMoney);
        }

        if (other.gameObject.tag == "Obstacle")
        {
            dead = true;
            if (currentScore > highScore)
            {
                PlayerPrefs.SetFloat("recordScore", Mathf.Round(currentScore));
            }
        }

    }

}
