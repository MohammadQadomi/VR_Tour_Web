using SFB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilesHandler : MonoBehaviour
{
    public string[] OpenFile()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel("Open File", "C:/Unity Projects/Virtual Tour Web/Assets/Skybox/Demo", "", false);
        print($"Path: {paths[0]}");
        return paths;
        //return new string[] { "" };
    }
}
