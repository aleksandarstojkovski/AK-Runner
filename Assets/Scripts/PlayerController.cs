using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool jump = false;
    public bool slide = false;

    public Animator anim;
    private Rigidbody rigidBody;
    private BoxCollider capsuleCollider;

    public GameObject trigger;

    public float JumpHeight = 10;
    private bool _isGrounded = true;
    public float GroundDistance = 10f;
    public LayerMask Ground;
    public Transform _groundChecker;
    public float velocita = 5.0f;
    public float score = 0;

    public Text scoreText;
    public Text bestScoreText;
    public bool death = false;
    public Image gameOverImage;
    public float bestScoreEver;

    bool AnimatorIsPlaying(string stateName)
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<BoxCollider>();
        bestScoreEver = PlayerPrefs.GetFloat("BestScore");
    }

    // Update is called once per frame
    void Update()
    {

        scoreText.text = score.ToString();
        bestScoreText.text = "Your score: " + score +"\n"+ "Best score: " + bestScoreEver;
      

        if (death == true)
        {
            gameOverImage.gameObject.SetActive(true);
        }


        _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);

        //Movimento giocatore in avanti
        if (death == false)
            transform.Translate(0, 0, velocita*Time.deltaTime);

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

        anim.SetBool("isJump", jump);
        anim.SetBool("isSlide", slide);

        if (jump == true && _isGrounded)
        {
            //transform.Translate(0, 2f* Time.deltaTime, 0.1f*Time.deltaTime);
            rigidBody.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }

        trigger = GameObject.FindGameObjectWithTag("Obstacle");
        velocita += 0.002f;
        anim.speed += 0.00002f;

    }

    void OnTriggerEnter(Collider other) {
        //Debug.Log(other);
        if (other.gameObject.tag == "Player") {
            Destroy(trigger.gameObject);
        }
        if (other.gameObject.tag == "Coin") {
            Destroy(other.gameObject,0.5f);
            score += 5f;
        }
        if (other.gameObject.tag == "Obstacle")
        {
            Debug.Log("Colpito ostacolo " + trigger.name);
            death = true;
            if (score > bestScoreEver)
            {
                PlayerPrefs.SetFloat("BestScore", score);
            }
        }
        if (other.gameObject.tag == "DeathPoint") {
            
        }
    }

}
