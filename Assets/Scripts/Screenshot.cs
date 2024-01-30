using UnityEngine;
using System.Collections;
//added libraries
using System.IO;
using System;
using System.Threading;
using System.Threading.Tasks;
//Firebase storage imports
using Firebase;
using Firebase.Storage;
using Firebase.Extensions;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms;
//using UnityEditor.VersionControl;

public class Screenshot : MonoBehaviour
{
    public string CamFolderAlias = "IP_Snapshot";
    private string StorageFolderAlias;

    private bool isPhoto = false;
    public Camera cam;
    public AuthManager authManager;

    private FirebaseStorage storage;

    // Start is called before the first frame update
    void Start()
    {
        storage = FirebaseStorage.DefaultInstance;
        StorageFolderAlias = "testing"/*authManager.uID*/;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.T) isPhoto == true)
        {
            DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
            string fileName = now.ToUnixTimeSeconds() + "-cam.png";
            //ScreenCapture.CaptureScreenshot("GameScreenshot.png");
            StartCoroutine(CoroutineScreenshot(fileName, cam));
        }*/
        Debug.DrawLine(transform.position, transform.position + (transform.forward * 10));
        RaycastHit hit;
        //if user mouse is at the button
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            //what happens after clicking
            t.text += "" + hit.transform.tag;
            if (hit.transform.tag == "Crocodile")
            {
                isPhoto = true;
            }
            else
            {
                isPhoto = false;
            }
        }
    }

    public void photoTake()
    {
        if (isPhoto == true)
        {
            DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
            string fileName = now.ToUnixTimeSeconds() + "-cam.png";
            StartCoroutine(CoroutineScreenshot(fileName, cam));
        }
    }

    private IEnumerator CoroutineScreenshot(string fileName, Camera cam)
    {
        yield return new WaitForEndOfFrame();

        try
        {
            int width = Screen.width;
            int height = Screen.height;
            RenderTexture screenTexture = new RenderTexture(width, height, 16);
            cam.targetTexture = screenTexture;
            RenderTexture.active = screenTexture;
            cam.Render();
            Texture2D renderedTexture = new Texture2D(width, height);
            renderedTexture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            RenderTexture.active = null;
            cam.targetTexture = null;
            byte[] bytes = renderedTexture.EncodeToPNG();
            string base64String = Convert.ToBase64String(bytes);

            //path.combine takes into OS consideration and adds on a correct path "/" or "\"
            //<app storage path>/<cam folder name>
            string path = Path.Combine(Application.persistentDataPath, CamFolderAlias);
            t.text += "" + path + "\n";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //Write out the PNG.persistentDataPath is the path of the application
            System.IO.File.WriteAllBytes(Path.Combine(path, fileName), bytes);
            t.text += "" + Path.Combine(path, fileName).ToString() + "\n";
            //StartCoroutine(UploadImage(2, fileName));
            StartCoroutine(UploadString(2, base64String, fileName));


        }
        catch (IOException e)
        {
            Debug.Log($"IO exception {e.ToString()}");
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
    
    IEnumerator UploadString(float delay, string base64String, string pathInStorage)
    {
        yield return new WaitForSeconds(delay);
        // Convert Base64 string to byte[]
        byte[] bytes = Convert.FromBase64String(base64String);
        // Create a reference to the storage location
        StorageReference storageRef = storage.GetReferenceFromUrl("gs://sungai-buloh.appspot.com/images");
        StorageReference imgRef = storageRef.Child(Path.Combine(StorageFolderAlias, pathInStorage)); // "images/" + fileName);
        // Upload the file to Firebase Storage
        imgRef.PutBytesAsync(bytes).ContinueWith((Task<StorageMetadata> task) =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError(task.Exception.ToString());
                // Handle the error...
            }
            else
            {
                // File uploaded successfully
                Debug.Log("File uploaded successfully.");
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
