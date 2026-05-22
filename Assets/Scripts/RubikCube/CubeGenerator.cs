using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    public GameObject cubiePrefab;

    void Start()
    {
        for (int x = 0; x < 3; x++)
            for (int y = 0; y < 3; y++)
                for (int z = 0; z < 3; z++)
                {
                    Vector3 pos = new Vector3(x - 1, y - 1, z - 1);

                    GameObject cubie = Instantiate(cubiePrefab, pos, Quaternion.identity, transform);

                    cubie.GetComponent<Cubie>().SetGridPosition(pos);
                }
    }
}