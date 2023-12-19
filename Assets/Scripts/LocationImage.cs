using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class LocationImage : MonoBehaviour
{
    public string addressWeb = "https://images.pexels.com/photos/17821306/pexels-photo-17821306/free-photo-of-landscape-of-hills-and-mountains.jpeg";
    public string imagePath = "";
    public string voiceOverPath = "";
    [SerializeField] int id;
    [SerializeField] Texture image;
    [SerializeField] Material skyboxMaterial;
    [SerializeField] int skyboxRotation;
    [SerializeField] GameObject environmentImage;

    [SerializeField] APIController aPIController;
    [SerializeField] List<TMP_Text> orientationText;
    [SerializeField] List<TMP_Text> descriptionText;
    [SerializeField] List<EmployeeInfo> employeeInfo = new List<EmployeeInfo>();
    [SerializeField] List<TMP_Text> policyText;
    [SerializeField] List<AudioClip> voiceOver;

    void Start()
    {
        GetOrientationTexts();
        GetEmployeeInfoTexts();
        if (environmentImage == null)
        {
            environmentImage = GameObject.Find("EnvironmentImage");
        }

        if (aPIController == null)
        {
            aPIController = GameObject.Find("API").GetComponent<APIController>();
        }
    }

    private void OnEnable()
    {
        NewLocation();
    }

    public void NewLocation()
    {
        print("New Location");
        //ApplyImage(imagePath);
        StartCoroutine(ApplyWebImage(imagePath));
        //skyboxMaterial.SetTexture("_MainTex", image);

        //LoadImage();

        //skyboxMaterial.SetInt("_Rotation", skyboxRotation);

        // Handle Rotation
        Camera.main.transform.GetComponent<CameraController>().RotateCameraOnUpVector(skyboxRotation);
        if(environmentImage!=null) environmentImage.transform.rotation = Camera.main.transform.rotation;

        //GetData();
    }


    [ContextMenu("Get orientation texts")]
    public void GetOrientationTexts()
    {
        orientationText.Clear();
        foreach (var item in transform.GetChild(0).GetComponentsInChildren<TMP_Text>())
        {
            orientationText.Add(item);
        }
    }

    [ContextMenu("Get employees texts")]
    public void GetEmployeeInfoTexts()
    {
        employeeInfo.Clear();

        if (transform.childCount < 2) return;

        var temp = transform.GetChild(2).GetComponentsInChildren<EmployeeInfo>();
        print($"Employees count: {temp.Length}");
        for(int i = 0; i<temp.Length;i++)
        {
            employeeInfo.Add(temp[i].GetEmployeeInfoTexts());
            print($"Emplyee {i} added!");
        }
    }

    [ContextMenu("Get Data")]
    /// <summary>
    /// Get the data from the API
    /// </summary>
    void GetData()
    {
        aPIController.NewLocation(id, orientationText, descriptionText, employeeInfo, policyText, voiceOver);
    }

    public void LoadImage()
    {
        print("Loading image...");
        StartCoroutine(IsDownloading(addressWeb));
    }

    IEnumerator IsDownloading(string url)
    {
        yield return new WaitForSeconds(1); // wait for one sec, without it you will have a compiler error


        var www = new WWW(url); // start a download of the given URL
        //var unityWebRequest = UnityWebRequestTexture.GetTexture(url);
        yield return www;       // wait until the download is done

        print("Image loaded");
        Texture2D texture = new Texture2D(www.texture.width, www.texture.height, TextureFormat.ASTC_10x10, false);// create a texture in DXT1/ASTC_10x10 format

        www.LoadImageIntoTexture(texture); // load data into a texture
        print($"Texture: {texture}");
        skyboxMaterial.SetTexture("_MainTex", texture);

        www.Dispose();

        www = null;
    }

    public void SetImage(Texture image)
    {
        this.image = image;
    }

    public Location GetLocation()
    {
        // Get all arrows and hotspots
        List<Arrow> arrows = new List<Arrow>();
        List<Hotspot> hotspots = new List<Hotspot>();
        List<Desk> desks = new List<Desk>();

        foreach (var item in transform.GetChild(0).GetComponentsInChildren<DirectionalHotspot>())
        {
            arrows.Add(new Arrow(item.GetTitleText(), item.transform.position, item.GetVisualsRotation(), item.transform.localScale, item.destinationId, item.isHidden, ColorUtility.ToHtmlStringRGBA(item.GetColor())));
        }

        foreach (var item in transform.GetChild(0).GetComponentsInChildren<NonDirectionalHotspot>())
        {
            hotspots.Add(new Hotspot(item.GetTitleText(), item.transform.position, item.transform.GetChild(0).transform.rotation, item.transform.localScale, item.isHidden));
        }

        foreach (var item in transform.GetChild(0).GetComponentsInChildren<EmployeeInfo>())
        {
            desks.Add(new Desk(item.GetEmployeeName(), item.transform.position, item.transform.GetChild(0).transform.rotation, item.transform.localScale, item.isHidden, item.GetEmployeeDepartment(), item.GetEmployeeDescription()));
        }

        Location location = new Location(id, imagePath, voiceOverPath, arrows, hotspots, desks);

        return location;
    }

    public void SetLocation(Location location)
    {
        this.id = location.id;
        this.imagePath = location.imagePath;
        this.voiceOverPath = location.voiceOverPath;
        StartCoroutine(ApplyWebImage(location.imagePath));
    }

    public void ApplyImage(string path)
    {
        // load texture from the given path
        if (path.Length <= 0) return;
        byte[] bytes = File.ReadAllBytes(path);
        Texture2D loadTexture = new Texture2D(1, 1);
        loadTexture.LoadImage(bytes);
        image = loadTexture;
    }

    IEnumerator ApplyWebImage(string url)
    {
        var loader = new WWW(url);
        yield return loader;
        image = loader.texture;
        skyboxMaterial.SetTexture("_MainTex", image);
    }
}
