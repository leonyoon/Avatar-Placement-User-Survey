using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture_list {
    public int ID;
    public string Category = null;
    public Vector3 Position, Size;
    public float[] Attribute = null;

    public Furniture_list(int fur_id, string fur_category, Vector3 fur_pos, Vector3 fur_size, float[] fur_attr)
    {
        ID = fur_id;
        Category = fur_category;
        Position = fur_pos;
        Size = fur_size;
        Attribute = fur_attr;
    }
}
