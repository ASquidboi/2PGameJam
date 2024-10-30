using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class P2Gun : MonoBehaviour
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
    [SerializeField] GameObject MuzzleFlash;
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
        // Initialize stuff
        ammo = maxAmmo;
        reloadTimeWithSlide += 1;
	    reloadText.SetText("");
    }

    // Update is called once per frame
    void Update()
    {
        
        

        if (Input.GetButtonDown("P2Fire") && ammo > 0 && Time.time >= nextTimeToFire)
        {
            //Sound, Muzzleflash, etc
            Instantiate(MuzzleFlash, bulletSpawn.transform.position, bulletSpawn.transform.rotation);

            // Firerate stuff
            nextTimeToFire = Time.time + 1f / fireRate;

            // Get random angle for spread and apply
            float angle = Random.Range(-spreadAngle, spreadAngle);
            Quaternion pelletRotation = Quaternion.Euler(bulletSpawn.transform.rotation.eulerAngles + new Vector3(0, 0, angle));
            Instantiate(bullet, bulletSpawn.transform.position, pelletRotation);

            ammo -= 1;
        }

        if (Input.GetButtonDown("P2Reload") && ammo < maxAmmo + 1)
        {
            StartCoroutine(Reload());
        }

        ammoText.SetText(ammo + "/" + maxAmmo); //Consistently update ammo counter.
    }
    IEnumerator Reload()
    {
        if (ammo == 0)
        { 
            //play animation, slide          
            reloadText.SetText("Reloading...");
            yield return new WaitForSeconds(reloadTime + 1);
            ammo = maxAmmo;
            Debug.Log(ammo);
            reloadText.SetText("");
        }
        else if (ammo > 0)
        {

            //play animation, no slide          
            reloadText.SetText("Reloading...");
            yield return new WaitForSeconds(reloadTime);
            ammo = maxAmmo + 1;
            Debug.Log(ammo);
            reloadText.SetText("");
        }
    }
    


    //Transform.LookAt
}
