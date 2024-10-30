using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class P1Auto : MonoBehaviour
{

    // GameObjects
    [Tooltip("Bullet prefab.")]
    [SerializeField] GameObject bullet;

    [Tooltip("Point where bullet spawns. Obviously.")]
    [SerializeField] GameObject bulletSpawn;

    [Tooltip("Ammo display.")]
    [SerializeField] TMP_Text ammoText;

    [Tooltip("Text that appears when reloading.")]
    [SerializeField] TMP_Text reloadText;

    [Tooltip("Muzzle flash prefab.")]
    [SerializeField] ParticleSystem MuzzleFlash;
    // Values

    [Tooltip("Magazine capacity.")]
    [SerializeField] int maxAmmo;

    [Tooltip("Time for reloads, in seconds.")]
    [SerializeField] float reloadTime;

    [Tooltip("Fire rate.")]
    [SerializeField] float fireRate;

    [Tooltip("The spread angle for bullets being instantiated. (Is multiplied by 2)")]
    [SerializeField] float spreadAngle = 10f;

    // Internal vars
    int ammo;
    float reloadTimeWithSlide;
    float nextTimeToFire;





    // Start is called before the first frame update
    void Start()
    {
        ammo = maxAmmo;
        reloadTimeWithSlide += 1;
        reloadText.SetText("");
    }

    // Update is called once per frame
    void Update()
    {

        //Vector2 rotation_direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //float angle = Mathf.Atan2(rotation_direction.y, rotation_direction.x) * Mathf.Rad2Deg;



        //Quaternion desiredRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, 0.025f);

        if (Input.GetButton("P1Fire") && ammo > 0 && Time.time >= nextTimeToFire)
        {
            //Sound, Muzzleflash, etc
            MuzzleFlash.Play();
            nextTimeToFire = Time.time + 1f / fireRate;

            // Determine and apply bullet spread range
            float angle = Random.Range(-spreadAngle, spreadAngle);
            Quaternion pelletRotation = Quaternion.Euler(bulletSpawn.transform.rotation.eulerAngles + new Vector3(0, 0, angle));
            Instantiate(bullet, bulletSpawn.transform.position, pelletRotation);

            ammo -= 1;
        }

        if (Input.GetButtonDown("P1Reload") && ammo < maxAmmo + 1)
        {
            StartCoroutine(Reload());
        }
        ammoText.SetText(ammo + "/" + maxAmmo);
    }
    IEnumerator Reload()
    {
        if (ammo == 0)
        {
            //waitcode
            //play animation, slide          
            reloadText.SetText("Reloading...");
            yield return new WaitForSeconds(reloadTime + 1);
            ammo = maxAmmo;
            Debug.Log(ammo);
            reloadText.SetText("");
        }
        else if (ammo > 0)
        {

            //waitcode
            //play animation, no slide          
            reloadText.SetText("Reloading...");
            yield return new WaitForSeconds(reloadTime);
            ammo = maxAmmo + 1;
            Debug.Log(ammo);
            reloadText.SetText("");
        }
    }

}
