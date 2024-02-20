using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float fuseDuration;
    private float _explodeTime;
    [SerializeField] float explosionRadius;
    [SerializeField] AnimationCurve explosionAttenuation;
    [SerializeField] float explosionStrength;
    [SerializeField] GameObject explosionEffect;
    [SerializeField] float damage;

    private bool _fuseLit;

    public void LightFuse()
    {
        _fuseLit = true;
        _explodeTime = Time.timeSinceLevelLoad + fuseDuration;

    }

    private void Update()
    {
        if (!_fuseLit) return;
        if(Time.timeSinceLevelLoad > _explodeTime)
        {
            Explode();
            Destroy(gameObject);
        }
    }

    public void Explode()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Collider[] hitByExplosive = Physics.OverlapSphere(transform.position, explosionRadius);

        for (int i = 0; i < hitByExplosive.Length; i++)
        {
            float attenuatedStrength = explosionAttenuation.Evaluate(Vector3.Distance(transform.position, hitByExplosive[i].transform.position) / explosionRadius);


            if (hitByExplosive[i].TryGetComponent(out Rigidbody rb))
            {
                rb.AddExplosionForce(attenuatedStrength * explosionStrength, transform.position, explosionRadius);
            }

            if (hitByExplosive[i].TryGetComponent(out HealthContainer hp))
            {
                hp.Damage(attenuatedStrength * damage);
            }
        }
    }

}
