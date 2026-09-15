using System;
using UnityEngine;

namespace ZombieRace
{
    public class TurretAim : MonoBehaviour
    {
        [SerializeField] private Transform turretPivot;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float minAngle;
        [SerializeField] private float maxAngle;

        public float AimAt(Vector3 target)
        {
            Vector3 direction = (target - this.turretPivot.position).FlatY().normalized;
            Vector3 localDirection = this.turretPivot.parent.InverseTransformDirection(direction);

            float targetAngle = Vector3.SignedAngle(Vector3.forward, localDirection, Vector3.up);
            return AimAt(targetAngle);
        }

        public float AimAt(float targetAngle)
        {
            targetAngle = Mathf.Clamp(targetAngle, this.minAngle, this.maxAngle);
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

            this.turretPivot.localRotation = Quaternion.RotateTowards(this.turretPivot.localRotation, targetRotation, this.rotationSpeed * Time.deltaTime);
            return targetAngle;
        }

        public void ResetAim()
        {
            this.turretPivot.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
