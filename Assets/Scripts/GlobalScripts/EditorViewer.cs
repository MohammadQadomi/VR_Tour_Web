using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorViewer : MonoBehaviour
{
    public bool isViewer;
    public static EditorViewer instance {get;  private set; }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
}
