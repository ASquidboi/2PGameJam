using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class P1Gun : MonoBehaviour
{
    
    [Tooltip("Bullet prefab. Not a multiplier.")][SerializeField] GameObject bullet;
    [Tooltip("Point where bullet spawns. Again. Not a multiplier.")][SerializeField] GameObject bulletSpawn;
    int ammo;
    [Tooltip("Magazine capacity. not a multiplier.")][SerializeField] int maxAmmo;
    [Tooltip("Time for reloads, in seconds.")][SerializeField] float reloadTime;
    float reloadTimeWithSlide;
    [Tooltip("text")][SerializeField] TMP_Text text;
    [Tooltip("text 2: text harder")][SerializeField] TMP_Text text2;
    [Tooltip("fire rate, still not a multiplier")][SerializeField] float fireRate;
    [SerializeField] GameObject MuzzleFlash;
    float nextTimeToFire;





    // Start is called before the first frame update
    void Start()
    {
        ammo = maxAmmo;
        reloadTimeWithSlide += 1;
	text2.SetText("");
    }

    // Update is called once per frame
    void Update()
    {
        
        //Vector2 rotation_direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //float angle = Mathf.Atan2(rotation_direction.y, rotation_direction.x) * Mathf.Rad2Deg;

     

        //Quaternion desiredRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, 0.025f);

        if (Input.GetButtonDown("P1Fire") && ammo > 0 && Time.time >= nextTimeToFire)
        {
            //Sound, Muzzleflash, etc
            Instantiate(MuzzleFlash, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            nextTimeToFire = Time.time + 1f / fireRate;
            Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            
            ammo -= 1;
        }

        if (Input.GetButtonDown("P1Reload") && ammo < maxAmmo + 1)
        {
            StartCoroutine(Reload());
        }
        text.SetText(ammo + "/" + maxAmmo);
    }
    IEnumerator Reload()
    {
        if (ammo == 0)
        {
            //waitcode
            //play animation, slide          
            text2.SetText("Reloading...");
            yield return new WaitForSeconds(reloadTime + 1);
            ammo = maxAmmo;
            Debug.Log(ammo);
            text2.SetText("");
        }
        else if (ammo > 0)
        {

            //waitcode
            //play animation, no slide          
            text2.SetText("Reloading...");
            yield return new WaitForSeconds(reloadTime);
            ammo = maxAmmo + 1;
            Debug.Log(ammo);
            text2.SetText("");
        }
    }
    


    //Transform.LookAt
}
