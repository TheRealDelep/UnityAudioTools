namespace Delep.Audio.Settings
{
    using System;

    public static partial class Settings
    {
        public static class PlayerPrefsKeys
        {
            public const string MasterVolume = "MasterVolume";
            public const string MusicVolume = "MusicVolume";
            public const string SFXVolume = "SFXVolume";
            public const string UIVolume = "UIVolume";
            public const string VoicesVolume = "VoicesVolume";

            public static string GetPlayerPrefsKey(AudioBus audioBus)
                => audioBus switch
                {
                    AudioBus.Master => MasterVolume,
                    AudioBus.Music => MusicVolume,
                    AudioBus.SFX => SFXVolume,
                    AudioBus.UI => UIVolume,
                    AudioBus.Voices => VoicesVolume,
                    _ => throw new ArgumentException(nameof(audioBus))
                };
        }
    }
}