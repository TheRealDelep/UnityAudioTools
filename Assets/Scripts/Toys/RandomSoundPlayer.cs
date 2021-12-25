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
        private int LastClipIndex { get; set; }

        private bool RandomizeVolume => randomVolumeRange.x != randomVolumeRange.y;
        private bool RandomizePitchVolume => randomPitchRange.x != randomPitchRange.y;

        public void Play()
        {
            var index = GenerateClipIndex();
            Source.clip = clips[index];

            if (RandomizeVolume)
            {
                randomVolumeRange.x = Mathf.Clamp(randomVolumeRange.x, 0, 1);
                randomVolumeRange.y = Mathf.Clamp(randomVolumeRange.y, 0, 1);
                Source.volume = Random.Range(randomVolumeRange.x, randomVolumeRange.y);
            }

            if (RandomizePitchVolume)
            {
                randomPitchRange.x = Mathf.Clamp(randomPitchRange.x, -3, 3);
                randomPitchRange.y = Mathf.Clamp(randomPitchRange.y, -3, 3);
                Source.pitch = Random.Range(randomPitchRange.x, randomPitchRange.y);
            }

            Source.Play();
            LastClipIndex = index;
        }

        private void Start()
        {
            Source = GetComponent<AudioSource>();
        }

        private int GenerateClipIndex()
        {
            var rnd = Random.Range(0, clips.Length);
            return rnd == LastClipIndex
                ? GenerateClipIndex()
                : rnd;
        }
    }
}