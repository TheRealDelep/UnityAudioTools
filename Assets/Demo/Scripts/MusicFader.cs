using System.Collections;

using Delep.Audio.Toys;

using UnityEngine;
using UnityEngine.UI;

public class MusicFader : MonoBehaviour
{
    [SerializeField]
    private Slider FadeFromSlider;

    [SerializeField]
    private Slider FadeToSlider;

    [SerializeField]
    private Slider SlopeValueSlider;

    [SerializeField]
    private Dropdown SlopeTypeDropdown;

    [SerializeField]
    private InputField DurationInput;

    [SerializeField]
    private Button StartFadeButton;

    [SerializeField]
    private AudioSource MusicSource;

    [SerializeField]
    private MusicFaderGraph Graph;

    private Fader Fader { get; set; }

    private void Start()
    {
        Fader = new Fader(
            FadeFromSlider.value,
            FadeToSlider.value,
            int.Parse(DurationInput.text),
            (SlopeType)SlopeTypeDropdown.value,
            SlopeValueSlider.value);

        FadeFromSlider.onValueChanged.AddListener(value => Fader.From = value);
        FadeToSlider.onValueChanged.AddListener(value => Fader.To = value);
        SlopeValueSlider.onValueChanged.AddListener(value =>
        {
            Fader.SlopeValue = value;
            Graph.SlopeValue = value;
            Graph.Draw();
        });
        SlopeTypeDropdown.onValueChanged.AddListener(value =>
        {
            Fader.SlopeType = (SlopeType)value;
            Graph.SlopeType = (SlopeType)value;
            Graph.Draw();
        });
        DurationInput.onValueChanged.AddListener(value => Fader.Duration = int.Parse(value));
        StartFadeButton.onClick.AddListener(() => StartCoroutine(Fade()));

        Graph.SlopeValue = SlopeValueSlider.value;
        Graph.SlopeType = (SlopeType)SlopeTypeDropdown.value;
        Graph.Draw();
    }

    public IEnumerator Fade()
    {
        float time = 0;
        while (time < Fader.Duration)
        {
            MusicSource.volume = Fader.GetValue(time);
            yield return null;
            time += Time.deltaTime;
        }
    }
}