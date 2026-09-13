using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace ZombieRace
{
    public class GameInputHandler : MonoBehaviour
    {
        private GameSession session;

        [Inject]
        private void Construct(GameSession session)
        {
            this.session = session;
        }

        private void Update()
        {
            if (!CheckInput())
                return;

            switch (session.CurrentState)
            {
                case EGameState.Ready:
                    this.session.StartLevel();
                    break;
                case EGameState.Win:
                case EGameState.Lose:
                    this.session.RestartLevel();
                    break;
            }
        }

        private bool CheckInput()
        {
            if (Mouse.current?.leftButton.wasPressedThisFrame ?? false)
                return true;

            if (Touchscreen.current?.primaryTouch.press.wasPressedThisFrame ?? false)
                return true;

            return false;
        }
    }
}
