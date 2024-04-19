using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private float _dividingChance = 1.0f;
    private float _forceMultiplyer = 1.0f;
    private float _radiusMultiplyer = 1.0f;

    public Action<Cube> Clicked;

    public float DividingChance => _dividingChance;
    public float ForceMultiplyer => _forceMultiplyer;
    public float RadiusMultiplyer => _radiusMultiplyer;

    private void OnMouseUpAsButton()
    {
        Clicked?.Invoke(this);
    }

    public void SetDividingChance(float dividingChance)
    {
        _dividingChance = Mathf.Clamp01(dividingChance);
    }

    public void SetExplosionSettings(float forceMultiplyer, float radiusMultiplyer)
    {
        if (forceMultiplyer <= 0.0f || radiusMultiplyer <= 0.0f)
            throw new ArgumentException(nameof(SetExplosionSettings));

        _forceMultiplyer = forceMultiplyer;
        _radiusMultiplyer = radiusMultiplyer;
    }
}
