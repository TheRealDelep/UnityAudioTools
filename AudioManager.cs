namespace Delep.Audio
{
    using UnityEngine;
    using UnityEngine.Audio;

    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer master;

        public static AudioManager Instance { get; private set; }

        private void Awake()
        {
            InitSingleton();
        }

        private void InitSingleton()
        {
            DontDestroyOnLoad(this);
            if (Instance != null)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }
    }
}