using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2HealthBar : MonoBehaviour
{
    [SerializeField] float ID;
    [SerializeField] GameObject self;
    [SerializeField] P2Controller controller;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (controller.P2Health < ID * 10)
        {
            self.SetActive(false);
        } else
        {
            self.SetActive(true);
        }
    }
}
