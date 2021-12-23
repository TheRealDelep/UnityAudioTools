namespace Delep.Audio.Settings
{
    using System;

    public static partial class Settings
    {
        public static class AudioMixer
        {
            public const string MasterBusName = "Master";
            public const string MusicBusName = "Music";
            public const string SFXBusName = "SFX";
            public const string UIBusName = "UI";
            public const string VoicesBusName = "Voices";

            public const int MaxVolumeDb = 0;
            public const int MinVolumeDb = -80;
            public const int VolumeHalfStep = 10;

            public static string GetBusName(AudioBus audioBus)
                => audioBus switch
                {
                    AudioBus.Master => MasterBusName,
                    AudioBus.Music => MusicBusName,
                    AudioBus.SFX => SFXBusName,
                    AudioBus.UI => UIBusName,
                    AudioBus.Voices => VoicesBusName,
                    _ => throw new ArgumentException(nameof(audioBus))
                };
        }
    }
}