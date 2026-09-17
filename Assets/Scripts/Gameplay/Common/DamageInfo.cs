using System;
using UnityEngine;

namespace ZombieRace
{
    public struct DamageInfo
    {
        public readonly float Amount;
        public readonly Vector3 Direction;
        public readonly Vector3 HitPoint;

        public DamageInfo(float amount, Vector3 direction, Vector3 hitPoint)
        {
            this.Amount = amount;
            this.Direction = direction;
            this.HitPoint = hitPoint;
        }

        public static DamageInfo Detailed(float amount, Vector3 direction, Vector3 hitPoint)
            => new DamageInfo(amount, direction, hitPoint);
        public static DamageInfo Simple(float amount)
            => new DamageInfo(amount, Vector3.zero, Vector3.zero);
    }
}
