namespace KBSL_MOD.Utils
{
    internal static class GamePlayUtils
    {
        private static bool _isGameStart;

        internal static bool isGameStart => _isGameStart;

        internal static void Init()
        {
            _isGameStart = false;
        }

        internal static void OnActionButtonWasPressed()
        {
            _isGameStart = true;
            Plugin.Log.Notice($"is Game Started! {isGameStart}");
        }
    }
}