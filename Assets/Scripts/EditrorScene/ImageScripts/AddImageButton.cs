using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddImageButton : MonoBehaviour
{
    [SerializeField] TMP_InputField idInputField;
    [SerializeField] TMP_InputField imagePathInputField;
    [SerializeField] TMP_InputField voiceOverPathInputField;
    [SerializeField] ImagesHandler imagesHandler;
    [SerializeField] CreateLocationTest createLocationTest;
    Button button;

    void Start()
    {
        if (button == null) button = GetComponent<Button>();
        button.onClick.AddListener(AddNewImage);
    }

    public void AddNewImage()
    {
        //imagesHandler.AddNewImage(int.Parse(idInputField.text), imagePathInputField.text);

        Location location = new Location(int.Parse(idInputField.text), imagePathInputField.text, voiceOverPathInputField.text, new List<Arrow>(), new List<Hotspot>(), new List<Desk>());
        createLocationTest.NewLocation(location);

        transform.parent.gameObject.SetActive(false);
    }
}
