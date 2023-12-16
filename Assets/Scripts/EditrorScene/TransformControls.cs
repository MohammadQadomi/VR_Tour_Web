using UnityEngine;

public class TransformControls : MonoBehaviour
{
    [SerializeField] private Transform selectedTransform;
    [SerializeField] private Vector3 initialMousePosition;

    private void Update()
    {
        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Raycast to select an object
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                selectedTransform = hit.transform;
                initialMousePosition = Input.mousePosition;
            }
        }

        // Check if an object is selected
        if (selectedTransform != null)
        {
            // Calculate the difference in mouse position
            Vector3 mouseDelta = Input.mousePosition - initialMousePosition;

            // Update the selected object's transform
            if (Input.GetKey(KeyCode.W)) // Move
            {
                selectedTransform.position += Camera.main.transform.forward * mouseDelta.y * 0.01f;
            }
            else if (Input.GetKey(KeyCode.E)) // Rotate
            {
                selectedTransform.Rotate(Vector3.up, -mouseDelta.x);
            }
            else if (Input.GetKey(KeyCode.R)) // Scale
            {
                float scaleDelta = mouseDelta.y * 0.01f;
                selectedTransform.localScale += new Vector3(scaleDelta, scaleDelta, scaleDelta);
            }

            // Update the initial mouse position for the next frame
            initialMousePosition = Input.mousePosition;

            // Deselect the object when the right mouse button is clicked
            if (Input.GetMouseButtonDown(1))
            {
                selectedTransform = null;
            }
        }
    }
}