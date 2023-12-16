using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ImagesHandler : MonoBehaviour
{
    [SerializeField] GameObject imagePrefab;
    [SerializeField] Transform parent;
    [SerializeField] List<GameObject> addedImages;

    public Texture image;
    void Start()
    {
        
    }

    public void AddNewImage(int id, string imagePath, string voiceOverPath)
    {
        // load texture from the given path
        byte[] bytes = File.ReadAllBytes(imagePath);
        Texture2D loadTexture = new Texture2D(1, 1);
        loadTexture.LoadImage(bytes);
        image = loadTexture;

        // Instantiate the new image prefab
        addedImages.Add(Instantiate(imagePrefab, parent));
        for (int i = 0; i < parent.childCount; i++)
        {
            parent.GetChild(i).gameObject.SetActive(false);
        }
        // Send the image to the Location and activate it
        var temp = addedImages[addedImages.Count - 1].GetComponent<LocationImage>();
        temp.SetImage(image);
        Location location = new Location(id, imagePath, voiceOverPath, new List<Arrow>(), new List<Hotspot>(), new List<Desk>());
        temp.SetLocation(location);
        temp.gameObject.SetActive(true);
    }

}
