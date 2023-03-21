using KBSL_MOD.Manager;
using KBSL_MOD.Models;
using Zenject;

namespace KBSL_MOD.Installers
{
    public class AppInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<PlayerManager>().AsSingle();
        }
    }
}