using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MobileControls : MonoBehaviour
{
#if !UNITY_EDITOR && UNITY_WEBGL
        [System.Runtime.InteropServices.DllImport("__Internal")]
        private static extern bool IsMobile();
#endif
        private void Awake()
        {
            CheckIfMobileAndEnableMobileUi();
        }

        private void CheckIfMobileAndEnableMobileUi()
        {
                var isMobile = false;
#if !UNITY_EDITOR && UNITY_WEBGL
                isMobile = IsMobile();
#endif
                SwitchToGamepadControls(isMobile);
        }

        private void SwitchToGamepadControls(bool activate)
        {
            if (!activate) return;

            foreach (Transform obj in transform)
            {
                obj.gameObject.SetActive(true);
            }
            
            FindObjectOfType<PlayerInput>().SwitchCurrentControlScheme("Gamepad", Gamepad.current);
        }
}
