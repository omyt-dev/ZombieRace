using System;
using Zenject;

namespace ZombieRace
{
    public class GameEndHandler : IDisposable
    {
        private readonly GameSession session;
        private readonly CarController carController;

        public GameEndHandler(GameSession session, CarController carController)
        {
            this.session = session;
            this.carController = carController;
            this.carController.Died += this.OnCarDied;
        }

        public void Dispose()
        {
            this.carController.Died -= this.OnCarDied;
        }

        private void OnCarDied()
        {
            this.session.FinishLevel(false);
        }
    }
}
