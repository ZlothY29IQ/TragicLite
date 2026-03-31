using System.IO;
using BepInEx;
using BepInEx.Configuration;

namespace TragicLite.Patches;

[BepInPlugin("org.Mangos.gorillatag.modmenupatch", "Mod Menu Patch", "1.0.2")]
public class ModMenuPatch : BaseUnityPlugin
{
    public static bool modmenupatch = true;

    public static ConfigEntry<float> multiplier;
    public static ConfigEntry<float> speedMultiplier;
    public static ConfigEntry<float> jumpMultiplier;
    public static ConfigEntry<bool>  randomColor;
    public static ConfigEntry<float> cycleSpeed;
    public static ConfigEntry<float> glowAmount;

    private void Start()
    {
        HarmonyPatches.ApplyHarmonyPatches();

        GorillaTagger.OnPlayerSpawned(() =>
                                      {
                                          NetworkSystem.Instance.OnJoinedRoomEvent        += RoomJoined;
                                          NetworkSystem.Instance.OnReturnedToSinglePlayer += RoomLeft;
                                      });

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

    private void RoomJoined()
    {
        modmenupatch = true;
    }

    private void RoomLeft()
    {
        modmenupatch = true;
    }
}