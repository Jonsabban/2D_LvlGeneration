using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CreatePng : MonoBehaviour {

    private int width = Screen.width;
    private int height = Screen.height;

    Texture2D selectedMap;

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("space")) {
            UploadPNG();
        }
    }

    void UploadPNG() {

        // We should only read the screen buffer after rendering is complete
        new WaitForEndOfFrame();

        // Create a texture the size of the screen, RGB24 format
        Texture2D tex = new Texture2D(width, height, TextureFormat.ARGB32, false);

        // Read screen contents into the texture
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                if (tex.GetPixel(x, y) == Color.black) {
                    tex.SetPixel(x, y, Color.blue);
                } else {
                    tex.SetPixel(x, y, Color.clear);
                }
            }
        }

        tex.Apply();

        // Encode texture into PNG
        byte[] bytes = tex.EncodeToPNG();
        Object.Destroy(tex);

        // For testing purposes, also write to a file in the project folder
        File.WriteAllBytes(Application.dataPath + "/../SelectedMap.png", bytes);




    }
    

}
