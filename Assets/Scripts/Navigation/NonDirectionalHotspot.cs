using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NonDirectionalHotspot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float originalScale;
    public float scaleUpScale;
    public bool isHidden;

    [SerializeField] GameObject visuals;

    bool scaleUp = false; // To handle scale up and down interruption

    [SerializeField] TMP_Text titleText;
    public string GetTitleText() => titleText.text;
    [SerializeField] TMP_InputField titleInputField;
    void Start()
    {
        if (titleInputField != null) titleInputField.text = titleText.text;
        originalScale = transform.lossyScale.x;
        scaleUpScale = originalScale * 1.07f;

        Hide(isHidden);// Hide the object if it's hidden
    }

    private void OnEnable()
    {
        Hide(isHidden);
    }

    public void ScaleHotspotUp()
    {
        transform.LeanScale(Vector3.one * scaleUpScale, 0.5f).setEase(LeanTweenType.easeOutCubic).setOnComplete(CheckHotspotScale);
    }
    public void ScaleHotspotDown()
    {
        transform.LeanScale(Vector3.one * originalScale, 0.5f).setEase(LeanTweenType.easeOutCubic).setOnComplete(CheckHotspotScale);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //ScaleHotspotUp();
        //scaleUp = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //ScaleHotspotDown();
        //scaleUp = false;
    }

    void CheckHotspotScale()
    {
        /*if (scaleUp) ScaleHotspotUp();
        else ScaleHotspotDown();*/
    }

    public void SetTitleText(string text)
    {
        titleText.text = text;
    }

    public void SetTitleText(TMP_InputField inputField)
    {
        titleText.text = inputField.text;
    }

    public void SetIsHidden(bool isHidden)
    {
        this.isHidden = isHidden;
    }

    /// <summary>
    /// Hide all children objects
    /// </summary>
    /// <param name="hide"></param>
    void Hide(bool hide)
    {
        if (!EditorViewer.instance.isViewer) return;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(!hide);
        }
    }

    public void SetVisualsRotation(Quaternion rotation)
    {
        visuals.transform.rotation = rotation;
    }

    public Quaternion GetVisualsRotation() => visuals.transform.rotation;

    public void SetColor(Color color)
    {
        visuals.GetComponentInChildren<Image>().color = color;
    }
}
