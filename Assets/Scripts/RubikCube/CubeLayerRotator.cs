using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeLayerRotator : MonoBehaviour
{
    public float rotationSpeed = 300f;
    private bool isRotating = false;

    void Update()
    {
        // TEST CONTROLS (PC)
        if (Input.GetKeyDown(KeyCode.R))
            Rotate(Vector3.right, 1);

        if (Input.GetKeyDown(KeyCode.U))
            Rotate(Vector3.up, 1);

        if (Input.GetKeyDown(KeyCode.F))
            Rotate(Vector3.forward, 1);
    }

    public void Rotate(Vector3 axis, int layer)
    {
        if (!isRotating)
            StartCoroutine(RotateLayer(axis, layer));
    }

    IEnumerator RotateLayer(Vector3 axis, int layerIndex)
    {
        isRotating = true;

        GameObject pivot = new GameObject("Pivot");

        List<Transform> selected = new List<Transform>();

        // STEP 1: select correct cubies using GRID
        foreach (Transform c in transform)
        {
            Cubie cubie = c.GetComponent<Cubie>();
            if (cubie == null) continue;

            Vector3Int gp = cubie.gridPos;

            bool match =
                (axis == Vector3.right && gp.x == layerIndex) ||
                (axis == Vector3.up && gp.y == layerIndex) ||
                (axis == Vector3.forward && gp.z == layerIndex);

            if (match)
                selected.Add(c);
        }

        // STEP 2: parent to pivot
        foreach (Transform c in selected)
            c.SetParent(pivot.transform);

        // STEP 3: smooth rotation
        float rotated = 0f;

        while (rotated < 90f)
        {
            float step = rotationSpeed * Time.deltaTime;
            pivot.transform.Rotate(axis, step, Space.World);
            rotated += step;
            yield return null;
        }

        pivot.transform.Rotate(axis, 90f - rotated, Space.World);

        // STEP 4: detach + snap back to grid
        foreach (Transform c in selected)
        {
            c.SetParent(transform);

            Vector3 p = c.localPosition;

            c.localPosition = new Vector3(
                Mathf.Round(p.x),
                Mathf.Round(p.y),
                Mathf.Round(p.z)
            );

            c.GetComponent<Cubie>().SetGridPosition(c.localPosition);
        }

        Destroy(pivot);

        isRotating = false;
    }
}