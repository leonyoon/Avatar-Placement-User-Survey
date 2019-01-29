using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Linq;


public class ClientSocketHandler : MonoBehaviour {

	//
	Socket sender = null;
	bool connection_success = false;

	// Data buffer for incoming data.  
	const int BUFFER_SIZE = 2048;
	byte[] buffer = new byte[BUFFER_SIZE];  

	// Use this for initialization
	void Start() {
		
		// Connect to a remote device.  
		try {  
			// Establish the remote endpoint for the socket.  
			// This example uses port 8820 on the local computer. 
			String ipString = "127.0.0.1";//127.0.0.1//143.248.8.108
			IPAddress ipAddress = IPAddress.Parse(ipString);
			//Debug.Log("Parsing your input string: " + "\"" + ipString + "\"" + " produces this address (shown in its standard notation): "+ ipAddress.ToString());
			IPEndPoint remoteEP = new IPEndPoint(ipAddress, 8820);  

			// Create a TCP/IP  socket.  
			sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );

			// Connect the socket to the remote endpoint. Catch any errors.  
			try {  
				sender.Connect(remoteEP);  
				connection_success = true;
				Debug.Log("Socket connected to " + sender.RemoteEndPoint.ToString());
			} 
			catch (ArgumentNullException ane) {  
				Debug.Log("ArgumentNullException : " + ane.ToString());  
			} 
			catch (SocketException se) {  
				Debug.Log("SocketException : " + se.ToString());  
			} 
			catch (Exception e) {  
				Debug.Log("Unexpected exception : " + e.ToString());
			}  

		} 
		catch (Exception e) {  
			Debug.Log( e.ToString());  
		} 
	}

	// Update is called once per frame
	public float[] getData(int data_size, float[] msg_sending){
		//
		if (!connection_success) {
			Debug.Log ("The connection was not successful.");
			return new float[0];
		}

		//Debug.Log ("Length of msg_sending: ");
		//for(i=0; i!=msg_sending.Length; ++i) 
		//foreach(var item in msg_sending) Debug.Log(item.ToString());
		//Debug.Log("Length of msg_sending: " + msg_sending.Length.ToString());

		// Encode the data string into a byte array.  
		// byte[] msg_sending_bytes = Encoding.ASCII.GetBytes (msg_sending);
		//byte[] msg_sending_bytes = msg_sending.Select(x => Convert.ToByte(x)).ToArray();

		// create a byte array and copy the floats into it...
		var msg_sending_bytes = new byte[msg_sending.Length * 4];
		Buffer.BlockCopy(msg_sending, 0, msg_sending_bytes, 0, msg_sending_bytes.Length);

		//foreach(var item in msg_sending_bytes) Debug.Log(item.ToString());
		//Debug.Log("Length of msg_sending_bytes: " + msg_sending_bytes.Length.ToString());

		// Send the data through the socket.  
		int bytesSent = sender.Send(msg_sending_bytes);

		// Receive the response from the remote device.
		int bytesRec = sender.Receive (buffer);  

		//
		float[] data = new float[data_size / sizeof(float)];
		for (int i = 0; i < data.Length; i++)
			data[i] = (float)BitConverter.ToSingle(buffer, i * sizeof(float));

		//
		return data;
	}

	// Update is called once per frame
	void Update() {

	}

	void OnDestroy() {
		// Release the socket.  
		sender.Shutdown(SocketShutdown.Send);  
		sender.Close(); 
	}
}


