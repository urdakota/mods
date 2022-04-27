using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Speed.Patches
{
    /// <summary>
    /// This is an example patch, made to demonstrate how to use Harmony. You should remove it if it is not used.
    /// </summary>
    [HarmonyPatch(typeof(GorillaLocomotion.Player))]
    [HarmonyPatch("LateUpdate", MethodType.Normal)]
    internal class SpeedPatch
    {
        private static void Prefix()
        {
            if (Speed.Enabled)
            {
                GorillaLocomotion.Player.Instance.maxJumpSpeed = 6.5f * (Speed.Multiplier.Value / 3 + 2f / 3f);
                GorillaLocomotion.Player.Instance.jumpMultiplier = 1.1f * (Speed.Multiplier.Value/3 + 2f/3f);
                GorillaLocomotion.Player.Instance.velocityLimit = 0.3f * (Speed.Multiplier.Value / 3 + 2f / 3f);
            }
            else
            {
                GorillaLocomotion.Player.Instance.jumpMultiplier = 1.1f;
                GorillaLocomotion.Player.Instance.maxJumpSpeed = 6.5f;
                GorillaLocomotion.Player.Instance.velocityLimit = 0.3f;
            }
        }
    }
}
