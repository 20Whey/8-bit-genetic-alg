using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class kingState : MonoBehaviour
{
  
    public int[] Original, Original2;

    private string[] strings;
    public GameObject strOb;
    public double IndependantTemprament;
    // Start is called before the first frame update

        void Start()
        {
        Original = processLikes();
        Original2 = processLikes();
        }


        public int[] processLikes() {
        
        strings = strOb.GetComponent<globalTraits>().strings;
        var a = new int[strings.Length];

        for (var i = 0; i < strings.Length; i++)
        {
            var rand = new System.Random();
            a[i] = rand.Next(0, 2);
        }
        //Translate To Original Array


        return a;
    }
}


