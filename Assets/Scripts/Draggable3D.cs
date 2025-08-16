using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable3D : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Camera mainCamera;
    private Vector3 offset;
    private bool isDragging = false;
    [SerializeField] private float dragSpeed = 0.1f; // Higher for more responsive dragging (0.05–0.2)
    [SerializeField] private float fixedZPosition = 5f; // Fixed z-position for 2D-like movement

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found for Draggable3D on " + gameObject.name);
            enabled = false;
            return;
        }

        Collider collider = GetComponent<Collider>();
        if (collider == null)
        {
            Debug.LogError("No Collider on " + gameObject.name + ". Adding BoxCollider as fallback.");
            gameObject.AddComponent<BoxCollider>();
        }

        if (mainCamera.GetComponent<PhysicsRaycaster>() == null)
        {
            Debug.LogWarning("No PhysicsRaycaster on Main Camera. Adding one.");
            mainCamera.gameObject.AddComponent<PhysicsRaycaster>();
        }

        // Ensure object starts at fixed z
        transform.position = new Vector3(transform.position.x, transform.position.y, fixedZPosition);
        Debug.Log($"Camera: {mainCamera.transform.position}, Rotation: {mainCamera.transform.rotation.eulerAngles}");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log($"OnPointerDown on {gameObject.name} at mouse position: {Input.mousePosition}");
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = fixedZPosition; // Use fixed z for screen-to-world conversion
        offset = transform.position - mainCamera.ScreenToWorldPoint(mousePos);
        Debug.Log($"Offset: {offset}");
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging || mainCamera == null)
        {
            Debug.LogWarning("OnDrag skipped: isDragging=" + isDragging + ", mainCamera=" + (mainCamera == null));
            return;
        }
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = fixedZPosition;
        Vector3 targetPosition = mainCamera.ScreenToWorldPoint(mousePos) + offset;
        // Move only in x-y plane, lock z
        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, dragSpeed);
        transform.position = new Vector3(newPosition.x, newPosition.y, fixedZPosition);
        Debug.Log($"Dragging {gameObject.name} to position: {transform.position}, Mouse: {mousePos}");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log($"OnPointerUp on {gameObject.name}");
        isDragging = false;
    }
}