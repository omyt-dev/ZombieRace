using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace ZombieRace
{
    public class CameraController : BaseBehaviour
    {
        private CinemachineCamera camera;
        private CarController car;
        private Vector3 lastPosition;

        [Inject]
        private void Construct(CarController car)
        {
            this.car = car;
            this.camera = this.GetComponent<CinemachineCamera>();
            this.camera.Target.TrackingTarget = car.transform;
        }

        protected override void ExitPlaying()
        {
            this.lastPosition = this.car.transform.position;
        }

        protected override void ResetBehaviour()
        {
            this.camera.OnTargetObjectWarped(this.car.transform, -lastPosition);
        }
    }
}
