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

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;

namespace Wave.XR.Sample.Controller
{
	public class HideUntrackedObject : MonoBehaviour
	{
		const string LOG_TAG = "Wave.XR.Sample.Controller.HideUntrackedObject";
		void DEBUG(string msg) { Debug.Log(LOG_TAG + msg); }
		void INTERVAL(string msg) { if (printIntervalLog && !Application.isEditor) { DEBUG(msg); } }

		[SerializeField]
		private InputActionReference m_IsTracked;
		public InputActionReference IsTracked { get => m_IsTracked; set => m_IsTracked = value; }

		[SerializeField]
		private GameObject m_ObjectToHide = null;
		public GameObject ObjectToHide { get => m_ObjectToHide; set => m_ObjectToHide = value; }

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

		int printFrame = 0;
		bool printIntervalLog = false;
		private void Update()
		{
			printFrame++;
			printFrame %= 300;
			printIntervalLog = (printFrame == 0);

			bool isTracked = false;
			if (VALIDATE(m_IsTracked, out string msg))
			{
				if (m_IsTracked.action.activeControl.valueType == typeof(float))
					isTracked = m_IsTracked.action.ReadValue<float>() > 0;
				if (m_IsTracked.action.activeControl.valueType == typeof(bool))
					isTracked = m_IsTracked.action.ReadValue<bool>();

				INTERVAL("Update() " + m_IsTracked.action.name + ", isTracked: " + isTracked);
			}
			else
			{
				INTERVAL("Update() " + msg);
			}
			m_ObjectToHide.SetActive(isTracked);
		}
	}
}
#endif
