using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jaccard {

    public static double Calc(HashSet<string> hs_i, HashSet<string> hs_u, HashSet<string> hs2)
    {
        /*
        Debug.Log("hs_i");
        foreach (var n in hs_i)
        {
            Debug.Log(n);
        }
 
        Debug.Log("hs2");
        foreach (var n in hs2)
        {
            Debug.Log(n);
        }
        */

        /*
        Debug.Log("hs_u");
        foreach (var n in hs_u)
        {
            Debug.Log(n);
        }

        Debug.Log("hs2");
        foreach (var n in hs2)
        {
            Debug.Log(n);
        }
        */


        hs_i.IntersectWith(hs2);

        //Debug.Log("-----------------------------------------------");
        //Debug.Log("Intersect--------------------------------------");
        foreach (var n in hs_i)
        {
            //Debug.Log(n);
        }

        hs_u.UnionWith(hs2);


        //Debug.Log("Union------------------------------------------");
        foreach (var n in hs_u)
        {
            //Debug.Log(n);
        }

        //Debug.Log("-----------------------------------------------");
        //hs3.UnionWith(hs2);


        /*
        Debug.Log("hs3");
        foreach (var n in hs3)
        {
            Debug.Log(n);
        }
        */




        return ((double)hs_i.Count / (double)hs_u.Count);

     
    }

    public static double Calc(List<string> ls1, List<string> ls2)
    {
        HashSet<string> hs_i = new HashSet<string>(ls1);
        HashSet<string> hs_u = new HashSet<string>(ls1);
        HashSet<string> hs2 = new HashSet<string>(ls2);
        return Calc(hs_i, hs_u, hs2);
    }
    

}
