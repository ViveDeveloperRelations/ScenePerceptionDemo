#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

namespace UnityEngine.XR.OpenXR.Samples.ControllerSample
{
    public class ActionToHaptics : MonoBehaviour
    {
        public InputActionReference action;
        public InputActionReference hapticAction;
        public float _amplitude = 1.0f;
        public float _duration = 0.1f;
        public float _frequency = 0.0f;

        private void Start()
        {
			if (action != null && action.action != null) { action.action.performed += OnActionPerformed; }
			if (action == null || hapticAction == null)
                return;

            action.action.Enable();
            hapticAction.action.Enable();
            action.action.performed += (ctx) =>
            {
                var control = action.action.activeControl;
                if (null == control)
                    return;
            };
        }

		protected void OnActionPerformed(InputAction.CallbackContext ctx)
		{
			Debug.Log("ActionToHaptics() " + ctx.control.device.description.product);
			if (ctx.control.device.description.product.Equals("WVR_CR_Left_001"))
			{
				XRControllerWithRumble controllerLeft = InputSystem.InputSystem.GetDevice<XRControllerWithRumble>("LeftHand");
				if (controllerLeft != null)
				{
					Debug.Log("ActionToHaptics() has left controllerLeft.");
					controllerLeft.SendImpulse(0.9f, 0.5f);
				}
			}
			if (ctx.control.device.description.product.Equals("WVR_CR_Right_001"))
			{
				XRControllerWithRumble controllerRight = InputSystem.InputSystem.GetDevice<XRControllerWithRumble>("RightHand");
				if (controllerRight != null)
				{
					Debug.Log("ActionToHaptics() has right controllerLeft.");
					controllerRight.SendImpulse(_amplitude, _duration);
				}
			}
		}
	}
}
#endif
