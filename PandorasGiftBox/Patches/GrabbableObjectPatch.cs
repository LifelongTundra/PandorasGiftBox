using HarmonyLib;
using PandorasGiftBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandorasJackInTheBox.Patches
{
    [HarmonyPatch(typeof(GrabbableObject))]
    internal class GrabbableObjectPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch("UseItemOnClient")]
        private static void useItemPatch(GrabbableObject __instance)
        {
            Item itemProperties = __instance.itemProperties;
            if (itemProperties.name == "GiftBox")
            {
                PandorasBoxBase.mls.LogInfo("Gift Box was opened");
                RoundManagerPatch.giftOpenedFlag = true;
            }
        }
    }
}
