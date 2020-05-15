/*
Handles username input field in test scene.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject startMenu;
    public GameObject connectPanel;
    public GameObject mutePanel;

    public InputField usernameField;
    public Toggle toggle;

    private void Awake() {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this) {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }

        mutePanel.SetActive(false);
        toggle.interactable = false;
    }

    public void ConnectToServer() {
        Debug.Log("Connecting...");
        connectPanel.SetActive(false);
        usernameField.interactable = false;

        mutePanel.transform.localScale += new Vector3(-.75f, -.75f, -.75f);
        mutePanel.transform.position = new Vector3(100f, 100f, 0f);

        mutePanel.SetActive(true);
        toggle.interactable = true;
        Client.instance.ConnectToServer();
    }
}
