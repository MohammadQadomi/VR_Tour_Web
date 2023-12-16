using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TransformTools : MonoBehaviour
{
    [SerializeField] GameObject activeObject; // Object to be edited
    [SerializeField] CameraController cameraController;

    Camera mainCamera;
    // for UI raycast
    [SerializeField] GraphicRaycaster m_Raycaster;
    [SerializeField] PointerEventData m_PointerEventData;
    [SerializeField] EventSystem m_EventSystem;

    // Tools
    [SerializeField] List<Toggle> toolsToggles;
    [SerializeField] List<bool> tools;

    // Properties
    [SerializeField] DirectionalHotspotProperties directionalHotspotProperties;
    [SerializeField] NonDirectionalHotspotProperties nonDirectionalHotspotProperties;
    [SerializeField] DeskProperties deskProperties;
    void Start()
    {
        mainCamera = Camera.main;
        tools = new List<bool> {false, false, false };// Position, Rotation, Scale
        //m_Raycaster = GetComponent<GraphicRaycaster>();
        //m_EventSystem = GetComponent<EventSystem>();
    }

    void Update()
    {
        if (activeObject != null)
        {
            ChangePosition();
            ChangeRotation();
            ChangeScale();
        }

        if (Input.GetMouseButtonDown(0))
        {
            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            m_Raycaster.Raycast(m_PointerEventData, results);
            if (results.Count > 0)
            {
                foreach (RaycastResult result in results)
                {
                    Debug.Log("Hit " + result.gameObject.name);
                    if (result.gameObject.CompareTag("Editable"))
                    {
                        Debug.Log("Editable " + result.gameObject.name);
                        activeObject = result.gameObject.transform.parent.parent.gameObject;
                        ActivateProperties(activeObject);// Activate the right properties for the selected object
                    }
                    else
                    {
                        activeObject = null;
                        ActivateProperties();// Deactivate the active properties
                    }
                }
            }
            else
            {
                activeObject = null;
                ActivateProperties();// Deactivate the active properties
            }
        }
    }

    public void UpdateToolsStatus()
    {
        if (tools.Count != toolsToggles.Count) return;

        for (int i = 0; i < toolsToggles.Count; i++)
        {
            tools[i] = toolsToggles[i].isOn;
        }
    }

    void ChangePosition()
    {
        if (tools[0] == false) return;

        if (Input.GetMouseButton(0))
        {
            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            m_Raycaster.Raycast(m_PointerEventData, results);
            if (results.Count > 0)
            {
                foreach (RaycastResult result in results)
                {
                    if(result.gameObject.transform.parent.parent.gameObject == activeObject)
                    {
                        if (Input.GetMouseButton(0))
                        {
                            RaycastHit hit;
                            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                            if (Physics.Raycast(ray, out hit, 10000, LayerMask.GetMask("Sphere")))
                            {
                                Debug.DrawRay(ray.origin, hit.point, Color.green);
                                activeObject.transform.position = hit.point;
                                var activeObjectRotation = activeObject.transform.rotation.eulerAngles;
                                var lookAtRotation = Quaternion.LookRotation(activeObject.transform.position - Camera.main.transform.position);
                                var temp = lookAtRotation.eulerAngles;
                                temp.z = activeObjectRotation.z;
                                lookAtRotation.eulerAngles = temp;
                                activeObject.transform.rotation = lookAtRotation;

                                cameraController.StopRotating();
                            }
                        }
                    }
                }
            }
        }
    }

    void ChangeRotation()
    {
        if (tools[1] == false) return;

        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            activeObject.transform.GetChild(0).Rotate(Vector3.back, mouseX * 5);
            cameraController.StopRotating();
        }
    }

    void ChangeScale()
    {
        if (tools[2] == false) return;

        if (Input.GetMouseButton(0))
        {
            float mouseY = Input.GetAxis("Mouse Y");
            float mouseX = Input.GetAxis("Mouse X");
            float avg = (mouseX + mouseY) / 2;

            // Calculate the new scale based on the mouse movement
            Vector3 newScale = activeObject.transform.localScale + Vector3.one * avg;
            // Clamp the scale to prevent it from becoming too small or too large
            newScale = Vector3.Max(newScale, Vector3.one * 0.1f);
            activeObject.transform.localScale = newScale;
            cameraController.StopRotating();
        }
    }

    public void ActivateProperties(GameObject selectedObject=null)
    {
        if (selectedObject == null) return;

        if(selectedObject.TryGetComponent<DirectionalHotspot>(out DirectionalHotspot directionalHotspot))
        {
            directionalHotspotProperties.Activate(directionalHotspot);
        }

        if (selectedObject.TryGetComponent<NonDirectionalHotspot>(out NonDirectionalHotspot nonDirectionalHotspot))
        {
            nonDirectionalHotspotProperties.Activate(nonDirectionalHotspot);
        }

        if (selectedObject.TryGetComponent<EmployeeInfo>(out EmployeeInfo employeeInfo))
        {
            deskProperties.Activate(employeeInfo);
        }
    }
}
