using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuHelpController : MonoBehaviour
{
    [SerializeField] GameObject instructionPanel;
    [SerializeField] bool isPressed = false;
    
    public void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public void enableDisablePanel()
    {
        isPressed = !isPressed;
        instructionPanel.SetActive(isPressed);
    }
}
