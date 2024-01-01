using HarmonyLib;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandorasJackInTheBox.Patches
{
    [HarmonyPatch(typeof(RoundManager))]
    internal class RoundManagerPatch
    {
        public static bool flag = false;

        [HarmonyPrefix]
        [HarmonyPatch("Update")]
        // Sets time to 12pm or if its past adds one hour. It then ups the spawn probability and sets the minimum enemies to eclipsed levels.
        public static void addTime(ref TimeOfDay ___timeScript, ref SelectableLevel ___currentLevel, ref int ___minOutsideEnemiesToSpawn, ref int ___minEnemiesToSpawn)
        {
            if (flag)
            {

                if (___timeScript.globalTime < 300)
                {
                    ___timeScript.globalTime += 60;
                    ___currentLevel.spawnProbabilityRange += 1f;
                    ___minOutsideEnemiesToSpawn = 4;
                    ___minEnemiesToSpawn = 4;
                }
                else
                {
                    ___timeScript.globalTime += 60;
                    ___currentLevel.spawnProbabilityRange += 1f;
                    ___minOutsideEnemiesToSpawn = 4;
                    ___minEnemiesToSpawn = 4;
                    flag = false;
                }
            }

        }
    }
}
