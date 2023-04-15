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
	public class StickUsage : MonoBehaviour
	{
		const string LOG_TAG = "Wave.Essence.Samples.ButtonTest.StickUsage";
		void DEBUG(string msg)
		{
			if (Log.EnableDebugLog)
				Log.d(LOG_TAG, msg, true);
		}

		public XR_Device deviceType = XR_Device.NonDominant;
		public WVR_InputId inputId = WVR_InputId.WVR_InputId_Alias1_System;
		public string usageName;

		public Text textComponent;
		public Slider horizontalSliderComponent;
		public Slider verticalSliderComponent;
		public Text valueTextComponent;

		public float currentXValue { get; private set; }
		public float currentYValue { get; private set; }

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
			Vector2 value = GetButtonVector(deviceType, inputId);

			currentXValue = value.x;
			currentYValue = value.y;

			if (horizontalSliderComponent != null)
				horizontalSliderComponent.value = value.x;

			if (verticalSliderComponent != null)
				verticalSliderComponent.value = value.y;

			if (valueTextComponent != null)
				valueTextComponent.text = string.Format("[{0},{1}]", value.x.ToString("F"), value.y.ToString("F"));
		}

		Vector2 GetButtonVector(XR_Device deviceType, WVR_InputId inputId)
		{
#if ENABLE_INPUT_SYSTEM
			switch (deviceType)
			{
				case XR_Device.NonDominant:
					{
						switch (inputId)
						{
							case WVR_InputId.WVR_InputId_Alias1_Touchpad:
								return (WaveXRController.current != null ? WaveXRController.current.leftTouchpadAxis.ReadValue() : Vector2.zero);
							case WVR_InputId.WVR_InputId_Alias1_Thumbstick:
								return (WaveXRController.current != null ? WaveXRController.current.leftJoystickAxis.ReadValue() : Vector2.zero);
							default:
								break;
						}
					}
					break;
				case XR_Device.Dominant:
					{
						switch (inputId)
						{
							case WVR_InputId.WVR_InputId_Alias1_Touchpad:
								return m_Input.Right.TouchpadAxis.ReadValue<Vector2>();
							case WVR_InputId.WVR_InputId_Alias1_Thumbstick:
								return m_Input.Right.JoystickAxis.ReadValue<Vector2>();
							default:
								break;
						}
					}
					break;
				default:
					break;
			}
#endif
			return Vector2.zero;
		}
	}
}
