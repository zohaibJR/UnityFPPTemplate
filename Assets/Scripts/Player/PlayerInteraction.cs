using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float distance = 3f;
    public LayerMask interactableLayer;

    private Camera cam;
    private Ray ray;
    private RaycastHit hit;
    private bool isHittingInteractable;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        ray = new Ray(cam.transform.position, cam.transform.forward);

        isHittingInteractable = Physics.Raycast(ray, out hit, distance, interactableLayer);

        if (isHittingInteractable)
        {
            // Optional: Tag check too (if you want both layer + tag support)
            if (hit.collider.CompareTag("Interactable"))
            {
                Debug.Log("RayCast hit Interactable Object: " + hit.collider.name);
            }
        }
    }

    void OnDrawGizmos()
    {
        if (cam == null) cam = Camera.main;
        if (cam == null) return;

        Gizmos.color = Color.red;

        Vector3 origin = cam.transform.position;
        Vector3 direction = cam.transform.forward;

        // Draw only when ray exists
        Gizmos.DrawRay(origin, direction * distance);

        // Optional: show hit point
        if (Application.isPlaying && isHittingInteractable)
        {
            Gizmos.DrawSphere(hit.point, 0.05f);
        }
    }
}