using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class create : MonoBehaviour
{
    public GameObject Pea;
    public int sPopSize;
    public List<GameObject> lis;
    public float rand()
    {
        System.Random ran = new System.Random();
        return Convert.ToSingle(ran.Next(-18,24));
    }
        // Start is called before the first frame update
        void Start()
        {
            for (var i = 0; i < sPopSize; i++)
            {
            GameObject nw = Instantiate(Pea, new Vector3(rand(), 1f, rand()), Quaternion.identity);
            nw.GetComponent<peasantInit>();
            nw.transform.SetParent(this.transform);
            lis.Add(nw);
            }
        }   

        // Update is called once per frame
        void Update()
        {

        }

   
}