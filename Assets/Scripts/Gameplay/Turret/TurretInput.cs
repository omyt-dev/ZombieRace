using UnityEngine;
using UnityEngine.InputSystem;

namespace ZombieRace
{
    public class TurretInput : MonoBehaviour
    {
        [SerializeField] private float sensitivity = 1f;

        public float GetInputDelta()
        {
            if (Mouse.current?.leftButton.IsPressed() ?? false)
                return Mouse.current.delta.ReadValue().x * this.sensitivity;

            if (Touchscreen.current?.primaryTouch.press.IsPressed() ?? false)
                return Touchscreen.current.primaryTouch.delta.ReadValue().x * this.sensitivity;

            return 0;
        }
    }
}
