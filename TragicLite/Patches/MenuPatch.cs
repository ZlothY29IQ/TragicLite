using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExitGames.Client.Photon;
using GorillaLocomotion;
using GorillaNetworking;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace TragicLite.Patches;

[HarmonyPatch(typeof(GTPlayer), nameof(GTPlayer.LateUpdate))]
public class MenuPatch
{
    private static bool setTrue;

    private static bool DoOnce;

    private static bool antiRepeat;

    private static float redValue;

    private static float greenValue;

    private static float blueValue;

    private static GradientColorKey[] colorKeys;

    public static int bigmonkecooldown;

    public static bool ResetSpeed = false;

    private static readonly string[] Buttons =
    {
            _10000._10001(371),
            _10000._10001(374),
            _10000._10001(378),
            _10000._10001(382),
            _10000._10001(386),
            _10000._10001(390),
            _10000._10001(394),
            _10000._10001(396),
            _10000._10001(399),
            _10000._10001(402),
            _10000._10001(404),
            _10000._10001(406),
            _10000._10001(409),
            _10000._10001(411),
            _10000._10001(413),
            _10000._10001(415),
            _10000._10001(417),
            _10000._10001(421),
            _10000._10001(423),
            _10000._10001(427),
            _10000._10001(429),
            _10000._10001(432),
            _10000._10001(435),
            _10000._10001(439),
            _10000._10001(441),
            _10000._10001(443),
            _10000._10001(445),
            _10000._10001(447),
    };

    private static readonly bool[] ButtonsActive =
    [
            false, false, false, false, false, false, false, false, false, false,
            false, false, false, false, false, false, false, false, false, false,
            false, false, false, false, false, false, false, false, false, false,
            false, false, false, false, false, false, false, false, false, false,
            false, false, false, false, false, false, false, false, false, false,
            false, false, false, false, false, false, false, false, false, false,
            false, false, false, false, false, false, false, false, false, false,
            false, false, false, false, false, false, false, false, false, false,
            false, false, false, false, false, false, false, false, false, false,
            false, false, false, false, false, false, false, false, false, false,
    ];

    private static bool gripDown;

    private static bool primaryRightDown;

    private static GameObject menu;

    private static GameObject canvasObj;

    private static GameObject reference;

    public static int FramePressCooldown = 0;

    private static GameObject pointer;

    private static bool gravityToggled;

    private static bool flying;

    private static int btnCooldown;

    private static int soundCooldown;

    private static float? maxJumpSpeed;

    private static float? jumpMultiplier;

    private static object index;

    public static int BlueMaterial = 5;

    public static string PreviousNotifi;

    public static int TransparentMaterial = 6;

    public static int LavaMaterial = 2;

    public static int RockMaterial = 1;

    public static int DefaultMaterial = 5;

    public static int NeonRed = 3;

    public static int RedTransparent = 4;

    public static int self = 0;

    private static Vector3? leftHandOffsetInitial;

    private static Vector3? rightHandOffsetInitial;

    private static float? maxArmLengthInitial;

    private static bool noClipDisabledOneshot = false;

    private static bool noClipEnabledAtLeastOnce = false;

    private static Vector3 head_direction;

    private static Vector3 roll_direction;

    private static Vector2 left_joystick;

    private static float acceleration;

    private static float maxs;

    private static float distance;

    private static float multiplier;

    private static float speed;

    private static bool Start;

    private static bool ghostToggle = false;

    private static bool bigMonkeyEnabled;

    private static bool bigMonkeAntiRepeat = false;

    private static int bigMonkeCooldown;

    private static bool ghostMonkeEnabled = false;

    private static bool ghostMonkeAntiRepeat = false;

    private static int ghostMonkeCooldown;

    private static bool checkedProps = false;

    private static bool teleportGunAntiRepeat;

    private static Color colorRgbMonke = new(0f, 0f, 0f);

    private static float hueRgbMonke = 0f;

    private static float timerRgbMonke = 0f;

    private static float updateRateRgbMonke = 0f;

    private static float updateTimerRgbMonke = 0f;

    private static bool flag2;

    private static bool flag1 = true;

    private static readonly Vector3 scale = new(0.0125f, 0.28f, 0.3825f);

    private static bool gripDown_left;

    private static bool gripDown_right;

    private static bool once_left;

    private static bool once_right;

    private static bool once_left_false;

    private static bool once_right_false;

    private static bool once_networking;

    private static readonly GameObject[] jump_left_network = new GameObject[9999];

    private static readonly GameObject[] jump_right_network = new GameObject[9999];

    private static GameObject jump_left_local;

    private static GameObject jump_right_local;

    private static readonly GradientColorKey[] colorKeysPlatformMonke = new GradientColorKey[4];

    private static Vector3? checkpointPos;

    private static bool checkpointTeleportAntiRepeat;

    private static bool foundPlayer;

    private static int btnTagSoundCooldown;

    private static float timeSinceLastChange;

    private static float myVarY1;

    private static float myVarY2;

    private static bool gain;

    private static bool less;

    private static GameObject C4;

    private static bool spawned;

    private static float SpawnGrip;

    private static float BoomGrip;

    private static float rSpawnGrip;

    private static float rBoomGrip;

    private static bool reset;

    private static bool fastr;

    private static Color color;

    private static bool speed1 = true;

    private static readonly float gainSpeed = 1f;

    private static float bigScale = 2f;

    private static readonly int pageSize = 4;

    private static int pageNumber;

    private static float updateRate;

    private static float updateTimer;

    private static float timer;

    private static float hue;

    private static int layers;

    private static bool up;

    private static bool down;

    private readonly Material AlertText = new(Shader.Find(_10000._10001(367)));

    private readonly int NotificationDecayTime = 150;

    private bool HasInit;

    private GameObject HUDObj;

    private GameObject HUDObj2;

    private GameObject MainCamera;

    private string newtext;

    private int NotificationDecayTimeCounter;

    private string[] Notifilines;

    private Text Testtext;

    private static void Prefix()
    {
        try
        {
            if (!maxJumpSpeed.HasValue)
                maxJumpSpeed = GTPlayer.Instance.maxJumpSpeed;

            if (!jumpMultiplier.HasValue)
                jumpMultiplier = GTPlayer.Instance.jumpMultiplier;

            if (!maxArmLengthInitial.HasValue)
            {
                maxArmLengthInitial    = GTPlayer.Instance.maxArmLength;
                leftHandOffsetInitial  = GTPlayer.Instance.LeftHand.handOffset;
                rightHandOffsetInitial = GTPlayer.Instance.RightHand.handOffset;
            }

            List<InputDevice> left  = new();
            List<InputDevice> right = new();

            InputDevices.GetDevicesWithCharacteristics((InputDeviceCharacteristics)324, left);
            InputDevices.GetDevicesWithCharacteristics((InputDeviceCharacteristics)580, right);

            InputDevice dev;

            dev = left[0];
            dev.TryGetFeatureValue(CommonUsages.grip,            out BoomGrip);
            dev.TryGetFeatureValue(CommonUsages.trigger,         out SpawnGrip);

            dev = right[0];
            dev.TryGetFeatureValue(CommonUsages.primaryButton, out primaryRightDown);

            bool gripDown = ControllerInputPoller.instance.leftGrab;

            if (gripDown && menu == null)
            {
                Draw();

                if (reference == null)
                {
                    reference = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    Object.Destroy(reference.GetComponent<MeshRenderer>());
                    reference.transform.SetParent(GTPlayer.Instance.RightHand.controllerTransform);
                    reference.transform.localPosition = new Vector3(0f, -0.1f, 0f);
                    reference.transform.localScale    = Vector3.one * 0.01f;
                }
            }
            else if (!gripDown && menu != null)
            {
                Object.Destroy(menu);
                menu = null;
                Object.Destroy(reference);
                reference = null;
            }

            if (gripDown && menu != null)
            {
                menu.transform.position = GTPlayer.Instance.LeftHand.controllerTransform.position;
                menu.transform.rotation = GTPlayer.Instance.LeftHand.controllerTransform.rotation;
            }

            if (ButtonsActive[0]) PhotonNetwork.Disconnect();
            if (ButtonsActive[1]) PhotonNetwork.JoinRandomRoom();
            if (ButtonsActive[2]) Application.Quit();
            if (ButtonsActive[3]) ProcessPlatformMonke();

            void Fly(float speed)
            {
                bool a = ControllerInputPoller.instance.leftControllerPrimaryButton;
                bool b = ControllerInputPoller.instance.leftControllerSecondaryButton;

                if (a)
                {
                    GTPlayer.Instance.transform.position +=
                            GTPlayer.Instance.headCollider.transform.forward * Time.deltaTime * speed;

                    GorillaTagger.Instance.rigidbody.linearVelocity = Vector3.zero;
                    flying                                          = true;
                }
                else if (flying)
                {
                    GorillaTagger.Instance.rigidbody.linearVelocity =
                            GTPlayer.Instance.headCollider.transform.forward * Time.deltaTime * speed;

                    flying = false;
                }

                if (b)
                {
                    Rigidbody rb = GTPlayer.Instance.bodyCollider.attachedRigidbody;
                    if (!gravityToggled)
                    {
                        rb.useGravity  = !rb.useGravity;
                        gravityToggled = true;
                    }
                }
                else
                {
                    gravityToggled = false;
                }
            }

            if (ButtonsActive[4]) Fly(15f);
            if (ButtonsActive[22]) Fly(10f);
            if (ButtonsActive[23]) Fly(100f);
            if (ButtonsActive[24]) Fly(500f);

            if (ButtonsActive[5]) ProcessNoClip();
            if (ButtonsActive[6]) ProcessTeleportGun();
            if (ButtonsActive[7]) beacons();
            if (ButtonsActive[9]) ProcessGhostMonke();
            if (ButtonsActive[10]) ProcessInvisibility();
            if (ButtonsActive[11]) triggerlagall();
            if (ButtonsActive[12]) ProcessInvisPlatformMonke();
            if (ButtonsActive[14]) ProcessRandomSound();
            if (ButtonsActive[16])
            {
                BOXBOXESP();
                tracers();
            }

            if (ButtonsActive[17]) starttaggame();
            if (ButtonsActive[19]) gungpt();
            if (ButtonsActive[26]) Soundspam();
            if (ButtonsActive[27]) ElfSpammer();

            GTPlayer.Instance.transform.localScale =
                    ButtonsActive[13] || ButtonsActive[18] ? Vector3.one * 0.5f :
                    ButtonsActive[25]                      ? Vector3.one * 0.1f :
                                                             Vector3.one;

            if (ButtonsActive[15])
            {
                GTPlayer.Instance.maxJumpSpeed   = ModMenuPatch.speedMultiplier.Value;
                GTPlayer.Instance.jumpMultiplier = ModMenuPatch.jumpMultiplier.Value;
            }
            else
            {
                GTPlayer.Instance.maxJumpSpeed   = maxJumpSpeed.Value;
                GTPlayer.Instance.jumpMultiplier = 1.05f;
            }

            if (ButtonsActive[20])
            {
                VRRig rig = PhotonNetwork.InRoom
                                    ? VRRig.LocalRig
                                    : GorillaTagger.Instance.offlineVRRig;

                rig.head.trackingRotationOffset.y += 15f;
            }
            else
            {
                VRRig rig = PhotonNetwork.InRoom
                                    ? VRRig.LocalRig
                                    : GorillaTagger.Instance.offlineVRRig;

                rig.head.trackingRotationOffset.y = 0f;
            }

            if (ButtonsActive[21])
            {
                VRRig rig = PhotonNetwork.InRoom
                                    ? VRRig.LocalRig
                                    : GorillaTagger.Instance.offlineVRRig;

                rig.head.rigTarget.eulerAngles      = Random.insideUnitSphere * 360f;
                rig.leftHand.rigTarget.eulerAngles  = Random.insideUnitSphere * 360f;
                rig.rightHand.rigTarget.eulerAngles = Random.insideUnitSphere * 360f;
            }

            if (btnCooldown > 0 && Time.frameCount > btnCooldown)
            {
                btnCooldown      = 0;
                ButtonsActive[7] = false;
                Object.Destroy(menu);
                menu = null;
                Draw();
            }

            if (soundCooldown > 0 && Time.frameCount > soundCooldown)
            {
                soundCooldown     = 0;
                ButtonsActive[14] = false;
                Object.Destroy(menu);
                menu = null;
                Draw();
            }

            if (btnTagSoundCooldown > 0 && Time.frameCount > btnTagSoundCooldown)
                btnTagSoundCooldown = 0;

            if (bigMonkeCooldown > 0 && Time.frameCount > bigMonkeCooldown)
                bigMonkeCooldown = 0;

            if (ghostMonkeCooldown > 0 && Time.frameCount > ghostMonkeCooldown)
                ghostMonkeCooldown = 0;
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message + " | " + ex.StackTrace);
        }
    }

    private static void ElfSpammer()
    {
        //do nothing
    }

    private static void Soundspam()
    {
        //why tf did this give you master :sob:
    }

    private static void starttaggame()
    {
        //do nothing
    }

    private static void gungpt()
    {
        undmaster();

        List<InputDevice> devices = new();
        InputDevices.GetDevicesWithCharacteristics(
                InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller, devices);

        InputDevice right = devices[0];

        bool triggerPressed = false;
        bool gripPressed    = false;

        right.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed);
        right.TryGetFeatureValue(CommonUsages.gripButton,    out gripPressed);

        Physics.Raycast(
                GTPlayer.Instance.RightHand.controllerTransform.position -
                GTPlayer.Instance.RightHand.controllerTransform.up,
                -GTPlayer.Instance.RightHand.controllerTransform.up,
                out RaycastHit hit);

        Player[] players = PhotonNetwork.PlayerList;

        foreach (Player p in players)
        {
            VRRig rig = GorillaGameManager.instance.FindPlayerVRRig(p);

            if (rig == null) continue;

            Object.Destroy(pointer);

            bool localTrigger = false;
            right.TryGetFeatureValue(CommonUsages.triggerButton, out localTrigger);

            if (localTrigger)
            {
                //rig.RPC(_10000._10001(93), RpcTarget.All, 69, false, 100f);
            }
        }

        antiRepeat = false;
        pointer    = null;
        antiRepeat = true;
    }

    private static void endbattle()
    {
        //do nothing
    }

    private static void randomizeteams()
    {
        //do nothing
    }

    private static void starthunt()
    {
        //do nothing
    }

    private static void endhunt()
    {
        //do nothing
    }

    private static void startbattle()
    {
        //do nothing
    }

    private static void endinfection()
    {
        //do nothing
    }

    private static void reviveall()
    {
        //do nothing
    }

    private static void killall()
    {
        //do nothing
    }

    private static void untagandtagspammer()
    {
        //do nothing
    }

    private static GorillaTagger GetGorillaTaggerInstance() => GorillaTagger.Instance;

    private static void giveslingshot()
    {
        if (DoOnce || antiRepeat)
            return;

        bool                             apply        = true;
        VRRig                            offlineVRRig = GetGorillaTaggerInstance().offlineVRRig;
        CosmeticsController              controller   = CosmeticsController.instance;
        CosmeticsController.CosmeticItem item         = controller.GetItemFromDict(_10000._10001(107));

        controller.ApplyCosmeticItemToSet(offlineVRRig.cosmeticSet, item, apply, apply);

        DoOnce     = true;
        antiRepeat = true;
    }

    public static VRRig FindVRRigForPlayer(Player player)
    {
        foreach (VRRig rig in GorillaParent.instance.vrrigs)
            if (!rig.isOfflineVRRig && rig.GetComponent<PhotonView>().Owner == player)
                return rig;

        return null;
    }

    private static void HuntwatchAll()
    {
        VRRig rig = FindVRRigForPlayer(PhotonNetwork.LocalPlayer);

        if (rig == null) return;

        Transform camTransform = rig.mainCamera.transform;
        PhotonNetwork.Instantiate(_10000._10001(112), camTransform.position, Quaternion.identity);
        undmaster();
    }

    private static void SlingshotAll()
    {
        VRRig rig = FindVRRigForPlayer(PhotonNetwork.LocalPlayer);

        if (rig == null) return;

        Vector3 camPos = rig.mainCamera.transform.position;
        PhotonNetwork.Instantiate(_10000._10001(115), camPos, Quaternion.identity);
        undmaster();
    }

    private static void matallbattle()
    {
        //do nothing
    }

    private static void matallhunt()
    {
        //do nothing
    }

    private static void BOXBOXESP()
    {
        Material material = new(Shader.Find(_10000._10001(122))) { color = new Color(1f, 0f, 0f, 0.3f), };
        VRRig[]  rigs     = Object.FindObjectsOfType<VRRig>();

        foreach (VRRig rig in rigs)
        {
            if (rig.isOfflineVRRig || rig.isMyPlayer || rig.isLocal)
                continue;

            void CreateMarker(Transform target, Vector3 scale, PrimitiveType type = PrimitiveType.Cube)
            {
                GameObject obj = GameObject.CreatePrimitive(type);
                Object.Destroy(obj.GetComponent<Collider>());
                Object.Destroy(obj.GetComponent<Rigidbody>());
                obj.transform.position                    = target.position;
                obj.transform.localScale                  = scale;
                obj.transform.rotation                    = Quaternion.identity;
                obj.GetComponent<MeshRenderer>().material = material;
                Object.Destroy(obj, Time.deltaTime);
            }

            CreateMarker(rig.rightHand.rigTarget, new Vector3(0.07f, 0.08f, 0.07f));
            CreateMarker(rig.leftHand.rigTarget,  new Vector3(0.07f, 0.08f, 0.07f));
            CreateMarker(rig.head.rigTarget,      new Vector3(0.3f,  0.6f,  0.3f), PrimitiveType.Cylinder);
        }
    }

    private static void invisall()
    {
        //do nothing 
    }

    private static void TagAll1()
    {
        /*GorillaTagManager[] tagManagers = Object.FindObjectsOfType<GorillaTagManager>();
        Player[]            players     = PhotonNetwork.PlayerList;

        foreach (GorillaTagManager manager in tagManagers)
        {
            if (!manager.photonView.IsMine)
                manager.photonView.RequestOwnership();

            foreach (Player player in players)
                manager.AddInfectedPlayer(player);
        }*/
    }

    private static void untagall()
    {
        /*GorillaTagManager[] tagManagers = Object.FindObjectsOfType<GorillaTagManager>();
        Player[]            players     = PhotonNetwork.PlayerList;

        foreach (GorillaTagManager manager in tagManagers)
        {
            bool notMine = !manager.photonView.IsMine;
            if (notMine)
                manager.photonView.RequestOwnership();

            foreach (Player player in players)
                if (manager.currentInfected.Contains(player))
                    manager.currentInfected.Remove(player);
        }*/
    }

    private static void tagurself()
    {
        /*GorillaTagManager[] tagManagers = Object.FindObjectsOfType<GorillaTagManager>();

        foreach (GorillaTagManager manager in tagManagers)
        {
            bool notMine = !manager.photonView.IsMine;
            if (notMine)
                manager.photonView.RequestOwnership();

            if (!manager.photonView.IsMine)
                continue;

            manager.currentInfected.Add(PhotonNetwork.LocalPlayer);
        }
        */
    }

    private static void untagurself()
    {
        /*GorillaTagManager[] tagManagers = Object.FindObjectsOfType<GorillaTagManager>();

        foreach (GorillaTagManager manager in tagManagers)
        {
            bool notMine = !manager.photonView.IsMine;
            if (notMine)
                manager.photonView.RequestOwnership();

            if (!manager.photonView.IsMine)
                continue;

            manager.currentInfected.Remove(PhotonNetwork.LocalPlayer);
        }
        */
    }

    private static void SoundSpam()
    {
        //do nothing
    }

    private static void ChangeGamemodeToInfection()
    {
        //do nothing
    }

    private static void ChangeGamemodeToHunt()
    {
        //do nothing
    }

    private static void ChangeGamemodeToCasual()
    {
        //do nothing
    }

    private static void ChangeGamemodeToBattle()
    {
        //do nothing
    }

    private static VRRig[] GetAllVRRigs() => Object.FindObjectsOfType<VRRig>();

    private static void BypassName(string name)
    {
        string lowerName = name.ToLower();
        PhotonNetwork.LocalPlayer.NickName = lowerName;
        PlayerPrefs.SetString("OfflineName", lowerName);
        PlayerPrefs.Save();
        VRRig.LocalRig.playerText1.text = lowerName;
    }

    private static void ProcessStrobe()
    {
        redValue   = PlayerPrefs.GetFloat("RedValue",   0f);
        greenValue = PlayerPrefs.GetFloat("GreenValue", 0f);
        blueValue  = PlayerPrefs.GetFloat("BlueValue",  0f);

        if (Time.time > timer)
        {
            color = Random.ColorHSV();
            PlayerPrefs.SetFloat("RedValue",   color.r);
            PlayerPrefs.SetFloat("GreenValue", color.g);
            PlayerPrefs.SetFloat("BlueValue",  color.b);
            PlayerPrefs.Save();

            redValue   = color.r;
            greenValue = color.g;
            blueValue  = color.b;

            GorillaTagger.Instance.UpdateColor(redValue, greenValue, blueValue);

            timer = Time.time + 0.344f;

            GorillaComputer.instance.friendJoinCollider.playerIDsCurrentlyTouching
                           .Add(PhotonNetwork.LocalPlayer.UserId);

            GorillaComputer.instance.friendJoinCollider.transform.position =
                    GTPlayer.Instance.headCollider.transform.position;

            //implement the new method for this vvv

            //GorillaTagger.Instance.myVRRig.photonView.RPC("UpdateColorRPC", RpcTarget.All, redValue, greenValue,
            //blueValue, true);
        }
    }

    private static void ProcessBigMonke(bool enable)
    {
        List<InputDevice> devices = new();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand, devices);

        bool triggerPressed = false;
        if (devices.Count > 0)
            devices[0].TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed);

        if (triggerPressed && bigMonkeyEnabled || !enable)
        {
            GTPlayer.Instance.transform.localScale = Vector3.one;
            bigMonkeyEnabled                       = false;

            return;
        }

        if (!bigMonkeyEnabled)
        {
            GTPlayer.Instance.transform.localScale = new Vector3(2f, 2f, 2f);
            bigMonkeyEnabled                       = true;
        }

        bigMonkeCooldown = Time.frameCount + 30;
    }

    private static void ProcessGhostMonke()
    {
        List<InputDevice> devices = new();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand, devices);

        bool triggerPressed = false;
        if (devices.Count > 0)
            devices[0].TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed);

        VRRig.LocalRig.enabled = !triggerPressed;
    }

    private static void ProcessInvisibility()
    {
        List<InputDevice> devices = new();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand, devices);

        bool triggerPressed = false;
        if (devices.Count > 0)
            devices[0].TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed);

        if (!triggerPressed)
        {
            VRRig.LocalRig.enabled            = true;
            VRRig.LocalRig.transform.position = new Vector3(100f, 100f, 100f);
        }
        else
        {
            VRRig.LocalRig.enabled = false;
        }
    }

    private static void TagAll()
    {
        //do nothing
    }

    private static void ProcessTriggerPlatformMonke()
    {
        List<InputDevice> leftDevices = new();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand, leftDevices);

        List<InputDevice> rightDevices = new();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand, rightDevices);

        if (leftDevices.Count > 0)
            leftDevices[0].TryGetFeatureValue(CommonUsages.triggerButton, out gripDown_left);

        if (rightDevices.Count > 0)
            rightDevices[0].TryGetFeatureValue(CommonUsages.triggerButton, out gripDown_right);

        if (gripDown_right && !once_right && jump_right_local == null)
        {
            jump_right_local                                         = GameObject.CreatePrimitive(PrimitiveType.Cube);
            jump_right_local.GetComponent<Renderer>().material.color = Color.black;
            jump_right_local.transform.localScale                    = scale;
            jump_right_local.transform.position = GTPlayer.Instance.RightHand.controllerTransform.position +
                                                  new Vector3(0f, -0.0075f, 0f);

            jump_right_local.transform.rotation = GTPlayer.Instance.RightHand.controllerTransform.rotation;

            RaiseEventOptions options = new() { Receivers = ReceiverGroup.All, };
            PhotonNetwork.RaiseEvent(70,
                    new object[] { jump_right_local.transform.position, jump_right_local.transform.rotation, }, options,
                    SendOptions.SendReliable);

            once_right       = true;
            once_right_false = false;

            ColorChanger colorChanger = jump_right_local.AddComponent<ColorChanger>();
            colorChanger.colors = new Gradient { colorKeys = colorKeys, };
            colorChanger.Start();
        }
        else if (!gripDown_right && jump_right_local != null)
        {
            Object.Destroy(jump_right_local);
            jump_right_local = null;
            once_right       = false;
            once_right_false = true;

            RaiseEventOptions options = new() { Receivers = ReceiverGroup.All, };
            PhotonNetwork.RaiseEvent(72, null, options, SendOptions.SendReliable);
        }

        if (gripDown_left && !once_left && jump_left_local == null)
        {
            jump_left_local = GameObject.CreatePrimitive(PrimitiveType.Cube);
            jump_left_local.GetComponent<Renderer>().material.color = Color.black;
            jump_left_local.transform.localScale = scale;
            jump_left_local.transform.position = GTPlayer.Instance.LeftHand.controllerTransform.position;
            jump_left_local.transform.rotation = GTPlayer.Instance.LeftHand.controllerTransform.rotation;

            RaiseEventOptions options = new() { Receivers = ReceiverGroup.All, };
            PhotonNetwork.RaiseEvent(69,
                    new object[] { jump_left_local.transform.position, jump_left_local.transform.rotation, }, options,
                    SendOptions.SendReliable);

            once_left       = true;
            once_left_false = false;

            ColorChanger colorChanger = jump_left_local.AddComponent<ColorChanger>();
            colorChanger.colors = new Gradient { colorKeys = colorKeys, };
            colorChanger.Start();
        }
        else if (!gripDown_left && jump_left_local != null)
        {
            Object.Destroy(jump_left_local);
            jump_left_local = null;
            once_left       = false;
            once_left_false = true;

            RaiseEventOptions options = new() { Receivers = ReceiverGroup.All, };
            PhotonNetwork.RaiseEvent(71, null, options, SendOptions.SendReliable);
        }
    }

    private static void ProcessTeleportGun()
    {
        List<InputDevice> devices = new();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand, devices);

        bool gripPressed = false;
        if (devices.Count > 0)
            devices[0].TryGetFeatureValue(CommonUsages.gripButton, out gripPressed);

        if (!gripPressed)
        {
            teleportGunAntiRepeat = false;
            if (pointer != null)
            {
                Object.Destroy(pointer);
                pointer = null;
            }

            return;
        }

        if (!teleportGunAntiRepeat)
        {
            pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Object.Destroy(pointer.GetComponent<Rigidbody>());
            pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            teleportGunAntiRepeat        = true;
        }

        RaycastHit hit;
        if (Physics.Raycast(GTPlayer.Instance.RightHand.controllerTransform.position - Vector3.up, -Vector3.up,
                    out hit))
        {
            GTPlayer.Instance.transform.position = hit.point;
            pointer.transform.position           = hit.point;
        }
    }

    private static void ProcessRGB()
    {
        updateTimer += Time.deltaTime;

        bool random = ModMenuPatch.randomColor.Value;

        if (random)
        {
            if (Time.time > timer)
            {
                color = Random.ColorHSV(
                        0f, 1f,
                        ModMenuPatch.glowAmount.Value,
                        ModMenuPatch.glowAmount.Value,
                        ModMenuPatch.glowAmount.Value,
                        ModMenuPatch.glowAmount.Value);

                timer       = Time.time + ModMenuPatch.cycleSpeed.Value;
                updateTimer = 999f;
            }
        }
        else
        {
            hue += ModMenuPatch.cycleSpeed.Value;

            if (hue >= 1f)
                hue = 0f;

            color = Color.HSVToRGB(
                    hue,
                    ModMenuPatch.glowAmount.Value,
                    ModMenuPatch.glowAmount.Value);
        }

        if (updateTimer > updateRate)
        {
            //implement the new way to do this

            /*GorillaTagger.Instance.UpdateColor(color.r, color.g, color.b);
            GorillaTagger.Instance.myVRRig.photonView.RPC(
                    _10000._10001(212),
                    RpcTarget.All,
                    color.r, color.g, color.b);*/
        }
    }

    private static void ProcessNoClip()
    {
        List<InputDevice> devices = new();
        InputDevices.GetDevicesWithCharacteristics((InputDeviceCharacteristics)324, devices);

        if (devices.Count == 0)
            return;

        InputDevice device = devices[0];

        bool trigger = false;
        device.TryGetFeatureValue(CommonUsages.triggerButton, out trigger);

        if (trigger && !flag2)
        {
            foreach (MeshCollider col in Resources.FindObjectsOfTypeAll<MeshCollider>())
                col.transform.localScale /= 10000f;

            flag2 = true;
        }
        else if (!trigger && flag2)
        {
            foreach (MeshCollider col in Resources.FindObjectsOfTypeAll<MeshCollider>())
                col.transform.localScale *= 10000f;

            flag2 = false;
        }
    }

    private static void ProcessInvisPlatformMonke()
    {
        List<InputDevice> devices = new();
        InputDevices.GetDevicesWithCharacteristics((InputDeviceCharacteristics)580, devices);

        if (devices.Count == 0)
            return;

        InputDevice device = devices[0];

        device.TryGetFeatureValue(CommonUsages.gripButton, out gripDown_right);

        if (gripDown_right)
        {
            if (!once_right && jump_right_local == null)
            {
                jump_right_local                                  = GameObject.CreatePrimitive(PrimitiveType.Cube);
                jump_right_local.GetComponent<Renderer>().enabled = false;
                jump_right_local.transform.localScale             = scale;
                jump_right_local.transform.position =
                        GTPlayer.Instance.RightHand.controllerTransform.position + new Vector3(0f, -0.0075f, 0f);

                jump_right_local.transform.rotation =
                        GTPlayer.Instance.RightHand.controllerTransform.rotation;

                once_right       = true;
                once_right_false = false;
            }
        }
        else if (!once_right_false && jump_right_local != null)
        {
            Object.Destroy(jump_right_local);
            jump_right_local = null;
            once_right       = false;
            once_right_false = true;
        }

        if (!PhotonNetwork.InRoom)
        {
            for (int i = 0; i < jump_right_network.Length; i++)
                Object.Destroy(jump_right_network[i]);

            for (int i = 0; i < jump_left_network.Length; i++)
                Object.Destroy(jump_left_network[i]);
        }
    }

    private static void AntiBan2()
    {
        //do nothing
    }

    private static void undmaster()
    {
        //do nothing
    }

    private static void ProcessPlatformMonke()
    {
        colorKeysPlatformMonke[0].color = new Color32(0, 0, 0, 1);
        colorKeysPlatformMonke[0].time  = 0f;

        colorKeysPlatformMonke[1].color = new Color32(80, 12, 130, 1);
        colorKeysPlatformMonke[1].time  = 0.4f;

        colorKeysPlatformMonke[2].color = new Color32(114, 39, 167, 1);
        colorKeysPlatformMonke[2].time  = 0.8f;

        colorKeysPlatformMonke[3].color = new Color32(0, 0, 0, 1);
        colorKeysPlatformMonke[3].time  = 1f;

        if (!once_networking)
        {
            PhotonNetwork.NetworkingClient.EventReceived += PlatformNetwork;
            once_networking                              =  true;
        }

        List<InputDevice> leftDevices = new();
        InputDevices.GetDevicesWithCharacteristics(
                InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller, leftDevices);

        if (leftDevices.Count > 0)
            leftDevices[0].TryGetFeatureValue(CommonUsages.gripButton, out gripDown_left);

        if (gripDown_left && !once_left && jump_left_local == null)
        {
            jump_left_local = GameObject.CreatePrimitive(PrimitiveType.Cube);
            jump_left_local.GetComponent<Renderer>().material.SetColor(_10000._10001(234), Color.black);
            jump_left_local.transform.localScale = scale;
            jump_left_local.transform.position   = GTPlayer.Instance.LeftHand.controllerTransform.position;
            jump_left_local.transform.rotation   = GTPlayer.Instance.LeftHand.controllerTransform.rotation;

            RaiseEventOptions opts = new() { Receivers = ReceiverGroup.Others, };
            PhotonNetwork.RaiseEvent(69,
                    new object[]
                    {
                            GTPlayer.Instance.LeftHand.controllerTransform.position,
                            GTPlayer.Instance.LeftHand.controllerTransform.rotation,
                    }, opts, SendOptions.SendReliable);

            once_left       = true;
            once_left_false = false;

            ColorChanger cc = jump_left_local.AddComponent<ColorChanger>();
            cc.colors = new Gradient { colorKeys = colorKeysPlatformMonke, };
            cc.Start();
        }
        else if (!gripDown_left && !once_left_false && jump_left_local != null)
        {
            Object.Destroy(jump_left_local);
            jump_left_local = null;
            once_left       = false;
            once_left_false = true;

            RaiseEventOptions opts = new() { Receivers = ReceiverGroup.Others, };
            PhotonNetwork.RaiseEvent(71, null, opts, SendOptions.SendReliable);
        }

        List<InputDevice> rightDevices = new();
        InputDevices.GetDevicesWithCharacteristics(
                InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller, rightDevices);

        if (rightDevices.Count > 0)
            rightDevices[0].TryGetFeatureValue(CommonUsages.gripButton, out gripDown_right);

        if (gripDown_right && !once_right && jump_right_local == null)
        {
            jump_right_local = GameObject.CreatePrimitive(PrimitiveType.Cube);
            jump_right_local.GetComponent<Renderer>().material.SetColor(_10000._10001(230), Color.black);
            jump_right_local.transform.localScale = scale;
            jump_right_local.transform.position = GTPlayer.Instance.RightHand.controllerTransform.position +
                                                  new Vector3(0f, -0.0075f, 0f);

            jump_right_local.transform.rotation = GTPlayer.Instance.RightHand.controllerTransform.rotation;

            RaiseEventOptions opts = new() { Receivers = ReceiverGroup.Others, };
            PhotonNetwork.RaiseEvent(70,
                    new object[] { jump_right_local.transform.position, jump_right_local.transform.rotation, }, opts,
                    SendOptions.SendReliable);

            once_right       = true;
            once_right_false = false;

            ColorChanger cc = jump_right_local.AddComponent<ColorChanger>();
            cc.colors = new Gradient { colorKeys = colorKeysPlatformMonke, };
            cc.Start();
        }
        else if (!gripDown_right && !once_right_false && jump_right_local != null)
        {
            Object.Destroy(jump_right_local);
            jump_right_local = null;
            once_right       = false;
            once_right_false = true;

            RaiseEventOptions opts = new() { Receivers = ReceiverGroup.Others, };
            PhotonNetwork.RaiseEvent(72, null, opts, SendOptions.SendReliable);
        }

        if (!PhotonNetwork.InRoom)
        {
            foreach (GameObject obj in jump_right_network)
                Object.Destroy(obj);

            foreach (GameObject obj in jump_left_network)
                Object.Destroy(obj);
        }
    }

    private static void PlatformNetwork(EventData eventData)
    {
        byte code = eventData.Code;

        if (code == 69)
        {
            if (jump_left_network[eventData.Sender] != null)
                return;

            object[] data = (object[])eventData.CustomData;

            jump_left_network[eventData.Sender] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            jump_left_network[eventData.Sender].GetComponent<Renderer>().material
                                               .SetColor(_10000._10001(243), Color.black);

            jump_left_network[eventData.Sender].transform.localScale = scale;
            jump_left_network[eventData.Sender].transform.position   = (Vector3)data[0];
            jump_left_network[eventData.Sender].transform.rotation   = (Quaternion)data[1];

            ColorChanger cc = jump_left_network[eventData.Sender].AddComponent<ColorChanger>();
            cc.colors = new Gradient { colorKeys = colorKeysPlatformMonke, };
            cc.Start();
        }
        else if (code == 70)
        {
            if (jump_left_network[eventData.Sender] != null)
            {
                Object.Destroy(jump_left_network[eventData.Sender]);
                jump_left_network[eventData.Sender] = null;
            }
        }
        else if (code == 71)
        {
            if (jump_right_network[eventData.Sender] != null)
                return;

            object[] data = (object[])eventData.CustomData;

            jump_right_network[eventData.Sender] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            jump_right_network[eventData.Sender].GetComponent<Renderer>().material
                                                .SetColor(_10000._10001(239), Color.black);

            jump_right_network[eventData.Sender].transform.localScale = scale;
            jump_right_network[eventData.Sender].transform.position   = (Vector3)data[0];
            jump_right_network[eventData.Sender].transform.rotation   = (Quaternion)data[1];

            ColorChanger cc = jump_right_network[eventData.Sender].AddComponent<ColorChanger>();
            cc.colors = new Gradient { colorKeys = colorKeysPlatformMonke, };
            cc.Start();
        }
        else if (code == 72)
        {
            if (jump_right_network[eventData.Sender] != null)
            {
                Object.Destroy(jump_right_network[eventData.Sender]);
                jump_right_network[eventData.Sender] = null;
            }
        }
    }

    private static void ProcessCheckpointTeleport()
    {
        List<InputDevice> devices = new();
        InputDevices.GetDevices(devices);
        InputDevices.GetDevicesWithCharacteristics((InputDeviceCharacteristics)324, devices);

        if (devices.Count == 0)
        {
            checkpointTeleportAntiRepeat = false;

            return;
        }

        InputDevice device = devices[0];

        bool trigger = false;
        bool grip    = false;

        device.TryGetFeatureValue(CommonUsages.triggerButton, out trigger);
        device.TryGetFeatureValue(CommonUsages.gripButton,    out grip);

        if (trigger)
            checkpointPos = GTPlayer.Instance.transform.position;

        if (grip && checkpointPos.HasValue)
        {
            if (!checkpointTeleportAntiRepeat)
            {
                GTPlayer.Instance.transform.position            = checkpointPos.Value;
                GorillaTagger.Instance.rigidbody.linearVelocity = Vector3.zero;
                checkpointTeleportAntiRepeat                    = true;
            }
        }
        else
        {
            checkpointTeleportAntiRepeat = false;
        }
    }

    private static void TeleportToRandomPlayer()
    {
        if (foundPlayer)
            return;

        Player[] players = PhotonNetwork.PlayerList;

        if (players.Length == 0)
            return;

        System.Random random = new();
        int           index  = random.Next(players.Length);

        VRRig rig = GorillaGameManager.instance.FindPlayerVRRig(players[index]);

        if (rig == null)
            return;

        GTPlayer.Instance.transform.position            = rig.transform.position;
        GorillaTagger.Instance.rigidbody.linearVelocity = Vector3.zero;
        foundPlayer                                     = true;
    }

    private static void ProcessStealIdentity()
    {
        List<InputDevice> devices = [];
        InputDevices.GetDevices(devices);
        InputDevices.GetDevicesWithCharacteristics((InputDeviceCharacteristics)580, devices);

        if (devices.Count == 0)
            return;

        InputDevice device = devices[0];

        bool trigger = false;
        bool grip    = false;

        device.TryGetFeatureValue(CommonUsages.triggerButton, out trigger);
        device.TryGetFeatureValue(CommonUsages.gripButton,    out grip);

        if (!Physics.Raycast(
                    GTPlayer.Instance.RightHand.controllerTransform.position -
                    GTPlayer.Instance.RightHand.controllerTransform.up,
                    -GTPlayer.Instance.RightHand.controllerTransform.up,
                    out RaycastHit hit))
        {
            if (pointer != null)
            {
                Object.Destroy(pointer);
                pointer = null;
            }

            return;
        }

        if (pointer == null)
        {
            pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Object.Destroy(pointer.GetComponent<Rigidbody>());
            Object.Destroy(pointer.GetComponent<SphereCollider>());
            pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }

        pointer.transform.position = hit.point;

        if (!grip)
            return;

        PhotonView view = hit.collider.GetComponentInParent<PhotonView>();

        if (view == null || view.Owner == PhotonNetwork.LocalPlayer)
            return;

        GorillaTagger.Instance.StartVibration(true, GorillaTagger.Instance.tagHapticStrength,
                GorillaTagger.Instance.tagHapticDuration);

        GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tagHapticStrength,
                GorillaTagger.Instance.tagHapticDuration);

        if (trigger)
        {
            string name = view.Owner.NickName;
            PhotonNetwork.LocalPlayer.NickName = name;
            PhotonNetwork.NickName             = name;
            PlayerPrefs.SetString(_10000._10001(249), name);
            GorillaComputer.instance.currentName = name;
            VRRig.LocalRig.playerText1.text      = name;
            PlayerPrefs.Save();

            //implement method to update colour with rpc here
        }
    }

    private static void ProcessRandomSound()
    {
        if (btnTagSoundCooldown != 0)
            return;

        btnTagSoundCooldown = Time.frameCount + 30;

        VRRig[] rigs   = Object.FindObjectsOfType<VRRig>();
        int[]   sounds = new[] { 0, 2, 5, };

        //implement new method to play the sounds here

        if (menu != null)
        {
            Object.Destroy(menu);
            menu = null;
        }

        Draw();
    }

    private static void ProcessTagGun()
    {
        List<InputDevice> devices = new();
        InputDevices.GetDevicesWithCharacteristics((InputDeviceCharacteristics)580, devices);

        if (devices.Count == 0)
        {
            checkpointTeleportAntiRepeat = false;

            return;
        }

        InputDevice device = devices[0];

        bool trigger = false;
        bool grip    = false;

        device.TryGetFeatureValue(CommonUsages.triggerButton, out trigger);
        device.TryGetFeatureValue(CommonUsages.gripButton,    out grip);

        if (!Physics.Raycast(
                    GTPlayer.Instance.RightHand.controllerTransform.position -
                    GTPlayer.Instance.RightHand.controllerTransform.up,
                    -GTPlayer.Instance.RightHand.controllerTransform.up,
                    out RaycastHit hit))
        {
            if (pointer != null)
            {
                Object.Destroy(pointer);
                pointer = null;
            }

            checkpointTeleportAntiRepeat = false;

            return;
        }

        if (pointer == null)
        {
            pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Object.Destroy(pointer.GetComponent<Rigidbody>());
            Object.Destroy(pointer.GetComponent<SphereCollider>());
            pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }

        pointer.transform.position = hit.point;

        if (!grip)
        {
            checkpointTeleportAntiRepeat = false;

            return;
        }

        if (checkpointTeleportAntiRepeat)
            return;

        Player[] players = PhotonNetwork.PlayerList;
        foreach (Player player in players)
        {
            if (player == PhotonNetwork.LocalPlayer)
                continue;

            PhotonView.Get(GorillaGameManager.instance).RPC(
                    _10000._10001(259),
                    RpcTarget.All,
                    player);
        }

        checkpointTeleportAntiRepeat = true;
    }

    private static void ProcessLongArms()
    {
        List<InputDevice> devices = new();
        InputDevices.GetDevicesWithCharacteristics((InputDeviceCharacteristics)580, devices);

        if (devices.Count == 0)
            return;

        InputDevice device = devices[0];

        device.TryGetFeatureValue(CommonUsages.gripButton,      out gain);
        device.TryGetFeatureValue(CommonUsages.triggerButton,   out less);
        device.TryGetFeatureValue(CommonUsages.primaryButton,   out reset);
        device.TryGetFeatureValue(CommonUsages.secondaryButton, out fastr);

        timeSinceLastChange += Time.deltaTime;

        if (reset)
        {
            myVarY1             = 0f;
            myVarY2             = 0f;
            timeSinceLastChange = 0f;
        }
        else if (gain && timeSinceLastChange > 0.2f)
        {
            myVarY1             += gainSpeed;
            myVarY2             += gainSpeed;
            timeSinceLastChange =  0f;
        }
        else if (less && timeSinceLastChange > 0.2f)
        {
            myVarY1             -= gainSpeed;
            myVarY2             -= gainSpeed;
            timeSinceLastChange =  0f;
        }

        myVarY1 = Mathf.Clamp(myVarY1, -5f, 200f);
        myVarY2 = Mathf.Clamp(myVarY2, -5f, 200f);

        GTPlayer.Instance.maxArmLength = 200f;

        if (fastr)
        {
            myVarY1 = 10f;
            myVarY2 = 10f;
        }
    }

    private static void AddButton(float offset, string text)
    {
        GameObject button = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Object.Destroy(button.GetComponent<Rigidbody>());
        Object.Destroy(button.GetComponent<BoxCollider>());

        button.transform.parent        = menu.transform;
        button.transform.localScale    = new Vector3(0.09f, 0.8f, 0.08f);
        button.transform.localPosition = new Vector3(0.56f, 0f,   0.28f - offset);
        button.transform.rotation      = Quaternion.identity;

        button.GetComponent<Renderer>().material.SetColor(_10000._10001(264), Color.black);

        ButtonCollider collider = button.AddComponent<ButtonCollider>();
        collider.relatedText = text;

        GameObject textObj = new();
        textObj.transform.parent = canvasObj.transform;

        Text label = textObj.AddComponent<Text>();
        label.text                 = text;
        label.fontSize             = 1;
        label.alignment            = TextAnchor.MiddleCenter;
        label.resizeTextForBestFit = true;
        label.resizeTextMinSize    = 0;

        Object builtin = Resources.GetBuiltinResource(typeof(Font), _10000._10001(272));
        label.font = (Font)builtin;

        RectTransform rect = label.GetComponent<RectTransform>();
        rect.localPosition = new Vector3(0.064f, 0f, 0.111f - offset / 2.55f);
        rect.sizeDelta     = new Vector2(0.2f, 0.03f);
        rect.rotation      = Quaternion.Euler(180f, 90f, 90f);

        int index = Array.IndexOf(Buttons, text);
        if (index >= 0 && ButtonsActive[index])
            button.GetComponent<Renderer>().material.SetColor(_10000._10001(268), Color.magenta);
        else
            button.GetComponent<Renderer>().material.SetColor(_10000._10001(266), Color.grey);
    }

    public static void Draw()
    {
        if (menu != null)
            Object.Destroy(menu);

        menu = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Object.Destroy(menu.GetComponent<Rigidbody>());
        Object.Destroy(menu.GetComponent<BoxCollider>());
        Object.Destroy(menu.GetComponent<Renderer>());

        menu.transform.localScale = new Vector3(0.1f, 0.3f, 0.4f);

        canvasObj                  = new GameObject();
        canvasObj.transform.parent = menu.transform;

        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;

        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.dynamicPixelsPerUnit = 1000f;

        canvasObj.AddComponent<GraphicRaycaster>();

        GameObject bg = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Object.Destroy(bg.GetComponent<Rigidbody>());
        Object.Destroy(bg.GetComponent<BoxCollider>());
        bg.transform.parent        = menu.transform;
        bg.transform.localScale    = new Vector3(0.1f,  1f, 1f);
        bg.transform.localPosition = new Vector3(0.05f, 0f, 0f);
        bg.transform.rotation      = Quaternion.identity;
        bg.GetComponent<Renderer>().material.SetColor(_10000._10001(275), Color.black);

        GradientColorKey[] keys = new GradientColorKey[5];
        keys[0] = new GradientColorKey(new Color32(0,   0,  0,   1), 0f);
        keys[1] = new GradientColorKey(new Color32(36,  11, 77,  1), 0.3f);
        keys[2] = new GradientColorKey(new Color32(37,  4,  90,  1), 0.6f);
        keys[3] = new GradientColorKey(new Color32(147, 81, 255, 1), 0.9f);
        keys[4] = new GradientColorKey(new Color32(0,   0,  0,   1), 1f);

        ColorChanger changer = bg.AddComponent<ColorChanger>();
        changer.colors = new Gradient { colorKeys = keys, };
        changer.Start();

        GameObject fpsObj = new();
        fpsObj.transform.parent = canvasObj.transform;

        Text   fps     = fpsObj.AddComponent<Text>();
        Object builtin = Resources.GetBuiltinResource(typeof(Font), _10000._10001(279));
        fps.font                 = (Font)builtin;
        fps.fontSize             = 1;
        fps.fontStyle            = FontStyle.Bold;
        fps.alignment            = TextAnchor.MiddleCenter;
        fps.resizeTextForBestFit = true;
        fps.resizeTextMinSize    = 0;
        fps.text                 = _10000._10001(282) + (int)(1f / Time.smoothDeltaTime);

        RectTransform fpsRect = fps.GetComponent<RectTransform>();
        fpsRect.localPosition = Vector3.zero;
        fpsRect.sizeDelta     = new Vector2(0.28f, 0.05f);
        fpsRect.rotation      = Quaternion.Euler(180f, 90f, 90f);

        AddPageButtons();

        string[] page = Buttons.Skip(pageNumber * pageSize).Take(pageSize).ToArray();
        for (int i = 0; i < page.Length; i++)
            AddButton(i * 0.13f + 0.26f, page[i]);
    }

    private static void AddPageButtons()
    {
        int totalPages   = (Buttons.Length + pageSize - 1) / pageSize;
        int previousPage = pageNumber - 1;
        int nextPage     = pageNumber + 1;

        float buttonOffset = 0.13f;

        GameObject prevButton = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Object.Destroy(prevButton.GetComponent<Rigidbody>());
        prevButton.transform.parent        = menu.transform;
        prevButton.transform.localScale    = new Vector3(0.09f, 0.8f, 0.08f);
        prevButton.transform.localPosition = new Vector3(0.56f, 0f,   0.28f - buttonOffset);
        prevButton.transform.rotation      = Quaternion.Euler(180f, 90f, 90f);
        prevButton.GetComponent<Renderer>().material.SetColor(_10000._10001(286), Color.grey);
        prevButton.AddComponent<ButtonCollider>().relatedText = _10000._10001(288);

        GameObject prevTextObj = new();
        prevTextObj.transform.parent = canvasObj.transform;
        Text prevText = prevTextObj.AddComponent<Text>();
        prevText.text                 = _10000._10001(305) + previousPage + _10000._10001(307);
        prevText.font                 = (Font)Resources.GetBuiltinResource(typeof(Font), _10000._10001(309));
        prevText.fontSize             = 1;
        prevText.alignment            = TextAnchor.MiddleCenter;
        prevText.resizeTextForBestFit = true;
        prevText.resizeTextMinSize    = 0;
        RectTransform prevRect = prevText.GetComponent<RectTransform>();
        prevRect.sizeDelta     = new Vector2(0.2f, 0.03f);
        prevRect.localPosition = new Vector3(0.064f, 0f, 0.111f - buttonOffset / 2.55f);
        prevRect.localRotation = Quaternion.Euler(180f, 90f, 90f);

        GameObject nextButton = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Object.Destroy(nextButton.GetComponent<Rigidbody>());
        nextButton.transform.parent        = menu.transform;
        nextButton.transform.localScale    = new Vector3(0.09f, 0.8f, 0.08f);
        nextButton.transform.localPosition = new Vector3(0.56f, 0f,   0.28f - buttonOffset);
        nextButton.transform.rotation      = Quaternion.identity;
        nextButton.GetComponent<Renderer>().material.SetColor(_10000._10001(311), Color.grey);
        nextButton.AddComponent<ButtonCollider>().relatedText = _10000._10001(292);

        GameObject nextTextObj = new();
        nextTextObj.transform.parent = canvasObj.transform;
        Text nextText = nextTextObj.AddComponent<Text>();
        nextText.text                 = _10000._10001(295) + nextPage + _10000._10001(298);
        nextText.font                 = (Font)Resources.GetBuiltinResource(typeof(Font), _10000._10001(301));
        nextText.fontSize             = 1;
        nextText.alignment            = TextAnchor.MiddleCenter;
        nextText.resizeTextForBestFit = true;
        nextText.resizeTextMinSize    = 0;
        RectTransform nextRect = nextText.GetComponent<RectTransform>();
        nextRect.sizeDelta     = new Vector2(0.2f, 0.03f);
        nextRect.localPosition = Vector3.zero;
        nextRect.localRotation = Quaternion.Euler(180f, 90f, 90f);
    }

    public static void Toggle(string relatedText)
    {
        int totalPages = (Buttons.Length + pageSize - 1) / pageSize;

        if (relatedText == _10000._10001(316))
        {
            pageNumber--;
            if (pageNumber < 0) pageNumber = 0;
        }
        else if (relatedText == _10000._10001(318))
        {
            pageNumber++;
            if (pageNumber > totalPages - 1) pageNumber = totalPages - 1;
        }
        else
        {
            for (int i = 0; i < Buttons.Length; i++)
                if (relatedText == Buttons[i])
                {
                    ButtonsActive[i] = !ButtonsActive[i];

                    break;
                }
        }

        Object.Destroy(menu);
        menu = null;
        Draw();
    }

    private void Init()
    {
        MainCamera = GameObject.Find(_10000._10001(329));

        HUDObj      = new GameObject();
        HUDObj.name = _10000._10001(327);
        HUDObj.AddComponent<Canvas>();
        HUDObj.AddComponent<CanvasScaler>();
        HUDObj.AddComponent<GraphicRaycaster>();
        HUDObj.GetComponent<Canvas>().renderMode  = RenderMode.WorldSpace;
        HUDObj.GetComponent<Canvas>().worldCamera = MainCamera.GetComponent<Camera>();
        HUDObj.transform.position                 = MainCamera.transform.position;
        HUDObj.transform.localScale               = new Vector3(1f, 1f, 1f);
        Quaternion rotation = HUDObj.GetComponent<RectTransform>().rotation;
        HUDObj.transform.localPosition = new Vector3(0f, 0f, 1.6f);
        Vector3 eulerAngles = rotation.eulerAngles;
        eulerAngles.y             = -270f;
        HUDObj.transform.rotation = Quaternion.Euler(eulerAngles);

        HUDObj2                    = new GameObject();
        HUDObj2.name               = _10000._10001(323);
        HUDObj.transform.parent    = HUDObj2.transform;
        HUDObj2.transform.position = MainCamera.transform.position - new Vector3(0f, 0f, 4.6f);

        GameObject textObj = new();
        textObj.transform.parent             = HUDObj.transform;
        Testtext                             = textObj.AddComponent<Text>();
        Testtext.text                        = "";
        Testtext.font                        = GameObject.Find(_10000._10001(331)).GetComponent<Text>().font;
        Testtext.fontSize                    = 10;
        Testtext.alignment                   = TextAnchor.MiddleCenter;
        Testtext.resizeTextForBestFit        = true;
        Testtext.rectTransform.sizeDelta     = new Vector2(260f, 70f);
        Testtext.rectTransform.localPosition = new Vector3(-1.5f, -0.9f, -0.6f);
        Testtext.rectTransform.localScale    = new Vector3(0.01f, 0.01f, 1f);
        Testtext.material                    = AlertText;
    }

    private void FixedUpdate()
    {
        if (!HasInit && GameObject.Find(_10000._10001(334)) != null)
        {
            Init();
            HasInit = true;
        }

        HUDObj2.transform.position = MainCamera.transform.position;
        HUDObj2.transform.rotation = MainCamera.transform.rotation;

        if (Testtext.text != "")
        {
            NotificationDecayTimeCounter++;
            if (NotificationDecayTimeCounter > NotificationDecayTime)
            {
                Notifilines = Testtext.text.Split(Environment.NewLine.ToCharArray()).Skip(1).ToArray();
                string newText = "";
                foreach (string line in Notifilines)
                    if (line != "")
                        newText += line + _10000._10001(336);

                Testtext.text                = newText;
                NotificationDecayTimeCounter = 0;
            }
        }
        else
        {
            NotificationDecayTimeCounter = 0;
        }
    }

    public static void SendNotification(string NotificationText)
    {
        GameObject val = GameObject.Find(_10000._10001(341));

        if (!NotificationText.Contains(Environment.NewLine))
            NotificationText += Environment.NewLine;

        Text textComponent = val.transform.GetChild(0).GetComponent<Text>();
        textComponent.text += NotificationText;

        PreviousNotifi = NotificationText;
    }

    private static void legitwallwalk()
    {
        RaycastHit hit;
        bool isNearWall = Physics.Raycast(GTPlayer.Instance.RightHand.controllerTransform.position,
                                  GTPlayer.Instance.RightHand.controllerTransform.right, out hit, 100f, 512)
                       && hit.distance < 0.7f;

        Rigidbody bodyRb = GTPlayer.Instance.bodyCollider.attachedRigidbody;

        if (isNearWall)
        {
            bodyRb.useGravity =  false;
            bodyRb.velocity   -= hit.normal * (9.8f * Time.deltaTime);
        }
        else
        {
            bodyRb.useGravity = true;
        }
    }

    private static void triggerlagall()
    {
        //do nothing
    }

    private static void lagall()
    {
        //do nothing
    }

    public static void CreatePublicRoom()
    {
        //do nothing
    }

    private static void beacons()
    {
        foreach (VRRig rig in GorillaParent.instance.vrrigs)
        {
            if (rig.isMyPlayer || rig.isOfflineVRRig)
                continue;

            if (!rig.isLocal)
            {
                GameObject beacon = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                Object.Destroy(beacon.GetComponent<BoxCollider>());
                Object.Destroy(beacon.GetComponent<Rigidbody>());
                Object.Destroy(beacon.GetComponent<Collider>());
                beacon.transform.rotation                = Quaternion.identity;
                beacon.transform.localScale              = new Vector3(0.04f, 200f, 0.04f);
                beacon.transform.position                = rig.transform.position;
                beacon.GetComponent<Renderer>().material = rig.mainSkin.material;
                Object.Destroy(beacon, Time.deltaTime);
            }
        }
    }

    private static void UpdateScaleForBeacons(GameObject startObj, GameObject endObj, GameObject beaconObj)
    {
        float distance = Vector3.Distance(startObj.transform.position, endObj.transform.position);
        beaconObj.transform.localScale = new Vector3(beaconObj.transform.localScale.x, distance / 2f,
                beaconObj.transform.localScale.z);

        beaconObj.transform.position = (startObj.transform.position + endObj.transform.position) / 2f;
        beaconObj.transform.up       = endObj.transform.position - startObj.transform.position;
    }

    private static void tracers()
    {
        foreach (VRRig rig in GorillaParent.instance.vrrigs)
        {
            if (rig.isOfflineVRRig || rig.isMyPlayer)
                continue;

            if (!rig.isLocal)
            {
                GameObject tracer = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                Object.Destroy(tracer.GetComponent<BoxCollider>());
                Object.Destroy(tracer.GetComponent<Rigidbody>());
                Object.Destroy(tracer.GetComponent<Collider>());
                Object.Destroy(tracer.GetComponent<MeshCollider>());

                tracer.transform.rotation   = Quaternion.identity;
                tracer.transform.localScale = new Vector3(0.003f, 0.003f, 0.003f);
                tracer.transform.position   = rig.transform.position;

                UpdateScaleForBeacons(GorillaTagger.Instance.rightHandTransform.gameObject, rig.gameObject, tracer);

                tracer.GetComponent<Renderer>().material.color = new Color32(88, 195, 207, 1);
                Object.Destroy(tracer, Time.deltaTime);

                List<InputDevice> devices = new();
                InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, devices);
                if (devices.Count > 0)
                {
                    bool triggerPressed = false;
                    devices[0].TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed);
                }
            }
        }
    }

    public class TimedBehaviour : MonoBehaviour
    {
        public    bool  complete;
        public    bool  loop = true;
        public    float progress;
        protected float duration = 2f;

        protected bool  paused;
        protected float startTime;

        public virtual void Start()
        {
            startTime = Time.time;
        }

        public virtual void Update()
        {
            if (complete)
                return;

            float elapsed = Time.time - startTime;
            progress = Mathf.Clamp(elapsed / duration, 0f, 1f);

            if (elapsed <= duration)
                return;

            if (loop)
                OnLoop();
            else
                complete = true;
        }

        public virtual void OnLoop()
        {
            startTime = Time.time;
        }
    }

    public class ColorChanger : TimedBehaviour
    {
        public Renderer gameObjectRenderer;
        public Gradient colors;
        public Color    color;
        public bool     timeBased = true;

        public override void Start()
        {
            base.Start();
            gameObjectRenderer = GetComponent<Renderer>();
        }

        public override void Update()
        {
            base.Update();

            if (colors == null)
                return;

            if (timeBased)
                color = colors.Evaluate(progress);

            gameObjectRenderer.material.SetColor("_Color",         color);
            gameObjectRenderer.material.SetColor("_EmissionColor", color);
        }
    }
}