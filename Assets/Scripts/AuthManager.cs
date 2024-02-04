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
    Firebase.Auth.FirebaseAuth auth;

    [SerializeField]
    private TMP_InputField inputEmail;
    [SerializeField]
    private TMP_InputField inputPassword;
    [SerializeField]
    private TMP_InputField logEmail;
    [SerializeField]
    private TMP_InputField logPassword;
    [SerializeField]
    private TMP_InputField inputUser;
    [SerializeField]
    private TMP_InputField inputAge;

    DatabaseReference mDatabaseRef;
    DatabaseReference reference;

    public static string UID;
    public string uID;
    public GameObject dataUI;
    public GameObject authUI;
    public DropDown DropDown;
    public DropDown DropDown1;

    public Trash Trash;
    public Distance Distance;

    public static int dis;
    public static int image;

    public int distance;
    public int img;

    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
        reference = FirebaseDatabase.DefaultInstance.GetReference("players");
        uID = UID;
    }

    private void Start()
    {
        uID = UID;
        distance = dis;
        img = image;
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
                UID = newPlayer.User.UserId;
                dataUI.SetActive(true);
                authUI.SetActive(false);
                //do anything you want after player creation eg. create new player
            }
        });
    }

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
                Debug.Log("login success");
                UID = currentPlayer.UserId;
                authUI.SetActive(false);
                SceneManager.LoadScene(1);
                Login();
            }
        });
    }

    public void CreateData()
    {
        string newUser = inputUser.text;
        string newGend = DropDown1.selectedOption;
        string newOccu = DropDown.selectedOption;
        int newAge = int.Parse(inputAge.text);
        WriteNewUser(newUser, 0, 0, newGend, newOccu, newAge, false, 0, 0);
    }

    /// <summary>
    /// Create data on firebase
    /// </summary>
    private void WriteNewUser(string username, int distance, int rubbish, string gender, string occupation, int age, bool companion, int img, int prawn)
    {
        User user = new User(username, distance, rubbish, gender, occupation, age, companion, img, prawn);
        string json = JsonUtility.ToJson(user);
        mDatabaseRef.Child("players").Child(UID).SetRawJsonValueAsync(json);
        SceneManager.LoadScene(1);
    }

    public void UpdateTrash()
    {
        int rubbish = Trash.num;
        Dictionary<string, object> childUpdates = new Dictionary<string, object>();
        childUpdates["/rubbish"] = rubbish;

        reference.Child(UID).UpdateChildrenAsync(childUpdates);
    }

    public void UpdateDistance()
    {
        distance += 5;
        Dictionary<string, object> childUpdates = new Dictionary<string, object>();
        childUpdates["/distance"] = distance;

        reference.Child(UID).UpdateChildrenAsync(childUpdates);
    }

    public void UpdatePrawn()
    {
        int rubbish = Trash.pnum;
        Dictionary<string, object> childUpdates = new Dictionary<string, object>();
        childUpdates["/prawn"] = rubbish;

        reference.Child(UID).UpdateChildrenAsync(childUpdates);
    }

    public void UpdateImg()
    {
        img += 1;
        Dictionary<string, object> childUpdates = new Dictionary<string, object>();
        childUpdates["/img"] = img;

        reference.Child(UID).UpdateChildrenAsync(childUpdates);
    }

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
                    string data = v.ToString();

                    string playerKey = v.Key;
                    string playerDetails = v.GetRawJsonValue();

                    User p = JsonUtility.FromJson<User>(playerDetails);

                    if (v.Key == UID)
                    {
                        t.text += "" + (p.age).ToString();
                        //t.text += "" + p.country;
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
