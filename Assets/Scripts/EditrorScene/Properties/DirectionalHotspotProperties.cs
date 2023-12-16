using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DirectionalHotspotProperties : MonoBehaviour
{
    public DirectionalHotspot selectedObject;
    [SerializeField] TMP_InputField inputField_1;
    [SerializeField] TMP_InputField inputField_2;
    [SerializeField] Toggle toggle;

    void Start()
    {
        
    }

    private void OnEnable()
    {
        DeactivateAllSiblings();
    }

    public void UpdateSelectedObjectValues()
    {
        selectedObject.SetTitleText(inputField_1.text);
        selectedObject.SetDestinationId(inputField_2.text);
        selectedObject.SetIsHidden(toggle.isOn);
    }

    public void UpdatePropertiesValues()
    {
        inputField_1.text = selectedObject.GetTitleText();
        inputField_2.text = selectedObject.destinationId.ToString();
        toggle.isOn = selectedObject.isHidden;
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

        UpdatePropertiesValues();// Set initial values for the fields
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
