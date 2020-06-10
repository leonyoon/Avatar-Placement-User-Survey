using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using System.IO;
//using System.Linq;
using System.Text;


public class Experiment : MonoBehaviour, IPointerClickHandler
{

    GameObject HumXT, HumXC, HumXT_Sit, HumXC_Sit, AvaXT, AvaXC, AvaXT_Sit, AvaXC_Sit; //HumX and AvaX for Test
    GameObject HumYT, HumYC, HumYT_Sit, HumYC_Sit, AvaYT, AvaYC, AvaYT_Sit, AvaYC_Sit; //HumY and AvaY for Test
    GameObject AvaXT_1, AvaXT_Sit_1, AvaXT_2, AvaXT_Sit_2, AvaXT_3, AvaXT_Sit_3, AvaXT_4, AvaXT_Sit_4, AvaXT_5, AvaXT_Sit_5; //AllDataView(); | Avatar placed by user
    GameObject AvaXT_6, AvaXT_Sit_6, AvaXT_7, AvaXT_Sit_7, AvaXT_8, AvaXT_Sit_8, AvaXT_9, AvaXT_Sit_9, AvaXT_10, AvaXT_Sit_10; //AllDataView(); | Avatar placed by user
    GameObject AvaXC_1, AvaXC_Sit_1; //Camera view of avatar placed by user  
    GameObject AvaX_A_1, AvaX_A_2, AvaX_A_3, AvaX_A_4, AvaX_A_5, AvaX_A_6; //Avatar placed by algorithm (<-- check why only 6 instead of 10)
    GameObject AvaX_B_1, AvaX_B_2, AvaX_B_3, AvaX_B_4, AvaX_B_5, AvaX_B_6, AvaX_B_7, AvaX_B_8, AvaX_B_9, AvaX_B_10, AvaX_B_11, AvaX_B_12, AvaX_B_13, AvaX_B_14, AvaX_B_15, AvaX_B_16, AvaX_B_17, AvaX_B_18, AvaX_B_19, AvaX_B_20;
    GameObject AvaX_1R, AvaX_2O, AvaX_3Y, AvaX_4G, AvaX_5B;
    GameObject AvaX, HumY, HumX, AvaY; //Sit or not?
    GameObject AvaX_U_1, AvaX_U_2, AvaX_U_3, AvaX_U_4, AvaX_U_5, AvaX_U_6, AvaX_U_7, AvaX_U_8, AvaX_U_9, AvaX_U_10;
    public GameObject AvaX_P_prefab, AvaX_N_prefab;
    GameObject[] AvaX_P_clone, AvaX_N_clone, AvaX_N_clone_1, AvaX_N_clone_2, AvaX_N_clone_3, AvaX_N_clone_4, AvaX_N_clone_5, AvaX_N_clone_6, AvaX_N_clone_7, AvaX_N_clone_8, AvaX_N_clone_9, AvaX_N_clone_10;
    public Text pair_num, txt_numScene, user_num;
    GameObject coll1, coll2;
    GameObject[] coll1_objs, coll2_objs;
    int numSec, levelOfAfford, levelOfSpace, level, numSampling, standOrSit, numBin;
    public int numPair, numScene, numUser;
    float randsqrt, totHeight, angHum, angAva, distTtoC1, distTtoC2;
    float arrowX, arrowZ;
    Vector3 ray_start, ray_iso, ray_dir_down, ray_waist;
    Vector3 HumXPos, HumXeulerY, AvaXeulerY;
    public Camera HumXCam, AvaXCam, Top1Cam, Top2Cam;
    GameObject S1, S2, S3, S4, S5, S6, S7, S8;
    GameObject S1Top, S2Top, S3Top, S4Top, S5Top, S6Top, S7Top, S8Top;
    public Button backButton, nextButton, saveButton, redoButton;
    public InputField CHR_IF, Pair_IF, User_IF;
    public Slider rotateY;
    GameObject testAva, creaTa, next, back, save, chr, redo, pair, user, lines;
    int tSitOrStand;
    float[] testData = new float[5*48];
    float[] createData = new float[3*5 * 36];
    float[] createData_48 = new float[3 * 5 * 12];
    public StreamWriter sw, sw_1, sw_2, sw_3;
    public StreamReader sr;
    public FileStream fileStr;
    public bool Userdata, adminmode, edit, Alldata;
    public LineRenderer line1, line2, line3, line4;
    GameObject l1, l2, l3, l4;
    public Dropdown pairDD;
    bool pClicked;
    float[] viewData1, viewData2, viewData3, viewData4, viewData5, viewData6, viewData7, viewData8, viewData9, viewData10;
    Color hColor1, hColor2, hColor3, hColor4, hColor5, hColor6, hColor7, hColor8, hColor9, hColor10;

    float[] fromXfeat, avaXfeat, humXfeat, Dist_feat, Dist_feat4d, humXaff, avaXaff, feat7d, feat_fromX; //, basicFeat;
    Vector3 tempVec3;
    float EulerAngleY, distToY, angleXwrtY, angleYwrtX;
    GameObject[] AvaX_U;
    float[,] AvaU_feat;
    float[] feat, feat4d;
    GameObject[] Ava_Clones;
    bool please = false;
    bool deep_pos = false;
    bool cmc_old = false;
    bool cmc_new = false;
    bool PosRNeg = false;
    bool comSample = false;
    bool comSample_48 = false;
    bool similarity = false;
    bool placement = false;
    bool heatmap = false;
    bool feat_Vis = false;
    bool consistency_bool = false;
    bool consistency_48_bool = false;
    int zzz, yyy, t_count;
    float[,,] similarity_svm = null;
    int pxw, pyh, pzs;
    float timestamp;
    //System.DateTime now;
    int feat_count, dec_count;
    float aim;
    float lowestSim, highestSim, SimDiff, heatLev, heatSim;

    //Play with map
    public Node[,,] Map_whole, Map1, Map2, Map3, Map4, Map5, Map6, Map7, Map8, Map_L, Map_R;
    int widthX, heightZ;
    // 노란색 네모칸으로 node index부터 구하는
    public int X_start, X_end, Z_start, Z_end;
    int S1_X_start, S1_X_end, S1_Z_start, S1_Z_end, S2_X_start, S2_X_end, S2_Z_start, S2_Z_end;
    int S3_X_start, S3_X_end, S3_Z_start, S3_Z_end, S4_X_start, S4_X_end, S4_Z_start, S4_Z_end;
    int S5_X_start, S5_X_end, S5_Z_start, S5_Z_end, S6_X_start, S6_X_end, S6_Z_start, S6_Z_end;
    int S7_X_start, S7_X_end, S7_Z_start, S7_Z_end, S8_X_start, S8_X_end, S8_Z_start, S8_Z_end;
    int S1_width, S1_height, S2_width, S2_height;
    int S3_width, S3_height, S4_width, S4_height;
    int S5_width, S5_height, S6_width, S6_height;
    int S7_width, S7_height, S8_width, S8_height;
    int Map_L_X_start, Map_L_X_end, Map_L_Z_start, Map_L_Z_end;
    int Map_R_X_start, Map_R_X_end, Map_R_Z_start, Map_R_Z_end;
    int Map_L_width, Map_L_height, Map_R_width, Map_R_height;
    int Map_X_start, Map_X_end, Map_Z_start, Map_Z_end;
    int L_space, tptt;
    int f_w_count = 0;
    SimIndex[] sIndex = null;
    SimIndex[] hIndex = null;
    Pathfinder Pathfind;
    Furnitures fur_script;
    Telepresence telp_script;
    float[] sv_coef;
    float[,] SVs;
    float[,] Mat_Lab;
    float[] HumX45, AvaX45, HumX44, AvaX44;
    const int DIM_FEATURE = 85;
    const int FRAME_BUFFER_SIZE = sizeof(float) * DIM_FEATURE * 2;
    int[] rank100_cmc;
    int consistency_count = 0;
    public bool IP = false;
    public bool VA = false;
    public bool PA = false;
    public bool SP = false;

    // Use this for initialization
    void Awake() {

        //Float array initialization for feature extraction and user placement algorithm
        humXfeat = new float[20];
        avaXfeat = new float[20];
        fromXfeat = new float[20];
        Dist_feat = new float[10];
        Dist_feat4d = new float[4];
        humXaff = new float[16];
        avaXaff = new float[16];
        Ava_Clones = new GameObject[10];
        feat7d = new float[10];
        feat4d = new float[4];
        AvaX_U = new GameObject[10];
        AvaU_feat = new float[10, 3];
        feat = new float[3];
        distToY = 0f;
        angleXwrtY = 0f;
        angleYwrtX = 0f;
        AvaX_N_clone = new GameObject[100];
        AvaX_N_clone_1 = new GameObject[10];

        feat_fromX = new float[85];
        HumX45 = new float[45];
        AvaX45 = new float[45];
        HumX44 = new float[44];
        AvaX44 = new float[44];

        rank100_cmc = new int[100];

        //Find GameObject of HumX, AvaX, HumY, AvaT, AvaX1-AvaX10 
        FindAvatars();
        
        //Set active false of all
        DeactAvatars();
        
        //Find line of head orientation
        FindLines();
        
    }
    void Start()
    {
        Application.runInBackground = true;
        SupportVectorsFromText();
        TxtToMat();
        fur_script = GameObject.Find("Furnitures_script").GetComponent<Furnitures>();
        telp_script = GameObject.Find("Socket_script").GetComponent<Telepresence>();

        //Initialization////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        FindModels();
        FindCamera();
        
        //Initialization of 10 different colors
        ColorInit();

        //Miscellaneous initialization
        InitAll();

        //5 circles and rest are fixed?
        levelOfAfford = 1; levelOfSpace = 3; level = 4; numSampling = 1000; numSec = 24; L_space = 2; numBin = 7;

        //Whole_map
        Pathfind = Pathfinder.Instance; Map_whole = Pathfind.Map;
        widthX = Pathfind.width; heightZ = Pathfind.height;
        Map_X_start = 0; Map_X_end = widthX - 1; Map_Z_start = 0; Map_Z_end = heightZ - 1;
        
        //Model activate
        S1.SetActive(true); S2.SetActive(true); S3.SetActive(true); S4.SetActive(true);
        S5.SetActive(true); S6.SetActive(true); S7.SetActive(true); S8.SetActive(true);

        //Whole map to map1, map2, map...////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        MapDivision();

        //Space1 and space2 deactivate after feature extraction----------------------------------------------------------------------------------------------
        S1.SetActive(false); S2.SetActive(false); S3.SetActive(false); S4.SetActive(false);
        S5.SetActive(false); S6.SetActive(false); S7.SetActive(false); S8.SetActive(false);


    }
    // Update is called once per frame
    void Update () {

        //Displaying the text of num Pair, Scene, User
        pair_num.text = "Pair " + numPair.ToString();
        txt_numScene.text = "Scene " + numScene.ToString();
        user_num.text = "User " + numUser.ToString();
        
        /* Darw rectangle around two maps
        if (Map_L != null)
        {
            DrawRectangle(Map_L[0, 0, 0].xCoord, Map_L[Map_L_width - 1, Map_L_height - 1, 0].xCoord, Map_L[0, 0, 0].zCoord, Map_L[Map_L_width - 1, Map_L_height - 1, 0].zCoord, Color.yellow);
            DrawRectangle(Map_R[0, 0, 0].xCoord, Map_R[Map_R_width - 1, Map_R_height - 1, 0].xCoord, Map_R[0, 0, 0].zCoord, Map_R[Map_R_width - 1, Map_R_height - 1, 0].zCoord, Color.yellow);
        }
        */

        #region mode options
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Various menu options
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //View each data now starts with scene 13
        if (Userdata == true && numScene > 36 && numScene < 49)
        {
            SingleUserData();
        }
        //Showing all 10 data of each scene
        if (Alldata==true && numScene > 36 && numScene < 49)
        {
            AllUserData_48();
        }
        //Showing all 10 data of each scene
        if (Alldata == true && numScene > 0 && numScene < 37)
        {
            AllUserData();
        }
        //Each scene data for Pair 12 or greater
        if (adminmode == false && numPair >= 12 && numScene <= 37)
        {
            ViewScene();
        }
        if (adminmode == false && numScene >= 37)
        {
            ViewScene_48();
        }

        //UI for scene generation
        if (adminmode==true)
        {
            SceneGenerateUI();
        }
        // Avatar placement during the experiment
        pClicked = true;
        if(adminmode==false && pClicked==true)
        {
            AvaPlace();
        }
        
        #endregion mode options

        //Call it 'AvaX' whether it's 'AvaXT' or 'AvaXT_Sit'
        RecallAvaX();
        RecallHumY();
        RecallHumX();
        RecallAvaY();
        HeadOrientationLine();//Fixed for HumX, AvaY and HumY head orientation
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

////////FeatureVisualiztion
        #region FeatureVisualization
        if (Input.GetKeyDown(KeyCode.P) && feat_Vis == false)
        {
            feat_Vis = true;
            Debug.Log("Feature Visualization On");
        }
        else if (Input.GetKeyDown(KeyCode.P) && feat_Vis == true)
        {
            feat_Vis = false;
            Debug.Log("Feature Visualization Off");
        }

        if (feat_Vis == true && AvaXT != null && AvaXT.activeSelf == true)
        {
            FeatureVisualization(HumX, AvaY);
            FeatureVisualization(AvaXT, HumY);
            if (SP)
            {
                fur_script.Hum_to_fur_line();
            }
            if(VA)
            {
                fur_script.Hum_to_fur_line_VA();
            }
            
        }
        else if (feat_Vis == true && AvaXT_Sit != null && AvaXT_Sit.activeSelf == true)
        {
            FeatureVisualization(HumX, AvaY);
            FeatureVisualization(AvaXT_Sit, HumY);

            if (SP)
            {
                fur_script.Hum_to_fur_line();
            }
            if (VA)
            {
                fur_script.Hum_to_fur_line_VA();
            }
        }

        #endregion FeatureVisualization
        

        /////////10 negative samples near user data
        if (Input.GetKeyDown(KeyCode.Z))
        {
            comSample = true;
            zzz = 1;
            yyy = 13;
        }
        AllCloseNegativeSamples();

        if (Input.GetKeyDown(KeyCode.A))
        {
            comSample_48 = true;
            zzz = 1;
            yyy = 37;
        }
        AllCloseNegativeSamples_48();

        /////////10 positive user sample + 100 random negative samples for ranksvm
        if (Input.GetKeyDown(KeyCode.Slash))
        {
            deep_pos = true;
            zzz = 1;
            yyy = 13;
        }
        PositiveAndRandomNegativeSamples_Deep();
        
        if (Input.GetKeyDown(KeyCode.Quote))
        {
            PosRNeg = true;
            zzz = 1;
            yyy = 37;
        }
        PositiveAndRandomNegativeSamples_Deep_48();
        
        ///////////////CMC
        if (Input.GetKeyDown(KeyCode.D))
        {
            cmc_old = true;
            zzz = 1;
            yyy = 13;
            Debug.Log("Rank_CMC (start) Hour : " + System.DateTime.Now.Hour + " Minute: " + System.DateTime.Now.Minute + " Second: " + System.DateTime.Now.Second + " Millisecond: " + System.DateTime.Now.Millisecond);
        }
        CMC_OldData();

        if (Input.GetKeyDown(KeyCode.E))
        {
            cmc_new = true;
            zzz = 1;
            yyy = 37;
            Debug.Log("Rank_CMC (start) Hour : " + System.DateTime.Now.Hour + " Minute: " + System.DateTime.Now.Minute + " Second: " + System.DateTime.Now.Second + " Millisecond: " + System.DateTime.Now.Millisecond);
        }
        CMC_NewData();

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////Placement!!
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            heatmap = false;
            similarity = true;
            //placement = true;
            //similarity_svm = new float[Map_R_width, Map_R_height, numSec];
            //Debug.Log(Map_R_height * Map_R_width * numSec + 10);
            Debug.Log("Map_R_height : " + Map_R_height + "Map_R_width : " + Map_R_width + "numSec : " + numSec);
            Debug.Log("Similarity calculation(start) Hour : " + System.DateTime.Now.Hour + " Minute: " + System.DateTime.Now.Minute + " Second: " + System.DateTime.Now.Second + " Millisecond: " + System.DateTime.Now.Millisecond);
            pxw = 0;
            pyh = 0;
            pzs = 0;
            timestamp = 0f;
            feat_count = 0;
            lowestSim = 0f;
            sIndex = new SimIndex[Map_R_height * Map_R_width * numSec + 10];
            //humXfeat = FeatureValues(HumX, AvaY, humXfeat);
            //HumX44 = Feat_fromX(HumX, AvaY, 0);

        }
        PlacementByAlgorithm();


//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////HeatMap
        if (Input.GetKeyDown(KeyCode.V))
        {
            if(heatmap==false)
            {
                heatmap = true;
            }
            else
            {
                heatmap = false;
            }
        }
        if (heatmap && Map_R != null)
        {
            HeatMapVis();
        }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////Feature value check
        if (Input.GetKeyDown(KeyCode.F))
        {
           RecallAvaX_U(); //AvaX_U_1, AvaX_U_2, AvaX_U_3, AvaX_U_4....
                            // GameObject array for 10 user selected avatar
            AvaX_U[0] = AvaX_U_1;
            AvaX_U[1] = AvaX_U_2;
            AvaX_U[2] = AvaX_U_3;
            AvaX_U[3] = AvaX_U_4;
            AvaX_U[4] = AvaX_U_5;
            AvaX_U[5] = AvaX_U_6;
            AvaX_U[6] = AvaX_U_7;
            AvaX_U[7] = AvaX_U_8;
            AvaX_U[8] = AvaX_U_9;
            AvaX_U[9] = AvaX_U_10;
            
            float[] data = null;
            float[] sample_input = new float[90];
            float[] humanX = new float[45];
            float[] avatarX = new float[45];
            //Debug.Log("HumX ----------------------------------");

            Debug.Log("AvaX_Pos ----------------------------------");
            for (int i=0;i<10;i++)
            {
                HumX45 = Feat_fromX(HumX, AvaY, 0);

                for (int m = 0; m < 45; m++)
                {
                    sample_input[m] = HumX45[m];
                    humanX[m] = HumX45[m];
                    //Debug.Log("HumX" + (i + 1) + ":" + (m+1) + " : " + sample_input[m]);
                    //Debug.Log("Sample_input"  + (m+1) + " : " + sample_input[m]);

                }

                AvaX45 = Feat_fromX(AvaX_U[i], HumY, 1);
                //Debug.Log("AvaX " + (i + 1) + "----------------------------------");

                for (int m = 0; m < 45; m++)
                {
                    sample_input[45 + m] = AvaX45[m];
                    avatarX[m] = AvaX45[m];
                    //Debug.Log("AvaX"+(i+1) +":"+(m+1) + " : " + sample_input[m+44]);
                    //Debug.Log("Sample_input" + (m + 45) + " : " + sample_input[m + 45]);
                }
                /*
                float MKML_dist = MKML_distance(humanX, avatarX, Mat_Lab);
                Debug.Log("Ava_U" + (i + 1) + " : " + MKML_dist);
                */
                
                data = telp_script.clientSocketHandler.getData(FRAME_BUFFER_SIZE, sample_input);
                //
                if (data != null && data.Length == DIM_FEATURE * 2)
                {
                    
                    //Debug.Log ("Distance: " + (data[0]).ToString());
                    Debug.Log("Ava_U"+(i+1)+string.Format(" value: {0:F6}", data[0]));
                }
                
            }
            //Debug.Log("AvaX_Neg---------------------------------------------------------");
            /*
            int count = 0;
            while (count < 100)
            {
                AvaX.transform.position = new Vector3(Random.Range(Map_R[0, 0, 0].xCoord, Map_R[Map_R_width - 1, Map_R_height - 1, 0].xCoord), 0f, Random.Range(Map_R[0, 0, 0].zCoord, Map_R[Map_R_width - 1, Map_R_height - 1, 0].zCoord));
                AvaX.transform.eulerAngles = new Vector3(AvaX.transform.eulerAngles.x, Random.Range(0f, 360f), AvaX.transform.eulerAngles.z);
                feat7d = Dist_Feat_6d(HumX, AvaY, AvaX, HumY);
                if (!(feat7d[0] < 0.2f && feat7d[1] < 0.1f && feat7d[2] < 0.2f && feat7d[3] < 0.7f && feat7d[4] < 0.4f && feat7d[5] < 0.7f))
                {
                    AvaX_N_clone[count] = Instantiate(AvaX_N_prefab, AvaX.transform.position, Quaternion.Euler(AvaX.transform.eulerAngles)) as GameObject;
                    AvaX44 = Feat_fromX(AvaX, HumY, 1);
                    for (int m = 0; m < 44; m++)
                    {
                        sample_input[44 + m] = AvaX44[m];
                    }
                    count++;
                    data = telp_script.clientSocketHandler.getData(FRAME_BUFFER_SIZE, sample_input);
                    //
                    if (data != null && data.Length == DIM_FEATURE * 2)
                    {
                        //Debug.Log ("Distance: " + (data[0]).ToString());
                        Debug.Log(string.Format("value: {0:F6}", data[0]));
                    }

                }
            }
            */
            //AvaX_N_clone_1 = NsampleNearUserdata(AvaX_U, HumY); // Function that generate samples

            Debug.Log("---------------------------------------------------------");
            /*
            for (int i = 0; i < 10; i++)
            {

                feat7d = Dist_Feat_6d(HumX, AvaY, AvaX_U[i], HumY);
                Debug.Log("Distance differnce : " + feat7d[1]);
            }
            */
        }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Destroy 10 (game objects) generated avatars for closely negative samples
        if(Input.GetKeyDown(KeyCode.G))
        {
            if (AvaX_N_clone[0] != null)
            {
                for (int i = 0; i < 10; i++)
                {
                    Destroy(AvaX_N_clone[i]);

                }
            }
        }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Similarity value between HumX and clicked AvaX
        if (Input.GetKeyDown(KeyCode.H))
        {
            //HumX44 = Feat_fromX(HumX, AvaY, 0);
            
            float[] data = null;
            float[] sample_input = new float[170];
            float[] humanX = new float[85];
            float[] avatarX = new float[85];
            Debug.Log("Clicked avaX ----------------------------------");
            float[] HumX85 = Feat_fromX(HumX, AvaY, 0);
            for (int m = 0; m < 85; m++)
            {
                sample_input[m] = HumX85[m];
                humanX[m] = HumX85[m];
                //Debug.Log("HumX"+(m+1) + " : " + sample_input[m]);
                //Debug.Log("Sample_input" + (m + 1) + " : " + sample_input[m]);

            }
            Debug.Log("----------------------------------");
            float[] AvaX85 = Feat_fromX(AvaX, HumY, 1);
            for (int m = 0; m < 85; m++)
            {
                sample_input[85 + m] = AvaX85[m];
                avatarX[m] = AvaX85[m];
                //Debug.Log("AvaX"+(m+1) + " : " + sample_input[m+45]);
                //Debug.Log("Sample_input" + (m + 46) + " : " + sample_input[m + 45]);
            }

            for (int m = 0; m < 85; m++)
            {
                if (m == 0) Debug.Log("Pose affordance------------------------------------");
                if (m == 17) Debug.Log("Interpersonal-------------------------------------");
                if (m == 20) Debug.Log("Area Code---------------------------------------");
                if (m == 26) Debug.Log("Behavior-------------------------------");
                if (m == 36) Debug.Log("Fur.num--------------------------------");
                if (m == 48) Debug.Log("Fur.dist-------------------------------");
                if (m == 60) Debug.Log("Fur.ang--------------------------------");
                if (m == 72) Debug.Log("Visual attention-----------------------");
                Debug.Log( m  + "||  " + sample_input[m] + "     ||  " + sample_input[m + 85]);

            }


            //data = telp_script.clientSocketHandler.getData(FRAME_BUFFER_SIZE, sample_input);
            //Debug.Log("----------------------------------");
            if (data != null && data.Length == DIM_FEATURE * 2)
            {

                //Debug.Log ("Distance: " + (data[0]).ToString());
                Debug.Log(string.Format("value: {0:F6}", data[0]));
            }
            Debug.Log("----------------------------------");

        }
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Consistency check for data
        if (Input.GetKeyDown(KeyCode.R))
        {
            RecallAvaX_U(); //AvaX_U_1, AvaX_U_2, AvaX_U_3, AvaX_U_4....
            // GameObject array for 10 user selected avatar
            AvaX_U[0] = AvaX_U_1;
            AvaX_U[1] = AvaX_U_2;
            AvaX_U[2] = AvaX_U_3;
            AvaX_U[3] = AvaX_U_4;
            AvaX_U[4] = AvaX_U_5;
            AvaX_U[5] = AvaX_U_6;
            AvaX_U[6] = AvaX_U_7;
            AvaX_U[7] = AvaX_U_8;
            AvaX_U[8] = AvaX_U_9;
            AvaX_U[9] = AvaX_U_10;

            Debug.Log("Consistency measure ----------------------------------" + "Pair" + numPair + "Scene" + numScene );
            float[] temp_distance_array = new float[10];
            float[] temp_angle1_array = new float[10];
            float[] temp_angle2_array = new float[10];
            float[] temp_dissimilarity_array = new float[10];
            float average_distance = 0f;
            float average_angle1 = 0f;
            float average_angle2 = 0f;
            float average_dissimilarity = 0f;

            for (int i = 0; i < 10; i++)
            {
                float min_distance = 100f;
                float min_angle1 = 0f;
                float min_angle2 = 0f;
                int min_j = 0;

                for (int j = 0; j < 10; j++)
                {
                    if ( i != j)
                    {
                        float temp_distance = 0f;
                        float temp_angle_i = 0f;
                        float temp_angle_j = 0f;
                        float temp_angle_difference1 = 0f;
                        float temp_angle_difference2 = 0f;

                        //distance
                        Vector3 temp_vec3 = new Vector3(AvaX_U[i].transform.position.x - AvaX_U[j].transform.position.x, 0f, AvaX_U[i].transform.position.z - AvaX_U[j].transform.position.z);
                        temp_distance = Vector3.Magnitude(temp_vec3);

                        //angle1
                        temp_angle_i = ContAngle(AvaX_U[i].transform.forward, HumY.transform.position - AvaX_U[i].transform.position);
                        temp_angle_i = Mathf.Min(temp_angle_i, (360f - temp_angle_i));
                        temp_angle_j = ContAngle(AvaX_U[j].transform.forward, HumY.transform.position - AvaX_U[j].transform.position);
                        temp_angle_j = Mathf.Min(temp_angle_j, (360f - temp_angle_j));
                        temp_angle_difference1 = Mathf.Abs(temp_angle_i - temp_angle_j);

                        //angle2
                        temp_angle_i = ContAngle(HumY.transform.forward, AvaX_U[i].transform.position - HumY.transform.position);
                        temp_angle_i = Mathf.Min(temp_angle_i, (360f - temp_angle_i));
                        temp_angle_j = ContAngle(HumY.transform.forward, AvaX_U[j].transform.position - HumY.transform.position);
                        temp_angle_j = Mathf.Min(temp_angle_j, (360f - temp_angle_j));
                        temp_angle_difference2 = Mathf.Abs(temp_angle_i - temp_angle_j);

                        if (temp_distance < min_distance)
                        {
                            min_distance = temp_distance;
                            min_angle1 = temp_angle_difference1;
                            min_angle2 = temp_angle_difference2;
                            min_j = j;
                        }
                        //Debug.Log("AvatarX_U" + (i + 1) + " - " + "AvatarX_U" + (j + 1) + " | " + " Distance : " + temp_distance + " Angle1 : " + temp_angle_difference1 + " Angle2 : " + temp_angle_difference2);
                    }
                }

                //normalize
                temp_distance_array[i] = min_distance/5f;
                temp_angle1_array[i] = min_angle1/180f;
                temp_angle2_array[i] = min_angle2/180f;

                //dissmilarity
                temp_dissimilarity_array[i] = Mathf.Sqrt(temp_distance_array[i]* temp_distance_array[i] + temp_angle1_array[i]* temp_angle1_array[i] + temp_angle2_array[i] * temp_angle2_array[i]);
                Debug.Log("AvatarX_U" + (i + 1) + " - " + "AvatarX_U" + (min_j + 1) + " | " + " Distance : " + temp_distance_array[i] + " Angle1: " + temp_angle1_array[i] + " Angle2: " + temp_angle2_array[i] + "  ||  Dissimilarity: " + temp_dissimilarity_array[i]);

                //summation
                average_distance = average_distance + temp_distance_array[i];
                average_angle1 = average_angle1 + temp_angle1_array[i];
                average_angle2 = average_angle2 + temp_angle2_array[i];
                average_dissimilarity = average_dissimilarity + temp_dissimilarity_array[i];
            }
            Debug.Log("---------------------------------------------------------");
            
            //Average
            average_distance = average_distance / 10f;
            average_angle1 = average_angle1 / 10f;
            average_angle2 = average_angle2 / 10f;
            average_dissimilarity = average_dissimilarity / 10f;
            Debug.Log("Average distance : " + average_distance + "  Average angle1 : " + average_angle1 + "  Average angle2 : " + average_angle2 + "  ||  Average dissimilarity : " + average_dissimilarity);

            //variance
            float variance_distance = 0f;
            float variance_angle1 = 0f;
            float variance_angle2 = 0f;
            float variance_dissimilarity = 0f;
            for (int i = 0; i < 10; i++)
            {
                variance_distance = variance_distance + Mathf.Pow((temp_distance_array[i] - average_distance),2f);
                variance_angle1 = variance_angle1 + Mathf.Pow((temp_angle1_array[i] - average_angle1), 2f);
                variance_angle2 = variance_angle2 + Mathf.Pow((temp_angle2_array[i] - average_angle2), 2f);
                variance_dissimilarity = variance_dissimilarity + Mathf.Pow((temp_dissimilarity_array[i] - average_dissimilarity), 2f);
            }
            variance_distance = variance_distance / 9f;
            variance_angle1 = variance_angle1 / 9f;
            variance_angle2 = variance_angle2 / 9f;
            variance_dissimilarity = variance_dissimilarity / 9f;
            Debug.Log("Variance distance : " + variance_distance + "  Variance angle1 : " + variance_angle1 + "  Variance angle2 : " + variance_angle2 + "  ||  Variance dissimilarity : " + variance_dissimilarity);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            consistency_bool = true;
            zzz = 1;
            yyy = 13;
            consistency_count = 0;
        }
        ConsistencyMeasure();

        if (Input.GetKeyDown(KeyCode.U))
        {
            consistency_48_bool = true;
            zzz = 1;
            yyy = 37;
            consistency_count = 0;
        }
        //ConsistencyMeasure_48();
    }
    void OnDrawGizmos()
    {
    }
    void ConsistencyMeasure() //For all data from 13-48, collecting consistent data based on dist/ang differences between user data
    {
        if (consistency_bool)
        {
            if (yyy == 49)
            {
                zzz++;
                pairDD.value = zzz;
                yyy = 13;
                PairChange();
                fur_script.PairToFurnitureMap(numPair);
            }

            if(yyy >= 13 && yyy <= 36)
            {
                numScene = yyy;
                numScene--;
                TestScene(0);
                if (Alldata == true && numScene > 0 && numScene < 37)
                {
                    AllUserData();
                }
                if (adminmode == false && numPair >= 12)
                {
                    ViewScene();
                }
            }
            else if(yyy >= 36 && yyy <= 48)
            {
                numScene = yyy;
                DeactAvatars();
                ViewScene_48();
                if (Alldata == true && numScene > 0 && numScene < 49)
                {
                    AllUserData_48();
                }
            }





            RecallAvaX();
            RecallHumY();
            RecallHumX();
            RecallAvaY();
            RecallAvaX_U(); //AvaX_U_1, AvaX_U_2, AvaX_U_3, AvaX_U_4....
            // GameObject array for 10 user selected avatar
            AvaX_U[0] = AvaX_U_1;
            AvaX_U[1] = AvaX_U_2;
            AvaX_U[2] = AvaX_U_3;
            AvaX_U[3] = AvaX_U_4;
            AvaX_U[4] = AvaX_U_5;
            AvaX_U[5] = AvaX_U_6;
            AvaX_U[6] = AvaX_U_7;
            AvaX_U[7] = AvaX_U_8;
            AvaX_U[8] = AvaX_U_9;
            AvaX_U[9] = AvaX_U_10;

            //Debug.Log("Consistency measure ----------------------------------" + "Pair" + numPair + "Scene" + numScene);
            float[] temp_distance_array = new float[10];
            float[] temp_angle1_array = new float[10];
            float[] temp_angle2_array = new float[10];
            float[] temp_dissimilarity_array = new float[10];
            float average_distance = 0f;
            float average_angle1 = 0f;
            float average_angle2 = 0f;
            float average_dissimilarity = 0f;

            for (int i = 0; i < 10; i++)
            {
                float min_distance = 100f;
                float min_angle1 = 0f;
                float min_angle2 = 0f;
                int min_j = 0;

                for (int j = 0; j < 10; j++)
                {
                    if (i != j)
                    {
                        float temp_distance = 0f;
                        float temp_angle_i = 0f;
                        float temp_angle_j = 0f;
                        float temp_angle_difference1 = 0f;
                        float temp_angle_difference2 = 0f;

                        //distance
                        Vector3 temp_vec3 = new Vector3(AvaX_U[i].transform.position.x - AvaX_U[j].transform.position.x, 0f, AvaX_U[i].transform.position.z - AvaX_U[j].transform.position.z);
                        temp_distance = Vector3.Magnitude(temp_vec3);

                        //angle1
                        temp_angle_i = ContAngle(AvaX_U[i].transform.forward, HumY.transform.position - AvaX_U[i].transform.position);
                        temp_angle_i = Mathf.Min(temp_angle_i, (360f - temp_angle_i));
                        temp_angle_j = ContAngle(AvaX_U[j].transform.forward, HumY.transform.position - AvaX_U[j].transform.position);
                        temp_angle_j = Mathf.Min(temp_angle_j, (360f - temp_angle_j));
                        temp_angle_difference1 = Mathf.Abs(temp_angle_i - temp_angle_j);

                        //angle2
                        temp_angle_i = ContAngle(HumY.transform.forward, AvaX_U[i].transform.position - HumY.transform.position);
                        temp_angle_i = Mathf.Min(temp_angle_i, (360f - temp_angle_i));
                        temp_angle_j = ContAngle(HumY.transform.forward, AvaX_U[j].transform.position - HumY.transform.position);
                        temp_angle_j = Mathf.Min(temp_angle_j, (360f - temp_angle_j));
                        temp_angle_difference2 = Mathf.Abs(temp_angle_i - temp_angle_j);

                        if (temp_distance < min_distance)
                        {
                            min_distance = temp_distance;
                            min_angle1 = temp_angle_difference1;
                            min_angle2 = temp_angle_difference2;
                            min_j = j;
                        }
                        //Debug.Log("AvatarX_U" + (i + 1) + " - " + "AvatarX_U" + (j + 1) + " | " + " Distance : " + temp_distance + " Angle1 : " + temp_angle_difference1 + " Angle2 : " + temp_angle_difference2);
                    }
                }

                //normalize
                temp_distance_array[i] = min_distance / 5f;
                temp_angle1_array[i] = min_angle1 / 180f;
                temp_angle2_array[i] = min_angle2 / 180f;

                //dissmilarity
                temp_dissimilarity_array[i] = Mathf.Sqrt(temp_distance_array[i] * temp_distance_array[i] + temp_angle1_array[i] * temp_angle1_array[i] + temp_angle2_array[i] * temp_angle2_array[i]);
                //Debug.Log("AvatarX_U" + (i + 1) + " - " + "AvatarX_U" + (min_j + 1) + " | " + " Distance : " + temp_distance_array[i] + " Angle1: " + temp_angle1_array[i] + " Angle2: " + temp_angle2_array[i] + "  ||  Dissimilarity: " + temp_dissimilarity_array[i]);

                //summation
                average_distance = average_distance + temp_distance_array[i];
                average_angle1 = average_angle1 + temp_angle1_array[i];
                average_angle2 = average_angle2 + temp_angle2_array[i];
                average_dissimilarity = average_dissimilarity + temp_dissimilarity_array[i];
            }
            //Debug.Log("---------------------------------------------------------");

            //Average
            average_distance = average_distance / 10f;
            average_angle1 = average_angle1 / 10f;
            average_angle2 = average_angle2 / 10f;
            average_dissimilarity = average_dissimilarity / 10f;
            //Debug.Log("Average distance : " + average_distance + "  Average angle1 : " + average_angle1 + "  Average angle2 : " + average_angle2 + "  ||  Average dissimilarity : " + average_dissimilarity);

            //variance
            float variance_distance = 0f;
            float variance_angle1 = 0f;
            float variance_angle2 = 0f;
            float variance_dissimilarity = 0f;
            for (int i = 0; i < 10; i++)
            {
                variance_distance = variance_distance + Mathf.Pow((temp_distance_array[i] - average_distance), 2f);
                variance_angle1 = variance_angle1 + Mathf.Pow((temp_angle1_array[i] - average_angle1), 2f);
                variance_angle2 = variance_angle2 + Mathf.Pow((temp_angle2_array[i] - average_angle2), 2f);
                variance_dissimilarity = variance_dissimilarity + Mathf.Pow((temp_dissimilarity_array[i] - average_dissimilarity), 2f);
            }
            variance_distance = variance_distance / 9f;
            variance_angle1 = variance_angle1 / 9f;
            variance_angle2 = variance_angle2 / 9f;
            variance_dissimilarity = variance_dissimilarity / 9f;
            //Debug.Log("Variance distance : " + variance_distance + "  Variance angle1 : " + variance_angle1 + "  Variance angle2 : " + variance_angle2 + "  ||  Variance dissimilarity : " + variance_dissimilarity);

            //Debug.Log("Average dissimilarity: " + average_dissimilarity + "  ||  Variance dissimilarity : " + variance_dissimilarity);

            string fileName = Application.dataPath + "\\Consistency\\consistency_table.txt";
            FileStream fileStr = new FileStream(@fileName, FileMode.Append, FileAccess.Write);
            StreamWriter sw_x = new StreamWriter(fileStr);
            
            if (average_dissimilarity > 0.2f)
            {
                sw_x.Write(0 + "\t");
                //Debug.Log("Not consistent");
            }
            else
            {
                sw_x.Write(1 + "\t");
                //Debug.Log("Consistent");
                consistency_count++;

            }
            if (yyy == 48 && zzz != 24)
            {
                sw_x.Write("\n");
            }
            sw_x.Close();

            /*
            sw_x.Write(average_dissimilarity + "\t");
            if (yyy==48 && zzz != 24)
            {
                sw_x.Write("\n");
            }
            sw_x.Close();
            
            string fileName1 = Application.dataPath + "\\Consistency\\variance.txt";
            FileStream fileStr1 = new FileStream(@fileName1, FileMode.Append, FileAccess.Write);
            StreamWriter sw_x1 = new StreamWriter(fileStr1);
            sw_x1.Write(variance_dissimilarity + "\t");
            if (yyy == 48 && zzz != 24)
            {
                sw_x1.Write("\n");
            }
            sw_x1.Close();
            */



            yyy++;
            if (zzz == 24 && yyy == 49)
            {
                consistency_bool = false;
                Debug.Log("Consistency count : " + consistency_count);
            }

        }
    }
    /*
    void ConsistencyMeasure()
    {
        if (consistency_bool)
        {
            if (yyy == 37)
            {
                zzz++;
                pairDD.value = zzz;
                yyy = 13;
                PairChange();
                fur_script.PairToFurnitureMap(numPair);
            }

            //numPair = zzz;
            numScene = yyy;
            numScene--;
            TestScene(0);

            //Showing all 10 data of each scene
            if (Alldata == true && numScene > 0 && numScene < 37)
            {
                AllUserData();
            }
            //Each scene data for Pair 12 or greater
            if (adminmode == false && numPair >= 12)
            {
                ViewScene();
            }
            RecallAvaX();
            RecallHumY();
            RecallHumX();
            RecallAvaY();
            //Debug.Log("Pair : " + numPair + " NumScene : " + numScene);

            RecallAvaX_U(); //AvaX_U_1, AvaX_U_2, AvaX_U_3, AvaX_U_4....
            // GameObject array for 10 user selected avatar
            AvaX_U[0] = AvaX_U_1;
            AvaX_U[1] = AvaX_U_2;
            AvaX_U[2] = AvaX_U_3;
            AvaX_U[3] = AvaX_U_4;
            AvaX_U[4] = AvaX_U_5;
            AvaX_U[5] = AvaX_U_6;
            AvaX_U[6] = AvaX_U_7;
            AvaX_U[7] = AvaX_U_8;
            AvaX_U[8] = AvaX_U_9;
            AvaX_U[9] = AvaX_U_10;

            
            float[] temp_distance_array = new float[10];
            float[] temp_angle1_array = new float[10];
            float[] temp_angle2_array = new float[10];
            float[] temp_dissimilarity_array = new float[10];
            float average_distance = 0f;
            float average_angle1 = 0f;
            float average_angle2 = 0f;
            float average_dissimilarity = 0f;

            for (int i = 0; i < 10; i++)
            {
                float min_distance = 100f;
                float min_angle1 = 0f;
                float min_angle2 = 0f;
                int min_j = 0;

                for (int j = 0; j < 10; j++)
                {
                    if (i != j)
                    {
                        float temp_distance = 0f;
                        float temp_angle_i = 0f;
                        float temp_angle_j = 0f;
                        float temp_angle_difference1 = 0f;
                        float temp_angle_difference2 = 0f;

                        //distance
                        Vector3 temp_vec3 = new Vector3(AvaX_U[i].transform.position.x - AvaX_U[j].transform.position.x, 0f, AvaX_U[i].transform.position.z - AvaX_U[j].transform.position.z);
                        temp_distance = Vector3.Magnitude(temp_vec3);

                        //angle1
                        temp_angle_i = ContAngle(AvaX_U[i].transform.forward, HumY.transform.position - AvaX_U[i].transform.position);
                        temp_angle_i = Mathf.Min(temp_angle_i, (360f - temp_angle_i));
                        temp_angle_j = ContAngle(AvaX_U[j].transform.forward, HumY.transform.position - AvaX_U[j].transform.position);
                        temp_angle_j = Mathf.Min(temp_angle_j, (360f - temp_angle_j));
                        temp_angle_difference1 = Mathf.Abs(temp_angle_i - temp_angle_j);

                        //angle2
                        temp_angle_i = ContAngle(HumY.transform.forward, AvaX_U[i].transform.position - HumY.transform.position);
                        temp_angle_i = Mathf.Min(temp_angle_i, (360f - temp_angle_i));
                        temp_angle_j = ContAngle(HumY.transform.forward, AvaX_U[j].transform.position - HumY.transform.position);
                        temp_angle_j = Mathf.Min(temp_angle_j, (360f - temp_angle_j));
                        temp_angle_difference2 = Mathf.Abs(temp_angle_i - temp_angle_j);

                        if (temp_distance < min_distance)
                        {
                            min_distance = temp_distance;
                            min_angle1 = temp_angle_difference1;
                            min_angle2 = temp_angle_difference2;
                            min_j = j;
                        }
                        //Debug.Log("AvatarX_U" + (i + 1) + " - " + "AvatarX_U" + (j + 1) + " | " + " Distance : " + temp_distance + " Angle1 : " + temp_angle_difference1 + " Angle2 : " + temp_angle_difference2);
                    }
                }

                //normalize
                temp_distance_array[i] = min_distance / 5f;
                temp_angle1_array[i] = min_angle1 / 180f;
                temp_angle2_array[i] = min_angle2 / 180f;

                //dissmilarity
                temp_dissimilarity_array[i] = Mathf.Sqrt(temp_distance_array[i] * temp_distance_array[i] + temp_angle1_array[i] * temp_angle1_array[i] + temp_angle2_array[i] * temp_angle2_array[i]);
                //Debug.Log("AvatarX_U" + (i + 1) + " - " + "AvatarX_U" + (min_j + 1) + " | " + " Distance : " + temp_distance_array[i] + " Angle1: " + temp_angle1_array[i] + " Angle2: " + temp_angle2_array[i] + "  ||  Dissimilarity: " + temp_dissimilarity_array[i]);

                //summation
                average_distance = average_distance + temp_distance_array[i];
                average_angle1 = average_angle1 + temp_angle1_array[i];
                average_angle2 = average_angle2 + temp_angle2_array[i];
                average_dissimilarity = average_dissimilarity + temp_dissimilarity_array[i];
            }
            //Debug.Log("---------------------------------------------------------");

            //Average
            average_distance = average_distance / 10f;
            average_angle1 = average_angle1 / 10f;
            average_angle2 = average_angle2 / 10f;
            average_dissimilarity = average_dissimilarity / 10f;
            //Debug.Log("Average distance : " + average_distance + "  Average angle1 : " + average_angle1 + "  Average angle2 : " + average_angle2 + "  ||  Average dissimilarity : " + average_dissimilarity);

            //variance
            float variance_distance = 0f;
            float variance_angle1 = 0f;
            float variance_angle2 = 0f;
            float variance_dissimilarity = 0f;
            for (int i = 0; i < 10; i++)
            {
                variance_distance = variance_distance + Mathf.Pow((temp_distance_array[i] - average_distance), 2f);
                variance_angle1 = variance_angle1 + Mathf.Pow((temp_angle1_array[i] - average_angle1), 2f);
                variance_angle2 = variance_angle2 + Mathf.Pow((temp_angle2_array[i] - average_angle2), 2f);
                variance_dissimilarity = variance_dissimilarity + Mathf.Pow((temp_dissimilarity_array[i] - average_dissimilarity), 2f);
            }
            variance_distance = variance_distance / 9f;
            variance_angle1 = variance_angle1 / 9f;
            variance_angle2 = variance_angle2 / 9f;
            variance_dissimilarity = variance_dissimilarity / 9f;
            //Debug.Log("Variance distance : " + variance_distance + "  Variance angle1 : " + variance_angle1 + "  Variance angle2 : " + variance_angle2 + "  ||  Variance dissimilarity : " + variance_dissimilarity);

           

            string fileName = Application.dataPath + "\\Consistency\\consistency_pair" + numPair.ToString() + ".txt";
            FileStream fileStr = new FileStream(@fileName, FileMode.Append, FileAccess.Write);
            StreamWriter sw_x = new StreamWriter(fileStr);
            if(average_dissimilarity > 0.2f || variance_dissimilarity > 0.02)
            {
                sw_x.Write(0 + "\t");
                //Debug.Log("Consistency measure ----------------------------------" + "Pair" + numPair + "Scene" + numScene);
                //Debug.Log("Not consistent");
                //Debug.Log("Pair" + numPair + "Scene" + numScene + "|    Average dissimilarity: " + average_dissimilarity + "  ||  Variance dissimilarity : " + variance_dissimilarity);
            }
            else
            {
                sw_x.Write(1 + "\t");
                //Debug.Log("Consistent");
                consistency_count++;
            }
            sw_x.Close();
            
            yyy++;
            if (zzz == 24 && yyy == 37)
            {
                consistency_bool = false;
                Debug.Log("Consistency count : " + consistency_count);
            }

        }
    }
    void ConsistencyMeasure_48()
    {
        if (consistency_48_bool)
        {
            if (yyy == 49)
            {
                zzz++;
                pairDD.value = zzz;
                yyy = 37;
                PairChange();
                fur_script.PairToFurnitureMap(numPair);
            }

            //numPair = zzz;
            numScene = yyy;
            DeactAvatars();
            ViewScene_48();
            //Debug.Log("Pair : " + numPair + " NumScene : " + numScene);

            //Showing all 10 data of each scene
            if (Alldata == true && numScene > 0 && numScene < 49)
            {
                AllUserData_48();
            }



            RecallAvaX();
            RecallHumY();
            RecallHumX();
            RecallAvaY();
            RecallAvaX_U(); //AvaX_U_1, AvaX_U_2, AvaX_U_3, AvaX_U_4....
            // GameObject array for 10 user selected avatar
            AvaX_U[0] = AvaX_U_1;
            AvaX_U[1] = AvaX_U_2;
            AvaX_U[2] = AvaX_U_3;
            AvaX_U[3] = AvaX_U_4;
            AvaX_U[4] = AvaX_U_5;
            AvaX_U[5] = AvaX_U_6;
            AvaX_U[6] = AvaX_U_7;
            AvaX_U[7] = AvaX_U_8;
            AvaX_U[8] = AvaX_U_9;
            AvaX_U[9] = AvaX_U_10;

            //Debug.Log("Consistency measure ----------------------------------" + "Pair" + numPair + "Scene" + numScene);
            float[] temp_distance_array = new float[10];
            float[] temp_angle1_array = new float[10];
            float[] temp_angle2_array = new float[10];
            float[] temp_dissimilarity_array = new float[10];
            float average_distance = 0f;
            float average_angle1 = 0f;
            float average_angle2 = 0f;
            float average_dissimilarity = 0f;

            for (int i = 0; i < 10; i++)
            {
                float min_distance = 100f;
                float min_angle1 = 0f;
                float min_angle2 = 0f;
                int min_j = 0;

                for (int j = 0; j < 10; j++)
                {
                    if (i != j)
                    {
                        float temp_distance = 0f;
                        float temp_angle_i = 0f;
                        float temp_angle_j = 0f;
                        float temp_angle_difference1 = 0f;
                        float temp_angle_difference2 = 0f;

                        //distance
                        Vector3 temp_vec3 = new Vector3(AvaX_U[i].transform.position.x - AvaX_U[j].transform.position.x, 0f, AvaX_U[i].transform.position.z - AvaX_U[j].transform.position.z);
                        temp_distance = Vector3.Magnitude(temp_vec3);

                        //angle1
                        temp_angle_i = ContAngle(AvaX_U[i].transform.forward, HumY.transform.position - AvaX_U[i].transform.position);
                        temp_angle_i = Mathf.Min(temp_angle_i, (360f - temp_angle_i));
                        temp_angle_j = ContAngle(AvaX_U[j].transform.forward, HumY.transform.position - AvaX_U[j].transform.position);
                        temp_angle_j = Mathf.Min(temp_angle_j, (360f - temp_angle_j));
                        temp_angle_difference1 = Mathf.Abs(temp_angle_i - temp_angle_j);

                        //angle2
                        temp_angle_i = ContAngle(HumY.transform.forward, AvaX_U[i].transform.position - HumY.transform.position);
                        temp_angle_i = Mathf.Min(temp_angle_i, (360f - temp_angle_i));
                        temp_angle_j = ContAngle(HumY.transform.forward, AvaX_U[j].transform.position - HumY.transform.position);
                        temp_angle_j = Mathf.Min(temp_angle_j, (360f - temp_angle_j));
                        temp_angle_difference2 = Mathf.Abs(temp_angle_i - temp_angle_j);

                        if (temp_distance < min_distance)
                        {
                            min_distance = temp_distance;
                            min_angle1 = temp_angle_difference1;
                            min_angle2 = temp_angle_difference2;
                            min_j = j;
                        }
                        //Debug.Log("AvatarX_U" + (i + 1) + " - " + "AvatarX_U" + (j + 1) + " | " + " Distance : " + temp_distance + " Angle1 : " + temp_angle_difference1 + " Angle2 : " + temp_angle_difference2);
                    }
                }

                //normalize
                temp_distance_array[i] = min_distance / 5f;
                temp_angle1_array[i] = min_angle1 / 180f;
                temp_angle2_array[i] = min_angle2 / 180f;

                //dissmilarity
                temp_dissimilarity_array[i] = Mathf.Sqrt(temp_distance_array[i] * temp_distance_array[i] + temp_angle1_array[i] * temp_angle1_array[i] + temp_angle2_array[i] * temp_angle2_array[i]);
                //Debug.Log("AvatarX_U" + (i + 1) + " - " + "AvatarX_U" + (min_j + 1) + " | " + " Distance : " + temp_distance_array[i] + " Angle1: " + temp_angle1_array[i] + " Angle2: " + temp_angle2_array[i] + "  ||  Dissimilarity: " + temp_dissimilarity_array[i]);

                //summation
                average_distance = average_distance + temp_distance_array[i];
                average_angle1 = average_angle1 + temp_angle1_array[i];
                average_angle2 = average_angle2 + temp_angle2_array[i];
                average_dissimilarity = average_dissimilarity + temp_dissimilarity_array[i];
            }
            //Debug.Log("---------------------------------------------------------");

            //Average
            average_distance = average_distance / 10f;
            average_angle1 = average_angle1 / 10f;
            average_angle2 = average_angle2 / 10f;
            average_dissimilarity = average_dissimilarity / 10f;
            //Debug.Log("Average distance : " + average_distance + "  Average angle1 : " + average_angle1 + "  Average angle2 : " + average_angle2 + "  ||  Average dissimilarity : " + average_dissimilarity);

            //variance
            float variance_distance = 0f;
            float variance_angle1 = 0f;
            float variance_angle2 = 0f;
            float variance_dissimilarity = 0f;
            for (int i = 0; i < 10; i++)
            {
                variance_distance = variance_distance + Mathf.Pow((temp_distance_array[i] - average_distance), 2f);
                variance_angle1 = variance_angle1 + Mathf.Pow((temp_angle1_array[i] - average_angle1), 2f);
                variance_angle2 = variance_angle2 + Mathf.Pow((temp_angle2_array[i] - average_angle2), 2f);
                variance_dissimilarity = variance_dissimilarity + Mathf.Pow((temp_dissimilarity_array[i] - average_dissimilarity), 2f);
            }
            variance_distance = variance_distance / 9f;
            variance_angle1 = variance_angle1 / 9f;
            variance_angle2 = variance_angle2 / 9f;
            variance_dissimilarity = variance_dissimilarity / 9f;
            //Debug.Log("Variance distance : " + variance_distance + "  Variance angle1 : " + variance_angle1 + "  Variance angle2 : " + variance_angle2 + "  ||  Variance dissimilarity : " + variance_dissimilarity);

            //Debug.Log("Average dissimilarity: " + average_dissimilarity + "  ||  Variance dissimilarity : " + variance_dissimilarity);

            string fileName = Application.dataPath + "\\Consistency\\consistency_pair" + numPair.ToString() + ".txt";
            FileStream fileStr = new FileStream(@fileName, FileMode.Append, FileAccess.Write);
            StreamWriter sw_x = new StreamWriter(fileStr);
            if (average_dissimilarity > 0.2f || variance_dissimilarity > 0.02)
            {
                sw_x.Write(0 + "\t");
                //Debug.Log("Not consistent");
            }
            else
            {
                sw_x.Write(1 + "\t");
                //Debug.Log("Consistent");
                consistency_count++;

            }
            sw_x.Close();

            yyy++;
            if (zzz == 24 && yyy == 49)
            {
                consistency_48_bool = false;
                Debug.Log("Consistency count : " + consistency_count);
            }

        }
    }
    */
    void PositiveAndRandomNegativeSamples_Deep()
    {
        if (deep_pos)
        {
            if (yyy == 37)
            {
                zzz++;
                pairDD.value = zzz;
                yyy = 13;
                PairChange();
                fur_script.PairToFurnitureMap(numPair);
            }

            //numPair = zzz;
            numScene = yyy;
            numScene--;
            TestScene(0);

            //Showing all 10 data of each scene
            if (Alldata == true && numScene > 0 && numScene < 37)
            {
                AllUserData();
            }
            //Each scene data for Pair 12 or greater
            if (adminmode == false && numPair >= 12)
            {
                ViewScene();
            }
            RecallAvaX();
            RecallHumY();
            RecallHumX();
            RecallAvaY();
            Debug.Log(numPair + "_" + numScene + "_a");


            string fileName = Application.dataPath + "\\Deep_feature\\FBSMap\\feature_x_" + numPair.ToString() + ".txt";
            FileStream fileStr = new FileStream(@fileName, FileMode.Append, FileAccess.Write);
            StreamWriter sw_x = new StreamWriter(fileStr);

            float[] feat85d = new float[85];
            feat85d = Feat_fromX(HumX, AvaY, 0);

            for (int i = 0; i < 110; i++)
            {
                for (int j = 0; j < 85; j++)
                {
                    if (j < 84)
                    {
                        //Debug.Log(j);
                        sw_x.Write(feat85d[j] + "\t");
                    }
                    else
                    {
                        sw_x.Write(feat85d[j] + "\n");
                    }
                }
            }

            sw_x.Close();


            fileName = Application.dataPath + "\\Deep_feature\\FBSMap\\feature_x_pos" + numPair.ToString() + ".txt";
            fileStr = new FileStream(@fileName, FileMode.Append, FileAccess.Write);
            StreamWriter sw_x_pos = new StreamWriter(fileStr);

            RecallAvaX_U(); //AvaX_U_1, AvaX_U_2, AvaX_U_3, AvaX_U_4....
                            // GameObject array for 10 user selected avatar
            AvaX_U[0] = AvaX_U_1;
            AvaX_U[1] = AvaX_U_2;
            AvaX_U[2] = AvaX_U_3;
            AvaX_U[3] = AvaX_U_4;
            AvaX_U[4] = AvaX_U_5;
            AvaX_U[5] = AvaX_U_6;
            AvaX_U[6] = AvaX_U_7;
            AvaX_U[7] = AvaX_U_8;
            AvaX_U[8] = AvaX_U_9;
            AvaX_U[9] = AvaX_U_10;
            for (int h = 0; h < 11; h++)
            {
                for (int i = 0; i < 10; i++)
                {
                    feat85d = Feat_fromX(AvaX_U[i], HumY, 1);
                    //Debug.Log("Count : " + h + " Pair : " + numPair + " Scene : " + numScene + " Ava " + i);
                    for (int j = 0; j < 85; j++)
                    {
                        if (j < 84)
                        {
                            sw_x_pos.Write(feat85d[j] + "\t");
                        }
                        else
                        {
                            sw_x_pos.Write(feat85d[j] + "\n");
                        }
                    }
                }
            }

            sw_x_pos.Close();

            //Debug.Log(numPair + "_" + numScene + "_b");

            //fileName = Application.dataPath + "\\Metric\\ranksvm\\qid\\ranksvm_" + numPair.ToString() + "_" + numScene + "b.txt";
            /*
            fileName = Application.dataPath + "\\Metric\\ranksvm\\NoDistance134567\\ranksvm_NoDis_" + numPair.ToString() + ".txt";
            fileStr = new FileStream(@fileName, FileMode.Append, FileAccess.Write);
            sw = new StreamWriter(fileStr);

            fileName = Application.dataPath + "\\Metric\\ranksvm\\Spatial1567\\ranksvm_Spatial_" + numPair.ToString() + ".txt";
            fileStr = new FileStream(@fileName, FileMode.Append, FileAccess.Write);
            sw_2 = new StreamWriter(fileStr);
            */

            fileName = Application.dataPath + "\\Deep_feature\\FBSMap\\feature_x_neg" + numPair.ToString() + ".txt";
            fileStr = new FileStream(@fileName, FileMode.Append, FileAccess.Write);
            StreamWriter sw_ranneg = new StreamWriter(fileStr);

            int count = 0;

            while (count < 100)
            {
                AvaX.transform.position = new Vector3(Random.Range(Map_R[0, 0, 0].xCoord, Map_R[Map_R_width - 1, Map_R_height - 1, 0].xCoord), 0f, Random.Range(Map_R[0, 0, 0].zCoord, Map_R[Map_R_width - 1, Map_R_height - 1, 0].zCoord));
                AvaX.transform.eulerAngles = new Vector3(AvaX.transform.eulerAngles.x, Random.Range(0f, 360f), AvaX.transform.eulerAngles.z);
                feat7d = Dist_Feat_6d(HumX, AvaY, AvaX, HumY);
                if (!(feat7d[0] < 0.2f && feat7d[1] < 0.1f && feat7d[2] < 0.2f && feat7d[3] < 0.7f && feat7d[4] < 0.4f && feat7d[5] < 0.7f))
                {
                    AvaX_N_clone[count] = Instantiate(AvaX_N_prefab, AvaX.transform.position, Quaternion.Euler(AvaX.transform.eulerAngles)) as GameObject;
                    feat85d = Feat_fromX(AvaX, HumY, 1);
                    for (int j = 0; j < 85; j++)
                    {
                        if (j < 84)
                        {
                            sw_ranneg.Write(feat85d[j] + "\t");
                        }
                        else
                        {
                            sw_ranneg.Write(feat85d[j] + "\n");
                        }
                    }
                    count++;

                }
            }

            sw_ranneg.Close();
            if (AvaX_N_clone[0] != null)
            {
                for (int i = 0; i < 100; i++)
                {
                    Destroy(AvaX_N_clone[i]);

                }
            }

            AvaX_N_clone_1 = NsampleNearUserdata(AvaX_U, HumY); // Function that generate samples
            if (AvaX_N_clone_1[0] != null)
            {
                for (int i = 0; i < 10; i++)
                {
                    Destroy(AvaX_N_clone_1[i]);

                }
            }

            yyy++;
            if (zzz == 24 && yyy == 37)
            {
                deep_pos = false;
            }

        }
    }
    void PositiveAndRandomNegativeSamples_Deep_48()
    {
        if (PosRNeg)
        {
            if (yyy == 49)
            {
                zzz++;
                pairDD.value = zzz;
                yyy = 37;
                PairChange();
                fur_script.PairToFurnitureMap(numPair);
            }

            //numPair = zzz;
            numScene = yyy;
            DeactAvatars();
            ViewScene_48();
            Debug.Log("Pair : " + numPair + " NumScene : " + numScene);

            //Showing all 10 data of each scene
            if (Alldata == true && numScene > 0 && numScene < 49)
            {
                AllUserData_48();
            }



            RecallAvaX();
            RecallHumY();
            RecallHumX();
            RecallAvaY();
            //Debug.Log(numPair + "_" + numScene + "_a");


            string fileName = Application.dataPath + "\\Deep_feature\\FBSMap\\feature_x_" + numPair.ToString() + ".txt";
            FileStream fileStr = new FileStream(@fileName, FileMode.Append, FileAccess.Write);
            StreamWriter sw_x = new StreamWriter(fileStr);

            float[] feat85d = new float[85];
            feat85d = Feat_fromX(HumX, AvaY, 0);

            for (int i = 0; i < 110; i++)
            {
                for (int j = 0; j < 85; j++)
                {
                    if (j < 84)
                    {
                        //Debug.Log(j);
                        sw_x.Write(feat85d[j] + "\t");
                    }
                    else
                    {
                        sw_x.Write(feat85d[j] + "\n");
                    }
                }
            }

            sw_x.Close();


            fileName = Application.dataPath + "\\Deep_feature\\FBSMap\\feature_x_pos" + numPair.ToString() + ".txt";
            fileStr = new FileStream(@fileName, FileMode.Append, FileAccess.Write);
            StreamWriter sw_x_pos = new StreamWriter(fileStr);

            RecallAvaX_U(); //AvaX_U_1, AvaX_U_2, AvaX_U_3, AvaX_U_4....
                            // GameObject array for 10 user selected avatar
            AvaX_U[0] = AvaX_U_1;
            AvaX_U[1] = AvaX_U_2;
            AvaX_U[2] = AvaX_U_3;
            AvaX_U[3] = AvaX_U_4;
            AvaX_U[4] = AvaX_U_5;
            AvaX_U[5] = AvaX_U_6;
            AvaX_U[6] = AvaX_U_7;
            AvaX_U[7] = AvaX_U_8;
            AvaX_U[8] = AvaX_U_9;
            AvaX_U[9] = AvaX_U_10;
            for (int h = 0; h < 11; h++)
            {
                for (int i = 0; i < 10; i++)
                {
                    feat85d = Feat_fromX(AvaX_U[i], HumY, 1);
                    //Debug.Log("Count : " + h + " Pair : " + numPair + " Scene : " + numScene + " Ava " + i);
                    for (int j = 0; j < 85; j++)
                    {
                        if (j < 84)
                        {
                            sw_x_pos.Write(feat85d[j] + "\t");
                        }
                        else
                        {
                            sw_x_pos.Write(feat85d[j] + "\n");
                        }
                    }
                }
            }

            sw_x_pos.Close();

            //Debug.Log(numPair + "_" + numScene + "_b");

            //fileName = Application.dataPath + "\\Metric\\ranksvm\\qid\\ranksvm_" + numPair.ToString() + "_" + numScene + "b.txt";
            /*
            fileName = Application.dataPath + "\\Metric\\ranksvm\\NoDistance134567\\ranksvm_NoDis_" + numPair.ToString() + ".txt";
            fileStr = new FileStream(@fileName, FileMode.Append, FileAccess.Write);
            sw = new StreamWriter(fileStr);

            fileName = Application.dataPath + "\\Metric\\ranksvm\\Spatial1567\\ranksvm_Spatial_" + numPair.ToString() + ".txt";
            fileStr = new FileStream(@fileName, FileMode.Append, FileAccess.Write);
            sw_2 = new StreamWriter(fileStr);
            */

            fileName = Application.dataPath + "\\Deep_feature\\FBSMap\\feature_x_neg" + numPair.ToString() + ".txt";
            fileStr = new FileStream(@fileName, FileMode.Append, FileAccess.Write);
            StreamWriter sw_ranneg = new StreamWriter(fileStr);

            int count = 0;

            while (count < 100)
            {
                AvaX.transform.position = new Vector3(Random.Range(Map_R[0, 0, 0].xCoord, Map_R[Map_R_width - 1, Map_R_height - 1, 0].xCoord), 0f, Random.Range(Map_R[0, 0, 0].zCoord, Map_R[Map_R_width - 1, Map_R_height - 1, 0].zCoord));
                AvaX.transform.eulerAngles = new Vector3(AvaX.transform.eulerAngles.x, Random.Range(0f, 360f), AvaX.transform.eulerAngles.z);
                feat7d = Dist_Feat_6d(HumX, AvaY, AvaX, HumY);
                if (!(feat7d[0] < 0.2f && feat7d[1] < 0.1f && feat7d[2] < 0.2f && feat7d[3] < 0.7f && feat7d[4] < 0.4f && feat7d[5] < 0.7f))
                {
                    AvaX_N_clone[count] = Instantiate(AvaX_N_prefab, AvaX.transform.position, Quaternion.Euler(AvaX.transform.eulerAngles)) as GameObject;
                    feat85d = Feat_fromX(AvaX, HumY, 1);
                    for (int j = 0; j < 85; j++)
                    {
                        if (j < 84)
                        {
                            sw_ranneg.Write(feat85d[j] + "\t");
                        }
                        else
                        {
                            sw_ranneg.Write(feat85d[j] + "\n");
                        }
                    }
                    count++;

                }
            }

            sw_ranneg.Close();
            if (AvaX_N_clone[0] != null)
            {
                for (int i = 0; i < 100; i++)
                {
                    Destroy(AvaX_N_clone[i]);

                }
            }

            AvaX_N_clone_1 = NsampleNearUserdata(AvaX_U, HumY); // Function that generate samples
            if (AvaX_N_clone_1[0] != null)
            {
                for (int i = 0; i < 10; i++)
                {
                    Destroy(AvaX_N_clone_1[i]);

                }
            }

            yyy++;
            if (zzz == 24 && yyy == 49)
            {
                PosRNeg = false;
            }

        }
    }
    float[] Feat_fromX(GameObject FromX, GameObject ToY, int HumOrAva)
    {


        fromXfeat = FeatureValues(FromX, ToY, fromXfeat);

        //Pose Affordance
        //Feat41[0]-Feat41[16]
        feat_fromX[0] = fromXfeat[0] / 1000f;
        for (int i = 0; i < 16; i++)
        {
            if (i == 0)
            {
                feat_fromX[i + 1] = (0.25f * fromXfeat[16] + 0.5f * fromXfeat[i + 1] + 0.25f * fromXfeat[i + 2]) / 1000f;
            }

            feat_fromX[i + 1] = (0.25f * fromXfeat[i] + 0.5f * fromXfeat[i + 1] + 0.25f * fromXfeat[i + 2]) / 1000f;

            if (i == 15)

            {
                feat_fromX[i + 1] = (0.25f * fromXfeat[i] + 0.5f * fromXfeat[i + 1] + 0.25f * fromXfeat[1]) / 1000f;
            }
        }

        //Distance from X to Y
        //Feat41[17]
        feat_fromX[17] = fromXfeat[17] / 10f;

        //Angle between X front and X to Y
        //Feat41[18]
        feat_fromX[18] = Mathf.Min(fromXfeat[18], (360f - fromXfeat[18])) / 180f;
        feat_fromX[19] = Mathf.Min(fromXfeat[19], (360f - fromXfeat[19])) / 180f;

        //FBSMap area {kitchen(0) or living room(1) or dining area(2) or bedroom(3) or work space(4) or door(5)} 
        feat_fromX[20] = 0f;
        feat_fromX[21] = 0f;
        feat_fromX[22] = 0f;
        feat_fromX[23] = 0f;
        feat_fromX[24] = 0f;
        feat_fromX[25] = 0f;


        RaycastHit hit;
        Vector3 ray_start = new Vector3(0f, 0f, 0f);
        Vector3 ray_dir_down = new Vector3(0f, -1f, 0f);
        ray_start.Set(FromX.transform.position.x, -2f, FromX.transform.position.z);
        if (Physics.Raycast(ray_start, ray_dir_down, out hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "Untagged")
            {
                //Debug.Log("IDK");
            }
            else if(hit.collider.tag == "kitchen")
            {
                feat_fromX[20] = 1f;
                //Debug.Log("kitchen");
            }
            else if (hit.collider.tag == "livingroom")
            {
                feat_fromX[21] = 1f;
                //Debug.Log("livingroom");
            }
            else if (hit.collider.tag == "dining_area")
            {
                feat_fromX[22] = 1f;
                //Debug.Log("dining_area");
            }
            else if (hit.collider.tag == "bedroom")
            {
                feat_fromX[23] = 1f;
                //Debug.Log("bedroom");
            }
            else if (hit.collider.tag == "work_space")
            {
                feat_fromX[24] = 1f;
                //Debug.Log("work_space");
            }
            else if (hit.collider.tag == "door")
            {
                feat_fromX[25] = 1f;
                //Debug.Log("door");
            }
        }









        //Object category frequency nexr X and X'
        //Feat41[19]-Feat41[30]
        float[] object_category_frequency = new float[46];
        if (HumOrAva == 0)
        {
            object_category_frequency = fur_script.Object_category_frequency_HumX(FromX);
        }
        else
        {
            object_category_frequency = fur_script.Object_category_frequency_AvaX(FromX);
        }

        for (int i = 0; i < 46; i++)
        {
            feat_fromX[i + 26] = object_category_frequency[i];
        }

        //Visual attention of X as category frequency
        //Feat41[31]-Feat41[42]
        float[] visual_attention_category_frequency = new float[12];
        if (HumOrAva == 0)
        {
            visual_attention_category_frequency = fur_script.VisualAttention_Hum(FromX);

        }
        else
        {
            visual_attention_category_frequency = fur_script.VisualAttention_Ava(FromX);
        }

        for (int i = 0; i < 12; i++)
        {
            feat_fromX[i + 72] = visual_attention_category_frequency[i];
        }


        //SitOrStand
        //Feat41[43]
        feat_fromX[84] = SitOrStandofX(FromX);


        //skipfornow
        //Dist_feat[5] = fur_script.Free_space_diff(HumX, AvaX);


        return feat_fromX;
    }
    float[] Feat_fromX_old(GameObject FromX, GameObject ToY, int HumOrAva)
    {


        fromXfeat = FeatureValues(FromX, ToY, fromXfeat);

        //Pose Affordance
        //Feat41[0]-Feat41[16]
        feat_fromX[0] = fromXfeat[0] / 1000f;
        for (int i = 0; i < 16; i++)
        {
            if (i == 0)
            {
                feat_fromX[i + 1] = (0.25f * fromXfeat[16] + 0.5f * fromXfeat[i + 1] + 0.25f * fromXfeat[i + 2]) / 1000f;
            }

            feat_fromX[i + 1] = (0.25f * fromXfeat[i] + 0.5f * fromXfeat[i + 1] + 0.25f * fromXfeat[i + 2]) / 1000f;

            if (i == 15)

            {
                feat_fromX[i + 1] = (0.25f * fromXfeat[i] + 0.5f * fromXfeat[i + 1] + 0.25f * fromXfeat[1]) / 1000f;
            }
        }

        //Distance from X to Y
        //Feat41[17]
        feat_fromX[17] = fromXfeat[17] / 10f;

        //Angle between X front and X to Y
        //Feat41[18]
        feat_fromX[18] = Mathf.Min(fromXfeat[18], (360f - fromXfeat[18])) / 180f;
        feat_fromX[19] = Mathf.Min(fromXfeat[19], (360f - fromXfeat[19])) / 180f;
        //Object category frequency nexr X and X'
        //Feat41[19]-Feat41[30]
        float[] object_category_frequency = new float[12];
        if (HumOrAva == 0)
        {
            object_category_frequency = fur_script.Object_category_frequency_HumX(FromX);
        }
        else
        {
            object_category_frequency = fur_script.Object_category_frequency_AvaX(FromX);
        }

        for (int i = 0; i < 12; i++)
        {
            feat_fromX[i + 20] = object_category_frequency[i];
        }

        //Visual attention of X as category frequency
        //Feat41[31]-Feat41[42]
        float[] visual_attention_category_frequency = new float[12];
        if (HumOrAva == 0)
        {
            visual_attention_category_frequency = fur_script.VisualAttention_Hum(FromX);

        }
        else
        {
            visual_attention_category_frequency = fur_script.VisualAttention_Ava(FromX);
        }

        for (int i = 0; i < 12; i++)
        {
            feat_fromX[i + 32] = visual_attention_category_frequency[i];
        }


        //SitOrStand
        //Feat41[43]
        feat_fromX[44] = SitOrStandofX(FromX);


        //skipfornow
        //Dist_feat[5] = fur_script.Free_space_diff(HumX, AvaX);


        return feat_fromX;
    }
    float[] FeatureValues(GameObject FromX, GameObject ToY, float[] basicFeat)
    {
        //Debug.Log("FromX : " + FromX);
        //Debug.Log("ToY : " + ToY);

        //float[]  basicFeat = new float[20];


        EulerAngleY = FromX.transform.eulerAngles.y;

        //////////1.Social feature
        tempVec3 = new Vector3(FromX.transform.position.x - ToY.transform.position.x, 0f, FromX.transform.position.z - ToY.transform.position.z);
        distToY = Vector3.Magnitude(tempVec3);

        basicFeat[17] = distToY;

        angleXwrtY = ContAngle(FromX.transform.forward, ToY.transform.position - FromX.transform.position);
        /*
        if(angleXwrtY>180)
        {
            angleXwrtY = 360 - angleXwrtY;
        }
        */

        //Debug.Log("Angle between the forward direction of X and the position of Y : " + angleXwrtY);
        basicFeat[18] = angleXwrtY;

        angleYwrtX = ContAngle(ToY.transform.forward, FromX.transform.position - ToY.transform.position);

        /*
        if (angleYwrtX > 180)
        {
            angleYwrtX = 360 - angleYwrtX;
        }
        */
        //Debug.Log("Angle between the forward direction of X and the position of Y : " + angleXwrtY);
        basicFeat[19] = angleYwrtX;



        //////////2.Affordance
        RaycastHit hit;

        //Debug.Log("Circular height field----------------------------------------------------------------");
        for (int l = 0; l < 2; l++)
        {
            if (l == 0)
            {
                totHeight = 0.0f;
                for (int m = 0; m < numSampling / 10f; m++)
                {
                    randsqrt = Mathf.Sqrt(Random.Range(0.0f, 1.0f));
                    ray_start.Set(FromX.transform.position.x + 0.25f * (l + randsqrt) * Mathf.Cos(Mathf.PI / 180f * Random.Range(0, 360f)), 2.25f, FromX.transform.position.z + 0.25f * (l + randsqrt) * Mathf.Sin(Mathf.PI / 180f * Random.Range(0, 360f)));
                    if (Physics.Raycast(ray_start, ray_dir_down, out hit, 2f))
                    {
                        totHeight = totHeight + hit.point.y;
                        //Debug.DrawLine(ray_start, hit.point, Color.blue);
                    }
                }
                basicFeat[0] = totHeight * 10f;
                //Debug.Log ("Feat[" + 0 + "] : " + Feat [0]);
            }
            else
            {
                for (int k = 1; k < 17; k++)
                {
                    totHeight = 0.0f;
                    for (int m = 0; m < numSampling / 10f; m++)
                    {
                        randsqrt = Mathf.Sqrt(Random.Range(0.0f, 1.0f));
                        ray_start.Set(FromX.transform.position.x + 0.25f * (l + randsqrt) * Mathf.Cos(Mathf.PI / 180f * Random.Range((22.5F * k - EulerAngleY + 90f) % 360f, (22.5F * (k + 1) - EulerAngleY + 90f) % 360f)), 2.25f, FromX.transform.position.z + 0.25f * (l + randsqrt) * Mathf.Sin(Mathf.PI / 180f * Random.Range((22.5F * k - EulerAngleY + 90f) % 360f, (22.5F * (k + 1) - EulerAngleY + 90f) % 360f)));
                        if (Physics.Raycast(ray_start, ray_dir_down, out hit, 2f))
                        {
                            totHeight = totHeight + hit.point.y;
                            //Debug.DrawLine(ray_start, hit.point, Color.blue);
                        }
                    }
                    //totHeight = totHeight / numSampling  ;
                    //Debug.Log("numSec: "+ k +" level: " + l + " Height: " +	  totHeight);
                    //circCount =  k + numSec * l;
                    basicFeat[k] = totHeight * 10f;
                    //Debug.Log ("Feat[" + k + "] : " + Feat [k]);
                }
            }
        }







        //Debug.Log("Feat[25]" + Feat[25]);


        /*
        //////////3.Space feature
        //Debug.Log("AvatarX Space feature count "+count);
        for (int l = 0; l < 1; l++) // l<L_space
        {
            for (int k = 0; k < 3; k++) // k < numSec
            {
                float bin1, bin2, bin3, bin4, bin5, bin6, bin7;
                totHeight = 0.0f;
                bin1 = 0; bin2 = 0; bin3 = 0; bin4 = 0; bin5 = 0; bin6 = 0; bin7 = 0;
                for (int m = 0; m < numSampling; m++)
                {
                    randsqrt = Mathf.Sqrt(Random.Range(0.0f, 1.0f));
                    ray_start.Set(FromX.transform.position.x + 0.50f * (l + randsqrt) * Mathf.Cos(Mathf.PI / 180f * Random.Range((15.0F * k - EulerAngleY + 90f) % 360f, (15.0F * (k+1) - EulerAngleY + 90f) % 360f)), 2.25f, FromX.transform.position.z + 0.50f * (l + randsqrt) * Mathf.Sin(Mathf.PI / 180f * Random.Range((15.0F * k - EulerAngleY + 90f) % 360f, (15.0F * (k+1) - EulerAngleY + 90f) % 360f)));
                    if (Physics.Raycast(ray_start, ray_dir_down, out hit, 2f))
                    {
                        if (hit.point.y <= 0.5f)
                        {
                            bin2 = bin2 + 1;
                        }
                        if (hit.point.y > 0.5f && hit.point.y <= 1.0f)
                        {
                            bin3 = bin3 + 1;
                        }
                        if (hit.point.y > 1.0f && hit.point.y <= 1.5f)
                        {
                            bin4 = bin4 + 1;
                        }
                        if (hit.point.y > 1.5f && hit.point.y <= 2.0f)
                        {
                            bin5 = bin5 + 1;
                        }
                        if (hit.point.y > 2.0f && hit.point.y <= 2.5f)
                        {
                            bin6 = bin6 + 1;
                        }
                        if (hit.point.y > 2.5f && hit.point.y <= 3.0f)
                        {
                            bin7 = bin7 + 1;
                        }
                    }
                    else
                    {
                        bin1 = bin1 + 1;
                    }
                }
                // 2(soc) + 24(Aff) + 4(level) * 24(sector) * 7(bin) 
                Feat[l * numSec * numBin + k * numBin + 0] = bin1 / numSampling;
                Feat[l * numSec * numBin + k * numBin + 1] = bin2 / numSampling;
                Feat[l * numSec * numBin + k * numBin + 2] = bin3 / numSampling;
                Feat[l * numSec * numBin + k * numBin + 3] = bin4 / numSampling;
                Feat[l * numSec * numBin + k * numBin + 4] = bin5 / numSampling;
                Feat[l * numSec * numBin + k * numBin + 5] = bin6 / numSampling;
                Feat[l * numSec * numBin + k * numBin + 6] = bin7 / numSampling;

                Debug.Log("SFeat[" + (l * numSec * numBin + k * numBin + 0) + "][level : " + l + ", sector : " +  k + "].bin1 : " + Feat[l * numSec * numBin + k * numBin + 0]);
                Debug.Log("SFeat[" + (l * numSec * numBin + k * numBin + 1) + "][level : " + l + ", sector : " +  k + "].bin2 : " + Feat[l * numSec * numBin + k * numBin + 1]);
                Debug.Log("SFeat[" + (l * numSec * numBin + k * numBin + 2) + "][level : " + l + ", sector : " + k + "].bin3 : " + Feat[l * numSec * numBin + k * numBin + 2]);
                Debug.Log("SFeat[" + (l * numSec * numBin + k * numBin + 3) + "][level : " + l + ", sector : " + k + "].bin4 : " + Feat[l * numSec * numBin + k * numBin + 3]);
                Debug.Log("SFeat[" + (l * numSec * numBin + k * numBin + 4) + "][level : " + l + ", sector : " + k + "].bin5 : " + Feat[l * numSec * numBin + k * numBin + 4]);
                Debug.Log("SFeat[" + (l * numSec * numBin + k * numBin + 5) + "][level : " + l + ", sector : " + k + "].bin6 : " + Feat[l * numSec * numBin + k * numBin + 5]);
                Debug.Log("SFeat[" + (l * numSec * numBin + k * numBin + 6) + "][level : " + l + ", sector : " + k + "].bin7 : " + Feat[l * numSec * numBin + k * numBin + 6]);
                Debug.Log("-------------------");

            }
        }
        */
        //Debug.Log("----------------------------------------------------------------------------------------------------------------------------");
        /*////////////4. Isovist
        Debug.Log("Isovist----------------------------------------------------------------");
        int isoCount = socFeatureSize + affordFeatureSize + spatialFeatureSize;
        for (int k = 1; k < 14; k++) {

            ray_start.Set (FromX.transform.position.x, 0.5f, FromX.transform.position.z);
            ray_iso.Set (Mathf.Cos (Mathf.PI / 180f * ((15.0F * (k - 1)-EulerAngleY)%360)), 0, Mathf.Sin (Mathf.PI / 180f * ((15.0F * (k - 1)-EulerAngleY)%360)));
          
            //Debug.Log(isoCount);
            //Debug.Log(k);
            if (Physics.Raycast (ray_start, ray_iso, out hit, 4f)) {
                Debug.DrawLine (ray_start, hit.point, Color.cyan);
                float Dist_Isovist = Vector3.Distance (ray_start, hit.point);
                //Dist_Isovist = Dist_Isovist / 5f / 13f;
                //Debug.Log ("Isovist(" + k + ") : " + Dist_Isovist);
                Feat [isoCount] = Dist_Isovist;
                //Debug.Log ("[" + isoCount + "] : " + Feat [isoCount]);
            } 
            else { 
                float iso_nothit = 4f;
                //Debug.Log("Isovist(" + k + ") : " + iso_nothit);
                Feat [isoCount] = iso_nothit;
                //Debug.Log ("[" + isoCount + "] : " + Feat [isoCount]);
            }
            isoCount = isoCount + 1;
        }
        */
        return basicFeat;

    }

    void PlacementByAlgorithm()
    {


        if (similarity)
        {
            
            while (true)
            {
                if (pzs == 24)
                {
                    pxw++;
                    pzs = 0;
                }
                if (pxw == Map_R_width)
                {
                    pyh++;
                    pxw = 0;
                    break;
                }

                //if (Map_R[pxw, pyh, 0].walkable == true && pyh % 2 == 0 && pxw % 2 == 0 && pzs % 3 == 0)
                if (Map_R[pxw, pyh, 0].walkable == true)
                {
                    //Debug.Log("Height : " + pyh + " Width : " + pxw + " Sector : " + pzs);
                    Vector3 pos = new Vector3(Map_R[pxw, pyh, pzs].xCoord, 0f, Map_R[pxw, pyh, pzs].zCoord);
                    AvaX.transform.position = pos;
                    Vector3 eulerY = new Vector3(0f, pzs * 15f, 0f);
                    AvaX.transform.eulerAngles = eulerY;

                    feat_count++;


                    if (true) //0.06 0.167
                    {
                        
                        float sum = 0f;
                        
                        float[] data = null;
                        float[] sample_input = new float[170];
                        float[] humanX = new float[85];
                        float[] avatarX = new float[85];
                        float[] HumX85 = Feat_fromX(HumX, AvaY, 0);
                        for (int m=0;m<85;m++)
                        {
                            sample_input[m] = HumX85[m];
                            humanX[m] = HumX85[m];
                            //Debug.Log("HumX" + (m + 1) + " : " + sample_input[m]);
                        }
                        float[] AvaX85 = Feat_fromX(AvaX, HumY, 1);
                        for (int m = 0; m < 85; m++)
                        {
                            sample_input[85+m] = AvaX85[m];
                            avatarX[m] = AvaX85[m];
                            //Debug.Log("AvaX" + (m + 1) + " : " + sample_input[m + 45]);
                        }

                        ////float[] sample_input = { 0.3251861f, 0.2550962f, 0.2394775f, 0.1618072f, 0.0660079f, 0.02432691f, 0.01289389f, 0.01212421f, 0.02296004f, 0.0376715f, 0.06881931f, 0.1438747f, 0.2405722f, 0.304278f, 0.2320753f, 0.07234258f, 0.04741869f, 0.1435242f, 0.1371385f, 0.1371385f, 0.2f, 0f, 0f, 0f, 0.1f, 0f, 0f, 0f, 0f, 0f, 0f, 0.2f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 1f, 0.2294327f, 0.05805407f, 0.02046564f, 0.1045128f, 0.2211845f, 0.27195f, 0.2580025f, 0.200035f, 0.1082949f, 0.05355885f, 0.08557549f, 0.1843179f, 0.2626979f, 0.2715189f, 0.1916021f, 0.0626382f, 0.001404463f, 0.1129544f, 0.1483373f, 0.1483373f, 0.5f, 0.3f, 0f, 0.1f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0.1f, 0.1538392f, 0f, 0f, 0f, 0f, 0f, 0f, 0.03705015f, 0f, 0f, 0f, 0.02836293f, 1f };
                        //float[] sample_input = new float[90];
                        //float[] data = null;
                        data = telp_script.clientSocketHandler.getData(FRAME_BUFFER_SIZE, sample_input);
                        //
                        if (data != null && data.Length == DIM_FEATURE * 2)
                        {
                            //Debug.Log ("Distance: " + (data[0]).ToString());
                            //Debug.Log(string.Format("value: {0:F6}", data[0]));
                        }
                        float temp = data[0];
                        sum = temp;
                        
                        /*
                        float MKML_dist = MKML_distance(humanX, avatarX, Mat_Lab);
                        sum = MKML_dist;
                        */
                        
                        /*
                        for (int l = 0; l < 34560; l++)
                        {
                            float dotP = Mathf.Pow((feat7d[0] - SVs[l, 0]), 2) + Mathf.Pow((feat7d[1] - SVs[l, 1]), 2) + Mathf.Pow((feat7d[2] - SVs[l, 2]), 2) + Mathf.Pow((feat7d[3] - SVs[l, 3]), 2) + Mathf.Pow((feat7d[4] - SVs[l, 4]), 2) + Mathf.Pow((feat7d[5] - SVs[l, 5]), 2) + Mathf.Pow((feat7d[6] - SVs[l, 6]), 2);// + Mathf.Pow((feat7d[7] - SVs[l, 7]), 2) + Mathf.Pow((feat7d[8] - SVs[l, 8]), 2);
                            sum = sum + sv_coef[l] * Mathf.Exp(-0.142857f * dotP);
                        }
                        */
                        //Debug.Log("Decision value (end) Hour : " + System.DateTime.Now.Hour + " Minute: " + System.DateTime.Now.Minute + " Second: " + System.DateTime.Now.Second + " Millisecond: " + System.DateTime.Now.Millisecond);
                        //similarity_svm[pxw, pyh, pzs] = (-1f) * sum;
                        //sIndex[pyh * Map_R_width * numSec + pxw * numSec + pzs] = new SimIndex(pxw, pyh, pzs, similarity_svm[pxw, pyh, pzs]);
                        sIndex[pyh * Map_R_width * numSec + pxw * numSec + pzs] = new SimIndex(pxw, pyh, pzs, sum);
                        //Debug.Log("[" + pxw + "," + pyh + "," + pzs + "] " + (-1f) * sum);
                        if(sum >lowestSim)
                        {
                            lowestSim = sum;
                        }
                    }
                    else
                    {
                        sIndex[pyh * Map_R_width * numSec + pxw * numSec + pzs] = new SimIndex(pxw, pyh, pzs, 1000000000000F);
                    }


                }
                else
                {
                    //similarity[j, i, k] = 1000000000000F;
                    //similarity_svm[pxw, pyh, pzs] = 1000000000000F;
                    //sIndex[pyh * Map_R_width * numSec + pxw * numSec + pzs] = new SimIndex(pxw, pyh, pzs, similarity_svm[pxw, pyh, pzs]);
                    sIndex[pyh * Map_R_width * numSec + pxw * numSec + pzs] = new SimIndex(pxw, pyh, pzs, 1000000000000F);
                    //Debug.Log("[" + pxw + "," + pyh + "," + pzs + "] " + 1000000000000F);

                }
                pzs++;
                if (pxw == Map_R_width - 1 && pyh == Map_R_height - 1 && pzs == numSec)
                {
                    similarity = false;
                    placement = true;
                    //Debug.Log("Similarity calculation(end) Hour : " + System.DateTime.Now.Hour + " Minute: " + System.DateTime.Now.Minute + " Second: " + System.DateTime.Now.Second + " Millisecond: " + System.DateTime.Now.Millisecond);
                    //Debug.Log("Feat_count : " + feat_count);
                }
            }
        }
        if (placement)
        {
            //Debug.Log(pyh * Map_R_width * numSec + pxw * numSec + pzs + 1);
            int samp_mult = 1;
            ////////////////////// Avatar_B
            for (int i = 0; i < 10; i++)
            {
                 sIndex[Map_R_height * Map_R_width * numSec + i] = new SimIndex(pxw, pyh, pzs, 1000000000000F);
            }
            var result = sIndex.OrderBy(SimIndex => SimIndex.simValue);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //Actual placement
            int topX = result.ElementAt(0).wIdx;
            int topZ = result.ElementAt(0).hIdx;
            int topS = result.ElementAt(0).sIdx;
            //Debug.Log("Rank1 : "+result.ElementAt(0).simValue);
            //highestSim = 0f; //
            highestSim = result.ElementAt(0).simValue;
            AvaX_1R.SetActive(true);

            Vector3 XTpos = new Vector3(Map_R[topX, topZ, topS].xCoord, 0f, Map_R[topX, topZ, topS].zCoord);
            AvaX_1R.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, topS * 15f, 0f);
            AvaX_1R.transform.eulerAngles = XTeulerY;

            topX = result.ElementAt(1).wIdx;
            topZ = result.ElementAt(1).hIdx;
            topS = result.ElementAt(1).sIdx;
            //Debug.Log("Rank2 : " + result.ElementAt(1).simValue);
            AvaX_2O.SetActive(true);

            XTpos = new Vector3(Map_R[topX, topZ, topS].xCoord, 0f, Map_R[topX, topZ, topS].zCoord);
            AvaX_2O.transform.position = XTpos;
            XTeulerY = new Vector3(0f, topS * 15f, 0f);
            AvaX_2O.transform.eulerAngles = XTeulerY;


            topX = result.ElementAt(2).wIdx;
            topZ = result.ElementAt(2).hIdx;
            topS = result.ElementAt(2).sIdx;
            //Debug.Log("Rank3 : " + result.ElementAt(2).simValue);
            AvaX_3Y.SetActive(true);

            XTpos = new Vector3(Map_R[topX, topZ, topS].xCoord, 0f, Map_R[topX, topZ, topS].zCoord);
            AvaX_3Y.transform.position = XTpos;
            XTeulerY = new Vector3(0f, topS * 15f, 0f);
            AvaX_3Y.transform.eulerAngles = XTeulerY;


            topX = result.ElementAt(3).wIdx;
            topZ = result.ElementAt(3).hIdx;
            topS = result.ElementAt(3).sIdx;
            //Debug.Log("Rank4 : " + result.ElementAt(3).simValue);

            AvaX_4G.SetActive(true);

            XTpos = new Vector3(Map_R[topX, topZ, topS].xCoord, 0f, Map_R[topX, topZ, topS].zCoord);
            AvaX_4G.transform.position = XTpos;
            XTeulerY = new Vector3(0f, topS * 15f, 0f);
            AvaX_4G.transform.eulerAngles = XTeulerY;


            topX = result.ElementAt(4).wIdx;
            topZ = result.ElementAt(4).hIdx;
            topS = result.ElementAt(4).sIdx;
            //Debug.Log("Rank5 : " + result.ElementAt(4).simValue);
            //Debug.Log("-----------------------------------");
            AvaX_5B.SetActive(true);

            XTpos = new Vector3(Map_R[topX, topZ, topS].xCoord, 0f, Map_R[topX, topZ, topS].zCoord);
            AvaX_5B.transform.position = XTpos;
            XTeulerY = new Vector3(0f, topS * 15f, 0f);
            AvaX_5B.transform.eulerAngles = XTeulerY;

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //Debug.Log("Rank_by_sort(start) Hour : " + System.DateTime.Now.Hour + " Minute: " + System.DateTime.Now.Minute + " Second: " + System.DateTime.Now.Second + " Millisecond: " + System.DateTime.Now.Millisecond);
            RecallAvaX_U(); //AvaX_U_1, AvaX_U_2, AvaX_U_3, AvaX_U_4....
            AvaX_U[0] = AvaX_U_1;
            AvaX_U[1] = AvaX_U_2;
            AvaX_U[2] = AvaX_U_3;
            AvaX_U[3] = AvaX_U_4;
            AvaX_U[4] = AvaX_U_5;
            AvaX_U[5] = AvaX_U_6;
            AvaX_U[6] = AvaX_U_7;
            AvaX_U[7] = AvaX_U_8;
            AvaX_U[8] = AvaX_U_9;
            AvaX_U[9] = AvaX_U_10;
            float[] data = null;
            float[] sample_input = new float[170];
            //float[] humanX = new float[45];
            //float[] avatarX = new float[45];
            float[] HumX85 = Feat_fromX(HumX, AvaY, 0);
            for (int m = 0; m < 85; m++)
            {
                sample_input[m] = HumX85[m];
                //humanX[m] = HumX45[m];
            }
            float[] temp_avaU = new float[10];
            for (int i = 0; i < 10; i++)
            {
                float[] AvaX85 = Feat_fromX(AvaX_U[i], HumY, 1);
                for (int m = 0; m < 85; m++)
                {
                    sample_input[85 + m] = AvaX85[m];
                    //avatarX[m] = AvaX45[m];
                }
                data = telp_script.clientSocketHandler.getData(FRAME_BUFFER_SIZE, sample_input);
                temp_avaU[i] = data[0];
                //Debug.Log("Ava_U" + (i + 1) + ": " + data[0]);
                sIndex[pyh * Map_R_width * numSec + pxw * numSec + pzs + i] = new SimIndex(pxw, pyh, pzs, data[0]);
            }

            //var rank = sIndex.OrderBy(SimIndex => SimIndex.simValue).Select
            var rank = sIndex.Select((x, i) => new { OldIndex = i, Value = x.simValue, NewIndex = -1 })
                                  .OrderBy(x => x.Value).Select((x, i) => new { OldIndex = x.OldIndex, Value = x.Value, NewIndex = i + 1 })
                                  .OrderBy(x => x.OldIndex);

            float average_rank = 0f;
            for (int i = 0; i < 10; i++)
            {
                int rank100 = rank.ElementAt(pyh * Map_R_width * numSec + pxw * numSec + pzs + i).NewIndex;
                rank100 = Mathf.CeilToInt((float)rank100 / ((float)feat_count + 10f) * 100f);
                Debug.Log("Ava_U" + (i + 1) + ": " + temp_avaU[i] + " Rank (" + rank.ElementAt(pyh * Map_R_width * numSec + pxw * numSec + pzs + i).NewIndex + ")" + " Rank : " + rank100);
                average_rank = average_rank + rank100;
            }
            Debug.Log("-----------------------------------");
            Debug.Log("Average rank : " + Mathf.CeilToInt(average_rank/10f) + "   Rank sum : " + average_rank);
            
            /*
            Debug.Log(rank.ElementAt(pyh * Map_R_width * numSec + pxw * numSec + pzs).NewIndex);
            Debug.Log(rank.ElementAt(pyh * Map_R_width * numSec + pxw * numSec + pzs + 1).NewIndex);
            Debug.Log(rank.ElementAt(pyh * Map_R_width * numSec + pxw * numSec + pzs + 2).NewIndex);
            Debug.Log(rank.ElementAt(pyh * Map_R_width * numSec + pxw * numSec + pzs + 3).NewIndex);
            Debug.Log(rank.ElementAt(pyh * Map_R_width * numSec + pxw * numSec + pzs + 4).NewIndex);
            Debug.Log(rank.ElementAt(pyh * Map_R_width * numSec + pxw * numSec + pzs + 5).NewIndex);
            Debug.Log(rank.ElementAt(pyh * Map_R_width * numSec + pxw * numSec + pzs + 6).NewIndex);
            Debug.Log(rank.ElementAt(pyh * Map_R_width * numSec + pxw * numSec + pzs + 7).NewIndex);
            Debug.Log(rank.ElementAt(pyh * Map_R_width * numSec + pxw * numSec + pzs + 8).NewIndex);
            Debug.Log(rank.ElementAt(pyh * Map_R_width * numSec + pxw * numSec + pzs + 9).NewIndex);
            Debug.Log("Rank_by_sort(end) Hour : " + System.DateTime.Now.Hour + " Minute: " + System.DateTime.Now.Minute + " Second: " + System.DateTime.Now.Second + " Millisecond: " + System.DateTime.Now.Millisecond);
            */
            /*///////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Debug.Log("-----------------------------------");
            Debug.Log("Rank_by_LEO(start) Hour : " + System.DateTime.Now.Hour + " Minute: " + System.DateTime.Now.Minute + " Second: " + System.DateTime.Now.Second + " Millisecond: " + System.DateTime.Now.Millisecond);
            HumX45 = Feat_fromX(HumX, AvaY, 0);
            for (int m = 0; m < 45; m++)
            {
                sample_input[m] = HumX45[m];
                //humanX[m] = HumX45[m];
            }
            for (int i = 0; i < 10; i++)
            {
                AvaX45 = Feat_fromX(AvaX_U[i], HumY, 1);
                for (int m = 0; m < 45; m++)
                {
                    sample_input[45 + m] = AvaX45[m];
                    //avatarX[m] = AvaX45[m];
                }
                data = telp_script.clientSocketHandler.getData(FRAME_BUFFER_SIZE, sample_input);
                for (int l = 0; l < pyh * Map_R_width * numSec + pxw * numSec + pzs; l++)
                {
                    if (data[0] < result.ElementAt(l).simValue)
                    {
                        Debug.Log("Ava_U" + (i + 1) + ": " + data[0] + " Rank (" + l + ")");
                        break;
                    }
                }
            }
            Debug.Log("Rank_by_LEO(end) Hour : " + System.DateTime.Now.Hour + " Minute: " + System.DateTime.Now.Minute + " Second: " + System.DateTime.Now.Second + " Millisecond: " + System.DateTime.Now.Millisecond);
            Debug.Log("-----------------------------------");
            */
            /*
            RecallAvaX_U(); //AvaX_U_1, AvaX_U_2, AvaX_U_3, AvaX_U_4....
            AvaX_U[0] = AvaX_U_1;
            AvaX_U[1] = AvaX_U_2;
            AvaX_U[2] = AvaX_U_3;
            AvaX_U[3] = AvaX_U_4;
            AvaX_U[4] = AvaX_U_5;
            AvaX_U[5] = AvaX_U_6;
            AvaX_U[6] = AvaX_U_7;
            AvaX_U[7] = AvaX_U_8;
            AvaX_U[8] = AvaX_U_9;
            AvaX_U[9] = AvaX_U_10;
            float[] data = null;
            float[] sample_input = new float[90];
            float[] humanX = new float[45];
            float[] avatarX = new float[45];
            HumX45 = Feat_fromX(HumX, AvaY, 0);
            for (int m = 0; m < 45; m++)
            {
                sample_input[m] = HumX45[m];
                humanX[m] = HumX45[m];
            }
            for (int i = 0; i < 10; i++)
            {
                AvaX45 = Feat_fromX(AvaX_U[i], HumY, 1);
                for (int m = 0; m < 45; m++)
                {
                    sample_input[45 + m] = AvaX45[m];
                    avatarX[m] = AvaX45[m];
                }
                data = telp_script.clientSocketHandler.getData(FRAME_BUFFER_SIZE, sample_input);
                for(int l=0; l< pyh * Map_R_width * numSec + pxw * numSec + pzs;l++)
                {
                    if(data[0] <result.ElementAt(l).simValue)
                    {
                        Debug.Log("Ava_U" + (i + 1) + ": " + data[0] + " Rank (" + l + ")");
                        break;
                    }
                }
            }
            */



            placement = false;
            //heatmap = true;


            //lowestSim = 1f;
            SimDiff = Mathf.Abs(lowestSim - highestSim);
            heatLev = SimDiff / 5f;
            //Debug.Log("Highest Similarity value : " + highestSim);
            //Debug.Log("Lowest Similarity value : " + lowestSim);
            //Debug.Log("Heatmap Level : " + heatLev);
            Debug.Log("Placement--------------------END ");

        }
    }
    void CMC_OldData()
    {
        if (cmc_old)
        {
            if (yyy == 37)
            {
                zzz++;
                pairDD.value = zzz;
                yyy = 13;
                PairChange();
                fur_script.PairToFurnitureMap(numPair);
            }
            //numPair = zzz;
            numScene = yyy;
            numScene--;
            TestScene(0);

            //Showing all 10 data of each scene
            if (Alldata == true && numScene > 0 && numScene < 37)
            {
                AllUserData();
            }
            //Each scene data for Pair 12 or greater
            if (adminmode == false && numPair >= 12)
            {
                ViewScene();
            }
            RecallAvaX();
            RecallHumY();
            RecallHumX();
            RecallAvaY();
            Debug.Log(numPair + "_" + numScene + "_a");

            sIndex = new SimIndex[Map_R_height * Map_R_width * numSec + 10];
            //Debug.Log("Initial value" + (Map_R_height * Map_R_width * numSec + 10));
            float[] data = null;
            float[] sample_input = new float[90];
            pxw = 0;
            pyh = 0;
            pzs = 0;
            timestamp = 0f;
            feat_count = 0;
            lowestSim = 0f;
            int ccount = 0;
            for (int pxw = 0; pxw < Map_R_width; pxw++)
            {
                for (int pyh = 0; pyh < Map_R_height; pyh++)
                {
                    for (int pzs = 0; pzs < numSec; pzs++)
                    {
                        //Debug.Log("Count : " + ccount);
                        ccount++;
                        if (Map_R[pxw, pyh, 0].walkable == true && pyh % 2 == 0 && pxw % 2 == 0 && pzs % 3 == 0)
                        {
                            //Debug.Log("Height : " + pyh + " Width : " + pxw + " Sector : " + pzs);
                            Vector3 pos = new Vector3(Map_R[pxw, pyh, pzs].xCoord, 0f, Map_R[pxw, pyh, pzs].zCoord);
                            AvaX.transform.position = pos;
                            Vector3 eulerY = new Vector3(0f, pzs * 15f, 0f);
                            AvaX.transform.eulerAngles = eulerY;
                            feat_count++;
                            float sum = 0f;

                            float[] humanX = new float[45];
                            float[] avatarX = new float[45];
                            HumX45 = Feat_fromX(HumX, AvaY, 0);
                            for (int m = 0; m < 45; m++)
                            {
                                sample_input[m] = HumX45[m];
                                humanX[m] = HumX45[m];
                                //Debug.Log("HumX" + (m + 1) + " : " + sample_input[m]);
                            }
                            AvaX45 = Feat_fromX(AvaX, HumY, 1);
                            for (int m = 0; m < 45; m++)
                            {
                                sample_input[45 + m] = AvaX45[m];
                                avatarX[m] = AvaX45[m];
                                //Debug.Log("AvaX" + (m + 1) + " : " + sample_input[m + 45]);
                            }
                            data = telp_script.clientSocketHandler.getData(FRAME_BUFFER_SIZE, sample_input);
                            float temp = data[0];
                            sum = temp;
                            sIndex[pyh * Map_R_width * numSec + pxw * numSec + pzs] = new SimIndex(pxw, pyh, pzs, sum);
                            if (sum > lowestSim)
                            {
                                lowestSim = sum;
                            }
                        }
                        else
                        {
                            sIndex[pyh * Map_R_width * numSec + pxw * numSec + pzs] = new SimIndex(pxw, pyh, pzs, 1000000000000F);
                        }

                    }
                }
            }
            Debug.Log("Feat_count : " + feat_count);


            ////////////////////// Avatar_B
            for (int i = 0; i < 10; i++)
            {
                sIndex[Map_R_height * Map_R_width * numSec + i] = new SimIndex(Map_R_width - 1, Map_R_height - 1, numSec, 1000000000000F);
                //Debug.Log("Count : " + (Map_R_height * Map_R_width * numSec + i));
            }
            var result = sIndex.OrderBy(SimIndex => SimIndex.simValue);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //Actual placement
            int topX = result.ElementAt(0).wIdx;
            int topZ = result.ElementAt(0).hIdx;
            int topS = result.ElementAt(0).sIdx;
            //Debug.Log("Rank1 : " + result.ElementAt(0).simValue);
            //highestSim = 0f; //
            highestSim = result.ElementAt(0).simValue;
            AvaX_1R.SetActive(true);

            Vector3 XTpos = new Vector3(Map_R[topX, topZ, topS].xCoord, 0f, Map_R[topX, topZ, topS].zCoord);
            AvaX_1R.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, topS * 15f, 0f);
            AvaX_1R.transform.eulerAngles = XTeulerY;

            topX = result.ElementAt(1).wIdx;
            topZ = result.ElementAt(1).hIdx;
            topS = result.ElementAt(1).sIdx;
            //Debug.Log("Rank2 : " + result.ElementAt(1).simValue);
            AvaX_2O.SetActive(true);

            XTpos = new Vector3(Map_R[topX, topZ, topS].xCoord, 0f, Map_R[topX, topZ, topS].zCoord);
            AvaX_2O.transform.position = XTpos;
            XTeulerY = new Vector3(0f, topS * 15f, 0f);
            AvaX_2O.transform.eulerAngles = XTeulerY;


            topX = result.ElementAt(2).wIdx;
            topZ = result.ElementAt(2).hIdx;
            topS = result.ElementAt(2).sIdx;
            //Debug.Log("Rank3 : " + result.ElementAt(2).simValue);
            AvaX_3Y.SetActive(true);

            XTpos = new Vector3(Map_R[topX, topZ, topS].xCoord, 0f, Map_R[topX, topZ, topS].zCoord);
            AvaX_3Y.transform.position = XTpos;
            XTeulerY = new Vector3(0f, topS * 15f, 0f);
            AvaX_3Y.transform.eulerAngles = XTeulerY;


            topX = result.ElementAt(3).wIdx;
            topZ = result.ElementAt(3).hIdx;
            topS = result.ElementAt(3).sIdx;
            //Debug.Log("Rank4 : " + result.ElementAt(3).simValue);

            AvaX_4G.SetActive(true);

            XTpos = new Vector3(Map_R[topX, topZ, topS].xCoord, 0f, Map_R[topX, topZ, topS].zCoord);
            AvaX_4G.transform.position = XTpos;
            XTeulerY = new Vector3(0f, topS * 15f, 0f);
            AvaX_4G.transform.eulerAngles = XTeulerY;


            topX = result.ElementAt(4).wIdx;
            topZ = result.ElementAt(4).hIdx;
            topS = result.ElementAt(4).sIdx;
            //Debug.Log("Rank5 : " + result.ElementAt(4).simValue);
            Debug.Log("-----------------------------------");
            AvaX_5B.SetActive(true);

            XTpos = new Vector3(Map_R[topX, topZ, topS].xCoord, 0f, Map_R[topX, topZ, topS].zCoord);
            AvaX_5B.transform.position = XTpos;
            XTeulerY = new Vector3(0f, topS * 15f, 0f);
            AvaX_5B.transform.eulerAngles = XTeulerY;

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //Debug.Log("Rank_by_sort(start) Hour : " + System.DateTime.Now.Hour + " Minute: " + System.DateTime.Now.Minute + " Second: " + System.DateTime.Now.Second + " Millisecond: " + System.DateTime.Now.Millisecond);
            RecallAvaX_U(); //AvaX_U_1, AvaX_U_2, AvaX_U_3, AvaX_U_4....
            AvaX_U[0] = AvaX_U_1;
            AvaX_U[1] = AvaX_U_2;
            AvaX_U[2] = AvaX_U_3;
            AvaX_U[3] = AvaX_U_4;
            AvaX_U[4] = AvaX_U_5;
            AvaX_U[5] = AvaX_U_6;
            AvaX_U[6] = AvaX_U_7;
            AvaX_U[7] = AvaX_U_8;
            AvaX_U[8] = AvaX_U_9;
            AvaX_U[9] = AvaX_U_10;
            //float[] data = null;
            //float[] sample_input = new float[90];

            HumX45 = Feat_fromX(HumX, AvaY, 0);
            for (int m = 0; m < 45; m++)
            {
                sample_input[m] = HumX45[m];
                //humanX[m] = HumX45[m];
            }
            float[] temp_avaU = new float[10];
            for (int i = 0; i < 10; i++)
            {
                AvaX45 = Feat_fromX(AvaX_U[i], HumY, 1);
                for (int m = 0; m < 45; m++)
                {
                    sample_input[45 + m] = AvaX45[m];
                    //avatarX[m] = AvaX45[m];
                }
                data = telp_script.clientSocketHandler.getData(FRAME_BUFFER_SIZE, sample_input);
                temp_avaU[i] = data[0];
                //Debug.Log("Ava_U" + (i + 1) + ": " + data[0]);
                sIndex[pyh * Map_R_width * numSec + pxw * numSec + pzs + i] = new SimIndex(pxw, pyh, pzs, data[0]);
            }

            //var rank = sIndex.OrderBy(SimIndex => SimIndex.simValue).Select
            var rank = sIndex.Select((x, i) => new { OldIndex = i, Value = x.simValue, NewIndex = -1 })
                                  .OrderBy(x => x.Value).Select((x, i) => new { OldIndex = x.OldIndex, Value = x.Value, NewIndex = i + 1 })
                                  .OrderBy(x => x.OldIndex);


            for (int i = 0; i < 10; i++)
            {

                int rank100 = rank.ElementAt(pyh * Map_R_width * numSec + pxw * numSec + pzs + i).NewIndex;
                Debug.Log("Ava_U" + (i + 1) + ": " + rank100 + " (" + Mathf.FloorToInt((float)rank100 / ((float)feat_count + 10f) * 100f) + ")");
                rank100_cmc[Mathf.FloorToInt((float)rank100 / ((float)feat_count + 10f) * 100f)]++;

            }
            //lowestSim = 1f;
            SimDiff = Mathf.Abs(lowestSim - highestSim);
            heatLev = SimDiff / 5f;
            Debug.Log(rank100_cmc[0] + " " + rank100_cmc[1] + " " + rank100_cmc[2] + " " + rank100_cmc[3] + " " + rank100_cmc[4] + " " + rank100_cmc[5] + " " + rank100_cmc[6] + " " + rank100_cmc[7] + " " + rank100_cmc[8] + " " + rank100_cmc[9]);
            Debug.Log("Placement--------------------END ");

            yyy++;
            if (zzz == 24 && yyy == 37)
            {
                cmc_old = false;

                string fileName = Application.dataPath + "\\ranking_measure\\rank100_cont_triple_24.txt";
                fileStr = new FileStream(@fileName, FileMode.Append, FileAccess.Write);
                StreamWriter sw_rank = new StreamWriter(fileStr);


                for (int l = 0; l < 100; l++)
                {
                    sw_rank.Write(rank100_cmc[l] + " ");
                }
                sw_rank.Close();
                Debug.Log("Rank_CMC (end) Hour : " + System.DateTime.Now.Hour + " Minute: " + System.DateTime.Now.Minute + " Second: " + System.DateTime.Now.Second + " Millisecond: " + System.DateTime.Now.Millisecond);
            }
        }
    }
    void CMC_NewData()
    {
        if (cmc_new)
        {
            if (yyy == 49)
            {
                zzz++;
                pairDD.value = zzz;
                yyy = 37;
                PairChange();
                fur_script.PairToFurnitureMap(numPair);
            }
            //numPair = zzz;
            numScene = yyy;
            DeactAvatars();
            ViewScene_48();

            //Showing all 10 data of each scene
            if (Alldata == true && numScene > 0 && numScene < 49)
            {
                AllUserData_48();
            }
            RecallAvaX();
            RecallHumY();
            RecallHumX();
            RecallAvaY();
            Debug.Log(numPair + "_" + numScene + "_a");

            sIndex = new SimIndex[Map_R_height * Map_R_width * numSec + 10];
            //Debug.Log("Initial value" + (Map_R_height * Map_R_width * numSec + 10));
            float[] data = null;
            float[] sample_input = new float[90];
            pxw = 0;
            pyh = 0;
            pzs = 0;
            timestamp = 0f;
            feat_count = 0;
            lowestSim = 0f;
            int ccount = 0;
            for (int pxw = 0; pxw < Map_R_width; pxw++)
            {
                for (int pyh = 0; pyh < Map_R_height; pyh++)
                {
                    for (int pzs = 0; pzs < numSec; pzs++)
                    {
                        //Debug.Log("Count : " + ccount);
                        ccount++;
                        if (Map_R[pxw, pyh, 0].walkable == true && pyh % 2 == 0 && pxw % 2 == 0 && pzs % 3 == 0)
                        {
                            //Debug.Log("Height : " + pyh + " Width : " + pxw + " Sector : " + pzs);
                            Vector3 pos = new Vector3(Map_R[pxw, pyh, pzs].xCoord, 0f, Map_R[pxw, pyh, pzs].zCoord);
                            AvaX.transform.position = pos;
                            Vector3 eulerY = new Vector3(0f, pzs * 15f, 0f);
                            AvaX.transform.eulerAngles = eulerY;
                            feat_count++;
                            float sum = 0f;

                            float[] humanX = new float[45];
                            float[] avatarX = new float[45];
                            HumX45 = Feat_fromX(HumX, AvaY, 0);
                            for (int m = 0; m < 45; m++)
                            {
                                sample_input[m] = HumX45[m];
                                humanX[m] = HumX45[m];
                                //Debug.Log("HumX" + (m + 1) + " : " + sample_input[m]);
                            }
                            AvaX45 = Feat_fromX(AvaX, HumY, 1);
                            for (int m = 0; m < 45; m++)
                            {
                                sample_input[45 + m] = AvaX45[m];
                                avatarX[m] = AvaX45[m];
                                //Debug.Log("AvaX" + (m + 1) + " : " + sample_input[m + 45]);
                            }
                            data = telp_script.clientSocketHandler.getData(FRAME_BUFFER_SIZE, sample_input);
                            float temp = data[0];
                            sum = temp;
                            sIndex[pyh * Map_R_width * numSec + pxw * numSec + pzs] = new SimIndex(pxw, pyh, pzs, sum);
                            if (sum > lowestSim)
                            {
                                lowestSim = sum;
                            }
                        }
                        else
                        {
                            sIndex[pyh * Map_R_width * numSec + pxw * numSec + pzs] = new SimIndex(pxw, pyh, pzs, 1000000000000F);
                        }

                    }
                }
            }
            Debug.Log("Feat_count : " + feat_count);


            ////////////////////// Avatar_B
            for (int i = 0; i < 10; i++)
            {
                sIndex[Map_R_height * Map_R_width * numSec + i] = new SimIndex(Map_R_width - 1, Map_R_height - 1, numSec, 1000000000000F);
                //Debug.Log("Count : " + (Map_R_height * Map_R_width * numSec + i));
            }
            var result = sIndex.OrderBy(SimIndex => SimIndex.simValue);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //Actual placement
            int topX = result.ElementAt(0).wIdx;
            int topZ = result.ElementAt(0).hIdx;
            int topS = result.ElementAt(0).sIdx;
            //Debug.Log("Rank1 : " + result.ElementAt(0).simValue);
            //highestSim = 0f; //
            highestSim = result.ElementAt(0).simValue;
            AvaX_1R.SetActive(true);

            Vector3 XTpos = new Vector3(Map_R[topX, topZ, topS].xCoord, 0f, Map_R[topX, topZ, topS].zCoord);
            AvaX_1R.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, topS * 15f, 0f);
            AvaX_1R.transform.eulerAngles = XTeulerY;

            topX = result.ElementAt(1).wIdx;
            topZ = result.ElementAt(1).hIdx;
            topS = result.ElementAt(1).sIdx;
            //Debug.Log("Rank2 : " + result.ElementAt(1).simValue);
            AvaX_2O.SetActive(true);

            XTpos = new Vector3(Map_R[topX, topZ, topS].xCoord, 0f, Map_R[topX, topZ, topS].zCoord);
            AvaX_2O.transform.position = XTpos;
            XTeulerY = new Vector3(0f, topS * 15f, 0f);
            AvaX_2O.transform.eulerAngles = XTeulerY;


            topX = result.ElementAt(2).wIdx;
            topZ = result.ElementAt(2).hIdx;
            topS = result.ElementAt(2).sIdx;
            //Debug.Log("Rank3 : " + result.ElementAt(2).simValue);
            AvaX_3Y.SetActive(true);

            XTpos = new Vector3(Map_R[topX, topZ, topS].xCoord, 0f, Map_R[topX, topZ, topS].zCoord);
            AvaX_3Y.transform.position = XTpos;
            XTeulerY = new Vector3(0f, topS * 15f, 0f);
            AvaX_3Y.transform.eulerAngles = XTeulerY;


            topX = result.ElementAt(3).wIdx;
            topZ = result.ElementAt(3).hIdx;
            topS = result.ElementAt(3).sIdx;
            //Debug.Log("Rank4 : " + result.ElementAt(3).simValue);

            AvaX_4G.SetActive(true);

            XTpos = new Vector3(Map_R[topX, topZ, topS].xCoord, 0f, Map_R[topX, topZ, topS].zCoord);
            AvaX_4G.transform.position = XTpos;
            XTeulerY = new Vector3(0f, topS * 15f, 0f);
            AvaX_4G.transform.eulerAngles = XTeulerY;


            topX = result.ElementAt(4).wIdx;
            topZ = result.ElementAt(4).hIdx;
            topS = result.ElementAt(4).sIdx;
            //Debug.Log("Rank5 : " + result.ElementAt(4).simValue);
            Debug.Log("-----------------------------------");
            AvaX_5B.SetActive(true);

            XTpos = new Vector3(Map_R[topX, topZ, topS].xCoord, 0f, Map_R[topX, topZ, topS].zCoord);
            AvaX_5B.transform.position = XTpos;
            XTeulerY = new Vector3(0f, topS * 15f, 0f);
            AvaX_5B.transform.eulerAngles = XTeulerY;

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //Debug.Log("Rank_by_sort(start) Hour : " + System.DateTime.Now.Hour + " Minute: " + System.DateTime.Now.Minute + " Second: " + System.DateTime.Now.Second + " Millisecond: " + System.DateTime.Now.Millisecond);
            RecallAvaX_U(); //AvaX_U_1, AvaX_U_2, AvaX_U_3, AvaX_U_4....
            AvaX_U[0] = AvaX_U_1;
            AvaX_U[1] = AvaX_U_2;
            AvaX_U[2] = AvaX_U_3;
            AvaX_U[3] = AvaX_U_4;
            AvaX_U[4] = AvaX_U_5;
            AvaX_U[5] = AvaX_U_6;
            AvaX_U[6] = AvaX_U_7;
            AvaX_U[7] = AvaX_U_8;
            AvaX_U[8] = AvaX_U_9;
            AvaX_U[9] = AvaX_U_10;
            //float[] data = null;
            //float[] sample_input = new float[90];

            HumX45 = Feat_fromX(HumX, AvaY, 0);
            for (int m = 0; m < 45; m++)
            {
                sample_input[m] = HumX45[m];
                //humanX[m] = HumX45[m];
            }
            float[] temp_avaU = new float[10];
            for (int i = 0; i < 10; i++)
            {
                AvaX45 = Feat_fromX(AvaX_U[i], HumY, 1);
                for (int m = 0; m < 45; m++)
                {
                    sample_input[45 + m] = AvaX45[m];
                    //avatarX[m] = AvaX45[m];
                }
                data = telp_script.clientSocketHandler.getData(FRAME_BUFFER_SIZE, sample_input);
                temp_avaU[i] = data[0];
                //Debug.Log("Ava_U" + (i + 1) + ": " + data[0]);
                sIndex[pyh * Map_R_width * numSec + pxw * numSec + pzs + i] = new SimIndex(pxw, pyh, pzs, data[0]);
            }

            //var rank = sIndex.OrderBy(SimIndex => SimIndex.simValue).Select
            var rank = sIndex.Select((x, i) => new { OldIndex = i, Value = x.simValue, NewIndex = -1 })
                                  .OrderBy(x => x.Value).Select((x, i) => new { OldIndex = x.OldIndex, Value = x.Value, NewIndex = i + 1 })
                                  .OrderBy(x => x.OldIndex);


            for (int i = 0; i < 10; i++)
            {

                int rank100 = rank.ElementAt(pyh * Map_R_width * numSec + pxw * numSec + pzs + i).NewIndex;
                Debug.Log("Ava_U" + (i + 1) + ": " + rank100 + " (" + Mathf.FloorToInt((float)rank100 / (float)feat_count * 100f) + ")");
                rank100_cmc[Mathf.FloorToInt((float)rank100 / ((float)feat_count + 10f) * 100f)]++;
            }
            //lowestSim = 1f;
            SimDiff = Mathf.Abs(lowestSim - highestSim);
            heatLev = SimDiff / 5f;
            Debug.Log(rank100_cmc[0] + " " + rank100_cmc[1] + " " + rank100_cmc[2] + " " + rank100_cmc[3] + " " + rank100_cmc[4] + " " + rank100_cmc[5] + " " + rank100_cmc[6] + " " + rank100_cmc[7] + " " + rank100_cmc[8] + " " + rank100_cmc[9]);
            Debug.Log("Placement--------------------END ");

            yyy++;
            if (zzz == 24 && yyy == 49)
            {
                cmc_new = false;

                string fileName = Application.dataPath + "\\ranking_measure\\rank100_cont_triple_36.txt";
                fileStr = new FileStream(@fileName, FileMode.Append, FileAccess.Write);
                StreamWriter sw_rank = new StreamWriter(fileStr);


                for (int l = 0; l < 100; l++)
                {
                    sw_rank.Write(rank100_cmc[l] + " ");
                }
                sw_rank.Close();
                Debug.Log("Rank_CMC (end) Hour : " + System.DateTime.Now.Hour + " Minute: " + System.DateTime.Now.Minute + " Second: " + System.DateTime.Now.Second + " Millisecond: " + System.DateTime.Now.Millisecond);
            }
        }
    }
    void AllCloseNegativeSamples()
    {
        if (comSample)
        {

            if (yyy == 37)
            {
                zzz++;
                yyy = 13;
            }

            numPair = zzz;
            numScene = yyy;
            numScene--;
            TestScene(0);

            //Showing all 10 data of each scene
            if (Alldata == true && numScene > 0 && numScene < 37)
            {
                AllUserData();
            }
            //Each scene data for Pair 12 or greater
            if (adminmode == false && numPair >= 12)
            {
                ViewScene();
            }
            RecallAvaX();
            RecallHumY();
            RecallHumX();
            RecallAvaY();
            //Debug.Log(numPair + "_" + numScene + "_cd");
            RecallAvaX_U(); //AvaX_U_1, AvaX_U_2, AvaX_U_3, AvaX_U_4....
                            //GameObject[] AvaX_U = new GameObject[10]; // GameObject array for 10 user selected avatar
            AvaX_U[0] = AvaX_U_1;
            AvaX_U[1] = AvaX_U_2;
            AvaX_U[2] = AvaX_U_3;
            AvaX_U[3] = AvaX_U_4;
            AvaX_U[4] = AvaX_U_5;
            AvaX_U[5] = AvaX_U_6;
            AvaX_U[6] = AvaX_U_7;
            AvaX_U[7] = AvaX_U_8;
            AvaX_U[8] = AvaX_U_9;
            AvaX_U[9] = AvaX_U_10;
            // Array for clone of avatar (10 positive /10 negative)
            AvaX_N_clone_1 = NsampleNearUserdata(AvaX_U, HumY); // Function that generate samples
            if (AvaX_N_clone_1[0] != null)
            {
                for (int i = 0; i < 10; i++)
                {
                    Destroy(AvaX_N_clone_1[i]);

                }
            }
        }
        if (t_count == 10)
        {
            yyy++;
            t_count = 0;
        }

        if (zzz == 24 && yyy == 37)
        {
            comSample = false;
        }
    }
    void AllCloseNegativeSamples_48()
    {
        if (comSample_48)
        {

            if (yyy == 49)
            {
                zzz++;
                pairDD.value = zzz;
                yyy = 37;
                PairChange();
            }


            numScene = yyy;
            DeactAvatars();
            //numScene--;
            ViewScene_48();
            Debug.Log("Pair : " + numPair + " NumScene : " + numScene);
            //Showing all 10 data of each scene
            if (Alldata == true && numScene > 0 && numScene < 49)
            {
                AllUserData();
            }

            RecallAvaX();
            RecallHumY();
            RecallHumX();
            RecallAvaY();
            //Debug.Log(numPair + "_" + numScene + "_cd");
            RecallAvaX_U(); //AvaX_U_1, AvaX_U_2, AvaX_U_3, AvaX_U_4....
                            //GameObject[] AvaX_U = new GameObject[10]; // GameObject array for 10 user selected avatar
            AvaX_U[0] = AvaX_U_1;
            AvaX_U[1] = AvaX_U_2;
            AvaX_U[2] = AvaX_U_3;
            AvaX_U[3] = AvaX_U_4;
            AvaX_U[4] = AvaX_U_5;
            AvaX_U[5] = AvaX_U_6;
            AvaX_U[6] = AvaX_U_7;
            AvaX_U[7] = AvaX_U_8;
            AvaX_U[8] = AvaX_U_9;
            AvaX_U[9] = AvaX_U_10;
            // Array for clone of avatar (10 positive /10 negative)
            AvaX_N_clone_1 = NsampleNearUserdata_48(AvaX_U, HumY); // Function that generate samples
            if (AvaX_N_clone_1[0] != null)
            {
                for (int i = 0; i < 10; i++)
                {
                    Destroy(AvaX_N_clone_1[i]);

                }
            }
        }
        if (t_count == 10)
        {
            yyy++;
            t_count = 0;
        }

        if (zzz == 24 && yyy == 49)
        {
            comSample_48 = false;
        }
    }
    void PositiveAndRandomNegativeSamples()
    {
        if (please)
        {
            if (yyy == 37)
            {
                zzz++;
                pairDD.value = zzz;
                yyy = 13;
                PairChange();
            }

            //numPair = zzz;
            numScene = yyy;
            numScene--;
            TestScene(0);

            //Showing all 10 data of each scene
            if (Alldata == true && numScene > 0 && numScene < 37)
            {
                AllUserData();
            }
            //Each scene data for Pair 12 or greater
            if (adminmode == false && numPair >= 12)
            {
                ViewScene();
            }
            RecallAvaX();
            RecallHumY();
            RecallHumX();
            RecallAvaY();
            //Debug.Log(numPair + "_" + numScene + "_a");
            //#region combined_feature from user selected avatarX
            //string fileName = Application.dataPath + "\\Metric\\ranksvm\\qid\\ranksvm_" + numPair.ToString() + "_" + numScene + "a.txt";
            /*
            string fileName = Application.dataPath + "\\Metric\\ranksvm\\NoDistance134567\\ranksvm_NoDis_" + numPair.ToString() + ".txt";
            fileStr = new FileStream(@fileName, FileMode.Append, FileAccess.Write);
            sw = new StreamWriter(fileStr);

            fileName = Application.dataPath + "\\Metric\\ranksvm\\Spatial1567\\ranksvm_Spatial_" + numPair.ToString() + ".txt";
            fileStr = new FileStream(@fileName, FileMode.Append, FileAccess.Write);
            sw_2 = new StreamWriter(fileStr);
            */

            string fileName = Application.dataPath + "\\Deep_feature\\First\\_feature_x_" + numPair.ToString() + ".txt";
            fileStr = new FileStream(@fileName, FileMode.Append, FileAccess.Write);
            sw_3 = new StreamWriter(fileStr);

            RecallAvaX_U(); //AvaX_U_1, AvaX_U_2, AvaX_U_3, AvaX_U_4....
                            // GameObject array for 10 user selected avatar
            AvaX_U[0] = AvaX_U_1;
            AvaX_U[1] = AvaX_U_2;
            AvaX_U[2] = AvaX_U_3;
            AvaX_U[3] = AvaX_U_4;
            AvaX_U[4] = AvaX_U_5;
            AvaX_U[5] = AvaX_U_6;
            AvaX_U[6] = AvaX_U_7;
            AvaX_U[7] = AvaX_U_8;
            AvaX_U[8] = AvaX_U_9;
            AvaX_U[9] = AvaX_U_10;

            for (int i = 0; i < 10; i++)
            {

                feat7d = Dist_Feat_6d(HumX, AvaY, AvaX_U[i], HumY);
                float preference = 5;
                //sw.Write(preference + " " + "qid:" + (numScene - 13 + (numPair - 1) * 24) + " ");

                //preference = 2;
                //sw_2.Write(preference + " " + "qid:" + (numScene - 13 + (numPair - 1) * 24) + " ");
                for (int j = 0; j < 10; j++)
                {
                    preference = preference - (Mathf.Sqrt(Mathf.Pow(AvaX_U[i].transform.position.x - AvaX_U[j].transform.position.x, 2) + Mathf.Pow(AvaX_U[i].transform.position.z - AvaX_U[j].transform.position.z, 2)) / 10f);
                }

                Debug.Log("Preference : " + preference);
                sw_3.Write(preference + " " + "qid:" + (numScene - 13 + (numPair - 1) * 24) + " ");
                /*
                for (int j = 0; j < 7; j++)
                {

                    if (j < 1)
                    {
                        sw.Write((j + 1) + ":" + feat7d[j] + " ");
                        //sw_2.Write((j + 1) + ":" + feat7d[j] + " ");
                    }
                    if (j == 1)
                    {
                        continue;
                    }
                    if (j > 1 && j < 6)
                    {
                        sw.Write((j) + ":" + feat7d[j] + " ");
                        //sw_2.Write((j) + ":" + feat7d[j] + " ");
                    }
                    if (j == 6)
                    {
                        sw.Write((j) + ":" + feat7d[j] + " \n");
                        //sw_2.Write((j) + ":" + feat7d[j] + " \n");
                    }
                }

                for (int j = 0; j < 7; j++)
                {

                    if (j < 1)
                    {
                        //sw.Write((j + 1) + ":" + feat7d[j] + " ");
                        sw_2.Write((j + 1) + ":" + feat7d[j] + " ");
                    }
                    if (j > 0 && j < 4)
                    {
                        continue;
                    }
                    if (j > 3 && j < 6)
                    {
                        //sw.Write((j) + ":" + feat7d[j] + " ");
                        sw_2.Write((j-2) + ":" + feat7d[j] + " ");
                    }
                    if (j == 6)
                    {
                        //sw.Write((j) + ":" + feat7d[j] + " \n");
                        sw_2.Write((j-2) + ":" + feat7d[j] + " \n");
                    }
                }
                */
                //feat7d = Dist_Feat_AbsOr(HumX, AvaY, AvaX_U[i], HumY);
                for (int j = 0; j < 9; j++)
                {
                    if (j < 8)
                    {
                        sw_3.Write((j + 1) + ":" + feat7d[j] + " ");
                    }
                    else
                    {
                        sw_3.Write((j + 1) + ":" + feat7d[j] + " \n");
                    }
                }
            }
            //sw.Close();
            //sw_2.Close();
            sw_3.Close();

            //Debug.Log(numPair + "_" + numScene + "_b");

            //fileName = Application.dataPath + "\\Metric\\ranksvm\\qid\\ranksvm_" + numPair.ToString() + "_" + numScene + "b.txt";
            /*
            fileName = Application.dataPath + "\\Metric\\ranksvm\\NoDistance134567\\ranksvm_NoDis_" + numPair.ToString() + ".txt";
            fileStr = new FileStream(@fileName, FileMode.Append, FileAccess.Write);
            sw = new StreamWriter(fileStr);

            fileName = Application.dataPath + "\\Metric\\ranksvm\\Spatial1567\\ranksvm_Spatial_" + numPair.ToString() + ".txt";
            fileStr = new FileStream(@fileName, FileMode.Append, FileAccess.Write);
            sw_2 = new StreamWriter(fileStr);
            */

            fileName = Application.dataPath + "\\Metric\\ranksvm\\TwoAngles9D\\ranksvm_TwoAng_" + numPair.ToString() + ".txt";
            fileStr = new FileStream(@fileName, FileMode.Append, FileAccess.Write);
            sw_3 = new StreamWriter(fileStr);

            int count = 0;

            while (count < 100)
            {
                //System.GC.Collect();
                AvaX.transform.position = new Vector3(Random.Range(Map_R[0, 0, 0].xCoord, Map_R[Map_R_width - 1, Map_R_height - 1, 0].xCoord), 0f, Random.Range(Map_R[0, 0, 0].zCoord, Map_R[Map_R_width - 1, Map_R_height - 1, 0].zCoord));
                AvaX.transform.eulerAngles = new Vector3(AvaX.transform.eulerAngles.x, Random.Range(0f, 360f), AvaX.transform.eulerAngles.z);
                //feat7d = new float[7];
                feat7d = Dist_Feat_6d(HumX, AvaY, AvaX, HumY);
                if (!(feat7d[0] < 0.2f && feat7d[1] < 0.1f && feat7d[2] < 0.2f))
                {
                    AvaX_N_clone[count] = Instantiate(AvaX_N_prefab, AvaX.transform.position, Quaternion.Euler(AvaX.transform.eulerAngles)) as GameObject;
                    float preference = 1;
                    //sw.Write(preference + " " + "qid:" + (numScene - 13 + (numPair - 1) * 24) + " ");
                    //sw_2.Write(preference + " " + "qid:" + (numScene - 13 + (numPair - 1) * 24) + " ");
                    sw_3.Write(preference + " " + "qid:" + (numScene - 13 + (numPair - 1) * 24) + " ");
                    for (int j = 0; j < 9; j++)
                    {
                        if (j < 8)
                        {
                            sw_3.Write((j + 1) + ":" + feat7d[j] + " ");
                        }
                        else
                        {
                            sw_3.Write((j + 1) + ":" + feat7d[j] + " \n");
                        }
                    }
                    count++;

                }
            }

            sw_3.Close();
            if (AvaX_N_clone[0] != null)
            {
                for (int i = 0; i < 100; i++)
                {
                    Destroy(AvaX_N_clone[i]);

                }
            }
            yyy++;
            if (zzz == 24 && yyy == 37)
            {
                please = false;
            }
 
        }
    }
    void PositiveAndRandomNegativeSamples_48()
    {
        if (PosRNeg)
        {
            if (yyy == 49)
            {
                zzz++;
                pairDD.value = zzz;
                yyy = 37;
                PairChange();
            }

            
            numScene = yyy;
            DeactAvatars();
            //numScene--;
            ViewScene_48();
            Debug.Log("Pair : " + numPair + " NumScene : " + numScene);
            //Showing all 10 data of each scene
            if (Alldata == true && numScene > 0 && numScene < 49)
            {
                AllUserData();
            }

            RecallAvaX();
            RecallHumY();
            RecallHumX();
            RecallAvaY();
            //Debug.Log(numPair + "_" + numScene + "_a");
            //#region combined_feature from user selected avatarX
            //string fileName = Application.dataPath + "\\Metric\\ranksvm\\qid\\ranksvm_" + numPair.ToString() + "_" + numScene + "a.txt";
            /*
            string fileName = Application.dataPath + "\\Metric\\ranksvm\\NoDistance134567\\ranksvm_NoDis_" + numPair.ToString() + ".txt";
            fileStr = new FileStream(@fileName, FileMode.Append, FileAccess.Write);
            sw = new StreamWriter(fileStr);

            fileName = Application.dataPath + "\\Metric\\ranksvm\\Spatial1567\\ranksvm_Spatial_" + numPair.ToString() + ".txt";
            fileStr = new FileStream(@fileName, FileMode.Append, FileAccess.Write);
            sw_2 = new StreamWriter(fileStr);
            */

            string fileName = Application.dataPath + "\\Metric\\ranksvm\\public48\\ranksvm_Pub_" + numPair.ToString() + ".txt";
            fileStr = new FileStream(@fileName, FileMode.Append, FileAccess.Write);
            sw_3 = new StreamWriter(fileStr);

            RecallAvaX_U(); //AvaX_U_1, AvaX_U_2, AvaX_U_3, AvaX_U_4....
                            // GameObject array for 10 user selected avatar
            AvaX_U[0] = AvaX_U_1;
            AvaX_U[1] = AvaX_U_2;
            AvaX_U[2] = AvaX_U_3;
            AvaX_U[3] = AvaX_U_4;
            AvaX_U[4] = AvaX_U_5;
            AvaX_U[5] = AvaX_U_6;
            AvaX_U[6] = AvaX_U_7;
            AvaX_U[7] = AvaX_U_8;
            AvaX_U[8] = AvaX_U_9;
            AvaX_U[9] = AvaX_U_10;

            for (int i = 0; i < 10; i++)
            {

                feat7d = Dist_Feat_6d(HumX, AvaY, AvaX_U[i], HumY);
                float preference = 7;
                //sw.Write(preference + " " + "qid:" + (numScene - 13 + (numPair - 1) * 24) + " ");

                //preference = 2;
                //sw_2.Write(preference + " " + "qid:" + (numScene - 13 + (numPair - 1) * 24) + " ");
                for (int j = 0; j < 10; j++)
                {
                    preference = preference - (Mathf.Sqrt(Mathf.Pow(AvaX_U[i].transform.position.x - AvaX_U[j].transform.position.x, 2) + Mathf.Pow(AvaX_U[i].transform.position.z - AvaX_U[j].transform.position.z, 2)) / 10f);
                }

                //Debug.Log("Preference : " + preference);
                sw_3.Write(preference + " " + "qid:" + (numScene - 13 + (numPair - 1) * 24) + " ");
                /*
                for (int j = 0; j < 7; j++)
                {

                    if (j < 1)
                    {
                        sw.Write((j + 1) + ":" + feat7d[j] + " ");
                        //sw_2.Write((j + 1) + ":" + feat7d[j] + " ");
                    }
                    if (j == 1)
                    {
                        continue;
                    }
                    if (j > 1 && j < 6)
                    {
                        sw.Write((j) + ":" + feat7d[j] + " ");
                        //sw_2.Write((j) + ":" + feat7d[j] + " ");
                    }
                    if (j == 6)
                    {
                        sw.Write((j) + ":" + feat7d[j] + " \n");
                        //sw_2.Write((j) + ":" + feat7d[j] + " \n");
                    }
                }

                for (int j = 0; j < 7; j++)
                {

                    if (j < 1)
                    {
                        //sw.Write((j + 1) + ":" + feat7d[j] + " ");
                        sw_2.Write((j + 1) + ":" + feat7d[j] + " ");
                    }
                    if (j > 0 && j < 4)
                    {
                        continue;
                    }
                    if (j > 3 && j < 6)
                    {
                        //sw.Write((j) + ":" + feat7d[j] + " ");
                        sw_2.Write((j-2) + ":" + feat7d[j] + " ");
                    }
                    if (j == 6)
                    {
                        //sw.Write((j) + ":" + feat7d[j] + " \n");
                        sw_2.Write((j-2) + ":" + feat7d[j] + " \n");
                    }
                }
                */
                //feat7d = Dist_Feat_AbsOr(HumX, AvaY, AvaX_U[i], HumY);
                for (int j = 0; j < 7; j++)
                {
                    if (j < 6)
                    {
                        sw_3.Write((j + 1) + ":" + feat7d[j] + " ");
                    }
                    else
                    {
                        sw_3.Write((j + 1) + ":" + feat7d[j] + " \n");
                    }
                }
            }
            //sw.Close();
            //sw_2.Close();
            sw_3.Close();

            //Debug.Log(numPair + "_" + numScene + "_b");

            //fileName = Application.dataPath + "\\Metric\\ranksvm\\qid\\ranksvm_" + numPair.ToString() + "_" + numScene + "b.txt";
            /*
            fileName = Application.dataPath + "\\Metric\\ranksvm\\NoDistance134567\\ranksvm_NoDis_" + numPair.ToString() + ".txt";
            fileStr = new FileStream(@fileName, FileMode.Append, FileAccess.Write);
            sw = new StreamWriter(fileStr);

            fileName = Application.dataPath + "\\Metric\\ranksvm\\Spatial1567\\ranksvm_Spatial_" + numPair.ToString() + ".txt";
            fileStr = new FileStream(@fileName, FileMode.Append, FileAccess.Write);
            sw_2 = new StreamWriter(fileStr);
            */
            
            fileName = Application.dataPath + "\\Metric\\ranksvm\\public48\\ranksvm_Pub_" + numPair.ToString() + ".txt";
            fileStr = new FileStream(@fileName, FileMode.Append, FileAccess.Write);
            sw_3 = new StreamWriter(fileStr);

            int count = 0;

            while (count < 100)
            {
                //System.GC.Collect();
                AvaX.transform.position = new Vector3(Random.Range(Map_R[0, 0, 0].xCoord, Map_R[Map_R_width - 1, Map_R_height - 1, 0].xCoord), 0f, Random.Range(Map_R[0, 0, 0].zCoord, Map_R[Map_R_width - 1, Map_R_height - 1, 0].zCoord));
                AvaX.transform.eulerAngles = new Vector3(AvaX.transform.eulerAngles.x, Random.Range(0f, 360f), AvaX.transform.eulerAngles.z);
                //feat7d = new float[7];
                feat7d = Dist_Feat_6d(HumX, AvaY, AvaX, HumY);
                //여기 조건을 바꾸면 되네
                if (!(feat7d[0] < 0.2f && feat7d[1] < 0.1f && feat7d[2] < 0.2f && feat7d[3] < 0.7f && feat7d[4] < 0.4f && feat7d[5] < 0.7f))
                {
                    AvaX_N_clone[count] = Instantiate(AvaX_N_prefab, AvaX.transform.position, Quaternion.Euler(AvaX.transform.eulerAngles)) as GameObject;
                    float preference = 1;
                    //sw.Write(preference + " " + "qid:" + (numScene - 13 + (numPair - 1) * 24) + " ");
                    //sw_2.Write(preference + " " + "qid:" + (numScene - 13 + (numPair - 1) * 24) + " ");
                    sw_3.Write(preference + " " + "qid:" + (numScene - 13 + (numPair - 1) * 24) + " ");
                    for (int j = 0; j < 7; j++)
                    {
                        if (j < 6)
                        {
                            sw_3.Write((j + 1) + ":" + feat7d[j] + " ");
                        }
                        else
                        {
                            sw_3.Write((j + 1) + ":" + feat7d[j] + " \n");
                        }
                    }
                    count++;

                }
            }


            if (AvaX_N_clone[0] != null)
            {
                for (int i = 0; i < 100; i++)
                {
                    Destroy(AvaX_N_clone[i]);

                }
            }
            sw_3.Close();
            
            yyy++;
            if (zzz == 24 && yyy == 49)
            {
                PosRNeg = false;
            }

        }
    }
    GameObject[] NsampleNearUserdata(GameObject[] AvaU, GameObject HumY)
    {

         //Array for samples (10 positive / 10 negative)
        //string fileName = Application.dataPath + "\\Metric\\ranksvm\\qid\\ranksvm_" + numPair.ToString() + "_" + numScene + "c.txt";
        string fileName = Application.dataPath + "\\Deep_feature\\FBSMap\\feature_x_neg" + numPair.ToString() + ".txt";
        fileStr = new FileStream(@fileName, FileMode.Append, FileAccess.Write);
        StreamWriter sw_nn = new StreamWriter(fileStr);

        int t_attempt = 0;
        int t_count = 0;
        while (t_count < 10)
        {
            for (int j = 0; j < 10; j++) // j for user 1 to user 10
            {
                int attempt = 0;  //break if attempt go over 500 times

                //int p_count = 0;//(current)positive sample count
                //int num_p = 1; //(target)number of positive sample

                int n_count = 0; //(current)negative sample count
                int num_n = 1; //(target)number of negative sample


                //while (n_count < num_n && attempt < 100) //while number of (currently found) n sample is smaller than 1 AND attempt is less than 500 times
                //while ((n_count < num_n || p_count < num_p) && attempt < 25) //while number of (currently found) n sample or p sample is smaller than 1 AND attempt is less than 500 times
                while (n_count < num_n && attempt < 5 && t_count < 10)
                {
                    //AvaU_feat = new float[10, 3]; // array for feature 1,2,3 of 10 user selected avatars
                    bool isCloseToUser = false;
                    //Random position and orientaion close to User selected avatar
                    AvaX.transform.position = new Vector3(Random.Range(AvaU[j].transform.position.x - 1.0f, AvaU[j].transform.position.x + 1.0f), 0f, Random.Range(AvaU[j].transform.position.z - 1.0f, AvaU[j].transform.position.z + 1.0f));
                    AvaX.transform.eulerAngles = new Vector3(AvaU[j].transform.eulerAngles.x, Random.Range(AvaU[j].transform.eulerAngles.y - 180f, AvaU[j].transform.eulerAngles.y + 180f), AvaU[j].transform.eulerAngles.z);

                    for (int i = 0; i < 10; i++) // 3D feature difference between random positioned avatar and 10 user selected avatars
                    {
                        float[] feat3d = new float[3];
                        feat3d = Dist_Feat_3d(AvaU[i], HumY, AvaX, HumY);
                        AvaU_feat[i, 0] = feat3d[0];
                        AvaU_feat[i, 1] = feat3d[1];
                        AvaU_feat[i, 2] = feat3d[2];
                    }
                    //int pos = 0;
                    //int neg = 0;

                    for (int k = 0; k < 10; k++)
                    {

                        //Debug.Log("Avatar U (j) " + j + "Avatar U (k) " + k);
                        //Debug.Log(AvaU_feat[k, 0] + " " + AvaU_feat[k, 1] + " " + AvaU_feat[k, 2]);
                        if (AvaU_feat[k, 1] < 0.1f && AvaU_feat[k, 0] < 0.2f && AvaU_feat[k, 2] < 0.2f)
                        {
                            //Debug.Log("Avatar U (j) " + j + "Avatar U (k) " + k);
                            //Debug.Log("Close : " +  AvaU_feat[k, 0] + " " + AvaU_feat[k, 1] + " " + AvaU_feat[k, 2]);
                            isCloseToUser = true;
                            break;
                        }
                    }

                    if (isCloseToUser == false)
                    {
                        if (n_count < num_n)
                        {
                            Ava_Clones[t_count] = Instantiate(AvaX_N_prefab, AvaX.transform.position, Quaternion.Euler(AvaX.transform.eulerAngles)) as GameObject;
                            n_count++;
                            t_count++;
                            float[] feat85d = new float[85];
                            feat85d = Feat_fromX(AvaX, HumY,1);


                            for (int l = 0; l < 85; l++)
                            {
                                if (l < 84)
                                {
                                    sw_nn.Write(feat85d[l] + "\t");
                                }
                                else
                                {
                                    sw_nn.Write(feat85d[l] + "\n");
                                }
                            }
                            //sw_1.Write(AvaX.transform.position.x + "\t" + AvaX.transform.position.y + "\t" + AvaX.transform.position.z + "\t"+ AvaX.transform.eulerAngles.y + "\n");

                        }
                        /*
                        else if (p_count < num_p && AvaU_feat[j, 0] < 0.25 && AvaU_feat[j, 2] < 0.08)
                        {
                            Ava_Clones[p_count + 10 + j] = Instantiate(AvaX_P_prefab, AvaX.transform.position, Quaternion.Euler(AvaX.transform.eulerAngles)) as GameObject;
                            p_count++;
                        }
                        */
                    }

                    attempt++;
                    t_attempt++;
                    //}
                    //Debug.Log("Count : " + attempt + " Num_user : " + (j + 1));
                    //Debug.Log("Sample count : " + t_count);


                }
                if (t_count == 10)
                {
                    break;
                }
            }
        }
        sw_nn.Close();

        return Ava_Clones;
    }
    GameObject[] NsampleNearUserdata_48(GameObject[] AvaU, GameObject HumY)
    {

        //Array for samples (10 positive / 10 negative)
        //string fileName = Application.dataPath + "\\Metric\\ranksvm\\qid\\ranksvm_" + numPair.ToString() + "_" + numScene + "c.txt";
        string fileName = Application.dataPath + "\\Metric\\ranksvm\\public48\\ranksvm_Pub_" + numPair.ToString() + "n.txt";
        fileStr = new FileStream(@fileName, FileMode.Append, FileAccess.Write);
        sw = new StreamWriter(fileStr);

        //fileName = Application.dataPath + "\\Metric\\ranksvm\\qid\\ranksvm_" + numPair.ToString() + "_" + numScene + "d.txt";
        /*
        fileName = Application.dataPath + "\\Metric\\ranksvm\\relaxed7d\\ranksvm_qid_2_" + numPair.ToString() + "n.txt";
        fileStr = new FileStream(@fileName, FileMode.Append, FileAccess.Write);
        sw_1 = new StreamWriter(fileStr);
        */
        int t_attempt = 0;
        //int t_count = 0;
        //while(t_count < 10)
        //{
        for (int j = 0; j < 10; j++) // j for user 1 to user 10
        {
            int attempt = 0;  //break if attempt go over 500 times

            //int p_count = 0;//(current)positive sample count
            //int num_p = 1; //(target)number of positive sample

            int n_count = 0; //(current)negative sample count
            int num_n = 1; //(target)number of negative sample


            //while (n_count < num_n && attempt < 100) //while number of (currently found) n sample is smaller than 1 AND attempt is less than 500 times
            //while ((n_count < num_n || p_count < num_p) && attempt < 25) //while number of (currently found) n sample or p sample is smaller than 1 AND attempt is less than 500 times
            while (n_count < num_n && attempt < 5 && t_count < 10)
            {
                //AvaU_feat = new float[10, 3]; // array for feature 1,2,3 of 10 user selected avatars
                bool isCloseToUser = false;
                //Random position and orientaion close to User selected avatar
                AvaX.transform.position = new Vector3(Random.Range(AvaU[j].transform.position.x - 1.0f, AvaU[j].transform.position.x + 1.0f), 0f, Random.Range(AvaU[j].transform.position.z - 1.0f, AvaU[j].transform.position.z + 1.0f));
                AvaX.transform.eulerAngles = new Vector3(AvaU[j].transform.eulerAngles.x, Random.Range(AvaU[j].transform.eulerAngles.y - 180f, AvaU[j].transform.eulerAngles.y + 180f), AvaU[j].transform.eulerAngles.z);

                for (int i = 0; i < 10; i++) // 3D feature difference between random positioned avatar and 10 user selected avatars
                {
                    //float[] feat = new float[3];
                    feat7d = Dist_Feat_6d(AvaU[i], HumY, AvaX, HumY);
                    //AvaU_feat[i, 0] = feat7d[0];
                    //AvaU_feat[i, 1] = feat7d[1];
                    //AvaU_feat[i, 2] = feat7d[2];
                }
                //int pos = 0;
                //int neg = 0;

                for (int k = 0; k < 10; k++)
                {

                    //Debug.Log("Avatar U (j) " + j + "Avatar U (k) " + k);
                    //Debug.Log(AvaU_feat[k, 0] + " " + AvaU_feat[k, 1] + " " + AvaU_feat[k, 2]);
                    if (feat7d[0] < 0.2f && feat7d[1] < 0.1f && feat7d[2] < 0.2f && feat7d[3] < 0.7f && feat7d[4] < 0.4f && feat7d[5] < 0.7f)
                    {
                        //Debug.Log("Avatar U (j) " + j + "Avatar U (k) " + k);
                        //Debug.Log("Close : " +  AvaU_feat[k, 0] + " " + AvaU_feat[k, 1] + " " + AvaU_feat[k, 2]);
                        isCloseToUser = true;
                        break;
                    }
                }

                if (isCloseToUser == false)
                {
                    if (n_count < num_n)
                    {
                        Ava_Clones[t_count] = Instantiate(AvaX_N_prefab, AvaX.transform.position, Quaternion.Euler(AvaX.transform.eulerAngles)) as GameObject;
                        n_count++;
                        t_count++;
                        //feat7d = new float[7];
                        //feat7d = Dist_Feat_7d(HumX, AvaY, AvaX, HumY);
                        float preference = 1f;
                        sw.Write(preference + " " + "qid:" + (numScene - 13 + (numPair - 1) * 24) + " ");
                        //preference = 1;
                        //sw_1.Write(preference + " " + "qid:" + (numScene - 13 + (numPair - 1) * 24) + " ");

                        for (int l = 0; l < 7; l++)
                        {
                            if (l < 6)
                            {
                                sw.Write((l + 1) + ":" + feat7d[l] + " ");
                                //sw_1.Write((l + 1) + ":" + feat7d[l] + " ");
                            }
                            else
                            {
                                sw.Write((l + 1) + ":" + feat7d[l] + " \n");
                                //sw_1.Write((l + 1) + ":" + feat7d[l] + " \n");
                            }
                        }
                        //sw_1.Write(AvaX.transform.position.x + "\t" + AvaX.transform.position.y + "\t" + AvaX.transform.position.z + "\t"+ AvaX.transform.eulerAngles.y + "\n");

                    }
                    /*
                    else if (p_count < num_p && AvaU_feat[j, 0] < 0.25 && AvaU_feat[j, 2] < 0.08)
                    {
                        Ava_Clones[p_count + 10 + j] = Instantiate(AvaX_P_prefab, AvaX.transform.position, Quaternion.Euler(AvaX.transform.eulerAngles)) as GameObject;
                        p_count++;
                    }
                    */
                }

                attempt++;
                t_attempt++;
                //}
                //Debug.Log("Count : " + attempt + " Num_user : " + (j + 1));
                //Debug.Log("Sample count : " + t_count);


            }
            if (t_count == 10)
            {
                break;
            }
        }
        sw.Close();
        //sw_1.Close();
        //Debug.Log("Total attempt : " + t_attempt);
        //Debug.Log("Total count : " + t_count);

        return Ava_Clones;
    }
    void HeatMapVis()
    {
        for (int y = 0; y < Map_R_height; y++)
        {
            for (int x = 0; x < Map_R_width; x++)
            {
                if (Map_R[x, y, 0].walkable)
                {
                    hIndex = new SimIndex[24];
                    for (int z = 0; z < 24; z++)
                    {
                        hIndex[z] = new SimIndex(x, y, z, sIndex[y * Map_R_width * numSec + x * numSec + z].simValue);
                    }
                    var result = hIndex.OrderBy(SimIndex => SimIndex.simValue);
                    Vector3 pos = new Vector3(Map_R[x, y, 0].xCoord, 0f, Map_R[x, y, 0].zCoord);
                    Vector3 end = new Vector3(pos.x + 0.2f * Mathf.Cos(Mathf.PI / 180f * (-15f * result.ElementAt(0).sIdx + 90f) % 360), 0f, pos.z + 0.2f * Mathf.Sin(Mathf.PI / 180f * (-15f * result.ElementAt(0).sIdx + 90f) % 360));
                    Vector3 dir = new Vector3(end.x - pos.x, 0f, end.z - pos.z);
                    heatSim = result.ElementAt(0).simValue;

                    if (heatSim < highestSim + SimDiff)
                    {
                        heatSim = (heatSim - highestSim) / SimDiff;
                        heatSim = 1 - heatSim;
                        float p;
                        Color heatColor;
                        if (heatSim < 0.05f)
                        {
                            p = 20f * heatSim;
                            heatColor = new Color(0.15f - 0.15f * p, 0.15f - 0.15f * p, 0.85f * p + 0.15f);
                        }
                        else if (heatSim > (2.0f / 3.0f))
                        {
                            p = 3f * heatSim - 2f;
                            heatColor = new Color(1.0f * p, 1.0f - p, 0f);
                        }
                        else if (heatSim > (1.0f / 3.0f))
                        {
                            p = 3f * heatSim - 1f;
                            heatColor = new Color(p, 1.0f, 0f);
                        }
                        else
                        {
                            p = 3f * heatSim;
                            heatColor = new Color(0, (1.0f / 0.85f) * (p - 0.15f), 1.0f - (1.0f / 0.85f) * (p - 0.15f));
                        }
                        DebugExtension.DebugArrow(pos, dir, heatColor);
                    }



                    /*
                    if (highestSim <= heatSim && heatSim < highestSim + heatLev)
                    {
                        DebugExtension.DebugArrow(pos, dir, Color.red);
                        //Debug.DrawLine(pos, end, Color.red);
                    }
                    if (highestSim + heatLev <= heatSim && heatSim < highestSim + heatLev * 2f)
                    {
                        DebugExtension.DebugArrow(pos, dir, Color.magenta);
                    }
                    if (highestSim + heatLev * 2f <= heatSim && heatSim < highestSim + heatLev * 3f)
                    {
                        DebugExtension.DebugArrow(pos, dir, Color.yellow);
                    }
                    if (highestSim + heatLev * 3f <= heatSim && heatSim < highestSim + heatLev * 4f)
                    {
                        DebugExtension.DebugArrow(pos, dir, Color.green);
                    }

                    if (highestSim + heatLev * 4f <= heatSim && heatSim <= highestSim + heatLev * 5f)
                    {
                        DebugExtension.DebugArrow(pos, dir, Color.blue);
                    }
                    */
                }
            }
        }
    }
    void TxtToMat()
    {
        string tempFileName;
        string line;
        string[] numbers;
        tempFileName = Application.dataPath + "\\SVsFromMatlab\\MLR_w2.txt";
        sr = new StreamReader(@tempFileName);
        Mat_Lab = new float[45, 45];

        line = sr.ReadLine();
        numbers = line.Split(' ');
        for (int i = 0; i < 45; i++)
        {
            for (int j = 0; j < 45; j++)
            {
                Mat_Lab[i, j] = float.Parse(numbers[j + 45 * (i)]);
                //Debug.Log(Mat_Lab[i, j]);
            }
        }
    }
    float[,] Mat_multiplication(float[,] Mat_A, float[,] Mat_B)
    {
        float[,] Mat_C = new float[Mat_A.GetLength(0), Mat_B.GetLength(1)];

        for (int i = 0; i < Mat_A.GetLength(0); i++)
        {
            for (int j = 0; j < Mat_B.GetLength(1); j++)
            {
                Mat_C[i, j] = 0;

                for (int k = 0; k < 2; k++)
                {
                    Mat_C[i, j] += Mat_A[i, k] * Mat_B[k, j];
                }

            }

        }
        return Mat_C;
    }
    float MKML_distance(float[] humanX, float[] avatarX, float[,] Mat_W)
    {
        float distance = 0f;
        float[,] feature_difference = new float[1, 45];
        for (int i = 0; i < 45; i++)
        {
            feature_difference[0, i] = humanX[i] - avatarX[i];
        }

        float[,] Mat_temp = Mat_multiplication(feature_difference, Mat_W);
        float[,] feature_difference_T = new float[45, 1];
        for (int i = 0; i < 45; i++)
        {
            feature_difference_T[i, 0] = humanX[i] - avatarX[i];
        }
        float[,] Mat_result = Mat_multiplication(Mat_temp, feature_difference_T);

        distance = Mat_result[0, 0];

        return distance;
    }
    void DrawRectangle(float X_start, float X_end, float Z_start, float Z_end, Color Square_color)
    {
        Debug.DrawLine(new Vector3(X_start, 0f, Z_start), new Vector3(X_start, 0f, Z_end), Square_color);
        Debug.DrawLine(new Vector3(X_start, 0f, Z_end), new Vector3(X_end, 0f, Z_end), Square_color);
        Debug.DrawLine(new Vector3(X_end, 0f, Z_end), new Vector3(X_end, 0f, Z_start), Square_color);
        Debug.DrawLine(new Vector3(X_end, 0f, Z_start), new Vector3(X_start, 0f, Z_start), Square_color);
    }
    void SupportVectorsFromText()
    {
        string tempFileName;
        string line;
        string[] numbers;
        tempFileName = Application.dataPath + "\\SVsFromMatlab\\pub48.txt";
        sr = new StreamReader(@tempFileName);
        SVs = new float[34560, 7];
        sv_coef = new float[34560];
        for (int i = 0; i < 34560; i++)
        {
            line = sr.ReadLine();
            numbers = line.Split(' ');
            for (int j = 0; j < numbers.Count() - 1; j++)
            {
                float hard;
                if (j == 0)
                {
                    hard = float.Parse(numbers[j]);
                    sv_coef[i] = hard;
                }
                else
                {
                    string temp = numbers[j].Remove(0, 2);
                    hard = float.Parse(temp);
                    SVs[i, (j - 1)] = hard;
                }
            }
        }
    }
    void InitAll()
    {

        //Needs to be improved
        standOrSit = 0; distTtoC1 = 15f; distTtoC2 = 15f;
        //Direction down vector for raycast
        ray_dir_down.Set(0f, -1f, 0f);
        
        //허리욤 FeatureVisualization 이후로 체키라웃각
        ray_waist.Set(0f, 1.25f, 0f);
        
        //Will be wrapped somewhere
        pair_num.text = ""; txt_numScene.text = ""; user_num.text = "";

        //Find buttons, Set false for different mode
        next = GameObject.Find("Next_button");
        back = GameObject.Find("Back_button");
        save = GameObject.Find("Save_button");
        save.SetActive(false);
        redo = GameObject.Find("Redo_button");
        redo.SetActive(false);

        //Num of chr for admin mode (1 ,2, 3, 4 for position of human X and Y, avatar X and Y)
        chr = GameObject.Find("CHR_IF");
        chr.SetActive(false);

        //Only used at the experiment
        //pair = GameObject.Find("Pair_IF");
        //user = GameObject.Find("User_IF");

        //Adminmode should be wrapped?
        if (adminmode == true)
        {
            next.SetActive(false);
            back.SetActive(false);
            save.SetActive(true);
            chr.SetActive(true);
            redo.SetActive(true);
        }

        //Delegate of buttons 
        nextButton.onClick.AddListener(delegate { TestScene(0); });
        backButton.onClick.AddListener(delegate { TestScene(1); });
        rotateY.onValueChanged.AddListener(delegate { RotationYSet(standOrSit); });
        saveButton.onClick.AddListener(delegate { CreateScene_48(1); });
        redoButton.onClick.AddListener(delegate { CreateScene_48(0); });
        pairDD.onValueChanged.AddListener(delegate { PairChange(); });
    }
    public class GetChildrenPlz
    {
        public static GameObject[] getChildren(GameObject parent, bool recursive = false)
        {
            List<GameObject> items = new List<GameObject>();
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                items.Add(parent.transform.GetChild(i).gameObject);
                if (recursive)
                { // set true to go through the hiearchy.
                    items.AddRange(getChildren(parent.transform.GetChild(i).gameObject, recursive));
                }
            }
            return items.ToArray();
        }
    }
    void AddMeshColliders (int pair)
    {
        if(pair==1){
            coll1 = GameObject.Find("S1_Top");
            coll2 = GameObject.Find("S2_Top");
        }
        if (pair==2){
            coll1 = GameObject.Find("S1_Top");
            coll2 = GameObject.Find("S3_Top");
        }
        if (pair==3){
            coll1 = GameObject.Find("S1_Top");
            coll2 = GameObject.Find("S4_Top");
        }
        if (pair==4)
        {
            coll1 = GameObject.Find("S1_Top");
            coll2 = GameObject.Find("S6_Top");
        }
        if (pair==5)
        {
            coll1 = GameObject.Find("S1_Top");
            coll2 = GameObject.Find("S7_Top");
        }
        if (pair==6)
        {
            coll1 = GameObject.Find("S1_Top");
            coll2 = GameObject.Find("S8_Top");
        }
        if (pair == 7)
        {
            coll1 = GameObject.Find("S2_Top");
            coll2 = GameObject.Find("S3_Top");
        }
        if (pair == 8)
        {
            coll1 = GameObject.Find("S2_Top");
            coll2 = GameObject.Find("S4_Top");
        }
        if (pair == 9)
        {
            coll1 = GameObject.Find("S2_Top");
            coll2 = GameObject.Find("S5_Top");
        }
        if (pair == 10)
        {
            coll1 = GameObject.Find("S2_Top");
            coll2 = GameObject.Find("S7_Top");
        }
        if (pair == 11)
        {
            coll1 = GameObject.Find("S2_Top");
            coll2 = GameObject.Find("S8_Top");
        }
        if (pair == 12)
        {
            coll1 = GameObject.Find("S3_Top");
            coll2 = GameObject.Find("S4_Top");
        }
        if (pair == 13)
        {
            coll1 = GameObject.Find("S3_Top");
            coll2 = GameObject.Find("S5_Top");
        }
        if (pair == 14)
        {
            coll1 = GameObject.Find("S3_Top");
            coll2 = GameObject.Find("S6_Top");
        }
        if (pair == 15)
        {
            coll1 = GameObject.Find("S3_Top");
            coll2 = GameObject.Find("S8_Top");
        }
        if (pair == 16)
        {
            coll1 = GameObject.Find("S4_Top");
            coll2 = GameObject.Find("S5_Top");
        }
        if (pair == 17)
        {
            coll1 = GameObject.Find("S4_Top");
            coll2 = GameObject.Find("S6_Top");
        }
        if (pair == 18)
        {
            coll1 = GameObject.Find("S4_Top");
            coll2 = GameObject.Find("S7_Top");
        }
        if (pair == 19)
        {
            coll1 = GameObject.Find("S5_Top");
            coll2 = GameObject.Find("S6_Top");
        }
        if (pair == 20)
        {
            coll1 = GameObject.Find("S5_Top");
            coll2 = GameObject.Find("S7_Top");
        }
        if (pair == 21)
        {
            coll1 = GameObject.Find("S5_Top");
            coll2 = GameObject.Find("S8_Top");
        }
        if (pair == 22)
        {
            coll1 = GameObject.Find("S6_Top");
            coll2 = GameObject.Find("S7_Top");
        }
        if (pair == 23)
        {
            coll1 = GameObject.Find("S6_Top");
            coll2 = GameObject.Find("S8_Top");
        }
        if (pair == 24)
        {
            coll1 = GameObject.Find("S7_Top");
            coll2 = GameObject.Find("S8_Top");
        }
        coll1_objs = GetChildrenPlz.getChildren(coll1, true);
        for (var f = 0; f < coll1_objs.Length; f++)
        {
            Component MR = coll1_objs[f].GetComponent("MeshRenderer");
            if (MR != null)
            {
                coll1_objs[f].AddComponent<MeshCollider>();
            }
        }
        coll2_objs = GetChildrenPlz.getChildren(coll2, true);
        for (var f = 0; f < coll2_objs.Length; f++)
        {
            Component MR = coll2_objs[f].GetComponent("MeshRenderer");
            if (MR != null)
            {
                coll2_objs[f].AddComponent<MeshCollider>();
                
            }
        }
    }
    void FindAvatars()
    {
        //HumX and AvaX for Test
        HumXT = GameObject.Find("HumXT");
        HumXT_Sit = GameObject.Find("HumXT_Sit");
        HumXC = GameObject.Find("HumXC");
        HumXC_Sit = GameObject.Find("HumXC_Sit");
        AvaXT = GameObject.Find("AvaXT");
        AvaXT_Sit = GameObject.Find("AvaXT_Sit");
        AvaXC = GameObject.Find("AvaXC");
        AvaXC_Sit = GameObject.Find("AvaXC_Sit");

        //HumY and AvaY for Test
        HumYT = GameObject.Find("HumYT");
        HumYT_Sit = GameObject.Find("HumYT_Sit");
        HumYC = GameObject.Find("HumYC");
        HumYC_Sit = GameObject.Find("HumYC_Sit");
        AvaYT = GameObject.Find("AvaYT");
        AvaYT_Sit = GameObject.Find("AvaYT_Sit");
        AvaYC = GameObject.Find("AvaYC");
        AvaYC_Sit = GameObject.Find("AvaYC_Sit");

        //Avatars placed by users (AvaX_U)
        AvaXT_1 = GameObject.Find("AvaXT_1");
        AvaXT_Sit_1 = GameObject.Find("AvaXT_Sit_1");
        AvaXC_1 = GameObject.Find("AvaXC_1");
        AvaXC_Sit_1 = GameObject.Find("AvaXC_Sit_1");
        AvaXT_2 = GameObject.Find("AvaXT_2");
        AvaXT_Sit_2 = GameObject.Find("AvaXT_Sit_2");
        AvaXT_3 = GameObject.Find("AvaXT_3");
        AvaXT_Sit_3 = GameObject.Find("AvaXT_Sit_3");
        AvaXT_4 = GameObject.Find("AvaXT_4");
        AvaXT_Sit_4 = GameObject.Find("AvaXT_Sit_4");
        AvaXT_5 = GameObject.Find("AvaXT_5");
        AvaXT_Sit_5 = GameObject.Find("AvaXT_Sit_5");
        AvaXT_6 = GameObject.Find("AvaXT_6");
        AvaXT_Sit_6 = GameObject.Find("AvaXT_Sit_6");
        AvaXT_7 = GameObject.Find("AvaXT_7");
        AvaXT_Sit_7 = GameObject.Find("AvaXT_Sit_7");
        AvaXT_8 = GameObject.Find("AvaXT_8");
        AvaXT_Sit_8 = GameObject.Find("AvaXT_Sit_8");
        AvaXT_9 = GameObject.Find("AvaXT_9");
        AvaXT_Sit_9 = GameObject.Find("AvaXT_Sit_9");
        AvaXT_10 = GameObject.Find("AvaXT_10");
        AvaXT_Sit_10 = GameObject.Find("AvaXT_Sit_10");

        //Avatar placed by algorithms (AvaX_A)
        AvaX_A_1 = GameObject.Find("AvaX_A_1");
        AvaX_A_2 = GameObject.Find("AvaX_A_2");
        AvaX_A_3 = GameObject.Find("AvaX_A_3");
        AvaX_A_4 = GameObject.Find("AvaX_A_4");
        AvaX_A_5 = GameObject.Find("AvaX_A_5");
        AvaX_A_6 = GameObject.Find("AvaX_A_6");

        AvaX_B_1 = GameObject.Find("AvaX_B_1");
        AvaX_B_2 = GameObject.Find("AvaX_B_2");
        AvaX_B_3 = GameObject.Find("AvaX_B_3");
        AvaX_B_4 = GameObject.Find("AvaX_B_4");
        AvaX_B_5 = GameObject.Find("AvaX_B_5");
        AvaX_B_6 = GameObject.Find("AvaX_B_6");
        AvaX_B_7 = GameObject.Find("AvaX_B_7");
        AvaX_B_8 = GameObject.Find("AvaX_B_8");
        AvaX_B_9 = GameObject.Find("AvaX_B_9");
        AvaX_B_10 = GameObject.Find("AvaX_B_10");
        AvaX_B_11 = GameObject.Find("AvaX_B_11");
        AvaX_B_12 = GameObject.Find("AvaX_B_12");
        AvaX_B_13 = GameObject.Find("AvaX_B_13");
        AvaX_B_14 = GameObject.Find("AvaX_B_14");
        AvaX_B_15 = GameObject.Find("AvaX_B_15");
        AvaX_B_16 = GameObject.Find("AvaX_B_16");
        AvaX_B_17 = GameObject.Find("AvaX_B_17");
        AvaX_B_18 = GameObject.Find("AvaX_B_18");
        AvaX_B_19 = GameObject.Find("AvaX_B_19");
        AvaX_B_20 = GameObject.Find("AvaX_B_20");

        AvaX_1R = GameObject.Find("AvaX_1R");
        AvaX_2O = GameObject.Find("AvaX_2O");
        AvaX_3Y = GameObject.Find("AvaX_3Y");
        AvaX_4G = GameObject.Find("AvaX_4G");
        AvaX_5B = GameObject.Find("AvaX_5B");

    }
    void DeactAvatars()
    {
        //HumX and AvaX for Test
        HumXT.SetActive(false);
        HumXT_Sit.SetActive(false);
        HumXC.SetActive(false);
        HumXC_Sit.SetActive(false);
        AvaXT.SetActive(false);
        AvaXT_Sit.SetActive(false);
        AvaXC.SetActive(false);
        AvaXC_Sit.SetActive(false);

        HumYT.SetActive(false);
        HumYT_Sit.SetActive(false);
        HumYC.SetActive(false);
        HumYC_Sit.SetActive(false);
        AvaYT.SetActive(false);
        AvaYT_Sit.SetActive(false);
        AvaYC.SetActive(false);
        AvaYC_Sit.SetActive(false);

        //Avatars placed by users (AvaX_U) sent to origin instead of deactivated
        Vector3 Origin = new Vector3(0f,0f,0f);
        AvaXT_1.transform.position = Origin;
        AvaXT_Sit_1.transform.position = Origin;
        AvaXC_1.transform.position = Origin;
        AvaXC_Sit_1.transform.position = Origin;
        AvaXT_2.transform.position = Origin;
        AvaXT_Sit_2.transform.position = Origin;
        AvaXT_3.transform.position = Origin;
        AvaXT_Sit_3.transform.position = Origin;
        AvaXT_4.transform.position = Origin;
        AvaXT_Sit_4.transform.position = Origin;
        AvaXT_5.transform.position = Origin;
        AvaXT_Sit_5.transform.position = Origin;
        AvaXT_6.transform.position = Origin;
        AvaXT_Sit_6.transform.position = Origin;
        AvaXT_7.transform.position = Origin;
        AvaXT_Sit_7.transform.position = Origin;
        AvaXT_8.transform.position = Origin;
        AvaXT_Sit_8.transform.position = Origin;
        AvaXT_9.transform.position = Origin;
        AvaXT_Sit_9.transform.position = Origin;
        AvaXT_10.transform.position = Origin;
        AvaXT_Sit_10.transform.position = Origin;
        
        /*
        AvaXT_1.SetActive(false);
        AvaXT_Sit_1.SetActive(false);
        AvaXC_1.SetActive(false);
        AvaXC_Sit_1.SetActive(false);
        AvaXT_2.SetActive(false);
        AvaXT_Sit_2.SetActive(false);
        AvaXT_3.SetActive(false);
        AvaXT_Sit_3.SetActive(false);
        AvaXT_4.SetActive(false);
        AvaXT_Sit_4.SetActive(false);
        AvaXT_5.SetActive(false);
        AvaXT_Sit_5.SetActive(false);
        AvaXT_6.SetActive(false);
        AvaXT_Sit_6.SetActive(false);
        AvaXT_7.SetActive(false);
        AvaXT_Sit_7.SetActive(false);
        AvaXT_8.SetActive(false);
        AvaXT_Sit_8.SetActive(false);
        AvaXT_9.SetActive(false);
        AvaXT_Sit_9.SetActive(false);
        AvaXT_10.SetActive(false);
        AvaXT_Sit_10.SetActive(false);
        */

    }
    void RotationYSet(int stand)
    {

        AvaXeulerY.Set(0f, rotateY.value, 0f);

        if (stand == 0)
        {
            AvaXT.transform.eulerAngles = AvaXeulerY;
            AvaXC.transform.eulerAngles = AvaXeulerY;
        }
        else if (stand == 1)
        {
            AvaXT_Sit.transform.eulerAngles = AvaXeulerY;
            AvaXC_Sit.transform.eulerAngles = AvaXeulerY;
        }
    }
    void FindModels()
    {
        S1 = GameObject.Find("S1");
        S2 = GameObject.Find("S2");
        S3 = GameObject.Find("S3");
        S4 = GameObject.Find("S4");
        S5 = GameObject.Find("S5");
        S6 = GameObject.Find("S6");
        S7 = GameObject.Find("S7");
        S8 = GameObject.Find("S8");

        S1.SetActive(false);
        S2.SetActive(false);
        S3.SetActive(false);
        S4.SetActive(false);
        S5.SetActive(false);
        S6.SetActive(false);
        S7.SetActive(false);
        S8.SetActive(false);
    }
    void FindCamera()
    {
        S1Top = GameObject.Find("S1_Top_Camera");
        S2Top = GameObject.Find("S2_Top_Camera");
        S3Top = GameObject.Find("S3_Top_Camera");
        S4Top = GameObject.Find("S4_Top_Camera");
        S5Top = GameObject.Find("S5_Top_Camera");
        S6Top = GameObject.Find("S6_Top_Camera");
        S7Top = GameObject.Find("S7_Top_Camera");
        S8Top = GameObject.Find("S8_Top_Camera");
        S1Top.SetActive(false);
        S2Top.SetActive(false);
        S3Top.SetActive(false);
        S4Top.SetActive(false);
        S5Top.SetActive(false);
        S6Top.SetActive(false);
        S7Top.SetActive(false);
        S8Top.SetActive(false);
    }
    void FindLines()
    {
        l1 = GameObject.Find("Line1");
        l1.SetActive(false);
        l2 = GameObject.Find("Line2");
        l2.SetActive(false);
        l3 = GameObject.Find("Line3");
        l3.SetActive(false);
        l4 = GameObject.Find("Line4");
        l4.SetActive(false);
    }
    void GetCamera(int pair)
    {
        if (pair == 1)
        {
            S1.SetActive(true);
            S2.SetActive(true);
            S1Top.SetActive(true);
            S2Top.SetActive(true);
            AddMeshColliders(pair);
            Top1Cam = S1Top.GetComponent<Camera>();
            Top2Cam = S2Top.GetComponent<Camera>();
            //Top2Cam.rect.Set(0.5f, 0f, 0.5f, 0.5f);
            //Top2Cam.rect = new Rect(0.5f,0f,0.5f,0.5f);
            Top1Cam.rect = new Rect(0f, 0f, 0.5f, 0.5f);
            Top2Cam.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
            Map_L = Map1; Map_R = Map2;
            Map_L_X_start  = S1_X_start; Map_L_X_end = S1_X_end; Map_L_Z_start = S1_Z_start; Map_L_Z_end = S1_Z_end;
            Map_R_X_start = S2_X_start; Map_R_X_end = S2_X_end; Map_R_Z_start = S2_Z_start; Map_R_Z_end = S2_Z_end;
            Map_L_width = S1_width; Map_L_height = S1_height;
            Map_R_width = S2_width; Map_R_height = S2_height;
         
        }
        if (pair == 2)
        {
            S1.SetActive(true);
            S3.SetActive(true);
            S1Top.SetActive(true);
            S3Top.SetActive(true);
            AddMeshColliders(pair);
            Top1Cam = S1Top.GetComponent<Camera>();
            Top2Cam = S3Top.GetComponent<Camera>();
            Top1Cam.rect = new Rect(0f, 0f, 0.5f, 0.5f);
            Top2Cam.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
            Map_L = Map1; Map_R = Map3;
            Map_L_X_start = S1_X_start; Map_L_X_end = S1_X_end; Map_L_Z_start = S1_Z_start; Map_L_Z_end = S1_Z_end;
            Map_R_X_start = S3_X_start; Map_R_X_end = S3_X_end; Map_R_Z_start = S3_Z_start; Map_R_Z_end = S3_Z_end;
            Map_L_width = S1_width; Map_L_height = S1_height;
            Map_R_width = S3_width; Map_R_height = S3_height;
        }
        if (pair == 3)
        {
            S1.SetActive(true);
            S4.SetActive(true);
            S1Top.SetActive(true);
            S4Top.SetActive(true);
            AddMeshColliders(pair);
            Top1Cam = S1Top.GetComponent<Camera>();
            Top2Cam = S4Top.GetComponent<Camera>();
            Top1Cam.rect = new Rect(0f, 0f, 0.5f, 0.5f);
            Top2Cam.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
            Map_L = Map1; Map_R = Map4;
            Map_L_X_start = S1_X_start; Map_L_X_end = S1_X_end; Map_L_Z_start = S1_Z_start; Map_L_Z_end = S1_Z_end;
            Map_R_X_start = S4_X_start; Map_R_X_end = S4_X_end; Map_R_Z_start = S4_Z_start; Map_R_Z_end = S4_Z_end;
            Map_L_width = S1_width; Map_L_height = S1_height;
            Map_R_width = S4_width; Map_R_height = S4_height;
        }
        if (pair == 4)
        {
            S1.SetActive(true);
            S6.SetActive(true);
            S1Top.SetActive(true);
            S6Top.SetActive(true);
            AddMeshColliders(pair);
            Top1Cam = S1Top.GetComponent<Camera>();
            Top2Cam = S6Top.GetComponent<Camera>();
            Top1Cam.rect = new Rect(0f, 0f, 0.5f, 0.5f);
            Top2Cam.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
            Map_L = Map1; Map_R = Map6;
            Map_L_X_start = S1_X_start; Map_L_X_end = S1_X_end; Map_L_Z_start = S1_Z_start; Map_L_Z_end = S1_Z_end;
            Map_R_X_start = S6_X_start; Map_R_X_end = S6_X_end; Map_R_Z_start = S6_Z_start; Map_R_Z_end = S6_Z_end;
            Map_L_width = S1_width; Map_L_height = S1_height;
            Map_R_width = S6_width; Map_R_height = S6_height;
        }
        if (pair == 5)
        {
            S1.SetActive(true);
            S7.SetActive(true);
            S1Top.SetActive(true);
            S7Top.SetActive(true);
            AddMeshColliders(pair);
            Top1Cam = S1Top.GetComponent<Camera>();
            Top2Cam = S7Top.GetComponent<Camera>();
            Top1Cam.rect = new Rect(0f, 0f, 0.5f, 0.5f);
            Top2Cam.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
            Map_L = Map1; Map_R = Map7;
            Map_L_X_start = S1_X_start; Map_L_X_end = S1_X_end; Map_L_Z_start = S1_Z_start; Map_L_Z_end = S1_Z_end;
            Map_R_X_start = S7_X_start; Map_R_X_end = S7_X_end; Map_R_Z_start = S7_Z_start; Map_R_Z_end = S7_Z_end;
            Map_L_width = S1_width; Map_L_height = S1_height;
            Map_R_width = S7_width; Map_R_height = S7_height;
        }
        if (pair == 6)
        {
            S1.SetActive(true);
            S8.SetActive(true);
            S1Top.SetActive(true);
            S8Top.SetActive(true);
            AddMeshColliders(pair);
            Top1Cam = S1Top.GetComponent<Camera>();
            Top2Cam = S8Top.GetComponent<Camera>();
            Top1Cam.rect = new Rect(0f, 0f, 0.5f, 0.5f);
            Top2Cam.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
            Map_L = Map1; Map_R = Map8;
            Map_L_X_start = S1_X_start; Map_L_X_end = S1_X_end; Map_L_Z_start = S1_Z_start; Map_L_Z_end = S1_Z_end;
            Map_R_X_start = S8_X_start; Map_R_X_end = S8_X_end; Map_R_Z_start = S8_Z_start; Map_R_Z_end = S8_Z_end;
            Map_L_width = S1_width; Map_L_height = S1_height;
            Map_R_width = S8_width; Map_R_height = S8_height;
        }
        if (pair == 7)
        {
            S2.SetActive(true);
            S3.SetActive(true);
            S2Top.SetActive(true);
            S3Top.SetActive(true);
            AddMeshColliders(pair);
            Top1Cam = S2Top.GetComponent<Camera>();
            Top2Cam = S3Top.GetComponent<Camera>();
            Top1Cam.rect = new Rect(0f, 0f, 0.5f, 0.5f);
            Top2Cam.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
            Map_L = Map2; Map_R = Map3;
            Map_L_X_start = S2_X_start; Map_L_X_end = S2_X_end; Map_L_Z_start = S2_Z_start; Map_L_Z_end = S2_Z_end;
            Map_R_X_start = S3_X_start; Map_R_X_end = S3_X_end; Map_R_Z_start = S3_Z_start; Map_R_Z_end = S3_Z_end;
            Map_L_width = S2_width; Map_L_height = S2_height;
            Map_R_width = S3_width; Map_R_height = S3_height;
        }
        if (pair == 8)
        {
            S2.SetActive(true);
            S4.SetActive(true);
            S2Top.SetActive(true);
            S4Top.SetActive(true);
            AddMeshColliders(pair);
            Top1Cam = S2Top.GetComponent<Camera>();
            Top2Cam = S4Top.GetComponent<Camera>();
            Top1Cam.rect = new Rect(0f, 0f, 0.5f, 0.5f);
            Top2Cam.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
            Map_L = Map2; Map_R = Map4;
            Map_L_X_start = S2_X_start; Map_L_X_end = S2_X_end; Map_L_Z_start = S2_Z_start; Map_L_Z_end = S2_Z_end;
            Map_R_X_start = S4_X_start; Map_R_X_end = S4_X_end; Map_R_Z_start = S4_Z_start; Map_R_Z_end = S4_Z_end;
            Map_L_width = S2_width; Map_L_height = S2_height;
            Map_R_width = S4_width; Map_R_height = S4_height;
        }
        if (pair == 9)
        {
            S2.SetActive(true);
            S5.SetActive(true);
            S2Top.SetActive(true);
            S5Top.SetActive(true);
            AddMeshColliders(pair);
            Top1Cam = S2Top.GetComponent<Camera>();
            Top2Cam = S5Top.GetComponent<Camera>();
            Top1Cam.rect = new Rect(0f, 0f, 0.5f, 0.5f);
            Top2Cam.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
            Map_L = Map2; Map_R = Map5;
            Map_L_X_start = S2_X_start; Map_L_X_end = S2_X_end; Map_L_Z_start = S2_Z_start; Map_L_Z_end = S2_Z_end;
            Map_R_X_start = S5_X_start; Map_R_X_end = S5_X_end; Map_R_Z_start = S5_Z_start; Map_R_Z_end = S5_Z_end;
            Map_L_width = S2_width; Map_L_height = S2_height;
            Map_R_width = S5_width; Map_R_height = S5_height;
        }
        if (pair == 10)
        {
            S2.SetActive(true);
            S7.SetActive(true);
            S2Top.SetActive(true);
            S7Top.SetActive(true);
            AddMeshColliders(pair);
            Top1Cam = S2Top.GetComponent<Camera>();
            Top2Cam = S7Top.GetComponent<Camera>();
            Top1Cam.rect = new Rect(0f, 0f, 0.5f, 0.5f);
            Top2Cam.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
            Map_L = Map2; Map_R = Map7;
            Map_L_X_start = S2_X_start; Map_L_X_end = S2_X_end; Map_L_Z_start = S2_Z_start; Map_L_Z_end = S2_Z_end;
            Map_R_X_start = S7_X_start; Map_R_X_end = S7_X_end; Map_R_Z_start = S7_Z_start; Map_R_Z_end = S7_Z_end;
            Map_L_width = S2_width; Map_L_height = S2_height;
            Map_R_width = S7_width; Map_R_height = S7_height;
        }
        if (pair == 11)
        {
            S2.SetActive(true);
            S8.SetActive(true);
            S2Top.SetActive(true);
            S8Top.SetActive(true);
            AddMeshColliders(pair);
            Top1Cam = S2Top.GetComponent<Camera>();
            Top2Cam = S8Top.GetComponent<Camera>();
            Top1Cam.rect = new Rect(0f, 0f, 0.5f, 0.5f);
            Top2Cam.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
            Map_L = Map2; Map_R = Map8;
            Map_L_X_start = S2_X_start; Map_L_X_end = S2_X_end; Map_L_Z_start = S2_Z_start; Map_L_Z_end = S2_Z_end;
            Map_R_X_start = S8_X_start; Map_R_X_end = S8_X_end; Map_R_Z_start = S8_Z_start; Map_R_Z_end = S8_Z_end;
            Map_L_width = S2_width; Map_L_height = S2_height;
            Map_R_width = S8_width; Map_R_height = S8_height;
        }
        if (pair == 12)
        {
            S3.SetActive(true);
            S4.SetActive(true);
            S3Top.SetActive(true);
            S4Top.SetActive(true);
            AddMeshColliders(pair);
            Top1Cam = S3Top.GetComponent<Camera>();
            Top2Cam = S4Top.GetComponent<Camera>();
            Top1Cam.rect = new Rect(0f, 0f, 0.5f, 0.5f);
            Top2Cam.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
            Map_L = Map3; Map_R = Map4;
            Map_L_X_start = S3_X_start; Map_L_X_end = S3_X_end; Map_L_Z_start = S3_Z_start; Map_L_Z_end = S3_Z_end;
            Map_R_X_start = S4_X_start; Map_R_X_end = S4_X_end; Map_R_Z_start = S4_Z_start; Map_R_Z_end = S4_Z_end;
            Map_L_width = S3_width; Map_L_height = S3_height;
            Map_R_width = S4_width; Map_R_height = S4_height;
        }
        if (pair == 13)
        {
            S3.SetActive(true);
            S5.SetActive(true);
            S3Top.SetActive(true);
            S5Top.SetActive(true);
            AddMeshColliders(pair);
            Top1Cam = S3Top.GetComponent<Camera>();
            Top2Cam = S5Top.GetComponent<Camera>();
            Top1Cam.rect = new Rect(0f, 0f, 0.5f, 0.5f);
            Top2Cam.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
            Map_L = Map3; Map_R = Map5;
            Map_L_X_start = S3_X_start; Map_L_X_end = S3_X_end; Map_L_Z_start = S3_Z_start; Map_L_Z_end = S3_Z_end;
            Map_R_X_start = S5_X_start; Map_R_X_end = S5_X_end; Map_R_Z_start = S5_Z_start; Map_R_Z_end = S5_Z_end;
            Map_L_width = S3_width; Map_L_height = S3_height;
            Map_R_width = S5_width; Map_R_height = S5_height;
        }
        if (pair == 14)
        {
            S3.SetActive(true);
            S6.SetActive(true);
            S3Top.SetActive(true);
            S6Top.SetActive(true);
            AddMeshColliders(pair);
            Top1Cam = S3Top.GetComponent<Camera>();
            Top2Cam = S6Top.GetComponent<Camera>();
            Top1Cam.rect = new Rect(0f, 0f, 0.5f, 0.5f);
            Top2Cam.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
            Map_L = Map3; Map_R = Map6;
            Map_L_X_start = S3_X_start; Map_L_X_end = S3_X_end; Map_L_Z_start = S3_Z_start; Map_L_Z_end = S3_Z_end;
            Map_R_X_start = S6_X_start; Map_R_X_end = S6_X_end; Map_R_Z_start = S6_Z_start; Map_R_Z_end = S6_Z_end;
            Map_L_width = S3_width; Map_L_height = S3_height;
            Map_R_width = S6_width; Map_R_height = S6_height;
        }
        if (pair == 15)
        {
            S3.SetActive(true);
            S8.SetActive(true);
            S3Top.SetActive(true);
            S8Top.SetActive(true);
            AddMeshColliders(pair);
            Top1Cam = S3Top.GetComponent<Camera>();
            Top2Cam = S8Top.GetComponent<Camera>();
            Top1Cam.rect = new Rect(0f, 0f, 0.5f, 0.5f);
            Top2Cam.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
            Map_L = Map3; Map_R = Map8;
            Map_L_X_start = S3_X_start; Map_L_X_end = S3_X_end; Map_L_Z_start = S3_Z_start; Map_L_Z_end = S3_Z_end;
            Map_R_X_start = S8_X_start; Map_R_X_end = S8_X_end; Map_R_Z_start = S8_Z_start; Map_R_Z_end = S8_Z_end;
            Map_L_width = S3_width; Map_L_height = S3_height;
            Map_R_width = S8_width; Map_R_height = S8_height;
        }
        if (pair == 16)
        {
            S4.SetActive(true);
            S5.SetActive(true);
            S4Top.SetActive(true);
            S5Top.SetActive(true);
            AddMeshColliders(pair);
            Top1Cam = S4Top.GetComponent<Camera>();
            Top2Cam = S5Top.GetComponent<Camera>();
            Top1Cam.rect = new Rect(0f, 0f, 0.5f, 0.5f);
            Top2Cam.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
            Map_L = Map4; Map_R = Map5;
            Map_L_X_start = S4_X_start; Map_L_X_end = S4_X_end; Map_L_Z_start = S4_Z_start; Map_L_Z_end = S4_Z_end;
            Map_R_X_start = S5_X_start; Map_R_X_end = S5_X_end; Map_R_Z_start = S5_Z_start; Map_R_Z_end = S5_Z_end;
            Map_L_width = S4_width; Map_L_height = S4_height;
            Map_R_width = S5_width; Map_R_height = S5_height;
        }
        if (pair == 17)
        {
            S4.SetActive(true);
            S6.SetActive(true);
            S4Top.SetActive(true);
            S6Top.SetActive(true);
            AddMeshColliders(pair);
            Top1Cam = S4Top.GetComponent<Camera>();
            Top2Cam = S6Top.GetComponent<Camera>();
            Top1Cam.rect = new Rect(0f, 0f, 0.5f, 0.5f);
            Top2Cam.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
            Map_L = Map4; Map_R = Map6;
            Map_L_X_start = S4_X_start; Map_L_X_end = S4_X_end; Map_L_Z_start = S4_Z_start; Map_L_Z_end = S4_Z_end;
            Map_R_X_start = S6_X_start; Map_R_X_end = S6_X_end; Map_R_Z_start = S6_Z_start; Map_R_Z_end = S6_Z_end;
            Map_L_width = S4_width; Map_L_height = S4_height;
            Map_R_width = S6_width; Map_R_height = S6_height;
        }
        if (pair == 18)
        {
            S4.SetActive(true);
            S7.SetActive(true);
            S4Top.SetActive(true);
            S7Top.SetActive(true);
            AddMeshColliders(pair);
            Top1Cam = S4Top.GetComponent<Camera>();
            Top2Cam = S7Top.GetComponent<Camera>();
            Top1Cam.rect = new Rect(0f, 0f, 0.5f, 0.5f);
            Top2Cam.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
            Map_L = Map4; Map_R = Map7;
            Map_L_X_start = S4_X_start; Map_L_X_end = S4_X_end; Map_L_Z_start = S4_Z_start; Map_L_Z_end = S4_Z_end;
            Map_R_X_start = S7_X_start; Map_R_X_end = S7_X_end; Map_R_Z_start = S7_Z_start; Map_R_Z_end = S7_Z_end;
            Map_L_width = S4_width; Map_L_height = S4_height;
            Map_R_width = S7_width; Map_R_height = S7_height;
        }
        if (pair == 19)
        {
            S5.SetActive(true);
            S6.SetActive(true);
            S5Top.SetActive(true);
            S6Top.SetActive(true);
            AddMeshColliders(pair);
            Top1Cam = S5Top.GetComponent<Camera>();
            Top2Cam = S6Top.GetComponent<Camera>();
            Top1Cam.rect = new Rect(0f, 0f, 0.5f, 0.5f);
            Top2Cam.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
            Map_L = Map5; Map_R = Map6;
            Map_L_X_start = S5_X_start; Map_L_X_end = S5_X_end; Map_L_Z_start = S5_Z_start; Map_L_Z_end = S5_Z_end;
            Map_R_X_start = S6_X_start; Map_R_X_end = S6_X_end; Map_R_Z_start = S6_Z_start; Map_R_Z_end = S6_Z_end;
            Map_L_width = S5_width; Map_L_height = S5_height;
            Map_R_width = S6_width; Map_R_height = S6_height;
        }
        if (pair == 20)
        {
            S5.SetActive(true);
            S7.SetActive(true);
            S5Top.SetActive(true);
            S7Top.SetActive(true);
            AddMeshColliders(pair);
            Top1Cam = S5Top.GetComponent<Camera>();
            Top2Cam = S7Top.GetComponent<Camera>();
            Top1Cam.rect = new Rect(0f, 0f, 0.5f, 0.5f);
            Top2Cam.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
            Map_L = Map5; Map_R = Map7;
            Map_L_X_start = S5_X_start; Map_L_X_end = S5_X_end; Map_L_Z_start = S5_Z_start; Map_L_Z_end = S5_Z_end;
            Map_R_X_start = S7_X_start; Map_R_X_end = S7_X_end; Map_R_Z_start = S7_Z_start; Map_R_Z_end = S7_Z_end;
            Map_L_width = S5_width; Map_L_height = S5_height;
            Map_R_width = S7_width; Map_R_height = S7_height;

        }
        if (pair == 21)
        {
            S5.SetActive(true);
            S8.SetActive(true);
            S5Top.SetActive(true);
            S8Top.SetActive(true);
            AddMeshColliders(pair);
            Top1Cam = S5Top.GetComponent<Camera>();
            Top2Cam = S8Top.GetComponent<Camera>();
            Top1Cam.rect = new Rect(0f, 0f, 0.5f, 0.5f);
            Top2Cam.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
            Map_L = Map5; Map_R = Map8;
            Map_L_X_start = S5_X_start; Map_L_X_end = S5_X_end; Map_L_Z_start = S5_Z_start; Map_L_Z_end = S5_Z_end;
            Map_R_X_start = S8_X_start; Map_R_X_end = S8_X_end; Map_R_Z_start = S8_Z_start; Map_R_Z_end = S8_Z_end;
            Map_L_width = S5_width; Map_L_height = S5_height;
            Map_R_width = S8_width; Map_R_height = S8_height;
        }
        if (pair == 22)
        {
            S6.SetActive(true);
            S7.SetActive(true);
            S6Top.SetActive(true);
            S7Top.SetActive(true);
            AddMeshColliders(pair);
            Top1Cam = S6Top.GetComponent<Camera>();
            Top2Cam = S7Top.GetComponent<Camera>();
            Top1Cam.rect = new Rect(0f, 0f, 0.5f, 0.5f);
            Top2Cam.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
            Map_L = Map6; Map_R = Map7;
            Map_L_X_start = S6_X_start; Map_L_X_end = S6_X_end; Map_L_Z_start = S6_Z_start; Map_L_Z_end = S6_Z_end;
            Map_R_X_start = S7_X_start; Map_R_X_end = S7_X_end; Map_R_Z_start = S7_Z_start; Map_R_Z_end = S7_Z_end;
            Map_L_width = S6_width; Map_L_height = S6_height;
            Map_R_width = S7_width; Map_R_height = S7_height;
        }
        if (pair == 23)
        {
            S6.SetActive(true);
            S8.SetActive(true);
            S6Top.SetActive(true);
            S8Top.SetActive(true);
            AddMeshColliders(pair);
            Top1Cam = S6Top.GetComponent<Camera>();
            Top2Cam = S8Top.GetComponent<Camera>();
            Top1Cam.rect = new Rect(0f, 0f, 0.5f, 0.5f);
            Top2Cam.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
            Map_L = Map6; Map_R = Map8;
            Map_L_X_start = S6_X_start; Map_L_X_end = S6_X_end; Map_L_Z_start = S6_Z_start; Map_L_Z_end = S6_Z_end;
            Map_R_X_start = S8_X_start; Map_R_X_end = S8_X_end; Map_R_Z_start = S8_Z_start; Map_R_Z_end = S8_Z_end;
            Map_L_width = S6_width; Map_L_height = S6_height;
            Map_R_width = S8_width; Map_R_height = S8_height;

        }
        if (pair == 24)
        {
            S7.SetActive(true);
            S8.SetActive(true);
            S7Top.SetActive(true);
            S8Top.SetActive(true);
            AddMeshColliders(pair);
            Top1Cam = S7Top.GetComponent<Camera>();
            Top2Cam = S8Top.GetComponent<Camera>();
            Top1Cam.rect = new Rect(0f, 0f, 0.5f, 0.5f);
            Top2Cam.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
            Map_L = Map7; Map_R = Map8;
            Map_L_X_start = S7_X_start; Map_L_X_end = S7_X_end; Map_L_Z_start = S7_Z_start; Map_L_Z_end = S7_Z_end;
            Map_R_X_start = S8_X_start; Map_R_X_end = S8_X_end; Map_R_Z_start = S8_Z_start; Map_R_Z_end = S8_Z_end;
            Map_L_width = S7_width; Map_L_height = S7_height;
            Map_R_width = S8_width; Map_R_height = S8_height;
        }

    }
    void SingleUserData() //Single user data for Num User
    {
        string fileName = Application.dataPath + "\\testdata_48\\pair" + numPair.ToString() + "user" + numUser.ToString() + "_48.txt";
        //string fileName = "c:\\EXP\\pair" + numPair.ToString() + User_IF.text + ".txt";
        sr = new StreamReader(@fileName);
        string line;
        line = sr.ReadLine();
        string[] numbers = line.Split(' ');
        float[] viewData = new float[numbers.Count()];
        for (int i = 0; i < numbers.Count(); i++)
        {
            float hard;
            hard = float.Parse(numbers[i]);
            viewData[i] = hard;
        }

        if(viewData[(numScene - 1 - 36) * 5]==0)
        {
            AvaXT.SetActive(true);
            Vector3 XTpos = new Vector3(viewData[1 + (numScene - 1 - 36) * 5], viewData[2 + (numScene - 1 - 36) * 5], viewData[3 + (numScene - 1 - 36) * 5]);
            AvaXT.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData[4 + (numScene - 1 - 36) * 5], 0f);
            AvaXT.transform.eulerAngles = XTeulerY;

            AvaXC.SetActive(true);
            Vector3 XCpos = new Vector3(XTpos.x + distTtoC2, XTpos.y, XTpos.z);
            AvaXC.transform.position = XCpos;
            AvaXC.transform.eulerAngles = XTeulerY;
        }

        if (viewData[ (numScene - 1 - 36) * 5] == 1)
        {
            AvaXT_Sit.SetActive(true);
            Vector3 XTpos = new Vector3(viewData[1 + (numScene - 1 - 36) * 5], viewData[2 + (numScene - 1 - 36) * 5], viewData[3 + (numScene - 1 - 36) * 5]);
            AvaXT_Sit.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData[4 + (numScene - 1 - 36) * 5], 0f);
            AvaXT_Sit.transform.eulerAngles = XTeulerY;

            AvaXC_Sit.SetActive(true);
            Vector3 XCpos = new Vector3(XTpos.x + distTtoC2, XTpos.y, XTpos.z);
            AvaXC_Sit.transform.position = XCpos;
            AvaXC_Sit.transform.eulerAngles = XTeulerY;
        }
    }
    void AllUserData()//For selected numPair and numScene, but all users...1-10
    {
        string tempFileName;
        string line;
        string[] numbers;

        for (int i = 1; i <= 10; i++)
        {
            tempFileName = Application.dataPath + "\\testdata\\pair" + numPair.ToString() + "user" + i.ToString() + ".txt";
            sr = new StreamReader(@tempFileName);
            line = sr.ReadLine();
            numbers = line.Split(' ');

            if (i == 1)
            {
                viewData1 = new float[numbers.Count()];
                for (int j = 0; j < numbers.Count(); j++)
                {
                    float hard;
                    hard = float.Parse(numbers[j]);
                    viewData1[j] = hard;
                }
            }
            if (i == 2)
            {
                viewData2 = new float[numbers.Count()];
                for (int j = 0; j < numbers.Count(); j++)
                {
                    float hard;
                    hard = float.Parse(numbers[j]);
                    viewData2[j] = hard;
                }
            }
            if (i == 3)
            {
                viewData3 = new float[numbers.Count()];
                for (int j = 0; j < numbers.Count(); j++)
                {
                    float hard;
                    hard = float.Parse(numbers[j]);
                    viewData3[j] = hard;
                }
            }
            if (i == 4)
            {
                viewData4 = new float[numbers.Count()];
                for (int j = 0; j < numbers.Count(); j++)
                {
                    float hard;
                    hard = float.Parse(numbers[j]);
                    viewData4[j] = hard;
                }
            }
            if (i == 5)
            {
                viewData5 = new float[numbers.Count()];
                for (int j = 0; j < numbers.Count(); j++)
                {
                    float hard;
                    hard = float.Parse(numbers[j]);
                    viewData5[j] = hard;
                }
            }
            if (i == 6)
            {
                viewData6 = new float[numbers.Count()];
                for (int j = 0; j < numbers.Count(); j++)
                {
                    float hard;
                    hard = float.Parse(numbers[j]);
                    viewData6[j] = hard;
                }
            }
            if (i == 7)
            {
                viewData7 = new float[numbers.Count()];
                for (int j = 0; j < numbers.Count(); j++)
                {
                    float hard;
                    hard = float.Parse(numbers[j]);
                    viewData7[j] = hard;
                }
            }
            if (i == 8)
            {
                viewData8 = new float[numbers.Count()];
                for (int j = 0; j < numbers.Count(); j++)
                {
                    float hard;
                    hard = float.Parse(numbers[j]);
                    viewData8[j] = hard;
                }
            }
            if (i == 9)
            {
                viewData9 = new float[numbers.Count()];
                for (int j = 0; j < numbers.Count(); j++)
                {
                    float hard;
                    hard = float.Parse(numbers[j]);
                    viewData9[j] = hard;
                }
            }
            if (i == 10)
            {
                viewData10 = new float[numbers.Count()];
                for (int j = 0; j < numbers.Count(); j++)
                {
                    float hard;
                    hard = float.Parse(numbers[j]);
                    viewData10[j] = hard;
                }
            }
        }


        if (viewData1[(numScene - 1) * 5] == 0)
        {
            AvaXT_1.SetActive(true);
            AvaXT_Sit_1.SetActive(false);
            Vector3 XTpos = new Vector3(viewData1[1 + (numScene - 1) * 5], viewData1[2 + (numScene - 1) * 5], viewData1[3 + (numScene - 1) * 5]);
            AvaXT_1.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData1[4 + (numScene - 1) * 5], 0f);
            AvaXT_1.transform.eulerAngles = XTeulerY;

            AvaXC_1.SetActive(true);
            AvaXC_Sit_1.SetActive(false);
            Vector3 XCpos = new Vector3(XTpos.x + distTtoC2, XTpos.y, XTpos.z);
            AvaXC_1.transform.position = XCpos;
            AvaXC_1.transform.eulerAngles = XTeulerY;
        }

        if (viewData1[(numScene - 1) * 5] == 1)
        {
            AvaXT_Sit_1.SetActive(true);
            AvaXT_1.SetActive(false);
            Vector3 XTpos = new Vector3(viewData1[1 + (numScene - 1) * 5], viewData1[2 + (numScene - 1) * 5], viewData1[3 + (numScene - 1) * 5]);
            AvaXT_Sit_1.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData1[4 + (numScene - 1) * 5], 0f);
            AvaXT_Sit_1.transform.eulerAngles = XTeulerY;

            AvaXC_Sit_1.SetActive(true);
            AvaXC_1.SetActive(false);
            Vector3 XCpos = new Vector3(XTpos.x + distTtoC2, XTpos.y, XTpos.z);
            AvaXC_Sit_1.transform.position = XCpos;
            AvaXC_Sit_1.transform.eulerAngles = XTeulerY;
        }

        if (viewData2[(numScene - 1) * 5] == 0)
        {
            AvaXT_2.SetActive(true);
            AvaXT_Sit_2.SetActive(false);
            Vector3 XTpos = new Vector3(viewData2[1 + (numScene - 1) * 5], viewData2[2 + (numScene - 1) * 5], viewData2[3 + (numScene - 1) * 5]);
            AvaXT_2.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData2[4 + (numScene - 1) * 5], 0f);
            AvaXT_2.transform.eulerAngles = XTeulerY;
        }

        if (viewData2[(numScene - 1) * 5] == 1)
        {
            AvaXT_Sit_2.SetActive(true);
            AvaXT_2.SetActive(false);
            Vector3 XTpos = new Vector3(viewData2[1 + (numScene - 1) * 5], viewData2[2 + (numScene - 1) * 5], viewData2[3 + (numScene - 1) * 5]);
            AvaXT_Sit_2.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData2[4 + (numScene - 1) * 5], 0f);
            AvaXT_Sit_2.transform.eulerAngles = XTeulerY;
        }

        if (viewData3[(numScene - 1) * 5] == 0)
        {
            AvaXT_3.SetActive(true);
            AvaXT_Sit_3.SetActive(false);
            Vector3 XTpos = new Vector3(viewData3[1 + (numScene - 1) * 5], viewData3[2 + (numScene - 1) * 5], viewData3[3 + (numScene - 1) * 5]);
            AvaXT_3.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData3[4 + (numScene - 1) * 5], 0f);
            AvaXT_3.transform.eulerAngles = XTeulerY;
        }

        if (viewData3[(numScene - 1) * 5] == 1)
        {
            AvaXT_Sit_3.SetActive(true);
            AvaXT_3.SetActive(false);
            Vector3 XTpos = new Vector3(viewData3[1 + (numScene - 1) * 5], viewData3[2 + (numScene - 1) * 5], viewData3[3 + (numScene - 1) * 5]);
            AvaXT_Sit_3.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData3[4 + (numScene - 1) * 5], 0f);
            AvaXT_Sit_3.transform.eulerAngles = XTeulerY;
        }

        if (viewData4[(numScene - 1) * 5] == 0)
        {
            AvaXT_4.SetActive(true);
            AvaXT_Sit_4.SetActive(false);
            Vector3 XTpos = new Vector3(viewData4[1 + (numScene - 1) * 5], viewData4[2 + (numScene - 1) * 5], viewData4[3 + (numScene - 1) * 5]);
            AvaXT_4.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData4[4 + (numScene - 1) * 5], 0f);
            AvaXT_4.transform.eulerAngles = XTeulerY;
        }

        if (viewData4[(numScene - 1) * 5] == 1)
        {
            AvaXT_Sit_4.SetActive(true);
            AvaXT_4.SetActive(false);
            Vector3 XTpos = new Vector3(viewData4[1 + (numScene - 1) * 5], viewData4[2 + (numScene - 1) * 5], viewData4[3 + (numScene - 1) * 5]);
            AvaXT_Sit_4.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData4[4 + (numScene - 1) * 5], 0f);
            AvaXT_Sit_4.transform.eulerAngles = XTeulerY;
        }

        if (viewData5[(numScene - 1) * 5] == 0)
        {
            AvaXT_5.SetActive(true);
            AvaXT_Sit_5.SetActive(false);
            Vector3 XTpos = new Vector3(viewData5[1 + (numScene - 1) * 5], viewData5[2 + (numScene - 1) * 5], viewData5[3 + (numScene - 1) * 5]);
            AvaXT_5.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData5[4 + (numScene - 1) * 5], 0f);
            AvaXT_5.transform.eulerAngles = XTeulerY;
        }

        if (viewData5[(numScene - 1) * 5] == 1)
        {
            AvaXT_Sit_5.SetActive(true);
            AvaXT_5.SetActive(false);
            Vector3 XTpos = new Vector3(viewData5[1 + (numScene - 1) * 5], viewData5[2 + (numScene - 1) * 5], viewData5[3 + (numScene - 1) * 5]);
            AvaXT_Sit_5.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData5[4 + (numScene - 1) * 5], 0f);
            AvaXT_Sit_5.transform.eulerAngles = XTeulerY;
        }

        if (viewData6[(numScene - 1) * 5] == 0)
        {
            AvaXT_6.SetActive(true);
            AvaXT_Sit_6.SetActive(false);
            Vector3 XTpos = new Vector3(viewData6[1 + (numScene - 1) * 5], viewData6[2 + (numScene - 1) * 5], viewData6[3 + (numScene - 1) * 5]);
            AvaXT_6.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData6[4 + (numScene - 1) * 5], 0f);
            AvaXT_6.transform.eulerAngles = XTeulerY;
        }

        if (viewData6[(numScene - 1) * 5] == 1)
        {
            AvaXT_Sit_6.SetActive(true);
            AvaXT_6.SetActive(false);
            Vector3 XTpos = new Vector3(viewData6[1 + (numScene - 1) * 5], viewData6[2 + (numScene - 1) * 5], viewData6[3 + (numScene - 1) * 5]);
            AvaXT_Sit_6.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData6[4 + (numScene - 1) * 5], 0f);
            AvaXT_Sit_6.transform.eulerAngles = XTeulerY;
        }

        if (viewData7[(numScene - 1) * 5] == 0)
        {
            AvaXT_7.SetActive(true);
            AvaXT_Sit_7.SetActive(false);
            Vector3 XTpos = new Vector3(viewData7[1 + (numScene - 1) * 5], viewData7[2 + (numScene - 1) * 5], viewData7[3 + (numScene - 1) * 5]);
            AvaXT_7.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData7[4 + (numScene - 1) * 5], 0f);
            AvaXT_7.transform.eulerAngles = XTeulerY;
        }

        if (viewData7[(numScene - 1) * 5] == 1)
        {
            AvaXT_Sit_7.SetActive(true);
            AvaXT_7.SetActive(false);
            Vector3 XTpos = new Vector3(viewData7[1 + (numScene - 1) * 5], viewData7[2 + (numScene - 1) * 5], viewData7[3 + (numScene - 1) * 5]);
            AvaXT_Sit_7.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData7[4 + (numScene - 1) * 5], 0f);
            AvaXT_Sit_7.transform.eulerAngles = XTeulerY;
        }

        if (viewData8[(numScene - 1) * 5] == 0)
        {
            AvaXT_8.SetActive(true);
            AvaXT_Sit_8.SetActive(false);
            Vector3 XTpos = new Vector3(viewData8[1 + (numScene - 1) * 5], viewData8[2 + (numScene - 1) * 5], viewData8[3 + (numScene - 1) * 5]);
            AvaXT_8.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData8[4 + (numScene - 1) * 5], 0f);
            AvaXT_8.transform.eulerAngles = XTeulerY;
        }

        if (viewData8[(numScene - 1) * 5] == 1)
        {
            AvaXT_Sit_8.SetActive(true);
            AvaXT_8.SetActive(false);
            Vector3 XTpos = new Vector3(viewData8[1 + (numScene - 1) * 5], viewData8[2 + (numScene - 1) * 5], viewData8[3 + (numScene - 1) * 5]);
            AvaXT_Sit_8.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData8[4 + (numScene - 1) * 5], 0f);
            AvaXT_Sit_8.transform.eulerAngles = XTeulerY;
        }

        if (viewData9[(numScene - 1) * 5] == 0)
        {
            AvaXT_9.SetActive(true);
            AvaXT_Sit_9.SetActive(false);
            Vector3 XTpos = new Vector3(viewData9[1 + (numScene - 1) * 5], viewData9[2 + (numScene - 1) * 5], viewData9[3 + (numScene - 1) * 5]);
            AvaXT_9.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData9[4 + (numScene - 1) * 5], 0f);
            AvaXT_9.transform.eulerAngles = XTeulerY;
        }

        if (viewData9[(numScene - 1) * 5] == 1)
        {
            AvaXT_Sit_9.SetActive(true);
            AvaXT_9.SetActive(false);
            Vector3 XTpos = new Vector3(viewData9[1 + (numScene - 1) * 5], viewData9[2 + (numScene - 1) * 5], viewData9[3 + (numScene - 1) * 5]);
            AvaXT_Sit_9.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData9[4 + (numScene - 1) * 5], 0f);
            AvaXT_Sit_9.transform.eulerAngles = XTeulerY;
        }

        if (viewData10[(numScene - 1) * 5] == 0)
        {
            AvaXT_10.SetActive(true);
            AvaXT_Sit_10.SetActive(false);
            Vector3 XTpos = new Vector3(viewData10[1 + (numScene - 1) * 5], viewData10[2 + (numScene - 1) * 5], viewData10[3 + (numScene - 1) * 5]);
            AvaXT_10.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData10[4 + (numScene - 1) * 5], 0f);
            AvaXT_10.transform.eulerAngles = XTeulerY;
        }

        if (viewData10[(numScene - 1) * 5] == 1)
        {
            AvaXT_Sit_10.SetActive(true);
            AvaXT_10.SetActive(false);
            Vector3 XTpos = new Vector3(viewData10[1 + (numScene - 1) * 5], viewData10[2 + (numScene - 1) * 5], viewData10[3 + (numScene - 1) * 5]);
            AvaXT_Sit_10.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData10[4 + (numScene - 1) * 5], 0f);
            AvaXT_Sit_10.transform.eulerAngles = XTeulerY;
        }
    }
    void AllUserData_48()//For selected numPair and numScene, but all users...1-10
    {
        string tempFileName;
        string line;
        string[] numbers;

        for (int i = 1; i <= 10; i++)
        {
            tempFileName = Application.dataPath + "\\testdata_48\\pair" + numPair.ToString() + "user" + i.ToString() + "_48.txt";
            sr = new StreamReader(@tempFileName);
            line = sr.ReadLine();
            numbers = line.Split(' ');

            if (i == 1)
            {
                viewData1 = new float[numbers.Count()];
                for (int j = 0; j < numbers.Count(); j++)
                {
                    float hard;
                    hard = float.Parse(numbers[j]);
                    viewData1[j] = hard;
                }
            }
            if (i == 2)
            {
                viewData2 = new float[numbers.Count()];
                for (int j = 0; j < numbers.Count(); j++)
                {
                    float hard;
                    hard = float.Parse(numbers[j]);
                    viewData2[j] = hard;
                }
            }
            if (i == 3)
            {
                viewData3 = new float[numbers.Count()];
                for (int j = 0; j < numbers.Count(); j++)
                {
                    float hard;
                    hard = float.Parse(numbers[j]);
                    viewData3[j] = hard;
                }
            }
            if (i == 4)
            {
                viewData4 = new float[numbers.Count()];
                for (int j = 0; j < numbers.Count(); j++)
                {
                    float hard;
                    hard = float.Parse(numbers[j]);
                    viewData4[j] = hard;
                }
            }
            if (i == 5)
            {
                viewData5 = new float[numbers.Count()];
                for (int j = 0; j < numbers.Count(); j++)
                {
                    float hard;
                    hard = float.Parse(numbers[j]);
                    viewData5[j] = hard;
                }
            }
            if (i == 6)
            {
                viewData6 = new float[numbers.Count()];
                for (int j = 0; j < numbers.Count(); j++)
                {
                    float hard;
                    hard = float.Parse(numbers[j]);
                    viewData6[j] = hard;
                }
            }
            if (i == 7)
            {
                viewData7 = new float[numbers.Count()];
                for (int j = 0; j < numbers.Count(); j++)
                {
                    float hard;
                    hard = float.Parse(numbers[j]);
                    viewData7[j] = hard;
                }
            }
            if (i == 8)
            {
                viewData8 = new float[numbers.Count()];
                for (int j = 0; j < numbers.Count(); j++)
                {
                    float hard;
                    hard = float.Parse(numbers[j]);
                    viewData8[j] = hard;
                }
            }
            if (i == 9)
            {
                viewData9 = new float[numbers.Count()];
                for (int j = 0; j < numbers.Count(); j++)
                {
                    float hard;
                    hard = float.Parse(numbers[j]);
                    viewData9[j] = hard;
                }
            }
            if (i == 10)
            {
                viewData10 = new float[numbers.Count()];
                for (int j = 0; j < numbers.Count(); j++)
                {
                    float hard;
                    hard = float.Parse(numbers[j]);
                    viewData10[j] = hard;
                }
            }
        }


        if (viewData1[(numScene - 1 - 36) * 5] == 0)
        {
            AvaXT_1.SetActive(true);
            AvaXT_Sit_1.SetActive(false);
            Vector3 XTpos = new Vector3(viewData1[1 + (numScene - 1 - 36) * 5], viewData1[2 + (numScene - 1 - 36) * 5], viewData1[3 + (numScene - 1 - 36) * 5]);
            AvaXT_1.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData1[4 + (numScene - 1 - 36) * 5], 0f);
            AvaXT_1.transform.eulerAngles = XTeulerY;

            AvaXC_1.SetActive(true);
            AvaXC_Sit_1.SetActive(false);
            Vector3 XCpos = new Vector3(XTpos.x + distTtoC2, XTpos.y, XTpos.z);
            AvaXC_1.transform.position = XCpos;
            AvaXC_1.transform.eulerAngles = XTeulerY;
        }

        if (viewData1[(numScene - 1 - 36) * 5] == 1)
        {
            AvaXT_Sit_1.SetActive(true);
            AvaXT_1.SetActive(false);
            Vector3 XTpos = new Vector3(viewData1[1 + (numScene - 1 - 36) * 5], viewData1[2 + (numScene - 1 - 36) * 5], viewData1[3 + (numScene - 1 - 36) * 5]);
            AvaXT_Sit_1.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData1[4 + (numScene - 1 - 36) * 5], 0f);
            AvaXT_Sit_1.transform.eulerAngles = XTeulerY;

            AvaXC_Sit_1.SetActive(true);
            AvaXC_1.SetActive(false);
            Vector3 XCpos = new Vector3(XTpos.x + distTtoC2, XTpos.y, XTpos.z);
            AvaXC_Sit_1.transform.position = XCpos;
            AvaXC_Sit_1.transform.eulerAngles = XTeulerY;
        }

        if (viewData2[(numScene - 1 - 36) * 5] == 0)
        {
            AvaXT_2.SetActive(true);
            AvaXT_Sit_2.SetActive(false);
            Vector3 XTpos = new Vector3(viewData2[1 + (numScene - 1 - 36) * 5], viewData2[2 + (numScene - 1 - 36) * 5], viewData2[3 + (numScene - 1 - 36) * 5]);
            AvaXT_2.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData2[4 + (numScene - 1 - 36) * 5], 0f);
            AvaXT_2.transform.eulerAngles = XTeulerY;
        }

        if (viewData2[(numScene - 1 - 36) * 5] == 1)
        {
            AvaXT_Sit_2.SetActive(true);
            AvaXT_2.SetActive(false);
            Vector3 XTpos = new Vector3(viewData2[1 + (numScene - 1 - 36) * 5], viewData2[2 + (numScene - 1 - 36) * 5], viewData2[3 + (numScene - 1 - 36) * 5]);
            AvaXT_Sit_2.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData2[4 + (numScene - 1 - 36) * 5], 0f);
            AvaXT_Sit_2.transform.eulerAngles = XTeulerY;
        }

        if (viewData3[(numScene - 1 - 36) * 5] == 0)
        {
            AvaXT_3.SetActive(true);
            AvaXT_Sit_3.SetActive(false);
            Vector3 XTpos = new Vector3(viewData3[1 + (numScene - 1 - 36) * 5], viewData3[2 + (numScene - 1 - 36) * 5], viewData3[3 + (numScene - 1 - 36) * 5]);
            AvaXT_3.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData3[4 + (numScene - 1 - 36) * 5], 0f);
            AvaXT_3.transform.eulerAngles = XTeulerY;
        }

        if (viewData3[(numScene - 1 - 36) * 5] == 1)
        {
            AvaXT_Sit_3.SetActive(true);
            AvaXT_3.SetActive(false);
            Vector3 XTpos = new Vector3(viewData3[1 + (numScene - 1 - 36) * 5], viewData3[2 + (numScene - 1 - 36) * 5], viewData3[3 + (numScene - 1 - 36) * 5]);
            AvaXT_Sit_3.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData3[4 + (numScene - 1 - 36) * 5], 0f);
            AvaXT_Sit_3.transform.eulerAngles = XTeulerY;
        }

        if (viewData4[(numScene - 1 - 36) * 5] == 0)
        {
            AvaXT_4.SetActive(true);
            AvaXT_Sit_4.SetActive(false);
            Vector3 XTpos = new Vector3(viewData4[1 + (numScene - 1 - 36) * 5], viewData4[2 + (numScene - 1 - 36) * 5], viewData4[3 + (numScene - 1 - 36) * 5]);
            AvaXT_4.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData4[4 + (numScene - 1 - 36) * 5], 0f);
            AvaXT_4.transform.eulerAngles = XTeulerY;
        }

        if (viewData4[(numScene - 1 - 36) * 5] == 1)
        {
            AvaXT_Sit_4.SetActive(true);
            AvaXT_4.SetActive(false);
            Vector3 XTpos = new Vector3(viewData4[1 + (numScene - 1 - 36) * 5], viewData4[2 + (numScene - 1 - 36) * 5], viewData4[3 + (numScene - 1 - 36) * 5]);
            AvaXT_Sit_4.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData4[4 + (numScene - 1 - 36) * 5], 0f);
            AvaXT_Sit_4.transform.eulerAngles = XTeulerY;
        }

        if (viewData5[(numScene - 1 - 36) * 5] == 0)
        {
            AvaXT_5.SetActive(true);
            AvaXT_Sit_5.SetActive(false);
            Vector3 XTpos = new Vector3(viewData5[1 + (numScene - 1 - 36) * 5], viewData5[2 + (numScene - 1 - 36) * 5], viewData5[3 + (numScene - 1 - 36) * 5]);
            AvaXT_5.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData5[4 + (numScene - 1 - 36) * 5], 0f);
            AvaXT_5.transform.eulerAngles = XTeulerY;
        }

        if (viewData5[(numScene - 1 - 36) * 5] == 1)
        {
            AvaXT_Sit_5.SetActive(true);
            AvaXT_5.SetActive(false);
            Vector3 XTpos = new Vector3(viewData5[1 + (numScene - 1 - 36) * 5], viewData5[2 + (numScene - 1 - 36) * 5], viewData5[3 + (numScene - 1 - 36) * 5]);
            AvaXT_Sit_5.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData5[4 + (numScene - 1 - 36) * 5], 0f);
            AvaXT_Sit_5.transform.eulerAngles = XTeulerY;
        }

        if (viewData6[(numScene - 1 - 36) * 5] == 0)
        {
            AvaXT_6.SetActive(true);
            AvaXT_Sit_6.SetActive(false);
            Vector3 XTpos = new Vector3(viewData6[1 + (numScene - 1 - 36) * 5], viewData6[2 + (numScene - 1 - 36) * 5], viewData6[3 + (numScene - 1 - 36) * 5]);
            AvaXT_6.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData6[4 + (numScene - 1 - 36) * 5], 0f);
            AvaXT_6.transform.eulerAngles = XTeulerY;
        }

        if (viewData6[(numScene - 1 - 36) * 5] == 1)
        {
            AvaXT_Sit_6.SetActive(true);
            AvaXT_6.SetActive(false);
            Vector3 XTpos = new Vector3(viewData6[1 + (numScene - 1 - 36) * 5], viewData6[2 + (numScene - 1 - 36) * 5], viewData6[3 + (numScene - 1 - 36) * 5]);
            AvaXT_Sit_6.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData6[4 + (numScene - 1 - 36) * 5], 0f);
            AvaXT_Sit_6.transform.eulerAngles = XTeulerY;
        }

        if (viewData7[(numScene - 1 - 36) * 5] == 0)
        {
            AvaXT_7.SetActive(true);
            AvaXT_Sit_7.SetActive(false);
            Vector3 XTpos = new Vector3(viewData7[1 + (numScene - 1 - 36) * 5], viewData7[2 + (numScene - 1 - 36) * 5], viewData7[3 + (numScene - 1 - 36) * 5]);
            AvaXT_7.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData7[4 + (numScene - 1 - 36) * 5], 0f);
            AvaXT_7.transform.eulerAngles = XTeulerY;
        }

        if (viewData7[(numScene - 1 - 36) * 5] == 1)
        {
            AvaXT_Sit_7.SetActive(true);
            AvaXT_7.SetActive(false);
            Vector3 XTpos = new Vector3(viewData7[1 + (numScene - 1 - 36) * 5], viewData7[2 + (numScene - 1 - 36) * 5], viewData7[3 + (numScene - 1 - 36) * 5]);
            AvaXT_Sit_7.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData7[4 + (numScene - 1 - 36) * 5], 0f);
            AvaXT_Sit_7.transform.eulerAngles = XTeulerY;
        }

        if (viewData8[(numScene - 1 - 36) * 5] == 0)
        {
            AvaXT_8.SetActive(true);
            AvaXT_Sit_8.SetActive(false);
            Vector3 XTpos = new Vector3(viewData8[1 + (numScene - 1 - 36) * 5], viewData8[2 + (numScene - 1 - 36) * 5], viewData8[3 + (numScene - 1 - 36) * 5]);
            AvaXT_8.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData8[4 + (numScene - 1 - 36) * 5], 0f);
            AvaXT_8.transform.eulerAngles = XTeulerY;
        }

        if (viewData8[(numScene - 1 - 36) * 5] == 1)
        {
            AvaXT_Sit_8.SetActive(true);
            AvaXT_8.SetActive(false);
            Vector3 XTpos = new Vector3(viewData8[1 + (numScene - 1 - 36) * 5], viewData8[2 + (numScene - 1 - 36) * 5], viewData8[3 + (numScene - 1 - 36) * 5]);
            AvaXT_Sit_8.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData8[4 + (numScene - 1 - 36) * 5], 0f);
            AvaXT_Sit_8.transform.eulerAngles = XTeulerY;
        }

        if (viewData9[(numScene - 1 - 36) * 5] == 0)
        {
            AvaXT_9.SetActive(true);
            AvaXT_Sit_9.SetActive(false);
            Vector3 XTpos = new Vector3(viewData9[1 + (numScene - 1 - 36) * 5], viewData9[2 + (numScene - 1 - 36) * 5], viewData9[3 + (numScene - 1 - 36) * 5]);
            AvaXT_9.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData9[4 + (numScene - 1 - 36) * 5], 0f);
            AvaXT_9.transform.eulerAngles = XTeulerY;
        }

        if (viewData9[(numScene - 1 - 36) * 5] == 1)
        {
            AvaXT_Sit_9.SetActive(true);
            AvaXT_9.SetActive(false);
            Vector3 XTpos = new Vector3(viewData9[1 + (numScene - 1 - 36) * 5], viewData9[2 + (numScene - 1 - 36) * 5], viewData9[3 + (numScene - 1 - 36) * 5]);
            AvaXT_Sit_9.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData9[4 + (numScene - 1 - 36) * 5], 0f);
            AvaXT_Sit_9.transform.eulerAngles = XTeulerY;
        }

        if (viewData10[(numScene - 1 - 36) * 5] == 0)
        {
            AvaXT_10.SetActive(true);
            AvaXT_Sit_10.SetActive(false);
            Vector3 XTpos = new Vector3(viewData10[1 + (numScene - 1 - 36) * 5], viewData10[2 + (numScene - 1 - 36) * 5], viewData10[3 + (numScene - 1 - 36) * 5]);
            AvaXT_10.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData10[4 + (numScene - 1 - 36) * 5], 0f);
            AvaXT_10.transform.eulerAngles = XTeulerY;
        }

        if (viewData10[(numScene - 1 - 36) * 5] == 1)
        {
            AvaXT_Sit_10.SetActive(true);
            AvaXT_10.SetActive(false);
            Vector3 XTpos = new Vector3(viewData10[1 + (numScene - 1 - 36) * 5], viewData10[2 + (numScene - 1 - 36) * 5], viewData10[3 + (numScene - 1 - 36) * 5]);
            AvaXT_Sit_10.transform.position = XTpos;
            Vector3 XTeulerY = new Vector3(0f, viewData10[4 + (numScene - 1 - 36) * 5], 0f);
            AvaXT_Sit_10.transform.eulerAngles = XTeulerY;
        }
    }
    void AllDataFromText() //Obsolete? (numPair)user1-user10 __ text file to viewData1 
    {
        string tempFileName;
        string line;
        string[] numbers;
        
        for (int i=1 ; i <= 10;i++) //10 Users with same pair
        {
            tempFileName = Application.dataPath + "\\testdata_48\\pair" + numPair.ToString() + "user" + i.ToString() + "_48.txt";
            sr = new StreamReader(@tempFileName);
            line = sr.ReadLine();
            numbers = line.Split(' ');

            if (i == 1)
            {
                viewData1 = new float[numbers.Count()];
                for (int j = 0; j < numbers.Count(); j++)
                {
                    float hard;
                    hard = float.Parse(numbers[j]);
                    viewData1[j] = hard;
                }
            }
            if (i == 2)
            {
                viewData2 = new float[numbers.Count()];
                for (int j = 0; j < numbers.Count(); j++)
                {
                    float hard;
                    hard = float.Parse(numbers[j]);
                    viewData2[j] = hard;
                }
            }
            if (i == 3)
            {
                viewData3 = new float[numbers.Count()];
                for (int j = 0; j < numbers.Count(); j++)
                {
                    float hard;
                    hard = float.Parse(numbers[j]);
                    viewData3[j] = hard;
                }
            }
            if (i == 4)
            {
                viewData4 = new float[numbers.Count()];
                for (int j = 0; j < numbers.Count(); j++)
                {
                    float hard;
                    hard = float.Parse(numbers[j]);
                    viewData4[j] = hard;
                }
            }
            if (i == 5)
            {
                viewData5 = new float[numbers.Count()];
                for (int j = 0; j < numbers.Count(); j++)
                {
                    float hard;
                    hard = float.Parse(numbers[j]);
                    viewData5[j] = hard;
                }
            }
            if (i == 6)
            {
                viewData6 = new float[numbers.Count()];
                for (int j = 0; j < numbers.Count(); j++)
                {
                    float hard;
                    hard = float.Parse(numbers[j]);
                    viewData6[j] = hard;
                }
            }
            if (i == 7)
            {
                viewData7 = new float[numbers.Count()];
                for (int j = 0; j < numbers.Count(); j++)
                {
                    float hard;
                    hard = float.Parse(numbers[j]);
                    viewData7[j] = hard;
                }
            }
            if (i == 8)
            {
                viewData8 = new float[numbers.Count()];
                for (int j = 0; j < numbers.Count(); j++)
                {
                    float hard;
                    hard = float.Parse(numbers[j]);
                    viewData8[j] = hard;
                }
            }
            if (i == 9)
            {
                viewData9 = new float[numbers.Count()];
                for (int j = 0; j < numbers.Count(); j++)
                {
                    float hard;
                    hard = float.Parse(numbers[j]);
                    viewData9[j] = hard;
                }
            }
            if (i == 10)
            {
                viewData10 = new float[numbers.Count()];
                for (int j = 0; j < numbers.Count(); j++)
                {
                    float hard;
                    hard = float.Parse(numbers[j]);
                    viewData10[j] = hard;
                }
            }
        }
    } 
    void ViewScene()
    {
        //string fileName = "c:\\EXP\\scene\\pair" + numPair.ToString() + "scene" + ".txt";
        string fileName = Application.dataPath + "\\scene\\pair" + numPair.ToString() + "scene" + ".txt";
        sr = new StreamReader(@fileName);
        string line;
        line = sr.ReadLine();
        string[] numbers = line.Split(' ');
        float[] viewData = new float[numbers.Count()];
        for (int i = 0; i < numbers.Count(); i++)
        {
            float hard;
            hard = float.Parse(numbers[i]);
            viewData[i] = hard;
        }
        if(numScene>0 && numScene<37)
        { 
            if (viewData[ (numScene - 1) * 15] == 0)
            {
                HumXT.SetActive(true);
                Vector3 XTpos = new Vector3(viewData[1 + (numScene - 1) * 15], viewData[2 + (numScene - 1) * 15], viewData[3 + (numScene - 1) * 15]);
                HumXT.transform.position = XTpos;
                Vector3 XTeulerY = new Vector3(0f, viewData[4 + (numScene - 1) * 15], 0f);
                HumXT.transform.eulerAngles = XTeulerY;

                HumXC.SetActive(true);
                Vector3 XCpos = new Vector3(XTpos.x - distTtoC1, XTpos.y, XTpos.z);
                HumXC.transform.position = XCpos;
                HumXC.transform.eulerAngles = XTeulerY;
            }

            if (viewData[ (numScene - 1) * 15] == 1)
            {
                HumXT_Sit.SetActive(true);
                Vector3 XTpos = new Vector3(viewData[1 + (numScene - 1) * 15], viewData[2 + (numScene - 1) * 15], viewData[3 + (numScene - 1) * 15]);
                HumXT_Sit.transform.position = XTpos;
                Vector3 XTeulerY = new Vector3(0f, viewData[4 + (numScene - 1) * 15], 0f);
                HumXT_Sit.transform.eulerAngles = XTeulerY;

                HumXC_Sit.SetActive(true);
                Vector3 XCpos = new Vector3(XTpos.x - distTtoC1, XTpos.y, XTpos.z);
                HumXC_Sit.transform.position = XCpos;
                HumXC_Sit.transform.eulerAngles = XTeulerY;
            }
            if(numScene>=13&&numScene<=36)
            {
                if (viewData[(numScene - 1) * 15+5] == 0)
                {
                    AvaYT.SetActive(true);
                    Vector3 XTpos = new Vector3(viewData[6 + (numScene - 1) * 15], viewData[7 + (numScene - 1) * 15], viewData[8 + (numScene - 1) * 15]);
                    AvaYT.transform.position = XTpos;
                    Vector3 XTeulerY = new Vector3(0f, viewData[9 + (numScene - 1) * 15], 0f);
                    AvaYT.transform.eulerAngles = XTeulerY;

                    AvaYC.SetActive(true);
                    Vector3 XCpos = new Vector3(XTpos.x - distTtoC1, XTpos.y, XTpos.z);
                    AvaYC.transform.position = XCpos;
                    AvaYC.transform.eulerAngles = XTeulerY;
                }

                if (viewData[(numScene - 1) * 15+5] == 1)
                {
                    AvaYT_Sit.SetActive(true);
                    Vector3 XTpos = new Vector3(viewData[6 + (numScene - 1) * 15], viewData[7 + (numScene - 1) * 15], viewData[8 + (numScene - 1) * 15]);
                    AvaYT_Sit.transform.position = XTpos;
                    Vector3 XTeulerY = new Vector3(0f, viewData[9 + (numScene - 1) * 15], 0f);
                    AvaYT_Sit.transform.eulerAngles = XTeulerY;

                    AvaYC_Sit.SetActive(true);
                    Vector3 XCpos = new Vector3(XTpos.x - distTtoC1, XTpos.y, XTpos.z);
                    AvaYC_Sit.transform.position = XCpos;
                    AvaYC_Sit.transform.eulerAngles = XTeulerY;
                }

                if (viewData[(numScene - 1) * 15 + 10] == 0)
                {
                    HumYT.SetActive(true);
                    Vector3 XTpos = new Vector3(viewData[11 + (numScene - 1) * 15], viewData[12 + (numScene - 1) * 15], viewData[13 + (numScene - 1) * 15]);
                    HumYT.transform.position = XTpos;
                    Vector3 XTeulerY = new Vector3(0f, viewData[14 + (numScene - 1) * 15], 0f);
                    HumYT.transform.eulerAngles = XTeulerY;

                    HumYC.SetActive(true);
                    Vector3 XCpos = new Vector3(XTpos.x + distTtoC2, XTpos.y, XTpos.z);
                    HumYC.transform.position = XCpos;
                    HumYC.transform.eulerAngles = XTeulerY;
                }

                if (viewData[(numScene - 1) * 15 + 10] == 1)
                {
                    HumYT_Sit.SetActive(true);
                    Vector3 XTpos = new Vector3(viewData[11 + (numScene - 1) * 15], viewData[12 + (numScene - 1) * 15], viewData[13 + (numScene - 1) * 15]);
                    HumYT_Sit.transform.position = XTpos;
                    Vector3 XTeulerY = new Vector3(0f, viewData[14 + (numScene - 1) * 15], 0f);
                    HumYT_Sit.transform.eulerAngles = XTeulerY;

                    HumYC_Sit.SetActive(true);
                    Vector3 XCpos = new Vector3(XTpos.x + distTtoC2, XTpos.y, XTpos.z);
                    HumYC_Sit.transform.position = XCpos;
                    HumYC_Sit.transform.eulerAngles = XTeulerY;
                }
            }
        }

    }
    void ViewScene_48()
    {
        //string fileName = "c:\\EXP\\scene\\pair" + numPair.ToString() + "scene" + ".txt";
        string fileName = Application.dataPath + "\\scene_48\\pair" + numPair.ToString() + "scene_48" + ".txt";
        sr = new StreamReader(@fileName);
        string line;
        line = sr.ReadLine();
        string[] numbers = line.Split(' ');
        float[] viewData = new float[numbers.Count()];
        for (int i = 0; i < numbers.Count(); i++)
        {
            float hard;
            hard = float.Parse(numbers[i]);
            viewData[i] = hard;
        }
        if (numScene > 36 && numScene < 49)
        {
            if (viewData[(numScene - 1 - 36) * 15] == 0)
            {
                HumXT.SetActive(true);
                Vector3 XTpos = new Vector3(viewData[1 + (numScene - 1 - 36) * 15], viewData[2 + (numScene - 1 - 36) * 15], viewData[3 + (numScene - 1 - 36) * 15]);
                HumXT.transform.position = XTpos;
                Vector3 XTeulerY = new Vector3(0f, viewData[4 + (numScene - 1 - 36) * 15], 0f);
                HumXT.transform.eulerAngles = XTeulerY;

                HumXC.SetActive(true);
                Vector3 XCpos = new Vector3(XTpos.x - distTtoC1, XTpos.y, XTpos.z);
                HumXC.transform.position = XCpos;
                HumXC.transform.eulerAngles = XTeulerY;
            }

            if (viewData[(numScene - 1 - 36) * 15] == 1)
            {
                HumXT_Sit.SetActive(true);
                Vector3 XTpos = new Vector3(viewData[1 + (numScene - 1 - 36) * 15], viewData[2 + (numScene - 1 - 36) * 15], viewData[3 + (numScene - 1 - 36) * 15]);
                HumXT_Sit.transform.position = XTpos;
                Vector3 XTeulerY = new Vector3(0f, viewData[4 + (numScene - 1 - 36) * 15], 0f);
                HumXT_Sit.transform.eulerAngles = XTeulerY;

                HumXC_Sit.SetActive(true);
                Vector3 XCpos = new Vector3(XTpos.x - distTtoC1, XTpos.y, XTpos.z);
                HumXC_Sit.transform.position = XCpos;
                HumXC_Sit.transform.eulerAngles = XTeulerY;
            }
            if (numScene >= 37 && numScene <= 48)
            {
                if (viewData[(numScene - 1 - 36) * 15 + 5] == 0)
                {
                    AvaYT.SetActive(true);
                    Vector3 XTpos = new Vector3(viewData[6 + (numScene - 1 - 36) * 15], viewData[7 + (numScene - 1 - 36) * 15], viewData[8 + (numScene - 1 - 36) * 15]);
                    AvaYT.transform.position = XTpos;
                    Vector3 XTeulerY = new Vector3(0f, viewData[9 + (numScene - 1 - 36) * 15], 0f);
                    AvaYT.transform.eulerAngles = XTeulerY;

                    AvaYC.SetActive(true);
                    Vector3 XCpos = new Vector3(XTpos.x - distTtoC1, XTpos.y, XTpos.z);
                    AvaYC.transform.position = XCpos;
                    AvaYC.transform.eulerAngles = XTeulerY;
                }

                if (viewData[(numScene - 1 - 36) * 15 + 5] == 1)
                {
                    AvaYT_Sit.SetActive(true);
                    Vector3 XTpos = new Vector3(viewData[6 + (numScene - 1 - 36) * 15], viewData[7 + (numScene - 1 - 36) * 15], viewData[8 + (numScene - 1 - 36) * 15]);
                    AvaYT_Sit.transform.position = XTpos;
                    Vector3 XTeulerY = new Vector3(0f, viewData[9 + (numScene - 1 - 36) * 15], 0f);
                    AvaYT_Sit.transform.eulerAngles = XTeulerY;

                    AvaYC_Sit.SetActive(true);
                    Vector3 XCpos = new Vector3(XTpos.x - distTtoC1, XTpos.y, XTpos.z);
                    AvaYC_Sit.transform.position = XCpos;
                    AvaYC_Sit.transform.eulerAngles = XTeulerY;
                }

                if (viewData[(numScene - 1 - 36) * 15 + 10] == 0)
                {
                    HumYT.SetActive(true);
                    Vector3 XTpos = new Vector3(viewData[11 + (numScene - 1 - 36) * 15], viewData[12 + (numScene - 1 - 36) * 15], viewData[13 + (numScene - 1 - 36) * 15]);
                    HumYT.transform.position = XTpos;
                    Vector3 XTeulerY = new Vector3(0f, viewData[14 + (numScene - 1 - 36) * 15], 0f);
                    HumYT.transform.eulerAngles = XTeulerY;

                    HumYC.SetActive(true);
                    Vector3 XCpos = new Vector3(XTpos.x + distTtoC2, XTpos.y, XTpos.z);
                    HumYC.transform.position = XCpos;
                    HumYC.transform.eulerAngles = XTeulerY;
                }

                if (viewData[(numScene - 1 - 36) * 15 + 10] == 1)
                {
                    HumYT_Sit.SetActive(true);
                    Vector3 XTpos = new Vector3(viewData[11 + (numScene - 1 - 36) * 15], viewData[12 + (numScene - 1 - 36) * 15], viewData[13 + (numScene - 1 - 36) * 15]);
                    HumYT_Sit.transform.position = XTpos;
                    Vector3 XTeulerY = new Vector3(0f, viewData[14 + (numScene - 1 - 36) * 15], 0f);
                    HumYT_Sit.transform.eulerAngles = XTeulerY;

                    HumYC_Sit.SetActive(true);
                    Vector3 XCpos = new Vector3(XTpos.x + distTtoC2, XTpos.y, XTpos.z);
                    HumYC_Sit.transform.position = XCpos;
                    HumYC_Sit.transform.eulerAngles = XTeulerY;
                }
            }
        }

    }
    void CreateScene(int saveOrRedo)
    {
        if (saveOrRedo == 1)
        {
            if (numScene > 0 && numScene < 37)
            {
                
            
                if (HumXT.activeSelf == true)
                {
                    standOrSit = 0;
                    creaTa = HumXT;
                }
                if (HumXT_Sit.activeSelf == true)
                {
                    standOrSit = 1;
                    creaTa = HumXT_Sit;
                }

                createData[(numScene - 1) * 15] = standOrSit;
                createData[(numScene - 1) * 15 + 1] = creaTa.transform.position.x;
                createData[(numScene - 1) * 15 + 2] = creaTa.transform.position.y;
                createData[(numScene - 1) * 15 + 3] = creaTa.transform.position.z;
                createData[(numScene - 1) * 15 + 4] = creaTa.transform.eulerAngles.y;

                if (AvaYT.activeSelf == true)
                {
                    standOrSit = 0;
                    creaTa = AvaYT;
                }
                if (AvaYT_Sit.activeSelf == true)
                {
                    standOrSit = 1;
                    creaTa = AvaYT_Sit;
                }

                createData[(numScene - 1) * 15 + 5] = standOrSit;
                createData[(numScene - 1) * 15 + 6] = creaTa.transform.position.x;
                createData[(numScene - 1) * 15 + 7] = creaTa.transform.position.y;
                createData[(numScene - 1) * 15 + 8] = creaTa.transform.position.z;
                createData[(numScene - 1) * 15 + 9] = creaTa.transform.eulerAngles.y;

                if (HumYT.activeSelf == true)
                {
                    standOrSit = 0;
                    creaTa = HumYT;
                }
                if (HumYT_Sit.activeSelf == true)
                {
                    standOrSit = 1;
                    creaTa = HumYT_Sit;
                }

                createData[(numScene - 1) * 15 + 10] = standOrSit;
                createData[(numScene - 1) * 15 + 11] = creaTa.transform.position.x;
                createData[(numScene - 1) * 15 + 12] = creaTa.transform.position.y;
                createData[(numScene - 1) * 15 + 13] = creaTa.transform.position.z;
                createData[(numScene - 1) * 15 + 14] = creaTa.transform.eulerAngles.y;

            }
            if (numScene == 36 && Userdata == false)
            {
                save.SetActive(false);
                CreateToTxt();
            }
            numScene++;
            //DeactAvatars();
            if (edit == true)
            {
                DeactAvatars();
                ViewScene();
            }

        }
        else//Back
        {
            if (numScene == 37)
            {
                //GameObject next = GameObject.Find("Next_button");
                save.SetActive(true);
            }
            numScene--;
            DeactAvatars();
            if (numScene > 0)
            {
                if (createData[(numScene - 1) * 15] == 0)
                {
                    HumXT.SetActive(true);
                    Vector3 HumXPos = new Vector3(createData[(numScene - 1) * 15 + 1], createData[(numScene - 1) * 15 + 2], createData[(numScene - 1) * 15 + 3]);
                    Vector3 HumXeY = new Vector3(0f, createData[(numScene - 1) * 15 + 4], 0f);
                    HumXT.transform.position = HumXPos;
                    HumXT.transform.eulerAngles = HumXeY;
                }
                else if (createData[(numScene - 1) * 15] == 1)
                {

                    HumXT_Sit.SetActive(true);
                    Vector3 HumXPos = new Vector3(createData[(numScene - 1) * 15 + 1], createData[(numScene - 1) * 15 + 2], createData[(numScene - 1) * 15 + 3]);
                    Vector3 HumXeY = new Vector3(0f, createData[(numScene - 1) * 15 + 4], 0f);
                    HumXT_Sit.transform.position = HumXPos;
                    HumXT_Sit.transform.eulerAngles = HumXeY;
                }

                if (numScene >= 13 && numScene <= 36)
                {
                    if (createData[(numScene - 1) * 15 + 5] == 0)
                    {
                        AvaYT.SetActive(true);
                        Vector3 AvaYPos = new Vector3(createData[(numScene - 1) * 15 + 5 + 1], createData[(numScene - 1) * 15 + 5 + 2], createData[(numScene - 1) * 15 + 5 + 3]);
                        Vector3 AvaYeY = new Vector3(0f, createData[(numScene - 1) * 15 + 5 + 4], 0f);
                        AvaYT.transform.position = AvaYPos;
                        AvaYT.transform.eulerAngles = AvaYeY;
                    }
                    else if (createData[(numScene - 1) * 15 + 5] == 1)
                    {

                        AvaYT_Sit.SetActive(true);
                        Vector3 AvaYPos = new Vector3(createData[(numScene - 1) * 15 + 5 + 1], createData[(numScene - 1) * 15 + 5 + 2], createData[(numScene - 1) * 15 + 5 + 3]);
                        Vector3 AvaYeY = new Vector3(0f, createData[(numScene - 1) * 15 + 5 + 4], 0f);
                        AvaYT_Sit.transform.position = AvaYPos;
                        AvaYT_Sit.transform.eulerAngles = AvaYeY;
                    }
                    if (createData[(numScene - 1) * 15 + 10] == 0)
                    {
                        HumYT.SetActive(true);
                        Vector3 HumYPos = new Vector3(createData[(numScene - 1) * 15 + 10 + 1], createData[(numScene - 1) * 15 + 10 + 2], createData[(numScene - 1) * 15 + 10 + 3]);
                        Vector3 HumYeY = new Vector3(0f, createData[(numScene - 1) * 15 + 10 + 4], 0f);
                        HumYT.transform.position = HumYPos;
                        HumYT.transform.eulerAngles = HumYeY;
                    }
                    else if (createData[(numScene - 1) * 15 + 10] == 1)
                    {

                        HumYT_Sit.SetActive(true);
                        Vector3 HumYPos = new Vector3(createData[(numScene - 1) * 15 + 10 + 1], createData[(numScene - 1) * 15 + 10 + 2], createData[(numScene - 1) * 15 + 10 + 3]);
                        Vector3 HumYeY = new Vector3(0f, createData[(numScene - 1) * 15 + 10 + 4], 0f);
                        HumYT_Sit.transform.position = HumYPos;
                        HumYT_Sit.transform.eulerAngles = HumYeY;
                    }
                }
            }
        }


    }
    void CreateScene_48(int saveOrRedo)
    {
        if (saveOrRedo == 1)
        {
            if (numScene > 36 && numScene < 49)
            {


                if (HumXT.activeSelf == true)
                {
                    standOrSit = 0;
                    creaTa = HumXT;
                }
                if (HumXT_Sit.activeSelf == true)
                {
                    standOrSit = 1;
                    creaTa = HumXT_Sit;
                }

                createData_48[(numScene - 1 - 36) * 15] = standOrSit;
                createData_48[(numScene - 1 - 36) * 15 + 1] = creaTa.transform.position.x;
                createData_48[(numScene - 1 - 36) * 15 + 2] = creaTa.transform.position.y;
                createData_48[(numScene - 1 - 36) * 15 + 3] = creaTa.transform.position.z;
                createData_48[(numScene - 1 - 36) * 15 + 4] = creaTa.transform.eulerAngles.y;

                if (AvaYT.activeSelf == true)
                {
                    standOrSit = 0;
                    creaTa = AvaYT;
                }
                if (AvaYT_Sit.activeSelf == true)
                {
                    standOrSit = 1;
                    creaTa = AvaYT_Sit;
                }

                createData_48[(numScene - 1 - 36) * 15 + 5] = standOrSit;
                createData_48[(numScene - 1 - 36) * 15 + 6] = creaTa.transform.position.x;
                createData_48[(numScene - 1 - 36) * 15 + 7] = creaTa.transform.position.y;
                createData_48[(numScene - 1 - 36) * 15 + 8] = creaTa.transform.position.z;
                createData_48[(numScene - 1 - 36) * 15 + 9] = creaTa.transform.eulerAngles.y;

                if (HumYT.activeSelf == true)
                {
                    standOrSit = 0;
                    creaTa = HumYT;
                }
                if (HumYT_Sit.activeSelf == true)
                {
                    standOrSit = 1;
                    creaTa = HumYT_Sit;
                }

                createData_48[(numScene - 1 - 36) * 15 + 10] = standOrSit;
                createData_48[(numScene - 1 - 36) * 15 + 11] = creaTa.transform.position.x;
                createData_48[(numScene - 1 - 36) * 15 + 12] = creaTa.transform.position.y;
                createData_48[(numScene - 1 - 36) * 15 + 13] = creaTa.transform.position.z;
                createData_48[(numScene - 1 - 36) * 15 + 14] = creaTa.transform.eulerAngles.y;

            }
            if (numScene == 48 && Userdata == false)
            {
                save.SetActive(false);
                CreateToTxt();
            }
            numScene++;
            //DeactAvatars();
            if (edit == true)
            {
                DeactAvatars();
                ViewScene_48();
            }

        }
        else//Back
        {
            if (numScene == 49)
            {
                //GameObject next = GameObject.Find("Next_button");
                save.SetActive(true);
            }
            numScene--;
            DeactAvatars();
            if (numScene > 0)
            {
                if (createData_48[(numScene - 1 - 36) * 15] == 0)
                {
                    HumXT.SetActive(true);
                    Vector3 HumXPos = new Vector3(createData_48[(numScene - 1 - 36) * 15 + 1], createData_48[(numScene - 1 - 36) * 15 + 2], createData_48[(numScene - 1 - 36) * 15 + 3]);
                    Vector3 HumXeY = new Vector3(0f, createData_48[(numScene - 1 - 36) * 15 + 4], 0f);
                    HumXT.transform.position = HumXPos;
                    HumXT.transform.eulerAngles = HumXeY;
                }
                else if (createData_48[(numScene - 1 - 36) * 15] == 1)
                {

                    HumXT_Sit.SetActive(true);
                    Vector3 HumXPos = new Vector3(createData_48[(numScene - 1 - 36) * 15 + 1], createData_48[(numScene - 1 - 36) * 15 + 2], createData_48[(numScene - 1 - 36) * 15 + 3]);
                    Vector3 HumXeY = new Vector3(0f, createData_48[(numScene - 1 - 36) * 15 + 4], 0f);
                    HumXT_Sit.transform.position = HumXPos;
                    HumXT_Sit.transform.eulerAngles = HumXeY;
                }

                if (numScene >= 37 && numScene <= 48)
                {
                    if (createData_48[(numScene - 1 - 36) * 15 + 5] == 0)
                    {
                        AvaYT.SetActive(true);
                        Vector3 AvaYPos = new Vector3(createData_48[(numScene - 1 - 36) * 15 + 5 + 1], createData_48[(numScene - 1 - 36) * 15 + 5 + 2], createData_48[(numScene - 1 - 36) * 15 + 5 + 3]);
                        Vector3 AvaYeY = new Vector3(0f, createData_48[(numScene - 1 - 36) * 15 + 5 + 4], 0f);
                        AvaYT.transform.position = AvaYPos;
                        AvaYT.transform.eulerAngles = AvaYeY;
                    }
                    else if (createData_48[(numScene - 1 - 36) * 15 + 5] == 1)
                    {

                        AvaYT_Sit.SetActive(true);
                        Vector3 AvaYPos = new Vector3(createData_48[(numScene - 1 - 36) * 15 + 5 + 1], createData_48[(numScene - 1 - 36) * 15 + 5 + 2], createData_48[(numScene - 1 - 36) * 15 + 5 + 3]);
                        Vector3 AvaYeY = new Vector3(0f, createData_48[(numScene - 1 - 36) * 15 + 5 + 4], 0f);
                        AvaYT_Sit.transform.position = AvaYPos;
                        AvaYT_Sit.transform.eulerAngles = AvaYeY;
                    }
                    if (createData_48[(numScene - 1 - 36) * 15 + 10] == 0)
                    {
                        HumYT.SetActive(true);
                        Vector3 HumYPos = new Vector3(createData_48[(numScene - 1 - 36) * 15 + 10 + 1], createData_48[(numScene - 1 - 36) * 15 + 10 + 2], createData_48[(numScene - 1 - 36) * 15 + 10 + 3]);
                        Vector3 HumYeY = new Vector3(0f, createData_48[(numScene - 1 - 36) * 15 + 10 + 4], 0f);
                        HumYT.transform.position = HumYPos;
                        HumYT.transform.eulerAngles = HumYeY;
                    }
                    else if (createData_48[(numScene - 1 - 36) * 15 + 10] == 1)
                    {

                        HumYT_Sit.SetActive(true);
                        Vector3 HumYPos = new Vector3(createData_48[(numScene - 1 - 36) * 15 + 10 + 1], createData_48[(numScene - 1 - 36) * 15 + 10 + 2], createData_48[(numScene - 1 - 36) * 15 + 10 + 3]);
                        Vector3 HumYeY = new Vector3(0f, createData_48[(numScene - 1 - 36) * 15 + 10 + 4], 0f);
                        HumYT_Sit.transform.position = HumYPos;
                        HumYT_Sit.transform.eulerAngles = HumYeY;
                    }
                }
            }
        }


    }
    void TestScene(int nextOrBack)
    {
        // Next: Test data to array and write to txt
        if (nextOrBack == 0)
        {
            if (numScene > 0 && numScene < 49)
            {
                if (AvaXT.activeSelf == true)
                {
                    testAva = AvaXT;
                    tSitOrStand = 0;
                }
                if (AvaXT_Sit.activeSelf == true)
                {
                    testAva = AvaXT_Sit;
                    tSitOrStand = 1;
                }
                if(testAva==null)
                {
                    Debug.Log("Position not selected");
                }
                else
                {
                    testData[(numScene - 1) * 5] = tSitOrStand;
                    testData[(numScene - 1) * 5 + 1] = testAva.transform.position.x;
                    testData[(numScene - 1) * 5 + 2] = testAva.transform.position.y;
                    testData[(numScene - 1) * 5 + 3] = testAva.transform.position.z;
                    testData[(numScene - 1) * 5 + 4] = testAva.transform.eulerAngles.y;
                }

                /*
                Debug.Log("Scene " + numScene);
                for (int i = 0; i < 5; i++)
                {
                    Debug.Log(testData[(numScene - 1) * 5 + i]);
                }
                */
            }
            if (numScene == 48 && Userdata==false) //WriteToTxt
            {
                next.SetActive(false);
                /*
                for (int i = 0; i < 180; i++)
                {
                    Debug.Log(i+"] "+testData[i]);
                }
                */
                WriteToTxt();
                numScene = -1;
            }
            numScene++;
            
        }
        else//Back
        {
            if (numScene == 49)
            {
                //GameObject next = GameObject.Find("Next_button");
                next.SetActive(true);
            }
            numScene--;

        }
        DeactAvatars();
        // Scenes...
        if (numPair == 1)
        {
            DeactAvatars();
            if (numScene == 1)
            {
                SetPositionX(HumXT_Sit, new Vector3(-3.759f, -0.598f, 22.261f), new Vector3(0f, 250f, 0f));
            }
            if (numScene == 2)
            {
                SetPositionX(HumXT, new Vector3(-6.152f, 0f, 21.2f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 3)
            {
                SetPositionX(HumXT_Sit, new Vector3(-5.306f, -0.39f, 20.818f), new Vector3(0f, 145f, 0f));
            }
            if (numScene == 4)
            {
                SetPositionX(HumXT, new Vector3(-4.119f, 0f, 19.767f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 5)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.48f, -0.39f, 19.905f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 6)
            {
                SetPositionX(HumXT_Sit, new Vector3(-4.944f, -0.567f, 17.369f), new Vector3(0f, 325f, 0f));
            }
            if (numScene == 7)
            {
                SetPositionX(HumXT, new Vector3(-4.091f, 0f, 14.94f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 8)
            {
                SetPositionX(HumXT, new Vector3(-5.3f, 0f, 12.58f), new Vector3(0f, 210f, 0f));
            }
            if (numScene == 9)
            {
                SetPositionX(HumXT_Sit, new Vector3(-5.523f, -0.329f, 9.58f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 10)
            {
                SetPositionX(HumXT, new Vector3(-4.437f, 0f, 9.917f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 11)
            {
                SetPositionX(HumXT, new Vector3(-4.437f, 0f, 7.993f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 12)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.291f, -0.329f, 8.165f), new Vector3(0f, 0f, 0f));
                
            }
            if (numScene == 13)
            {
                SetPositionX(HumXT_Sit, new Vector3(-3.759f, -0.598f, 22.261f), new Vector3(0f, 250f, 0f));
                SetPositionY(AvaYT, new Vector3(-5f, 0f, 21.54f), new Vector3(0f, 60f, 0f));
                SetPositionY(HumYT, new Vector3(10.9f, 0f, 13.18f), new Vector3(0f, 120f, 0f));
            }
            if (numScene == 14)
            {
                SetPositionX(HumXT, new Vector3(-6.152f, 0f, 21.2f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.05f, 0f, 19.895f), new Vector3(0f, 310f, 0f));
                SetPositionY(HumYT, new Vector3(11.146f, 0f, 12.169f), new Vector3(0f, 50f, 0f));
            }
            if (numScene == 15)
            {
                SetPositionX(HumXT_Sit, new Vector3(-5.306f, -0.39f, 20.818f), new Vector3(0f, 145f, 0f));
                SetPositionY(AvaYT, new Vector3(-5.05f, 0f, 18.2f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT, new Vector3(8.57f, 0f, 11.93f), new Vector3(0f, 60f, 0f));
            }
            if (numScene == 16)
            {
                SetPositionX(HumXT, new Vector3(-4.119f, 0f, 19.767f), new Vector3(0f, 270f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.71f, 0f, 18.32f), new Vector3(0f, 70f, 0f));
                SetPositionY(HumYT, new Vector3(9.73f, 0f, 12.6f), new Vector3(0f, 150f, 0f));
            }
            if (numScene == 17)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.48f, -0.39f, 19.905f), new Vector3(0f, 90f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.548f, -0.448f, 18.629f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(9.651f, -0.418f, 14.17f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 18)
            {
                SetPositionX(HumXT_Sit, new Vector3(-4.944f, -0.567f, 17.369f), new Vector3(0f, 325f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.548f, -0.448f, 19.28f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(10.37f, -0.418f, 14.17f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 19)
            {
                SetPositionX(HumXT, new Vector3(-4.091f, 0f, 14.94f), new Vector3(0f, 260f, 0f));
                SetPositionY(AvaYT, new Vector3(-5.24f, 0f, 14.64f), new Vector3(0f, 80f, 0f));
                SetPositionY(HumYT, new Vector3(8.154f, 0f, 13.093f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 20)
            {
                SetPositionX(HumXT, new Vector3(-5.3f, 0f, 12.58f), new Vector3(0f, 210f, 0f));
                SetPositionY(AvaYT, new Vector3(-6.14f, 0f, 12.9f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT, new Vector3(8.154f, 0f, 11.99f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 21)
            {
                SetPositionX(HumXT_Sit, new Vector3(-5.523f, -0.329f, 9.58f), new Vector3(0f, 180f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.297f, -0.386f, 9.641f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(6.973f, -0.525f, 14.373f), new Vector3(0f, 140f, 0f));
            }
            if (numScene == 22)
            {
                SetPositionX(HumXT, new Vector3(-4.437f, 0f, 9.917f), new Vector3(0f, 90f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.297f, -0.386f, 8.12f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(4.911f, -0.525f, 9.872f), new Vector3(0f, 20f, 0f));
            }
            if (numScene == 23)
            {
                SetPositionX(HumXT, new Vector3(-4.437f, 0f, 7.993f), new Vector3(0f, 180f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.691f, 0f, 13.9f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT, new Vector3(7.091f, 0f, 11.99f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 24)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.291f, -0.329f, 8.165f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.297f, -0.386f, 9.641f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(7.009f, -0.496f, 13.322f), new Vector3(0f, 40f, 0f));
            }

            if (numScene == 25)
            {
                SetPositionX(HumXT, new Vector3(-5f, 0f, 21.54f), new Vector3(0f, 60f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-3.759f, -0.598f, 22.261f), new Vector3(0f, 250f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(11.989f, -0.585f, 12.441f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 26)
            {
                SetPositionX(HumXT, new Vector3(-4.05f, 0f, 19.895f), new Vector3(0f, 310f, 0f));
                SetPositionY(AvaYT, new Vector3(-6.152f, 0f, 21.2f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT, new Vector3(12.414f, 0f, 13.926f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 27)
            {
                SetPositionX(HumXT, new Vector3(-5.05f, 0f, 18.2f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-5.306f, -0.39f, 20.818f), new Vector3(0f, 145f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(9.224f, -0.585f, 12.441f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 28)
            {
                SetPositionX(HumXT, new Vector3(-4.71f, 0f, 18.32f), new Vector3(0f, 70f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.119f, 0f, 19.767f), new Vector3(0f, 270f, 0f));
                SetPositionY(HumYT, new Vector3(11.148f, 0f, 11.755f), new Vector3(0f, 320f, 0f));
            }
            if (numScene == 29)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.548f, -0.448f, 18.629f), new Vector3(0f, 90f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.48f, -0.39f, 19.905f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(10.814f, -0.418f, 14.17f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 30)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.548f, -0.448f, 19.28f), new Vector3(0f, 90f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-4.944f, -0.567f, 17.369f), new Vector3(0f, 325f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(9.153f, -0.585f, 13.181f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 31)
            {
                SetPositionX(HumXT, new Vector3(-5.24f, 0f, 14.64f), new Vector3(0f, 80f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.091f, 0f, 14.94f), new Vector3(0f, 260f, 0f));
                SetPositionY(HumYT, new Vector3(6.913f, 0f, 9.033f), new Vector3(0f, 330f, 0f));
            }
            if (numScene == 32)
            {
                SetPositionX(HumXT, new Vector3(-3.76f, 0f, 12.055f), new Vector3(0f, 330f, 0f));
                SetPositionY(AvaYT, new Vector3(-5.874f, 0f, 14.158f), new Vector3(0f, 60f, 0f));
                SetPositionY(HumYT, new Vector3(6.889f, 0f, 12.403f), new Vector3(0f, 120f, 0f));
            }
            if (numScene == 33)
            {
                SetPositionX(HumXT, new Vector3(-4.906f, 0f, 7.863f), new Vector3(0f, 330f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.297f, -0.386f, 9.641f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(6.973f, -0.525f, 14.373f), new Vector3(0f, 140f, 0f));
            }
            if (numScene == 34)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.297f, -0.386f, 8.12f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.437f, 0f, 9.917f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT, new Vector3(6.282f, 0f, 12.939f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 35)
            {
                SetPositionX(HumXT, new Vector3(-4.691f, 0f, 13.9f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.437f, 0f, 7.993f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT, new Vector3(5.93f, 0f, 13.8f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 36)
            {
                SetPositionX(HumXT_Sit, new Vector3(-5.542f, -0.35f, 9.6209f), new Vector3(0f, 180f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.291f, -0.329f, 8.165f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(7.009f, -0.496f, 13.322f), new Vector3(0f, 40f, 0f));
            }
        }
        if (numPair == 2)
        {
            DeactAvatars();
            if (numScene == 1)
            {
                SetPositionX(HumXT_Sit, new Vector3(-3.759f, -0.598f, 22.261f), new Vector3(0f, 250f, 0f));
            }
            if (numScene == 2)
            {
                SetPositionX(HumXT, new Vector3(-6.152f, 0f, 21.2f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 3)
            {
                SetPositionX(HumXT_Sit, new Vector3(-5.306f, -0.39f, 20.818f), new Vector3(0f, 145f, 0f));
            }
            if (numScene == 4)
            {
                SetPositionX(HumXT, new Vector3(-4.119f, 0f, 19.767f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 5)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.48f, -0.39f, 19.905f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 6)
            {
                SetPositionX(HumXT_Sit, new Vector3(-4.944f, -0.567f, 17.369f), new Vector3(0f, 325f, 0f));
            }
            if (numScene == 7)
            {
                SetPositionX(HumXT, new Vector3(-4.091f, 0f, 14.94f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 8)
            {
                SetPositionX(HumXT, new Vector3(-5.3f, 0f, 12.58f), new Vector3(0f, 210f, 0f));
            }
            if (numScene == 9)
            {
                SetPositionX(HumXT_Sit, new Vector3(-5.523f, -0.329f, 9.58f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 10)
            {
                SetPositionX(HumXT, new Vector3(-4.437f, 0f, 9.917f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 11)
            {
                SetPositionX(HumXT, new Vector3(-4.437f, 0f, 7.993f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 12)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.291f, -0.329f, 8.165f), new Vector3(0f, 0f, 0f));

            }
            if (numScene == 13)
            {
                SetPositionX(HumXT_Sit, new Vector3(-3.759f, -0.598f, 22.261f), new Vector3(0f, 250f, 0f));
                SetPositionY(AvaYT, new Vector3(-5f, 0f, 21.54f), new Vector3(0f, 60f, 0f));
                SetPositionY(HumYT, new Vector3(-4.99f, 0f, 33.03f), new Vector3(0f, 240f, 0f));
            }
            if (numScene == 14)
            {
                SetPositionX(HumXT, new Vector3(-6.152f, 0f, 21.2f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.05f, 0f, 19.895f), new Vector3(0f, 310f, 0f));
                SetPositionY(HumYT, new Vector3(-5.82f, 0f, 34.46f), new Vector3(0f, 60f, 0f));
            }
            if (numScene == 15)
            {
                SetPositionX(HumXT_Sit, new Vector3(-5.306f, -0.39f, 20.818f), new Vector3(0f, 145f, 0f));
                SetPositionY(AvaYT, new Vector3(-5.05f, 0f, 18.2f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT, new Vector3(-5.87f, 0f, 32.55f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 16)
            {
                SetPositionX(HumXT, new Vector3(-4.119f, 0f, 19.767f), new Vector3(0f, 270f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.71f, 0f, 18.32f), new Vector3(0f, 70f, 0f));
                SetPositionY(HumYT, new Vector3(-6.53f, 0f, 31.89f), new Vector3(0f, 60f, 0f));
            }
            if (numScene == 17)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.48f, -0.39f, 19.905f), new Vector3(0f, 90f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.548f, -0.448f, 18.629f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-7.15f, -0.416f, 32.827f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 18)
            {
                SetPositionX(HumXT_Sit, new Vector3(-4.944f, -0.567f, 17.369f), new Vector3(0f, 325f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.548f, -0.448f, 19.28f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-7.2f, -0.416f, 33.508f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 19)
            {
                SetPositionX(HumXT, new Vector3(-4.091f, 0f, 14.94f), new Vector3(0f, 260f, 0f));
                SetPositionY(AvaYT, new Vector3(-5.24f, 0f, 14.64f), new Vector3(0f, 80f, 0f));
                SetPositionY(HumYT, new Vector3(-7.7f, 0f, 31.22f), new Vector3(0f, 330f, 0f));
            }
            if (numScene == 20)
            {
                SetPositionX(HumXT, new Vector3(-5.3f, 0f, 12.58f), new Vector3(0f, 210f, 0f));
                SetPositionY(AvaYT, new Vector3(-6.14f, 0f, 12.9f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT, new Vector3(-10.9f, 0f, 30.6f), new Vector3(0f, 60f, 0f));
            }
            if (numScene == 21)
            {
                SetPositionX(HumXT_Sit, new Vector3(-5.523f, -0.329f, 9.58f), new Vector3(0f, 180f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.297f, -0.386f, 9.641f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-10.07f, -0.416f, 33.03f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 22)
            {
                SetPositionX(HumXT, new Vector3(-4.437f, 0f, 9.917f), new Vector3(0f, 90f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.297f, -0.386f, 8.12f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-8.71f, -0.416f, 33.03f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 23)
            {
                SetPositionX(HumXT, new Vector3(-4.437f, 0f, 7.993f), new Vector3(0f, 180f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.691f, 0f, 13.9f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT, new Vector3(-5.96f, 0f, 30.77f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 24)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.291f, -0.329f, 8.165f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.297f, -0.386f, 9.641f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-10.07f, -0.416f, 33.03f), new Vector3(0f, 90f, 0f));
            }

            if (numScene == 25)
            {
                SetPositionX(HumXT, new Vector3(-5f, 0f, 21.54f), new Vector3(0f, 60f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-3.759f, -0.598f, 22.261f), new Vector3(0f, 250f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-4.756f, -0.416f, 35.824f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 26)
            {
                SetPositionX(HumXT, new Vector3(-4.05f, 0f, 19.895f), new Vector3(0f, 310f, 0f));
                SetPositionY(AvaYT, new Vector3(-6.152f, 0f, 21.2f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT, new Vector3(-6.754f, 0f, 31.95f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 27)
            {
                SetPositionX(HumXT, new Vector3(-5.05f, 0f, 18.2f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-5.306f, -0.39f, 20.818f), new Vector3(0f, 145f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-5.69f, -0.416f, 35.824f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 28)
            {
                SetPositionX(HumXT, new Vector3(-4.71f, 0f, 18.32f), new Vector3(0f, 70f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.119f, 0f, 19.767f), new Vector3(0f, 270f, 0f));
                SetPositionY(HumYT, new Vector3(-5.15f, 0f, 34.19f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 29)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.548f, -0.448f, 18.629f), new Vector3(0f, 90f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.48f, -0.39f, 19.905f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-7.18f, -0.416f, 34.235f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 30)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.548f, -0.448f, 19.28f), new Vector3(0f, 90f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-4.944f, -0.567f, 17.369f), new Vector3(0f, 325f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-5.84f, -0.374f, 31.778f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 31)
            {
                SetPositionX(HumXT, new Vector3(-5.24f, 0f, 14.64f), new Vector3(0f, 80f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.091f, 0f, 14.94f), new Vector3(0f, 260f, 0f));
                SetPositionY(HumYT, new Vector3(-10.07f, 0f, 31.24f), new Vector3(0f, 200f, 0f));
            }
            if (numScene == 32)
            {
                SetPositionX(HumXT, new Vector3(-3.76f, 0f, 12.055f), new Vector3(0f, 330f, 0f));
                SetPositionY(AvaYT, new Vector3(-5.874f, 0f, 14.158f), new Vector3(0f, 60f, 0f));
                SetPositionY(HumYT, new Vector3(-9.44f, 0f, 30.88f), new Vector3(0f, 300f, 0f));
            }
            if (numScene == 33)
            {
                SetPositionX(HumXT, new Vector3(-4.906f, 0f, 7.863f), new Vector3(0f, 330f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.297f, -0.386f, 9.641f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-10.07f, -0.416f, 33.03f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 34)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.297f, -0.386f, 8.12f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.437f, 0f, 9.917f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT, new Vector3(-9.77f, 0f, 30.71f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 35)
            {
                SetPositionX(HumXT, new Vector3(-4.691f, 0f, 13.9f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.437f, 0f, 7.993f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT, new Vector3(-11.88f, 0f, 30.92f), new Vector3(0f, 210f, 0f));
            }
            if (numScene == 36)
            {
                SetPositionX(HumXT_Sit, new Vector3(-5.542f, -0.35f, 9.6209f), new Vector3(0f, 180f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.291f, -0.329f, 8.165f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-8.71f, -0.416f, 33.03f), new Vector3(0f, 270f, 0f));
            }
        }
        if (numPair == 3)
        {
            DeactAvatars();
            if (numScene == 1)
            {
                SetPositionX(HumXT_Sit, new Vector3(-3.759f, -0.598f, 22.261f), new Vector3(0f, 250f, 0f));
            }
            if (numScene == 2)
            {
                SetPositionX(HumXT, new Vector3(-6.152f, 0f, 21.2f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 3)
            {
                SetPositionX(HumXT_Sit, new Vector3(-5.306f, -0.39f, 20.818f), new Vector3(0f, 145f, 0f));
            }
            if (numScene == 4)
            {
                SetPositionX(HumXT, new Vector3(-4.119f, 0f, 19.767f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 5)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.48f, -0.39f, 19.905f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 6)
            {
                SetPositionX(HumXT_Sit, new Vector3(-4.944f, -0.567f, 17.369f), new Vector3(0f, 325f, 0f));
            }
            if (numScene == 7)
            {
                SetPositionX(HumXT, new Vector3(-4.091f, 0f, 14.94f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 8)
            {
                SetPositionX(HumXT, new Vector3(-5.3f, 0f, 12.58f), new Vector3(0f, 210f, 0f));
            }
            if (numScene == 9)
            {
                SetPositionX(HumXT_Sit, new Vector3(-5.523f, -0.329f, 9.58f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 10)
            {
                SetPositionX(HumXT, new Vector3(-4.437f, 0f, 9.917f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 11)
            {
                SetPositionX(HumXT, new Vector3(-4.437f, 0f, 7.993f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 12)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.291f, -0.329f, 8.165f), new Vector3(0f, 0f, 0f));

            }
            if (numScene == 13)
            {
                SetPositionX(HumXT_Sit, new Vector3(-3.759f, -0.598f, 22.261f), new Vector3(0f, 250f, 0f));
                SetPositionY(AvaYT, new Vector3(-5f, 0f, 21.54f), new Vector3(0f, 60f, 0f));
                SetPositionY(HumYT, new Vector3(10.323f, 0f, 32.206f), new Vector3(0f, 210f, 0f));
            }
            if (numScene == 14)
            {
                SetPositionX(HumXT, new Vector3(-6.152f, 0f, 21.2f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.05f, 0f, 19.895f), new Vector3(0f, 310f, 0f));
                SetPositionY(HumYT, new Vector3(9.433f, 0f, 33.703f), new Vector3(0f, 120f, 0f));
            }
            if (numScene == 15)
            {
                SetPositionX(HumXT_Sit, new Vector3(-5.306f, -0.39f, 20.818f), new Vector3(0f, 145f, 0f));
                SetPositionY(AvaYT, new Vector3(-5.05f, 0f, 18.2f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT, new Vector3(10.814f, 0f, 35.765f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 16)
            {
                SetPositionX(HumXT, new Vector3(-4.119f, 0f, 19.767f), new Vector3(0f, 270f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.71f, 0f, 18.32f), new Vector3(0f, 70f, 0f));
                SetPositionY(HumYT, new Vector3(10.401f, 0f, 35.792f), new Vector3(0f, 240f, 0f));
            }
            if (numScene == 17)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.48f, -0.39f, 19.905f), new Vector3(0f, 90f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.548f, -0.448f, 18.629f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(12.227f, -0.315f, 35.095f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 18)
            {
                SetPositionX(HumXT_Sit, new Vector3(-4.944f, -0.567f, 17.369f), new Vector3(0f, 325f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.548f, -0.448f, 19.28f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(12.227f, -0.315f, 34.475f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 19)
            {
                SetPositionX(HumXT, new Vector3(-4.091f, 0f, 14.94f), new Vector3(0f, 260f, 0f));
                SetPositionY(AvaYT, new Vector3(-5.24f, 0f, 14.64f), new Vector3(0f, 80f, 0f));
                SetPositionY(HumYT, new Vector3(11.1f, 0f, 32.03f), new Vector3(0f, 240f, 0f));
            }
            if (numScene == 20)
            {
                SetPositionX(HumXT, new Vector3(-5.3f, 0f, 12.58f), new Vector3(0f, 210f, 0f));
                SetPositionY(AvaYT, new Vector3(-6.14f, 0f, 12.9f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT, new Vector3(12.39f, 0f, 29.54f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 21)
            {
                SetPositionX(HumXT_Sit, new Vector3(-5.523f, -0.329f, 9.58f), new Vector3(0f, 180f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.297f, -0.386f, 9.641f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(11.204f, -0.149f, 30.383f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 22)
            {
                SetPositionX(HumXT, new Vector3(-4.437f, 0f, 9.917f), new Vector3(0f, 90f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.297f, -0.386f, 8.12f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(9.794f, -0.149f, 30.383f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 23)
            {
                SetPositionX(HumXT, new Vector3(-4.437f, 0f, 7.993f), new Vector3(0f, 180f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.691f, 0f, 13.9f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT, new Vector3(10.658f, 0f, 31.451f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 24)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.291f, -0.329f, 8.165f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.297f, -0.386f, 9.641f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(11.204f, -0.149f, 30.383f), new Vector3(0f, 270f, 0f));
            }

            if (numScene == 25)
            {
                SetPositionX(HumXT, new Vector3(-5f, 0f, 21.54f), new Vector3(0f, 60f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-3.759f, -0.598f, 22.261f), new Vector3(0f, 250f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(10.936f, -0.294f, 32.6f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 26)
            {
                SetPositionX(HumXT, new Vector3(-4.05f, 0f, 19.895f), new Vector3(0f, 310f, 0f));
                SetPositionY(AvaYT, new Vector3(-6.152f, 0f, 21.2f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT, new Vector3(11.764f, 0f, 32.229f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 27)
            {
                SetPositionX(HumXT, new Vector3(-5.05f, 0f, 18.2f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-5.306f, -0.39f, 20.818f), new Vector3(0f, 145f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(10.487f, -0.294f, 32.917f), new Vector3(0f, 310f, 0f));
            }
            if (numScene == 28)
            {
                SetPositionX(HumXT, new Vector3(-4.71f, 0f, 18.32f), new Vector3(0f, 70f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.119f, 0f, 19.767f), new Vector3(0f, 270f, 0f));
                SetPositionY(HumYT, new Vector3(9.513f, 0f, 33.907f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 29)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.548f, -0.448f, 18.629f), new Vector3(0f, 90f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.48f, -0.39f, 19.905f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(12.224f, -0.294f, 33.801f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 30)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.548f, -0.448f, 19.28f), new Vector3(0f, 90f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-4.944f, -0.567f, 17.369f), new Vector3(0f, 325f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(10.583f, -0.294f, 36.126f), new Vector3(0f, 120f, 0f));
            }
            if (numScene == 31)
            {
                SetPositionX(HumXT, new Vector3(-5.24f, 0f, 14.64f), new Vector3(0f, 80f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.091f, 0f, 14.94f), new Vector3(0f, 260f, 0f));
                SetPositionY(HumYT, new Vector3(9.513f, 0f, 32.634f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 32)
            {
                SetPositionX(HumXT, new Vector3(-3.76f, 0f, 12.055f), new Vector3(0f, 330f, 0f));
                SetPositionY(AvaYT, new Vector3(-5.874f, 0f, 14.158f), new Vector3(0f, 60f, 0f));
                SetPositionY(HumYT, new Vector3(10.375f, 0f, 31.656f), new Vector3(0f, 320f, 0f));
            }
            if (numScene == 33)
            {
                SetPositionX(HumXT, new Vector3(-4.906f, 0f, 7.863f), new Vector3(0f, 330f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.297f, -0.386f, 9.641f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(11.204f, -0.149f, 30.383f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 34)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.297f, -0.386f, 8.12f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.437f, 0f, 9.917f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT, new Vector3(9.697f, 0f, 31.656f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 35)
            {
                SetPositionX(HumXT, new Vector3(-4.691f, 0f, 13.9f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.437f, 0f, 7.993f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT, new Vector3(9.627f, 0f, 28.602f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 36)
            {
                SetPositionX(HumXT_Sit, new Vector3(-5.542f, -0.35f, 9.6209f), new Vector3(0f, 180f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.291f, -0.329f, 8.165f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(9.862f, -0.135f, 29.311f), new Vector3(0f, 90f, 0f));
            }
        }

        if (numPair == 4)
        {
            DeactAvatars();
            if (numScene == 1)
            {
                SetPositionX(HumXT_Sit, new Vector3(-3.759f, -0.598f, 22.261f), new Vector3(0f, 250f, 0f));
            }
            if (numScene == 2)
            {
                SetPositionX(HumXT, new Vector3(-6.152f, 0f, 21.2f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 3)
            {
                SetPositionX(HumXT_Sit, new Vector3(-5.306f, -0.39f, 20.818f), new Vector3(0f, 145f, 0f));
            }
            if (numScene == 4)
            {
                SetPositionX(HumXT, new Vector3(-4.119f, 0f, 19.767f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 5)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.48f, -0.39f, 19.905f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 6)
            {
                SetPositionX(HumXT_Sit, new Vector3(-4.944f, -0.567f, 17.369f), new Vector3(0f, 325f, 0f));
            }
            if (numScene == 7)
            {
                SetPositionX(HumXT, new Vector3(-4.091f, 0f, 14.94f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 8)
            {
                SetPositionX(HumXT, new Vector3(-5.3f, 0f, 12.58f), new Vector3(0f, 210f, 0f));
            }
            if (numScene == 9)
            {
                SetPositionX(HumXT_Sit, new Vector3(-5.523f, -0.329f, 9.58f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 10)
            {
                SetPositionX(HumXT, new Vector3(-4.437f, 0f, 9.917f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 11)
            {
                SetPositionX(HumXT, new Vector3(-4.437f, 0f, 7.993f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 12)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.291f, -0.329f, 8.165f), new Vector3(0f, 0f, 0f));

            }
            if (numScene == 13)
            {
                SetPositionX(HumXT_Sit, new Vector3(-3.759f, -0.598f, 22.261f), new Vector3(0f, 250f, 0f));
                SetPositionY(AvaYT, new Vector3(-5f, 0f, 21.54f), new Vector3(0f, 60f, 0f));
                SetPositionY(HumYT, new Vector3(11.745f, 0f, 51.29f), new Vector3(0f, 30f, 0f));
            }
            if (numScene == 14)
            {
                SetPositionX(HumXT, new Vector3(-6.152f, 0f, 21.2f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.05f, 0f, 19.895f), new Vector3(0f, 310f, 0f));
                SetPositionY(HumYT, new Vector3(12.424f, 0f, 50.205f), new Vector3(0f, 330f, 0f));
            }
            if (numScene == 15)
            {
                SetPositionX(HumXT_Sit, new Vector3(-5.306f, -0.39f, 20.818f), new Vector3(0f, 145f, 0f));
                SetPositionY(AvaYT, new Vector3(-5.05f, 0f, 18.2f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT, new Vector3(12.04f, 0f, 49.11f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 16)
            {
                SetPositionX(HumXT, new Vector3(-4.119f, 0f, 19.767f), new Vector3(0f, 270f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.71f, 0f, 18.32f), new Vector3(0f, 70f, 0f));
                SetPositionY(HumYT, new Vector3(12.03f, 0f, 49.11f), new Vector3(0f, 70f, 0f));
            }
            if (numScene == 17)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.48f, -0.39f, 19.905f), new Vector3(0f, 90f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.548f, -0.448f, 18.629f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(10.37f, -0.356f, 49.186f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 18)
            {
                SetPositionX(HumXT_Sit, new Vector3(-4.944f, -0.567f, 17.369f), new Vector3(0f, 325f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.548f, -0.448f, 19.28f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(10.37f, -0.356f, 49.835f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 19)
            {
                SetPositionX(HumXT, new Vector3(-4.091f, 0f, 14.94f), new Vector3(0f, 260f, 0f));
                SetPositionY(AvaYT, new Vector3(-5.24f, 0f, 14.64f), new Vector3(0f, 80f, 0f));
                SetPositionY(HumYT, new Vector3(7.894f, 0f, 48.548f), new Vector3(0f, 330f, 0f));
            }
            if (numScene == 20)
            {
                SetPositionX(HumXT, new Vector3(-5.3f, 0f, 12.58f), new Vector3(0f, 210f, 0f));
                SetPositionY(AvaYT, new Vector3(-6.14f, 0f, 12.9f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT, new Vector3(6.542f, 0f, 49.465f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 21)
            {
                SetPositionX(HumXT_Sit, new Vector3(-5.523f, -0.329f, 9.58f), new Vector3(0f, 180f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.297f, -0.386f, 9.641f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(6.355f, -0.411f, 47.391f), new Vector3(0f, 225f, 0f));
            }
            if (numScene == 22)
            {
                SetPositionX(HumXT, new Vector3(-4.437f, 0f, 9.917f), new Vector3(0f, 90f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.297f, -0.386f, 8.12f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(5.306f, -0.411f, 46.342f), new Vector3(0f, 45f, 0f));
            }
            if (numScene == 23)
            {
                SetPositionX(HumXT, new Vector3(-4.437f, 0f, 7.993f), new Vector3(0f, 180f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.691f, 0f, 13.9f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT, new Vector3(7.383f, 0f, 49.465f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 24)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.291f, -0.329f, 8.165f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.297f, -0.386f, 9.641f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(5.299f, -0.411f, 47.419f), new Vector3(0f, 135f, 0f));
            }

            if (numScene == 25)
            {
                SetPositionX(HumXT, new Vector3(-5f, 0f, 21.54f), new Vector3(0f, 60f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-3.759f, -0.598f, 22.261f), new Vector3(0f, 250f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(12.199f, -0.504f, 50.861f), new Vector3(0f, 210f, 0f));
            }
            if (numScene == 26)
            {
                SetPositionX(HumXT, new Vector3(-4.05f, 0f, 19.895f), new Vector3(0f, 310f, 0f));
                SetPositionY(AvaYT, new Vector3(-6.152f, 0f, 21.2f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT, new Vector3(11.325f, 0f, 51.332f), new Vector3(0f, 60f, 0f));
            }
            if (numScene == 27)
            {
                SetPositionX(HumXT, new Vector3(-5.05f, 0f, 18.2f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-5.306f, -0.39f, 20.818f), new Vector3(0f, 145f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(12.199f, -0.504f, 50.861f), new Vector3(0f, 140f, 0f));
            }
            if (numScene == 28)
            {
                SetPositionX(HumXT, new Vector3(-4.71f, 0f, 18.32f), new Vector3(0f, 70f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.119f, 0f, 19.767f), new Vector3(0f, 270f, 0f));
                SetPositionY(HumYT, new Vector3(12.449f, 0f, 50.231f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 29)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.548f, -0.448f, 18.629f), new Vector3(0f, 90f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.48f, -0.39f, 19.905f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(10.388f, -0.361f, 50.387f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 30)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.548f, -0.448f, 19.28f), new Vector3(0f, 90f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-4.944f, -0.567f, 17.369f), new Vector3(0f, 325f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(12.365f, -0.525f, 48.827f), new Vector3(0f, 300f, 0f));
            }
            if (numScene == 31)
            {
                SetPositionX(HumXT, new Vector3(-5.24f, 0f, 14.64f), new Vector3(0f, 80f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.091f, 0f, 14.94f), new Vector3(0f, 260f, 0f));
                SetPositionY(HumYT, new Vector3(7.336f, 0f, 49.961f), new Vector3(0f, 150f, 0f));
            }
            if (numScene == 32)
            {
                SetPositionX(HumXT, new Vector3(-3.76f, 0f, 12.055f), new Vector3(0f, 330f, 0f));
                SetPositionY(AvaYT, new Vector3(-5.874f, 0f, 14.158f), new Vector3(0f, 60f, 0f));
                SetPositionY(HumYT, new Vector3(9.175f, 0f, 49.153f), new Vector3(0f, 30f, 0f));
            }
            if (numScene == 33)
            {
                SetPositionX(HumXT, new Vector3(-4.906f, 0f, 7.863f), new Vector3(0f, 330f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.297f, -0.386f, 9.641f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(9.314f, -0.403f, 50.204f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 34)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.297f, -0.386f, 8.12f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.437f, 0f, 9.917f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT, new Vector3(7.028f, 0f, 49.47f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 35)
            {
                SetPositionX(HumXT, new Vector3(-4.691f, 0f, 13.9f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.437f, 0f, 7.993f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT, new Vector3(5.453f, 0f, 50.726f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 36)
            {
                SetPositionX(HumXT_Sit, new Vector3(-5.542f, -0.35f, 9.6209f), new Vector3(0f, 180f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.291f, -0.329f, 8.165f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(7.772f, -0.403f, 51.058f), new Vector3(0f, 135f, 0f));
            }
        }

        if (numPair == 5)
        {
            DeactAvatars();
            if (numScene == 1)
            {
                SetPositionX(HumXT_Sit, new Vector3(-3.759f, -0.598f, 22.261f), new Vector3(0f, 250f, 0f));
            }
            if (numScene == 2)
            {
                SetPositionX(HumXT, new Vector3(-6.152f, 0f, 21.2f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 3)
            {
                SetPositionX(HumXT_Sit, new Vector3(-5.306f, -0.39f, 20.818f), new Vector3(0f, 145f, 0f));
            }
            if (numScene == 4)
            {
                SetPositionX(HumXT, new Vector3(-4.119f, 0f, 19.767f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 5)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.48f, -0.39f, 19.905f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 6)
            {
                SetPositionX(HumXT_Sit, new Vector3(-4.944f, -0.567f, 17.369f), new Vector3(0f, 325f, 0f));
            }
            if (numScene == 7)
            {
                SetPositionX(HumXT, new Vector3(-4.091f, 0f, 14.94f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 8)
            {
                SetPositionX(HumXT, new Vector3(-5.3f, 0f, 12.58f), new Vector3(0f, 210f, 0f));
            }
            if (numScene == 9)
            {
                SetPositionX(HumXT_Sit, new Vector3(-5.523f, -0.329f, 9.58f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 10)
            {
                SetPositionX(HumXT, new Vector3(-4.437f, 0f, 9.917f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 11)
            {
                SetPositionX(HumXT, new Vector3(-4.437f, 0f, 7.993f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 12)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.291f, -0.329f, 8.165f), new Vector3(0f, 0f, 0f));

            }
            if (numScene == 13)
            {
                SetPositionX(HumXT_Sit, new Vector3(-3.759f, -0.598f, 22.261f), new Vector3(0f, 250f, 0f));
                SetPositionY(AvaYT, new Vector3(-5f, 0f, 21.54f), new Vector3(0f, 60f, 0f));
                SetPositionY(HumYT, new Vector3(-6.32f, 0f, 70.43f), new Vector3(0f, 50f, 0f));
            }
            if (numScene == 14)
            {
                SetPositionX(HumXT, new Vector3(-6.152f, 0f, 21.2f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.05f, 0f, 19.895f), new Vector3(0f, 310f, 0f));
                SetPositionY(HumYT, new Vector3(-5.72f, 0f, 70.43f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 15)
            {
                SetPositionX(HumXT_Sit, new Vector3(-5.306f, -0.39f, 20.818f), new Vector3(0f, 145f, 0f));
                SetPositionY(AvaYT, new Vector3(-5.05f, 0f, 18.2f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT, new Vector3(-7.2f, 0f, 67.56f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 16)
            {
                SetPositionX(HumXT, new Vector3(-4.119f, 0f, 19.767f), new Vector3(0f, 270f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.71f, 0f, 18.32f), new Vector3(0f, 70f, 0f));
                SetPositionY(HumYT, new Vector3(-7.61f, 0f, 69.3f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 17)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.48f, -0.39f, 19.905f), new Vector3(0f, 90f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.548f, -0.448f, 18.629f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-6.285f, -0.363f, 68.473f), new Vector3(0f, 40f, 0f));
            }
            if (numScene == 18)
            {
                SetPositionX(HumXT_Sit, new Vector3(-4.944f, -0.567f, 17.369f), new Vector3(0f, 325f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.548f, -0.448f, 19.28f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-6.867f, -0.306f, 68.444f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 19)
            {
                SetPositionX(HumXT, new Vector3(-4.091f, 0f, 14.94f), new Vector3(0f, 260f, 0f));
                SetPositionY(AvaYT, new Vector3(-5.24f, 0f, 14.64f), new Vector3(0f, 80f, 0f));
                SetPositionY(HumYT, new Vector3(-9.22f, 0f, 67.73f), new Vector3(0f, 280f, 0f));
            }
            if (numScene == 20)
            {
                SetPositionX(HumXT, new Vector3(-5.3f, 0f, 12.58f), new Vector3(0f, 210f, 0f));
                SetPositionY(AvaYT, new Vector3(-6.14f, 0f, 12.9f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT, new Vector3(-11.15f, 0f, 66.45f), new Vector3(0f, 330f, 0f));
            }
            if (numScene == 21)
            {
                SetPositionX(HumXT_Sit, new Vector3(-5.523f, -0.329f, 9.58f), new Vector3(0f, 180f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.297f, -0.386f, 9.641f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-9.102f, -0.363f, 69.894f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 22)
            {
                SetPositionX(HumXT, new Vector3(-4.437f, 0f, 9.917f), new Vector3(0f, 90f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.297f, -0.386f, 8.12f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-10.51f, -0.363f, 69.976f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 23)
            {
                SetPositionX(HumXT, new Vector3(-4.437f, 0f, 7.993f), new Vector3(0f, 180f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.691f, 0f, 13.9f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT, new Vector3(-9.38f, 0f, 67.31f), new Vector3(0f, 40f, 0f));
            }
            if (numScene == 24)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.291f, -0.329f, 8.165f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.297f, -0.386f, 9.641f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-9.102f, -0.363f, 69.894f), new Vector3(0f, 270f, 0f));
            }

            if (numScene == 25)
            {
                SetPositionX(HumXT, new Vector3(-5f, 0f, 21.54f), new Vector3(0f, 60f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-3.759f, -0.598f, 22.261f), new Vector3(0f, 250f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-5.397f, -0.301f, 71.799f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 26)
            {
                SetPositionX(HumXT, new Vector3(-4.05f, 0f, 19.895f), new Vector3(0f, 310f, 0f));
                SetPositionY(AvaYT, new Vector3(-6.152f, 0f, 21.2f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT, new Vector3(-6.61f, 0f, 70.56f), new Vector3(0f, 30f, 0f));
            }
            if (numScene == 27)
            {
                SetPositionX(HumXT, new Vector3(-5.05f, 0f, 18.2f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-5.306f, -0.39f, 20.818f), new Vector3(0f, 145f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-7.551f, -0.301f, 70.493f), new Vector3(0f, 130f, 0f));
            }
            if (numScene == 28)
            {
                SetPositionX(HumXT, new Vector3(-4.71f, 0f, 18.32f), new Vector3(0f, 70f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.119f, 0f, 19.767f), new Vector3(0f, 270f, 0f));
                SetPositionY(HumYT, new Vector3(-7.423f, 0f, 70.069f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 29)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.548f, -0.448f, 18.629f), new Vector3(0f, 90f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.48f, -0.39f, 19.905f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-7.567f, -0.306f, 68.444f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 30)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.548f, -0.448f, 19.28f), new Vector3(0f, 90f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-4.944f, -0.567f, 17.369f), new Vector3(0f, 325f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-6.325f, -0.294f, 71.761f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 31)
            {
                SetPositionX(HumXT, new Vector3(-5.24f, 0f, 14.64f), new Vector3(0f, 80f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.091f, 0f, 14.94f), new Vector3(0f, 260f, 0f));
                SetPositionY(HumYT, new Vector3(-9.938f, 0f, 67.874f), new Vector3(0f, 220f, 0f));
            }
            if (numScene == 32)
            {
                SetPositionX(HumXT, new Vector3(-3.76f, 0f, 12.055f), new Vector3(0f, 330f, 0f));
                SetPositionY(AvaYT, new Vector3(-5.874f, 0f, 14.158f), new Vector3(0f, 60f, 0f));
                SetPositionY(HumYT, new Vector3(-8.804f, 0f, 68.062f), new Vector3(0f, 120f, 0f));
            }
            if (numScene == 33)
            {
                SetPositionX(HumXT, new Vector3(-4.906f, 0f, 7.863f), new Vector3(0f, 330f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.297f, -0.386f, 9.641f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-9.102f, -0.363f, 69.894f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 34)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.297f, -0.386f, 8.12f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.437f, 0f, 9.917f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT, new Vector3(-10.35f, 0f, 68.062f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 35)
            {
                SetPositionX(HumXT, new Vector3(-4.691f, 0f, 13.9f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.437f, 0f, 7.993f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT, new Vector3(-8.55f, 0f, 70.56f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 36)
            {
                SetPositionX(HumXT_Sit, new Vector3(-5.542f, -0.35f, 9.6209f), new Vector3(0f, 180f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.291f, -0.329f, 8.165f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-10.56f, -0.345f, 69.928f), new Vector3(0f, 90f, 0f));
            }
        }

        if (numPair == 6)
        {
            DeactAvatars();
            if (numScene == 1)
            {
                SetPositionX(HumXT_Sit, new Vector3(-3.759f, -0.598f, 22.261f), new Vector3(0f, 250f, 0f));
            }
            if (numScene == 2)
            {
                SetPositionX(HumXT, new Vector3(-6.152f, 0f, 21.2f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 3)
            {
                SetPositionX(HumXT_Sit, new Vector3(-5.306f, -0.39f, 20.818f), new Vector3(0f, 145f, 0f));
            }
            if (numScene == 4)
            {
                SetPositionX(HumXT, new Vector3(-4.119f, 0f, 19.767f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 5)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.48f, -0.39f, 19.905f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 6)
            {
                SetPositionX(HumXT_Sit, new Vector3(-4.944f, -0.567f, 17.369f), new Vector3(0f, 325f, 0f));
            }
            if (numScene == 7)
            {
                SetPositionX(HumXT, new Vector3(-4.091f, 0f, 14.94f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 8)
            {
                SetPositionX(HumXT, new Vector3(-5.3f, 0f, 12.58f), new Vector3(0f, 210f, 0f));
            }
            if (numScene == 9)
            {
                SetPositionX(HumXT_Sit, new Vector3(-5.523f, -0.329f, 9.58f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 10)
            {
                SetPositionX(HumXT, new Vector3(-4.437f, 0f, 9.917f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 11)
            {
                SetPositionX(HumXT, new Vector3(-4.437f, 0f, 7.993f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 12)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.291f, -0.329f, 8.165f), new Vector3(0f, 0f, 0f));

            }
            if (numScene == 13)
            {
                SetPositionX(HumXT_Sit, new Vector3(-3.759f, -0.598f, 22.261f), new Vector3(0f, 250f, 0f));
                SetPositionY(AvaYT, new Vector3(-5f, 0f, 21.54f), new Vector3(0f, 60f, 0f));
                SetPositionY(HumYT, new Vector3(11.644f, 0f, 68.024f), new Vector3(0f, 120f, 0f));
            }
            if (numScene == 14)
            {
                SetPositionX(HumXT, new Vector3(-6.152f, 0f, 21.2f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.05f, 0f, 19.895f), new Vector3(0f, 310f, 0f));
                SetPositionY(HumYT, new Vector3(11.208f, 0f, 67.877f), new Vector3(0f, 30f, 0f));
            }
            if (numScene == 15)
            {
                SetPositionX(HumXT_Sit, new Vector3(-5.306f, -0.39f, 20.818f), new Vector3(0f, 145f, 0f));
                SetPositionY(AvaYT, new Vector3(-5.05f, 0f, 18.2f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT, new Vector3(10.52f, 0f, 68.781f), new Vector3(0f, 120f, 0f));
            }
            if (numScene == 16)
            {
                SetPositionX(HumXT, new Vector3(-4.119f, 0f, 19.767f), new Vector3(0f, 270f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.71f, 0f, 18.32f), new Vector3(0f, 70f, 0f));
                SetPositionY(HumYT, new Vector3(10.52f, 0f, 67.883f), new Vector3(0f, 80f, 0f));
            }
            if (numScene == 17)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.48f, -0.39f, 19.905f), new Vector3(0f, 90f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.548f, -0.448f, 18.629f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(11.115f, -0.304f, 69.968f), new Vector3(0f, 120f, 0f));
            }
            if (numScene == 18)
            {
                SetPositionX(HumXT_Sit, new Vector3(-4.944f, -0.567f, 17.369f), new Vector3(0f, 325f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.548f, -0.448f, 19.28f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(11.668f, -0.304f, 69.968f), new Vector3(0f, 130f, 0f));
            }
            if (numScene == 19)
            {
                SetPositionX(HumXT, new Vector3(-4.091f, 0f, 14.94f), new Vector3(0f, 260f, 0f));
                SetPositionY(AvaYT, new Vector3(-5.24f, 0f, 14.64f), new Vector3(0f, 80f, 0f));
                SetPositionY(HumYT, new Vector3(9.49f, 0f, 69.01f), new Vector3(0f, 170f, 0f));
            }
            if (numScene == 20)
            {
                SetPositionX(HumXT, new Vector3(-5.3f, 0f, 12.58f), new Vector3(0f, 210f, 0f));
                SetPositionY(AvaYT, new Vector3(-6.14f, 0f, 12.9f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT, new Vector3(8.84f, 0f, 69.62f), new Vector3(0f, 320f, 0f));
            }
            if (numScene == 21)
            {
                SetPositionX(HumXT_Sit, new Vector3(-5.523f, -0.329f, 9.58f), new Vector3(0f, 180f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.297f, -0.386f, 9.641f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(7.948f, -0.105f, 67.74f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 22)
            {
                SetPositionX(HumXT, new Vector3(-4.437f, 0f, 9.917f), new Vector3(0f, 90f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.297f, -0.386f, 8.12f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(7.914f, -0.149f, 69.14f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 23)
            {
                SetPositionX(HumXT, new Vector3(-4.437f, 0f, 7.993f), new Vector3(0f, 180f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.691f, 0f, 13.9f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT, new Vector3(9.408f, 0f, 68.729f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 24)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.291f, -0.329f, 8.165f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.297f, -0.386f, 9.641f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(7.948f, -0.105f, 67.74f), new Vector3(0f, 0f, 0f));
            }

            if (numScene == 25)
            {
                SetPositionX(HumXT, new Vector3(-5f, 0f, 21.54f), new Vector3(0f, 60f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-3.759f, -0.598f, 22.261f), new Vector3(0f, 250f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(12.688f, -0.261f, 67.413f), new Vector3(0f, 340f, 0f));
            }
            if (numScene == 26)
            {
                SetPositionX(HumXT, new Vector3(-4.05f, 0f, 19.895f), new Vector3(0f, 310f, 0f));
                SetPositionY(AvaYT, new Vector3(-6.152f, 0f, 21.2f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT, new Vector3(12.905f, 0f, 69.37f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 27)
            {
                SetPositionX(HumXT, new Vector3(-5.05f, 0f, 18.2f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-5.306f, -0.39f, 20.818f), new Vector3(0f, 145f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(11.872f, -0.261f, 67.367f), new Vector3(0f, 60f, 0f));
            }
            if (numScene == 28)
            {
                SetPositionX(HumXT, new Vector3(-4.71f, 0f, 18.32f), new Vector3(0f, 70f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.119f, 0f, 19.767f), new Vector3(0f, 270f, 0f));
                SetPositionY(HumYT, new Vector3(12.447f, 0f, 67.876f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 29)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.548f, -0.448f, 18.629f), new Vector3(0f, 90f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.48f, -0.39f, 19.905f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(12.267f, -0.295f, 69.997f), new Vector3(0f, 130f, 0f));
            }
            if (numScene == 30)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.548f, -0.448f, 19.28f), new Vector3(0f, 90f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-4.944f, -0.567f, 17.369f), new Vector3(0f, 325f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(10.931f, -0.295f, 67.18f), new Vector3(0f, 30f, 0f));
            }
            if (numScene == 31)
            {
                SetPositionX(HumXT, new Vector3(-5.24f, 0f, 14.64f), new Vector3(0f, 80f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.091f, 0f, 14.94f), new Vector3(0f, 260f, 0f));
                SetPositionY(HumYT, new Vector3(9.76f, 0f, 67.876f), new Vector3(0f, 340f, 0f));
            }
            if (numScene == 32)
            {
                SetPositionX(HumXT, new Vector3(-3.76f, 0f, 12.055f), new Vector3(0f, 330f, 0f));
                SetPositionY(AvaYT, new Vector3(-5.874f, 0f, 14.158f), new Vector3(0f, 60f, 0f));
                SetPositionY(HumYT, new Vector3(9.453f, 0f, 69.317f), new Vector3(0f, 150f, 0f));
            }
            if (numScene == 33)
            {
                SetPositionX(HumXT, new Vector3(-4.906f, 0f, 7.863f), new Vector3(0f, 330f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.297f, -0.386f, 9.641f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(7.948f, -0.105f, 67.74f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 34)
            {
                SetPositionX(HumXT_Sit, new Vector3(-6.297f, -0.386f, 8.12f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.437f, 0f, 9.917f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT, new Vector3(7.272f, 0f, 67.218f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 35)
            {
                SetPositionX(HumXT, new Vector3(-4.691f, 0f, 13.9f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT, new Vector3(-4.437f, 0f, 7.993f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT, new Vector3(5.932f, 0f, 67.218f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 36)
            {
                SetPositionX(HumXT_Sit, new Vector3(-5.542f, -0.35f, 9.6209f), new Vector3(0f, 180f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(-6.291f, -0.329f, 8.165f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(7.914f, -0.149f, 69.14f), new Vector3(0f, 180f, 0f));
            }
        }
        if (numPair == 7)
        {
            DeactAvatars();
            if (numScene == 1)
            {
                SetPositionX(HumXT_Sit, new Vector3(4.905f, -0.393f, 9.889f), new Vector3(0f, 25f, 0f));
            }
            if (numScene == 2)
            {
                SetPositionX(HumXT_Sit, new Vector3(5.784f, -0.388f, 10.986f), new Vector3(0f, 255f, 0f));
            }
            if (numScene == 3)
            {
                SetPositionX(HumXT, new Vector3(6.609f, 0f, 9.209f), new Vector3(0f, 330f, 0f));
            }
            if (numScene == 4)
            {
                SetPositionX(HumXT, new Vector3(7.496f, 0f, 11.506f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 5)
            {
                SetPositionX(HumXT_Sit, new Vector3(5.56f, -0.089f, 12.458f), new Vector3(0f, 40f, 0f));
            }
            if (numScene == 6)
            {
                SetPositionX(HumXT, new Vector3(6.151f, 0f, 13.258f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 7)
            {
                SetPositionX(HumXT_Sit, new Vector3(6.992f, -0.396f, 13.305f), new Vector3(0f, 40f, 0f));
            }
            if (numScene == 8)
            {
                SetPositionX(HumXT, new Vector3(7.85f, 0f, 11.877f), new Vector3(0f, 45f, 0f));
            }
            if (numScene == 9)
            {
                SetPositionX(HumXT_Sit, new Vector3(9.357f, -0.47f, 12.371f), new Vector3(0f, 130f, 0f));
            }
            if (numScene == 10)
            {
                SetPositionX(HumXT_Sit, new Vector3(10.91f, -0.286f, 14.009f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 11)
            {
                SetPositionX(HumXT_Sit, new Vector3(11.892f, -0.489f, 13.261f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 12)
            {
                SetPositionX(HumXT, new Vector3(12.444f, 0f, 13.935f), new Vector3(0f, 0f, 0f));

            }
            if (numScene == 13)
            {
                SetPositionX(HumXT_Sit, new Vector3(4.905f, -0.493f, 9.889f), new Vector3(0f, 25f, 0f));
                SetPositionY(AvaYT, new Vector3(5.388f, 0f, 11.458f), new Vector3(0f, 200f, 0f));
                SetPositionY(HumYT, new Vector3(-8.761f, 0f, 32.07f), new Vector3(0f, 330f, 0f));
            }
            if (numScene == 14)
            {
                SetPositionX(HumXT_Sit, new Vector3(5.737f, -0.493f, 10.974f), new Vector3(0f, 255f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(4.859f, -0.429f, 9.868f), new Vector3(0f, 25f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-9.428f, -0.374f, 34.378f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 15)
            {
                SetPositionX(HumXT, new Vector3(6.609f, 0f, 9.209f), new Vector3(0f, 330f, 0f));
                SetPositionY(AvaYT, new Vector3(6.398f, 0f, 11.133f), new Vector3(0f, 60f, 0f));
                SetPositionY(HumYT, new Vector3(-9.358f, 0f, 31.116f), new Vector3(0f, 50f, 0f));
            }
            if (numScene == 16)
            {
                SetPositionX(HumXT, new Vector3(7.496f, 0f, 11.506f), new Vector3(0f, 270f, 0f));
                SetPositionY(AvaYT, new Vector3(6.18f, 0f, 13.23f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT, new Vector3(-6.34f, 0f, 34.49f), new Vector3(0f, 330f, 0f));
            }
            if (numScene == 17)
            {
                SetPositionX(HumXT_Sit, new Vector3(5.56f, -0.089f, 12.458f), new Vector3(0f, 40f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(6.97f, -0.468f, 14.333f), new Vector3(0f, 135f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-10.14f, -0.341f, 33.589f), new Vector3(0f, 90f, 0f));
            }
            ////////////
            if (numScene == 18)
            {
                SetPositionX(HumXT, new Vector3(6.151f, 0f, 13.258f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(8.008f, -0.393f, 14.389f), new Vector3(0f, 225f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-8.696f, -0.341f, 33.589f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 19)
            {
                SetPositionX(HumXT_Sit, new Vector3(6.992f, -0.396f, 13.305f), new Vector3(0f, 40f, 0f));
                SetPositionY(AvaYT, new Vector3(8.775f, 0f, 13.842f), new Vector3(0f, 140f, 0f));
                SetPositionY(HumYT, new Vector3(-8.149f, 0f, 31.839f), new Vector3(0f, 70f, 0f));
            }
            if (numScene == 20)
            {
                SetPositionX(HumXT, new Vector3(7.85f, 0f, 11.877f), new Vector3(0f, 45f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(10.288f, -0.328f, 14.138f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-7.175f, -0.334f, 33.418f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 21)
            {
                SetPositionX(HumXT_Sit, new Vector3(9.357f, -0.47f, 12.371f), new Vector3(0f, 130f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(11.728f, -0.328f, 12.516f), new Vector3(0f, 220f, 0f));
                SetPositionY(HumYT, new Vector3(-6.41f, 0f, 34.64f), new Vector3(0f, 120f, 0f));
            }
            if (numScene == 22)
            {
                SetPositionX(HumXT_Sit, new Vector3(10.91f, -0.286f, 14.009f), new Vector3(0f, 180f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(10.271f, -0.328f, 14.127f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-7.175f, -0.334f, 33.418f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 23)
            {
                SetPositionX(HumXT_Sit, new Vector3(11.892f, -0.489f, 13.261f), new Vector3(0f, 270f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(9.249f, -0.499f, 12.405f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-5.874f, -0.289f, 31.772f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 24)
            {
                SetPositionX(HumXT, new Vector3(12.444f, 0f, 13.935f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT, new Vector3(12.52f, 0f, 15.529f), new Vector3(0f, 300f, 0f));
                SetPositionY(HumYT, new Vector3(-5.328f, 0f, 34.974f), new Vector3(0f, 0f, 0f));
            }

            if (numScene == 25)
            {
                SetPositionY(AvaYT_Sit, new Vector3(4.905f, -0.493f, 9.889f), new Vector3(0f, 25f, 0f));
                SetPositionX(HumXT, new Vector3(5.388f, 0f, 11.458f), new Vector3(0f, 200f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-10.14f, -0.341f, 33.589f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 26)
            {
                SetPositionY(AvaYT_Sit, new Vector3(5.737f, -0.493f, 10.974f), new Vector3(0f, 255f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(4.859f, -0.429f, 9.868f), new Vector3(0f, 25f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-9.479f, -0.37f, 32.146f), new Vector3(0f, 30f, 0f));
            }
            if (numScene == 27)
            {
                SetPositionY(AvaYT, new Vector3(6.609f, 0f, 9.209f), new Vector3(0f, 330f, 0f));
                SetPositionX(HumXT, new Vector3(6.398f, 0f, 11.133f), new Vector3(0f, 60f, 0f));
                SetPositionY(HumYT, new Vector3(-11.39f, 0f, 29.63f), new Vector3(0f, 30f, 0f));
            }
            if (numScene == 28)
            {
                SetPositionY(AvaYT, new Vector3(7.496f, 0f, 11.506f), new Vector3(0f, 270f, 0f));
                SetPositionX(HumXT, new Vector3(6.18f, 0f, 13.23f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT, new Vector3(-10.09f, 0f, 30.95f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 29)
            {
                SetPositionY(AvaYT_Sit, new Vector3(5.56f, -0.089f, 12.458f), new Vector3(0f, 40f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(6.97f, -0.468f, 14.333f), new Vector3(0f, 135f, 0f));
                SetPositionY(HumYT, new Vector3(-8.43f, 0f, 32.06f), new Vector3(0f, 330f, 0f));
            }
            ////////////
            if (numScene == 30)
            {
                SetPositionY(AvaYT, new Vector3(6.151f, 0f, 13.258f), new Vector3(0f, 0f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(8.008f, -0.393f, 14.389f), new Vector3(0f, 225f, 0f));
                SetPositionY(HumYT, new Vector3(-8.43f, 0f, 34.24f), new Vector3(0f, 15f, 0f));
            }
            if (numScene == 31)
            {
                SetPositionY(AvaYT_Sit, new Vector3(6.992f, -0.396f, 13.305f), new Vector3(0f, 40f, 0f));
                SetPositionX(HumXT, new Vector3(8.775f, 0f, 13.842f), new Vector3(0f, 140f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-10.1f, -0.37f, 32.973f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 32)
            {
                SetPositionY(AvaYT, new Vector3(7.85f, 0f, 11.877f), new Vector3(0f, 45f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(10.288f, -0.328f, 14.138f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT, new Vector3(-4.837f, 0f, 31.332f), new Vector3(0f, 330f, 0f));
            }
            if (numScene == 33)
            {
                SetPositionY(AvaYT_Sit, new Vector3(9.357f, -0.47f, 12.371f), new Vector3(0f, 130f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(11.728f, -0.328f, 12.516f), new Vector3(0f, 220f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-6.048f, -0.224f, 31.912f), new Vector3(0f, 45f, 0f));
            }
            if (numScene == 34)
            {
                SetPositionY(AvaYT_Sit, new Vector3(10.91f, -0.286f, 14.009f), new Vector3(0f, 180f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(10.271f, -0.328f, 14.127f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-7.175f, -0.334f, 33.418f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 35)
            {
                SetPositionY(AvaYT_Sit, new Vector3(11.892f, -0.489f, 13.261f), new Vector3(0f, 270f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(9.249f, -0.499f, 12.405f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-5.742f, -0.224f, 35.643f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 36)
            {
                SetPositionY(AvaYT, new Vector3(12.444f, 0f, 13.935f), new Vector3(0f, 0f, 0f));
                SetPositionX(HumXT, new Vector3(12.52f, 0f, 15.529f), new Vector3(0f, 300f, 0f));
                SetPositionY(HumYT, new Vector3(-5.148f, 0f, 33.93f), new Vector3(0f, 0f, 0f));
            }
        }
        if (numPair == 8)
        {
            DeactAvatars();
            if (numScene == 1)
            {
                SetPositionX(HumXT_Sit, new Vector3(4.905f, -0.393f, 9.889f), new Vector3(0f, 25f, 0f));
            }
            if (numScene == 2)
            {
                SetPositionX(HumXT_Sit, new Vector3(5.784f, -0.388f, 10.986f), new Vector3(0f, 255f, 0f));
            }
            if (numScene == 3)
            {
                SetPositionX(HumXT, new Vector3(6.609f, 0f, 9.209f), new Vector3(0f, 330f, 0f));
            }
            if (numScene == 4)
            {
                SetPositionX(HumXT, new Vector3(7.496f, 0f, 11.506f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 5)
            {
                SetPositionX(HumXT_Sit, new Vector3(5.56f, -0.089f, 12.458f), new Vector3(0f, 40f, 0f));
            }
            if (numScene == 6)
            {
                SetPositionX(HumXT, new Vector3(6.151f, 0f, 13.258f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 7)
            {
                SetPositionX(HumXT_Sit, new Vector3(6.992f, -0.396f, 13.305f), new Vector3(0f, 40f, 0f));
            }
            if (numScene == 8)
            {
                SetPositionX(HumXT, new Vector3(7.85f, 0f, 11.877f), new Vector3(0f, 45f, 0f));
            }
            if (numScene == 9)
            {
                SetPositionX(HumXT_Sit, new Vector3(9.357f, -0.47f, 12.371f), new Vector3(0f, 130f, 0f));
            }
            if (numScene == 10)
            {
                SetPositionX(HumXT_Sit, new Vector3(10.91f, -0.286f, 14.009f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 11)
            {
                SetPositionX(HumXT_Sit, new Vector3(11.892f, -0.489f, 13.261f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 12)
            {
                SetPositionX(HumXT, new Vector3(12.444f, 0f, 13.935f), new Vector3(0f, 0f, 0f));

            }
            if (numScene == 13)
            {
                SetPositionX(HumXT_Sit, new Vector3(4.905f, -0.493f, 9.889f), new Vector3(0f, 25f, 0f));
                SetPositionY(AvaYT, new Vector3(5.388f, 0f, 11.458f), new Vector3(0f, 200f, 0f));
                SetPositionY(HumYT, new Vector3(9.58f, 0f, 30.94f), new Vector3(0f, 120f, 0f));
            }
            if (numScene == 14)
            {
                SetPositionX(HumXT_Sit, new Vector3(5.737f, -0.493f, 10.974f), new Vector3(0f, 255f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(4.859f, -0.429f, 9.868f), new Vector3(0f, 25f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(9.821f, -0.174f, 29.313f), new Vector3(0f, 70f, 0f));
            }
            if (numScene == 15)
            {
                SetPositionX(HumXT, new Vector3(6.609f, 0f, 9.209f), new Vector3(0f, 330f, 0f));
                SetPositionY(AvaYT, new Vector3(6.398f, 0f, 11.133f), new Vector3(0f, 60f, 0f));
                SetPositionY(HumYT, new Vector3(9.09f, 0f, 32.14f), new Vector3(0f, 50f, 0f));
            }
            if (numScene == 16)
            {
                SetPositionX(HumXT, new Vector3(7.496f, 0f, 11.506f), new Vector3(0f, 270f, 0f));
                SetPositionY(AvaYT, new Vector3(6.18f, 0f, 13.23f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT, new Vector3(9.23f, 0f, 30.13f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 17)
            {
                SetPositionX(HumXT_Sit, new Vector3(5.56f, -0.089f, 12.458f), new Vector3(0f, 40f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(6.97f, -0.468f, 14.333f), new Vector3(0f, 135f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(11.185f, -0.174f, 30.473f), new Vector3(0f, 330f, 0f));
            }
            ////////////
            if (numScene == 18)
            {
                SetPositionX(HumXT, new Vector3(6.151f, 0f, 13.258f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(8.008f, -0.393f, 14.389f), new Vector3(0f, 225f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(11.205f, -0.174f, 29.381f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 19)
            {
                SetPositionX(HumXT_Sit, new Vector3(6.992f, -0.396f, 13.305f), new Vector3(0f, 40f, 0f));
                SetPositionY(AvaYT, new Vector3(8.775f, 0f, 13.842f), new Vector3(0f, 140f, 0f));
                SetPositionY(HumYT, new Vector3(11.704f, 0f, 32.119f), new Vector3(0f, 320f, 0f));
            }
            if (numScene == 20)
            {
                SetPositionX(HumXT, new Vector3(7.85f, 0f, 11.877f), new Vector3(0f, 45f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(10.288f, -0.328f, 14.138f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(12.218f, -0.322f, 34.428f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 21)
            {
                SetPositionX(HumXT_Sit, new Vector3(9.357f, -0.47f, 12.371f), new Vector3(0f, 130f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(11.728f, -0.328f, 12.516f), new Vector3(0f, 220f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(10.584f, -0.322f, 33.13f), new Vector3(0f, 300f, 0f));
            }
            if (numScene == 22)
            {
                SetPositionX(HumXT_Sit, new Vector3(10.91f, -0.286f, 14.009f), new Vector3(0f, 180f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(10.271f, -0.328f, 14.127f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(12.218f, -0.322f, 34.428f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 23)
            {
                SetPositionX(HumXT_Sit, new Vector3(11.892f, -0.489f, 13.261f), new Vector3(0f, 270f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(9.249f, -0.499f, 12.405f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(10.238f, -0.322f, 36.387f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 24)
            {
                SetPositionX(HumXT, new Vector3(12.444f, 0f, 13.935f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT, new Vector3(12.52f, 0f, 15.529f), new Vector3(0f, 300f, 0f));
                SetPositionY(HumYT, new Vector3(9.419f, 0f, 35.962f), new Vector3(0f, 0f, 0f));
            }

            if (numScene == 25)
            {
                SetPositionY(AvaYT_Sit, new Vector3(4.905f, -0.493f, 9.889f), new Vector3(0f, 25f, 0f));
                SetPositionX(HumXT, new Vector3(5.388f, 0f, 11.458f), new Vector3(0f, 200f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(9.805f, -0.201f, 29.448f), new Vector3(0f, 70f, 0f));
            }
            if (numScene == 26)
            {
                SetPositionY(AvaYT_Sit, new Vector3(5.737f, -0.493f, 10.974f), new Vector3(0f, 255f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(4.859f, -0.429f, 9.868f), new Vector3(0f, 25f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(11.18f, -0.201f, 30.372f), new Vector3(0f, 250f, 0f));
            }
            if (numScene == 27)
            {
                SetPositionY(AvaYT, new Vector3(6.609f, 0f, 9.209f), new Vector3(0f, 330f, 0f));
                SetPositionX(HumXT, new Vector3(6.398f, 0f, 11.133f), new Vector3(0f, 60f, 0f));
                SetPositionY(HumYT, new Vector3(9.03f, 0f, 28.19f), new Vector3(0f, 30f, 0f));
            }
            if (numScene == 28)
            {
                SetPositionY(AvaYT, new Vector3(7.496f, 0f, 11.506f), new Vector3(0f, 270f, 0f));
                SetPositionX(HumXT, new Vector3(6.18f, 0f, 13.23f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT, new Vector3(9.03f, 0f, 33.06f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 29)
            {
                SetPositionY(AvaYT_Sit, new Vector3(5.56f, -0.089f, 12.458f), new Vector3(0f, 40f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(6.97f, -0.468f, 14.333f), new Vector3(0f, 135f, 0f));
                SetPositionY(HumYT, new Vector3(12.12f, 0f, 31.85f), new Vector3(0f, 210f, 0f));
            }
            ////////////
            if (numScene == 30)
            {
                SetPositionY(AvaYT, new Vector3(6.151f, 0f, 13.258f), new Vector3(0f, 0f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(8.008f, -0.393f, 14.389f), new Vector3(0f, 225f, 0f));
                SetPositionY(HumYT, new Vector3(9.442f, 0f, 28.673f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 31)
            {
                SetPositionY(AvaYT_Sit, new Vector3(6.992f, -0.396f, 13.305f), new Vector3(0f, 40f, 0f));
                SetPositionX(HumXT, new Vector3(8.775f, 0f, 13.842f), new Vector3(0f, 140f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(9.805f, -0.201f, 29.448f), new Vector3(0f, 50f, 0f));
            }
            if (numScene == 32)
            {
                SetPositionY(AvaYT, new Vector3(7.85f, 0f, 11.877f), new Vector3(0f, 45f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(10.288f, -0.328f, 14.138f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT, new Vector3(9.61f, 0f, 32.16f), new Vector3(0f, 50f, 0f));
            }
            if (numScene == 33)
            {
                SetPositionY(AvaYT_Sit, new Vector3(9.357f, -0.47f, 12.371f), new Vector3(0f, 130f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(11.728f, -0.328f, 12.516f), new Vector3(0f, 220f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(10.639f, -0.201f, 36.24f), new Vector3(0f, 220f, 0f));
            }
            if (numScene == 34)
            {
                SetPositionY(AvaYT_Sit, new Vector3(10.91f, -0.286f, 14.009f), new Vector3(0f, 180f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(10.271f, -0.328f, 14.127f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(12.196f, -0.201f, 33.907f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 35)
            {
                SetPositionY(AvaYT_Sit, new Vector3(11.892f, -0.489f, 13.261f), new Vector3(0f, 270f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(9.249f, -0.499f, 12.405f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(10.94f, -0.302f, 33.024f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 36)
            {
                SetPositionY(AvaYT, new Vector3(12.444f, 0f, 13.935f), new Vector3(0f, 0f, 0f));
                SetPositionX(HumXT, new Vector3(12.52f, 0f, 15.529f), new Vector3(0f, 300f, 0f));
                SetPositionY(HumYT, new Vector3(9.61f, 0f, 35.078f), new Vector3(0f, 0f, 0f));
            }
        }
        if (numPair == 9)
        {
            DeactAvatars();
            if (numScene == 1)
            {
                SetPositionX(HumXT_Sit, new Vector3(4.905f, -0.393f, 9.889f), new Vector3(0f, 25f, 0f));
            }
            if (numScene == 2)
            {
                SetPositionX(HumXT_Sit, new Vector3(5.784f, -0.388f, 10.986f), new Vector3(0f, 255f, 0f));
            }
            if (numScene == 3)
            {
                SetPositionX(HumXT, new Vector3(6.609f, 0f, 9.209f), new Vector3(0f, 330f, 0f));
            }
            if (numScene == 4)
            {
                SetPositionX(HumXT, new Vector3(7.496f, 0f, 11.506f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 5)
            {
                SetPositionX(HumXT_Sit, new Vector3(5.56f, -0.089f, 12.458f), new Vector3(0f, 40f, 0f));
            }
            if (numScene == 6)
            {
                SetPositionX(HumXT, new Vector3(6.151f, 0f, 13.258f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 7)
            {
                SetPositionX(HumXT_Sit, new Vector3(6.992f, -0.396f, 13.305f), new Vector3(0f, 40f, 0f));
            }
            if (numScene == 8)
            {
                SetPositionX(HumXT, new Vector3(7.85f, 0f, 11.877f), new Vector3(0f, 45f, 0f));
            }
            if (numScene == 9)
            {
                SetPositionX(HumXT_Sit, new Vector3(9.357f, -0.47f, 12.371f), new Vector3(0f, 130f, 0f));
            }
            if (numScene == 10)
            {
                SetPositionX(HumXT_Sit, new Vector3(10.91f, -0.286f, 14.009f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 11)
            {
                SetPositionX(HumXT_Sit, new Vector3(11.892f, -0.489f, 13.261f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 12)
            {
                SetPositionX(HumXT, new Vector3(12.444f, 0f, 13.935f), new Vector3(0f, 0f, 0f));

            }
            if (numScene == 13)
            {
                SetPositionX(HumXT_Sit, new Vector3(4.905f, -0.493f, 9.889f), new Vector3(0f, 25f, 0f));
                SetPositionY(AvaYT, new Vector3(5.388f, 0f, 11.458f), new Vector3(0f, 200f, 0f));
                SetPositionY(HumYT, new Vector3(-4.84f, 0f, 49.704f), new Vector3(0f, 220f, 0f));
            }
            if (numScene == 14)
            {
                SetPositionX(HumXT_Sit, new Vector3(5.737f, -0.493f, 10.974f), new Vector3(0f, 255f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(4.859f, -0.429f, 9.868f), new Vector3(0f, 25f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-5.98f, -0.265f, 47.789f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 15)
            {
                SetPositionX(HumXT, new Vector3(6.609f, 0f, 9.209f), new Vector3(0f, 330f, 0f));
                SetPositionY(AvaYT, new Vector3(6.398f, 0f, 11.133f), new Vector3(0f, 60f, 0f));
                SetPositionY(HumYT, new Vector3(-6.66f, 0f, 49.84f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 16)
            {
                SetPositionX(HumXT, new Vector3(7.496f, 0f, 11.506f), new Vector3(0f, 270f, 0f));
                SetPositionY(AvaYT, new Vector3(6.18f, 0f, 13.23f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT, new Vector3(-3.93f, 0f, 45.15f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 17)
            {
                SetPositionX(HumXT_Sit, new Vector3(5.56f, -0.089f, 12.458f), new Vector3(0f, 40f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(6.97f, -0.468f, 14.333f), new Vector3(0f, 135f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-5.185f, -0.265f, 47.789f), new Vector3(0f, 0f, 0f));
            }
            ////////////
            if (numScene == 18)
            {
                SetPositionX(HumXT, new Vector3(6.151f, 0f, 13.258f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(8.008f, -0.393f, 14.389f), new Vector3(0f, 225f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-5.96f, -0.265f, 49.197f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 19)
            {
                SetPositionX(HumXT_Sit, new Vector3(6.992f, -0.396f, 13.305f), new Vector3(0f, 40f, 0f));
                SetPositionY(AvaYT, new Vector3(8.775f, 0f, 13.842f), new Vector3(0f, 140f, 0f));
                SetPositionY(HumYT, new Vector3(-7.364f, 0f, 55.959f), new Vector3(0f, 120f, 0f));
            }
            if (numScene == 20)
            {
                SetPositionX(HumXT, new Vector3(7.85f, 0f, 11.877f), new Vector3(0f, 45f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(10.288f, -0.328f, 14.138f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-5.185f, -0.265f, 55.883f), new Vector3(0f, 150f, 0f));
            }
            if (numScene == 21)
            {
                SetPositionX(HumXT_Sit, new Vector3(9.357f, -0.47f, 12.371f), new Vector3(0f, 130f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(11.728f, -0.328f, 12.516f), new Vector3(0f, 220f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-6.028f, -0.462f, 53.301f), new Vector3(0f, 70f, 0f));
            }
            if (numScene == 22)
            {
                SetPositionX(HumXT_Sit, new Vector3(10.91f, -0.286f, 14.009f), new Vector3(0f, 180f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(10.271f, -0.328f, 14.127f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-5.185f, -0.265f, 55.883f), new Vector3(0f, 150f, 0f));
            }
            if (numScene == 23)
            {
                SetPositionX(HumXT_Sit, new Vector3(11.892f, -0.489f, 13.261f), new Vector3(0f, 270f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(9.249f, -0.499f, 12.405f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-4.673f, -0.462f, 53.098f), new Vector3(0f, 320f, 0f));
            }
            if (numScene == 24)
            {
                SetPositionX(HumXT, new Vector3(12.444f, 0f, 13.935f), new Vector3(0f, 0f, 0f));
                SetPositionY(AvaYT, new Vector3(12.52f, 0f, 15.529f), new Vector3(0f, 300f, 0f));
                SetPositionY(HumYT, new Vector3(-4.54f, 0f, 57.34f), new Vector3(0f, 300f, 0f));
            }

            if (numScene == 25)
            {
                SetPositionY(AvaYT_Sit, new Vector3(4.905f, -0.493f, 9.889f), new Vector3(0f, 25f, 0f));
                SetPositionX(HumXT, new Vector3(5.388f, 0f, 11.458f), new Vector3(0f, 200f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-5.98f, -0.249f, 47.789f), new Vector3(0f, 30f, 0f)); ;
            }
            if (numScene == 26)
            {
                SetPositionY(AvaYT_Sit, new Vector3(5.737f, -0.493f, 10.974f), new Vector3(0f, 255f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(4.859f, -0.429f, 9.868f), new Vector3(0f, 25f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-5.211f, -0.276f, 49.11f), new Vector3(0f, 210f, 0f));
            }
            if (numScene == 27)
            {
                SetPositionY(AvaYT, new Vector3(6.609f, 0f, 9.209f), new Vector3(0f, 330f, 0f));
                SetPositionX(HumXT, new Vector3(6.398f, 0f, 11.133f), new Vector3(0f, 60f, 0f));
                SetPositionY(HumYT, new Vector3(-6.803f, 0f, 46.857f), new Vector3(0f, 40f, 0f));
            }
            if (numScene == 28)
            {
                SetPositionY(AvaYT, new Vector3(7.496f, 0f, 11.506f), new Vector3(0f, 270f, 0f));
                SetPositionX(HumXT, new Vector3(6.18f, 0f, 13.23f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT, new Vector3(-4.161f, 0f, 50.738f), new Vector3(0f, 200f, 0f));
            }
            if (numScene == 29)
            {
                SetPositionY(AvaYT_Sit, new Vector3(5.56f, -0.089f, 12.458f), new Vector3(0f, 40f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(6.97f, -0.468f, 14.333f), new Vector3(0f, 135f, 0f));
                SetPositionY(HumYT, new Vector3(-4.161f, 0f, 45.239f), new Vector3(0f, 330f, 0f));
            }
            ////////////
            if (numScene == 30)
            {
                SetPositionY(AvaYT, new Vector3(6.151f, 0f, 13.258f), new Vector3(0f, 0f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(8.008f, -0.393f, 14.389f), new Vector3(0f, 225f, 0f));
                SetPositionY(HumYT, new Vector3(-4.972f, 0f, 45.239f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 31)
            {
                SetPositionY(AvaYT_Sit, new Vector3(6.992f, -0.396f, 13.305f), new Vector3(0f, 40f, 0f));
                SetPositionX(HumXT, new Vector3(8.775f, 0f, 13.842f), new Vector3(0f, 140f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-5.185f, -0.265f, 47.789f), new Vector3(0f, 330f, 0f));
            }
            if (numScene == 32)
            {
                SetPositionY(AvaYT, new Vector3(7.85f, 0f, 11.877f), new Vector3(0f, 45f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(10.288f, -0.328f, 14.138f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT, new Vector3(-4.216f, 0f, 52.12f), new Vector3(0f, 345f, 0f));
            }
            if (numScene == 33)
            {
                SetPositionY(AvaYT_Sit, new Vector3(9.357f, -0.47f, 12.371f), new Vector3(0f, 130f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(11.728f, -0.328f, 12.516f), new Vector3(0f, 220f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-5.93f, -0.437f, 53.16f), new Vector3(0f, 80f, 0f));
            }
            if (numScene == 34)
            {
                SetPositionY(AvaYT_Sit, new Vector3(10.91f, -0.286f, 14.009f), new Vector3(0f, 180f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(10.271f, -0.328f, 14.127f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-6.716f, -0.328f, 54.975f), new Vector3(0f, 100f, 0f));
            }
            if (numScene == 35)
            {
                SetPositionY(AvaYT_Sit, new Vector3(11.892f, -0.489f, 13.261f), new Vector3(0f, 270f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(9.249f, -0.499f, 12.405f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-5.795f, -0.32f, 55.953f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 36)
            {
                SetPositionY(AvaYT, new Vector3(12.444f, 0f, 13.935f), new Vector3(0f, 0f, 0f));
                SetPositionX(HumXT, new Vector3(12.52f, 0f, 15.529f), new Vector3(0f, 300f, 0f));
                SetPositionY(HumYT, new Vector3(-3.707f, 0f, 55.613f), new Vector3(0f, 330f, 0f));
            }
        }

        if (numPair == 10)
        {
            DeactAvatars();
            if (numScene == 1)
            {
                SetPositionX(HumXT_Sit, new Vector3(10.983f, -0.294f, 13.992f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 2)
            {
                SetPositionX(HumXT_Sit, new Vector3(12.003f, -0.494f, 12.4694f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 3)
            {
                SetPositionX(HumXT, new Vector3(12.255f, 0f, 14.499f), new Vector3(0f, -30f, 0f));
            }
            if (numScene == 4)
            {
                SetPositionX(HumXT, new Vector3(12.32447f, 0f, 11.91559f), new Vector3(0f, -134.321f, 0f));
            }
            if (numScene == 5)
            {
                SetPositionX(HumXT_Sit, new Vector3(8.006f, -0.393f, 14.39f), new Vector3(0f, 224.648f, 0f));
            }
            if (numScene == 6)
            {
                SetPositionX(HumXT, new Vector3(8.491033f, 0f, 13.62631f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 7)
            {
                SetPositionX(HumXT_Sit, new Vector3(5.501f, -0.029f, 12.383f), new Vector3(0f, 400f, 0f));
            }
            if (numScene == 8)
            {
                SetPositionX(HumXT, new Vector3(6.833f, 0f, 12.51f), new Vector3(0f, 45f, 0f));
            }
            if (numScene == 9)
            {
                SetPositionX(HumXT_Sit, new Vector3(5.792f, -0.389f, 11.005f), new Vector3(0f, 252.672f, 0f));
            }
            if (numScene == 10)
            {
                SetPositionX(HumXT, new Vector3(4.432f, 0f, 9.037f), new Vector3(0f, 45f, 0f));
            }
            if (numScene == 11)
            {
                SetPositionX(HumXT_Sit, new Vector3(9.3f, -0.489f, 13.131f), new Vector3(0f, -270f, 0f));
            }
            if (numScene == 12)
            {
                SetPositionX(HumXT, new Vector3(4.779f, 0f, 9.128f), new Vector3(0f, -90f, 0f));

            }
            ////////////
            if (numScene == 13)
            {
                SetPositionX(HumXT_Sit, new Vector3(10.983f, -0.294f, 13.992f), new Vector3(0f, 180f, 0f));
                SetPositionY(AvaYT, new Vector3(11.08073f, 0f, 12.13628f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT, new Vector3(-5.95f, 0f, 70.19f), new Vector3(0f, -180f, 0f));
            }
            if (numScene == 14)
            {
                SetPositionX(HumXT_Sit, new Vector3(12.003f, -0.494f, 12.4694f), new Vector3(0f, 270f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(9.178f, -0.429f, 13.096f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-6.309f, -0.265f, 71.767f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 15)
            {
                SetPositionX(HumXT, new Vector3(12.255f, 0f, 14.499f), new Vector3(0f, -30f, 0f));
                SetPositionY(AvaYT, new Vector3(11.06736f, 0f, 13.18914f), new Vector3(0f, 44.821f, 0f));
                SetPositionY(HumYT, new Vector3(-7.914f, 0f, 69.42f), new Vector3(0f, -332.008f, 0f));
            }
            if (numScene == 16)
            {
                SetPositionX(HumXT, new Vector3(12.32447f, 0f, 11.91559f), new Vector3(0f, -134.321f, 0f));
                SetPositionY(AvaYT, new Vector3(10.78202f, 0f, 11.69818f), new Vector3(0f, 91.441f, 0f));
                SetPositionY(HumYT, new Vector3(-7.413f, 0f, 67.332f), new Vector3(0f, -629.969f, 0f));
            }
            if (numScene == 17)
            {
                SetPositionX(HumXT_Sit, new Vector3(8.006f, -0.393f, 14.39f), new Vector3(0f, 224.648f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(6.97f, -0.468f, 14.333f), new Vector3(0f, 135f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-10.555f, -0.366f, 69.878f), new Vector3(0f, 91.82401f, 0f));
            }
            ////////////
            if (numScene == 18)
            {
                SetPositionX(HumXT, new Vector3(8.491033f, 0f, 13.62631f), new Vector3(0f, 90f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(8.008f, -0.393f, 14.389f), new Vector3(0f, 225f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-9.088f, -0.378f, 69.218f), new Vector3(0f, -90f, 0f));
            }
            if (numScene == 19)
            {
                SetPositionX(HumXT_Sit, new Vector3(5.501f, -0.029f, 12.383f), new Vector3(0f, 400f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(12.066f, -0.5f, 13.17f), new Vector3(0f, -90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-7.666f, -0.367f, 68.434f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 20)
            {
                SetPositionX(HumXT, new Vector3(6.833f, 0f, 12.51f), new Vector3(0f, 45f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(11.988f, -0.532f, 13.198f), new Vector3(0f, -90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-6.361f, -0.256f, 71.752f), new Vector3(0f, -180f, 0f));
            }
            if (numScene == 21)
            {
                SetPositionX(HumXT_Sit, new Vector3(5.792f, -0.389f, 11.005f), new Vector3(0f, 252.672f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(4.32f, -0.613f, 11f), new Vector3(0f, -597.744f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-11.056f, -0.249f, 65.603f), new Vector3(0f, -42.3f, 0f));
            }
            if (numScene == 22)
            {
                SetPositionX(HumXT, new Vector3(4.432f, 0f, 9.037f), new Vector3(0f, 45f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(5.753975f, -0.428f, 11.08115f), new Vector3(0f, -506.038f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-9.118f, -0.373f, 69.878f), new Vector3(0f, -90f, 0f));
            }
            if (numScene == 23)
            {
                SetPositionX(HumXT_Sit, new Vector3(9.3f, -0.489f, 13.131f), new Vector3(0f, -270f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(9.249f, -0.499f, 12.405f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-6.884f, -0.328f, 68.409f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 24)
            {
                SetPositionX(HumXT, new Vector3(4.779f, 0f, 9.128f), new Vector3(0f, -90f, 0f));
                SetPositionY(AvaYT, new Vector3(5.367085f, 0f, 11.4929f), new Vector3(0f, -154.897f, 0f));
                SetPositionY(HumYT, new Vector3(-8.74f, 0f, 67.82f), new Vector3(0f, -32.68f, 0f));
            }

            ////////////
            if (numScene == 25)
            {
                SetPositionY(AvaYT_Sit, new Vector3(5.461f, -0.066f, 12.427f), new Vector3(0f, 41.838f, 0f));
                SetPositionX(HumXT, new Vector3(5.723613f, 0f, 14.2165f), new Vector3(0f, -180f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-10.487f, -0.386f, 69.915f), new Vector3(0f, 90f, 0f)); ;
            }
            if (numScene == 26)
            {
                SetPositionY(AvaYT_Sit, new Vector3(5.737f, -0.493f, 10.974f), new Vector3(0f, 255f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(4.859f, -0.429f, 9.868f), new Vector3(0f, 25f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-7.272f, -0.36f, 68.364f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 27)
            {
                SetPositionY(AvaYT, new Vector3(12.39f, 0f, 14.71f), new Vector3(0f, -120.328f, 0f));
                SetPositionX(HumXT, new Vector3(7.55f, 0f, 12.367f), new Vector3(0f, -300f, 0f));
                SetPositionY(HumYT, new Vector3(-8.6f, 0f, 70.75f), new Vector3(0f, 130.86f, 0f));
            }
            if (numScene == 28)
            {
                SetPositionY(AvaYT, new Vector3(7.496f, 0f, 11.506f), new Vector3(0f, 270f, 0f));
                SetPositionX(HumXT, new Vector3(6.18f, 0f, 13.23f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT, new Vector3(-10.04f, 0f, 66.95f), new Vector3(0f, -90f, 0f));
            }
            if (numScene == 29)
            {
                SetPositionY(AvaYT_Sit, new Vector3(5.56f, -0.089f, 12.458f), new Vector3(0f, 40f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(6.97f, -0.468f, 14.333f), new Vector3(0f, 135f, 0f));
                SetPositionY(HumYT, new Vector3(-9.07f, 0f, 68.44f), new Vector3(0f, 36.801f, 0f));
            }
            ////////////
            if (numScene == 30)
            {
                SetPositionY(AvaYT, new Vector3(6.151f, 0f, 13.258f), new Vector3(0f, 0f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(8.008f, -0.393f, 14.389f), new Vector3(0f, 225f, 0f));
                SetPositionY(HumYT, new Vector3(-11.91f, 0f, 66.74f), new Vector3(0f, -119.119f, 0f));
            }
            if (numScene == 31)
            {
                SetPositionY(AvaYT_Sit, new Vector3(6.992f, -0.396f, 13.305f), new Vector3(0f, 40f, 0f));
                SetPositionX(HumXT, new Vector3(5.59f, 0f, 14.04f), new Vector3(0f, -61.287f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-9.087f, -0.385f, 69.31f), new Vector3(0f, -90f, 0f));
            }
            if (numScene == 32)
            {
                SetPositionY(AvaYT, new Vector3(7.85f, 0f, 11.877f), new Vector3(0f, 45f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(10.288f, -0.328f, 14.138f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT, new Vector3(-8.56f, 0f, 68.08f), new Vector3(0f, -31.82f, 0f));
            }
            if (numScene == 33)
            {
                SetPositionY(AvaYT_Sit, new Vector3(9.357f, -0.47f, 12.371f), new Vector3(0f, 130f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(11.77f, -0.328f, 13.15f), new Vector3(0f, 220f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-7.61f, -0.294f, 70.55f), new Vector3(0f, 135.212f, 0f));
            }
            if (numScene == 34)
            {
                SetPositionY(HumYT_Sit, new Vector3(-7.616721f, -0.328f, 70.58225f), new Vector3(0f, 129.886f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(10.271f, -0.328f, 14.127f), new Vector3(0f, 180f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(6.953f, -0.328f, 14.282f), new Vector3(0f, 130.94f, 0f));
            }
            if (numScene == 35)
            {
                SetPositionY(AvaYT_Sit, new Vector3(11.892f, -0.489f, 13.261f), new Vector3(0f, 270f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(9.249f, -0.499f, 12.405f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(-6.37f, -0.262f, 71.79f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 36)
            {
                SetPositionY(AvaYT, new Vector3(11.8741f, 0f, 14.98594f), new Vector3(0f, -27.694f, 0f));
                SetPositionX(HumXT, new Vector3(12.52f, 0f, 15.529f), new Vector3(0f, 300f, 0f));
                SetPositionY(HumYT, new Vector3(-9.20975f, 0f, 68.25565f), new Vector3(0f, -18.182f, 0f));
            }
        }


        if (numPair == 11)
        {
            DeactAvatars();
            if (numScene == 1)
            {
                SetPositionX(HumXT_Sit, new Vector3(10.983f, -0.294f, 13.992f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 2)
            {
                SetPositionX(HumXT_Sit, new Vector3(12.003f, -0.494f, 12.4694f), new Vector3(0f, 270f, 0f));
            }
            if (numScene == 3)
            {
                SetPositionX(HumXT, new Vector3(12.255f, 0f, 14.499f), new Vector3(0f, -30f, 0f));
            }
            if (numScene == 4)
            {
                SetPositionX(HumXT, new Vector3(12.32447f, 0f, 11.91559f), new Vector3(0f, -134.321f, 0f));
            }
            if (numScene == 5)
            {
                SetPositionX(HumXT_Sit, new Vector3(8.006f, -0.393f, 14.39f), new Vector3(0f, 224.648f, 0f));
            }
            if (numScene == 6)
            {
                SetPositionX(HumXT, new Vector3(8.491033f, 0f, 13.62631f), new Vector3(0f, 90f, 0f));
            }
            if (numScene == 7)
            {
                SetPositionX(HumXT_Sit, new Vector3(5.501f, -0.029f, 12.383f), new Vector3(0f, 400f, 0f));
            }
            if (numScene == 8)
            {
                SetPositionX(HumXT, new Vector3(6.833f, 0f, 12.51f), new Vector3(0f, 45f, 0f));
            }
            if (numScene == 9)
            {
                SetPositionX(HumXT_Sit, new Vector3(5.792f, -0.389f, 11.005f), new Vector3(0f, 252.672f, 0f));
            }
            if (numScene == 10)
            {
                SetPositionX(HumXT, new Vector3(4.432f, 0f, 9.037f), new Vector3(0f, 45f, 0f));
            }
            if (numScene == 11)
            {
                SetPositionX(HumXT_Sit, new Vector3(9.3f, -0.489f, 13.131f), new Vector3(0f, -270f, 0f));
            }
            if (numScene == 12)
            {
                SetPositionX(HumXT, new Vector3(4.779f, 0f, 9.128f), new Vector3(0f, -90f, 0f));

            }
            ////////////
            if (numScene == 13)
            {
                SetPositionX(HumXT_Sit, new Vector3(10.983f, -0.294f, 13.992f), new Vector3(0f, 180f, 0f));
                SetPositionY(AvaYT, new Vector3(11.08073f, 0f, 12.13628f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT, new Vector3(10.819f, 0f, 68.643f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 14)
            {
                SetPositionX(HumXT_Sit, new Vector3(12.003f, -0.494f, 12.4694f), new Vector3(0f, 270f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(9.178f, -0.429f, 13.096f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(12.069f, -0.335f, 70.249f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 15)
            {
                SetPositionX(HumXT, new Vector3(12.255f, 0f, 14.499f), new Vector3(0f, -30f, 0f));
                SetPositionY(AvaYT, new Vector3(11.06736f, 0f, 13.18914f), new Vector3(0f, 44.821f, 0f));
                SetPositionY(HumYT, new Vector3(6.93f, 0f, 70.07f), new Vector3(0f, -133.672f, 0f));
            }
            if (numScene == 16)
            {
                SetPositionX(HumXT, new Vector3(12.32447f, 0f, 11.91559f), new Vector3(0f, -134.321f, 0f));
                SetPositionY(AvaYT, new Vector3(10.78202f, 0f, 11.69818f), new Vector3(0f, 91.441f, 0f));
                SetPositionY(HumYT, new Vector3(13.137f, 0f, 69.559f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 17)
            {
                SetPositionX(HumXT_Sit, new Vector3(8.006f, -0.393f, 14.39f), new Vector3(0f, 224.648f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(6.97f, -0.468f, 14.333f), new Vector3(0f, 135f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(6.904f, -0.146f, 69.216f), new Vector3(0f, 180f, 0f));
            }
            ////////////
            if (numScene == 18)
            {
                SetPositionX(HumXT, new Vector3(8.491033f, 0f, 13.62631f), new Vector3(0f, 90f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(8.008f, -0.393f, 14.389f), new Vector3(0f, 225f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(6.893f, -0.129f, 69.26985f), new Vector3(0f, -180f, 0f));
            }
            if (numScene == 19)
            {
                SetPositionX(HumXT_Sit, new Vector3(5.501f, -0.029f, 12.383f), new Vector3(0f, 400f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(12.066f, -0.5f, 13.17f), new Vector3(0f, -90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(10.91f, -0.269f, 67.122f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 20)
            {
                SetPositionX(HumXT, new Vector3(6.833f, 0f, 12.51f), new Vector3(0f, 45f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(11.988f, -0.532f, 13.198f), new Vector3(0f, -90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(10.991f, -0.316f, 70.17f), new Vector3(0f, -180f, 0f));
            }
            if (numScene == 21)
            {
                SetPositionX(HumXT_Sit, new Vector3(5.792f, -0.389f, 11.005f), new Vector3(0f, 252.672f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(4.32f, -0.613f, 11f), new Vector3(0f, -597.744f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(7.975f, -0.152f, 67.70651f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 22)
            {
                SetPositionX(HumXT, new Vector3(4.432f, 0f, 9.037f), new Vector3(0f, 45f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(5.753975f, -0.428f, 11.08115f), new Vector3(0f, -506.038f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(7.934f, -0.145f, 69.167f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 23)
            {
                SetPositionX(HumXT_Sit, new Vector3(9.3f, -0.489f, 13.131f), new Vector3(0f, -270f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(9.249f, -0.499f, 12.405f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(6.903f, -0.142f, 69.172f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 24)
            {
                SetPositionX(HumXT, new Vector3(4.779f, 0f, 9.128f), new Vector3(0f, -90f, 0f));
                SetPositionY(AvaYT, new Vector3(5.367085f, 0f, 11.4929f), new Vector3(0f, -154.897f, 0f));
                SetPositionY(HumYT, new Vector3(8.89f, 0f, 69.3f), new Vector3(0f, -123.726f, 0f));
            }

            ////////////
            if (numScene == 25)
            {
                SetPositionY(AvaYT_Sit, new Vector3(5.461f, -0.066f, 12.427f), new Vector3(0f, 41.838f, 0f));
                SetPositionX(HumXT, new Vector3(5.723613f, 0f, 14.2165f), new Vector3(0f, -180f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(11.15f, -0.305f, 70.12f), new Vector3(0f, 193.593f, 0f));
            }
            if (numScene == 26)
            {
                SetPositionY(AvaYT_Sit, new Vector3(5.737f, -0.493f, 10.974f), new Vector3(0f, 255f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(4.859f, -0.429f, 9.868f), new Vector3(0f, 25f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(11.558f, -0.245f, 67.185f), new Vector3(0f, 34.48f, 0f));
            }
            if (numScene == 27)
            {
                SetPositionY(AvaYT, new Vector3(12.39f, 0f, 14.71f), new Vector3(0f, -120.328f, 0f));
                SetPositionX(HumXT, new Vector3(7.55f, 0f, 12.367f), new Vector3(0f, -300f, 0f));
                SetPositionY(HumYT, new Vector3(9.33f, 0f, 70.29f), new Vector3(0f, 130.86f, 0f));
            }
            if (numScene == 28)
            {
                SetPositionY(AvaYT, new Vector3(7.496f, 0f, 11.506f), new Vector3(0f, 270f, 0f));
                SetPositionX(HumXT, new Vector3(6.18f, 0f, 13.23f), new Vector3(0f, 0f, 0f));
                SetPositionY(HumYT, new Vector3(8.92f, 0f, 69.57f), new Vector3(0f, -90f, 0f));
            }
            if (numScene == 29)
            {
                SetPositionY(AvaYT_Sit, new Vector3(5.56f, -0.089f, 12.458f), new Vector3(0f, 40f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(6.97f, -0.468f, 14.333f), new Vector3(0f, 135f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(7.928719f, -0.118f, 67.80653f), new Vector3(0f, 51.596f, 0f));
            }
            ////////////
            if (numScene == 30)
            {
                SetPositionY(AvaYT, new Vector3(6.151f, 0f, 13.258f), new Vector3(0f, 0f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(8.008f, -0.393f, 14.389f), new Vector3(0f, 225f, 0f));
                SetPositionY(HumYT, new Vector3(6.42f, 0f, 69.586f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 31)
            {
                SetPositionY(AvaYT_Sit, new Vector3(6.992f, -0.396f, 13.305f), new Vector3(0f, 40f, 0f));
                SetPositionX(HumXT, new Vector3(5.59f, 0f, 14.04f), new Vector3(0f, -61.287f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(6.935f, -0.14f, 67.717f), new Vector3(0f, 0f, 0f));
            }
            if (numScene == 32)
            {
                SetPositionY(AvaYT, new Vector3(7.85f, 0f, 11.877f), new Vector3(0f, 45f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(10.288f, -0.328f, 14.138f), new Vector3(0f, 180f, 0f));
                SetPositionY(HumYT, new Vector3(9.951f, 0f, 68.203f), new Vector3(0f, 42.199f, 0f));
            }
            if (numScene == 33)
            {
                SetPositionY(AvaYT_Sit, new Vector3(9.357f, -0.47f, 12.371f), new Vector3(0f, 130f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(11.77f, -0.328f, 13.15f), new Vector3(0f, 220f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(12.853f, -0.294f, 67.345f), new Vector3(0f, 41.639f, 0f));
            }
            if (numScene == 34)
            {
                SetPositionY(HumYT_Sit, new Vector3(11.69f, -0.328f, 70.028f), new Vector3(0f, 129.886f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(10.271f, -0.328f, 14.127f), new Vector3(0f, 180f, 0f));
                SetPositionY(AvaYT_Sit, new Vector3(6.953f, -0.328f, 14.282f), new Vector3(0f, 130.94f, 0f));
            }
            if (numScene == 35)
            {
                SetPositionY(AvaYT_Sit, new Vector3(11.892f, -0.489f, 13.261f), new Vector3(0f, 270f, 0f));
                SetPositionX(HumXT_Sit, new Vector3(9.249f, -0.499f, 12.405f), new Vector3(0f, 90f, 0f));
                SetPositionY(HumYT_Sit, new Vector3(11.13f, -0.262f, 70.11f), new Vector3(0f, 180f, 0f));
            }
            if (numScene == 36)
            {
                SetPositionY(AvaYT, new Vector3(11.8741f, 0f, 14.98594f), new Vector3(0f, -27.694f, 0f));
                SetPositionX(HumXT, new Vector3(12.52f, 0f, 15.529f), new Vector3(0f, 300f, 0f));
                SetPositionY(HumYT, new Vector3(5.874f, 0f, 68.821f), new Vector3(0f, -59.685f, 0f));
            }
        }

        // Show previously selected position of Ava
        if (nextOrBack==1)
        {

            if (numScene>0)
            { 
                if (testData[(numScene - 1) * 5] == 0)
                {

                    //Debug.Log("0");
                    AvaXT.SetActive(true);
                    Vector3 avaXPos = new Vector3(testData[(numScene - 1) * 5 + 1], testData[(numScene - 1) * 5 + 2], testData[(numScene - 1) * 5 + 3]);
                    Vector3 avaXeY = new Vector3(0f, testData[(numScene - 1) * 5 + 4], 0f);
                    AvaXT.transform.position = avaXPos;
                    AvaXT.transform.eulerAngles = avaXeY;
                }
                else if (testData[(numScene - 1) * 5] == 1)
                {
                    //Debug.Log("1");
                    AvaXT_Sit.SetActive(true);
                    Vector3 avaXPos = new Vector3(testData[(numScene - 1) * 5 + 1], testData[(numScene - 1) * 5 + 2], testData[(numScene - 1) * 5 + 3]);
                    Vector3 avaXeY = new Vector3(0f, testData[(numScene - 1) * 5 + 4], 0f);
                    AvaXT_Sit.transform.position = avaXPos;
                    AvaXT_Sit.transform.eulerAngles = avaXeY;
                }
            }

        }
    }
    void TestScene_48(int nextOrBack)
    {
        // Next: Test data to array and write to txt
        if (nextOrBack == 0)
        {
            if (numScene > 36 && numScene < 49)
            {
                if (AvaXT.activeSelf == true)
                {
                    testAva = AvaXT;
                    tSitOrStand = 0;
                }
                if (AvaXT_Sit.activeSelf == true)
                {
                    testAva = AvaXT_Sit;
                    tSitOrStand = 1;
                }
                if (testAva == null)
                {
                    Debug.Log("Position not selected");
                }
                else
                {
                    testData[(numScene - 1 - 36) * 5] = tSitOrStand;
                    testData[(numScene - 1 - 36) * 5 + 1] = testAva.transform.position.x;
                    testData[(numScene - 1 - 36) * 5 + 2] = testAva.transform.position.y;
                    testData[(numScene - 1 - 36) * 5 + 3] = testAva.transform.position.z;
                    testData[(numScene - 1 - 36) * 5 + 4] = testAva.transform.eulerAngles.y;
                }

                /*
                Debug.Log("Scene " + numScene);
                for (int i = 0; i < 5; i++)
                {
                    Debug.Log(testData[(numScene - 1) * 5 + i]);
                }
                */
            }
            if (numScene == 48 && Userdata == false) //WriteToTxt
            {
                next.SetActive(false);
                /*
                for (int i = 0; i < 180; i++)
                {
                    Debug.Log(i+"] "+testData[i]);
                }
                */
                WriteToTxt();
                numScene = -1;
            }
            numScene++;

        }
        else//Back
        {
            if (numScene == 49)
            {
                //GameObject next = GameObject.Find("Next_button");
                next.SetActive(true);
            }
            numScene--;

        }
        DeactAvatars();
        // Scenes...
       
        
        
        // Show previously selected position of Ava
        if (nextOrBack == 1)
        {

            if (numScene > 0)
            {
                if (testData[(numScene - 1 - 36) * 5] == 0)
                {

                    //Debug.Log("0");
                    AvaXT.SetActive(true);
                    Vector3 avaXPos = new Vector3(testData[(numScene - 1 - 36) * 5 + 1], testData[(numScene - 1 - 36) * 5 + 2], testData[(numScene - 1 - 36) * 5 + 3]);
                    Vector3 avaXeY = new Vector3(0f, testData[(numScene - 1 - 36) * 5 + 4], 0f);
                    AvaXT.transform.position = avaXPos;
                    AvaXT.transform.eulerAngles = avaXeY;
                }
                else if (testData[(numScene - 1 - 36) * 5] == 1)
                {
                    //Debug.Log("1");
                    AvaXT_Sit.SetActive(true);
                    Vector3 avaXPos = new Vector3(testData[(numScene - 1 - 36) * 5 + 1], testData[(numScene - 1 - 36) * 5 + 2], testData[(numScene - 1 - 36) * 5 + 3]);
                    Vector3 avaXeY = new Vector3(0f, testData[(numScene - 1 - 36) * 5 + 4], 0f);
                    AvaXT_Sit.transform.position = avaXPos;
                    AvaXT_Sit.transform.eulerAngles = avaXeY;
                }
            }

        }
    }
    public void SetPositionX(GameObject Character, Vector3 posXYZ, Vector3 eulerY) {
        Character.SetActive(true);
        Character.transform.position = posXYZ;
        Character.transform.eulerAngles = eulerY;
        if (Character.name =="HumXT")
        {
            HumXC.SetActive(true);
            Vector3 XCpos = new Vector3(posXYZ.x- distTtoC1, posXYZ.y, posXYZ.z);
            HumXC.transform.position = XCpos;
            HumXC.transform.eulerAngles = eulerY;
        }
        else if (Character.name == "HumXT_Sit")
        {
            HumXC_Sit.SetActive(true);
            Vector3 XCpos = new Vector3(posXYZ.x - distTtoC1, posXYZ.y, posXYZ.z);
            HumXC_Sit.transform.position = XCpos;
            HumXC_Sit.transform.eulerAngles = eulerY;
        }
    }
    public void SetPositionY(GameObject Character, Vector3 posXYZ, Vector3 eulerY)
    {
        Character.SetActive(true);
        Character.transform.position = posXYZ;
        Character.transform.eulerAngles = eulerY;
        if (Character.name == "HumYT")
        {
            HumYC.SetActive(true);
            Vector3 YCpos = new Vector3(posXYZ.x + distTtoC2, posXYZ.y, posXYZ.z);
            HumYC.transform.position = YCpos;
            HumYC.transform.eulerAngles = eulerY;
        }
        else if (Character.name == "HumYT_Sit")
        {
            HumYC_Sit.SetActive(true);
            Vector3 YCpos = new Vector3(posXYZ.x + distTtoC2, posXYZ.y, posXYZ.z);
            HumYC_Sit.transform.position = YCpos;
            HumYC_Sit.transform.eulerAngles = eulerY;
        }
        else if (Character.name == "AvaYT")
        {
            AvaYC.SetActive(true);
            Vector3 YCpos = new Vector3(posXYZ.x - distTtoC1, posXYZ.y, posXYZ.z);
            AvaYC.transform.position = YCpos;
            AvaYC.transform.eulerAngles = eulerY;
        }
        else if (Character.name == "AvaYT_Sit")
        {
            AvaYC_Sit.SetActive(true);
            Vector3 YCpos = new Vector3(posXYZ.x - distTtoC1, posXYZ.y, posXYZ.z);
            AvaYC_Sit.transform.position = YCpos;
            AvaYC_Sit.transform.eulerAngles = eulerY;
        }
    }
    public void WriteToTxt()
    {
        //string fileName = Application.dataPath + "\\testdata_48\\pair" + numPair.ToString() + User_IF.text + "_48.txt";
        //string fileName = "c:\\EXP\\pair" + numPair.ToString() + User_IF.text + ".txt";
        string fileName = Application.dataPath + "\\testdata_48\\pair" + numPair.ToString() + "user" + numUser.ToString() + "_48.txt";
        //string fileName = "d:\\testdata\\pair" + numPair.ToString() + "user" + numUser.ToString() + ".txt";
        fileStr = new FileStream(@fileName, FileMode.Create, FileAccess.Write);
        //fileStr = new FileStream(@"d:\livingroom.txt", FileMode.Create, FileAccess.Write);
        sw = new StreamWriter(fileStr);
        int numData = 35;
        for (int i = 0; i <= numData; i++)
        {

            sw.Write(testData[0 + i * 5] + " ");
            sw.Write(testData[1 + i * 5] + " ");
            sw.Write(testData[2 + i * 5] + " ");
            sw.Write(testData[3 + i * 5] + " ");

            if (i == numData)
            {
                sw.Write(testData[4 + i * 5]);
            }
            else
            {
                sw.Write(testData[4 + i * 5] + " ");
            }

        }
        sw.Close();
        Debug.Log("Text file created");
    }
    public void CreateToTxt()
    {

        //fileName = Application.dataPath + "\\Metric\\ranksvm\\TwoAngles9D\\ranksvm_TwoAng_" + numPair.ToString() + ".txt";
        string fileName = Application.dataPath + "\\scenedata_48\\pair" + numPair.ToString() + "scene_48" + ".txt";
        if (edit == true)
        {
            fileName = Application.dataPath + "\\scenedata_48\\pair" + numPair.ToString() + "scene_48_E" + ".txt";
        }
        fileStr = new FileStream(@fileName, FileMode.Create, FileAccess.Write);
        sw = new StreamWriter(fileStr);
        int numData = 12;
        for (int i = 0; i < numData; i++)
        {

            sw.Write(createData_48[0 + i * 15] + " ");
            sw.Write(createData_48[1 + i * 15] + " ");
            sw.Write(createData_48[2 + i * 15] + " ");
            sw.Write(createData_48[3 + i * 15] + " ");
            sw.Write(createData_48[4 + i * 15] + " ");
            sw.Write(createData_48[5 + i * 15] + " ");
            sw.Write(createData_48[6 + i * 15] + " ");
            sw.Write(createData_48[7 + i * 15] + " ");
            sw.Write(createData_48[8 + i * 15] + " ");
            sw.Write(createData_48[9 + i * 15] + " ");
            sw.Write(createData_48[10 + i * 15] + " ");
            sw.Write(createData_48[11 + i * 15] + " ");
            sw.Write(createData_48[12 + i * 15] + " ");
            sw.Write(createData_48[13 + i * 15] + " ");

            if (i == numData-1)
            {
                sw.Write(createData_48[14 + i * 15]);
            }
            else
            {
                sw.Write(createData_48[14 + i * 15] + " ");
            }

        }
        sw.Close();
        Debug.Log("Scene file created");
    }
    float ContAngle(Vector3 fwd, Vector3 targetDir)
    {
        float angle = Vector3.Angle(fwd, targetDir);

        if (AngleDir(fwd, targetDir, Vector3.up) == -1)
        {
            angle = 360.0f - angle;
            if (angle > 359.9999f)
                angle -= 360.0f;
            return angle;
        }
        else
            return angle;
    }
    int AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0.0)
            return 1;
        else if (dir < 0.0)
            return -1;
        else
            return 0;
    }
    public void PairChange()
    {
        numPair = pairDD.value;
        //Debug.Log(numPair);
        //FindModels();
        numScene = 12;
        if(adminmode)
        {
            save.SetActive(true);
        }
        

        S1.SetActive(false);
        S2.SetActive(false);
        S3.SetActive(false);
        S4.SetActive(false);
        S5.SetActive(false);
        S6.SetActive(false);
        S7.SetActive(false);
        S8.SetActive(false);
        S1Top.SetActive(false);
        S2Top.SetActive(false);
        S3Top.SetActive(false);
        S4Top.SetActive(false);
        S5Top.SetActive(false);
        S6Top.SetActive(false);
        S7Top.SetActive(false);
        S8Top.SetActive(false);

        distTtoC1 = 15f; distTtoC2 = 15f;
        if (numPair == 2 || numPair == 5 || numPair == 7 || numPair == 9 || numPair == 10 || numPair == 13 || numPair == 16 || numPair == 18 || numPair == 20 || numPair == 22) { distTtoC2 = -15f; }
        if (numPair == 7 || numPair == 8 || numPair == 9 || numPair == 10 || numPair == 11 || numPair == 16 || numPair == 17 || numPair == 18 || numPair == 22 || numPair == 23) { distTtoC1 = -15f; }
        next.SetActive(true);

        //Only look at the hum-ava pair data
        //if (numScene != 13)
        //numScene = 0;

        //FindCamera();
        GetCamera(numPair);
    }
    public void OnPointerClick(PointerEventData evd)
    {
        pClicked = false;
    }
    void SceneGenerateUI()
    {
        RaycastHit hit;
        chr.SetActive(true);
        //HumanX!!!!!start
        if (int.Parse(CHR_IF.text) == 1)
        {
            if (Input.GetButton("Fire1") && Input.mousePosition.x < (Screen.width / 10) * 5F && Input.mousePosition.y < (Screen.height / 10) * 4.85F && Input.mousePosition.y > (Screen.height / 10) * 0.25F)
            {
                //HumXT
                {
                    Debug.Log(int.Parse(CHR_IF.text));
                    Ray ray = Top1Cam.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        if (hit.collider != null)
                        {
                            Debug.Log(hit.collider.tag);
                            if (string.Equals(hit.collider.tag, "Floor"))
                            {
                                HumXT_Sit.SetActive(false);
                                HumXC_Sit.SetActive(false);
                                HumXT.SetActive(true);
                                HumXC.SetActive(true);
                                HumXT.transform.position = hit.point;
                                Vector3 tPos = new Vector3(hit.point.x - distTtoC1, hit.point.y, hit.point.z);
                                HumXC.transform.position = tPos;
                                standOrSit = 0;
                            }
                            else if (string.Equals(hit.collider.tag, "Chair"))
                            {
                                HumXT.SetActive(false);
                                HumXC.SetActive(false);
                                HumXT_Sit.SetActive(true);
                                HumXC_Sit.SetActive(true);
                                Vector3 tPos = new Vector3(hit.point.x, hit.point.y - 0.85f, hit.point.z);
                                HumXT_Sit.transform.position = tPos;
                                tPos = new Vector3(hit.point.x - distTtoC1, hit.point.y - 0.85f, hit.point.z);
                                HumXC_Sit.transform.position = tPos;
                                standOrSit = 1;
                            }
                        }
                    }
                }
            }
            if (Input.GetButton("Fire2") && Input.mousePosition.x < (Screen.width / 10) * 5F && Input.mousePosition.y < (Screen.height / 10) * 4.85F && Input.mousePosition.y > (Screen.height / 10) * 0.25F)
            {
                Ray ray = Top1Cam.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider != null)
                    {
                        if (HumXT.activeSelf == true)
                        {
                            Vector3 fwd = new Vector3(0f, 0f, 1f);
                            Vector3 tar = new Vector3(hit.point.x - HumXT.transform.position.x, 0f, HumXT.transform.position.z - hit.point.z);
                            float angleY = ContAngle(fwd, tar);
                            Vector3 slideY = new Vector3(0f, -angleY + 180f, 0f);
                            HumXT.transform.eulerAngles = slideY;
                            HumXC.transform.eulerAngles = slideY;
                        }
                        if (HumXT_Sit.activeSelf == true)
                        {
                            Vector3 fwd = new Vector3(0f, 0f, 1f);
                            Vector3 tar = new Vector3(hit.point.x - HumXT_Sit.transform.position.x, 0f, HumXT_Sit.transform.position.z - hit.point.z);
                            float angleY = ContAngle(fwd, tar);
                            Vector3 slideY = new Vector3(0f, -angleY + 180f, 0f);
                            HumXT_Sit.transform.eulerAngles = slideY;
                            HumXC_Sit.transform.eulerAngles = slideY;
                        }
                    }
                }
            }
        }
        //HumanX!!!!!end
        //AvatarY!!!!!start
        if (int.Parse(CHR_IF.text) == 2)
        {
            if (Input.GetButton("Fire1") && Input.mousePosition.x < (Screen.width / 10) * 5F && Input.mousePosition.y < (Screen.height / 10) * 4.85F && Input.mousePosition.y > (Screen.height / 10) * 0.25F)
            {
                //AvaYT
                {
                    Debug.Log(int.Parse(CHR_IF.text));
                    Ray ray = Top1Cam.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        if (hit.collider != null)
                        {
                            Debug.Log(hit.collider.tag);
                            if (string.Equals(hit.collider.tag, "Floor"))
                            {
                                AvaYT_Sit.SetActive(false);
                                AvaYC_Sit.SetActive(false);
                                AvaYT.SetActive(true);
                                AvaYC.SetActive(true);
                                AvaYT.transform.position = hit.point;
                                Vector3 tPos = new Vector3(hit.point.x - distTtoC1, hit.point.y, hit.point.z);
                                AvaYC.transform.position = tPos;
                                standOrSit = 0;
                            }
                            else if (string.Equals(hit.collider.tag, "Chair"))
                            {
                                AvaYT.SetActive(false);
                                AvaYC.SetActive(false);
                                AvaYT_Sit.SetActive(true);
                                AvaYC_Sit.SetActive(true);
                                Vector3 tPos = new Vector3(hit.point.x, hit.point.y - 0.85f, hit.point.z);
                                AvaYT_Sit.transform.position = tPos;
                                tPos = new Vector3(hit.point.x - distTtoC1, hit.point.y - 0.85f, hit.point.z);
                                AvaYC_Sit.transform.position = tPos;
                                standOrSit = 1;
                            }
                        }
                    }
                }
            }
            if (Input.GetButton("Fire2") && Input.mousePosition.x < (Screen.width / 10) * 5F && Input.mousePosition.y < (Screen.height / 10) * 4.85F && Input.mousePosition.y > (Screen.height / 10) * 0.25F)
            {
                Ray ray = Top1Cam.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider != null)
                    {
                        if (AvaYT.activeSelf == true)
                        {
                            Vector3 fwd = new Vector3(0f, 0f, 1f);
                            Vector3 tar = new Vector3(hit.point.x - AvaYT.transform.position.x, 0f, AvaYT.transform.position.z - hit.point.z);
                            float angleY = ContAngle(fwd, tar);
                            Vector3 slideY = new Vector3(0f, -angleY + 180f, 0f);
                            AvaYT.transform.eulerAngles = slideY;
                            AvaYC.transform.eulerAngles = slideY;
                        }
                        if (AvaYT_Sit.activeSelf == true)
                        {
                            Vector3 fwd = new Vector3(0f, 0f, 1f);
                            Vector3 tar = new Vector3(hit.point.x - AvaYT_Sit.transform.position.x, 0f, AvaYT_Sit.transform.position.z - hit.point.z);
                            float angleY = ContAngle(fwd, tar);
                            Vector3 slideY = new Vector3(0f, -angleY + 180f, 0f);
                            AvaYT_Sit.transform.eulerAngles = slideY;
                            AvaYC_Sit.transform.eulerAngles = slideY;
                        }
                    }
                }
            }
        }
        //AvatarY!!!!!end

        //HumanY!!!start
        if (int.Parse(CHR_IF.text) == 3)
        {
            if (Input.GetButton("Fire1") && Input.mousePosition.x > (Screen.width / 10) * 5F && Input.mousePosition.y < (Screen.height / 10) * 4.85F && Input.mousePosition.y > (Screen.height / 10) * 0.25F)
            {
                Ray ray = Top2Cam.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider != null)
                    {
                        Debug.Log(hit.collider.tag);
                        if (string.Equals(hit.collider.tag, "Floor"))
                        {
                            HumYT_Sit.SetActive(false);
                            HumYC_Sit.SetActive(false);
                            HumYT.SetActive(true);
                            HumYC.SetActive(true);
                            HumYT.transform.position = hit.point;
                            Vector3 tPos = new Vector3(hit.point.x + distTtoC2, hit.point.y, hit.point.z);
                            HumYC.transform.position = tPos;
                            standOrSit = 0;
                        }
                        else if (string.Equals(hit.collider.tag, "Chair"))
                        {
                            HumYT.SetActive(false);
                            HumYC.SetActive(false);
                            HumYT_Sit.SetActive(true);
                            HumYC_Sit.SetActive(true);
                            Vector3 tPos = new Vector3(hit.point.x, hit.point.y - 0.85f, hit.point.z);
                            HumYT_Sit.transform.position = tPos;
                            tPos = new Vector3(hit.point.x + distTtoC2, hit.point.y - 0.85f, hit.point.z);
                            HumYC_Sit.transform.position = tPos;
                            standOrSit = 1;
                        }
                    }
                }
            }
            //float angleXwrtY =ContAngle (HumY.transform.position - AvaX.transform.position, AvaX.transform.forward);
            if (Input.GetButton("Fire2") && Input.mousePosition.x > (Screen.width / 10) * 5F && Input.mousePosition.y < (Screen.height / 10) * 4.85F && Input.mousePosition.y > (Screen.height / 10) * 0.25F)
            {
                Ray ray = Top2Cam.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider != null)
                    {

                        if (HumYT.activeSelf == true)
                        {
                            Vector3 fwd = new Vector3(0f, 0f, 1f);
                            Vector3 tar = new Vector3(hit.point.x - HumYT.transform.position.x, 0f, HumYT.transform.position.z - hit.point.z);
                            float angleY = ContAngle(fwd, tar);
                            Vector3 slideY = new Vector3(0f, -angleY + 180f, 0f);
                            HumYT.transform.eulerAngles = slideY;
                            HumYC.transform.eulerAngles = slideY;
                        }
                        if (HumYT_Sit.activeSelf == true)
                        {
                            Vector3 fwd = new Vector3(0f, 0f, 1f);
                            Vector3 tar = new Vector3(hit.point.x - HumYT_Sit.transform.position.x, 0f, HumYT_Sit.transform.position.z - hit.point.z);
                            float angleY = ContAngle(fwd, tar);
                            Vector3 slideY = new Vector3(0f, -angleY + 180f, 0f);
                            HumYT_Sit.transform.eulerAngles = slideY;
                            HumYC_Sit.transform.eulerAngles = slideY;
                        }

                    }
                }
            }
        }
        //HumanY!!!end
    }
    void HeadOrientationLine()
    {
        if (HumXT != null && HumXT.activeSelf)
        {
            l1.SetActive(true);
            line1.SetPosition(0, HumXT.transform.position + new Vector3(0f, 1.5f, 0f));
            arrowX = Mathf.Cos((90f - HumXT.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + HumXT.transform.position.x;
            arrowZ = Mathf.Sin((90f - HumXT.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + HumXT.transform.position.z;
            line1.SetPosition(1, new Vector3(arrowX, 1.5f, arrowZ));
        }
        if (HumXT_Sit != null && HumXT_Sit.activeSelf)
        {
            l1.SetActive(true);
            line1.SetPosition(0, HumXT_Sit.transform.position + new Vector3(0f, 2f, 0f));
            arrowX = Mathf.Cos((90f - HumXT_Sit.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + HumXT_Sit.transform.position.x;
            arrowZ = Mathf.Sin((90f - HumXT_Sit.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + HumXT_Sit.transform.position.z;
            line1.SetPosition(1, new Vector3(arrowX, 2f, arrowZ));
        }

        if (AvaYT != null && AvaYT.activeSelf)
        {
            l2.SetActive(true);
            line2.SetPosition(0, AvaYT.transform.position + new Vector3(0f, 1.5f, 0f));
            arrowX = Mathf.Cos((90f - AvaYT.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + AvaYT.transform.position.x;
            arrowZ = Mathf.Sin((90f - AvaYT.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + AvaYT.transform.position.z;
            line2.SetPosition(1, new Vector3(arrowX, 1.5f, arrowZ));
        }
        if (AvaYT_Sit != null && AvaYT_Sit.activeSelf)
        {
            l2.SetActive(true);
            line2.SetPosition(0, AvaYT_Sit.transform.position + new Vector3(0f, 2f, 0f));
            arrowX = Mathf.Cos((90f - AvaYT_Sit.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + AvaYT_Sit.transform.position.x;
            arrowZ = Mathf.Sin((90f - AvaYT_Sit.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + AvaYT_Sit.transform.position.z;
            line2.SetPosition(1, new Vector3(arrowX, 2f, arrowZ));
        }

        if (AvaXT != null && AvaXT.activeSelf)
        {
            l3.SetActive(true);
            line3.SetPosition(0, AvaXT.transform.position + new Vector3(0f, 1.5f, 0f));
            arrowX = Mathf.Cos((90f - AvaXT.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + AvaXT.transform.position.x;
            arrowZ = Mathf.Sin((90f - AvaXT.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + AvaXT.transform.position.z;
            line3.SetPosition(1, new Vector3(arrowX, 1.5f, arrowZ));
        }
        if (AvaXT_Sit != null && AvaXT_Sit.activeSelf)
        {
            l3.SetActive(true);
            line3.SetPosition(0, AvaXT_Sit.transform.position + new Vector3(0f, 2f, 0f));
            arrowX = Mathf.Cos((90f - AvaXT_Sit.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + AvaXT_Sit.transform.position.x;
            arrowZ = Mathf.Sin((90f - AvaXT_Sit.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + AvaXT_Sit.transform.position.z;
            line3.SetPosition(1, new Vector3(arrowX, 2f, arrowZ));
        }

        if (HumYT != null && HumYT.activeSelf)
        {
            l4.SetActive(true);
            line4.SetPosition(0, HumYT.transform.position + new Vector3(0f, 1.5f, 0f));
            arrowX = Mathf.Cos((90f - HumYT.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + HumYT.transform.position.x;
            arrowZ = Mathf.Sin((90f - HumYT.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + HumYT.transform.position.z;
            line4.SetPosition(1, new Vector3(arrowX, 1.5f, arrowZ));
        }
        if (HumYT_Sit != null && HumYT_Sit.activeSelf)
        {
            l4.SetActive(true);
            line4.SetPosition(0, HumYT_Sit.transform.position + new Vector3(0f, 2f, 0f));
            arrowX = Mathf.Cos((90f - HumYT_Sit.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + HumYT_Sit.transform.position.x;
            arrowZ = Mathf.Sin((90f - HumYT_Sit.transform.eulerAngles.y) / 360f * (2f * Mathf.PI)) + HumYT_Sit.transform.position.z;
            line4.SetPosition(1, new Vector3(arrowX, 2f, arrowZ));
        }
        /*
        if (numScene < 13 || numScene > 36)
        {
            l2.SetActive(false);
            l4.SetActive(false);
        }
        if (numScene == 0)
        {
            l1.SetActive(false);
        }
        if (AvaXT.activeSelf == false && AvaXT_Sit.activeSelf == false)
        {
            l3.SetActive(false);
        }
        */
    }
    void AvaPlace()
    {
        RaycastHit hit;
        if (Input.GetButton("Fire1") && Input.mousePosition.x > (Screen.width / 10) * 5F && Input.mousePosition.y < (Screen.height / 10) * 4.85F && Input.mousePosition.y > (Screen.height / 10) * 0.25F)
        {

            if (Top2Cam != null)
            {
                Ray ray = Top2Cam.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider != null)
                    {
                        //Debug.Log(hit.collider.tag);
                        //Debug.Log(hit.collider.name);
                        if (string.Equals(hit.collider.tag, "Floor"))
                        {
                            AvaXT_Sit.SetActive(false);
                            AvaXC_Sit.SetActive(false);
                            AvaXT.SetActive(true);
                            AvaXC.SetActive(true);
                            AvaXT.transform.position = hit.point;
                            Vector3 tPos = new Vector3(hit.point.x + distTtoC2, hit.point.y, hit.point.z);
                            AvaXC.transform.position = tPos;
                            standOrSit = 0;
                        }
                        else if (string.Equals(hit.collider.tag, "Chair"))
                        {
                            AvaXT.SetActive(false);
                            AvaXC.SetActive(false);
                            AvaXT_Sit.SetActive(true);
                            AvaXC_Sit.SetActive(true);
                            Vector3 tPos = new Vector3(hit.point.x, hit.point.y - 0.85f, hit.point.z);
                            AvaXT_Sit.transform.position = tPos;
                            tPos = new Vector3(hit.point.x + distTtoC2, hit.point.y - 0.85f, hit.point.z);
                            AvaXC_Sit.transform.position = tPos;
                            standOrSit = 1;
                        }
                    }
                }
            }
        }
        //float angleXwrtY =ContAngle (HumY.transform.position - AvaX.transform.position, AvaX.transform.forward);
        if (Input.GetButton("Fire2") && Input.mousePosition.x > (Screen.width / 10) * 5F && Input.mousePosition.y < (Screen.height / 10) * 4.85F && Input.mousePosition.y > (Screen.height / 10) * 0.25F)
        {
            Ray ray = Top2Cam.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider != null)
                {

                    if (AvaXT.activeSelf == true)
                    {
                        Vector3 fwd = new Vector3(0f, 0f, 1f);
                        Vector3 tar = new Vector3(hit.point.x - AvaXT.transform.position.x, 0f, AvaXT.transform.position.z - hit.point.z);
                        float angleY = ContAngle(fwd, tar);
                        Vector3 slideY = new Vector3(0f, -angleY + 180f, 0f);
                        AvaXT.transform.eulerAngles = slideY;
                        AvaXC.transform.eulerAngles = slideY;
                    }
                    if (AvaXT_Sit.activeSelf == true)
                    {
                        Vector3 fwd = new Vector3(0f, 0f, 1f);
                        Vector3 tar = new Vector3(hit.point.x - AvaXT_Sit.transform.position.x, 0f, AvaXT_Sit.transform.position.z - hit.point.z);
                        float angleY = ContAngle(fwd, tar);
                        Vector3 slideY = new Vector3(0f, -angleY + 180f, 0f);
                        AvaXT_Sit.transform.eulerAngles = slideY;
                        AvaXC_Sit.transform.eulerAngles = slideY;
                    }

                }
            }
        }
    }
    void ColorInit()
    {
        hColor1 = new Color(255f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
        hColor2 = new Color(255f / 255f, 128f / 255f, 0f / 255f, 255f / 255f);
        hColor3 = new Color(255f / 255f, 255f / 255f, 0f / 255f, 255f / 255f);
        hColor4 = new Color(128f / 255f, 255f / 255f, 0f / 255f, 255f / 255f);
        hColor5 = new Color(0f / 255f, 255f / 255f, 0f / 255f, 255f / 255f);
        hColor6 = new Color(0f / 255f, 255f / 255f, 128f / 255f, 255f / 255f);
        hColor7 = new Color(0f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
        hColor8 = new Color(0f / 255f, 128f / 255f, 255f / 255f, 255f / 255f);
        hColor9 = new Color(0f / 255f, 0f / 255f, 255f / 255f, 255f / 255f);
        hColor10 = new Color(127f / 255f, 0f / 255f, 255f / 255f, 255f / 255f);
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
    void RecallAvaX_U()
    {
        if (AvaXT_1.activeSelf == true)
        {
            AvaX_U_1 = AvaXT_1;
        }
        else if (AvaXT_Sit_1.activeSelf == true)
        {
            AvaX_U_1 = AvaXT_Sit_1;
        }

        if (AvaXT_2.activeSelf == true)
        {
            AvaX_U_2 = AvaXT_2;
        }
        else if (AvaXT_Sit_2.activeSelf == true)
        {
            AvaX_U_2 = AvaXT_Sit_2;
        }

        if (AvaXT_3.activeSelf == true)
        {
            AvaX_U_3 = AvaXT_3;
        }
        else if (AvaXT_Sit_3.activeSelf == true)
        {
            AvaX_U_3 = AvaXT_Sit_3;
        }

        if (AvaXT_4.activeSelf == true)
        {
            AvaX_U_4 = AvaXT_4;
        }
        else if (AvaXT_Sit_4.activeSelf == true)
        {
            AvaX_U_4 = AvaXT_Sit_4;
        }

        if (AvaXT_5.activeSelf == true)
        {
            AvaX_U_5 = AvaXT_5;
        }
        else if (AvaXT_Sit_5.activeSelf == true)
        {
            AvaX_U_5 = AvaXT_Sit_5;
        }

        if (AvaXT_6.activeSelf == true)
        {
            AvaX_U_6 = AvaXT_6;
        }
        else if (AvaXT_Sit_6.activeSelf == true)
        {
            AvaX_U_6 = AvaXT_Sit_6;
        }

        if (AvaXT_7.activeSelf == true)
        {
            AvaX_U_7 = AvaXT_7;
        }
        else if (AvaXT_Sit_7.activeSelf == true)
        {
            AvaX_U_7 = AvaXT_Sit_7;
        }

        if (AvaXT_8.activeSelf == true)
        {
            AvaX_U_8 = AvaXT_8;
        }
        else if (AvaXT_Sit_8.activeSelf == true)
        {
            AvaX_U_8 = AvaXT_Sit_8;
        }

        if (AvaXT_9.activeSelf == true)
        {
            AvaX_U_9 = AvaXT_9;
        }
        else if (AvaXT_Sit_9.activeSelf == true)
        {
            AvaX_U_9 = AvaXT_Sit_9;
        }

        if (AvaXT_10.activeSelf == true)
        {
            AvaX_U_10 = AvaXT_10;
        }
        else if (AvaXT_Sit_10.activeSelf == true)
        {
            AvaX_U_10 = AvaXT_Sit_10;
        }
    }
    void RecallHumY()
    {
        if (HumYT.activeSelf == true)
        {
            HumY = HumYT;
        }
        else if (HumYT_Sit.activeSelf == true)
        {
            HumY = HumYT_Sit;
        }
    }
    void RecallAvaY()
    {
        if (AvaYT.activeSelf == true)
        {
            AvaY = AvaYT;
        }
        else if (AvaYT_Sit.activeSelf == true)
        {
            AvaY = AvaYT_Sit;
        }
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
    void FeatureVisualization(GameObject FromX, GameObject ToY)
    {
        RaycastHit hit;
        angAva = FromX.transform.eulerAngles.y;
        angHum = ToY.transform.eulerAngles.y;

        /*//Free space
        DebugExtension.DebugCircle(FromX.transform.position, Color.yellow, 0.5f);
        DebugExtension.DebugCircle(FromX.transform.position, Color.yellow, 1f);
        DebugExtension.DebugCircle(FromX.transform.position, Color.yellow, 1.5f);
        DebugExtension.DebugCircle(FromX.transform.position, Color.yellow, 2f);
        */

        //Object Category
        if (SP)
        {
            DebugExtension.DebugCircle(FromX.transform.position, Color.black, 3f);
        }

        //Visual attention two lines
        
        ray_start.Set(FromX.transform.position.x, 0f, FromX.transform.position.z);
        ray_iso.Set(Mathf.Cos(Mathf.PI / 180f * ((70f - angAva + 90f - 90f) % 360)), 0f, Mathf.Sin(Mathf.PI / 180f * ((70f - angAva + 90f - 90f) % 360)));
        if(VA)
        {
            Debug.DrawLine(ray_start, ray_start + 4f * ray_iso, Color.black);
        }
        

        ray_start.Set(FromX.transform.position.x, 0f, FromX.transform.position.z);
        ray_iso.Set(Mathf.Cos(Mathf.PI / 180f * ((110f - angAva + 90f - 90f) % 360)), 0f, Mathf.Sin(Mathf.PI / 180f * ((110f - angAva + 90f - 90f) % 360)));
        if (VA)
        {
            Debug.DrawLine(ray_start, ray_start + 4f * ray_iso, Color.black);
        }

        /*//Free space drawline
        for (int k = 1; k < 13; k++)
        {
            ray_start.Set(FromX.transform.position.x, 0f, FromX.transform.position.z);
            ray_iso.Set(Mathf.Cos(Mathf.PI / 180f * ((30F * (k - 1) - angAva + 90f - 90f) % 360)), 0f, Mathf.Sin(Mathf.PI / 180f * ((30F * (k - 1) - angAva + 90f - 90f) % 360)));

            if (k == 1)
            {
                Debug.DrawLine(ray_start, ray_start + 2f * ray_iso, Color.red);
            }
            else
            {
                Debug.DrawLine(ray_start, ray_start + 2f * ray_iso, Color.yellow);
            }
        }
        */

        //1. FromX orientation + relative angle with respect to ToY

        //Line from waist of Avatar X to the waist of Human Y
        if(IP)
        {
            Debug.DrawLine(FromX.transform.position + ray_waist, ToY.transform.position + ray_waist, Color.black);
        }


        //Line from waist of Avatar X to the forward direction of Avatar X
        //Debug.DrawLine(FromX.transform.position + ray_waist, FromX.transform.position + 5f * FromX.transform.forward + ray_waist, Color.red);


        //Now Pose affordance
        //2. FromX space feature (currently visualizing 5 circles divided by 24 sectors only, no height field)
        int numSector = 17;
        if(PA)
        {
            DebugExtension.DebugCircle(FromX.transform.position, Color.black, 0.25f);
            DebugExtension.DebugCircle(FromX.transform.position, Color.black, 0.5f);
        }
        
        for (int k = 1; k < numSector; k++)  //numSec
        {
            int AffLev = L_space+1;
            int SFV_L = 1;
            // Drawing the line of 24 sectors
            ray_start.Set(FromX.transform.position.x, 0f, FromX.transform.position.z);
            ray_iso.Set(Mathf.Cos(Mathf.PI / 180f * ((22.5F * (k - 1) - angAva+90f) % 360)), 0f, Mathf.Sin(Mathf.PI / 180f * ((22.5F * (k - 1) - angAva+90f) % 360)));
            if(PA)
            {
                Debug.DrawLine(ray_start + SFV_L * 0.25f * ray_iso, ray_start + SFV_L * 0.5f * ray_iso, Color.black);
            }
            
            /*
            if (k == 1)
            {
                Debug.DrawLine(ray_start + SFV_L * 0.25f * ray_iso, ray_start + SFV_L * 0.5f * ray_iso, Color.red);
            }
            else if (k == 5)
            {
                Debug.DrawLine(ray_start + SFV_L * 0.25f * ray_iso, ray_start + SFV_L * 0.5f * ray_iso, Color.yellow);
            }
            else if (k == 9)
            {
                Debug.DrawLine(ray_start + SFV_L * 0.25f * ray_iso, ray_start + SFV_L * 0.5f * ray_iso, Color.green);
            }
            else if (k == 13)
            {
                Debug.DrawLine(ray_start + SFV_L * 0.25f * ray_iso, ray_start + SFV_L * 0.5f * ray_iso, Color.blue);
            }
            else
            {
                Debug.DrawLine(ray_start + SFV_L * 0.25f * ray_iso, ray_start + SFV_L * 0.5f * ray_iso, Color.white);
            }
            */
            // Drawing the different size circles
            for (int l = 1; l < AffLev; l++)
            {
                //DebugExtension.DebugCircle(FromX.transform.position, Color.magenta, l * 0.25f);
                
                if (l == 1)
                {
                    //DebugExtension.DebugCircle(FromX.transform.position, Color.yellow, l * 0.25f);
                }
                
                else if(l< SFV_L + 1)
                {
                    //DebugExtension.DebugCircle(FromX.transform.position, Color.yellow, l * 0.5f);
                }
                totHeight = 0.0f;
                
                if (l < SFV_L+1)
                { 
                    for (int m = 0; m < 100; m++) //numSampling
                    {
                        randsqrt = Mathf.Sqrt(Random.Range(0.0f, 1.0f));
                        ray_start.Set(FromX.transform.position.x + 0.50f * (l - 1f + randsqrt) * Mathf.Cos(Mathf.PI / 180f * Random.Range((15.0F * (k - 1) - angAva + 90f) % 360f, (15.0F * k - angAva + 90f) % 360f)), 2.25f, FromX.transform.position.z + 0.50f * (l - 1f + randsqrt) * Mathf.Sin(Mathf.PI / 180f * Random.Range((15.0F * (k - 1) - angAva + 90f) % 360f, (15.0F * k - angAva + 90f) % 360f)));
                        if (Physics.Raycast(ray_start, ray_dir_down, out hit, 2.0f))
                        {

                            //Debug.DrawLine(ray_start, hit.point, Color.red);
                            /*
                            if (hit.point.y <= 0.5f/3f)
                            {
                                Debug.DrawLine(ray_start, hit.point, hColor1);
                            }
                            if (hit.point.y > 0.5f / 3f && hit.point.y <= 1.0f / 3f)
                            {
                                Debug.DrawLine(ray_start, hit.point, hColor2);
                            }
                            if (hit.point.y > 1.0f / 3f && hit.point.y <= 1.5f / 3f)
                            {
                                Debug.DrawLine(ray_start, hit.point, hColor3);
                            }
                            if (hit.point.y > 1.5f / 3f && hit.point.y <= 2.0f / 3f)
                            {
                                Debug.DrawLine(ray_start, hit.point, hColor4);
                            }
                            if (hit.point.y > 2.0f / 3f && hit.point.y <= 2.5f / 3f)
                            {
                                Debug.DrawLine(ray_start, hit.point, hColor5);
                            }
                            if (hit.point.y > 2.5f / 3f && hit.point.y <= 3.0f / 3f)
                            {
                                Debug.DrawLine(ray_start, hit.point, hColor6);
                            }
                            */
                        }
                    }
                }
                
                
            }
        }

        /*
        //3. FromX ISO feature
        for (int k = 1; k < 14; k++)
        {

            ray_start.Set(FromX.transform.position.x, 0.5f, FromX.transform.position.z);
            ray_iso.Set(Mathf.Cos(Mathf.PI / 180f * ((15.0F * (k - 1) - angAva) % 360)), 0, Mathf.Sin(Mathf.PI / 180f * ((15.0F * (k - 1) - angAva) % 360)));
            if (Physics.Raycast(ray_start, ray_iso, out hit, level * 0.5f))
            {
                Debug.DrawLine(ray_start, hit.point, Color.cyan);
            }
            else
            {
                ray_iso.Set(level * 0.5f * Mathf.Cos(Mathf.PI / 180f * ((15.0F * (k - 1) - angAva) % 360)), 0, level * 0.5f * Mathf.Sin(Mathf.PI / 180f * ((15.0F * (k - 1) - angAva) % 360)));
                Debug.DrawLine(ray_start, ray_start + ray_iso, Color.blue);
            }
        }
        */


    }
    
    float[] Feat_fromX_44(GameObject FromX, GameObject ToY, int HumOrAva)
    {


        fromXfeat = FeatureValues(FromX, ToY, fromXfeat);

        //Pose Affordance
        //Feat41[0]-Feat41[16]
        feat_fromX[0] = fromXfeat[0] / 1000f;
        for (int i = 0; i < 16; i++)
        {
            if (i == 0)
            {
                feat_fromX[i + 1] = (0.25f * fromXfeat[16] + 0.5f * fromXfeat[i + 1] + 0.25f * fromXfeat[i + 2]) / 1000f;
            }

            feat_fromX[i + 1] = (0.25f * fromXfeat[i] + 0.5f * fromXfeat[i + 1] + 0.25f * fromXfeat[i + 2]) / 1000f;

            if (i == 15)

            {
                feat_fromX[i + 1] = (0.25f * fromXfeat[i] + 0.5f * fromXfeat[i + 1] + 0.25f * fromXfeat[1]) / 1000f;
            }
        }

        //Distance from X to Y
        //Feat41[17]
        feat_fromX[17] = fromXfeat[17] / 10f;

        //Angle between X front and X to Y
        //Feat41[18]
        feat_fromX[18] = Mathf.Min(fromXfeat[18], (360f - fromXfeat[18])) / 180f;
        //feat_fromX[19] = Mathf.Min(fromXfeat[19], (360f - fromXfeat[19])) / 180f;
        //Object category frequency nexr X and X'
        //Feat41[19]-Feat41[30]
        float[] object_category_frequency = new float[12];
        if (HumOrAva == 0)
        {
            object_category_frequency = fur_script.Object_category_frequency_HumX(FromX);
        }
        else
        {
            object_category_frequency = fur_script.Object_category_frequency_AvaX(FromX);
        }

        for (int i = 0; i < 12; i++)
        {
            feat_fromX[i + 19] = object_category_frequency[i] / 10f;
        }

        //Visual attention of X as category frequency
        //Feat41[31]-Feat41[42]
        float[] visual_attention_category_frequency = new float[12];
        if (HumOrAva == 0)
        {
            visual_attention_category_frequency = fur_script.VisualAttention_Hum(FromX);

        }
        else
        {
            visual_attention_category_frequency = fur_script.VisualAttention_Ava(FromX);
        }

        for (int i = 0; i < 12; i++)
        {
            feat_fromX[i + 31] = visual_attention_category_frequency[i];
        }


        //SitOrStand
        //Feat41[43]
        feat_fromX[43] = SitOrStandofX(FromX);


        //skipfornow
        //Dist_feat[5] = fur_script.Free_space_diff(HumX, AvaX);


        return feat_fromX;
    }
    float[] Dist_Feat_3d(GameObject HumX, GameObject AvaY, GameObject AvaX, GameObject HumY)
    {

        //float[] humXfeat = new float[19];
        //float[] avaXfeat = new float[19];
        float[] Dist_feat = new float[3];

        humXfeat = FeatureValues(HumX, AvaY, humXfeat);
        avaXfeat = FeatureValues(AvaX, HumY, avaXfeat);

        for (int i = 0; i < 17; i++)
        {
            if (i == 0)
            {
                Dist_feat[0] = 10f * Mathf.Abs(humXfeat[0] - avaXfeat[0]);
            }
            else
            {
                Dist_feat[0] = Dist_feat[0] + Mathf.Abs(humXfeat[i] - avaXfeat[i]);
            }
        }

        Dist_feat[0] = Dist_feat[0] / 20000f;
        Dist_feat[1] = Mathf.Abs(humXfeat[17] - avaXfeat[17]) / 10f;
        Dist_feat[2] = Mathf.Min(Mathf.Min(Mathf.Abs(humXfeat[18] - avaXfeat[18]), (360f - Mathf.Abs(humXfeat[18] - avaXfeat[18]))), Mathf.Min(Mathf.Abs(humXfeat[18] + avaXfeat[18]), Mathf.Abs((360f - humXfeat[18] + avaXfeat[18])))) / 180f;
        //fur_script.Space_similarity(HumX, AvaX);
        //Dist_feat[3] = 1f - fur_script.global_cat_sim;
        //Dist_feat[4] = 1f - fur_script.global_occup_sim;
        //Dist_feat[3] = 0f;
        //Dist_feat[4] = 0f;
        return Dist_feat;
    }
    float[] Dist_Feat_4d(GameObject HumX, GameObject AvaY, GameObject AvaX, GameObject HumY, float[] humXfeat)
    {

        //Debug.Log("HumX : " + HumX);
        //Debug.Log("AvaY : " + AvaY);
        //Debug.Log("AvaX : " + AvaX);
        //Debug.Log("HumY : " + HumY);

        //float[] humXfeat = new float[20];
        

        //Debug.Log("Hum_X" + humXfeat);
        //Debug.Log("Hum_X" + " : " + " [0] " + humXfeat[0] + " [1] " + humXfeat[1] + " [2] " + humXfeat[2] + " [3] " + humXfeat[3] + " [4] " + humXfeat[4] + " [5] " + humXfeat[5] + " [6] " + humXfeat[6] + " [7] " + humXfeat[7] + " [8] " + humXfeat[8] + " [17] " + humXfeat[17] + " [18] " + humXfeat[18] + " [19] " + humXfeat[19]);


        //float[] avaXfeat = new float[20];
        avaXfeat = FeatureValues(AvaX, HumY, avaXfeat);

        //Debug.Log("Ava_X" + avaXfeat);
        //Debug.Log("Ava_X" + " : " + " [0] " + avaXfeat[0] + " [1] " + avaXfeat[1] + " [2] " + avaXfeat[2] + " [3] " + avaXfeat[3] + " [4] " + avaXfeat[4] + " [5] " + avaXfeat[5] + " [6] " + avaXfeat[6] + " [7] " + avaXfeat[7] + " [8] " + avaXfeat[8] + " [17] " + avaXfeat[17] + " [18] " + avaXfeat[18] + " [19] " + avaXfeat[19]);

        //Debug.Log("humXfeat[17] : " + humXfeat[17]);
        //Debug.Log("avaXfeat[17] : " + avaXfeat[17]);

        for (int i = 0; i < 16; i++)
        {
            if (i == 0)
            {
                humXaff[i] = 0.25f * humXfeat[16] + 0.5f * humXfeat[i + 1] + 0.25f * humXfeat[i + 2];
                avaXaff[i] = 0.25f * avaXfeat[16] + 0.5f * avaXfeat[i + 1] + 0.25f * avaXfeat[i + 2];
            }

            humXaff[i] = 0.25f * humXfeat[i] + 0.5f * humXfeat[i + 1] + 0.25f * humXfeat[i + 2];
            avaXaff[i] = 0.25f * avaXfeat[i] + 0.5f * avaXfeat[i + 1] + 0.25f * avaXfeat[i + 2];

            if (i == 15)
            {
                humXaff[i] = 0.25f * humXfeat[i] + 0.5f * humXfeat[i + 1] + 0.25f * humXfeat[1];
                avaXaff[i] = 0.25f * avaXfeat[i] + 0.5f * avaXfeat[i + 1] + 0.25f * avaXfeat[1];
            }
        }

        for (int i = 0; i < 17; i++)
        {
            if (i == 0)
            {
                Dist_feat4d[0] = 10f * Mathf.Abs(humXfeat[0] - avaXfeat[0]);
            }
            else
            {
                Dist_feat4d[0] = Dist_feat[0] + Mathf.Abs(humXaff[i - 1] - avaXaff[i - 1]);
            }
        }

        Dist_feat4d[0] = Dist_feat4d[0] / 20000f;
        //Debug.Log("Dist_feat[0] : " + Dist_feat[0]);

        Dist_feat4d[1] = Mathf.Abs(humXfeat[17] - avaXfeat[17]) / 10f;
        //Debug.Log("humXfeat[17] : " + humXfeat[17]);
        //Debug.Log("avaXfeat[17] : " + avaXfeat[17]);
        //Debug.Log("Dist_feat[1] : " + Dist_feat[1]);

        //Dist_feat[2]
        if (Mathf.Abs(humXfeat[18] - avaXfeat[18]) > 180f)
        {
            Dist_feat4d[2] = (360f - Mathf.Abs(humXfeat[18] - avaXfeat[18])) / 180f;
        }
        else
        {
            Dist_feat4d[2] = Mathf.Abs(humXfeat[18] - avaXfeat[18]) / 180f;
        }
        //Debug.Log("Dist_feat[2] : " + Dist_feat[2]);

        //Dist_feat[3]
        if (Mathf.Abs(humXfeat[19] - avaXfeat[19]) > 180f)
        {
            Dist_feat4d[3] = (360f - Mathf.Abs(humXfeat[19] - avaXfeat[19])) / 180f;
        }
        else
        {
            Dist_feat4d[3] = Mathf.Abs(humXfeat[19] - avaXfeat[19]) / 180f;
        }
        //Debug.Log("Dist_feat[3] : " + Dist_feat[3]);
        /*
        fur_script.Space_similarity(HumX, AvaX);
        Dist_feat[4] = 1f - fur_script.global_cat_sim;
        //Dist_feat[4] = 1f - fur_script.global_occup_sim;

        //Dist_feat[5] Visual attention similarity
        Dist_feat[5] = fur_script.VA_diff(HumX, AvaX);

        //Dist_feat[6] Free space map
        Dist_feat[6] = fur_script.Free_space_diff(HumX, AvaX);
        */

        return Dist_feat4d;
    }
    float[] Dist_Feat_5d(GameObject HumX, GameObject AvaY, GameObject AvaX, GameObject HumY)
    {

        float[] humXfeat = new float[19];
        float[] avaXfeat = new float[19];
        float[] Dist_feat = new float[5];

        humXfeat = FeatureValues(HumX, AvaY, humXfeat);
        avaXfeat = FeatureValues(AvaX, HumY, avaXfeat);

        for (int i = 0; i < 17; i++)
        {
            if (i == 0)
            {
                Dist_feat[0] = 10f * Mathf.Abs(humXfeat[0] - avaXfeat[0]);
            }
            else
            {
                Dist_feat[0] = Dist_feat[0] + Mathf.Abs(humXfeat[i] - avaXfeat[i]);
            }
        }

        Dist_feat[0] = Dist_feat[0] / 20000f;
        Dist_feat[1] = Mathf.Abs(humXfeat[17] - avaXfeat[17]) / 10f;
        Dist_feat[2] = Mathf.Abs(humXfeat[18] - avaXfeat[18]) / 180f;
        fur_script.Space_similarity(HumX, AvaX);
        Dist_feat[3] = 1f - fur_script.global_cat_sim;
        Dist_feat[4] = 1f - fur_script.global_occup_sim;
        return Dist_feat;
    }
    float[] Dist_Feat_6d(GameObject HumX, GameObject AvaY, GameObject AvaX, GameObject HumY)
    {

        //Debug.Log("HumX : " + HumX);
        //Debug.Log("AvaY : " + AvaY);
        //Debug.Log("AvaX : " + AvaX);
        //Debug.Log("HumY : " + HumY);

        //float[] humXfeat = new float[20];
        humXfeat = FeatureValues(HumX, AvaY, humXfeat);

        //Debug.Log("Hum_X" + humXfeat);
        //Debug.Log("Hum_X" + " : " + " [0] " + humXfeat[0] + " [1] " + humXfeat[1] + " [2] " + humXfeat[2] + " [3] " + humXfeat[3] + " [4] " + humXfeat[4] + " [5] " + humXfeat[5] + " [6] " + humXfeat[6] + " [7] " + humXfeat[7] + " [8] " + humXfeat[8] + " [17] " + humXfeat[17] + " [18] " + humXfeat[18] + " [19] " + humXfeat[19]);


        //float[] avaXfeat = new float[20];
        avaXfeat = FeatureValues(AvaX, HumY, avaXfeat);

        //Debug.Log("Ava_X" + avaXfeat);
        //Debug.Log("Ava_X" + " : " + " [0] " + avaXfeat[0] + " [1] " + avaXfeat[1] + " [2] " + avaXfeat[2] + " [3] " + avaXfeat[3] + " [4] " + avaXfeat[4] + " [5] " + avaXfeat[5] + " [6] " + avaXfeat[6] + " [7] " + avaXfeat[7] + " [8] " + avaXfeat[8] + " [17] " + avaXfeat[17] + " [18] " + avaXfeat[18] + " [19] " + avaXfeat[19]);

        //Debug.Log("humXfeat[17] : " + humXfeat[17]);
        //Debug.Log("avaXfeat[17] : " + avaXfeat[17]);

        for (int i = 0; i < 16; i++)
        {
            if (i == 0)
            {
                humXaff[i] = 0.25f * humXfeat[16] + 0.5f * humXfeat[i + 1] + 0.25f * humXfeat[i + 2];
                avaXaff[i] = 0.25f * avaXfeat[16] + 0.5f * avaXfeat[i + 1] + 0.25f * avaXfeat[i + 2];
            }

            humXaff[i] = 0.25f * humXfeat[i] + 0.5f * humXfeat[i + 1] + 0.25f * humXfeat[i + 2];
            avaXaff[i] = 0.25f * avaXfeat[i] + 0.5f * avaXfeat[i + 1] + 0.25f * avaXfeat[i + 2];

            if (i == 15)
            {
                humXaff[i] = 0.25f * humXfeat[i] + 0.5f * humXfeat[i + 1] + 0.25f * humXfeat[1];
                avaXaff[i] = 0.25f * avaXfeat[i] + 0.5f * avaXfeat[i + 1] + 0.25f * avaXfeat[1];
            }
        }

        for (int i = 0; i < 17; i++)
        {
            if (i == 0)
            {
                Dist_feat[0] = 10f * Mathf.Abs(humXfeat[0] - avaXfeat[0]);
            }
            else
            {
                Dist_feat[0] = Dist_feat[0] + Mathf.Abs(humXaff[i - 1] - avaXaff[i - 1]);
            }
        }

        Dist_feat[0] = Dist_feat[0] / 20000f;
        //Debug.Log("Dist_feat[0] : " + Dist_feat[0]);

        Dist_feat[1] = Mathf.Abs(humXfeat[17] - avaXfeat[17]) / 10f;
        //Debug.Log("humXfeat[17] : " + humXfeat[17]);
        //Debug.Log("avaXfeat[17] : " + avaXfeat[17]);
        //Debug.Log("Dist_feat[1] : " + Dist_feat[1]);

        Dist_feat[2] = Mathf.Min(Mathf.Min(Mathf.Abs(humXfeat[18] - avaXfeat[18]), (360f - Mathf.Abs(humXfeat[18] - avaXfeat[18]))), Mathf.Min(Mathf.Abs(humXfeat[18] + avaXfeat[18]), Mathf.Abs((360f - humXfeat[18] + avaXfeat[18]))))/180f;
        /*
        //Dist_feat[2]
        if (Mathf.Abs(humXfeat[18] - avaXfeat[18]) > 180f)
        {
            Dist_feat[2] = (360f - Mathf.Abs(humXfeat[18] - avaXfeat[18])) / 180f;
        }
        else
        {
            Dist_feat[2] = Mathf.Abs(humXfeat[18] - avaXfeat[18]) / 180f;
        }
        //Debug.Log("Dist_feat[2] : " + Dist_feat[2]);

        //Dist_feat[3]
        if (Mathf.Abs(humXfeat[19] - avaXfeat[19]) > 180f)
        {
            Dist_feat[3] = (360f - Mathf.Abs(humXfeat[19] - avaXfeat[19])) / 180f;
        }
        else
        {
            Dist_feat[3] = Mathf.Abs(humXfeat[19] - avaXfeat[19]) / 180f;
        }
        //Debug.Log("Dist_feat[3] : " + Dist_feat[3]);
        */
        fur_script.Space_similarity(HumX, AvaX);
        Dist_feat[3] = 1f - fur_script.global_cat_sim;
        //Dist_feat[4] = 1f - fur_script.global_occup_sim;

        //Dist_feat[5] Visual attention similarity
        Dist_feat[4] = fur_script.VA_diff(HumX, AvaX);

        //Dist_feat[6] Free space map
        Dist_feat[5] = fur_script.Free_space_diff(HumX, AvaX);
        /*
        Dist_feat[7] = Mathf.Abs(humXfeat[18] - avaXfeat[18]) / 180f;

        Dist_feat[8] = Mathf.Abs(humXfeat[19] - avaXfeat[19]) / 180f;
        */
        Dist_feat[6] = SitOrStand(HumX, AvaX);

        return Dist_feat;
    }
    float[] Dist_Feat_AbsOr(GameObject HumX, GameObject AvaY, GameObject AvaX, GameObject HumY)
    {

        //Debug.Log("HumX : " + HumX);
        //Debug.Log("AvaY : " + AvaY);
        //Debug.Log("AvaX : " + AvaX);
        //Debug.Log("HumY : " + HumY);

        //float[] humXfeat = new float[20];
        humXfeat = FeatureValues(HumX, AvaY, humXfeat);

        //Debug.Log("Hum_X" + humXfeat);
        //Debug.Log("Hum_X" + " : " + " [0] " + humXfeat[0] + " [1] " + humXfeat[1] + " [2] " + humXfeat[2] + " [3] " + humXfeat[3] + " [4] " + humXfeat[4] + " [5] " + humXfeat[5] + " [6] " + humXfeat[6] + " [7] " + humXfeat[7] + " [8] " + humXfeat[8] + " [17] " + humXfeat[17] + " [18] " + humXfeat[18] + " [19] " + humXfeat[19]);


        //float[] avaXfeat = new float[20];
        avaXfeat = FeatureValues(AvaX, HumY, avaXfeat);

        //Debug.Log("Ava_X" + avaXfeat);
        //Debug.Log("Ava_X" + " : " + " [0] " + avaXfeat[0] + " [1] " + avaXfeat[1] + " [2] " + avaXfeat[2] + " [3] " + avaXfeat[3] + " [4] " + avaXfeat[4] + " [5] " + avaXfeat[5] + " [6] " + avaXfeat[6] + " [7] " + avaXfeat[7] + " [8] " + avaXfeat[8] + " [17] " + avaXfeat[17] + " [18] " + avaXfeat[18] + " [19] " + avaXfeat[19]);

        //Debug.Log("humXfeat[17] : " + humXfeat[17]);
        //Debug.Log("avaXfeat[17] : " + avaXfeat[17]);

        for (int i = 0; i < 16; i++)
        {
            if (i == 0)
            {
                humXaff[i] = 0.25f * humXfeat[16] + 0.5f * humXfeat[i + 1] + 0.25f * humXfeat[i + 2];
                avaXaff[i] = 0.25f * avaXfeat[16] + 0.5f * avaXfeat[i + 1] + 0.25f * avaXfeat[i + 2];
            }

            humXaff[i] = 0.25f * humXfeat[i] + 0.5f * humXfeat[i + 1] + 0.25f * humXfeat[i + 2];
            avaXaff[i] = 0.25f * avaXfeat[i] + 0.5f * avaXfeat[i + 1] + 0.25f * avaXfeat[i + 2];

            if (i == 15)
            {
                humXaff[i] = 0.25f * humXfeat[i] + 0.5f * humXfeat[i + 1] + 0.25f * humXfeat[1];
                avaXaff[i] = 0.25f * avaXfeat[i] + 0.5f * avaXfeat[i + 1] + 0.25f * avaXfeat[1];
            }
        }

        for (int i = 0; i < 17; i++)
        {
            if (i == 0)
            {
                Dist_feat[0] = 10f * Mathf.Abs(humXfeat[0] - avaXfeat[0]);
            }
            else
            {
                Dist_feat[0] = Dist_feat[0] + Mathf.Abs(humXaff[i - 1] - avaXaff[i - 1]);
            }
        }

        Dist_feat[0] = Dist_feat[0] / 20000f;
        //Debug.Log("Dist_feat[0] : " + Dist_feat[0]);

        Dist_feat[1] = Mathf.Abs(humXfeat[17] - avaXfeat[17]) / 10f;
        //Debug.Log("humXfeat[17] : " + humXfeat[17]);
        //Debug.Log("avaXfeat[17] : " + avaXfeat[17]);
        //Debug.Log("Dist_feat[1] : " + Dist_feat[1]);

        //Dist_feat[2]
        Dist_feat[2] = Mathf.Abs(humXfeat[18] - avaXfeat[18]) / 180f;
        /*
        if (Mathf.Abs(humXfeat[18] - avaXfeat[18]) > 180f)
        {
            Dist_feat[2] = (360f - Mathf.Abs(humXfeat[18] - avaXfeat[18])) / 180f;
        }
        else
        {
            Dist_feat[2] = Mathf.Abs(humXfeat[18] - avaXfeat[18]) / 180f;
        }
        */
        //Debug.Log("Dist_feat[2] : " + Dist_feat[2]);

        //Dist_feat[3]
        Dist_feat[3] = Mathf.Abs(humXfeat[19] - avaXfeat[19]) / 180f;
        /*
        if (Mathf.Abs(humXfeat[19] - avaXfeat[19]) > 180f)
        {
            Dist_feat[3] = (360f - Mathf.Abs(humXfeat[19] - avaXfeat[19])) / 180f;
        }
        else
        {
            Dist_feat[3] = Mathf.Abs(humXfeat[19] - avaXfeat[19]) / 180f;
        }
        */
        //Debug.Log("Dist_feat[3] : " + Dist_feat[3]);

        fur_script.Space_similarity(HumX, AvaX);
        Dist_feat[4] = 1f - fur_script.global_cat_sim;
        //Dist_feat[4] = 1f - fur_script.global_occup_sim;

        //Dist_feat[5] Visual attention similarity
        Dist_feat[5] = fur_script.VA_diff(HumX, AvaX);

        //Dist_feat[6] Free space map
        Dist_feat[6] = fur_script.Free_space_diff(HumX, AvaX);

        return Dist_feat;
    }
    float[] Dist_Feat_AbsOrP(GameObject HumX, GameObject AvaY, GameObject AvaX, GameObject HumY, float[] humXfeat)
    {

        //Debug.Log("HumX : " + HumX);
        //Debug.Log("AvaY : " + AvaY);
        //Debug.Log("AvaX : " + AvaX);
        //Debug.Log("HumY : " + HumY);

        //float[] humXfeat = new float[20];
        //humXfeat = FeatureValues(HumX, AvaY, humXfeat);

        //Debug.Log("Hum_X" + humXfeat);
        //Debug.Log("Hum_X" + " : " + " [0] " + humXfeat[0] + " [1] " + humXfeat[1] + " [2] " + humXfeat[2] + " [3] " + humXfeat[3] + " [4] " + humXfeat[4] + " [5] " + humXfeat[5] + " [6] " + humXfeat[6] + " [7] " + humXfeat[7] + " [8] " + humXfeat[8] + " [17] " + humXfeat[17] + " [18] " + humXfeat[18] + " [19] " + humXfeat[19]);


        //float[] avaXfeat = new float[20];
        avaXfeat = FeatureValues(AvaX, HumY, avaXfeat);

        //Debug.Log("Ava_X" + avaXfeat);
        //Debug.Log("Ava_X" + " : " + " [0] " + avaXfeat[0] + " [1] " + avaXfeat[1] + " [2] " + avaXfeat[2] + " [3] " + avaXfeat[3] + " [4] " + avaXfeat[4] + " [5] " + avaXfeat[5] + " [6] " + avaXfeat[6] + " [7] " + avaXfeat[7] + " [8] " + avaXfeat[8] + " [17] " + avaXfeat[17] + " [18] " + avaXfeat[18] + " [19] " + avaXfeat[19]);

        //Debug.Log("humXfeat[17] : " + humXfeat[17]);
        //Debug.Log("avaXfeat[17] : " + avaXfeat[17]);

        for (int i = 0; i < 16; i++)
        {
            if (i == 0)
            {
                humXaff[i] = 0.25f * humXfeat[16] + 0.5f * humXfeat[i + 1] + 0.25f * humXfeat[i + 2];
                avaXaff[i] = 0.25f * avaXfeat[16] + 0.5f * avaXfeat[i + 1] + 0.25f * avaXfeat[i + 2];
            }

            humXaff[i] = 0.25f * humXfeat[i] + 0.5f * humXfeat[i + 1] + 0.25f * humXfeat[i + 2];
            avaXaff[i] = 0.25f * avaXfeat[i] + 0.5f * avaXfeat[i + 1] + 0.25f * avaXfeat[i + 2];

            if (i == 15)
            {
                humXaff[i] = 0.25f * humXfeat[i] + 0.5f * humXfeat[i + 1] + 0.25f * humXfeat[1];
                avaXaff[i] = 0.25f * avaXfeat[i] + 0.5f * avaXfeat[i + 1] + 0.25f * avaXfeat[1];
            }
        }

        for (int i = 0; i < 17; i++)
        {
            if (i == 0)
            {
                Dist_feat[0] = 10f * Mathf.Abs(humXfeat[0] - avaXfeat[0]);
            }
            else
            {
                Dist_feat[0] = Dist_feat[0] + Mathf.Abs(humXaff[i - 1] - avaXaff[i - 1]);
            }
        }

        Dist_feat[0] = Dist_feat[0] / 20000f;
        //Debug.Log("Dist_feat[0] : " + Dist_feat[0]);

        Dist_feat[1] = Mathf.Abs(humXfeat[17] - avaXfeat[17]) / 10f;
        //Debug.Log("humXfeat[17] : " + humXfeat[17]);
        //Debug.Log("avaXfeat[17] : " + avaXfeat[17]);
        //Debug.Log("Dist_feat[1] : " + Dist_feat[1]);

        //Dist_feat[2]
        Dist_feat[2] = Mathf.Abs(humXfeat[18] - avaXfeat[18]) / 180f;
        /*
        if (Mathf.Abs(humXfeat[18] - avaXfeat[18]) > 180f)
        {
            Dist_feat[2] = (360f - Mathf.Abs(humXfeat[18] - avaXfeat[18])) / 180f;
        }
        else
        {
            Dist_feat[2] = Mathf.Abs(humXfeat[18] - avaXfeat[18]) / 180f;
        }
        */
        //Debug.Log("Dist_feat[2] : " + Dist_feat[2]);

        //Dist_feat[3]
        Dist_feat[3] = Mathf.Abs(humXfeat[19] - avaXfeat[19]) / 180f;
        /*
        if (Mathf.Abs(humXfeat[19] - avaXfeat[19]) > 180f)
        {
            Dist_feat[3] = (360f - Mathf.Abs(humXfeat[19] - avaXfeat[19])) / 180f;
        }
        else
        {
            Dist_feat[3] = Mathf.Abs(humXfeat[19] - avaXfeat[19]) / 180f;
        }
        */
        //Debug.Log("Dist_feat[3] : " + Dist_feat[3]);

        fur_script.Space_similarity(HumX, AvaX);
        Dist_feat[4] = 1f - fur_script.global_cat_sim;
        //Dist_feat[4] = 1f - fur_script.global_occup_sim;

        //Dist_feat[5] Visual attention similarity
        Dist_feat[5] = fur_script.VA_diff(HumX, AvaX);

        //Dist_feat[6] Free space map
        Dist_feat[6] = fur_script.Free_space_diff(HumX, AvaX);

        return Dist_feat;
    }
    float[] Dist_Feat_7d(GameObject HumX, GameObject AvaY, GameObject AvaX, GameObject HumY, float[] humXfeat)
    {

        //Debug.Log("HumX : " + HumX);
        //Debug.Log("AvaY : " + AvaY);
        //Debug.Log("AvaX : " + AvaX);
        //Debug.Log("HumY : " + HumY);

        //float[] humXfeat = new float[20];
        //humXfeat = FeatureValues(HumX, AvaY, humXfeat);

        //Debug.Log("Hum_X" + humXfeat);
        //Debug.Log("Hum_X" + " : " + " [0] " + humXfeat[0] + " [1] " + humXfeat[1] + " [2] " + humXfeat[2] + " [3] " + humXfeat[3] + " [4] " + humXfeat[4] + " [5] " + humXfeat[5] + " [6] " + humXfeat[6] + " [7] " + humXfeat[7] + " [8] " + humXfeat[8] + " [17] " + humXfeat[17] + " [18] " + humXfeat[18] + " [19] " + humXfeat[19]);


        //float[] avaXfeat = new float[20];
        avaXfeat = FeatureValues(AvaX, HumY, avaXfeat);

        //Debug.Log("Ava_X" + avaXfeat);
        //Debug.Log("Ava_X" + " : " + " [0] " + avaXfeat[0] + " [1] " + avaXfeat[1] + " [2] " + avaXfeat[2] + " [3] " + avaXfeat[3] + " [4] " + avaXfeat[4] + " [5] " + avaXfeat[5] + " [6] " + avaXfeat[6] + " [7] " + avaXfeat[7] + " [8] " + avaXfeat[8] + " [17] " + avaXfeat[17] + " [18] " + avaXfeat[18] + " [19] " + avaXfeat[19]);

        //Debug.Log("humXfeat[17] : " + humXfeat[17]);
        //Debug.Log("avaXfeat[17] : " + avaXfeat[17]);

        for (int i = 0; i < 16; i++)
        {
            if (i == 0)
            {
                humXaff[i] = 0.25f * humXfeat[16] + 0.5f * humXfeat[i+1] + 0.25f * humXfeat[i + 2];
                avaXaff[i] = 0.25f * avaXfeat[16] + 0.5f * avaXfeat[i+1] + 0.25f * avaXfeat[i + 2];
            }

            humXaff[i] = 0.25f * humXfeat[i] + 0.5f * humXfeat[i+1] + 0.25f * humXfeat[i + 2];
            avaXaff[i] = 0.25f * avaXfeat[i] + 0.5f * avaXfeat[i+1] + 0.25f * avaXfeat[i + 2];

            if (i == 15)
            {
                humXaff[i] = 0.25f * humXfeat[i] + 0.5f * humXfeat[i+1] + 0.25f * humXfeat[1];
                avaXaff[i] = 0.25f * avaXfeat[i] + 0.5f * avaXfeat[i+1] + 0.25f * avaXfeat[1];
            }
        }

        for (int i = 0; i < 17; i++)
        {
            if (i == 0)
            {
                Dist_feat[0] = 10f * Mathf.Abs(humXfeat[0] - avaXfeat[0]);
            }
            else
            {
                Dist_feat[0] = Dist_feat[0] + Mathf.Abs(humXaff[i-1] - avaXaff[i-1]);
            }
        }

        Dist_feat[0] = Dist_feat[0] / 20000f;
        //Debug.Log("Dist_feat[0] : " + Dist_feat[0]);

        Dist_feat[1] = Mathf.Abs(humXfeat[17] - avaXfeat[17]) / 10f;
        //Debug.Log("humXfeat[17] : " + humXfeat[17]);
        //Debug.Log("avaXfeat[17] : " + avaXfeat[17]);
        //Debug.Log("Dist_feat[1] : " + Dist_feat[1]);

        Dist_feat[2] = Mathf.Min(Mathf.Min(Mathf.Abs(humXfeat[18] - avaXfeat[18]), (360f - Mathf.Abs(humXfeat[18] - avaXfeat[18]))), Mathf.Min(Mathf.Abs(humXfeat[18] + avaXfeat[18]), Mathf.Abs((360f - humXfeat[18] + avaXfeat[18])))) / 180f;
        /*
        //Dist_feat[2]
        if (Mathf.Abs(humXfeat[18] - avaXfeat[18]) > 180f)
        {
            Dist_feat[2] = (360f - Mathf.Abs(humXfeat[18] - avaXfeat[18])) / 180f;
        }
        else
        {
            Dist_feat[2] = Mathf.Abs(humXfeat[18] - avaXfeat[18]) / 180f;
        }
        //Debug.Log("Dist_feat[2] : " + Dist_feat[2]);

        //Dist_feat[3]
        if (Mathf.Abs(humXfeat[19] - avaXfeat[19]) > 180f)
        {
            Dist_feat[3] = (360f - Mathf.Abs(humXfeat[19] - avaXfeat[19])) / 180f;
        }
        else
        {
            Dist_feat[3] = Mathf.Abs(humXfeat[19] - avaXfeat[19]) / 180f;
        }
        //Debug.Log("Dist_feat[3] : " + Dist_feat[3]);
        */
        fur_script.Space_similarity(HumX, AvaX);
        Dist_feat[3] = 1f - fur_script.global_cat_sim;
        //Dist_feat[4] = 1f - fur_script.global_occup_sim;

        //Dist_feat[5] Visual attention similarity
        Dist_feat[4] = fur_script.VA_diff(HumX, AvaX);

        //Dist_feat[6] Free space map
        Dist_feat[5] = fur_script.Free_space_diff(HumX, AvaX);
        /*
        Dist_feat[7] = Mathf.Abs(humXfeat[18] - avaXfeat[18]) / 180f;

        Dist_feat[8] = Mathf.Abs(humXfeat[19] - avaXfeat[19]) / 180f;
        */
        Dist_feat[6] = SitOrStand(HumX, AvaX);

        return Dist_feat;
    }
    
    float SitOrStand(GameObject HumX, GameObject AvaX)
    {
        RaycastHit hit;
        string HumX_pose;
        float Pose_difference = 0f;
        ray_start.Set(HumX.transform.position.x, 2.25f, HumX.transform.position.z);
        if (Physics.Raycast(ray_start, ray_dir_down, out hit, Mathf.Infinity))
        {
            HumX_pose = hit.collider.tag;
            //Debug.Log("HumX_pose : " + HumX_pose);
        }
        else
        {
            HumX_pose = "Untagged";
            //Debug.Log("HumX_pose(not hit) : " + HumX_pose);
        }

        string AvaX_pose;
        ray_start.Set(AvaX.transform.position.x, 2.25f, AvaX.transform.position.z);
        if (Physics.Raycast(ray_start, ray_dir_down, out hit, Mathf.Infinity))
        {
            AvaX_pose = hit.collider.tag;
            //Debug.Log("AvaX_pose : " + AvaX_pose);
        }
        else
        {
            AvaX_pose = "Untagged";
            //Debug.Log("AvaX_pose(not hit) : " + AvaX_pose);
        }

        if ((HumX_pose == "Chair" || HumX_pose == "Floor") && (AvaX_pose == "Chair" || AvaX_pose == "Floor"))
        {
            if (HumX_pose == AvaX_pose)
            {
                Pose_difference = 0f;
            }
            else
            {
                Pose_difference = 1f;
            }
        }
        else
        {
            Pose_difference = 1f;
        }
        return Pose_difference;
        //Debug.Log("Pose_difference : " + Pose_difference);
    }
    float SitOrStandofX(GameObject AvaX)
    {
        float Pose_value = 0f;
        RaycastHit hit;

        string AvaX_pose;
        ray_start.Set(AvaX.transform.position.x, 2.25f, AvaX.transform.position.z);
        if (Physics.Raycast(ray_start, ray_dir_down, out hit, Mathf.Infinity))
        {
            AvaX_pose = hit.collider.tag;
            //Debug.Log("AvaX_pose : " + AvaX_pose);
        }
        else
        {
            AvaX_pose = "Untagged";
            //Debug.Log("AvaX_pose(not hit) : " + AvaX_pose);
        }

        if ( (AvaX_pose == "Chair"))
        {
            Pose_value = 1;
        }
        else
        {
            Pose_value = 0;
        }
        return Pose_value;
    }
    Node[,,] SpaceToMap(int sMap_X_start, int sMap_X_end, int sMap_Z_start, int sMap_Z_end)
    {
        int sMap_width = sMap_X_end - sMap_X_start + 1;
        int sMap_height = sMap_Z_end - sMap_Z_start + 1;
        Node[,,] sMap = new Node[sMap_width, sMap_height, numSec];
        for (int i = 0; i < sMap_height; i++)
        {
            for (int j = 0; j < sMap_width; j++)
            {
                for (int k = 0; k < numSec; k++)
                {
                    sMap[j, i, k] = Map_whole[j + sMap_X_start, i + sMap_Z_start, k];
                }
            }
        }
        return sMap;

    }
    void MapDivision()
    {
        //Map1
        S1_X_start = 3 + 24; S1_X_end = 20 + 24; S1_Z_start = 2; S1_Z_end = 68;
        S1_width = S1_X_end - S1_X_start + 1; S1_height = S1_Z_end - S1_Z_start + 1;
        Map1 = SpaceToMap(S1_X_start, S1_X_end, S1_Z_start, S1_Z_end);

        //Map2
        S2_X_start = 46 + 24; S2_X_end = 85 + 24; S2_Z_start = 8; S2_Z_end = 40;
        S2_width = S2_X_end - S2_X_start + 1; S2_height = S2_Z_end - S2_Z_start + 1;
        Map2 = SpaceToMap(S2_X_start, S2_X_end, S2_Z_start, S2_Z_end);

        //Map3
        S3_X_start = 2; S3_X_end = 44; S3_Z_start = 87; S3_Z_end = 122;
        S3_width = S3_X_end - S3_X_start + 1; S3_height = S3_Z_end - S3_Z_start + 1;
        Map3 = SpaceToMap(S3_X_start, S3_X_end, S3_Z_start, S3_Z_end);

        //Map4
        //S4_X_start = 70; S4_X_end = 108; S4_Z_start = 86; S4_Z_end = 124;
        S4_X_start = 88; S4_X_end = 108; S4_Z_start = 86; S4_Z_end = 124;
        S4_width = S4_X_end - S4_X_start + 1; S4_height = S4_Z_end - S4_Z_start + 1;
        Map4 = SpaceToMap(S4_X_start, S4_X_end, S4_Z_start, S4_Z_end);

        //Map5
        S5_X_start = 26; S5_X_end = 42; S5_Z_start = 144; S5_Z_end = 208;
        S5_width = S5_X_end - S5_X_start + 1; S5_height = S5_Z_end - S5_Z_start + 1;
        Map5 = SpaceToMap(S5_X_start, S5_X_end, S5_Z_start, S5_Z_end);

        //Map6
        S6_X_start = 70; S6_X_end = 111; S6_Z_start = 154; S6_Z_end = 188;
        S6_width = S6_X_end - S6_X_start + 1; S6_height = S6_Z_end - S6_Z_start + 1;
        Map6 = SpaceToMap(S6_X_start, S6_X_end, S6_Z_start, S6_Z_end);

        //Map7
        S7_X_start = 0; S7_X_end = 41; S7_Z_start = 232; S7_Z_end = 265;
        S7_width = S7_X_end - S7_X_start + 1; S7_height = S7_Z_end - S7_Z_start + 1;
        Map7 = SpaceToMap(S7_X_start, S7_X_end, S7_Z_start, S7_Z_end);

        //Map8
        //S8_X_start = 74; S8_X_end = 112; S8_Z_start = 222; S8_Z_end = 259;
        S8_X_start = 74; S8_X_end = 112; S8_Z_start = 240; S8_Z_end = 259;
        S8_width = S8_X_end - S8_X_start + 1; S8_height = S8_Z_end - S8_Z_start + 1;
        Map8 = SpaceToMap(S8_X_start, S8_X_end, S8_Z_start, S8_Z_end);
    }
    public class SimIndex
    {
        public int wIdx, hIdx, sIdx;
        //public float[] feature;
        public float simValue;
        public SimIndex(int wIdx, int hIdx, int sIdx, float simValue)
        {
            this.wIdx = wIdx; this.hIdx = hIdx; this.sIdx=sIdx; this.simValue = simValue;
        }
    }
   

}
    