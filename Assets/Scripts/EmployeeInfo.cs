using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text;

public class EmployeeInfo : MonoBehaviour
{
    [SerializeField] TMP_Text employeeNameText;
    public string GetEmployeeName() => employeeNameText.text;

    [SerializeField] TMP_Text employeeNameCardText;

    [SerializeField] TMP_Text employeeDepartmentCardText;
    public string GetEmployeeDepartment() => employeeDepartmentCardText.text;

    [SerializeField] TMP_Text employeeDescriptionCardText;
    public string GetEmployeeDescription() => employeeDescriptionCardText.text;

    [SerializeField] Image imageHolder;
    [SerializeField] Image cardImageHolder;

    [SerializeField] LeanAnimation leanAnimation;

    public Sprite defaultProfilePicture;

    public bool isHidden;

    //Employee _employee;
    void Start()
    {
        //_employee = new Employee(employeeNameText, employeeNameCardText, employeeDescriptionCardText);
    }

    public EmployeeInfo GetEmployeeInfoTexts()
    {
        //Employee temp = new Employee(employeeNameText, employeeNameCardText, employeeDescriptionCardText);
        return this;
    }


    public void SetEmployeeData(string name="", string department="", string description="", string picture="")
    {
        employeeNameText.text = name;
        employeeNameCardText.text = name;
        employeeDepartmentCardText.text = department;
        employeeDescriptionCardText.text = description;

        //_employee.SetEmployeeTextData(name, description);
        if(picture.Length > 0) DecodeBase64ToImage(picture);
    }

    public void SetEmployeeName(string name)
    {
        employeeNameText.text = name;
        employeeNameCardText.text = name;
    }

    public void SetEmployeeDepartment(string department)
    {
        employeeDepartmentCardText.text = department;
    }

    public void SetEmployeeDescription(string description)
    {
        employeeDescriptionCardText.text = description;
    }

    public void DecodeBase64ToImage(string base64)
    {
        byte[] decodedBytes = Convert.FromBase64String(base64);
        //string decodedText = Encoding.UTF8.GetString(decodedBytes);

        Texture2D texture = new Texture2D(128, 128);
        texture.LoadImage(decodedBytes);

        var sprite = defaultProfilePicture;
        if (decodedBytes.Length > 0)
        {
            sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero, 100, 0, SpriteMeshType.FullRect, Vector4.zero, false);
        }
        imageHolder.sprite = sprite;
        cardImageHolder.sprite = sprite;
    }

    public void SetIsHidden(bool isHidden)
    {
        this.isHidden = isHidden;
    }

    public void switchTagCard()
    {
        leanAnimation.TriggerCardAnimation();
    }
}
