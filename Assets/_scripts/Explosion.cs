using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _radius;
    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private float _delay = 0.1f;

    private void Start()
    {
        Invoke(nameof(Explode), _delay);
    }

    public void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _radius);

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                Vector3 direction = (hit.transform.position - transform.position).normalized;
                float distance = Vector3.Distance(hit.transform.position, transform.position);
                float explosionForce = _explosionForce * (_radius - distance) / _radius;

                hit.attachedRigidbody.AddForce(explosionForce * direction, ForceMode.Force);
            }
        }
        
        Instantiate(_effect, gameObject.transform);

        Destroy(gameObject, _effect.main.duration);
    }
}