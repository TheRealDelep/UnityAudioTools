namespace Delep.Audio
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    using static Settings.Settings.PlayerPrefsKeys;

    public class VolumeSlider : MonoBehaviour
    {
        [SerializeField] private AudioBus audioBus;
        [SerializeField] private float defaultValue;

        private Slider Slider { get; set; }
        private string PlayerPrefsKey { get; set; }

        private void Start()
        {
            Slider = GetComponent<Slider>();
            PlayerPrefsKey = GetPlayerPrefsKey(audioBus);

            Slider.value = PlayerPrefs.GetFloat(PlayerPrefsKey, defaultValue);
            SetBusVolume(Slider.value);
        }

        public void SetBusVolume(float volume)
        {
#if UNITY_EDITOR
            _ = AudioManager.Instance ?? throw new NullReferenceException("There is no AudioManager in the scene");
#endif
            AudioManager.Instance?.SetBusVolume(audioBus, volume);
            PlayerPrefs.SetFloat(PlayerPrefsKey, volume);
        }
    }
}