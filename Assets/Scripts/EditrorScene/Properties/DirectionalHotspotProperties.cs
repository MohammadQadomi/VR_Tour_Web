using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TS.ColorPicker;

public class DirectionalHotspotProperties : MonoBehaviour
{
    public DirectionalHotspot selectedObject;
    [SerializeField] TMP_InputField inputField_1;
    [SerializeField] TMP_InputField inputField_2;
    [SerializeField] InputField inputField_3;
    [SerializeField] Image colorImage;
    [SerializeField] Toggle toggle;
    [SerializeField] ColorPicker colorPicker;

    void Start()
    {
        
    }

    private void OnEnable()
    {
        DeactivateAllSiblings();
        UpdatePropertiesValues();
    }

    public void UpdateSelectedObjectValues()
    {
        selectedObject.SetTitleText(inputField_1.text);
        selectedObject.SetDestinationId(inputField_2.text);
        selectedObject.SetIsHidden(toggle.isOn);

        Color color;
        if (ColorUtility.TryParseHtmlString("#"+inputField_3.text, out color))
        {
            selectedObject.SetColor(color);
            print($"Changed arrow color to {color}");
        }
    }

    public void UpdatePropertiesValues()
    {
        inputField_1.text = selectedObject.GetTitleText();
        inputField_2.text = selectedObject.destinationId.ToString();
        toggle.isOn = selectedObject.isHidden;
        colorImage.color = selectedObject.GetColor();
        colorPicker.UpdateColor(selectedObject.GetColor());
        //inputField_3.text = ColorUtility.ToHtmlStringRGBA(selectedObject.GetColor());
    }

    void DeactivateAllSiblings()
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i) != this.transform)
            {
                transform.parent.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void Activate(DirectionalHotspot selectedObject)
    {
        if (selectedObject == null)
        {
            gameObject.SetActive(false);
            return;
        }

        this.selectedObject = selectedObject;
        gameObject.SetActive(true);
        UpdatePropertiesValues();
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void DeleteSelectedObject()
    {
        Destroy(selectedObject.gameObject);
    }
}
