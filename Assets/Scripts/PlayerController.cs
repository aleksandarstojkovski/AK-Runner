using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool jump = false;
    public bool slide = false;

    public Animator anim;
    private Rigidbody rigidBody;
    private CapsuleCollider capsuleCollider;

    public GameObject trigger;

    public float JumpHeight = 10;
    private bool _isGrounded = true;
    public float GroundDistance = 10f;
    public LayerMask Ground;
    public Transform _groundChecker;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        Debug.Log("Capsule collider: " + capsuleCollider);
    }

    // Update is called once per frame
    void Update()
    {

        _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);

        //Movimento giocatore in avanti
        transform.Translate(0, 0, 5.0f*Time.deltaTime);

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

        if (slide == true)
        {
        }


        trigger = GameObject.FindGameObjectWithTag("Obstacle");
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            Destroy(trigger.gameObject);
        }
        if (other.gameObject.tag == "Coin") {
            Destroy(other.gameObject,0.5f);
        }
    }

}
