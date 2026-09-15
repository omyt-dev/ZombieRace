using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ZombieRace
{
    public class UIWorldSpaceCanvas : MonoBehaviour
    {
        private Canvas canvas;
        private new Camera camera;

        protected void Awake()
        {
            this.canvas = this.GetComponent<Canvas>();
            this.camera = Camera.main;

            this.canvas.worldCamera = this.camera;
        }

        private void LateUpdate()
        {
            this.transform.rotation = this.camera.transform.rotation;
        }
    }
}
