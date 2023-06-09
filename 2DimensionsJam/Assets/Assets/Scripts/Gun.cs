using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    [Header("Gun Info")]
    public int damage = 10;
    [SerializeField] float range = 25f;
    [SerializeField] float fireRate = 1f;
    [SerializeField] float nextTimeToFire = 0.5f;

    [SerializeField] AudioSource gunAudioSource;
    [SerializeField] ParticleSystem muzzleFlash;

    [Header("Ammo Info")]
    [SerializeField] int currentAmmo = 6;
    [SerializeField] int maxClipAmmo = 6;
    [SerializeField] int reserveAmmo = 0;

    [Header("Gun Reference")]
    public GameObject gunObject;
    public WeaponPickup weaponPickup;

    //[Header("Shooting Check")]
    //public MenuInterface menuInterface;

    private void Start()
    {
        currentAmmo = maxClipAmmo;
        weaponPickup = GetComponent<WeaponPickup>();
        gunAudioSource = GetComponentInChildren<AudioSource>();
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && currentAmmo > 0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            if (currentAmmo <= 0)
            {
                weaponPickup.DropWeapon();
                Debug.Log("I have dropped weapon");
                return;
            }
            Shoot();
        }
    }

    void Shoot()
    {
        currentAmmo--;
        muzzleFlash.Play();
        if(gunAudioSource)gunAudioSource.Play();
        RaycastHit hit;
        if (Physics.Raycast(gunObject.transform.position, gunObject.transform.forward, out hit, range))
        {
            Health aitarget = hit.transform.GetComponent<Health>();
            if (aitarget != null)
            {
                aitarget.TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(gunObject.transform.position, transform.position + transform.forward * range);
    }
}
