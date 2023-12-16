using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocationNavigationElements : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] GameObject editableArrowPrefab;
    [SerializeField] GameObject uneditableArrowPrefab;

    [SerializeField] GameObject hotspotPrefab;
    [SerializeField] GameObject editableHotspotPrefab;
    [SerializeField] GameObject uneditableHotspotPrefab;

    [SerializeField] GameObject deskPrefab;
    [SerializeField] GameObject editableDeskPrefab;
    [SerializeField] GameObject uneditableDeskPrefab;

    [SerializeField] GameObject ORParent;
    void Start()
    {
        ORParent = transform.GetChild(0).gameObject;
    }


    public void CreateNavigationElements(Location location)
    {
        // Change the arrow and hotspot prefabs based on if it's viewer or editor
        if (EditorViewer.instance.isViewer)
        {
            arrowPrefab = uneditableArrowPrefab;
            hotspotPrefab = uneditableHotspotPrefab;
            deskPrefab = uneditableDeskPrefab;
        }
        else
        {
            arrowPrefab = editableArrowPrefab;
            hotspotPrefab = editableHotspotPrefab;
            deskPrefab = editableDeskPrefab;
        }

        // Instantiate the arrows
        foreach (var arrow in location.arrows)
        {
            var temp = Instantiate(arrowPrefab, new Vector3(arrow.positionX, arrow.positionY, arrow.positionZ), Quaternion.identity, ORParent.transform);
            temp.transform.localScale = new Vector3(arrow.scaleX, arrow.scaleY, arrow.scaleZ);
            var dirctionalHotspot = temp.GetComponent<DirectionalHotspot>();
            dirctionalHotspot.SetTitleText(arrow.text);
            dirctionalHotspot.SetDestinationId(arrow.destinationId);
            dirctionalHotspot.SetIsHidden(arrow.isHidden);

            dirctionalHotspot.transform.rotation = Quaternion.LookRotation(dirctionalHotspot.transform.position - Camera.main.transform.position);// Set the whole object rotation
            dirctionalHotspot.SetVisualsRotation(new Quaternion(arrow.rotationX, arrow.rotationY, arrow.rotationZ, arrow.rotationW));// Set the visuals ("Arrow") rotation

        }

        // Instantiate the hotspots
        foreach (var hotspot in location.hotspots)
        {
            var temp = Instantiate(hotspotPrefab, new Vector3(hotspot.positionX, hotspot.positionY, hotspot.positionZ), new Quaternion(hotspot.rotationX, hotspot.rotationY, hotspot.rotationZ, hotspot.rotationW), ORParent.transform);
            temp.transform.localScale = new Vector3(hotspot.scaleX, hotspot.scaleY, hotspot.scaleZ);
            var nonDirctionalHotspot = temp.GetComponent<NonDirectionalHotspot>();
            nonDirctionalHotspot.SetTitleText(hotspot.text);
            nonDirctionalHotspot.SetIsHidden(hotspot.isHidden);

            nonDirctionalHotspot.transform.rotation = Quaternion.LookRotation(nonDirctionalHotspot.transform.position - Camera.main.transform.position);// Set the whole object rotation
            nonDirctionalHotspot.SetVisualsRotation(new Quaternion(hotspot.rotationX, hotspot.rotationY, hotspot.rotationZ, hotspot.rotationW));// Set the visuals ("Hotspot") rotation
        }

        // Instantiate the desks
        foreach (var desk in location.desks)
        {
            print("Desk!");
            var temp = Instantiate(deskPrefab, new Vector3(desk.positionX, desk.positionY, desk.positionZ), new Quaternion(desk.rotationX, desk.rotationY, desk.rotationZ, desk.rotationW), ORParent.transform);
            temp.transform.localScale = new Vector3(desk.scaleX, desk.scaleY, desk.scaleZ);
            var employeeInfo = temp.GetComponent<EmployeeInfo>();
            employeeInfo.SetEmployeeData(desk.employeeName, desk.employeeDepartment, desk.employeeDescription, "");
            employeeInfo.SetIsHidden(desk.isHidden);
        }

    }
}
