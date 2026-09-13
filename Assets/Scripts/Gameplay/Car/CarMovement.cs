using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ZombieRace
{
    public class CarMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;

        private bool isMoving = false;

        public void StartMoving()
        {
            isMoving = true;
        }

        public void StopMoving()
        {
            isMoving = false;
        }

        private void Update()
        {
            if (isMoving)
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
