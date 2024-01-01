using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using PandorasJackInTheBox.Patches;

namespace PandorasGiftBox
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class PandorasBoxBase : BaseUnityPlugin
    {
        private const string modGUID = "Tundra.PandorasGiftBoxMod";
        private const string modName = "Pandora's Gift Box Mod";
        private const string modVersion = "1.0.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static PandorasBoxBase Instance;

        public static ManualLogSource mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);

        public static ConfigEntry<bool> CanHoardingBugEscape;
        public static ConfigEntry<float> HoardingBugChanceToEscapeEveryMinute;
        public static ConfigEntry<float> HoardingBugChanceToNestNearShip;
        public static ConfigEntry<float> HoardingBugChanceToSpawnOutside;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls.LogInfo("Tundra's mod Pandora's Gift Box has opened!");

            harmony.PatchAll(typeof(PandorasBoxBase));
            harmony.PatchAll(typeof(RoundManagerPatch));
            harmony.PatchAll(typeof(GrabbableObjectPatch));

        }

    }
}
