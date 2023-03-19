﻿using BeatSaberMarkupLanguage.Attributes;


namespace KBSL_MOD.ConfigModels
{
    public class MainConfigModel
    {
        public string DisplayName => "Main";
        [UIValue(nameof(Enabled))] public virtual bool Enabled { get; set; } = true;
    }
}