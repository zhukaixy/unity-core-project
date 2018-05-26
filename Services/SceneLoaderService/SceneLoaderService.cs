﻿using System;
using System.Threading.Tasks;
using Core.Services.Assets;
using Core.Services.UI;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Core.Services.Scenes
{
    public class SceneLoaderService : Service
    {
        protected SceneLoaderServiceConfiguration configuration;

        [Inject]
        protected AssetService _assetService;

        [Inject]
        protected UIService _uiService;

        public SceneLoaderService(ServiceConfiguration config)
        {
            configuration = config as SceneLoaderServiceConfiguration;
        }

        /// <summary>
        /// Attempts to load a scene from an asset bundle
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="mode"> </param>
        /// <returns></returns>
        public async Task<UnityEngine.Object> LoadScene(string scene, LoadSceneMode mode = LoadSceneMode.Single)
        {
            _uiService.DarkenScreen(true).Subscribe();
            return await DoLoadScene(scene, mode);
        }

        private async Task<UnityEngine.Object> DoLoadScene(string scene, LoadSceneMode mode = LoadSceneMode.Single)
        {
            if (_assetService.GetLoadedBundle<UnityEngine.Object>(scene))
                throw new Exception("Scene " + scene + " is already loaded and open. Opening the same scene twice is not supported.");

            return await GetScene(scene, mode);
        }

        /// <summary>
        /// Gets a scene from an asset bundle
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="mode"> </param>
        /// <returns></returns>
        private async Task<UnityEngine.Object> GetScene(string scene, LoadSceneMode mode = LoadSceneMode.Single)
        {
            var bundleNeeded = new BundleRequest(AssetCategoryRoot.Scenes, scene, scene, _assetService.Configuration);
            var sceneObject = await _assetService.GetAndLoadAsset<UnityEngine.Object>(bundleNeeded);

            if (sceneObject)
            {
                Debug.Log(("SceneLoaderService: Loaded scene - " + scene).Colored(Colors.LightBlue));

                await SceneManager.LoadSceneAsync(scene, mode);
                Resources.UnloadUnusedAssets();
                //Scene loaded, return screen to normal.
                _uiService.DarkenScreen(false).Subscribe();
                Debug.Log(("SceneLoaderService: Opened scene - " + scene).Colored(Colors.LightBlue));
            }

            return sceneObject;
        }

        /// <summary>
        /// Unload scene.
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        public async Task UnLoadScene(string scene)
        {
            await SceneManager.UnloadSceneAsync(scene);
            
            Debug.Log(("SceneLoaderService: Unloaded scene - " + scene).Colored(Colors.LightBlue));
            
            _assetService.UnloadAsset(scene, true);
        }
    }
}