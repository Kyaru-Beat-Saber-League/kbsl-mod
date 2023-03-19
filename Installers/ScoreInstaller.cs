using KBSL_MOD.ConfigModels;
using UnityEngine;
using Zenject;

namespace KBSL_MOD.Installers
{
    public class ScoreInstaller : MonoInstaller
    {
        [Inject] private readonly PlayerDataModel DataModel;

        public override void InstallBindings()
        {
            MainConfigModel mainConfig = Plugin.MainConfig;

            if (!mainConfig.Enabled) return;

            Plugin.Log.Notice("Loading ScoreInstaller...");
        }
    }
}