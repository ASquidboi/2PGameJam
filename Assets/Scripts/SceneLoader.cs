using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Switching scenes");
    }
    public void ToMapSelect()
    {
        SceneManager.LoadScene("MapSelect");
        Debug.Log("Switching scenes");
    }
    public void ToKeybinds()
    {
        SceneManager.LoadScene("Keybinds");
        Debug.Log("Switching scenes");
    }
    public void ToWarehouse()
    {
        SceneManager.LoadScene("dm_warehouse");
        Debug.Log("Switching scenes");
    }
    public void ToOffice()
    {
        SceneManager.LoadScene("Office");
        Debug.Log("Switching scenes");
    }
}
