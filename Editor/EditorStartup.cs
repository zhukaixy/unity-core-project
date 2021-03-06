﻿using Core.Common.Extensions.String;
using UnityEditor;
using UnityEngine;
using Logger = UnityLogger.Logger;

namespace Core
{
    public class EditorStartup
    {
        [InitializeOnLoad]
        public class Startup
        {
            static Startup()
            {
                if (EditorPreferences.EditorprefFirstTimeUse)
                {
                    Logger.Log("First time use. Enabling asset bundle simulation mode.",Colors.Yellow);

                    EditorPreferences.EditorprefSimulateAssetBundles = true;
                    EditorPreferences.EditorprefFirstTimeUse = false;
                }
            }
        }
    }
}