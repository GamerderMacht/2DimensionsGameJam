using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    [Header("Gun Info")]
    public float damage = 50f;
    [SerializeField] float range = 25f;
    [SerializeField] float fireRate = 1f;
    [SerializeField] float nextTimeToFire = 0.5f;

    [SerializeField] AudioSource gunAudioSource;
    [SerializeField] ParticleSystem muzzleFlash;

    [Header("Reload Info")]
    [SerializeField] float reloadTime = 1f;
    [SerializeField] bool isReloading = false;

    [Header("Ammo Info")]
    [SerializeField] int currentAmmo = 6;
    [SerializeField] int maxClipAmmo = 6;
    [SerializeField] int reserveAmmo = 0;

    [Header("Ammo UI")]
    [SerializeField] TextMeshProUGUI ammoText;


    [Header("Gun Reference")]
    public GameObject gunObject;
    public WeaponPickup weaponPickup;

    //[Header("Shooting Check")]
    //public MenuInterface menuInterface;

    private void Start()
    {
        currentAmmo = maxClipAmmo;
        UpdateAmmoText();
    }

    void Update()
    {
        //UpdateAmmoText();

        /*if (menuInterface.isPaused)
        {
            return;
        }*/

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && currentAmmo > 0 && !isReloading)
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
            /*AI_Health aitarget = hit.transform.GetComponent<AI_Health>();
            if (aitarget != null)
            {
                aitarget.TakingDamage(damage);
            }*/
        }
    }

    public void UpdateAmmoText()
    {
        //ammoText.text = $"{currentAmmo} | {reserveAmmo}";
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(gunObject.transform.position, transform.position + transform.forward * range);
    }
}
