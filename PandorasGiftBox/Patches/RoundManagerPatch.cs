using HarmonyLib;
using PandorasGiftBox;
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
        public static bool giftOpenedFlag = false;
        // Can be 0 since starting global time currently is always 100
        private static float timeGiftOpened = 0f;
        private static int hoursToAdd = 0;

        [HarmonyPrefix]
        [HarmonyPatch("Update")]
        // Sets a scaling time punishment, ups the max power levels, and sets the minimum enemies to eclipsed levels + 1.
        public static void wasPandoraOpened(ref TimeOfDay ___timeScript, ref SelectableLevel ___currentLevel, ref int ___minOutsideEnemiesToSpawn, ref int ___minEnemiesToSpawn)
        {
            // Check if gift was opened and do the static var updates
            if (giftOpenedFlag)
            {
                timeGiftOpened = ___timeScript.globalTime;
                ___minOutsideEnemiesToSpawn = 5;
                ___minEnemiesToSpawn = 5;
                ___currentLevel.maxEnemyPowerCount += 4;
                ___currentLevel.maxOutsideEnemyPowerCount += 4;
                ___currentLevel.maxDaytimeEnemyPowerCount += 8;
                giftOpenedFlag = false;
            }

            // Gift opened between 8am and 12pm -> Add 4 hours 
            if (timeGiftOpened > 100f && timeGiftOpened < 300f)
            {
                hoursToAdd = 4;
            }
            // Gift opened between 12pm and 5pm -> Add 3 hours
            else if (timeGiftOpened > 300f && timeGiftOpened < 600f)
            {
                hoursToAdd = 3;
            }
            // Gift opened after 5pm -> Add 1 hour
            // Note: can't do else statement as this would trigger during the base case
            else if (timeGiftOpened > 600f)
            {
                hoursToAdd = 1;
            }

            // Time needs to be done in hour increments as enemies spawns are planned 1 hour in advance.
            // Due to this time is added an hour at a time over the next couple of updates
            // Note: if time added hits 5pm the remaining hours don't get added for some reason. Will still add an additional hour if opened after 5pm
            if (hoursToAdd > 0)
            {
                // Reset the opened time so next update doesn't add more
                timeGiftOpened = 0f;
                ___timeScript.globalTime += 60f;
                hoursToAdd--;
            }
        }
    }
}
