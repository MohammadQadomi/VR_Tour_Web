using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DirectionalHotspot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] EnvironmentImage environmentImage;
    public GameObject nextLocation;
    [SerializeField] GameObject visuals;
    GameObject parentObject;

    public float originalScale;
    public float scaleUpScale;
    public int destinationId;
    public bool isHidden;
    bool scaleUp = false; // To handle scale up and down interruption

    [SerializeField] APITest aPITest;
    [SerializeField] TMP_Text titleText;
    public string GetTitleText() => titleText.text;
    [SerializeField] TMP_InputField titleInputField;
    [SerializeField] TMP_InputField iDInputField;
    [SerializeField] Button button;
    void Start()
    {
        if(titleInputField != null) titleInputField.text = titleText.text;
        if(iDInputField != null) iDInputField.text = destinationId.ToString();

        if (aPITest == null)
        {
            aPITest = GameObject.FindGameObjectWithTag("API").GetComponent<APITest>();
        }
        originalScale = transform.lossyScale.x;
        scaleUpScale = originalScale * 1.07f;
        if (EditorViewer.instance.isViewer) 
        {
            //GetComponent<Button>().onClick.AddListener(EnvironmentTransition);
            button.onClick.AddListener(LoadLocation);
            print("You are in the viewer!");
        }
        parentObject = transform.parent.parent.gameObject;

        Hide(isHidden);// Hide the object if it's hidden
    }

    private void OnEnable()
    {
        Hide(isHidden);
    }

    void EnvironmentTransition()
    {
        // Scale arrow down
        ScaleArrowDown();
        scaleUp = false;
        
        environmentImage.LeanTransition(transform); // Image Transition effect
        nextLocation.SetActive(true); // Activate the next location's arrows
        parentObject.SetActive(false); // Disable the current arrows
    }

    public void ScaleArrowUp()
    {
        //transform.LeanScale(Vector3.one * scaleUpScale, 0.5f).setEase(LeanTweenType.easeOutCubic).setOnComplete(CheckArrowScale);
    }
    public void ScaleArrowDown()
    {
        //transform.LeanScale(Vector3.one * originalScale, 0.5f).setEase(LeanTweenType.easeOutCubic).setOnComplete(CheckArrowScale);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ScaleArrowUp();
        scaleUp = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ScaleArrowDown();
        scaleUp = false;
    }

    void CheckArrowScale()
    {
        if (scaleUp) ScaleArrowUp();
        else ScaleArrowDown();
    }

    public void SetEditable(bool editable)
    {
        if (editable)
        {
            this.gameObject.tag = "Editable";
        }
        else
        {
            this.gameObject.tag = "NotEditable";
        }
    }

    public void LoadLocation()
    {
        aPITest.GetData(destinationId);        
    }

    public void UpdateScaleValues()
    {
        originalScale = transform.lossyScale.x;
        scaleUpScale = originalScale * 1.07f;
    }
    public void SetTitleText(string text)
    {
        titleText.text = text;
    }

    public void SetTitleText(TMP_InputField inputfield)
    {
        titleText.text = inputfield.text;
    }

    public void SetDestinationId(string text)
    {
        if (text.Length < 1) return;
        destinationId = int.Parse(text);
    }

    public void SetDestinationId(int id)
    {
        destinationId = id;
    }

    public void SetDestinationId(TMP_InputField inputfield)
    {
        SetDestinationId(inputfield.text);
    }

    public void SetIsHidden(bool isHidden)
    {
        this.isHidden = isHidden;
    }

    public void SetVisualsRotation(Quaternion rotation)
    {
        visuals.transform.rotation = rotation;
    }

    public Quaternion GetVisualsRotation() => visuals.transform.rotation;

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
}
