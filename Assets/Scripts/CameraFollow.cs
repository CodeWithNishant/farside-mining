using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The target for the camera to follow (usually the player)
    public Vector3 offset; // The offset distance between the camera and the target
    public float smoothSpeed = 0.125f; // The speed at which the camera will smooth follow the target
    public float scrollSpeed = 2f; // The speed at which the camera zooms in and out
    public float minZoom = 5f; // Minimum zoom size
    public float maxZoom = 20f; // Maximum zoom size

    private Camera cam; // Reference to the camera component
    private ConstructPanel constructPanel; // Reference to the ConstructPanel script

    void Start()
    {
        cam = GetComponent<Camera>();
        if (!cam.orthographic)
        {
            Debug.LogError("Camera is not orthographic! Please set the camera to Orthographic mode.");
        }

        // Find the ConstructPanel script in the scene
        constructPanel = FindFirstObjectByType<ConstructPanel>();
        if (constructPanel == null)
        {
            Debug.LogError("ConstructPanel script not found in the scene!");
        }
    }

    void Update()
    {
        // Check if the construct panel is open before processing the scroll input
        if (constructPanel != null && !constructPanel.isConstructPanelOpen)
        {
            // Zoom in and out with the mouse scroll wheel
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            cam.orthographicSize -= scroll * scrollSpeed;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
        }
    }

    void LateUpdate()
    {
        // Desired position based on target position and offset
        Vector3 desiredPosition = target.position + offset;

        // Factorio like top-down camera view
        transform.rotation = Quaternion.Euler(45f, 0f, 0f);

        // Smoothly interpolate between the current position and the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Set the camera position to the smoothed position
        transform.position = smoothedPosition;
    }
}
