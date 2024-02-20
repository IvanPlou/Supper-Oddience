using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Gun : MonoBehaviour
{
    [SerializeField] GameObject casing;
    [SerializeField] GameObject projectile;

    [SerializeField] float projectileSpeed;
    [SerializeField] float ejectSpeed;

    [SerializeField] float recoilForce;
    [SerializeField] float fireRate;
    [SerializeField] Transform muzzleTransform;
    [SerializeField] Transform casingTransform;

    private float nextFireTime;

    [SerializeField] GameObject shootEffect;
    [SerializeField] AudioSource gunAudio;

    [SerializeField] bool auto;

    bool _isFiring;

    private Rigidbody _rb;

    [SerializeField] int bullets;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void PullTrigger()
    {
        if (Time.timeSinceLevelLoad < nextFireTime || bullets < 1) return;
        _isFiring = true;
        _rb.AddForce (transform.forward * recoilForce);
        nextFireTime = Time.timeSinceLevelLoad + (1 / fireRate);

        GameObject _projectile = Instantiate(projectile, muzzleTransform.position, Quaternion.identity);
        if (_projectile.TryGetComponent(out Rigidbody prb)) prb.velocity = transform.forward * projectileSpeed;

        _projectile.transform.forward = muzzleTransform.forward;

        GameObject _casing = Instantiate(casing, casingTransform.position, Quaternion.identity);
        if (_casing.TryGetComponent(out Rigidbody crb)) crb.velocity = transform.forward * ejectSpeed;

        _casing.transform.right = muzzleTransform.forward;

        Instantiate(shootEffect, muzzleTransform.position, Quaternion.identity).transform.forward = muzzleTransform.forward;
        bullets--;
    }

    public void ReleaseTrigger()
    {
        _isFiring = false;
    }

    private void Update()
    {
        if (_isFiring && auto) PullTrigger();
    }

}
