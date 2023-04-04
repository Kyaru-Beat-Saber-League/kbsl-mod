using System;
using KBSL_MOD.Manager;
using KBSL_MOD.Models;
using KBSL_MOD.Utils;
using UnityEngine;
using Zenject;

namespace KBSL_MOD.Installers
{
    public class AppInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Plugin.Log.Debug("AppInitInstaller Loading..");
            StartCoroutine(AuthUtils.Login());
        }
    }
}