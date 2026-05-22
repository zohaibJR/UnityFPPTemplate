using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Default Follow Settings")]
    public Vector3 offset = new Vector3(0f, 6f, -5f);
    public Vector3 rotationOffset = new Vector3(35f, 0f, 0f);

    [Header("Focus Point (Drag & Drop)")]
    public Transform focusPoint;

    [Header("Smooth Settings")]
    public float followSpeed = 5f;
    public float focusSpeed = 8f;

    void LateUpdate()
    {
        if (player == null)
            return;

        bool isFocusing = Input.GetMouseButton(1); // Right Click Hold

        if (isFocusing && focusPoint != null)
        {
            Debug.Log("Aiming");
            // Move to focus point position
            transform.position = Vector3.Lerp(
                transform.position,
                focusPoint.position,
                focusSpeed * Time.deltaTime
            );

            // Match focus rotation
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                focusPoint.rotation,
                focusSpeed * Time.deltaTime
            );
        }
        else
        {
            // Normal follow behavior
            Vector3 targetPosition = player.position + offset;

            transform.position = Vector3.Lerp(
                transform.position,
                targetPosition,
                followSpeed * Time.deltaTime
            );

            transform.rotation = Quaternion.Euler(rotationOffset);
        }
    }
}