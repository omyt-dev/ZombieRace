using System.Collections.Generic;

namespace ZombieRace
{
    public class GameResetHandler
    {
        private readonly IReadOnlyList<IResetable> resetables;

        public GameResetHandler(List<IResetable> resetables)
        {
            this.resetables = resetables;
        }

        public void Reset()
        {
            foreach (var item in resetables)
                item.Reset();
        }
    }
}
