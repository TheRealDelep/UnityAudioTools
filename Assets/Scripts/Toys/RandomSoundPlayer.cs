namespace Delep.Audio.Toys
{
    using UnityEngine;

    [RequireComponent(typeof(AudioSource))]
    public class RandomSoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip[] clips;
        [SerializeField] private Vector2 randomVolumeRange;
        [SerializeField] private Vector2 randomPitchRange;

        private AudioSource Source { get; set; }
        private int lastClipIndex { get; set; }

        private bool RandomizeVolume => randomVolumeRange.x != randomVolumeRange.y;
        private bool RandomizePitchVolume => randomPitchRange.x != randomPitchRange.y;

        public void Play()
        {
            var index = GenerateClipIndex();
            Source.clip = clips[index];

            if (RandomizeVolume)
            {
                Source.volume = Random.Range(randomVolumeRange.x, randomVolumeRange.y);
            }

            if (RandomizePitchVolume)
            {
                Source.pitch = Random.Range(randomPitchRange.x, randomPitchRange.y);
            }

            Source.Play();
            lastClipIndex = index;
        }

        private void Start()
        {
            Source = GetComponent<AudioSource>();
        }

        private int GenerateClipIndex()
        {
            var rnd = Random.Range(0, clips.Length);
            return rnd == lastClipIndex
                ? GenerateClipIndex()
                : rnd;
        }
    }
}