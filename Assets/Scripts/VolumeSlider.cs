namespace Delep.Audio
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class VolumeSlider : MonoBehaviour
    {
        [SerializeField] private AudioBus audioBus;

        private Slider Slider { get; set; }

        private void Start()
        {
            Slider = GetComponent<Slider>();
            SetBusVolume(Slider.value);
        }

        public void SetBusVolume(float volume)
        {
#if UNITY_EDITOR
            _ = AudioManager.Instance ?? throw new NullReferenceException("There is no AudioManager in the scene");
#endif
            AudioManager.Instance?.SetBusVolume(audioBus, volume);
        }
    }
}