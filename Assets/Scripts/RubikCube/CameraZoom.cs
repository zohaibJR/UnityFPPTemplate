using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 5f;
    public float minZoom = 3f;
    public float maxZoom = 10f;

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0f)
        {
            Debug.Log("Scrolling");
            Vector3 pos = transform.position;

            // move forward/backward in camera forward direction
            pos += transform.forward * scroll * zoomSpeed;

            float distance = Vector3.Distance(pos, Vector3.zero);

            // clamp zoom range
            if (distance > minZoom && distance < maxZoom)
            {
                transform.position = pos;
            }
        }
    }
}