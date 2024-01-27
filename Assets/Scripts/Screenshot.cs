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
//using UnityEditor.VersionControl;

public class Screenshot : MonoBehaviour
{
    public string CamFolderAlias = "IP_Snapshot";
    public string StorageFolderAlias = "image";

    private bool isPhoto = false;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) /*isPhoto == true*/)
        {
            DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
            string fileName = now.ToUnixTimeSeconds() + "-cam.png";
            //ScreenCapture.CaptureScreenshot("GameScreenshot.png");
            StartCoroutine(CoroutineScreenshot(fileName, cam));
        }
    }

    public void photoTake()
    {
        isPhoto = true;
    }

    private IEnumerator CoroutineScreenshot(string fileName, Camera cam)
    {
        yield return new WaitForEndOfFrame();

        /*int width = Screen.width;
        int height = Screen.height;
        Texture2D screenshotTexture = new Texture2D(width, height, TextureFormat.ARGB32, false);
        Rect rect = new Rect(0, 0, width, height);
        screenshotTexture.ReadPixels(rect, 0, 0);
        screenshotTexture.Apply();

        byte[] byteArray = screenshotTexture.EncodeToPNG();
        System.IO.File.WriteAllBytes(Application.dataPath + "/CameraScreenshot.png", byteArray);
        */
        try
        {
            int width = Screen.width;
            int height = Screen.height;
            /*Texture2D screenshotTexture = new Texture2D(width, height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, width, height);
            screenshotTexture.ReadPixels(rect, 0, 0);
            screenshotTexture.Apply();
            //Encode to a PNG
            byte[] bytes = screenshotTexture.EncodeToPNG();*/

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
            StartCoroutine(UploadImage(2, fileName));
            
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

    IEnumerator UploadImage(float delay, string fileName)
    {
        yield return new WaitForSeconds(delay);
        // Get a reference to the storage service, using the default Firebase App
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;
        // Create a storage reference from our storage service
        StorageReference storageRef =
            storage.GetReferenceFromUrl("gs://sungai-buloh.appspot.com/images");

        //File located on disk
        //take note we are using persistent data path here.
        //folders must be created before
        string path = Path.Combine(Application.persistentDataPath, CamFolderAlias);
        string localFile = Path.Combine(path, fileName);


        // Create a reference to the file you want to upload
        //folder must be created first
        StorageReference imgRef = storageRef.Child(Path.Combine(StorageFolderAlias, fileName)); // "images/" + fileName);
        t.text += "" + "Storage\n";
        Debug.Log(path);
        Debug.Log(localFile);

        //https://firebase.google.com/docs/storage/unity/upload-files
        // Create file metadata including the content type
        var newMetadata = new MetadataChange();
        newMetadata.ContentType = "image/png";

        // Upload the file to the path "images/rivers.jpg"
        imgRef.PutFileAsync(localFile, newMetadata)
        .ContinueWithOnMainThread((Task<StorageMetadata> task) => {
            if (task.IsFaulted || task.IsCanceled)
            {
                t.text += "" + (task.Exception.ToString());
            }
            else
            {
                // Metadata contains file metadata such as size, content-type, and download URL.
                StorageMetadata metadata = task.Result;
                string md5Hash = metadata.Md5Hash;
                t.text += "" + "pjoto/n";
            }
        });
        isPhoto = false;
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
