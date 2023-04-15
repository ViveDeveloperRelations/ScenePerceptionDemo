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
	public class AxisUsage : MonoBehaviour
	{
		const string LOG_TAG = "Wave.Essence.Samples.ButtonTest.AxisUsage";
		void DEBUG(string msg)
		{
			if (Log.EnableDebugLog)
				Log.d(LOG_TAG, msg, true);
		}

		public XR_Device deviceType = XR_Device.NonDominant;
		public WVR_InputId inputId = WVR_InputId.WVR_InputId_Alias1_System;
		public string usageName;

		public Text textComponent;
		public Slider sliderComponent;
		public Text valueTextComponent;

		public float currentValue { get; private set; }

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
			float value = GetButtonAxis(deviceType, inputId);

			currentValue = value;

			if (sliderComponent != null)
			{
				sliderComponent.value = value;
			}

			if (valueTextComponent != null)
			{
				valueTextComponent.text = value.ToString("F");
			}
		}

		float GetButtonAxis(XR_Device deviceType, WVR_InputId inputId)
		{
#if ENABLE_INPUT_SYSTEM
			switch (deviceType)
			{
				case XR_Device.NonDominant:
					{
						switch (inputId)
						{
							case WVR_InputId.WVR_InputId_Alias1_Grip:
								return m_Input.Left.GripAxis.ReadValue<float>();
							case WVR_InputId.WVR_InputId_Alias1_Bumper:
								return WaveXRController.current != null ? WaveXRController.current.leftBumperAxis.ReadValue() : 0;
							case WVR_InputId.WVR_InputId_Alias1_Trigger:
								return WaveXRController.current != null ? WaveXRController.current.leftTriggerAxis.ReadValue() : 0;
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
								return m_Input.Right.GripAxis.ReadValue<float>();
							case WVR_InputId.WVR_InputId_Alias1_Bumper:
								return WaveXRController.current != null ? WaveXRController.current.rightBumerAxis.ReadValue() : 0;
							case WVR_InputId.WVR_InputId_Alias1_Trigger:
								return WaveXRController.current != null ? WaveXRController.current.rightTriggerAxis.ReadValue(): 0;
							default:
								break;
						}
					}
					break;
				default:
					break;
			}
#endif

			return 0;
		}
	}
}
