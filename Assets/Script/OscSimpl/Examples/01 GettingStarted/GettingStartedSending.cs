/*
	Created by Carl Emil Carlsen.
	Copyright 2016-2020 Sixth Sensor.
	All rights reserved.
	http://sixthsensor.dk
*/

using UnityEngine;

namespace OscSimpl.Examples
{
	public class GettingStartedSending : MonoBehaviour
	{
		[SerializeField] OscOut _oscOut;

		OscMessage _message2; // Cached message.
		public GameObject Manager;
		//TesseractDemoScript Script;
		bool signalReceived = false;

		const string address1 = "/Animal_ID";
		const string address2 = "/test2";


		void Start()
		{
			// Ensure that we have a OscOut component.
			if( !_oscOut ) _oscOut = gameObject.AddComponent<OscOut>();

			// Prepare for sending messages locally on this device on port 7000.
			_oscOut.Open(_oscOut.port);

			// ... or, alternatively target a remote devices with an IP Address.
			//oscOut.Open( 7000, "192.168.1.101" );

			// If you want to send a single value then you can use this one-liner.
			//_oscOut.Send( address1, Random.value );

			// If you want to send a message with multiple values, then you
			// need to create a message, add your values and send it.
			// Always cache messages you create, so that you can reuse them.
			//_message2 = new OscMessage( address2 );
			//_message2.Add( Time.frameCount ).Add( Time.time ).Add( Random.value );
			//_oscOut.Send( _message2 );
			//Script = Manager.GetComponent<TesseractDemoScript>();

			// Subscribe to the signal received event
			GettingStartedReceiving.OnSignalReceived += OnSignalReceived;
		}

		void OnDestroy()
		{
			// Unsubscribe from the signal received event
			GettingStartedReceiving.OnSignalReceived -= OnSignalReceived;
		}


		void Update()
		{
			//if (Script.SendOSC)
			//{
			//	_oscOut.Send(address1, Script.ID);
           //     Script.SendOSC = false;
           // }

		}
		void OnSignalReceived(bool received)
		{
			if (received)
			{
				signalReceived = true;
			}
			else
			{
				signalReceived = false;
			}
		}
	}
}