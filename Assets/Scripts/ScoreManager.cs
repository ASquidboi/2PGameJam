using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int P1Score = 0;
    public int P2Score = 0;
    [SerializeField] TMP_Text P1;
    [SerializeField] TMP_Text P2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
	string P1String = P1Score.ToString();
	string P2String = P2Score.ToString();
    	//P1
	
	if (P1Score < 10)
	{
	    P1.SetText("0" + P1String);
	} else {
	    P1.SetText(P1String);
	}
	//P2
	if (P2Score < 10)
	{
	    P2.SetText("0" + P2String);
	} else {
	    P2.SetText(P2String);
	}
    }
}
