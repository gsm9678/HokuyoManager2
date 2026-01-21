/*
	Created by Carl Emil Carlsen.
	Copyright 2016-2020 Sixth Sensor.
	All rights reserved.
	http://sixthsensor.dk
*/

using UnityEngine;

namespace OscSimpl.Examples
{
	public class GettingStartedReceiving : MonoBehaviour
	{
		[SerializeField] OscIn _oscIn;
		public GameObject Manager;
		//TesseractDemoScript Script;

		const string address1 = "/Animal_ID_Received";
		const string address2 = "/test2";

		public static event System.Action<bool> OnSignalReceived;


		void Start()
		{
			// Ensure that we have a OscIn component and start receiving on port 7000.
			if( !_oscIn ) _oscIn = gameObject.AddComponent<OscIn>();
			_oscIn.Open(_oscIn.port);
			//Script = Manager.GetComponent<TesseractDemoScript>();
		}


		void OnEnable()
		{
			// You can "map" messages to methods in two ways:

			// 1) For messages with a single argument, route the value using the type specific map methods.
			_oscIn.MapBool( address1, DebugSiginal);
		}


		void OnDisable()
		{
			// If you want to stop receiving messages you have to "unmap".
			_oscIn.UnmapBool(DebugSiginal);
		}

		
		void DebugSiginal( bool value )
		{
			//Script.SendOSC = false;
			OnSignalReceived?.Invoke(value);
		}


	}
}