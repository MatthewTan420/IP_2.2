/*
 * Author: Matthew, Seth, Wee Kiat, Isabel, Clifford
 * Date: 8/2/2024
 * Description: AudioPlay
 */

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
    /// <summary>
    /// Folder alias for storing images in Firebase Storage.
    /// </summary>
    public string CamFolderAlias = "IP_Snapshot";

    /// <summary>
    /// Folder alias for storage in Firebase Storage.
    /// </summary>
    private string StorageFolderAlias;

    /// <summary>
    /// Flag indicating if a photo is ready to be taken.
    /// </summary>
    private bool isPhoto = false;

    /// <summary>
    /// Reference to the Camera component.
    /// </summary>
    public Camera cam;

    /// <summary>
    /// Reference to the AuthManager script.
    /// </summary>
    public AuthManager authManager;

    /// <summary>
    /// RenderTexture for the camera.
    /// </summary>
    public RenderTexture camTex;

    /// <summary>
    /// TextMeshPro UI element for displaying information.
    /// </summary>
    public TextMeshPro text;

    /// <summary>
    /// Firebase Storage instance.
    /// </summary>
    private FirebaseStorage storage;

    /// <summary>
    /// Flag indicating if the photo is of a crocodile.
    /// </summary>
    public bool isCroc = false;

    /// <summary>
    /// Flag indicating if the photo is of a tree.
    /// </summary>
    public bool isTree = false;

    /// <summary>
    /// Called before the first frame update. Initializes Firebase Storage and folder aliases.
    /// </summary>
    void Start()
    {
        storage = FirebaseStorage.DefaultInstance;
        StorageFolderAlias = "test"; // Set the storage folder alias
    }

    /// <summary>
    /// Called once per frame. Performs raycasting to identify targets and updates UI information.
    /// </summary>
    void Update()
    {
        Debug.DrawLine(transform.position, transform.position + (transform.forward * 10));
        RaycastHit hit;

        // Raycast to identify targets
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.transform.tag == "Crocodile")
            {
                isPhoto = true;
                isCroc = true;
                text.text = "" + hit.transform.tag;
            }
            else if (hit.transform.tag == "Tree")
            {
                isPhoto = true;
                isTree = true;
                text.text = "" + hit.transform.tag;
            }
            else if (hit.transform.tag == "Bird" || hit.transform.tag == "Monkey")
            {
                isPhoto = true;
                text.text = "" + hit.transform.tag;
            }
            else
            {
                isPhoto = false;
                text.text = "";
            }
        }
    }

    /// <summary>
    /// Initiates the photo capture process.
    /// </summary>
    public void photoTake()
    {
        if (isPhoto == true)
        {
            DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
            string fileName = now.ToUnixTimeSeconds() + "-cam.png";
            StartCoroutine(CoroutineScreenshot(fileName, cam));
        }
    }

    /// <summary>
    /// Coroutine for capturing screenshots and initiating the upload process.
    /// </summary>
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
            cam.targetTexture = camTex;
            byte[] bytes = renderedTexture.EncodeToPNG();
            string base64String = Convert.ToBase64String(bytes);

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

    /// <summary>
    /// Coroutine for uploading Base64 string to Firebase Storage.
    /// </summary>
    IEnumerator UploadString(float delay, string base64String, string pathInStorage)
    {
        yield return new WaitForSeconds(delay);
        // Convert Base64 string to byte[]
        byte[] bytes = Convert.FromBase64String(base64String);
        // Create a reference to the storage location
        StorageReference storageRef = storage.GetReferenceFromUrl("gs://sungai-buloh.appspot.com/images");
        StorageReference imgRef = storageRef.Child(Path.Combine(StorageFolderAlias, pathInStorage));
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
        authManager.UpdateImg();
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
