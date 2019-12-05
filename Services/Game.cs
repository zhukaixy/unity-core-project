﻿using UnityLogger;
using Zenject;

namespace Core.Services
{
    /// <summary>
    /// Starting point for Core Framework.
    /// </summary>
    public abstract class Game : CoreBehaviour
    {
        [Inject] //This is set up on SceneInstaller
        private SignalBus _signalBus;

        protected virtual void Awake()
        {
            //Listen to game lifetime events
            _signalBus.Subscribe<OnGameStartedSignal>(OnGameStart);
            _signalBus.Subscribe<OnGamePaused>(OnGamePausedInternal);
            _signalBus.Subscribe<OnGameResumed>(OnGameResumedInternal);
            _signalBus.Subscribe<OnGameLostFocus>(OnGameLostFocusInternal);
            _signalBus.Subscribe<OnGameGotFocus>(OnGameGotFocusInternal);
            _signalBus.Subscribe<OnGameQuit>(OnGameQuitInternal);
        }

        /// <summary>
        /// Method triggered when the game starts.
        /// </summary>
        protected virtual void OnGameStart()
        {
            _signalBus.TryUnsubscribe<OnGameStartedSignal>(OnGameStart);

            Logger.Log("Game Started", Colors.Lime);
        }

        private void OnGamePausedInternal()
        {
            OnGamePaused(true);
        }

        private void OnGameResumedInternal()
        {
            OnGamePaused(false);
        }

        private void OnGameLostFocusInternal()
        {
            OnGameFocusChange(false);
        }

        private void OnGameGotFocusInternal()
        {
            OnGameFocusChange(true);
        }

        private void OnGameQuitInternal()
        {
            _signalBus.TryUnsubscribe<OnGameQuit>(OnGameStart);

            OnGameQuit();
        }

        protected abstract void OnGamePaused(bool isPaused);
        protected abstract void OnGameFocusChange(bool hasFocus);
        protected abstract void OnGameQuit();
    }
}