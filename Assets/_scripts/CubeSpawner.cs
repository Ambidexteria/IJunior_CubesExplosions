using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private CubeFactory _cubeFactory;
    [SerializeField] private float _scaleMultiplyer = 0.5f;
    [SerializeField] private float _dividingChanceMultiplyer = 0.5f;
    [SerializeField] private int _cubeAmountMin = 2;
    [SerializeField] private int _cubeAmountMax = 4;
    [SerializeField] private Explosion _explosionPrefab;
    [SerializeField] private List<Cube> _spawnedCubes;

    private void Start()
    {
        SubscribeOnCubes();
    }

    private void SubscribeOnCubes()
    {
        if (_spawnedCubes.Count > 0)
        {
            foreach (Cube cube in _spawnedCubes)
            {
                cube.Clicked += SpawnSmallerCubes;
            }
        }
    }

    private void SpawnSmallerCubes(Transform point)
    {
        if (point.gameObject.TryGetComponent(out Cube cube) == false)
            return;

        if (Random.value < cube.DividingChance)
        {
            int cubesAmount = Random.Range(_cubeAmountMin, _cubeAmountMax);

            for (int i = 0; i < cubesAmount; i++)
                Spawn(cube);

            Instantiate(_explosionPrefab, point.position, Quaternion.identity);
        }

        _spawnedCubes.Remove(cube);
        Destroy(point.gameObject);
    }

    private void Spawn(Cube parent)
    {
        Cube spawnedCube = _cubeFactory.CreateCube(parent.transform);
        spawnedCube.transform.localScale = parent.transform.localScale * _scaleMultiplyer;
        spawnedCube.Clicked += SpawnSmallerCubes;

        float dividingChance = parent.DividingChance * _dividingChanceMultiplyer;
        spawnedCube.SetDividingChance(dividingChance);

        _spawnedCubes.Add(spawnedCube);
    }
}