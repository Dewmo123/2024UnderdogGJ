using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;

    private Dictionary<string, Slider> _sliders = new Dictionary<string, Slider>();

    private Transform _ui;
    private Player _player;
    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _ui = transform.GetChild(0);
        Slider[] sliders = GetComponentsInChildren<Slider>();
        sliders.ToList().ForEach(item =>
        {
            _sliders.Add(item.gameObject.name, item);
            float val;
            mixer.GetFloat(item.gameObject.name, out val);
            item.value = val;
        });
        _ui.gameObject.SetActive(false);
    }
    private void Start()
    {
        _player.GetCompo<InputReader>().OnEsc += Show;
    }
    private void OnDestroy()
    {
        _player.GetCompo<InputReader>().OnEsc -= Show;
    }
    public void Show()
    {
        Time.timeScale = 0;
        _ui.gameObject.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void X()
    {
        Time.timeScale = 1;
        _ui.gameObject.SetActive(false);
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void HandleSliderValueChanged(string name)
    {
        mixer.SetFloat(name, _sliders[name].value);
    }
}
