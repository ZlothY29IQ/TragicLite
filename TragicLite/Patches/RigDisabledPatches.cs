using HarmonyLib;

namespace TragicLite.Patches;

public static class RigDisabledPatches
{
    [HarmonyPatch(typeof(VRRig), nameof(VRRig.OnDisable))]
    public static class RigDisablePatch
    {
        private static bool Prefix(VRRig __instance) =>
                !__instance.isLocal;
    }

    [HarmonyPatch(typeof(VRRig), nameof(VRRig.PostTick))]
    public static class RigPostTickPatch
    {
        private static bool Prefix(VRRig __instance) =>
                !__instance.isLocal || __instance.enabled;
    }
}