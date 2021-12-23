namespace Delep.Audio
{
    using System;
    using UnityEngine;
    using UnityEngine.Audio;

    using static AudioBus;

    public class AudioManager : MonoBehaviour
    {
        private const string MasterBusName = "Master";
        private const string MusicBusName = "Music";
        private const string SfxBusName = "SFX";
        private const string UIBusName = "UI";
        private const string VoicesBusName = "Voices";

        private const int maxVolumeDb = 0;
        private const int minVolumeDb = -80;
        private const int volumeHalfStep = 10;

        public static AudioManager Instance { get; private set; }

        [SerializeField] private AudioMixer mixer;

        /// <summary>
        /// Sets the volume of the bus following a linear scale.
        /// </summary>
        /// <param name="busName">The name of the bus</param>
        /// <param name="volumeInPercent">A value between 0 and 1.</param>
        public void SetBusVolume(AudioBus audioBus, float volume)
        {
#if UNITY_EDITOR
            if (volume < 0 || volume > 1)
            {
                Debug.LogWarning($"Parameter {nameof(volume)} should be a value between 0 and 1");
            }
#endif
            mixer.SetFloat(GetBusName(audioBus), Mathf.Clamp(volume, 0, 1) switch
            {
                1 => maxVolumeDb,
                0 => minVolumeDb,
                _ => maxVolumeDb + (Mathf.Log(volume, 2) * volumeHalfStep)
            });
        }

        private string GetBusName(AudioBus audioBus)
            => audioBus switch
            {
                Master => MasterBusName,
                Music => MusicBusName,
                SFX => SfxBusName,
                UI => UIBusName,
                Voices => VoicesBusName,
                _ => throw new ArgumentException(nameof(audioBus))
            };

        private void Awake()
        {
            SetSingleton();
        }

        private void SetSingleton()
        {
            DontDestroyOnLoad(this);
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }
    }
}