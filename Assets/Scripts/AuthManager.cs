/*
 * Author: Matthew, Seth, Wee Kiat, Isabel, Clifford
 * Date: 8/2/2024
 * Description: Firebase
 */

using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;
using UnityEngine.UI;
using Firebase.Extensions;
using System.Data.Common;
using Oculus.Platform.Models;
using static OVRPlugin;

public class AuthManager : MonoBehaviour
{
    /// <summary>
    /// The Firebase authentication instance.
    /// </summary>
    private FirebaseAuth auth;

    /// <summary>
    /// Input field for user email during sign-up.
    /// </summary>
    [SerializeField] private TMP_InputField inputEmail;

    /// <summary>
    /// Input field for user password during sign-up.
    /// </summary>
    [SerializeField] private TMP_InputField inputPassword;

    /// <summary>
    /// Input field for user email during sign-in.
    /// </summary>
    [SerializeField] private TMP_InputField logEmail;

    /// <summary>
    /// Input field for user password during sign-in.
    /// </summary>
    [SerializeField] private TMP_InputField logPassword;

    /// <summary>
    /// Input field for user username during data creation.
    /// </summary>
    [SerializeField] private TMP_InputField inputUser;

    /// <summary>
    /// Input field for user age during data creation.
    /// </summary>
    [SerializeField] private TMP_InputField inputAge;

    /// <summary>
    /// The Firebase Realtime Database reference.
    /// </summary>
    private DatabaseReference mDatabaseRef;

    /// <summary>
    /// Reference to the "players" node in the database.
    /// </summary>
    private DatabaseReference reference;

    /// <summary>
    /// The unique user ID obtained after authentication.
    /// </summary>
    public static string UID;

    /// <summary>
    /// Variable to store the user ID.
    /// </summary>
    public string uID;

    /// <summary>
    /// UI GameObject for data display.
    /// </summary>
    public GameObject dataUI;

    /// <summary>
    /// UI GameObject for authentication.
    /// </summary>
    public GameObject authUI;

    /// <summary>
    /// Dropdown UI component for gender selection.
    /// </summary>
    public DropDown DropDown;

    /// <summary>
    /// Dropdown UI component for occupation selection.
    /// </summary>
    public DropDown DropDown1;

    /// <summary>
    /// Reference to the Trash script.
    /// </summary>
    public Trash Trash;

    /// <summary>
    /// Reference to the Distance script.
    /// </summary>
    public Distance Distance;

    /// <summary>
    /// Static variable to store distance.
    /// </summary>
    public static int dis;

    /// <summary>
    /// Static variable to store image count.
    /// </summary>
    public static int image;

    /// <summary>
    /// Variable to store distance.
    /// </summary>
    public int distance;

    /// <summary>
    /// Variable to store image count.
    /// </summary>
    public int img;

    /// <summary>
    /// Called on script initialization. Initializes Firebase services and references.
    /// </summary>
    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
        reference = FirebaseDatabase.DefaultInstance.GetReference("players");
        uID = UID;
    }

    /// <summary>
    /// Called on script start. Assigns initial values to user-specific variables.
    /// </summary>
    private void Start()
    {
        uID = UID;
        distance = dis;
        img = image;
    }

    /// <summary>
    /// Initiates the user sign-up process using Firebase Authentication.
    /// </summary>
    public void SignUp()
    {
        string newEmail = inputEmail.text;
        string newPassword = inputPassword.text;

        auth.CreateUserWithEmailAndPasswordAsync(newEmail, newPassword).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Sorry, there was an error creating your new account, ERROR: " + task.Exception);
                return;
            }
            else if (task.IsCompleted)
            {
                Firebase.Auth.AuthResult newPlayer = task.Result;
                UID = newPlayer.User.UserId;
                dataUI.SetActive(true);
                authUI.SetActive(false);
            }
        });
    }

    /// <summary>
    /// Initiates the user sign-in process using Firebase Authentication.
    /// </summary>
    public void SignInUser()
    {
        string email = logEmail.text;
        string password = logPassword.text;
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(authTask =>
        {
            if (authTask.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPassword Sync was cancelled");
                return;
            }
            if (authTask.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPassword Async encountered an error: " + authTask.Exception);
                return;
            }
            FirebaseUser currentPlayer = authTask.Result.User;
            if (currentPlayer != null)
            {
                UID = currentPlayer.UserId;
                authUI.SetActive(false);
                SceneManager.LoadScene(1);
                Login();
            }
        });
    }

    /// <summary>
    /// Creates user data on Firebase Realtime Database.
    /// </summary>
    public void CreateData()
    {
        string newUser = inputUser.text;
        string newGend = DropDown1.selectedOption;
        string newOccu = DropDown.selectedOption;
        int newAge = int.Parse(inputAge.text);
        WriteNewUser(newUser, 0, 0, newGend, newOccu, newAge, false, 0, 0);
    }

    /// <summary>
    /// Writes new user data to the database.
    /// </summary>
    private void WriteNewUser(string username, int distance, int rubbish, string gender, string occupation, int age, bool companion, int img, int prawn)
    {
        User user = new User(username, distance, rubbish, gender, occupation, age, companion, img, prawn);
        string json = JsonUtility.ToJson(user);
        mDatabaseRef.Child("players").Child(UID).SetRawJsonValueAsync(json);
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Updates the trash count on the database.
    /// </summary>
    public void UpdateTrash()
    {
        int rubbish = Trash.num;
        Dictionary<string, object> childUpdates = new Dictionary<string, object>();
        childUpdates["/rubbish"] = rubbish;

        reference.Child(UID).UpdateChildrenAsync(childUpdates);
    }

    /// <summary>
    /// Updates the distance value on the database.
    /// </summary>
    public void UpdateDistance()
    {
        distance += 5;
        Dictionary<string, object> childUpdates = new Dictionary<string, object>();
        childUpdates["/distance"] = distance;

        reference.Child(UID).UpdateChildrenAsync(childUpdates);
    }

    /// <summary>
    /// Updates the prawn count on the database.
    /// </summary>
    public void UpdatePrawn()
    {
        int rubbish = Trash.pnum;
        Dictionary<string, object> childUpdates = new Dictionary<string, object>();
        childUpdates["/prawn"] = rubbish;

        reference.Child(UID).UpdateChildrenAsync(childUpdates);
    }

    /// <summary>
    /// Updates the image count on the database.
    /// </summary>
    public void UpdateImg()
    {
        img += 1;
        Dictionary<string, object> childUpdates = new Dictionary<string, object>();
        childUpdates["/img"] = img;

        reference.Child(UID).UpdateChildrenAsync(childUpdates);
    }

    /// <summary>
    /// Updates the companion status on the database.
    /// </summary>
    public void UpdateComp()
    {
        Dictionary<string, object> childUpdates = new Dictionary<string, object>();
        childUpdates["/companion"] = true;

        reference.Child(UID).UpdateChildrenAsync(childUpdates);
    }

    /// <summary>
    /// Retrieves user data from the database and updates local variables.
    /// </summary>
    public void Login()
    {
        mDatabaseRef.Child("players").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                //@TODO
                //process error
            }
            else if (task.IsCompleted)
            {
                DataSnapshot ds = task.Result;

                foreach (var v in ds.Children)
                {
                    string playerKey = v.Key;
                    string playerDetails = v.GetRawJsonValue();

                    User p = JsonUtility.FromJson<User>(playerDetails);

                    if (v.Key == UID)
                    {
                        dis = p.distance;
                        image = p.img;
                    }
                }
            }
        });
    }

    public TextMeshProUGUI t;

    private void OnEnable()
    {
        Application.logMessageReceived += Debugt;
    }

    private void OnDisable()
    {

        Application.logMessageReceived -= Debugt;
    }

    private void Debugt(string msg, string st, LogType ty)
    {
        t.text += msg + "\n";
    }
}
