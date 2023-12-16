using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLocationTest : MonoBehaviour
{
    [SerializeField] GameObject locationPrefab;
    [SerializeField] GameObject parent;

    GameObject lastCreatedLocation;

    [SerializeField] Locations locations = new Locations();
    [SerializeField] List<GameObject> locationsOjbects;

    void Start()
    {
        
    }


    public void NewLocation(Location location)
    {
        if (location == null) return;

        if(lastCreatedLocation != null) Destroy(lastCreatedLocation);

        lastCreatedLocation = Instantiate(locationPrefab, parent.transform);

        if (lastCreatedLocation.TryGetComponent<LocationImage>(out LocationImage locationImage))
        {
            locationImage.SetLocation(location);
        }

        if (lastCreatedLocation.TryGetComponent<LocationNavigationElements>(out LocationNavigationElements locationNavigationElements))
        {
            locationNavigationElements.CreateNavigationElements(location);
        }

        // Refresh the created location image
        lastCreatedLocation.SetActive(false);
        lastCreatedLocation.SetActive(true);

        if (locations.Add(location)) 
        {
            locationsOjbects.Add(lastCreatedLocation);
        }
    }

    public Location GetCurrentLocationData()
    {
        if(lastCreatedLocation.TryGetComponent<LocationImage>(out LocationImage locationImage))
        {
            return locationImage.GetLocation();
        }
        return null;
    }
}
