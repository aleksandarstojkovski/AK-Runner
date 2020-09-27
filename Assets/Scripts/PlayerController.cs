using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool jump = false;
    public bool slide = false;

    public Animator anim;

    public GameObject trigger;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(0, 0, 0.05f);

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

        if (jump == true)
        {
            transform.Translate(0, 2f, 0.1f);
        }

        if (slide == true)
        {
            transform.Translate(0, 0, 0.1f);
        }

        trigger = GameObject.FindGameObjectWithTag("Obstacle");
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "PlayerTrigger") {
            Destroy(trigger.gameObject);
        }
        if (other.gameObject.tag == "Coin") {
            Destroy(other.gameObject,0.5f);
        }
    }

}
