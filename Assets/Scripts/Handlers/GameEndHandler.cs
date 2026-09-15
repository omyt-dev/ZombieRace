using System;
using Zenject;

namespace ZombieRace
{
    public class GameEndHandler : IDisposable
    {
        private readonly GameSession session;
        private readonly CarController carController;
        private readonly LevelProgress levelProgress;

        public GameEndHandler(GameSession session, CarController carController, LevelProgress levelProgress)
        {
            this.session = session;
            this.carController = carController;
            this.levelProgress = levelProgress;

            this.levelProgress.Completed += this.OnLevelCompleted;
            this.carController.Died += this.OnCarDied;
        }

        public void Dispose()
        {
            this.levelProgress.Completed -= this.OnLevelCompleted;
            this.carController.Died -= this.OnCarDied;
        }

        private void OnCarDied()
        {
            this.session.FinishLevel(false);
        }

        private void OnLevelCompleted()
        {
            this.session.FinishLevel(true);
        }
    }
}
