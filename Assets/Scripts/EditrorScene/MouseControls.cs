using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseControls : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField] GameObject[] prefabs;
    [SerializeField] GameObject locationsParent;
    [SerializeField] CameraController cameraController;

    public List<GameObject> createdObjects;

    public GameObject activeTool;
    public GameObject activeObject;

    // for UI raycast
    [SerializeField]GraphicRaycaster m_Raycaster;
    [SerializeField]PointerEventData m_PointerEventData;
    [SerializeField]EventSystem m_EventSystem;

    void Start()
    {
        mainCamera = Camera.main;

        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();
    }

    void Update()
    {
        if (activeTool != null)
        {
            if (Input.GetMouseButton(0))
            {
                RaycastHit hit;
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 10000,LayerMask.GetMask("Sphere")))
                {
                    Debug.DrawRay(ray.origin, hit.point, Color.red);
                    var arrowParent = GetActiveLocation().transform.GetChild(0);
                    var temp = Instantiate(activeTool, hit.point, Quaternion.identity, arrowParent.transform);
                    temp.transform.rotation = Quaternion.LookRotation(temp.transform.position - Camera.main.transform.position);

                    createdObjects.Add(temp);
                    activeTool = null;
                }
            }
        }/*
        else
        {
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
                            activeObject = result.gameObject;
                            cameraController.StopRotating();
                            break;
                        }
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                print("Mouse up");
                activeObject = null;
            }

            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 10000, LayerMask.GetMask("Sphere")))
            {
                if (activeObject != null)
                {
                    Debug.DrawRay(ray.origin, hit.point, Color.green);
                    activeObject.transform.position = hit.point;
                    activeObject.transform.rotation = Quaternion.LookRotation(activeObject.transform.position - Camera.main.transform.position);
                    activeTool = null;
                }
                else
                {
                    Debug.DrawRay(ray.origin, hit.point, Color.red);
                }
            }
        }*/

    }

    public void ActivatePrefab(int index)
    {
        if (activeTool == prefabs[index]) activeTool = null;
        else activeTool = prefabs[index];
    }

    public void DeactivatePrefab()
    {
        activeTool = null;
    }

    public GameObject GetActiveLocation()
    {
        for (int i = 0; i < locationsParent.transform.childCount; i++)
        {
            if (locationsParent.transform.GetChild(i).gameObject.activeSelf)
            {
                return locationsParent.transform.GetChild(i).gameObject;
            }
        }
        return null;
    }

    public void SetActiveObject(GameObject obj)
    {
        activeObject = obj;
    }
}
