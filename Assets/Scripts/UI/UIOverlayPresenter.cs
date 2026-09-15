using System;
using System.Collections.Generic;
using UnityEngine;
using ZombieRace;

namespace Assets.Scripts.UI
{
    public class UIOverlayPresenter : BaseBehaviour
    {
        [Serializable]
        private class OverlayItem
        {
            public EGameState gameState;
            public GameObject overlay;
        }

        [SerializeField] private List<OverlayItem> overlays;

        protected override void GameStateChanged(EGameState previousState, EGameState newState)
        {
            this.overlays.ForEach(x => x.overlay.SetActive(x.gameState == newState));
        }
    }
}
