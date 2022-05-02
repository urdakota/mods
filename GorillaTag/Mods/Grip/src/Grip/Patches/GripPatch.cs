using BepInEx;
using System;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;
using System.Reflection;
using GorillaLocomotion;
using System.Collections.Generic;
using UnityEngine.XR;
using Photon.Pun;
using System.IO;

namespace Grip.Patches
{
    /// <summary>
    /// This is an example patch, made to demonstrate how to use Harmony. You should remove it if it is not used.
    /// </summary>
    [HarmonyPatch(typeof(GorillaLocomotion.Player))]
    [HarmonyPatch("LateUpdate", MethodType.Normal)]

    internal class GripPatch
    {
        private static float LValue;
        private static float RValue;
        public static bool leftR;
        public static bool RightR;
        private static Vector3 GripPos;

        private static void Postfix(GorillaLocomotion.Player __instance)
        {
            if (Grip.Enabled == true)
            {
                List<InputDevice> list = new List<InputDevice>();
                InputDevices.GetDevices(list);

                for (int i = 0; i < list.Count; i++) //Get input
                {
                    if (list[i].characteristics.HasFlag(InputDeviceCharacteristics.Left))
                    {
                        list[i].TryGetFeatureValue(CommonUsages.grip, out LValue);
                    }
                    if (list[i].characteristics.HasFlag(InputDeviceCharacteristics.Right))
                    {
                        list[i].TryGetFeatureValue(CommonUsages.grip, out RValue);
                    }
                }

                if (RValue > 0.5f)
                {
                    if (!RightR && Physics.CheckSphere(__instance.rightHandTransform.position, 0.0895f * Grip.Range.Value, 1 << 9))
                    {
                        GripPos = __instance.rightHandTransform.position;
                        leftR = false;
                        RightR = true;
                    }
                    if (RightR) ApplyVelocity(__instance.rightHandTransform.position, GripPos, __instance);
                }
                else if (LValue > 0.5f)
                {
                    if (!leftR && Physics.CheckSphere(__instance.leftHandTransform.position, 0.0895f * Grip.Range.Value, 1 << 9))
                    {
                        GripPos = __instance.leftHandTransform.position;
                        leftR = true;
                        RightR = false;
                    }
                    if (leftR) ApplyVelocity(__instance.leftHandTransform.position, GripPos, __instance);
                }
                else
                {
                    if (RValue < 0.5f || LValue < 0.5f)
                    {
                        leftR = false;
                        RightR = false;
                        __instance.bodyCollider.attachedRigidbody.useGravity = true;
                    }
                }
            } else
            {
                __instance.bodyCollider.attachedRigidbody.useGravity = true;
            }
        } 
        public static void ApplyVelocity(Vector3 pos, Vector3 target, GorillaLocomotion.Player __instance)
        {
            var dir = target - pos;
                __instance.bodyCollider.attachedRigidbody.useGravity = false;
                __instance.bodyCollider.attachedRigidbody.velocity = dir * 32f;
        }
    }
}
