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
    [SerializeField] int currentAmmo = 30;
    [SerializeField] int maxClipAmmo = 30;
    [SerializeField] int reserveAmmo = 90;

    [Header("Ammo UI")]
    [SerializeField] TextMeshProUGUI ammoText;


    [Header("Gun Reference")]
    public GameObject gunObject;

    //[Header("Shooting Check")]
    //public MenuInterface menuInterface;

    private void Start()
    {
        currentAmmo = maxClipAmmo;
        UpdateAmmoText();
    }

    void Update()
    {
        UpdateAmmoText();

        /*if (menuInterface.isPaused)
        {
            return;
        }*/

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (reserveAmmo <= 0)
            {
                return;
            }
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && currentAmmo > 0 && !isReloading)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        currentAmmo--;
        muzzleFlash.Play();
        gunAudioSource.Play();
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

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        int remainder = maxClipAmmo - currentAmmo;
        int reloadAmount = 0;
        if (currentAmmo - remainder >= 0)
        {
            reloadAmount = remainder;
        }
        else if (reserveAmmo >= maxClipAmmo)
        {
            reloadAmount = maxClipAmmo;
        }
        else
        {
            reloadAmount = reserveAmmo;
        }
        currentAmmo += reloadAmount;
        reserveAmmo -= reloadAmount;
        isReloading = false;
    }

    public void AddAmmo(int ammoAmount)
    {
        currentAmmo += ammoAmount;

        if (currentAmmo > maxClipAmmo)
        {
            currentAmmo = maxClipAmmo;
        }
    }

    public void UpdateAmmoText()
    {
        ammoText.text = $"{currentAmmo} | {reserveAmmo}";
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(gunObject.transform.position, transform.position + transform.forward * range);
    }
}
