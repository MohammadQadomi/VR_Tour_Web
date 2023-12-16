using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoadLocationButton : MonoBehaviour
{
    [SerializeField] TMP_InputField idInputField;
    [SerializeField] APITest aPITest;

    Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(LoadLocation);
    }

    void LoadLocation()
    {
        aPITest.GetData(int.Parse(idInputField.text));

        transform.parent.gameObject.SetActive(false);
    }
}
