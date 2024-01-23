using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class peasantInit : MonoBehaviour
{

    public GameObject holder;
    public GameObject strOb;
    private globalTraits globalTraits;
    public string[] trait;
    public int[] illwill;
    public int parentHad;
    public double fitness;

    public int parentsHad
    {
        get { return parentHad; }
        set { parentHad = value; }
    }
    public string[] Traits
    {
        get { return trait; }
        set { trait = value; }
    }
    public int[] illWill
    {
        get { return illwill; }
        set { illwill = value; }
    }



    // Start is called before the first frame update
    void Awake()
    {
        System.Random rand = new System.Random();
        globalTraits = strOb.GetComponent<globalTraits>();
        trait = new string[globalTraits.strings.Length];
        illwill = new int[trait.Length];
       
            for (var i = 0; i < globalTraits.strings.Length; i++)
            {
                 trait[i] = globalTraits.strings[i]; 
            if (parentsHad < 1)
              {
                illwill[i] = rand.Next(0, 2);
            }
        }
       

        //impliment later
        /*switch(trait)
        {
            case "Red Faction":

            break;
            case "Blue Faction":

            break;
            case "Green Faction":

            break;
            case "Purple Faction":

            break;
            case "Yellow Faction":

            break;

        }*/


    }

    // Update is called once per frame

}
