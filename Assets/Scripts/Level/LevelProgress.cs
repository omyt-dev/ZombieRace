using System;
using UnityEngine;
using Zenject;

namespace ZombieRace
{
    public class LevelProgress : ITickable, IResetable
    {
        private readonly CarController carController;

        public float Distance { get; private set; }
        public float LevelLenght { get; }

        public float Progress => Mathf.Clamp01(this.LevelLenght > 0f ? this.Distance / this.LevelLenght : 0f);
        public bool IsCompleted => this.Distance >= this.LevelLenght;

        public event Action<float> ProgressChanged;
        public event Action Completed;

        public LevelProgress(CarController carController)
        {
            this.carController = carController;
            this.LevelLenght = 500f;
        }

        public void Tick()
        {
            float distance = this.carController.transform.position.z;

            if (distance == Distance)
                return;

            this.Distance = distance;
            this.ProgressChanged?.Invoke(this.Progress);

            StylizedLog.Log("LevelProgress", $"{Distance}/{LevelLenght}", StylizedLog.Orange);

            if (this.IsCompleted)
                this.Completed?.Invoke();
        }

        public void Reset()
        {
            this.Distance = 0f;
            this.ProgressChanged?.Invoke(0f);
        }
    }
}
