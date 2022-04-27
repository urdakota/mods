using BepInEx;
using BepInEx.Configuration;
using System;
using System.IO;
using System.ComponentModel;
using UnityEngine;
using Utilla;

namespace Speed
{
    /// <summary>
    /// This is your mod's main class.
    /// </summary>

    /* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
    [ModdedGamemode]
    [Description("HauntedModMenu")]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Speed : BaseUnityPlugin
    {
        bool inRoom = false;
        public static bool Enabled = false;
        public static ConfigEntry<float> Multiplier;

        void OnEnable()
        {
            var file = new ConfigFile(Path.Combine(Paths.ConfigPath, "Speed.cfg"), true);
            Multiplier = file.Bind("Speed Settings", "Multiplier", 2f, "Multiplier of jump boost (2 = 2x speed etc.)");

            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            HarmonyPatches.RemoveHarmonyPatches();
        }

        [ModdedGamemodeJoin]
        private void RoomJoined()
        {
            Enabled = true;
        }

        [ModdedGamemodeLeave]
        private void RoomLeft()
        {
            Enabled = false;
        }
    }
}
