using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viewer : MonoBehaviour
{
    [SerializeField] int startIndex;
    [SerializeField] APITest aPITest;
    void Start()
    {
        LoadLocation(startIndex); // Load the location at startIndex
    }

    void LoadLocation(int index)
    {
        aPITest.GetData(index);
    }
}
