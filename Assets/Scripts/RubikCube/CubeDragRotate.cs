using UnityEngine;

public class CubeDragRotate : MonoBehaviour
{
    public float rotationSpeed = 0.2f;

    private Vector2 lastTouchPos;
    private bool isDragging;

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isDragging = true;
                lastTouchPos = touch.position;
            }

            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                Vector2 delta = touch.position - lastTouchPos;

                float rotX = delta.y * rotationSpeed;
                float rotY = -delta.x * rotationSpeed;

                // rotate whole cube
                transform.Rotate(Vector3.up, rotY, Space.World);
                transform.Rotate(Vector3.right, rotX, Space.World);

                lastTouchPos = touch.position;
            }

            else if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false;
            }
        }
    }
}