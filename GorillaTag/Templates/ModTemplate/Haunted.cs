using System.ComponentModel;

namespace Project
{
    [Description("HauntedModMenu")] 
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [ModdedGamemode]
    public class ConfigablePlugin : BaseUnityPlugin
    {
        public static bool allowed = false;
        public static ConfigEntry<float> multiplier;

        void OnEnable()
        {
            Patches.ApplyHarmonyPatches();

            var customFile = new ConfigFile(Path.Combine(Paths.ConfigPath, "SpaceMonke.cfg"), true);
            multiplier = customFile.Bind("Configuration", "JumpMultiplier", 10f, "How much to multiply the jump height/distance by. 10 = 10x higher jumps");
        }

        void OnDisable()
        {
            SpaceMonkePatches.RemoveHarmonyPatches();
        }

        [ModdedGamemodeJoin]
        private void RoomJoined()
		{
            allowed = true;
		}

        [ModdedGamemodeLeave]
        private void RoomLeft()
		{
            allowed = false;
		}
    }
}
