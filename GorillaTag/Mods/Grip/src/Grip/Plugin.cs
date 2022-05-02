using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Generic;

using BepInEx;
using BepInEx.Configuration;

using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using UnityEngine.Rendering;

using Photon.Pun;
using Photon.Realtime;

using Utilla;

namespace Grip
{
    /// <summary>
    /// This is your mod's main class.
    /// </summary>

    /* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    [Description("HauntedModMenu")]
    public class Grip : BaseUnityPlugin
    {
        public static bool Enabled = false;
        public static ConfigEntry<float> Range;

        void OnEnable()
        {
            var Config = new ConfigFile(Path.Combine(Paths.ConfigPath, "GorillaGrip.cfg"), true);
            Range = Config.Bind("Settings", "Range Multiplier", 1f, "The range multiplier of grip");
            HarmonyPatches.ApplyHarmonyPatches();
        }
        void OnDisable()
        {
            HarmonyPatches.RemoveHarmonyPatches();
        }

        

        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            if (gamemode.ToLower().Contains("modded"))
            {
                Enabled = true;
            } else
            {
                Enabled = false;
            }
        }

        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            Enabled = false;
            GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.useGravity = true;
        }
    }
}
