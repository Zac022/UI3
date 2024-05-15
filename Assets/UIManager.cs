using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("UI Controls")]
    public Slider volumeSlider;
    public Slider sizeSlider;
    public Toggle soundToggle;
    public Toggle lightToggle;
    public TextMeshProUGUI timerText;
    public GameObject lightObject;
    public GameObject objectToScale;
    public AudioSource audioSource;

    private float elapsedTime = 0f;
    private float maxTime = 10f;
    private Color[] lightColors = { Color.green, Color.magenta, Color.red };

    private void Update()
    {
        UpdateTimer();
    }

    private void Start()
    {
        SetUIListeners();

#if UNITY_STANDALONE_WIN
        sizeSlider.interactable = false;
#endif

#if UNITY_EDITOR
        sizeSlider.interactable = true;
#endif
    }

    private void SetUIListeners()
    {
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        sizeSlider.onValueChanged.AddListener(OnSizeChanged);
        soundToggle.onValueChanged.AddListener(OnSoundToggle);
        lightToggle.onValueChanged.AddListener(OnLightToggle);
    }

    private void OnVolumeChanged(float value)
    {
        if (audioSource != null)
        {
            audioSource.volume = value;
        }
    }

    private void OnSizeChanged(float value)
    {
        objectToScale.transform.localScale = new Vector3(value, value, value) * 3;
    }

    private void OnSoundToggle(bool value)
    {
        if (audioSource != null)
        {
            audioSource.enabled = value;
        }
    }

    private void OnLightToggle(bool value)
    {
        lightObject.SetActive(value);
       
    }

    private void UpdateTimer()
    {
        elapsedTime += Time.deltaTime;
        timerText.text = FormatTime(elapsedTime);

        if (elapsedTime >= maxTime)
        {
            timerText.text = "<color=yellow>Time Up</color>";
        }
    }

    private string FormatTime(float timeInSeconds)
    {
        int hours = (int)(timeInSeconds / 3600);
        int minutes = (int)((timeInSeconds % 3600) / 60);
        int seconds = (int)(timeInSeconds % 60);
        return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }
}
