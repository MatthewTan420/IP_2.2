using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using UnityEngine.SceneManagement;
using Firebase;
//using Firebase.Database;
using UnityEngine.UI;
using Firebase.Extensions;
using System.Data.Common;

public class AuthManager : MonoBehaviour
{
    Firebase.Auth.FirebaseAuth auth;

    [SerializeField]
    private TMP_InputField inputEmail;
    [SerializeField]
    private TMP_InputField inputPassword;

    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    public void SignUp()
    {
        string newEmail = inputEmail.text;
        string newPassword = inputPassword.text;

        auth.CreateUserWithEmailAndPasswordAsync(newEmail, newPassword).ContinueWithOnMainThread(task =>
        {
            //perform task handling
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Sorry, there was an error creating your new account, ERROR: " + task.Exception);
                return;//exit from the attempt
            }
            else if (task.IsCompleted)
            {
                Firebase.Auth.AuthResult newPlayer = task.Result;
                //do anything you want after player creation eg. create new player
            }
        });
    }
}
