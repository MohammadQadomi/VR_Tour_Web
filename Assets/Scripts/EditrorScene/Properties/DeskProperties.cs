using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeskProperties : MonoBehaviour
{
    public EmployeeInfo selectedDesk;
    [SerializeField] TMP_InputField inputField_1;
    [SerializeField] TMP_InputField inputField_2;
    [SerializeField] TMP_InputField inputField_3;
    [SerializeField] Toggle toggle;

    void Start()
    {
        
    }
    private void OnEnable()
    {
        DeactivateAllSiblings();
    }

    public void Activate(EmployeeInfo selectedDesk)
    {
        this.selectedDesk = selectedDesk;
        gameObject.SetActive(true);
        UpdatePropertiesValues();
    }

    public void UpdateSelectedObjectValues()
    {
        selectedDesk.SetEmployeeData(inputField_1.text, inputField_2.text, inputField_3.text, "");
        selectedDesk.SetIsHidden(toggle.isOn);
    }
    public void UpdateSelectedObjectValues(int value)
    {
        if (value == 1) selectedDesk.SetEmployeeName(inputField_1.text);
        if (value == 2) selectedDesk.SetEmployeeDepartment(inputField_2.text);
        if (value == 3) selectedDesk.SetEmployeeDescription(inputField_3.text);
        if (value == 4) selectedDesk.switchTagCard();
        if (value == 5) selectedDesk.SetIsHidden(toggle.isOn);
        if (value == 6) Destroy(selectedDesk.gameObject);
    }

    public void UpdatePropertiesValues()
    {
        inputField_1.text = selectedDesk.GetEmployeeName();
        inputField_2.text = selectedDesk.GetEmployeeDepartment();
        inputField_3.text = selectedDesk.GetEmployeeDescription();
        toggle.isOn = selectedDesk.isHidden;
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

}
