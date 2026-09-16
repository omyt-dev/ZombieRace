using System;
using System.Collections.Generic;
using System.Text;
using Unity.AppUI.Redux;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ZombieRace
{
    public class CarMovement : MonoBehaviour
    {
        private float speed;
        private bool isMoving = false;

        private AnimationCurve angleCurve;
        private float angleMax;
        private float segmentDistanceMax;
        private float segmentDistanceMin;

        private float segmentEnd;
        private float segmentStart;
        private float segmentAngle;
        private float segmentSide;

        public void Initialize(float speed, AnimationCurve angleCurve, float angleMax, float segmentDistanceMin, float segmentDistanceMax)
        {
            this.angleCurve = angleCurve;
            this.angleMax = angleMax;
            this.segmentDistanceMin = segmentDistanceMin;
            this.segmentDistanceMax = segmentDistanceMax;
            this.speed = speed;
        }

        public void StartMoving()
        {
            this.isMoving = true;
            this.EvaluateSegmentValues();
        }

        public void StopMoving()
        {
            this.isMoving = false;
        }

        private void Update()
        {
            if (!isMoving)
                return;

            if (this.segmentEnd < this.transform.position.z)
                EvaluateSegmentValues();

            float curveTime = Mathf.InverseLerp(this.segmentStart, this.segmentEnd, this.transform.position.z);
            float currentShift = this.angleCurve.Evaluate(curveTime) * this.segmentAngle * this.segmentSide;

            this.transform.rotation = Quaternion.Euler(0, currentShift, 0);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private void EvaluateSegmentValues()
        {
            this.segmentAngle = Random.Range(0, angleMax);
            this.segmentStart = this.transform.position.z;
            this.segmentEnd = this.segmentStart + Random.Range(segmentDistanceMin, segmentDistanceMax);
            this.segmentSide = Random.Range(-1f, 1f) < 0 ? -1 : 1;
        }
    }
    
}
