using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Telepresence : MonoBehaviour {

	//
	public const int DIM_FEATURE = 45;
	public const int FRAME_BUFFER_SIZE = sizeof(float) * DIM_FEATURE * 2;

	//
	public GameObject clientSocketObject = null;
	public ClientSocketHandler clientSocketHandler = null;

	// Use this for initialization
	void Start () {
		//
		clientSocketHandler = clientSocketObject.GetComponent<ClientSocketHandler>();		
	}

	// Update is called once per frame
	void Update () {
		//
		if (!clientSocketHandler) {
			Debug.Log ("There is no clientSocketHandler.");
			return;
		}
        //
        /*float[] sample_input = new float[DIM_FEATURE*2]; 
		for (int i = 0; i < sample_input.Length/2; i++)
		{
			float number = 1.0f;//Random.Range(0.0f, 1.0f);
		    sample_input[i] = number;
		}
		for (int i = sample_input.Length/2; i < sample_input.Length; i++)
		{
			float number = 0.1f;//Random.Range(0.0f, 1.0f);
			sample_input[i] = number;
		}*/
        //

        //float[] sample_input = { 0.3251861f, 0.2550962f, 0.2394775f, 0.1618072f, 0.0660079f, 0.02432691f, 0.01289389f, 0.01212421f, 0.02296004f, 0.0376715f, 0.06881931f, 0.1438747f, 0.2405722f, 0.304278f, 0.2320753f, 0.07234258f, 0.04741869f, 0.1435242f, 0.1371385f, 0.2f, 0f, 0f, 0f, 0.1f, 0f, 0f, 0f, 0f, 0f, 0f, 0.2f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0.8085599f, 0.01441939f, 0f, 0.1f, 0.3f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0.1f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
        float[] sample_input = { 0.3251861f, 0.2550962f, 0.2394775f, 0.1618072f, 0.0660079f, 0.02432691f, 0.01289389f, 0.01212421f, 0.02296004f, 0.0376715f, 0.06881931f, 0.1438747f, 0.2405722f, 0.304278f, 0.2320753f, 0.07234258f, 0.04741869f, 0.1435242f, 0.1371385f, 0.2f, 0f, 0f, 0f, 0.1f, 0f, 0f, 0f, 0f, 0f, 0f, 0.2f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 1f, 0.2294327f,0.05805407f,0.02046564f,0.1045128f,0.2211845f,0.27195f,0.2580025f,0.200035f,0.1082949f,0.05355885f,0.08557549f,0.1843179f,0.2626979f,0.2715189f,0.1916021f,0.0626382f,0.001404463f,0.1129544f,0.1483373f,0.5f,0.3f,0f,0.1f,0f,0f,0f,0f,0f,0f,0f,0.1f,0.1538392f,0f,0f,0f,0f,0f,0f,0.03705015f,0f,0f,0f,0.02836293f,1f };
        




        float[] data = null;
		if (Input.GetKey("right")) {//Input.GetMouseButtonDown (1)) {			
			data = clientSocketHandler.getData (FRAME_BUFFER_SIZE, sample_input);	
			//
			if (data != null && data.Length == DIM_FEATURE*2) {					
				//Debug.Log ("Distance: " + (data[0]).ToString());
				Debug.Log(string.Format("value: {0:F6}", data[0]));
			}
		}

	}
}

//0.3251861	0.2550962	0.2394775	0.1618072	0.0660079	0.02432691	0.01289389	0.01212421	0.02296004	0.0376715	0.06881931	0.1438747	0.2405722	0.304278	0.2320753	0.07234258	0.04741869	0.1435242	0.1371385	0.2	0	0	0	0.1	0	0	0	0	0	0	0.2	0	0	0	0	0	0	0	0	0	0	0	0	1
//0	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0.8085599	0.01441939	0	0.1	0.3	0	0	0	0	0	0	0	0	0.1	0	0	0	0	0	0	0	0	0	0	0	0	0
//0.2294327	0.05805407	0.02046564	0.1045128	0.2211845	0.27195	0.2580025	0.200035	0.1082949	0.05355885	0.08557549	0.1843179	0.2626979	0.2715189	0.1916021	0.0626382	0.001404463	0.1129544	0.1483373	0.5	0.3	0	0.1	0	0	0	0	0	0	0	0.1	0.1538392	0	0	0	0	0	0	0.03705015	0	0	0	0.02836293	1
