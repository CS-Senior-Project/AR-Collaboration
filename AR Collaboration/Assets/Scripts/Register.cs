/**
 * 
 * Files created by the OSU ARC Senior Project Team
 * Carson Pemble
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

        errorMessage = GameObject.Find("ErrorMessage").GetComponent<Text>();                // Initialize some feedback messages.
        successMessage = GameObject.Find("SuccessMessage").GetComponent<Text>();

        bool UN = false;                                                                    // Set bool to false to start out.
        bool PW = false;
        bool CPW = false;

        if (Username != "")                                                                 // There has to be some input.
        {
            if (!System.IO.File.Exists(@"E:/UnityTestFolder/" + Username + ".txt"))
            {
                UN = true;                                                                  // Set true if the username is NOT already in the file.
            }
            else
            {
                Debug.LogWarning("Username Taken");
                errorMessage.text = "Username Taken";                                       // Send error messages to the screen so the user can see the message.
            }
        }
        else
        {
            Debug.LogWarning("Username field Empty");
            errorMessage.text = "Username field Empty";                                     // Send error messages to the screen so the user can see the message.
        }


        PW = ValidatePassword(Password);                                        // Checks to see if the password entered meets our requirements.


        if (ConfPassword != "")
        {
            if (ConfPassword == Password)
            {
                CPW = true;                                                     // Set true if the passwords match.
            }
            else
            {
                Debug.LogWarning("Passwords Don't Match");
                errorMessage.text = "Passwords Don't Match";                    // Send error messages to the screen so the user can see the message.
            }
        }
        else
        {
            Debug.LogWarning("Confirm Password Field Empty");
            errorMessage.text = "Confirm Password Field Empty";                 // Send error messages to the screen so the user can see the message.
        }


        if (UN == true && PW == true && CPW == true)
        {

            // Create Salt
            int size = 10;
            var range = new System.Security.Cryptography.RNGCryptoServiceProvider();        // Random range for security.
            var buffer = new byte[size];
            range.GetBytes(buffer);
            string salt = Convert.ToBase64String(buffer);
            //print("salt = " + salt);

            //Create Hash
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(Password + salt);
            System.Security.Cryptography.SHA256Managed sha256Hashed = new System.Security.Cryptography.SHA256Managed();     // Hash it up.
            byte[] hashAll = sha256Hashed.ComputeHash(bytes);
            string hash = System.Text.Encoding.UTF8.GetString(hashAll, 0, hashAll.Length);
            //print("hash = " + hash);


            form = (Username + Environment.NewLine + salt + Environment.NewLine + hash);        // Add the information to a form
            System.IO.File.WriteAllText(@"E:/UnityTestFolder/" + Username + ".txt", form);      // Print all the information to a file corresponding to the username
            username.GetComponent<InputField>().text = "";
            password.GetComponent<InputField>().text = "";
            confPassword.GetComponent<InputField>().text = "";
            print("Registration Complete");
            errorMessage.text = "";
            successMessage.text = "Registration Complete";                          // Send Success messages to the screen so the user can see that you are now registered.

        }

    }

    // Update is called once per frame
    void Update()                                                           // Improves the user experiance with tabs and enters.
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();               // Allow for tabs to bring the user to the next slot.
            }
            if (password.GetComponent<InputField>().isFocused)
            {
                confPassword.GetComponent<InputField>().Select();           // Allow for tabs to bring the user to the next slot.
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (Password != "" && Password != "" && ConfPassword != "")
            {
                RegisterButton();                                           // "Enter" will press the register button.
            }
        }
        Username = username.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
        ConfPassword = confPassword.GetComponent<InputField>().text;
    }


    private bool ValidatePassword(string password)                  // Make sure the password meets all of our standards.
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
            var hasNumber = new Regex(@"[0-9]+");                                   // Password must contain a number.
            var hasUpperChar = new Regex(@"[A-Z]+");                                // Password must contain an upper case character.
            var hasMiniMaxChars = new Regex(@".{8,15}");                            // Password must be more than 8 characters.
            var hasLowerChar = new Regex(@"[a-z]+");                                // Password must contain a lower case character.
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");         // Password must contain a special case character.

            if (!hasLowerChar.IsMatch(input))
            {
                Debug.LogWarning("Password should contain at least one lower case letter");
                errorMessage.text = "Password should contain at least one lower case letter";               // Print error message to the screen if user's input doesn't have the corresponding piece to the password.
                return false;
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                Debug.LogWarning("Password should contain at least one upper case letter");
                errorMessage.text = "Password should contain at least one upper case letter";               // Print error message to the screen if user's input doesn't have the corresponding piece to the password.
                return false;
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                Debug.LogWarning("Password should not be less than 8 or greater than 15 characters");
                errorMessage.text = "Password should not be less than 8 or greater than 15 characters";     // Print error message to the screen if user's input doesn't have the corresponding piece to the password.
                return false;
            }
            else if (!hasNumber.IsMatch(input))
            {
                Debug.LogWarning("Password should contain at least one numerical value");
                errorMessage.text = "Password should contain at least one numerical value";                 // Print error message to the screen if user's input doesn't have the corresponding piece to the password.
                return false;
            }

            else if (!hasSymbols.IsMatch(input))
            {
                Debug.LogWarning("Password should contain at least one special case characters");
                errorMessage.text = "Password should contain at least one special case characters";         // Print error message to the screen if user's input doesn't have the corresponding piece to the password.
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}