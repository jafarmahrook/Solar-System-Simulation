using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject instructionPanel;

    private void Awake()
    {
        SimulateKeyPress();

    }
    // Start is called before the first frame update
    void Start()
    {
        instructionPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("User input for F key is disabled.");
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Cursor.lockState = CursorLockMode.None;  // Unlock the cursor
            Cursor.visible = true;  // Show the cursor
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor to the center of the screen
            Cursor.visible = false;  // Hide the cursor
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            // Toggle the panel's visibility
            instructionPanel.SetActive(!instructionPanel.activeSelf);
        }
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        // Exit play mode in the editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Quit the application
        Application.Quit();
#endif
    }
    void SimulateKeyPress()
    {
        // Log or trigger any action you want for the F key press
        Debug.Log("F key is automatically pressed at the start.");

        // If there's anything you'd normally do when the F key is pressed, add that here
        // Example: DoSomething();
    }
}
