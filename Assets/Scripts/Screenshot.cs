using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            //ScreenCapture.CaptureScreenshot("GameScreenshot.png);
            StartCoroutine(CoroutineScreenshot());
        }
    }

    private IEnumerator CoroutineScreenshot()
    {
        yield return new WaitForEndOfFrame();

        int width = Screen.width;
        int height = Screen.height;
        Texture2D screenshotTexture = new Texture2D(width, height, TextureFormat.ARGB32, false);
        Rect rect = new Rect(0, 0, width, height);
        screenshotTexture.ReadPixels(rect, 0, 0);
        screenshotTexture.Apply();

        byte[] byteArray = screenshotTexture.EncodeToPNG();
        System.IO.File.WriteAllBytes(Application.dataPath + "/CameraScreenshot.png", byteArray);
    }
}
