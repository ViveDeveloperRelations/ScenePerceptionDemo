// "Wave SDK 
// © 2020 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the Wave SDK(s).
// You shall fully comply with all of HTC\u2019s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using UnityEngine;
using UnityEngine.UI;
using Wave.OpenXR;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;

namespace Wave.XR.Sample.Controller
{
	[RequireComponent(typeof(Text))]
	public class PoseTracking : MonoBehaviour
	{
		const string LOG_TAG = "Wave.XR.Sample.Controller.PoseTracking";
		void DEBUG(string msg) { Debug.Log(LOG_TAG + " " + (IsLeft ? "Left" : "Right") + ", " + msg); }

		public bool IsLeft = false;

		[SerializeField]
		private InputActionReference m_Position = null;
		public InputActionReference Position { get { return m_Position; } set { m_Position = value; } }

		private static bool VALIDATE(InputActionReference actionReference, out string msg)
		{
			msg = "Normal";

			if (actionReference == null)
			{
				msg = "Null reference.";
				return false;
			}
			else if (actionReference.action == null)
			{
				msg = "Null reference action.";
				return false;
			}
			else if (!actionReference.action.enabled)
			{
				msg = "Reference action disabled.";
				return false;
			}
			else if (actionReference.action.activeControl == null)
			{
				msg = "No active control of the reference action, phase: " + actionReference.action.phase;
				return false;
			}
			else if (actionReference.action.controls.Count <= 0)
			{
				msg = "Action control count is " + actionReference.action.controls.Count;
				return false;
			}

			return true;
		}

		Vector3 getPosition(InputActionReference actionReference)
		{
			var position = Vector3.zero;

			if (VALIDATE(actionReference, out string value))
			{
				if (actionReference.action.activeControl.valueType == typeof(Vector3))
				{
					position = actionReference.action.ReadValue<Vector3>();
					if (m_Text != null)
						m_Text.text = (IsLeft ? "Left" : "Right") + ": (" + position.x.ToString() + ", " + position.y.ToString() + ", " + position.z.ToString() + ")";
				}
			}
			else
			{
				if (InputDeviceControl.GetPosition(
					IsLeft ? InputDeviceControl.kControllerLeftCharacteristics : InputDeviceControl.kControllerRightCharacteristics,
					out position)
				)
				{
					if (m_Text != null)
						m_Text.text = (IsLeft ? "Left" : "Right") + ": (" + position.x + ", " + position.y + ", " + position.z + ")"
							+ "\ninvalid input: " + value;
				}
				else
				{
					if (m_Text != null)
						m_Text.text = (IsLeft ? "Left" : "Right") + ": invalid pose.";
				}
			}

			return position;
		}

		private Text m_Text = null;
		private void Start()
		{
			m_Text = GetComponent<Text>();
		}

		void Update()
		{
			getPosition(m_Position);
		}
	}
}
#endif
