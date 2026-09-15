using UnityEngine;

namespace ZombieRace
{
    public static class Vector3Extension
    {
        public static Vector3 FlatY(this Vector3 vector)
        {
            vector.y = 0;
            return vector;
        }
    }
}
