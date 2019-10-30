using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SR_RenderCamera : MonoBehaviour
{
    public int FileCounter = 0;

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            CamCapture();
        }
    }

    void CamCapture()
    {
        Camera Cam = GetComponent<Camera>();

        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = Cam.targetTexture;

        Cam.Render();

        Texture2D Image = new Texture2D(Cam.pixelWidth, Cam.pixelHeight);
        Image.ReadPixels(new Rect(0, 0, Cam.pixelWidth, Cam.pixelHeight), 0, 0);
        Image.Apply();
        RenderTexture.active = currentRT;

        var Bytes = Image.EncodeToPNG();
        Destroy(Image);

        string folderName = "C:/Users/g8-baker/Documents/Greg's Documents/PhD/VR Teleop Mobile Robot/";
        string picName = "waypoint_control_10_" + FileCounter + ".jpg";
        string filename = folderName + picName;

        File.WriteAllBytes(filename, Bytes);
        FileCounter++;

    }
}
