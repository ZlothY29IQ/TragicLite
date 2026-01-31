using System.ComponentModel;
using System.IO;
using BepInEx;
using BepInEx.Configuration;
using Utilla.Attributes;

namespace TragicLite.Patches;

[Description("HauntedModMenu")]
[BepInPlugin("org.Mangos.gorillatag.modmenupatch", "Mod Menu Patch", "1.0.2")]
[BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
[ModdedGamemode]
public class ModMenuPatch : BaseUnityPlugin
{
    public static bool modmenupatch = true;

    public static ConfigEntry<float> multiplier;
    public static ConfigEntry<float> speedMultiplier;
    public static ConfigEntry<float> jumpMultiplier;
    public static ConfigEntry<bool>  randomColor;
    public static ConfigEntry<float> cycleSpeed;
    public static ConfigEntry<float> glowAmount;

    private void OnEnable()
    {
        HarmonyPatches.ApplyHarmonyPatches();

        ConfigFile config = new(Path.Combine(Paths.ConfigPath, _10000._10001(49)), true);

        jumpMultiplier  = config.Bind(_10000._10001(33), _10000._10001(37), 1.5f,   _10000._10001(40));
        randomColor     = config.Bind(_10000._10001(3),  _10000._10001(7),  false,  _10000._10001(11));
        cycleSpeed      = config.Bind(_10000._10001(24), _10000._10001(28), 0.004f, _10000._10001(31));
        glowAmount      = config.Bind(_10000._10001(14), _10000._10001(17), 1f,     _10000._10001(20));
        speedMultiplier = config.Bind(_10000._10001(42), _10000._10001(45), 100f,   _10000._10001(47));
    }

    private void OnDisable()
    {
        HarmonyPatches.RemoveHarmonyPatches();
    }

    [ModdedGamemodeJoin]
    private void RoomJoined()
    {
        modmenupatch = true;
    }

    [ModdedGamemodeLeave]
    private void RoomLeft()
    {
        modmenupatch = true;
    }
}