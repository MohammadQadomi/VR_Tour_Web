using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RightClickMenu : MonoBehaviour
{
    [SerializeField] List<GameObject> menuItems;

    // for UI raycast
    [SerializeField] GraphicRaycaster m_Raycaster;
    [SerializeField] PointerEventData m_PointerEventData;
    [SerializeField] EventSystem m_EventSystem;

    Camera mainCamera;
    void Awake()
    {
        mainCamera = Camera.main;
        menuItems.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            menuItems.Add(transform.GetChild(i).gameObject);
        }
    }

    void Update()
    {
        // Activate the menu when the right mouse clicked
        if (Input.GetMouseButtonUp(1))
        {
            transform.position = Input.mousePosition;
            ActivateMenuItems(true);
        }

        // Deactivate the menu when the left mouse clicked
        if (Input.GetMouseButtonUp(0))
        {
            ActivateMenuItems(false);
        }
    }

    void ActivateMenuItems(bool active)
    {
        foreach (var item in menuItems)
        {
            item.SetActive(active);
        }
    }
}