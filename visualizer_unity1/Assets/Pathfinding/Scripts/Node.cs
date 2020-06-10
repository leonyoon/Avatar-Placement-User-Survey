using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Node
{
    public int x            = 0;
    public int y            = 0;
    public int s            = 0;

    public float yCoord     = 0;
    public float xCoord     = 0;
    public float zCoord     = 0;
    public int ID           = 0;
    public bool walkable    = true;
    public Node parent      = null;

    public int F            = 0;
    public int H            = 0;
    public int G            = 0;
  
    public float[] feature1 = null;
    public float[] feature2 = null;
    public float[] feature3 = null;
    public float[,,] similarity = null;

    //Use for faster look ups
    public int sortedIndex = -1;
    //public Node(int indexX, int indexY, float height, int idValue, float xcoord, float zcoord, bool w, Node p = null)
    //public Node(int indexX, int indexY, int indexS, float height, int idValue, float xcoord, float zcoord, bool w, int sect, float[] feat, float[,,] sim, Node p = null)
    public Node(int indexX, int indexY, int indexS, float height, int idValue, float xcoord, float zcoord, bool w, float[] feat1 = null, float[] feat2 = null, float[] feat3 = null, float[,,] sim = null, Node p = null)
    {
        x = indexX;
        y = indexY;
        s = indexS;
        yCoord = height;
        ID = idValue;
        xCoord = xcoord;
        zCoord = zcoord;
        walkable = w;
        parent = p;
        F = 0;
        G = 0;
        H = 0;

        feature1 = feat1;
        feature2 = feat2;
        feature3 = feat3;
        similarity = sim;
    }
}

