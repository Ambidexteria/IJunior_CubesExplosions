using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private float _dividingChance = 1.0f;

    public Action<Transform> Clicked;

    public float DividingChance => _dividingChance;

    private void OnMouseUpAsButton()
    {
        Clicked?.Invoke(transform);
    }

    public void SetDividingChance(float dividingChance)
    {
        _dividingChance = Mathf.Clamp01(dividingChance);
    }
}
