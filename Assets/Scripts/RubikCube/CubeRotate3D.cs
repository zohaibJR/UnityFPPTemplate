using UnityEngine;

public class CubeRotate3D : MonoBehaviour
{
    public float rotationSpeed = 200f;

    private Vector2 lastTouchPos;
    private bool isDragging = false;

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                isDragging = true;
                lastTouchPos = t.position;
            }
            else if (t.phase == TouchPhase.Moved && isDragging)
            {
                Vector2 delta = t.position - lastTouchPos;

                transform.Rotate(Vector3.up, -delta.x * rotationSpeed * Time.deltaTime, Space.World);
                transform.Rotate(Vector3.right, delta.y * rotationSpeed * Time.deltaTime, Space.World);

                lastTouchPos = t.position;
            }
        }
        else
        {
            isDragging = false;
        }
    }
}