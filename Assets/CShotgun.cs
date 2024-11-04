using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class CShotgun : MonoBehaviour
{

    [Tooltip("Bullet prefab. Not a multiplier.")][SerializeField] GameObject bullet;
    [Tooltip("Point where bullet spawns. Again. Not a multiplier.")][SerializeField] GameObject bulletSpawn;
    int ammo;
    [Tooltip("Magazine capacity. not a multiplier.")][SerializeField] int maxAmmo;
    [Tooltip("Time for reloads, in seconds (per shell)")][SerializeField] float reloadTime;
    float reloadTimeWithSlide;
    [Tooltip("text")][SerializeField] TMP_Text ammoText;
    [Tooltip("text 2: text harder")][SerializeField] TMP_Text reloadText;
    [Tooltip("fire rate, still not a multiplier")][SerializeField] float fireRate;
    [SerializeField] ParticleSystem MuzzleFlash;
    float nextTimeToFire;

    [SerializeField] int pellets = 5;
    [SerializeField] float spreadAngle = 15f;





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

        if (Input.GetButtonDown("CFire") && ammo > 0 && Time.time >= nextTimeToFire)
        {
            //Sound, Muzzleflash, etc
            //Instantiate(MuzzleFlash, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            //nextTimeToFire = Time.time + 1f / fireRate;
            //Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);

            //ammo -= 1;
            FireShotgun();
        }

        if (Input.GetButtonDown("CReload") && ammo < maxAmmo + 1)
        {
            StartCoroutine(Reload());
        }
        ammoText.SetText(ammo + "/" + maxAmmo);
    }

    void FireShotgun()
    {
        MuzzleFlash.Play();
        nextTimeToFire = Time.time + 1f / fireRate;

        for (int i = 0; i < pellets; i++)
        {
            float angle = Random.Range(-spreadAngle, spreadAngle);
            Quaternion pelletRotation = Quaternion.Euler(bulletSpawn.transform.rotation.eulerAngles + new Vector3(0, 0, angle));
            Instantiate(bullet, bulletSpawn.transform.position, pelletRotation);
            Debug.Log("pellet fired");
        }

        ammo--;
    }
    IEnumerator Reload()
    {
        reloadText.SetText("Reloading...");
        while (ammo < 5)
        {
            yield return new WaitForSeconds(reloadTime);
            ammo++;
        }
        reloadText.SetText("");
    }



    //Transform.LookAt
}
