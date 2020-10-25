using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotation : MonoBehaviour
{

    public bool goUp;
    public AudioSource audioSource;
    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.02f;
    }

    // Update is called once per frame
    void Update()
    {
        if (goUp == true)
        {
            transform.Rotate(0, 0, 0);
            transform.Translate(0, 0.4f, 0);
        }
        else
        {
            transform.Rotate(0, 0, 1f);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            goUp = true;
            audioSource.PlayOneShot(audioClip, 1);
        }
    }
}
