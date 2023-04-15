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
	public class PressUsage : MonoBehaviour
	{
		const string LOG_TAG = "Wave.Essence.Samples.ButtonTest.PressUsage";
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
				case XR_Device.Head:
					{
						switch (inputId)
						{
							case WVR_InputId.WVR_InputId_Alias1_System:
								return (WaveXRHmd.current != null ? WaveXRHmd.current.hmdSystemHold.isPressed : false);
							case WVR_InputId.WVR_InputId_Alias1_Back:
								return (WaveXRHmd.current != null ? WaveXRHmd.current.hmdBackHold.isPressed : false);
							case WVR_InputId.WVR_InputId_Alias1_Enter:
								return (WaveXRHmd.current != null ? WaveXRHmd.current.hmdEnterHold.isPressed : false);
							default:
								break;
						}
					}
					break;
				case XR_Device.NonDominant:
					{
						switch (inputId)
						{
							case WVR_InputId.WVR_InputId_Alias1_System:
								return (WaveXRController.current != null ? WaveXRController.current.leftSystemHold.isPressed : false);
							case WVR_InputId.WVR_InputId_Alias1_Menu:
								return (WaveXRController.current != null ? WaveXRController.current.leftMenuHold.isPressed : false);
							case WVR_InputId.WVR_InputId_Alias1_Grip:
								return (WaveXRController.current != null ? WaveXRController.current.leftGripHold.isPressed : false);
							case WVR_InputId.WVR_InputId_Alias1_Volume_Up:
								return (WaveXRController.current != null ? WaveXRController.current.leftVolumeUpHold.isPressed : false);
							case WVR_InputId.WVR_InputId_Alias1_Volume_Down:
								return (WaveXRController.current != null ? WaveXRController.current.leftVolumeDownHold.isPressed : false);
							case WVR_InputId.WVR_InputId_Alias1_Bumper:
								return (WaveXRController.current != null ? WaveXRController.current.leftBumperHold.isPressed : false);
							case WVR_InputId.WVR_InputId_Alias1_A:
								return (WaveXRController.current != null ? WaveXRController.current.leftAHold.isPressed : false);
							case WVR_InputId.WVR_InputId_Alias1_B:
								return (WaveXRController.current != null ? WaveXRController.current.leftBHold.isPressed : false);
							case WVR_InputId.WVR_InputId_Alias1_X:
								return (WaveXRController.current != null ? WaveXRController.current.leftXHold.isPressed : false);
							case WVR_InputId.WVR_InputId_Alias1_Y:
								return (WaveXRController.current != null ? WaveXRController.current.leftYHold.isPressed : false);
							case WVR_InputId.WVR_InputId_Alias1_Back:
								return (WaveXRController.current != null ? WaveXRController.current.leftBackHold.isPressed : false);
							case WVR_InputId.WVR_InputId_Alias1_Enter:
								return (WaveXRController.current != null ? WaveXRController.current.leftEnterHold.isPressed : false);
							case WVR_InputId.WVR_InputId_Alias1_Touchpad:
								return (WaveXRController.current != null ? WaveXRController.current.leftTouchpadHold.isPressed : false);
							case WVR_InputId.WVR_InputId_Alias1_Trigger:
								return (WaveXRController.current != null ? WaveXRController.current.leftTriggerHold.isPressed : false);
							case WVR_InputId.WVR_InputId_Alias1_Thumbstick:
								return (WaveXRController.current != null ? WaveXRController.current.leftJoystickHold.isPressed : false);
							default:
								break;
						}
					}
					break;
				case XR_Device.Dominant:
					{
						switch (inputId)
						{
							case WVR_InputId.WVR_InputId_Alias1_System:
								return (WaveXRController.current != null ? WaveXRController.current.rightSystemHold.isPressed : false);
							case WVR_InputId.WVR_InputId_Alias1_Menu:
								return (WaveXRController.current != null ? WaveXRController.current.rightMenuHold.isPressed : false);
							case WVR_InputId.WVR_InputId_Alias1_Grip:
								return (WaveXRController.current != null ? WaveXRController.current.rightGripHold.isPressed : false);
							case WVR_InputId.WVR_InputId_Alias1_Volume_Up:
								return (WaveXRController.current != null ? WaveXRController.current.rightVolumeUpHold.isPressed : false);
							case WVR_InputId.WVR_InputId_Alias1_Volume_Down:
								return (WaveXRController.current != null ? WaveXRController.current.rightVolumeDownHold.isPressed : false);
							case WVR_InputId.WVR_InputId_Alias1_Bumper:
								return (WaveXRController.current != null ? WaveXRController.current.rightBumperHold.isPressed : false);
							case WVR_InputId.WVR_InputId_Alias1_A:
								return (WaveXRController.current != null ? WaveXRController.current.rightAHold.isPressed : false);
							case WVR_InputId.WVR_InputId_Alias1_B:
								return (WaveXRController.current != null ? WaveXRController.current.rightBHold.isPressed : false);
							case WVR_InputId.WVR_InputId_Alias1_X:
								return (WaveXRController.current != null ? WaveXRController.current.rightXHold.isPressed : false);
							case WVR_InputId.WVR_InputId_Alias1_Y:
								return (WaveXRController.current != null ? WaveXRController.current.rightYHold.isPressed : false);
							case WVR_InputId.WVR_InputId_Alias1_Back:
								return (WaveXRController.current != null ? WaveXRController.current.rightBackHold.isPressed : false);
							case WVR_InputId.WVR_InputId_Alias1_Enter:
								return (WaveXRController.current != null ? WaveXRController.current.rightEnterHold.isPressed : false);
							case WVR_InputId.WVR_InputId_Alias1_Touchpad:
								return (WaveXRController.current != null ? WaveXRController.current.rightTouchpadHold.isPressed : false);
							case WVR_InputId.WVR_InputId_Alias1_Trigger:
								return (WaveXRController.current != null ? WaveXRController.current.rightTriggerHold.isPressed : false);
							case WVR_InputId.WVR_InputId_Alias1_Thumbstick:
								return (WaveXRController.current != null ? WaveXRController.current.rightJoystickHold.isPressed : false);
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
