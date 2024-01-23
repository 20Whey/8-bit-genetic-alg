using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class globalTraits : MonoBehaviour
{

    public string[] strings = {"Red Faction Support", "Blue Faction Support", "Green Faction Support",
    "Purple Faction Support", "Yellow Faction Support", "Mauve Faction Support", "Chrome Faction Support", "Turqoise Faction Support"};

    public float proportion;
    // Start is called before the first frame update
    public GameObject king;
    public GameObject platform;
    public List<GameObject> st;
    void Start()
    {
        System.Random rand = new System.Random();
        //Apply starting values
        st = platform.GetComponent<create>().lis;
        /*
       foreach(var item in st)
        {
            item.GetComponent<peasantInit>().illwill = rand.Next(0, 2) * 0.2;
        }
        */
    }

   

}
