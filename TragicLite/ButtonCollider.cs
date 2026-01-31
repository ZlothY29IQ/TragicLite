using TragicLite.Patches;
using UnityEngine;

namespace TragicLite;

public class ButtonCollider : MonoBehaviour
{
    public string relatedText;

    private void OnTriggerEnter(Collider collider)
    {
        if (Time.frameCount < MenuPatch.FramePressCooldown + 30)
            return;

        MenuPatch.Toggle(relatedText);
        MenuPatch.FramePressCooldown = Time.frameCount;

        GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, false, 0.25f);
    }
}