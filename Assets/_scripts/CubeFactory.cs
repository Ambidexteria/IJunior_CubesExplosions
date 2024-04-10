using UnityEngine;

public class CubeFactory : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    public Cube CreateCube(Transform point)
    {
        Cube spawnedCube = Instantiate(_cubePrefab, point.position, Quaternion.identity);
        Recolor(spawnedCube.gameObject.GetComponent<Renderer>().material);

        return spawnedCube;
    }

    private void Recolor(Material material)
    {
        float red = Random.value;
        float green = Random.value;
        float blue = Random.value;

        material.color = new Color(red, green, blue);
    }
}
