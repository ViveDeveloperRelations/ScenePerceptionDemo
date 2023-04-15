// "Wave SDK 
// © 2020 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the Wave SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using UnityEngine;
using UnityEngine.UI;
using Wave.Native;
#if ENABLE_INPUT_SYSTEM
using Wave.Essence.HIDPlugin;
#endif

namespace Wave.Essence.Samples.ButtonTest
{
	public class TouchUsage : MonoBehaviour
	{
		const string LOG_TAG = "Wave.Essence.Samples.ButtonTest.TouchUsage";
		void DEBUG(string msg)
		{
			if (Log.EnableDebugLog)
				Log.d(LOG_TAG, msg, true);
		}

		public XR_Device deviceType = XR_Device.NonDominant;
		public WVR_InputId inputId = WVR_InputId.WVR_InputId_Alias1_System;
		public string usageName;

		public Text textComponent;
		public Image imageComponent;

		private void Start()
		{
			if (textComponent != null)
			{
				textComponent.text = usageName;
			}
		}

#if ENABLE_INPUT_SYSTEM
		WaveXRInput m_Input = null;
		private void Awake()
		{
			if (m_Input == null)
				m_Input = new WaveXRInput();
		}
		private void OnEnable()
		{
			m_Input.Enable();
		}
		private void OnDisable()
		{
			m_Input.Disable();
		}
#endif

		void Update()
		{
			bool buttonState = GetButtonState(deviceType, inputId);

			if (buttonState)
			{
				imageComponent.color = Color.green;
			}
			else
			{
				imageComponent.color = Color.red;
			}
		}

		bool GetButtonState(XR_Device deviceType, WVR_InputId inputId)
		{
#if ENABLE_INPUT_SYSTEM
			switch (deviceType)
			{
				case XR_Device.NonDominant:
					{
						switch (inputId)
						{
							case WVR_InputId.WVR_InputId_Alias1_Grip:
								return m_Input.Left.GripTouch.ReadValue<float>() > 0;
							case WVR_InputId.WVR_InputId_Alias1_A:
								return m_Input.Left.ButtonATouch.ReadValue<float>() > 0;
							case WVR_InputId.WVR_InputId_Alias1_B:
								return m_Input.Left.ButtonBTouch.ReadValue<float>() > 0;
							case WVR_InputId.WVR_InputId_Alias1_X:
								return m_Input.Left.ButtonXTouch.ReadValue<float>() > 0;
							case WVR_InputId.WVR_InputId_Alias1_Y:
								return m_Input.Left.ButtonYTouch.ReadValue<float>() > 0;
							case WVR_InputId.WVR_InputId_Alias1_Touchpad:
								return m_Input.Left.TouchpadTouch.ReadValue<float>() > 0;
							case WVR_InputId.WVR_InputId_Alias1_Bumper:
								return WaveXRController.current != null ? WaveXRController.current.leftBumperTouch.isPressed : false;
							case WVR_InputId.WVR_InputId_Alias1_Trigger:
								return WaveXRController.current != null ? WaveXRController.current.leftTriggerTouch.isPressed : false;
							case WVR_InputId.WVR_InputId_Alias1_Thumbstick:
								return m_Input.Left.JoystickTouch.ReadValue<float>() > 0;
							case WVR_InputId.WVR_InputId_Alias1_Parking:
								return m_Input.Left.ParkingTouch.ReadValue<float>() > 0;
							default:
								break;
						}
					}
					break;
				case XR_Device.Dominant:
					{
						switch (inputId)
						{
							case WVR_InputId.WVR_InputId_Alias1_Grip:
								return m_Input.Right.GripTouch.ReadValue<float>() > 0;
							case WVR_InputId.WVR_InputId_Alias1_A:
								return m_Input.Right.ButtonATouch.ReadValue<float>() > 0;
							case WVR_InputId.WVR_InputId_Alias1_B:
								return m_Input.Right.ButtonBTouch.ReadValue<float>() > 0;
							case WVR_InputId.WVR_InputId_Alias1_X:
								return m_Input.Right.ButtonXTouch.ReadValue<float>() > 0;
							case WVR_InputId.WVR_InputId_Alias1_Y:
								return m_Input.Right.ButtonYTouch.ReadValue<float>() > 0;
							case WVR_InputId.WVR_InputId_Alias1_Touchpad:
								return m_Input.Right.TouchpadTouch.ReadValue<float>() > 0;
							case WVR_InputId.WVR_InputId_Alias1_Bumper:
								return WaveXRController.current != null ? WaveXRController.current.rightBumperTouch.isPressed : false;
							case WVR_InputId.WVR_InputId_Alias1_Trigger:
								return WaveXRController.current != null ? WaveXRController.current.rightTriggerTouch.isPressed : false;
							case WVR_InputId.WVR_InputId_Alias1_Thumbstick:
								return m_Input.Right.JoystickTouch.ReadValue<float>() > 0;
							case WVR_InputId.WVR_InputId_Alias1_Parking:
								return m_Input.Right.ParkingTouch.ReadValue<float>() > 0;
							default:
								break;
						}
					}
					break;
				default:
					break;
			}
#endif
			return false;
		}
	}
}
