using HarmonyLib;
using KBSL_MOD.Utils;

namespace KBSL_MOD.Patchers
{
    [HarmonyPatch(typeof(SinglePlayerLevelSelectionFlowCoordinator), "ActionButtonWasPressed")]
    public static class ActionButtonListener
    {
        [HarmonyPrefix]
        private static void Prefix() => GamePlayUtils.OnActionButtonWasPressed();

    }
    
    // [HarmonyPatch(typeof(PracticeViewController), "PlayButtonPressed")]
    // public static class PressPlayButtonListener
    // {
    //     [HarmonyPrefix]
    //     private static void Prefix() => GamePlayUtils.OnActionButtonWasPressed();
    // }
}