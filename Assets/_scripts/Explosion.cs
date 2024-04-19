using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _radius;
    [SerializeField] private ParticleSystem _effect;

    public void Explode(float forceMultiplyer = 1.0f, float radiusMultiplyer = 1.0f)
    {
        Vector3 position = transform.position;

        Collider[] hits = Physics.OverlapSphere(position, _radius * radiusMultiplyer);

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                Vector3 direction = (hit.transform.position - position).normalized;
                float distance = Vector3.Distance(hit.transform.position, position);
                float explosionForce = forceMultiplyer * _explosionForce * (_radius - distance) / _radius;

                hit.attachedRigidbody.AddForce(explosionForce * direction, ForceMode.Force);
            }
        }

        Instantiate(_effect, position, Quaternion.identity);

        Destroy(gameObject, _effect.main.duration);
    }
}