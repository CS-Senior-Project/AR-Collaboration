using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Text.RegularExpressions;

public class Login : MonoBehaviour
{
    public GameObject username;
    public GameObject password;
    private string Username;
    private string Password;
    private String[] Lines;
    private string DecryptedPass;

    private Text errorMessage;
    private Text successMessage;

    public void LoginButton()
    {

        errorMessage = GameObject.Find("ErrorMessage").GetComponent<Text>();
        successMessage = GameObject.Find("SuccessMessage").GetComponent<Text>();

        bool UN = false;
        bool PW = false;

        if (Username != "")
        {
            if (System.IO.File.Exists(@"E:/UnityTestFolder/" + Username + ".txt"))
            {
                UN = true;
                Lines = System.IO.File.ReadAllLines(@"E:/UnityTestFolder/" + Username + ".txt");
            }
            else
            {
                Debug.LogWarning("Username Invaild");
                errorMessage.text = "Username Invalid";
            }
        }
        else
        {
            Debug.LogWarning("Username Field Empty");
            errorMessage.text = "Username Field Empty";
        }


        if (Password != "")
        {
            if (System.IO.File.Exists(@"E:/UnityTestFolder/" + Username + ".txt"))
            {

                // Get Salt
                string salt = Lines[1];
                //print("login salt: " + salt);

                // Calculate Hash from user input password
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(Password + salt);
                System.Security.Cryptography.SHA256Managed sha256Hashed = new System.Security.Cryptography.SHA256Managed();
                byte[] hashAll = sha256Hashed.ComputeHash(bytes);
                string Comparehash = System.Text.Encoding.UTF8.GetString(hashAll, 0, hashAll.Length);
                //print("login hash: " + Comparehash);

                // Get Hash
                string fileHash = Lines[2];

                if (fileHash == Comparehash)
                {
                    PW = true;
                }
                else
                {
                    Debug.LogWarning("Password Is invalid");
                    errorMessage.text = "Password Is invalid";
                }
            }
            else
            {
                Debug.LogWarning("Password Is invalid");
                errorMessage.text = "Password Is invalid";
            }
        }
        else
        {
            Debug.LogWarning("Password Field Empty");
            errorMessage.text = "Password Field Empty";
        }
        if (UN == true && PW == true)
        {
            username.GetComponent<InputField>().text = "";
            password.GetComponent<InputField>().text = "";
            print("Login Successful");
            errorMessage.text = "";
            successMessage.text = "Login Successful";
            Application.LoadLevel("Input2.0");                          // TODO: Change to Whatever Start Scene is
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();           // Allow for tab to next slot
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (Password != "" && Password != "")
            {
                LoginButton();                                          // "Enter will press button
            }
        }
        Username = username.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
    }
}