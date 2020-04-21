/**
 * 
 * Files created by the OSU ARC Senior Project Team
 * April 20, 2020
 * 
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Text.RegularExpressions;

public class Register : MonoBehaviour
{
    public GameObject username;
    public GameObject password;
    public GameObject confPassword;
    private string Username;
    private string Password;
    private string ConfPassword;
    private string form;

    private Text errorMessage;
    private Text successMessage;


    public void RegisterButton()
    {

        errorMessage = GameObject.Find("ErrorMessage").GetComponent<Text>();
        successMessage = GameObject.Find("SuccessMessage").GetComponent<Text>();

        bool UN = false;
        bool PW = false;
        bool CPW = false;

        if (Username != "")
        {
            if (!System.IO.File.Exists(@"E:/UnityTestFolder/" + Username + ".txt"))
            {
                UN = true;
            }
            else
            {
                Debug.LogWarning("Username Taken");
                errorMessage.text = "Username Taken";
            }
        }
        else
        {
            Debug.LogWarning("Username field Empty");
            errorMessage.text = "Username field Empty";
        }


        PW = ValidatePassword(Password);


        if (ConfPassword != "")
        {
            if (ConfPassword == Password)
            {
                CPW = true;
            }
            else
            {
                Debug.LogWarning("Passwords Don't Match");
                errorMessage.text = "Passwords Don't Match";
            }
        }
        else
        {
            Debug.LogWarning("Confirm Password Field Empty");
            errorMessage.text = "Confirm Password Field Empty";
        }


        if (UN == true && PW == true && CPW == true)
        {

            // Create Salt
            int size = 10;
            var range = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buffer = new byte[size];
            range.GetBytes(buffer);
            string salt = Convert.ToBase64String(buffer);
            //print("salt = " + salt);

            //Create Hash
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(Password + salt);
            System.Security.Cryptography.SHA256Managed sha256Hashed = new System.Security.Cryptography.SHA256Managed();
            byte[] hashAll = sha256Hashed.ComputeHash(bytes);
            string hash = System.Text.Encoding.UTF8.GetString(hashAll, 0, hashAll.Length);
            //print("hash = " + hash);


            form = (Username + Environment.NewLine + salt + Environment.NewLine + hash);
            System.IO.File.WriteAllText(@"E:/UnityTestFolder/" + Username + ".txt", form);
            username.GetComponent<InputField>().text = "";
            password.GetComponent<InputField>().text = "";
            confPassword.GetComponent<InputField>().text = "";
            print("Registration Complete");
            errorMessage.text = "";
            successMessage.text = "Registration Complete";

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }
            if (password.GetComponent<InputField>().isFocused)
            {
                confPassword.GetComponent<InputField>().Select();
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (Password != "" && Password != "" && ConfPassword != "")
            {
                RegisterButton();
            }
        }
        Username = username.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
        ConfPassword = confPassword.GetComponent<InputField>().text;
    }


    private bool ValidatePassword(string password)
    {
        var input = password;

        if (string.IsNullOrWhiteSpace(input))
        {
            Debug.LogWarning("Password should not be empty");
            errorMessage.text = "Password should not be empty";
            return false;

        }
        else
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,15}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(input))
            {
                Debug.LogWarning("Password should contain at least one lower case letter");
                errorMessage.text = "Password should contain at least one lower case letter";
                return false;
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                Debug.LogWarning("Password should contain at least one upper case letter");
                errorMessage.text = "Password should contain at least one upper case letter";
                return false;
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                Debug.LogWarning("Password should not be less than 8 or greater than 15 characters");
                errorMessage.text = "Password should not be less than 8 or greater than 15 characters";
                return false;
            }
            else if (!hasNumber.IsMatch(input))
            {
                Debug.LogWarning("Password should contain at least one numerical value");
                errorMessage.text = "Password should contain at least one numerical value";
                return false;
            }

            else if (!hasSymbols.IsMatch(input))
            {
                Debug.LogWarning("Password should contain at least one special case characters");
                errorMessage.text = "Password should contain at least one special case characters";
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}