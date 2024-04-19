using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Explosion _explosionPrefab;
    [SerializeField] private float _forceMultiplyer = 1.25f;
    [SerializeField] private float _radiusMultiplyer = 1.25f;

    [SerializeField, Space(30)] private CubeFactory _cubeFactory;
    [SerializeField] private float _scaleMultiplyer = 0.5f;
    [SerializeField] private float _dividingChanceMultiplyer = 0.5f;
    [SerializeField] private int _cubeAmountMin = 2;
    [SerializeField] private int _cubeAmountMax = 4;
    [SerializeField] private List<Cube> _spawnedCubes;

    private void Start()
    {
        SubscribeOnCubes();
    }

    private void SubscribeOnCubes()
    {
        if (_spawnedCubes.Count > 0)
            foreach (Cube cube in _spawnedCubes)
                cube.Clicked += SpawnSmallerCubes;
    }

    private void SpawnSmallerCubes(Cube parent)
    {
        if (Random.value < parent.DividingChance)
        {
            int cubesAmount = Random.Range(_cubeAmountMin, _cubeAmountMax);

            for (int i = 0; i < cubesAmount; i++)
                Spawn(parent);
        }
        else
        {
            Explosion explosion = Instantiate(_explosionPrefab, parent.transform.position, Quaternion.identity);
            explosion.Explode(parent.ForceMultiplyer, parent.RadiusMultiplyer);
        }

        DestroyCube(parent);
    }

    private void Spawn(Cube parent)
    {
        Cube spawnedCube = _cubeFactory.CreateCube(parent.transform);
        spawnedCube.transform.localScale = parent.transform.localScale * _scaleMultiplyer;
        spawnedCube.Clicked += SpawnSmallerCubes;

        float dividingChance = parent.DividingChance * _dividingChanceMultiplyer;
        spawnedCube.SetDividingChance(dividingChance);

        float forceMultiplyer = parent.ForceMultiplyer * _forceMultiplyer;
        float radiusMultiplyer = parent.RadiusMultiplyer * _radiusMultiplyer;
        spawnedCube.SetExplosionSettings(forceMultiplyer, radiusMultiplyer);

        _spawnedCubes.Add(spawnedCube);
    }

    private void DestroyCube(Cube cube)
    {
        _spawnedCubes.Remove(cube);
        Destroy(cube.gameObject);
    }
}