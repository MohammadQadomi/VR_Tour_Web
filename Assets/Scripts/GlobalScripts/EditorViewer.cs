using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorViewer : MonoBehaviour
{
    public bool isViewer;
    public static EditorViewer instance;

    void Awake()
    {
        instance = this;
    }
}
