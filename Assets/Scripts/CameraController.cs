using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject firstPersonCam;
    public GameObject thirdPersonCam;
    public GameObject frontCamera;
    public GameObject frontCameraLight;
    int cameraMode = 3;

    // Start is called before the first frame update
    void Start()
    {
        Messenger.AddListener(GameEvent.ACTIVATE_FRONT_CAMERA, activateFrontCamera);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (cameraMode == 3)
                cameraMode = 1;
            else
                cameraMode = 3;
        }
        StartCoroutine(ChangeCamera());
    }

    IEnumerator ChangeCamera() {
        yield return new WaitForSeconds(0.01f);
        if (cameraMode == 1)
        {
            thirdPersonCam.SetActive(false);
            firstPersonCam.SetActive(true);
        }
        else if (cameraMode == 3)
        {
            firstPersonCam.SetActive(false);
            thirdPersonCam.SetActive(true);
        }
        else {
            firstPersonCam.SetActive(false);
            thirdPersonCam.SetActive(false);
            frontCamera.SetActive(true);
            frontCameraLight.SetActive(true);
        }
    }

    void activateFrontCamera()
    {
        cameraMode = 4;
    }

}
