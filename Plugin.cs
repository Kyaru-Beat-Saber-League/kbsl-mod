﻿
using KBSL_MOD.Installers;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using System.Reflection;
using KBSL_MOD.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SiraUtil.Zenject;
using IPALogger = IPA.Logging.Logger;
using HarmonyObj = HarmonyLib.Harmony;

namespace KBSL_MOD
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }
        internal static MainConfigModel MainConfig { get; private set; }

        private HarmonyObj HarmonyObj;

        /// <summary>
        /// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
        /// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
        /// Only use [Init] with one Constructor.
        /// </summary>
        [Init]
        public void Init(IPALogger logger,
            [Config.Name("KBSL")] Config conf,
            Zenjector zenjector)
        {
            Instance = this;
            Log = logger;
            MainConfig = conf.Generated<MainConfigModel>();
            HarmonyObj = new HarmonyObj("test");
            
            zenjector.UseHttpService();
            zenjector.Expose<CoreGameHUDController>("Environment");
            
            zenjector.Install<AppInstaller>(Location.App);
            zenjector.Install<GamePlayDataInstaller>(Location.StandardPlayer);

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            Log.Info("KBSL-MOD initialized.");
        }

        [OnEnable]
        public void OnEnable() => HarmonyObj.PatchAll(Assembly.GetExecutingAssembly());

        [OnDisable]
        public void OnDisable() => HarmonyObj.UnpatchSelf();
    }
}