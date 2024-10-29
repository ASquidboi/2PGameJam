using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;
    [SerializeField] bool P1;
    [SerializeField] bool P2;
    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (P1 == true && Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        } 
        else if (P1 == true && Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2) 
        {
            selectedWeapon = 1;
        } 
        else if (P1 == true && Input.GetKey(KeyCode.Alpha3))
        {
            selectedWeapon = 2;
        }
        else if (P2 == true && Input.GetKeyDown(KeyCode.Alpha0))
        {
            selectedWeapon = 0;
        }
        else if (P2 == true && Input.GetKeyDown(KeyCode.Alpha9))
        {
            selectedWeapon = 1;
        }
        else if (P2 == true && Input.GetKey(KeyCode.Alpha8))
        {
            selectedWeapon = 2;
        }
        SelectWeapon();
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
            
                weapon.gameObject.SetActive(false);  
            i++;
        }
    }

}
