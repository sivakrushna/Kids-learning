using UnityEngine;

public class Drag3DObject : MonoBehaviour
{
    private Vector3 offset;          // Offset between mouse and object position
    private float objectZ;           // Object's Z position in world space
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void OnMouseDown()
    {
        // Get object's Z in screen space
        objectZ = cam.WorldToScreenPoint(transform.position).z;

        // Calculate offset between mouse position and object position
        Vector3 mouseScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, objectZ);
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(mouseScreenPos);
        offset = transform.position - mouseWorldPos;
    }

    void OnMouseDrag()
    {
        // Keep same Z, only update X/Y from mouse
        Vector3 mouseScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, objectZ);
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(mouseScreenPos);

        transform.position = mouseWorldPos + offset;
    }
}
