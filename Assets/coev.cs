using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;
using System.Linq;

public class coev : MonoBehaviour
{
    
    
    public GameObject bsOB;
    public List<GameObject> lis;
    public GameObject mediator;
    public bool rolette;
    private Random random;
    public float proportion, other;
    public int[] Original;
    public List<GameObject> roulWheel;
    public List<GameObject> Elites;
    public Dictionary<GameObject, double> mx;
    public List<GameObject> sortedL;
   
  
    void Awake()
    {
        //a little bit of initialisation 
        proportion = gameObject.GetComponent<globalTraits>().proportion;
        other = 1.0f - proportion;
        Elites = GameObject.Find("Elite Holder").GetComponent<containEliteValues>().Elites;
    }

    void Update()
    {
        Original = mediator.GetComponent<kingState>().Original;
        lis = bsOB.GetComponent<create>().lis;



     
        /* var tempGamer1 = RouletteSelect(lis);
         Debug.Log(string.Join(",", tempGamer1[0]));
         Debug.Log(string.Join(",", tempGamer1[1]));
         Debug.Log(string.Join(",", returnMutatedChild(tempGamer1, 4)));
        */


        if (Input.GetKeyDown(KeyCode.R))
        {
            mx = new Dictionary<GameObject, double>();
           
            for (var i = 0; i < lis.Count; i++)
            {
                //lis[i].GetComponent<peasantInit>().fitness = ; 
                //mx.Add(lis[i], returnIndividualFitnessValue(Original, getDoub(lis[i])));
               // var orderedDeck1 = orderedDeckC.Union(orderedDeckH).GroupBy(x => x.Key).ToDictionary(x => x.Key, x => x.Sum(k => k.Value));

            var tmp = createPairsToSort(lis[i], returnIndividualFitnessValue(Original, getDoub(lis[i])));
            mx.Add((GameObject)tmp[0],  (double)tmp[1]);


                //mx.OrderBy(element => element[1]);
                //var rol = RouletteSelect(mx);
            }
            var sortedDict = mx.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
            sortedL = sortedDict.Keys.ToList();
           
           Elites = RouletteSelect(sortedL, 8);
           
        }
    }


   //mx = lis.foreach()

              //  mx.Add(returnIndividualFitnessValue(Original, getDoub(lis[i])));
                //{
                // Elites.Add(lis[i]);
                //}
     
    public object[] createPairsToSort(GameObject element, double finess)
    {
        var tmp = new object[] { element, finess};
        element.GetComponent<peasantInit>().fitness = finess;
        return tmp;
    } 

    public int[] returnMutatedChild(int[][] randomPair, int breakPoint)
    {
        int Mutate(int element)
        {
            if (element > 0)
            {
                element--;
            }
            else
            {
                element++;
            }
            return element;
        }
        randomPair[0] = randomPair[0].Take(breakPoint).ToArray();
        randomPair[1] = randomPair[1].Skip(breakPoint).ToArray();
        int[] result = randomPair[0].Concat(randomPair[1]).ToArray();

        if (produceRandDub() > 0.7)
        {
            var rndIndex = new System.Random().Next(0, randomPair.Length);
            result[rndIndex] = Mutate(result[rndIndex]);
        }
        return result;
    }
    //returnIndividualFitnessValue(Original,RouletteSelect(lis)[1]);
    public double returnIndividualFitnessValue(int[] Original, int[] Contender)
    {
        double score = 0;

        for (var i = 0; i < Contender.Length; i++)
        {
            if (Contender[i] == Original[i])
            {
                score += 0.1;
            }
        }
        return score;
    }
    int[] getDoub(GameObject Subject)
    {
        int[] arr = Subject.GetComponent<peasantInit>().illWill;
        return arr;
    }

    GameObject produceRandom(List<GameObject> sb)
    {
        random = new System.Random();
        return sb[random.Next(0, sb.Count)];
    }
    double produceRandDub()
    {
        random = new System.Random();
        return random.NextDouble();
    }

    List<GameObject> RouletteSelect(List<GameObject> input, int numberOfCycles)
    {
        List<GameObject> fillRoulWheel(List<GameObject> baseList)
        {
            roulWheel = new List<GameObject>();
            for (var i = 0; i < baseList.Count; i++)
            {
                var j = returnIndividualFitnessValue(Original, getDoub(baseList[i])) * 10;
                for (var j2 = 0; j2 < j; j2++)
                {
                    roulWheel.Add(baseList[i]);
                }
            }
            return roulWheel;
        }

        var wheelOfFortune =  fillRoulWheel(input);
        var tmp = new List<GameObject>();
        for(var i = 0; i < numberOfCycles; i++)
        {
        var rand = new System.Random().Next(0, wheelOfFortune.Count);
            tmp.Add(wheelOfFortune[rand]);

        }
        return tmp;


    }






    int[][] Combine(List<GameObject> roulWheel)
    {

        var gm1 = produceRandom(roulWheel);
        var gm2 = produceRandom(roulWheel);
        while (gm2 == gm1)
        {
            gm2 = produceRandom(roulWheel);
        }
        //Turn them into strings
        var egm1 = getDoub(gm1);
        var egm2 = getDoub(gm2);
        int[][] currGrp = new int[][] { egm1, egm2 };
        return currGrp;
    }
}


  



