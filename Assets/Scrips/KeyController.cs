using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{

    public static int count = 0;
    public string key_name;
    public GameObject jumper;
    public GameObject fiareEffect;
    void OnTriggerEnter()
    {
		GetComponent<Animation>().Play(key_name);
        count++;
        if(count == 6)
        {
            jumper.SetActive(true);
            fiareEffect.SetActive(true);
        }
	}
}
