using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathUI : MonoBehaviour
{
    private Transform _ui;
    private Player _player;
    [SerializeField] private TextMeshProUGUI _droneTxt;
    [SerializeField] private TextMeshProUGUI _scoreTxt;
    private void Awake()
    {
        _ui = transform.GetChild(0);
        _ui.gameObject.SetActive(false);
        _player = FindObjectOfType<Player>();
        _player.OnDead.AddListener(Show);
    }
    private void OnDestroy()
    {
        _player.OnDead.RemoveListener(Show);
    }
    public void Show()
    {
        Time.timeScale = 0;
        _droneTxt.text = "Destroyed Drones : " + GameManager.Instance.killedCount;
        _scoreTxt.text = "Score : " + GameManager.Instance.score;
        _ui.gameObject.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
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
}
