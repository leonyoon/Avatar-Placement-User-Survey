using System.Collections.Generic;
using UnityEngine;

public class Furnitures : MonoBehaviour
{
    public int ID, sector_ID, cat_ID;
    public string Category = null;
    public Vector3 Position, Size;
    public float[] S1_fur_01_attr, S2_fur_01_attr;
    public Furniture_list[] Map1_Fur, Map2_Fur, Map3_Fur, Map4_Fur, Map5_Fur, Map6_Fur, Map7_Fur, Map8_Fur = null;
    public Furniture_list[] S1_Furnit_list, S2_Furnit_list = null;
    public float global_cat_sim, global_occup_sim;
    int num_sect, num_cat;
    int map1_size, map2_size, map3_size, map4_size, map5_size, map6_size, map7_size, map8_size;
    GameObject HumXT, HumXT_Sit, HumX;
    GameObject AvaXT, AvaXT_Sit, AvaX;
    GameObject S1_fur_01, S1_fur_02, S1_fur_03, S1_fur_04, S1_fur_05, S1_fur_06, S1_fur_07, S1_fur_08, S1_fur_09, S1_fur_10, S1_fur_11, S1_fur_12, S1_fur_13, S1_fur_14, S1_fur_15, S1_fur_16, S1_fur_17, S1_fur_18, S1_fur_19, S1_fur_20, S1_fur_21, S1_fur_22, S1_fur_23, S1_fur_24, S1_fur_25;
    GameObject S2_fur_01, S2_fur_02, S2_fur_03, S2_fur_04, S2_fur_05, S2_fur_06, S2_fur_07, S2_fur_08, S2_fur_09, S2_fur_10, S2_fur_11, S2_fur_12, S2_fur_13, S2_fur_14, S2_fur_15, S2_fur_16, S2_fur_17, S2_fur_18, S2_fur_19, S2_fur_20, S2_fur_21, S2_fur_22, S2_fur_23, S2_fur_24, S2_fur_25, S2_fur_26, S2_fur_27, S2_fur_28, S2_fur_29, S2_fur_30;
    GameObject S3_fur_01, S3_fur_02, S3_fur_03, S3_fur_04, S3_fur_05, S3_fur_06, S3_fur_07, S3_fur_08, S3_fur_09, S3_fur_10, S3_fur_11, S3_fur_12, S3_fur_13, S3_fur_14, S3_fur_15, S3_fur_16, S3_fur_17, S3_fur_18, S3_fur_19, S3_fur_20, S3_fur_21, S3_fur_22, S3_fur_23;
    GameObject S4_fur_01, S4_fur_02, S4_fur_03, S4_fur_04, S4_fur_05, S4_fur_06, S4_fur_07, S4_fur_08, S4_fur_09, S4_fur_10, S4_fur_11, S4_fur_12, S4_fur_13, S4_fur_14, S4_fur_15, S4_fur_16;
    GameObject S5_fur_01, S5_fur_02, S5_fur_03, S5_fur_04, S5_fur_05, S5_fur_06, S5_fur_07, S5_fur_08, S5_fur_09, S5_fur_10, S5_fur_11, S5_fur_12, S5_fur_13, S5_fur_14, S5_fur_15, S5_fur_16, S5_fur_17, S5_fur_18, S5_fur_19, S5_fur_20, S5_fur_21, S5_fur_22, S5_fur_23, S5_fur_24, S5_fur_25;
    GameObject S6_fur_01, S6_fur_02, S6_fur_03, S6_fur_04, S6_fur_05, S6_fur_06, S6_fur_07, S6_fur_08, S6_fur_09, S6_fur_10, S6_fur_11, S6_fur_12, S6_fur_13, S6_fur_14, S6_fur_15, S6_fur_16, S6_fur_17, S6_fur_18, S6_fur_19, S6_fur_20, S6_fur_21, S6_fur_22, S6_fur_23, S6_fur_24, S6_fur_25;
    GameObject S7_fur_01, S7_fur_02, S7_fur_03, S7_fur_04, S7_fur_05, S7_fur_06, S7_fur_07, S7_fur_08, S7_fur_09, S7_fur_10, S7_fur_11, S7_fur_12, S7_fur_13, S7_fur_14, S7_fur_15, S7_fur_16, S7_fur_17, S7_fur_18, S7_fur_19, S7_fur_20, S7_fur_21;
    GameObject S8_fur_01, S8_fur_02, S8_fur_03, S8_fur_04, S8_fur_05, S8_fur_06, S8_fur_07, S8_fur_08, S8_fur_09, S8_fur_10, S8_fur_11, S8_fur_12, S8_fur_13, S8_fur_14, S8_fur_15, S8_fur_16;
    Experiment exp_script;
    int[] S1_cat_freq, S2_cat_freq;

    float Hum_arrow_x, Hum_arrow_z, Ava_arrow_x, Ava_arrow_z;
    Vector3 Hum_front, Ava_front, vec_dist;
    float each_cat_sim, total_cat_sim, cat_dist_sim;
    int intersection_count, union_count;
    Vector3 ray_vis, ray_start, ray_dir_down;
    int[] free_Ava, free_Hum;

    List<int> S1_fur_ID_VA, S2_fur_ID_VA;
    List<string> S1_fur_cat_VA, S2_fur_cat_VA;
    List<float> S1_fur_dist_VA, S2_fur_dist_VA;
    List<string> S1_fur_list, S2_fur_list;

    GameObject table_mc;

    //s3 - 20 , s4 - 13 , s5 - 19 , s6 - 18 , s7 - 18 ,  s8 - 13
    // Use this for initialization
    void Start()
    {
        num_sect = 12; num_cat = 12;
        map1_size = 25; map2_size = 30-7; map3_size = 23; map4_size = 16; map5_size = 25; map6_size = 25; map7_size = 21; map8_size = 16;

        free_Ava = new int[48];
        free_Hum = new int[48];
        ray_vis = new Vector3(0f, 0f, 0f);
        ray_start = new Vector3(0f, 0f, 0f);
        ray_dir_down = new Vector3(0f, 0f, 0f);
        S1_fur_list = new List<string>();
        S2_fur_list = new List<string>();

        S1_fur_ID_VA = new List<int>();
        S2_fur_ID_VA = new List<int>();

        S1_fur_cat_VA = new List<string>();
        S2_fur_cat_VA = new List<string>();

        S1_fur_dist_VA = new List<float>();
        S2_fur_dist_VA = new List<float>();

        S1_cat_freq = new int[num_cat];
        S2_cat_freq = new int[num_cat];

        exp_script = GameObject.Find("Scene").GetComponent<Experiment>();
        //Debug.Log("Pair_number : " + exp_script.numPair);
        
        //HumX
        HumXT = GameObject.Find("/Characters/For_Test/HumXT");
        HumXT_Sit = GameObject.Find("/Characters/For_Test/HumXT_Sit");

        //AvaX
        AvaXT = GameObject.Find("/Characters/For_Test/AvaXT");
        AvaXT_Sit = GameObject.Find("/Characters/For_Test/AvaXT_Sit");





        //Set up for S1
        Map1_Fur = new Furniture_list[map1_size];
        Map2_Fur = new Furniture_list[map2_size];
        Map3_Fur = new Furniture_list[map3_size];
        Map4_Fur = new Furniture_list[map4_size];
        Map5_Fur = new Furniture_list[map5_size];
        Map6_Fur = new Furniture_list[map6_size];
        Map7_Fur = new Furniture_list[map7_size];
        Map8_Fur = new Furniture_list[map8_size];

        //Attributes not used so far
        S1_fur_01_attr = new float[1];
        S2_fur_01_attr = new float[1];
        S1_fur_01_attr[0] = 1;
        S2_fur_01_attr[0] = 1;

        //All furnitures set up
        All_furniture_list();

    }

    // Update is called once per frame
    void Update()
    {
        RecallHumX();
        RecallAvaX();
        PairToFurnitureMap(exp_script.numPair);
        //Hum_to_fur_line();
        //Hum_to_fur_line_VA();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (HumX != null)
            {
                for (int i = 0; i < S1_Furnit_list.Length; i++)
                {
                    float Hum_arrow_x = Mathf.Cos((90f - HumX.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + HumX.transform.position.x;
                    float Hum_arrow_z = Mathf.Sin((90f - HumX.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + HumX.transform.position.z;
                    Vector3 Hum_front = new Vector3(Hum_arrow_x - HumX.transform.position.x, 0f, Hum_arrow_z - HumX.transform.position.z);
                    Vector3 vec_dist;
                    vec_dist = new Vector3(HumX.transform.position.x - S1_Furnit_list[i].Position.x, 0f, HumX.transform.position.z - S1_Furnit_list[i].Position.z);
                    float temp_ang = (SignedAngle(Hum_front, new Vector3(vec_dist.x, 0f, vec_dist.z), Vector3.up) + 180f);
                    if ((vec_dist.magnitude > 0.25f && vec_dist.magnitude < 4f) && (temp_ang > 340f || temp_ang < 20f))
                    {
                        S1_fur_ID_VA.Add(S1_Furnit_list[i].ID);
                        S1_fur_cat_VA.Add(S1_Furnit_list[i].Category);
                        S1_fur_dist_VA.Add(vec_dist.magnitude);
                        //Debug.DrawLine(HumX.transform.position, S1_Furnit_list[i].Position, Color.green);
                    }
                }
            }
            if (AvaX != null)
            {
                for (int i = 0; i < S2_Furnit_list.Length; i++)
                {
                    float Ava_arrow_x = Mathf.Cos((90f - AvaX.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + AvaX.transform.position.x;
                    float Ava_arrow_z = Mathf.Sin((90f - AvaX.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + AvaX.transform.position.z;
                    Vector3 Ava_front = new Vector3(Ava_arrow_x - AvaX.transform.position.x, 0f, Ava_arrow_z - AvaX.transform.position.z);
                    Vector3 vec_dist;
                    vec_dist = new Vector3(AvaX.transform.position.x - S2_Furnit_list[i].Position.x, 0f, AvaX.transform.position.z - S2_Furnit_list[i].Position.z);
                    float temp_ang = (SignedAngle(Ava_front, new Vector3(vec_dist.x, 0f, vec_dist.z), Vector3.up) + 180f);
                    if ((vec_dist.magnitude > 0.25f && vec_dist.magnitude < 4f) && (temp_ang > 340f || temp_ang < 20f))
                    {
                        S2_fur_ID_VA.Add(S2_Furnit_list[i].ID);
                        S2_fur_cat_VA.Add(S2_Furnit_list[i].Category);
                        S2_fur_dist_VA.Add(vec_dist.magnitude);
                        //Debug.DrawLine(AvaX.transform.position, S2_Furnit_list[i].Position, Color.green);
                    }
                }
            }
            Debug.Log("S1_fur : " + S1_fur_ID_VA.Count);
            Debug.Log("-------------------------------------------");
            for (int i = 0; i <S1_fur_ID_VA.Count;i++)
            {
                
                Debug.Log(S1_fur_cat_VA[i]);
                Debug.Log(S1_fur_dist_VA[i]);
            }
            Debug.Log("-------------------------------------------");
            Debug.Log("S2_fur : " + S2_fur_ID_VA.Count);
            Debug.Log("-------------------------------------------");
            for (int i = 0; i < S2_fur_ID_VA.Count; i++)
            {

                Debug.Log(S2_fur_cat_VA[i]);
                Debug.Log(S2_fur_dist_VA[i]);
            }
            Debug.Log("-------------------------------------------");
            float VA_dist = 0f;

            bool bothObject = true;
            bool leastMatched = false;
            if(S1_fur_cat_VA.Count == 0 || S2_fur_cat_VA.Count == 0)
            {
                bothObject = false;
            }


            while (S1_fur_cat_VA.Count >0 && S2_fur_cat_VA.Count > 0)
            {
                //Debug.Log("S1 Object Count :  " + S1_fur_cat_VA.Count + "-------"+ "S2 Object Count :  " + S2_fur_cat_VA.Count);
                bool isMatched = false;
                float VA_dist_single = 100000f;
                int s1_idx = -99;
                int s2_idx = -99;
                for (int j = 0; j < S1_fur_cat_VA.Count; j++)
                {
                    //Debug.Log("S1 Object Count :  " + j  + "-------");
                    for (int i = 0; i < S2_fur_cat_VA.Count; i++)
                    {
                        //Debug.Log("S2 Object Count (current) :  " + i + "-------");
                        //Debug.Log("S2 Object Count (total) :  " + S2_fur_ID_VA.Count + "-------");
                        if (S1_fur_cat_VA[j] == S2_fur_cat_VA[i])
                        {
                            Debug.Log("Debug Point");
                            isMatched = true;
                            float temp_dist = Mathf.Abs(S2_fur_dist_VA[i] - S1_fur_dist_VA[j]);


                            if (temp_dist < VA_dist_single)
                            {
                                VA_dist_single = temp_dist;
                                s2_idx = i;
                                s1_idx = j;
                                Debug.Log("Matched cndidate------------------------------------");
                                Debug.Log("VA_s1_idx : " + s1_idx);
                                Debug.Log("VA_s2_idx : " + s2_idx);
                                Debug.Log("VA_s1_cat : " + S1_fur_cat_VA[j]);
                                Debug.Log("VA_s2_cat : " + S2_fur_cat_VA[i]);
                                Debug.Log("VA_dist_single : " + VA_dist_single);
                                Debug.Log("-------------------------------------------");
                            }
                        }
                    }
                    if (isMatched == false)
                    {
                        Debug.Log("Not matched (removed) ------------------------------------");
                        Debug.Log("VA_s1_idx : " + j);
                        Debug.Log("VA_s1_cat : " + S1_fur_cat_VA[j]);
                        S1_fur_cat_VA.RemoveAt(j);
                        S1_fur_dist_VA.RemoveAt(j);
                    }
                }

                if (isMatched == true)
                {
                    leastMatched = true;
                    Debug.Log("Matched ------------------------------------");
                    Debug.Log("VA_s1_idx : " + s1_idx);
                    Debug.Log("VA_s2_idx : " + s2_idx);
                    Debug.Log("VA_s1_cat : " + S1_fur_cat_VA[s1_idx]);
                    Debug.Log("VA_s2_cat : " + S2_fur_cat_VA[s2_idx]);
                    S1_fur_cat_VA.RemoveAt(s1_idx);
                    S1_fur_dist_VA.RemoveAt(s1_idx);
                    S2_fur_cat_VA.RemoveAt(s2_idx);
                    S2_fur_dist_VA.RemoveAt(s2_idx);
                    VA_dist = VA_dist + VA_dist_single;
                }
                
            }
            float VA_sim = 1f - (VA_dist / 5f);
            if (bothObject == false || leastMatched == false)
            {
                VA_sim = 0f;
            }
            Debug.Log("Visual attention distance : " + VA_dist);
            Debug.Log("Visual attention similarity : " + VA_sim);
            Debug.Log("-------------------------------------------");
            S1_fur_ID_VA.Clear();
            S2_fur_ID_VA.Clear();

            S1_fur_cat_VA.Clear();
            S2_fur_cat_VA.Clear();

            S1_fur_dist_VA.Clear();
            S2_fur_dist_VA.Clear();
        }
        /*
        if (Input.GetKeyDown(KeyCode.W))
        {
            RaycastHit hit;
            Vector3 ray_start = new Vector3(0f, 0f, 0f);
            Vector3 ray_dir_down = new Vector3(0f, 0f, 0f);
            float angAva = AvaX.transform.eulerAngles.y;

            ray_dir_down.Set(0f, -1f, 0f);
            for(int i = 0; i<4; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    for (int k = 1; k < 4; k++)
                    {
                        for (int l = 1; l < 4; l++)
                        {
                            ray_start.Set(AvaX.transform.position.x + (0.25f * i + 0.05f * l) * Mathf.Cos(Mathf.PI / 180f * (30.0F * j + 6.0f * k - angAva + 90f)), 2.25f, AvaX.transform.position.z + (0.25f * i + 0.05f * l) * Mathf.Sin(Mathf.PI / 180f * (30.0F * j + 6.0f * k - angAva + 90f)));
                            //Random.Range((30.0F * (k - 1) - angAva + 90f) % 360f, (15.0F * k - angAva + 90f) % 360f)), 2.25f, AvaX.transform.position.z + 0.25f * (l - 1f + randsqrt) * Mathf.Sin(Mathf.PI / 180f * Random.Range((15.0F * (k - 1) - angAva + 90f) % 360f, (15.0F * k - angAva + 90f) % 360f)));
                            if (Physics.Raycast(ray_start, ray_dir_down, out hit, 2.0f))
                            {

                                Debug.DrawLine(ray_start, hit.point, Color.red);
                            }
                        }
                    }
                }
            }
           
        }
        */
        /*
        RaycastHit hit;
        Vector3 ray_vis = new Vector3(0f, 0f, 0f);
        Vector3 ray_start = new Vector3(0f, 0f, 0f);
        Vector3 ray_dir_down = new Vector3(0f, 0f, 0f);
        float angAva = AvaX.transform.eulerAngles.y;

        ray_dir_down.Set(0f, -1f, 0f);
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                for (int k = 1; k < 4; k++)
                {
                    for (int l = 1; l < 4; l++)
                    {
                        ray_start.Set(AvaX.transform.position.x + (0.5f * i + 0.1f * l) * Mathf.Cos(Mathf.PI / 180f * (30.0F * j + 6.0f * k - angAva + 90f)), 2.25f, AvaX.transform.position.z + (0.5f * i + 0.1f * l) * Mathf.Sin(Mathf.PI / 180f * (30.0F * j + 6.0f * k - angAva + 90f)));
                        //Random.Range((30.0F * (k - 1) - angAva + 90f) % 360f, (15.0F * k - angAva + 90f) % 360f)), 2.25f, AvaX.transform.position.z + 0.25f * (l - 1f + randsqrt) * Mathf.Sin(Mathf.PI / 180f * Random.Range((15.0F * (k - 1) - angAva + 90f) % 360f, (15.0F * k - angAva + 90f) % 360f)));
                        if (Physics.Raycast(ray_start, ray_dir_down, out hit, 3f))
                        {

                            if (hit.point.y <0.1)
                            {
                                ray_vis.Set(ray_start.x, 0.25f, ray_start.z);
                                Debug.DrawLine(ray_vis, hit.point, Color.blue);
                            }
                              
                            
                        }
                    }
                }
            }
        }
        */

        if (Input.GetKeyDown(KeyCode.Comma))
        {
            /*
            float Hum_arrow_x = Mathf.Cos((90f - HumX.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + HumX.transform.position.x;
            float Hum_arrow_z = Mathf.Sin((90f - HumX.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + HumX.transform.position.z;
            Vector3 Hum_front = new Vector3(Hum_arrow_x - HumX.transform.position.x, 0f, Hum_arrow_z - HumX.transform.position.z);

            float Ava_arrow_x = Mathf.Cos((90f - AvaX.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + AvaX.transform.position.x;
            float Ava_arrow_z = Mathf.Sin((90f - AvaX.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + AvaX.transform.position.z;
            Vector3 Ava_front = new Vector3(Ava_arrow_x - AvaX.transform.position.x, 0f, Ava_arrow_z - AvaX.transform.position.z);

            //Furnitures inside the boundary of circle - Loonking for a better name
            List<string> S1_fur_list = new List<string>();
            List<string> S2_fur_list = new List<string>();
            int[,] S1_occupancy = new int[num_sect, num_cat];
            int[,] S2_occupancy = new int[num_sect, num_cat];
            S1_cat_freq = new int[num_cat];
            S2_cat_freq = new int[num_cat];


            //S1 list of furniture that is inside of circle with radius 3
            //Debug.Log("-----------------------------------------------");
            for (int i = 0; i < S1_Furnit_list.Length; i++)
            {
                Vector3 vec_dist;
                vec_dist = new Vector3(HumX.transform.position.x - S1_Furnit_list[i].Position.x, 0f, HumX.transform.position.z - S1_Furnit_list[i].Position.z);
                if (vec_dist.magnitude < 3f)
                {
                    //Debug.Log("Map1_Fur[" + i + "].Category : " + Map1_Fur[i].Category); // + "                    Dist : " + vec_dist.magnitude);
                    S1_fur_list.Add(S1_Furnit_list[i].Category);
                    float temp_ang = (SignedAngle(Hum_front, new Vector3(vec_dist.x, 0f, vec_dist.z), Vector3.up) + 180f);
                    Occupancy_map(vec_dist.magnitude, temp_ang, S1_Furnit_list[i].Category);
                    S1_occupancy[sector_ID, cat_ID]++;
                }
                
            }

            //Debug.Log("-----------------------------------------------");
            for (int i = 0; i < S2_Furnit_list.Length; i++)
            {
                Vector3 vec_dist;
                vec_dist = new Vector3(AvaX.transform.position.x - S2_Furnit_list[i].Position.x, 0f, AvaX.transform.position.z - S2_Furnit_list[i].Position.z);
                if (vec_dist.magnitude < 3f)
                {
                    //Debug.Log("Map2_Fur[" + i + "].Category : " + Map2_Fur[i].Category + "                    Dist : " + vec_dist.magnitude);
                    S2_fur_list.Add(S2_Furnit_list[i].Category);
                    float temp_ang = (SignedAngle(Ava_front, new Vector3(vec_dist.x, 0f, vec_dist.z), Vector3.up) + 180f);
                    //Debug.Log("Map2_temp_ang : " + temp_ang);
                    //Set Sector ID and Category ID
                    Occupancy_map(vec_dist.magnitude, temp_ang, S2_Furnit_list[i].Category);
                    //2D array of sector and category distribution
                    S2_occupancy[sector_ID, cat_ID]++;
                }
            }

            foreach (var n in S2_fur_list)
            {

                S2_cat_freq[IndexOfCategory(n)]++;
            }
            //Debug.Log("Jaccard12: " + Jaccard.Calc(S1_fur_list, S2_fur_list));
            Jaccard.Calc(S1_fur_list, S2_fur_list);
            Debug.Log("-----------------------------------------------");
            float tmp_occup_sim = Occup_sim(S1_occupancy, S2_occupancy);
            */

        }

    }
    void All_furniture_list()
    {
        // 0 - sofa , 1 - table, 2 - chair , 3 - TV , 4 - air-condition , 5 - refriger , 6 - sink , 7 - lamp , 8 - piano , 9 - cabinet, 10 - shelf, 11 - window
        //S1_furniture_list
        S1_fur_01 = GameObject.Find("S1_Single_sofa_1");
        S1_fur_02 = GameObject.Find("S1_Large_sofa_1");
        S1_fur_03 = GameObject.Find("S1_Coffee_table_1");
        S1_fur_04 = GameObject.Find("S1_Side_table_1");
        S1_fur_05 = GameObject.Find("S1_Single_sofa_2");
        S1_fur_06 = GameObject.Find("S1_Air_conditioner_1");
        S1_fur_07 = GameObject.Find("S1_Single_sofa_3");
        S1_fur_08 = GameObject.Find("S1_TV");
        S1_fur_09 = GameObject.Find("S1_TV_table");
        S1_fur_10 = GameObject.Find("S1_Refrigerator");
        S1_fur_11 = GameObject.Find("S1_Dining_table");
        S1_fur_12 = GameObject.Find("S1_Kitchen_sink");
        S1_fur_13 = GameObject.Find("S1_Chair_1");
        S1_fur_14 = GameObject.Find("S1_Chair_2");
        S1_fur_15 = GameObject.Find("S1_Chair_3");
        S1_fur_16 = GameObject.Find("S1_Chair_4");

        S1_fur_17 = GameObject.Find("S1_Window_1");
        S1_fur_18 = GameObject.Find("S1_Window_2");
        S1_fur_19 = GameObject.Find("S1_Cabinet_1");
        S1_fur_20 = GameObject.Find("S1_Shelf_1");
        S1_fur_21 = GameObject.Find("S1_Cabinet_2");
        S1_fur_22 = GameObject.Find("S1_Cabinet_3");
        S1_fur_23 = GameObject.Find("S1_Cabinet_4");
        S1_fur_24 = GameObject.Find("S1_Window_3");
        S1_fur_25 = GameObject.Find("S1_Window_4");


        Map1_Fur[0] = new Furniture_list(1, "sofa", S1_fur_01.GetComponent<BoxCollider>().center, S1_fur_01.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map1_Fur[1] = new Furniture_list(2, "sofa", S1_fur_02.GetComponent<BoxCollider>().center, S1_fur_02.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map1_Fur[2] = new Furniture_list(3, "table", S1_fur_03.GetComponent<BoxCollider>().center, S1_fur_03.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map1_Fur[3] = new Furniture_list(4, "table", S1_fur_04.GetComponent<BoxCollider>().center, S1_fur_04.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map1_Fur[4] = new Furniture_list(5, "sofa", S1_fur_05.GetComponent<BoxCollider>().center, S1_fur_05.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map1_Fur[5] = new Furniture_list(6, "air_condition", S1_fur_06.GetComponent<BoxCollider>().center, S1_fur_06.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map1_Fur[6] = new Furniture_list(7, "sofa", S1_fur_07.GetComponent<BoxCollider>().center, S1_fur_07.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map1_Fur[7] = new Furniture_list(8, "TV", S1_fur_08.GetComponent<BoxCollider>().center, S1_fur_08.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map1_Fur[8] = new Furniture_list(9, "table", S1_fur_09.GetComponent<BoxCollider>().center, S1_fur_09.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map1_Fur[9] = new Furniture_list(10, "refriger", S1_fur_10.GetComponent<BoxCollider>().center, S1_fur_10.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map1_Fur[10] = new Furniture_list(11, "table", S1_fur_11.GetComponent<BoxCollider>().center, S1_fur_11.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map1_Fur[11] = new Furniture_list(12, "sink", S1_fur_12.GetComponent<BoxCollider>().center, S1_fur_12.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map1_Fur[12] = new Furniture_list(13, "chair", S1_fur_13.GetComponent<BoxCollider>().center, S1_fur_13.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map1_Fur[13] = new Furniture_list(14, "chair", S1_fur_14.GetComponent<BoxCollider>().center, S1_fur_14.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map1_Fur[14] = new Furniture_list(15, "chair", S1_fur_15.GetComponent<BoxCollider>().center, S1_fur_15.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map1_Fur[15] = new Furniture_list(16, "chair", S1_fur_16.GetComponent<BoxCollider>().center, S1_fur_16.GetComponent<BoxCollider>().size, S1_fur_01_attr);

        Map1_Fur[16] = new Furniture_list(17, "window", S1_fur_17.GetComponent<BoxCollider>().center, S1_fur_17.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map1_Fur[17] = new Furniture_list(18, "window", S1_fur_18.GetComponent<BoxCollider>().center, S1_fur_18.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map1_Fur[18] = new Furniture_list(19, "cabinet", S1_fur_19.GetComponent<BoxCollider>().center, S1_fur_19.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map1_Fur[19] = new Furniture_list(20, "shelf", S1_fur_20.GetComponent<BoxCollider>().center, S1_fur_20.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map1_Fur[20] = new Furniture_list(21, "cabinet", S1_fur_21.GetComponent<BoxCollider>().center, S1_fur_21.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map1_Fur[21] = new Furniture_list(22, "cabinet", S1_fur_22.GetComponent<BoxCollider>().center, S1_fur_22.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map1_Fur[22] = new Furniture_list(23, "cabinet", S1_fur_23.GetComponent<BoxCollider>().center, S1_fur_23.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map1_Fur[23] = new Furniture_list(24, "window", S1_fur_24.GetComponent<BoxCollider>().center, S1_fur_24.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map1_Fur[24] = new Furniture_list(25, "window", S1_fur_25.GetComponent<BoxCollider>().center, S1_fur_25.GetComponent<BoxCollider>().size, S1_fur_01_attr);

        //S2_furniture_list
        S2_fur_01 = GameObject.Find("S2_Chair_1");
        S2_fur_02 = GameObject.Find("S2_Chair_2");
        S2_fur_03 = GameObject.Find("S2_Chair_3");
        S2_fur_04 = GameObject.Find("S2_Dining_table_1");
        S2_fur_05 = GameObject.Find("S2_Chair_4");
        S2_fur_06 = GameObject.Find("S2_Chair_5");
        S2_fur_07 = GameObject.Find("S2_Chair_6");
        S2_fur_08 = GameObject.Find("S2_Chair_7");
        S2_fur_09 = GameObject.Find("S2_Dining_table_2");
        S2_fur_10 = GameObject.Find("S2_Refrigerator");
        S2_fur_11 = GameObject.Find("S2_Large_sofa_1");
        S2_fur_12 = GameObject.Find("S2_Floor_lamp_1");
        S2_fur_13 = GameObject.Find("S2_Side_table");
        S2_fur_14 = GameObject.Find("S2_Coffee_table");
        S2_fur_15 = GameObject.Find("S2_Small_sofa_1");
        S2_fur_16 = GameObject.Find("S2_Small_sofa_2");
        S2_fur_17 = GameObject.Find("S2_Small_sofa_3");
        S2_fur_18 = GameObject.Find("S2_Small_sofa_4");
        S2_fur_19 = GameObject.Find("S2_TV_table");
        S2_fur_20 = GameObject.Find("S2_TV");

        S2_fur_21 = GameObject.Find("S2_Dining_table_3");
        S2_fur_22 = GameObject.Find("S2_Chair_8");
        S2_fur_23 = GameObject.Find("S2_Chair_9");
        /*
        S2_fur_24 = GameObject.Find("S2_Window_1");
        S2_fur_25 = GameObject.Find("S2_Window_2");
        S2_fur_26 = GameObject.Find("S2_Window_3");
        S2_fur_27 = GameObject.Find("S2_Window_4");
        S2_fur_28 = GameObject.Find("S2_Window_5");
        S2_fur_29 = GameObject.Find("S2_Window_6");
        S2_fur_30 = GameObject.Find("S2_Window_7");
        */
        
        Map2_Fur[0] = new Furniture_list(1, "chair", S2_fur_01.transform.position, S2_fur_01.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map2_Fur[1] = new Furniture_list(2, "chair", S2_fur_02.transform.position, S2_fur_02.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map2_Fur[2] = new Furniture_list(3, "chair", S2_fur_03.transform.position, S2_fur_03.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map2_Fur[3] = new Furniture_list(4, "table", S2_fur_04.transform.position, S2_fur_04.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map2_Fur[4] = new Furniture_list(5, "chair", S2_fur_05.GetComponent<BoxCollider>().center, S2_fur_05.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map2_Fur[5] = new Furniture_list(6, "chair", S2_fur_06.GetComponent<BoxCollider>().center, S2_fur_06.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map2_Fur[6] = new Furniture_list(7, "chair", S2_fur_07.GetComponent<BoxCollider>().center, S2_fur_07.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map2_Fur[7] = new Furniture_list(8, "chair", S2_fur_08.GetComponent<BoxCollider>().center, S2_fur_08.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map2_Fur[8] = new Furniture_list(9, "table", S2_fur_09.GetComponent<BoxCollider>().center, S2_fur_09.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map2_Fur[9] = new Furniture_list(10, "refriger", S2_fur_10.GetComponent<BoxCollider>().center, S2_fur_10.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map2_Fur[10] = new Furniture_list(11, "sofa", S2_fur_11.GetComponent<BoxCollider>().center, S2_fur_11.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map2_Fur[11] = new Furniture_list(12, "lamp", S2_fur_12.GetComponent<BoxCollider>().center, S2_fur_12.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map2_Fur[12] = new Furniture_list(13, "table", S2_fur_13.GetComponent<BoxCollider>().center, S2_fur_13.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map2_Fur[13] = new Furniture_list(14, "table", S2_fur_14.GetComponent<BoxCollider>().center, S2_fur_14.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map2_Fur[14] = new Furniture_list(15, "sofa", S2_fur_15.GetComponent<BoxCollider>().center, S2_fur_15.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map2_Fur[15] = new Furniture_list(16, "sofa", S2_fur_16.GetComponent<BoxCollider>().center, S2_fur_16.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map2_Fur[16] = new Furniture_list(17, "sofa", S2_fur_17.GetComponent<BoxCollider>().center, S2_fur_17.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map2_Fur[17] = new Furniture_list(18, "sofa", S2_fur_18.GetComponent<BoxCollider>().center, S2_fur_18.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map2_Fur[18] = new Furniture_list(19, "table", S2_fur_19.GetComponent<BoxCollider>().center, S2_fur_19.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map2_Fur[19] = new Furniture_list(20, "TV", S2_fur_20.GetComponent<BoxCollider>().center, S2_fur_20.GetComponent<BoxCollider>().size, S1_fur_01_attr);

        Map2_Fur[20] = new Furniture_list(21, "chair", S2_fur_21.transform.position, S2_fur_21.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map2_Fur[21] = new Furniture_list(22, "chair", S2_fur_22.transform.position, S2_fur_22.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map2_Fur[22] = new Furniture_list(23, "table", S2_fur_23.transform.position, S2_fur_23.GetComponent<BoxCollider>().size, S1_fur_01_attr);

        /*
        Map2_Fur[23] = new Furniture_list(24, "window", S2_fur_24.transform.position, S2_fur_24.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map2_Fur[24] = new Furniture_list(25, "window", S2_fur_25.transform.position, S2_fur_25.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map2_Fur[25] = new Furniture_list(26, "window", S2_fur_26.transform.position, S2_fur_26.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map2_Fur[26] = new Furniture_list(27, "window", S2_fur_27.transform.position, S2_fur_27.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map2_Fur[27] = new Furniture_list(28, "window", S2_fur_28.transform.position, S2_fur_28.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map2_Fur[28] = new Furniture_list(29, "window", S2_fur_29.transform.position, S2_fur_29.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map2_Fur[29] = new Furniture_list(30, "window", S2_fur_30.transform.position, S2_fur_30.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        */




        //S3_furniture_list
        S3_fur_01 = GameObject.Find("S3_Piano_1");
        S3_fur_02 = GameObject.Find("S3_Chair_1");
        S3_fur_03 = GameObject.Find("S3_Cabinet_1");
        S3_fur_04 = GameObject.Find("S3_Dining_table_1");
        S3_fur_05 = GameObject.Find("S3_Chair_2");
        S3_fur_06 = GameObject.Find("S3_Chair_3");
        S3_fur_07 = GameObject.Find("S3_Chair_4");
        S3_fur_08 = GameObject.Find("S3_Chair_5");
        S3_fur_09 = GameObject.Find("S3_Chair_6");
        S3_fur_10 = GameObject.Find("S3_Chair_7");
        S3_fur_11 = GameObject.Find("S3_Cabinet_2");
        S3_fur_12 = GameObject.Find("S3_Cabinet_3");
        S3_fur_13 = GameObject.Find("S3_Shelf_1");
        S3_fur_14 = GameObject.Find("S3_Sofa_1");
        S3_fur_15 = GameObject.Find("S3_Coffee_table_1");
        S3_fur_16 = GameObject.Find("S3_Chair_8");
        S3_fur_17 = GameObject.Find("S3_Chair_9");
        S3_fur_18 = GameObject.Find("S3_Chair_10");
        S3_fur_19 = GameObject.Find("S3_TV_table");
        S3_fur_20 = GameObject.Find("S3_TV");

        S3_fur_21 = GameObject.Find("S3_Window_1");
        S3_fur_22 = GameObject.Find("S3_Window_2");
        S3_fur_23 = GameObject.Find("S3_Window_3");


        Map3_Fur[0] = new Furniture_list(1, "piano", S3_fur_01.GetComponent<BoxCollider>().center, S3_fur_01.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map3_Fur[1] = new Furniture_list(2, "chair", S3_fur_02.GetComponent<BoxCollider>().center, S3_fur_02.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map3_Fur[2] = new Furniture_list(3, "cabinet", S3_fur_03.GetComponent<BoxCollider>().center, S3_fur_03.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map3_Fur[3] = new Furniture_list(4, "table", S3_fur_04.GetComponent<BoxCollider>().center, S3_fur_04.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map3_Fur[4] = new Furniture_list(5, "chair", S3_fur_05.GetComponent<BoxCollider>().center, S3_fur_05.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map3_Fur[5] = new Furniture_list(6, "chair", S3_fur_06.GetComponent<BoxCollider>().center, S3_fur_06.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map3_Fur[6] = new Furniture_list(7, "chair", S3_fur_07.GetComponent<BoxCollider>().center, S3_fur_07.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map3_Fur[7] = new Furniture_list(8, "chair", S3_fur_08.GetComponent<BoxCollider>().center, S3_fur_08.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map3_Fur[8] = new Furniture_list(9, "chair", S3_fur_09.GetComponent<BoxCollider>().center, S3_fur_09.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map3_Fur[9] = new Furniture_list(10, "chair", S3_fur_10.GetComponent<BoxCollider>().center, S3_fur_10.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map3_Fur[10] = new Furniture_list(11, "cabinet", S3_fur_11.GetComponent<BoxCollider>().center, S3_fur_11.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map3_Fur[11] = new Furniture_list(12, "cabinet", S3_fur_12.GetComponent<BoxCollider>().center, S3_fur_12.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map3_Fur[12] = new Furniture_list(13, "shelf", S3_fur_13.GetComponent<BoxCollider>().center, S3_fur_13.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map3_Fur[13] = new Furniture_list(14, "sofa", S3_fur_14.GetComponent<BoxCollider>().center, S3_fur_14.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map3_Fur[14] = new Furniture_list(15, "table", S3_fur_15.GetComponent<BoxCollider>().center, S3_fur_15.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map3_Fur[15] = new Furniture_list(16, "chair", S3_fur_16.GetComponent<BoxCollider>().center, S3_fur_16.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map3_Fur[16] = new Furniture_list(17, "chair", S3_fur_17.GetComponent<BoxCollider>().center, S3_fur_17.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map3_Fur[17] = new Furniture_list(18, "chair", S3_fur_18.GetComponent<BoxCollider>().center, S3_fur_18.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map3_Fur[18] = new Furniture_list(19, "table", S3_fur_19.GetComponent<BoxCollider>().center, S3_fur_19.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map3_Fur[19] = new Furniture_list(20, "TV", S3_fur_20.GetComponent<BoxCollider>().center, S3_fur_20.GetComponent<BoxCollider>().size, S1_fur_01_attr);

        Map3_Fur[20] = new Furniture_list(21, "window", S3_fur_21.GetComponent<BoxCollider>().center, S3_fur_21.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map3_Fur[21] = new Furniture_list(22, "window", S3_fur_22.GetComponent<BoxCollider>().center, S3_fur_22.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map3_Fur[22] = new Furniture_list(23, "window", S3_fur_23.GetComponent<BoxCollider>().center, S3_fur_23.GetComponent<BoxCollider>().size, S1_fur_01_attr);

        //S4_furniture_list
        S4_fur_01 = GameObject.Find("S4_Dining_table_1");
        S4_fur_02 = GameObject.Find("S4_Chair_1");
        S4_fur_03 = GameObject.Find("S4_Chair_2");
        S4_fur_04 = GameObject.Find("S4_Chair_3");
        S4_fur_05 = GameObject.Find("S4_Chair_4");
        S4_fur_06 = GameObject.Find("S4_Cabinet_1");
        S4_fur_07 = GameObject.Find("S4_Sofa_1");
        S4_fur_08 = GameObject.Find("S4_Sofa_2");
        S4_fur_09 = GameObject.Find("S4_Sofa_3");
        S4_fur_10 = GameObject.Find("S4_Coffee_table_1");
        S4_fur_11 = GameObject.Find("S4_Side_table_1");
        S4_fur_12 = GameObject.Find("S4_TV_table");
        S4_fur_13 = GameObject.Find("S4_TV");
        S4_fur_14 = GameObject.Find("S4_Window_1");
        S4_fur_15 = GameObject.Find("S4_Window_2");
        S4_fur_16 = GameObject.Find("S4_Window_3");

        Map4_Fur[0] = new Furniture_list(1, "table", S4_fur_01.GetComponent<BoxCollider>().center, S4_fur_01.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map4_Fur[1] = new Furniture_list(2, "chair", S4_fur_02.GetComponent<BoxCollider>().center, S4_fur_02.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map4_Fur[2] = new Furniture_list(3, "chair", S4_fur_03.GetComponent<BoxCollider>().center, S4_fur_03.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map4_Fur[3] = new Furniture_list(4, "chair", S4_fur_04.GetComponent<BoxCollider>().center, S4_fur_04.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map4_Fur[4] = new Furniture_list(5, "chair", S4_fur_05.GetComponent<BoxCollider>().center, S4_fur_05.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map4_Fur[5] = new Furniture_list(6, "cabinet", S4_fur_06.GetComponent<BoxCollider>().center, S4_fur_06.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map4_Fur[6] = new Furniture_list(7, "sofa", S4_fur_07.GetComponent<BoxCollider>().center, S4_fur_07.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map4_Fur[7] = new Furniture_list(8, "sofa", S4_fur_08.GetComponent<BoxCollider>().center, S4_fur_08.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map4_Fur[8] = new Furniture_list(9, "sofa", S4_fur_09.GetComponent<BoxCollider>().center, S4_fur_09.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map4_Fur[9] = new Furniture_list(10, "table", S4_fur_10.GetComponent<BoxCollider>().center, S4_fur_10.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map4_Fur[10] = new Furniture_list(11, "table", S4_fur_11.GetComponent<BoxCollider>().center, S4_fur_11.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map4_Fur[11] = new Furniture_list(12, "table", S4_fur_12.GetComponent<BoxCollider>().center, S4_fur_12.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map4_Fur[12] = new Furniture_list(13, "TV", S4_fur_13.GetComponent<BoxCollider>().center, S4_fur_13.GetComponent<BoxCollider>().size, S1_fur_01_attr);


        Map4_Fur[13] = new Furniture_list(14, "window", S4_fur_14.transform.position, S4_fur_14.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map4_Fur[14] = new Furniture_list(15, "window", S4_fur_15.transform.position, S4_fur_15.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map4_Fur[15] = new Furniture_list(16, "window", S4_fur_16.transform.position, S4_fur_16.GetComponent<BoxCollider>().size, S1_fur_01_attr);


        //S5_furniture_list
        S5_fur_01 = GameObject.Find("S5_Air_condition_1");
        S5_fur_02 = GameObject.Find("S5_TV_table");
        S5_fur_03 = GameObject.Find("S5_TV");
        S5_fur_04 = GameObject.Find("S5_Sofa_1");
        S5_fur_05 = GameObject.Find("S5_Sofa_2");
        S5_fur_06 = GameObject.Find("S5_Sofa_3");
        S5_fur_07 = GameObject.Find("S5_Sofa_4");
        S5_fur_08 = GameObject.Find("S5_Side_table_1");
        S5_fur_09 = GameObject.Find("S5_Coffee_table_1");
        S5_fur_10 = GameObject.Find("S5_Dining_table_1");
        S5_fur_11 = GameObject.Find("S5_Chair_1");
        S5_fur_12 = GameObject.Find("S5_Chair_2");
        S5_fur_13 = GameObject.Find("S5_Chair_3");
        S5_fur_14 = GameObject.Find("S5_Chair_4");
        S5_fur_15 = GameObject.Find("S5_Cabinet_1");
        S5_fur_16 = GameObject.Find("S5_Refriger_1");
        S5_fur_17 = GameObject.Find("S5_Cabinet_2");
        S5_fur_18 = GameObject.Find("S5_Cabinet_3");
        S5_fur_19 = GameObject.Find("S5_Sink_1");

        S5_fur_20 = GameObject.Find("S5_Window_1");
        S5_fur_21 = GameObject.Find("S5_Window_2");
        S5_fur_22 = GameObject.Find("S5_Window_3");
        S5_fur_23 = GameObject.Find("S5_Window_4");
        S5_fur_24 = GameObject.Find("S5_Window_5");
        S5_fur_25 = GameObject.Find("S5_Window_6");

        Map5_Fur[0] = new Furniture_list(1, "air_condition", S5_fur_01.GetComponent<BoxCollider>().center, S5_fur_01.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map5_Fur[1] = new Furniture_list(2, "table", S5_fur_02.GetComponent<BoxCollider>().center, S5_fur_02.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map5_Fur[2] = new Furniture_list(3, "TV", S5_fur_03.GetComponent<BoxCollider>().center, S5_fur_03.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map5_Fur[3] = new Furniture_list(4, "sofa", S5_fur_04.GetComponent<BoxCollider>().center, S5_fur_04.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map5_Fur[4] = new Furniture_list(5, "sofa", S5_fur_05.GetComponent<BoxCollider>().center, S5_fur_05.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map5_Fur[5] = new Furniture_list(6, "sofa", S5_fur_06.GetComponent<BoxCollider>().center, S5_fur_06.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map5_Fur[6] = new Furniture_list(7, "sofa", S5_fur_07.GetComponent<BoxCollider>().center, S5_fur_07.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map5_Fur[7] = new Furniture_list(8, "table", S5_fur_08.GetComponent<BoxCollider>().center, S5_fur_08.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map5_Fur[8] = new Furniture_list(9, "table", S5_fur_09.GetComponent<BoxCollider>().center, S5_fur_09.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map5_Fur[9] = new Furniture_list(10, "table", S5_fur_10.GetComponent<BoxCollider>().center, S5_fur_10.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map5_Fur[10] = new Furniture_list(11, "chair", S5_fur_11.GetComponent<BoxCollider>().center, S5_fur_11.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map5_Fur[11] = new Furniture_list(12, "chair", S5_fur_12.GetComponent<BoxCollider>().center, S5_fur_12.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map5_Fur[12] = new Furniture_list(13, "chair", S5_fur_13.GetComponent<BoxCollider>().center, S5_fur_13.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map5_Fur[13] = new Furniture_list(14, "chair", S5_fur_14.GetComponent<BoxCollider>().center, S5_fur_14.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map5_Fur[14] = new Furniture_list(15, "cabinet", S5_fur_15.GetComponent<BoxCollider>().center, S5_fur_15.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map5_Fur[15] = new Furniture_list(16, "refriger", S5_fur_16.GetComponent<BoxCollider>().center, S5_fur_16.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map5_Fur[16] = new Furniture_list(17, "cabinet", S5_fur_17.GetComponent<BoxCollider>().center, S5_fur_17.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map5_Fur[17] = new Furniture_list(18, "cabinet", S5_fur_18.GetComponent<BoxCollider>().center, S5_fur_18.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map5_Fur[18] = new Furniture_list(19, "sink", S5_fur_19.GetComponent<BoxCollider>().center, S5_fur_19.GetComponent<BoxCollider>().size, S1_fur_01_attr);

        Map5_Fur[19] = new Furniture_list(20, "window", S5_fur_20.transform.position, S5_fur_20.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map5_Fur[20] = new Furniture_list(21, "window", S5_fur_21.transform.position, S5_fur_21.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map5_Fur[21] = new Furniture_list(22, "window", S5_fur_22.transform.position, S5_fur_22.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map5_Fur[22] = new Furniture_list(23, "window", S5_fur_23.transform.position, S5_fur_23.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map5_Fur[23] = new Furniture_list(24, "window", S5_fur_24.transform.position, S5_fur_24.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map5_Fur[24] = new Furniture_list(25, "window", S5_fur_25.transform.position, S5_fur_25.GetComponent<BoxCollider>().size, S1_fur_01_attr);

        //S6_furniture_list
        S6_fur_01 = GameObject.Find("S6_Dining_table_1");
        S6_fur_02 = GameObject.Find("S6_Chair_1");
        S6_fur_03 = GameObject.Find("S6_Chair_2");
        S6_fur_04 = GameObject.Find("S6_Chair_3");
        S6_fur_05 = GameObject.Find("S6_Chair_4");
        S6_fur_06 = GameObject.Find("S6_Refriger_1");
        S6_fur_07 = GameObject.Find("S6_Dining_table_2");
        S6_fur_08 = GameObject.Find("S6_Chair_5");
        S6_fur_09 = GameObject.Find("S6_Chair_6");
        S6_fur_10 = GameObject.Find("S6_Side_table_1");
        S6_fur_11 = GameObject.Find("S6_Shelf_1");
        S6_fur_12 = GameObject.Find("S6_Sofa_1");
        S6_fur_13 = GameObject.Find("S6_Sofa_2");
        S6_fur_14 = GameObject.Find("S6_Sofa_3");
        S6_fur_15 = GameObject.Find("S6_Sofa_4");
        S6_fur_16 = GameObject.Find("S6_Sofa_5");
        S6_fur_17 = GameObject.Find("S6_Coffee_table_1");
        S6_fur_18 = GameObject.Find("S6_TV");

        S6_fur_19 = GameObject.Find("S6_Window_1");
        S6_fur_20 = GameObject.Find("S6_Window_2");
        S6_fur_21 = GameObject.Find("S6_Window_3");
        S6_fur_22 = GameObject.Find("S6_Window_4");
        S6_fur_23 = GameObject.Find("S6_Window_5");
        S6_fur_24 = GameObject.Find("S6_Window_6");
        S6_fur_25 = GameObject.Find("S6_Window_7");

        Map6_Fur[0] = new Furniture_list(1, "table", S6_fur_01.GetComponent<BoxCollider>().center, S6_fur_01.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map6_Fur[1] = new Furniture_list(2, "chair", S6_fur_02.GetComponent<BoxCollider>().center, S6_fur_02.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map6_Fur[2] = new Furniture_list(3, "chair", S6_fur_03.GetComponent<BoxCollider>().center, S6_fur_03.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map6_Fur[3] = new Furniture_list(4, "chair", S6_fur_04.GetComponent<BoxCollider>().center, S6_fur_04.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map6_Fur[4] = new Furniture_list(5, "chair", S6_fur_05.GetComponent<BoxCollider>().center, S6_fur_05.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map6_Fur[5] = new Furniture_list(6, "refriger", S6_fur_06.GetComponent<BoxCollider>().center, S6_fur_06.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map6_Fur[6] = new Furniture_list(7, "table", S6_fur_07.GetComponent<BoxCollider>().center, S6_fur_07.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map6_Fur[7] = new Furniture_list(8, "chair", S6_fur_08.GetComponent<BoxCollider>().center, S6_fur_08.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map6_Fur[8] = new Furniture_list(9, "chair", S6_fur_09.GetComponent<BoxCollider>().center, S6_fur_09.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map6_Fur[9] = new Furniture_list(10, "table", S6_fur_10.GetComponent<BoxCollider>().center, S6_fur_10.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map6_Fur[10] = new Furniture_list(11, "shelf", S6_fur_11.GetComponent<BoxCollider>().center, S6_fur_11.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map6_Fur[11] = new Furniture_list(12, "sofa", S6_fur_12.GetComponent<BoxCollider>().center, S6_fur_12.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map6_Fur[12] = new Furniture_list(13, "sofa", S6_fur_13.GetComponent<BoxCollider>().center, S6_fur_13.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map6_Fur[13] = new Furniture_list(14, "sofa", S6_fur_14.GetComponent<BoxCollider>().center, S6_fur_14.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map6_Fur[14] = new Furniture_list(15, "sofa", S6_fur_15.GetComponent<BoxCollider>().center, S6_fur_15.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map6_Fur[15] = new Furniture_list(16, "sofa", S6_fur_16.GetComponent<BoxCollider>().center, S6_fur_16.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map6_Fur[16] = new Furniture_list(17, "table", S6_fur_17.GetComponent<BoxCollider>().center, S6_fur_17.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map6_Fur[17] = new Furniture_list(18, "TV", S6_fur_18.GetComponent<BoxCollider>().center, S6_fur_18.GetComponent<BoxCollider>().size, S1_fur_01_attr);

        Map6_Fur[18] = new Furniture_list(19, "window", S6_fur_19.transform.position, S6_fur_19.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map6_Fur[19] = new Furniture_list(20, "window", S6_fur_20.transform.position, S6_fur_20.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map6_Fur[20] = new Furniture_list(21, "window", S6_fur_21.transform.position, S6_fur_21.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map6_Fur[21] = new Furniture_list(22, "window", S6_fur_22.transform.position, S6_fur_22.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map6_Fur[22] = new Furniture_list(23, "window", S6_fur_23.transform.position, S6_fur_23.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map6_Fur[23] = new Furniture_list(24, "window", S6_fur_24.transform.position, S6_fur_24.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map6_Fur[24] = new Furniture_list(25, "window", S6_fur_25.transform.position, S6_fur_25.GetComponent<BoxCollider>().size, S1_fur_01_attr);

        //S7_furniture_list
        S7_fur_01 = GameObject.Find("S7_Piano_1");
        S7_fur_02 = GameObject.Find("S7_Chair_1");
        S7_fur_03 = GameObject.Find("S7_Cabinet_1");
        S7_fur_04 = GameObject.Find("S7_Dining_table_1");
        S7_fur_05 = GameObject.Find("S7_Chair_2");
        S7_fur_06 = GameObject.Find("S7_Chair_3");
        S7_fur_07 = GameObject.Find("S7_Chair_4");
        S7_fur_08 = GameObject.Find("S7_Chair_5");
        S7_fur_09 = GameObject.Find("S7_Cabinet_2");
        S7_fur_10 = GameObject.Find("S7_Sofa_1");
        S7_fur_11 = GameObject.Find("S7_Coffee_table_1");
        S7_fur_12 = GameObject.Find("S7_Chair_6");
        S7_fur_13 = GameObject.Find("S7_Cabinet_3");
        S7_fur_14 = GameObject.Find("S7_Shelf_1");
        S7_fur_15 = GameObject.Find("S7_Chair_7");
        S7_fur_16 = GameObject.Find("S7_Chair_8");
        S7_fur_17 = GameObject.Find("S7_TV_table_1");
        S7_fur_18 = GameObject.Find("S7_TV");
        S7_fur_19 = GameObject.Find("S7_Window_1");
        S7_fur_20 = GameObject.Find("S7_Window_2");
        S7_fur_21 = GameObject.Find("S7_Window_3");


        Map7_Fur[0] = new Furniture_list(1, "piano", S7_fur_01.GetComponent<BoxCollider>().center, S7_fur_01.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map7_Fur[1] = new Furniture_list(2, "chair", S7_fur_02.GetComponent<BoxCollider>().center, S7_fur_02.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map7_Fur[2] = new Furniture_list(3, "cabinet", S7_fur_03.GetComponent<BoxCollider>().center, S7_fur_03.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map7_Fur[3] = new Furniture_list(4, "table", S7_fur_04.GetComponent<BoxCollider>().center, S7_fur_04.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map7_Fur[4] = new Furniture_list(5, "chair", S7_fur_05.GetComponent<BoxCollider>().center, S7_fur_05.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map7_Fur[5] = new Furniture_list(6, "chair", S7_fur_06.GetComponent<BoxCollider>().center, S7_fur_06.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map7_Fur[6] = new Furniture_list(7, "chair", S7_fur_07.GetComponent<BoxCollider>().center, S7_fur_07.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map7_Fur[7] = new Furniture_list(8, "chair", S7_fur_08.GetComponent<BoxCollider>().center, S7_fur_08.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map7_Fur[8] = new Furniture_list(9, "cabinet", S7_fur_09.GetComponent<BoxCollider>().center, S7_fur_09.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map7_Fur[9] = new Furniture_list(10, "sofa", S7_fur_10.GetComponent<BoxCollider>().center, S7_fur_10.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map7_Fur[10] = new Furniture_list(11, "table", S7_fur_11.GetComponent<BoxCollider>().center, S7_fur_11.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map7_Fur[11] = new Furniture_list(12, "chair", S7_fur_12.GetComponent<BoxCollider>().center, S7_fur_12.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map7_Fur[12] = new Furniture_list(13, "cabinet", S7_fur_13.GetComponent<BoxCollider>().center, S7_fur_13.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map7_Fur[13] = new Furniture_list(14, "shelf", S7_fur_14.GetComponent<BoxCollider>().center, S7_fur_14.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map7_Fur[14] = new Furniture_list(15, "chair", S7_fur_15.GetComponent<BoxCollider>().center, S7_fur_15.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map7_Fur[15] = new Furniture_list(16, "chair", S7_fur_16.GetComponent<BoxCollider>().center, S7_fur_16.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map7_Fur[16] = new Furniture_list(17, "table", S7_fur_17.GetComponent<BoxCollider>().center, S7_fur_17.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map7_Fur[17] = new Furniture_list(18, "TV", S7_fur_18.GetComponent<BoxCollider>().center, S7_fur_18.GetComponent<BoxCollider>().size, S1_fur_01_attr);

        Map7_Fur[18] = new Furniture_list(19, "window", S7_fur_19.GetComponent<BoxCollider>().center, S7_fur_19.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map7_Fur[19] = new Furniture_list(20, "window", S7_fur_20.GetComponent<BoxCollider>().center, S7_fur_20.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map7_Fur[20] = new Furniture_list(21, "window", S7_fur_21.GetComponent<BoxCollider>().center, S7_fur_21.GetComponent<BoxCollider>().size, S1_fur_01_attr);

        //S8_furniture_list
        S8_fur_01 = GameObject.Find("S8_Dining_table_1");
        S8_fur_02 = GameObject.Find("S8_Chair_1");
        S8_fur_03 = GameObject.Find("S8_Chair_2");
        S8_fur_04 = GameObject.Find("S8_Chair_3");
        S8_fur_05 = GameObject.Find("S8_Chair_4");
        S8_fur_06 = GameObject.Find("S8_Cabinet_1");
        S8_fur_07 = GameObject.Find("S8_Side_table_1");
        S8_fur_08 = GameObject.Find("S8_Sofa_1");
        S8_fur_09 = GameObject.Find("S8_Sofa_2");
        S8_fur_10 = GameObject.Find("S8_Sofa_3");
        S8_fur_11 = GameObject.Find("S8_Coffee_table_1");
        S8_fur_12 = GameObject.Find("S8_TV_table_1");
        S8_fur_13 = GameObject.Find("S8_TV");

        S8_fur_14 = GameObject.Find("S8_Window_1");
        S8_fur_15 = GameObject.Find("S8_Window_2");
        S8_fur_16 = GameObject.Find("S8_Window_3");


        Map8_Fur[0] = new Furniture_list(1, "table", S8_fur_01.GetComponent<BoxCollider>().center, S8_fur_01.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map8_Fur[1] = new Furniture_list(2, "chair", S8_fur_02.GetComponent<BoxCollider>().center, S8_fur_02.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map8_Fur[2] = new Furniture_list(3, "chair", S8_fur_03.GetComponent<BoxCollider>().center, S8_fur_03.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map8_Fur[3] = new Furniture_list(4, "chair", S8_fur_04.GetComponent<BoxCollider>().center, S8_fur_04.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map8_Fur[4] = new Furniture_list(5, "chair", S8_fur_05.GetComponent<BoxCollider>().center, S8_fur_05.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map8_Fur[5] = new Furniture_list(6, "cabinet", S8_fur_06.GetComponent<BoxCollider>().center, S8_fur_06.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map8_Fur[6] = new Furniture_list(7, "table", S8_fur_07.GetComponent<BoxCollider>().center, S8_fur_07.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map8_Fur[7] = new Furniture_list(8, "sofa", S8_fur_08.GetComponent<BoxCollider>().center, S8_fur_08.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map8_Fur[8] = new Furniture_list(9, "sofa", S8_fur_09.GetComponent<BoxCollider>().center, S8_fur_09.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map8_Fur[9] = new Furniture_list(10, "sofa", S8_fur_10.GetComponent<BoxCollider>().center, S8_fur_10.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map8_Fur[10] = new Furniture_list(11, "table", S8_fur_11.GetComponent<BoxCollider>().center, S8_fur_11.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map8_Fur[11] = new Furniture_list(12, "table", S8_fur_12.GetComponent<BoxCollider>().center, S8_fur_12.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map8_Fur[12] = new Furniture_list(13, "TV", S8_fur_13.GetComponent<BoxCollider>().center, S8_fur_13.GetComponent<BoxCollider>().size, S1_fur_01_attr);

        Map8_Fur[13] = new Furniture_list(14, "window", S8_fur_14.transform.position, S8_fur_14.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map8_Fur[14] = new Furniture_list(15, "window", S8_fur_15.transform.position, S8_fur_15.GetComponent<BoxCollider>().size, S1_fur_01_attr);
        Map8_Fur[15] = new Furniture_list(16, "window", S8_fur_16.transform.position, S8_fur_16.GetComponent<BoxCollider>().size, S1_fur_01_attr);


        //s3 - 20 , s4 - 13 , s5 - 19 , s6 - 18 , s7 - 18 ,  s8 - 13
    }
    void RecallHumX()
    {
        if (HumXT.activeSelf == true)
        {
            HumX = HumXT;
        }
        else if (HumXT_Sit.activeSelf == true)
        {
            HumX = HumXT_Sit;
        }
    }
    void RecallAvaX()
    {
        if (AvaXT.activeSelf == true)
        {
            AvaX = AvaXT;
        }
        else if (AvaXT_Sit.activeSelf == true)
        {
            AvaX = AvaXT_Sit;
        }
    }
    public void PairToFurnitureMap(int pair)
    {
        if (pair == 1)
        {
            S1_Furnit_list = Map1_Fur;
            S2_Furnit_list = Map2_Fur;
        }
        else if (pair == 2)
        {
            S1_Furnit_list = Map1_Fur;
            S2_Furnit_list = Map3_Fur;
        }
        else if (pair == 3)
        {
            S1_Furnit_list = Map1_Fur;
            S2_Furnit_list = Map4_Fur;
        }
        else if (pair == 4)
        {
            S1_Furnit_list = Map1_Fur;
            S2_Furnit_list = Map6_Fur;
        }
        else if (pair == 5)
        {
            S1_Furnit_list = Map1_Fur;
            S2_Furnit_list = Map7_Fur;
        }
        else if (pair == 6)
        {
            S1_Furnit_list = Map1_Fur;
            S2_Furnit_list = Map8_Fur;
        }
        else if (pair == 7)
        {
            S1_Furnit_list = Map2_Fur;
            S2_Furnit_list = Map3_Fur;
        }
        else if (pair == 8)
        {
            S1_Furnit_list = Map2_Fur;
            S2_Furnit_list = Map4_Fur;
        }
        else if (pair == 9)
        {
            S1_Furnit_list = Map2_Fur;
            S2_Furnit_list = Map5_Fur;
        }
        else if (pair == 10)
        {
            S1_Furnit_list = Map2_Fur;
            S2_Furnit_list = Map7_Fur;
        }
        else if (pair == 11)
        {
            S1_Furnit_list = Map2_Fur;
            S2_Furnit_list = Map8_Fur;
        }
        else if (pair == 12)
        {
            S1_Furnit_list = Map3_Fur;
            S2_Furnit_list = Map4_Fur;
        }
        else if (pair == 13)
        {
            S1_Furnit_list = Map3_Fur;
            S2_Furnit_list = Map5_Fur;
        }
        else if (pair == 14)
        {
            S1_Furnit_list = Map3_Fur;
            S2_Furnit_list = Map6_Fur;
        }
        else if (pair == 15)
        {
            S1_Furnit_list = Map3_Fur;
            S2_Furnit_list = Map8_Fur;
        }
        else if (pair == 16)
        {
            S1_Furnit_list = Map4_Fur;
            S2_Furnit_list = Map5_Fur;
        }
        else if (pair == 17)
        {
            S1_Furnit_list = Map4_Fur;
            S2_Furnit_list = Map6_Fur;
        }
        else if (pair == 18)
        {
            S1_Furnit_list = Map4_Fur;
            S2_Furnit_list = Map7_Fur;
        }
        else if (pair == 19)
        {
            S1_Furnit_list = Map5_Fur;
            S2_Furnit_list = Map6_Fur;
        }
        else if (pair == 20)
        {
            S1_Furnit_list = Map5_Fur;
            S2_Furnit_list = Map7_Fur;
        }
        else if (pair == 21)
        {
            S1_Furnit_list = Map5_Fur;
            S2_Furnit_list = Map8_Fur;
        }
        else if (pair == 22)
        {
            S1_Furnit_list = Map6_Fur;
            S2_Furnit_list = Map7_Fur;
        }
        else if (pair == 23)
        {
            S1_Furnit_list = Map6_Fur;
            S2_Furnit_list = Map8_Fur;
        }
        else if (pair == 24)
        {
            S1_Furnit_list = Map7_Fur;
            S2_Furnit_list = Map8_Fur;
        }
    }
    public void Hum_to_fur_line()
    {
        if (HumX != null)
        {
            for (int i = 0; i < S1_Furnit_list.Length; i++)
            {
                Vector3 vec_dist;
                vec_dist = new Vector3(HumX.transform.position.x - S1_Furnit_list[i].Position.x, 0f, HumX.transform.position.z - S1_Furnit_list[i].Position.z);
                if (vec_dist.magnitude < 3f)
                {
                    Debug.DrawLine(HumX.transform.position, S1_Furnit_list[i].Position, Color.black);
                }
            }
        }
        if (AvaX != null)
        {
            for (int i = 0; i < S2_Furnit_list.Length; i++)
            {
                Vector3 vec_dist;
                vec_dist = new Vector3(AvaX.transform.position.x - S2_Furnit_list[i].Position.x,0f, AvaX.transform.position.z - S2_Furnit_list[i].Position.z);
                if (vec_dist.magnitude < 3f)
                {
                    Debug.DrawLine(AvaX.transform.position, S2_Furnit_list[i].Position, Color.black);
                }
            }
        }
    }
    public void Hum_to_fur_line_VA()
    {
        if (HumX != null)
        {
            for (int i = 0; i < S1_Furnit_list.Length; i++)
            {
                float Hum_arrow_x = Mathf.Cos((90f - HumX.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + HumX.transform.position.x;
                float Hum_arrow_z = Mathf.Sin((90f - HumX.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + HumX.transform.position.z;
                Vector3 Hum_front = new Vector3(Hum_arrow_x - HumX.transform.position.x, 0f, Hum_arrow_z - HumX.transform.position.z);
                Vector3 vec_dist;
                vec_dist = new Vector3(HumX.transform.position.x - S1_Furnit_list[i].Position.x, 0f, HumX.transform.position.z - S1_Furnit_list[i].Position.z);
                float temp_ang = (SignedAngle(Hum_front, new Vector3(vec_dist.x, 0f, vec_dist.z), Vector3.up) + 180f);
                if (vec_dist.magnitude < 4f && (temp_ang > 340f || temp_ang < 20f))
                {
                    Debug.DrawLine(HumX.transform.position, S1_Furnit_list[i].Position, Color.black);
                }
            }
        }
        if (AvaX != null)
        {
            for (int i = 0; i < S2_Furnit_list.Length; i++)
            {
                float Ava_arrow_x = Mathf.Cos((90f - AvaX.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + AvaX.transform.position.x;
                float Ava_arrow_z = Mathf.Sin((90f - AvaX.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + AvaX.transform.position.z;
                Vector3 Ava_front = new Vector3(Ava_arrow_x - AvaX.transform.position.x, 0f, Ava_arrow_z - AvaX.transform.position.z);
                Vector3 vec_dist;
                vec_dist = new Vector3(AvaX.transform.position.x - S2_Furnit_list[i].Position.x, 0f, AvaX.transform.position.z - S2_Furnit_list[i].Position.z);
                float temp_ang = (SignedAngle(Ava_front, new Vector3(vec_dist.x, 0f, vec_dist.z), Vector3.up) + 180f);
                if (vec_dist.magnitude < 4f && (temp_ang >340f || temp_ang <20f))
                {
                    Debug.DrawLine(AvaX.transform.position, S2_Furnit_list[i].Position, Color.black);
                }
            }
        }
    }
    int IndexOfCategory(string fur_category)
    {
        if (fur_category == "sofa")
        {
            return 0;
        }
        else if (fur_category == "table")
        {
            return 1;
        }
        else if (fur_category == "chair")
        {
            return 2;
        }
        else if (fur_category == "TV")
        {
            return 3;
        }
        else if (fur_category == "air_condition")
        {
            return 4;
        }
        else if (fur_category == "refriger")
        {
            return 5;
        }
        else if (fur_category == "sink")
        {
            return 6;
        }
        else if (fur_category == "lamp")
        {
            return 7;
        }
        else if (fur_category == "piano")
        {
            return 8;
        }
        else if (fur_category == "cabinet")
        {
            return 9;
        }
        else if (fur_category == "shelf")
        {
            return 10;
        }
        else if (fur_category == "window")
        {
            return 11;
        }

        else
        {
            return 99;
        }
    }
    float SignedAngle(Vector3 from, Vector3 to, Vector3 normal)
    {
        // angle in [0,180]
        float angle = Vector3.Angle(from, to);
        float sign = Mathf.Sign(Vector3.Dot(normal, Vector3.Cross(from, to)));
        return angle * sign;
    }
    void Occupancy_map(float dist, float ang, string cat)
    {
         //sector_ID
        if (dist <1.5f)
        {
            if(ang>=330f || ang<30F)
            {
                sector_ID = 0;
            }
            else if (ang >= 30f && ang < 90F)
            {
                sector_ID = 1;
            }
            else if (ang >= 90f && ang < 150F)
            {
                sector_ID = 2;
            }
            else if (ang >= 150f && ang < 210F)
            {
                sector_ID = 3;
            }
            else if (ang >= 210f && ang < 270F)
            {
                sector_ID = 4;
            }
            else if(ang >= 270f && ang < 330F)
            {
                sector_ID = 5;
            }
        }
        else
        {
            if (ang >= 330f || ang < 30F)
            {
                sector_ID = 6;
            }
            else if (ang >= 30f && ang < 90F)
            {
                sector_ID = 7;
            }
            else if (ang >= 90f && ang < 150F)
            {
                sector_ID = 8;
            }
            else if (ang >= 150f && ang < 210F)
            {
                sector_ID = 9;
            }
            else if (ang >= 210f && ang < 270F)
            {
                sector_ID = 10;
            }
            else if(ang >= 270f && ang < 330F)
            {
                sector_ID = 11;
            }
        }

        //cat_ID
        if(cat=="sofa")
        {
            cat_ID = 0;
        }
        else if (cat == "table")
        {
            cat_ID = 1;
        }
        else if (cat == "chair")
        {
            cat_ID = 2;
        }
        else if (cat == "TV")
        {
            cat_ID = 3;
        }
        else if (cat == "air_condition")
        {
            cat_ID = 4;
        }
        else if (cat == "refriger")
        {
            cat_ID = 5;
        }
        else if (cat == "sink")
        {
            cat_ID = 6;
        }
        else if (cat == "lamp")
        {
            cat_ID = 7;
        }
        else if (cat == "piano")
        {
            cat_ID = 8;
        }
        else if (cat == "cabinet")
        {
            cat_ID = 9;
        }
        else if (cat == "shelf")
        {
            cat_ID = 10;
        }
        else if (cat == "window")
        {
            cat_ID = 11;
        }
    }
    float Occup_sim(int[,] o_map1, int[,] o_map2)
    {
        float fur_sim_val = 0f;
        int[] map1_cat_vec = new int[num_cat];
        
        int[] map2_cat_vec = new int[num_cat];
        int[] map1_occup_vec = new int[num_sect];
        int[] map2_occup_vec = new int[num_sect];

        for (int i=0; i< num_cat; i++)
        {
            int col_sum = 0;
            for (int j = 0; j < num_sect; j++)
            {
                col_sum = col_sum + o_map1[j, i];
            }
            map1_cat_vec[i] = col_sum;
        }
        //Debug.Log("Map1_category : [ " + map1_cat_vec[0] + " , " + map1_cat_vec[1] + " , " + map1_cat_vec[2] + " , " + map1_cat_vec[3] + " , " + map1_cat_vec[4] + " , " + map1_cat_vec[5] + " , " + map1_cat_vec[6] + " , " + map1_cat_vec[7] + " , " + map1_cat_vec[8] + " , " + map1_cat_vec[9] + " , " + map1_cat_vec[10] + " ]");

        for (int i = 0; i < num_cat; i++)
        {
            int col_sum = 0;
            for (int j = 0; j < num_sect; j++)
            {
                col_sum = col_sum + o_map2[j, i];
            }
            map2_cat_vec[i] = col_sum;
        }
        //Debug.Log("map2_category : [ " + map2_cat_vec[0] + " , " + map2_cat_vec[1] + " , " + map2_cat_vec[2] + " , " + map2_cat_vec[3] + " , " + map2_cat_vec[4] + " , " + map2_cat_vec[5] + " , " + map2_cat_vec[6] + " , " + map2_cat_vec[7] + " , " + map2_cat_vec[8] + " , " + map2_cat_vec[9] + " , " + map2_cat_vec[10] + " ]");
        //Debug.Log("-----------------------------------------------");

        for (int i = 0; i < num_sect; i++)
        {
            int row_sum = 0;
            for (int j = 0; j < num_cat; j++)
            {
                row_sum = row_sum + o_map1[i, j];
            }
            map1_occup_vec[i] = row_sum;
        }
        //Debug.Log("Map1_occupancy : [ " + map1_occup_vec[0] + " , " + map1_occup_vec[1] + " , " + map1_occup_vec[2] + " , " + map1_occup_vec[3] + " , " + map1_occup_vec[4] + " , " + map1_occup_vec[5] + " , " + map1_occup_vec[6] + " , " + map1_occup_vec[7] + " , " + map1_occup_vec[8] + " , " + map1_occup_vec[9] + " , " + map1_occup_vec[10] + " , " + map1_occup_vec[11] + " ]");

        for (int i = 0; i < num_sect; i++)
        {
            int row_sum = 0;
            for (int j = 0; j < num_cat; j++)
            {
                row_sum = row_sum + o_map2[i, j];
            }
            map2_occup_vec[i] = row_sum;
        }
        //Debug.Log("map2_occupancy : [ " + map2_occup_vec[0] + " , " + map2_occup_vec[1] + " , " + map2_occup_vec[2] + " , " + map2_occup_vec[3] + " , " + map2_occup_vec[4] + " , " + map2_occup_vec[5] + " , " + map2_occup_vec[6] + " , " + map2_occup_vec[7] + " , " + map2_occup_vec[8] + " , " + map2_occup_vec[9] + " , " + map2_occup_vec[10] + " , " + map2_occup_vec[11] + " ]");
        //Debug.Log("-----------------------------------------------");

        //float[] cat_sim = new float[8];
        float each_cat_sim = 1f;
        int intersection_count = 0;
        int union_count = 0;
        for ( int i = 0; i<num_cat; i++)
        {
            if (map1_cat_vec[i] > 0 || map2_cat_vec[i] > 0)
            {
                union_count++;
            }
            if (map1_cat_vec[i]>0 && map2_cat_vec[i]>0)
            {
                //cat_sim[i] = (Mathf.Min(map1_cat_vec[i], map2_cat_vec[i]) / Mathf.Max(map1_cat_vec[i], map2_cat_vec[i]));
                intersection_count++;

                //Debug.Log("Cat[" + i + "] :" + ((float)Mathf.Min(map1_cat_vec[i], map2_cat_vec[i]) / Mathf.Max(map1_cat_vec[i], map2_cat_vec[i])));
                each_cat_sim = each_cat_sim * ((float)Mathf.Min(map1_cat_vec[i], map2_cat_vec[i]) / Mathf.Max(map1_cat_vec[i], map2_cat_vec[i]));
                
            }
        }
        if(intersection_count == 0)
        {
            each_cat_sim = 0f;
        }
        float total_cat_sim = (float)intersection_count / union_count;
        if (union_count == 0)
        {
            total_cat_sim = 0f;
        }
        //Debug.Log("Intersected_cat_sim : " + each_cat_sim);
        //Debug.Log("Total_cat_sim : " + total_cat_sim);
        float cat_dist_sim = 0f;
        if(total_cat_sim > 0f)
        {
            cat_dist_sim= Mathf.Pow(each_cat_sim, (1f / intersection_count)) * total_cat_sim;
        }
            
        //Debug.Log("-----------------------------------------------");
        //Debug.Log("Category_dist_sim : " + cat_dist_sim);
        
        union_count = 0;
        intersection_count = 0;
        float occup_sim = 1f;
        for (int i = 0; i < num_sect; i++)
        {
            if (map1_occup_vec[i] > 0 || map2_occup_vec[i] > 0)
            {
                union_count++;
            }
            if (map1_occup_vec[i] > 0 && map2_occup_vec[i] > 0)
            {
                intersection_count++;
            }
        }
        //Debug.Log("Total_occup_sim : " + ((float)intersection_count / union_count));
        if (union_count == 0)
        {
            occup_sim = 0;
        }
        else
        {
            occup_sim = ((float)intersection_count / union_count);
        }       
        
        //Debug.Log("-----------------------------------------------");
        float space_sim = cat_dist_sim + occup_sim;
        //Debug.Log("Space_sim : " + space_sim);
        //Debug.Log("-----------------------------------------------");
        global_cat_sim = cat_dist_sim;
        global_occup_sim = occup_sim;
        return fur_sim_val;
    }
    public void Space_similarity(GameObject temp_HumX, GameObject temp_AvaX)
    {

        Hum_arrow_x = Mathf.Cos((90f - temp_HumX.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + temp_HumX.transform.position.x;
        Hum_arrow_z = Mathf.Sin((90f - temp_HumX.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + temp_HumX.transform.position.z;
        Hum_front = new Vector3(Hum_arrow_x - temp_HumX.transform.position.x, 0f, Hum_arrow_z - temp_HumX.transform.position.z);

        Ava_arrow_x = Mathf.Cos((90f - temp_AvaX.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + temp_AvaX.transform.position.x;
        Ava_arrow_z = Mathf.Sin((90f - temp_AvaX.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + temp_AvaX.transform.position.z;
        Ava_front = new Vector3(Ava_arrow_x - temp_AvaX.transform.position.x, 0f, Ava_arrow_z - temp_AvaX.transform.position.z);

        //Furnitures inside the boundary of circle - Loonking for a better name

        //int[,] S1_occupancy = new int[num_sect, num_cat];
        //int[,] S2_occupancy = new int[num_sect, num_cat];



        //좌우 구분하는 부분 바꿔야해
        //S1 list of furniture that is inside of circle with radius 3
        //Debug.Log("-----------------------------------------------");
        for (int i = 0; i < S1_Furnit_list.Length; i++)
        {
            
            vec_dist = new Vector3(temp_HumX.transform.position.x - S1_Furnit_list[i].Position.x, 0f, temp_HumX.transform.position.z - S1_Furnit_list[i].Position.z);
            if (vec_dist.magnitude < 3f)
            {
                //Debug.Log("Map1_Fur[" + i + "].Category : " + Map1_Fur[i].Category); // + "                    Dist : " + vec_dist.magnitude);
                S1_fur_list.Add(S1_Furnit_list[i].Category);
                //float temp_ang = (SignedAngle(Hum_front, new Vector3(vec_dist.x, 0f, vec_dist.z), Vector3.up) + 180f);
                //Occupancy_map(vec_dist.magnitude, temp_ang, S1_Furnit_list[i].Category);
                //S1_occupancy[sector_ID, cat_ID]++;
            }

        }

        //Debug.Log("-----------------------------------------------");
        for (int i = 0; i < S2_Furnit_list.Length; i++)
        {

            vec_dist = new Vector3(temp_AvaX.transform.position.x - S2_Furnit_list[i].Position.x, 0f, temp_AvaX.transform.position.z - S2_Furnit_list[i].Position.z);
            if (vec_dist.magnitude < 3f)
            {
                //Debug.Log("Map2_Fur[" + i + "].Category : " + Map2_Fur[i].Category + "                    Dist : " + vec_dist.magnitude);
                S2_fur_list.Add(S2_Furnit_list[i].Category);
                //float temp_ang = (SignedAngle(Ava_front, new Vector3(vec_dist.x, 0f, vec_dist.z), Vector3.up) + 180f);
                //Debug.Log("Map2_temp_ang : " + temp_ang);
                //Set Sector ID and Category ID
                //Occupancy_map(vec_dist.magnitude, temp_ang, S2_Furnit_list[i].Category);
                //2D array of sector and category distribution
                //S2_occupancy[sector_ID, cat_ID]++;
            }
        }

        //Debug.Log("S1_fur_list-----------------------------------------------");
        foreach (var n in S1_fur_list)
        {

            //Debug.Log(n);
            //Debug.Log(IndexOfCategory(n));
            S1_cat_freq[IndexOfCategory(n)]++;
        }
        //Debug.Log("S1_cat_freq[" + S1_cat_freq[0] + ", " + S1_cat_freq[1] + ", " + S1_cat_freq[2] + ", " + S1_cat_freq[3] + ", " + S1_cat_freq[4] + ", " + S1_cat_freq[5] + ", " + S1_cat_freq[6] + ", " + S1_cat_freq[7] + ", " + S1_cat_freq[8] + ", " + S1_cat_freq[9] + ", " + S1_cat_freq[10] + ", "+ S1_cat_freq[11] + "]");

        //Debug.Log("S2_fur_list-----------------------------------------------");
        foreach (var n in S2_fur_list)
        {

            //Debug.Log(n);
            //Debug.Log(IndexOfCategory(n));
            S2_cat_freq[IndexOfCategory(n)]++;
        }
        //Debug.Log("S2_cat_freq[" + S2_cat_freq[0] + ", " + S2_cat_freq[1] + ", " + S2_cat_freq[2] + ", " + S2_cat_freq[3] + ", " + S2_cat_freq[4] + ", " + S2_cat_freq[5] + ", " + S2_cat_freq[6] + ", " + S2_cat_freq[7] + ", " + S2_cat_freq[8] + ", " + S2_cat_freq[9] + ", " + S2_cat_freq[10] + ", " + S2_cat_freq[11] + "]");




        each_cat_sim = 1f;
        intersection_count = 0;
        union_count = 0;
        for (int i = 0; i < num_cat; i++)
        {
            if (S1_cat_freq[i] > 0 || S2_cat_freq[i] > 0)
            {
                union_count++;
            }
            if (S1_cat_freq[i] > 0 && S2_cat_freq[i] > 0)
            {
                //cat_sim[i] = (Mathf.Min(map1_cat_vec[i], map2_cat_vec[i]) / Mathf.Max(map1_cat_vec[i], map2_cat_vec[i]));
                intersection_count++;

                //Debug.Log("Cat[" + i + "] :" + ((float)Mathf.Min(map1_cat_vec[i], map2_cat_vec[i]) / Mathf.Max(map1_cat_vec[i], map2_cat_vec[i])));
                each_cat_sim = each_cat_sim * ((float)Mathf.Min(S1_cat_freq[i], S2_cat_freq[i]) / Mathf.Max(S1_cat_freq[i], S2_cat_freq[i]));

            }
        }
        if (intersection_count == 0)
        {
            each_cat_sim = 0f;
        }
        total_cat_sim = (float)intersection_count / union_count;
        if (union_count == 0)
        {
            total_cat_sim = 0f;
        }
        //Debug.Log("Intersected_cat_sim : " + each_cat_sim);
        //Debug.Log("Total_cat_sim : " + total_cat_sim);
        cat_dist_sim = 0f;
        if (total_cat_sim > 0f)
        {
            cat_dist_sim = Mathf.Pow(each_cat_sim, (1f / intersection_count)) * total_cat_sim;
        }

        global_cat_sim = cat_dist_sim;

        S1_fur_list.Clear();
        S2_fur_list.Clear();
        for (int i = 0; i < num_cat; i++)
        {
            S1_cat_freq[i] = 0;
            S2_cat_freq[i] = 0;
        }
            

        //Debug.Log("Jaccard12: " + Jaccard.Calc(S1_fur_list, S2_fur_list));
        //Jaccard.Calc(S1_fur_list, S2_fur_list);
        //Debug.Log("-----------------------------------------------");
        //float tmp_occup_sim = Occup_sim(S1_occupancy, S2_occupancy);
    }
    public float[] Object_category_frequency_AvaX(GameObject temp_AvaX)
    {
        float[] S2_cat_freq_ava = new float[num_cat];
        Ava_arrow_x = Mathf.Cos((90f - temp_AvaX.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + temp_AvaX.transform.position.x;
        Ava_arrow_z = Mathf.Sin((90f - temp_AvaX.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + temp_AvaX.transform.position.z;
        Ava_front = new Vector3(Ava_arrow_x - temp_AvaX.transform.position.x, 0f, Ava_arrow_z - temp_AvaX.transform.position.z);

        //Debug.Log("-----------------------------------------------");
        for (int i = 0; i < S2_Furnit_list.Length; i++)
        {

            vec_dist = new Vector3(temp_AvaX.transform.position.x - S2_Furnit_list[i].Position.x, 0f, temp_AvaX.transform.position.z - S2_Furnit_list[i].Position.z);
            if (vec_dist.magnitude < 3f)
            {
                S2_fur_list.Add(S2_Furnit_list[i].Category);
                S2_cat_freq_ava[IndexOfCategory(S2_Furnit_list[i].Category)] += (3f - vec_dist.magnitude) / 3f/5f;
                //S2_cat_freq_ava[IndexOfCategory(S2_Furnit_list[i].Category)]++;
                //Debug.Log("AvaX Object - " + S2_Furnit_list[i].Category + " distance - " + vec_dist.magnitude);
            }
        }
        //Debug.Log("S2_fur_list-----------------------------------------------");
        foreach (var n in S2_fur_list)
        {
            //S2_cat_freq_ava[IndexOfCategory(n)]++;
        }
        S2_fur_list.Clear();
        return S2_cat_freq_ava;

    }
    public float[] Object_category_frequency_HumX(GameObject temp_HumX)
    {

        float[] S1_cat_freq_hum = new float[num_cat];

        Hum_arrow_x = Mathf.Cos((90f - temp_HumX.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + temp_HumX.transform.position.x;
        Hum_arrow_z = Mathf.Sin((90f - temp_HumX.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + temp_HumX.transform.position.z;
        Hum_front = new Vector3(Hum_arrow_x - temp_HumX.transform.position.x, 0f, Hum_arrow_z - temp_HumX.transform.position.z);

        //Debug.Log("-----------------------------------------------");
        for (int i = 0; i < S1_Furnit_list.Length; i++)
        {

            vec_dist = new Vector3(temp_HumX.transform.position.x - S1_Furnit_list[i].Position.x, 0f, temp_HumX.transform.position.z - S1_Furnit_list[i].Position.z);
            if (vec_dist.magnitude < 3f)
            {
                //Debug.Log("Map1_Fur[" + i + "].Category : " + Map1_Fur[i].Category); // + "                    Dist : " + vec_dist.magnitude);
                S1_fur_list.Add(S1_Furnit_list[i].Category);
                S1_cat_freq_hum[IndexOfCategory(S1_Furnit_list[i].Category)] += (3f - vec_dist.magnitude) / 3f/5f;
                //Debug.Log("HumX Object - " + S1_Furnit_list[i].Category + " distance - " + vec_dist.magnitude);
            }

        }


        //Debug.Log("S1_fur_list-----------------------------------------------");
        foreach (var n in S1_fur_list)
        {

            //Debug.Log(n);
            //Debug.Log(IndexOfCategory(n));
            //S1_cat_freq_hum[IndexOfCategory(n)]++;
        }
        //Debug.Log("S1_cat_freq[" + S1_cat_freq[0] + ", " + S1_cat_freq[1] + ", " + S1_cat_freq[2] + ", " + S1_cat_freq[3] + ", " + S1_cat_freq[4] + ", " + S1_cat_freq[5] + ", " + S1_cat_freq[6] + ", " + S1_cat_freq[7] + ", " + S1_cat_freq[8] + ", " + S1_cat_freq[9] + ", " + S1_cat_freq[10] + ", "+ S1_cat_freq[11] + "]");

        S1_fur_list.Clear();

        return S1_cat_freq_hum;


    }
    public float[] VisualAttention_Hum(GameObject HumX)
    {
        //Debug.Log("VisualAttention_Hum : " + HumX);
        float[] S1_vis_attn_hum = new float[num_cat];
        if (HumX != null)
        {
            for (int i = 0; i < S1_Furnit_list.Length; i++)
            {
                Hum_arrow_x = Mathf.Cos((90f - HumX.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + HumX.transform.position.x;
                Hum_arrow_z = Mathf.Sin((90f - HumX.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + HumX.transform.position.z;
                Hum_front = new Vector3(Hum_arrow_x - HumX.transform.position.x, 0f, Hum_arrow_z - HumX.transform.position.z);
                vec_dist = new Vector3(HumX.transform.position.x - S1_Furnit_list[i].Position.x, 0f, HumX.transform.position.z - S1_Furnit_list[i].Position.z);
                float temp_ang = (SignedAngle(Hum_front, new Vector3(vec_dist.x, 0f, vec_dist.z), Vector3.up) + 180f);
                if ((vec_dist.magnitude > 0.25f && vec_dist.magnitude < 4f) && (temp_ang > 340f || temp_ang < 20f))
                {
                    float normed_angle = 0f;
                    if(temp_ang >340f)
                    {
                        normed_angle = 1f - (360f - temp_ang) / 20f;
                    }
                    else
                    {
                        normed_angle = 1f - temp_ang / 20f;
                    }
                    S1_vis_attn_hum[IndexOfCategory(S1_Furnit_list[i].Category)] += (4f- vec_dist.magnitude)/4f/5f*normed_angle;
                    //Debug.Log("HumX Object - " + S1_Furnit_list[i].Category + " distance - " + vec_dist.magnitude + " normed_angle - " + normed_angle);

                }
            }
        }
        return S1_vis_attn_hum;

    }
    public float[] VisualAttention_Ava(GameObject AvaX)
    {
        //Debug.Log("VisualAttention_Ava : " + AvaX);
        float[] S2_vis_attn_hum = new float[num_cat];
        if (AvaX != null)
        {
            for (int i = 0; i < S2_Furnit_list.Length; i++)
            {
                Ava_arrow_x = Mathf.Cos((90f - AvaX.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + AvaX.transform.position.x;
                Ava_arrow_z = Mathf.Sin((90f - AvaX.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + AvaX.transform.position.z;
                Ava_front = new Vector3(Ava_arrow_x - AvaX.transform.position.x, 0f, Ava_arrow_z - AvaX.transform.position.z);
                vec_dist = new Vector3(AvaX.transform.position.x - S2_Furnit_list[i].Position.x, 0f, AvaX.transform.position.z - S2_Furnit_list[i].Position.z);
                float temp_ang = (SignedAngle(Ava_front, new Vector3(vec_dist.x, 0f, vec_dist.z), Vector3.up) + 180f);
                if ((vec_dist.magnitude > 0.25f && vec_dist.magnitude < 4f) && (temp_ang > 340f || temp_ang < 20f))
                {
                    float normed_angle = 0f;
                    if (temp_ang > 340f)
                    {
                        normed_angle = 1f - (360f - temp_ang) / 20f;
                    }
                    else
                    {
                        normed_angle = 1f - temp_ang / 20f;
                    }
                    S2_vis_attn_hum[IndexOfCategory(S2_Furnit_list[i].Category)] += (4f - vec_dist.magnitude) / 4f / 5f* normed_angle;
                    //Debug.Log("AvaX Object - " + S2_Furnit_list[i].Category + " distance - " + vec_dist.magnitude + " normed_angle - " + normed_angle);
                }
            }
        }
        return S2_vis_attn_hum;
  
    }
    public float VA_diff(GameObject HumX, GameObject AvaX)
    {
        if (HumX != null)
        {
            for (int i = 0; i < S1_Furnit_list.Length; i++)
            {
                Hum_arrow_x = Mathf.Cos((90f - HumX.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + HumX.transform.position.x;
                Hum_arrow_z = Mathf.Sin((90f - HumX.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + HumX.transform.position.z;
                Hum_front = new Vector3(Hum_arrow_x - HumX.transform.position.x, 0f, Hum_arrow_z - HumX.transform.position.z);
                vec_dist = new Vector3(HumX.transform.position.x - S1_Furnit_list[i].Position.x, 0f, HumX.transform.position.z - S1_Furnit_list[i].Position.z);
                float temp_ang = (SignedAngle(Hum_front, new Vector3(vec_dist.x, 0f, vec_dist.z), Vector3.up) + 180f);
                if ((vec_dist.magnitude > 0.25f && vec_dist.magnitude < 4f) && (temp_ang > 340f || temp_ang < 20f))
                {
                    S1_fur_ID_VA.Add(S1_Furnit_list[i].ID);
                    S1_fur_cat_VA.Add(S1_Furnit_list[i].Category);
                    S1_fur_dist_VA.Add(vec_dist.magnitude);
                    //Debug.DrawLine(HumX.transform.position, S1_Furnit_list[i].Position, Color.green);
                }
            }
        }
        if (AvaX != null)
        {
            for (int i = 0; i < S2_Furnit_list.Length; i++)
            {
                Ava_arrow_x = Mathf.Cos((90f - AvaX.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + AvaX.transform.position.x;
                Ava_arrow_z = Mathf.Sin((90f - AvaX.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + AvaX.transform.position.z;
                Ava_front = new Vector3(Ava_arrow_x - AvaX.transform.position.x, 0f, Ava_arrow_z - AvaX.transform.position.z);
                vec_dist = new Vector3(AvaX.transform.position.x - S2_Furnit_list[i].Position.x, 0f, AvaX.transform.position.z - S2_Furnit_list[i].Position.z);
                float temp_ang = (SignedAngle(Ava_front, new Vector3(vec_dist.x, 0f, vec_dist.z), Vector3.up) + 180f);
                if ((vec_dist.magnitude > 0.25f && vec_dist.magnitude < 4f) && (temp_ang > 340f || temp_ang < 20f))
                {
                    S2_fur_ID_VA.Add(S2_Furnit_list[i].ID);
                    S2_fur_cat_VA.Add(S2_Furnit_list[i].Category);
                    S2_fur_dist_VA.Add(vec_dist.magnitude);
                    //Debug.DrawLine(AvaX.transform.position, S2_Furnit_list[i].Position, Color.green);
                }
            }
        }
        /*
        //Debug.Log("S1_fur : " + S1_fur_ID_VA.Count);
        //Debug.Log("-------------------------------------------");
        for (int i = 0; i < S1_fur_ID_VA.Count; i++)
        {

            //Debug.Log(S1_fur_cat_VA[i]);
            //Debug.Log(S1_fur_dist_VA[i]);
        }
        //Debug.Log("-------------------------------------------");
        //Debug.Log("S2_fur : " + S2_fur_ID_VA.Count);
        //Debug.Log("-------------------------------------------");
        for (int i = 0; i < S2_fur_ID_VA.Count; i++)
        {

            //Debug.Log(S2_fur_cat_VA[i]);
            //Debug.Log(S2_fur_dist_VA[i]);
        }
        //Debug.Log("-------------------------------------------");
        */
        float VA_dist = 0f;

        bool bothObject = true;
        bool leastMatched = false;
        if (S1_fur_cat_VA.Count == 0 || S2_fur_cat_VA.Count == 0)
        {
            bothObject = false;
        }


        while (S1_fur_cat_VA.Count > 0 && S2_fur_cat_VA.Count > 0)
        {
            //Debug.Log("S1 Object Count :  " + S1_fur_cat_VA.Count + "-------"+ "S2 Object Count :  " + S2_fur_cat_VA.Count);
            bool isMatched = false;
            float VA_dist_single = 100000f;
            int s1_idx = -99;
            int s2_idx = -99;
            for (int j = 0; j < S1_fur_cat_VA.Count; j++)
            {
                //Debug.Log("S1 Object Count :  " + j  + "-------");
                for (int i = 0; i < S2_fur_cat_VA.Count; i++)
                {
                    //Debug.Log("S2 Object Count (current) :  " + i + "-------");
                    //Debug.Log("S2 Object Count (total) :  " + S2_fur_ID_VA.Count + "-------");
                    if (S1_fur_cat_VA[j] == S2_fur_cat_VA[i])
                    {
                        //Debug.Log("Debug Point");
                        isMatched = true;
                        float temp_dist = Mathf.Abs(S2_fur_dist_VA[i] - S1_fur_dist_VA[j]);


                        if (temp_dist < VA_dist_single)
                        {
                            VA_dist_single = temp_dist;
                            s2_idx = i;
                            s1_idx = j;
                            /*
                            Debug.Log("Matched cndidate------------------------------------");
                            Debug.Log("VA_s1_idx : " + s1_idx);
                            Debug.Log("VA_s2_idx : " + s2_idx);
                            Debug.Log("VA_s1_cat : " + S1_fur_cat_VA[j]);
                            Debug.Log("VA_s2_cat : " + S2_fur_cat_VA[i]);
                            Debug.Log("VA_dist_single : " + VA_dist_single);
                            Debug.Log("-------------------------------------------");
                            */
                        }
                    }
                }
                if (isMatched == false)
                {
                    /*
                    Debug.Log("Not matched (removed) ------------------------------------");
                    Debug.Log("VA_s1_idx : " + j);
                    Debug.Log("VA_s1_cat : " + S1_fur_cat_VA[j]);
                    */
                    S1_fur_cat_VA.RemoveAt(j);
                    S1_fur_dist_VA.RemoveAt(j);
                }
            }

            if (isMatched == true)
            {
                leastMatched = true;
                /*
                Debug.Log("Matched ------------------------------------");
                Debug.Log("VA_s1_idx : " + s1_idx);
                Debug.Log("VA_s2_idx : " + s2_idx);
                Debug.Log("VA_s1_cat : " + S1_fur_cat_VA[s1_idx]);
                Debug.Log("VA_s2_cat : " + S2_fur_cat_VA[s2_idx]);
                */
                S1_fur_cat_VA.RemoveAt(s1_idx);
                S1_fur_dist_VA.RemoveAt(s1_idx);
                S2_fur_cat_VA.RemoveAt(s2_idx);
                S2_fur_dist_VA.RemoveAt(s2_idx);
                VA_dist = VA_dist + VA_dist_single;
            }

        }
        //float VA_sim = 1f - (VA_dist / 5f);
        VA_dist = VA_dist / 5f;

        if (bothObject == false || leastMatched == false)
        {
            VA_dist = 1f;
        }
        //Debug.Log("Visual attention distance : " + VA_dist);
        //Debug.Log("Visual attention similarity : " + VA_sim);
        //Debug.Log("-------------------------------------------");
        S1_fur_ID_VA.Clear();
        S2_fur_ID_VA.Clear();

        S1_fur_cat_VA.Clear();
        S2_fur_cat_VA.Clear();

        S1_fur_dist_VA.Clear();
        S2_fur_dist_VA.Clear();
        return VA_dist;
    }
    public float Free_space_diff(GameObject HumX, GameObject AvaX)
    {
        float FS_diff = 0f;

        RaycastHit hit;

        float angAva = AvaX.transform.eulerAngles.y;
        

        ray_dir_down.Set(0f, -1f, 0f);
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                int temp_count = 0;
                for (int k = 1; k < 4; k++)
                {
                    for (int l = 1; l < 4; l++)
                    {
                        
                        ray_start.Set(AvaX.transform.position.x + (0.5f * i + 0.1f * l) * Mathf.Cos(Mathf.PI / 180f * (30.0F * j + 6.0f * k - angAva + 90f)), 2.25f, AvaX.transform.position.z + (0.5f * i + 0.1f * l) * Mathf.Sin(Mathf.PI / 180f * (30.0F * j + 6.0f * k - angAva + 90f)));
                        //Random.Range((30.0F * (k - 1) - angAva + 90f) % 360f, (15.0F * k - angAva + 90f) % 360f)), 2.25f, AvaX.transform.position.z + 0.25f * (l - 1f + randsqrt) * Mathf.Sin(Mathf.PI / 180f * Random.Range((15.0F * (k - 1) - angAva + 90f) % 360f, (15.0F * k - angAva + 90f) % 360f)));
                        if (Physics.Raycast(ray_start, ray_dir_down, out hit, 3f))
                        {

                            if (hit.point.y < 0.1)
                            {
                                temp_count++;
                                //ray_vis.Set(ray_start.x, 0.25f, ray_start.z);
                                //Debug.DrawLine(ray_vis, hit.point, Color.blue);
                            }
                        }
                    }
                }
                if(temp_count==9)
                {
                    free_Ava[i * 12 + j] = 1;
                }
                else
                {
                    free_Ava[i * 12 + j] = 0;
                }
            }
        }

        float angHum = HumX.transform.eulerAngles.y;
        

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                int temp_count = 0;
                for (int k = 1; k < 4; k++)
                {
                    for (int l = 1; l < 4; l++)
                    {

                        ray_start.Set(HumX.transform.position.x + (0.5f * i + 0.1f * l) * Mathf.Cos(Mathf.PI / 180f * (30.0F * j + 6.0f * k - angHum + 90f)), 2.25f, HumX.transform.position.z + (0.5f * i + 0.1f * l) * Mathf.Sin(Mathf.PI / 180f * (30.0F * j + 6.0f * k - angHum + 90f)));
                        //Random.Range((30.0F * (k - 1) - angAva + 90f) % 360f, (15.0F * k - angAva + 90f) % 360f)), 2.25f, AvaX.transform.position.z + 0.25f * (l - 1f + randsqrt) * Mathf.Sin(Mathf.PI / 180f * Random.Range((15.0F * (k - 1) - angAva + 90f) % 360f, (15.0F * k - angAva + 90f) % 360f)));
                        if (Physics.Raycast(ray_start, ray_dir_down, out hit, 3f))
                        {

                            if (hit.point.y < 0.1)
                            {
                                temp_count++;
                                //ray_vis.Set(ray_start.x, 0.25f, ray_start.z);
                                //Debug.DrawLine(ray_vis, hit.point, Color.blue);
                            }
                        }
                    }
                }
                if (temp_count == 9)
                {
                    free_Hum[i * 12 + j] = 1;
                }
                else
                {
                    free_Hum[i * 12 + j] = 0;
                }
            }
        }
        int diff_count = 0;
        for(int i=0; i<48; i++)
        {
            if(free_Ava[i] == 1 && free_Hum[i] == 1)
            {
                diff_count++;
            }
        }
        FS_diff = diff_count / 48f;
        FS_diff = 1 - FS_diff;

        return FS_diff;
    }
}