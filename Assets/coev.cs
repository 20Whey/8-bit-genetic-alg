using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;
using System.Linq;

public class coev : MonoBehaviour
{
    public GameObject Peasant;
    public GameObject bsOB;
    public List<GameObject> lis;
    public GameObject mediator;
    public bool rolette;
    private Random random;
    public float proportion, other;
    public int[] Original;
    public List<GameObject> roulWheel;
    public List<int[][]> unSortedRest;
    public List<GameObject> ElitesGms;
    public List<int[]> Elites;
    public Dictionary<GameObject, double> mx;
    public List<GameObject> sortedL;
    public List<GameObject> tmp;
    public List<int[]> theRest;
    private List<int[]> realUnsorted;
    private int[][] realSorted;


    void Awake()
    {
        //a little bit of initialisation 
        proportion = gameObject.GetComponent<globalTraits>().proportion;
        other = 1.0f - proportion;
        // Elites = GameObject.Find("Elite Holder").GetComponent<containEliteValues>().Elites;
    }

    void Update()
    {
        Original = mediator.GetComponent<kingState>().Original;


        


        /* var tempGamer1 = RouletteSelect(lis);
         Debug.Log(string.Join(",", tempGamer1[0]));
         Debug.Log(string.Join(",", tempGamer1[1]));
         Debug.Log(string.Join(",", returnMutatedChild(tempGamer1, 4)));
        */


        if (Input.GetKeyDown(KeyCode.R))
        {
            lis = bsOB.GetComponent<create>().lis;
            mx = new Dictionary<GameObject, double>();

            for (var i = 0; i < lis.Count; i++)
            {
                //Create Pairs for Wieghted Ordered Roulette Wheel
                var tmp = createPairsToSort(lis[i], returnIndividualFitnessValue(Original, getDoub(lis[i])));
                mx.Add((GameObject)tmp[0], (double)tmp[1]);
            }
            //Use Linq to sort the list properly by fitness and then process into binary arrays
            var sortedDict = mx.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
            sortedL = sortedDict.Keys.ToList();
            //Select Our Elites
            //create list of gameObjects and from there get their binary arrays
            ElitesGms = EliteSelect(sortedL, 10);
            Elites = process(ElitesGms);

            //get 45 objects from our weighted Wheel - Stoachistic 
            theRest = new List<int[]>();
            var tmap = RouletteSelect(sortedL, 45);
            realUnsorted = new List<int[]>() ;
            for (var i = 0; i < tmap.Count; i++)
            {
                realUnsorted.Add(getDoub(tmap[i]));
                var brh = returnMutatedChildPair(realUnsorted[i], realUnsorted[ new System.Random().Next(0, realUnsorted.Count-1)], 4);
                theRest.Add(brh);
                theRest.Add(brh);
            }
            for(var i = 0; i < Elites.Count; i++)
            {
                theRest.Add(Elites[i]);
            }
            Debug.Log(theRest.Count);

            


            for(var i = 0; i < theRest.Count; i++) {
                
                GameObject nw = Instantiate(Peasant, new Vector3(rand(), 1f, rand()), Quaternion.identity);
                var scr = nw.GetComponent<peasantInit>();
                scr.parentHad += 1;
               
                for (var j = 0; j < 8; j++)
                {
                    scr.illWill[j] = theRest[i][j];
                    nw.transform.SetParent(bsOB.transform);
                }

                scr.fitness = returnIndividualFitnessValue(Original, theRest[i]);
                Destroy(bsOB.transform.GetChild(i).gameObject);
                lis.RemoveAt(i);
                lis.Insert(i, nw);
            }
           





            /*
            List<int[]> twoD = new List<int[]>();
          var ex = returnMutatedChildPair(realUnsorted[i][0], realUnsorted[i][1], 4);

            returnMutatedChildPair(realUnsorted[i], 4);*/
            /*returnMutatedChildPair(realUnsorted[i], 4);
        int[][] twoD = returnMutatedChildPair(realUnsorted[i], 4);
        // twoD[0] =

        Debug.Log(twoD);

        */

            //  Debug.Log(string.Join(",", realUnsorted[i][0]));

            //var tmp = returnMutatedChildPair(realUnsorted[i][0], realUnsorted[i][1], 4);
            //realUnsorted[i] = tmp;

        }
    }


    public float rand()
    {
        System.Random ran = new System.Random();
        return Convert.ToSingle(ran.Next(-18, 24));
    }


    public List<int[]> process(List<GameObject> input)
        {
            var tmp = new List<int[]>();
            for (var i = 0; i < input.Count; i++)
            {
                tmp.Add(getDoub(input[i]));
            }
            return tmp;
        }
        public object[] createPairsToSort(GameObject element, double finess)
        {
            var tmp = new object[] { element, finess };
            element.GetComponent<peasantInit>().fitness = finess;
            return tmp;
        }

        public int[] returnMutatedChildPair(int[] array, int[] array2, int breakPoint)
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


            int[] concatanateNSwitch(int[] array, int[] array2)
            {
                var rndOpOnePoint = array.Take(breakPoint).ToArray();
                var rndOpOnePointTwo = array2.Skip(breakPoint).ToArray();

                int[] result = rndOpOnePoint.Concat(rndOpOnePointTwo).ToArray();
                if (produceRandDub() > 0.7)
                {
                    var rndIndex = new System.Random().Next(0, result.Length);
                    result[rndIndex] = Mutate(result[rndIndex]);
                }
                return result;
            }

            //Perform CrossOver
            var resultOne = concatanateNSwitch(array, array2);
            //var resultTwo = concatanateNSwitch(array2, array);


            return resultOne;
        }
        //returnIndividualFitnessValue(Original,RouletteSelect(lis)[1]);
        public double returnIndividualFitnessValue(int[] Original, int[] Contender)
        {
            double score = 0;

            for (var i = 0; i < Contender.Length; i++)
            {
                if (Contender[i] == Original[i])
                {
                    score += 0.125;
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
    int[] produceRandomS(List<int[]> sb)
    {
        random = new System.Random();
        return sb[random.Next(0, sb.Count)];
    }
    double produceRandDub()
        {
            random = new System.Random();
            return random.NextDouble();
        }

        List<GameObject> EliteSelect(List<GameObject> input, int numberOfCycles)
        {
            var tmp = new List<GameObject>();
            for (var i = 0; i < numberOfCycles; i++)
            {
                tmp.Add(input[input.Count - 1 - i]);
            }
            return tmp;
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

            var ft = fillRoulWheel(input);
            tmp = new List<GameObject>();
            for (var i = 0; i < numberOfCycles; i++)
            {
                var rand = new System.Random().Next(0, ft.Count);

                tmp.Add(ft[rand]);

            }
            return tmp;
        }
    /*
       public int[] toCombine(List<GameObject> roulWheel, GameObject element)
        {
            var gm1 = element;
            var gm2 = produceRandom(roulWheel);

            while (gm2 == gm1)
            {
                gm2 = produceRandom(roulWheel);
            }

            // Turn them into arrays
            var egm1 = getDoub(gm1);
            var egm2 = getDoub(gm2);

            int[][] currGrp = new int[2][];
            currGrp[0] = egm1;
            currGrp[1] = egm2;
            return currGrp;
        }*/
}





