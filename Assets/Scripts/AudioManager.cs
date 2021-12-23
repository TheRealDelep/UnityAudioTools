namespace Delep.Audio
{
    using UnityEngine;
    using UnityEngine.Audio;

    using static Settings.Settings.AudioMixer;

    public class AudioManager : MonoBehaviour
    {
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
                0 => MinVolumeDb,
                1 => MaxVolumeDb,
                _ => MaxVolumeDb + (Mathf.Log(volume, 2) * VolumeHalfStep)
            }); ;
        }

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