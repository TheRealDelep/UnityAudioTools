namespace Delep.Audio
{
    using System;
    using UnityEngine;
    using UnityEngine.Audio;

    using static Settings.Settings.AudioMixer;

    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        private AudioMixer mixer;

        private static AudioManager instance;

        public static AudioManager Instance
            => instance ?? throw new NullReferenceException("There is no AudioManager in the scene");

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
                0 => MinVolumeDb,
                1 => MaxVolumeDb,
                _ => MaxVolumeDb + (Mathf.Log(volume, 2) * VolumeHalfStep)
            }); ;
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }
    }
}