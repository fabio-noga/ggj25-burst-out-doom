using System.Collections;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance { get; private set; }

    public GameObject _tapioca;

    //[SerializeField]
    private double _tapiocaHp;
    [SerializeField]
    private int _maxTapiocaHp;

    private bool _state1 = false;
    private bool _state2 = false;
    private bool _state3 = false;

    private AudioSource _audioSource;

    private SceneMaster _sceneMasterInstance;

    [Header("Windows")]
    [SerializeField]
    private GameObject _gameOverBox;
    [SerializeField]
    public TMP_Text _gameOverText;
    [SerializeField]
    public GameObject _restartButton;
    [SerializeField]
    public GameObject _continueButton;
    [SerializeField]
    public TMP_Text _gameOverScore;
    [SerializeField]
    private GameObject _winBox;

    public AudioResource _defeatMusic;
    public AudioResource _winMusic;

    public Mound _mound;

    private void Awake()
    {
        if(instance != null)
        {
            return;
        }
        instance = this;
    }

    private void Start()
    {
        _sceneMasterInstance = SceneMaster.instance;
        _gameOverBox.SetActive(false);
        //_tapiocaHp = _maxTapiocaHp;
        _tapiocaHp = BuildManager.instance.currencyTotal;
    }

    /*public void SubTapiocaHp(int valueToSub)
    {
        _tapiocaHp -= valueToSub;
        if(_tapiocaHp <= _maxTapiocaHp / 2)
        {
            SetTapiocaState(1);
        }

        if (_tapiocaHp <= _maxTapiocaHp / 4)
        {
            SetTapiocaState(2);
        }

        if (_tapiocaHp <= 0)
        {
            _tapiocaHp = 0;
            GameOver();
        }
    }*/

    public void SetTapiocaState(int state)
    {
        if(state == 1)
        {
            if(_state1 == false)
            {
                _state1 = true;
                _mound.RemoveBalls();
            }
        }
        else if(state == 2)
        {
            if (_state2 == false)
            {
                _state2 = true;
                _mound.RemoveBalls();
            }
        }
        else if (state == 3)
        {
            if (_state3 == false)
            {
                _state3 = true;
                _mound.RemoveBalls();
            }
        }
    }

    public void Win()
    {
        SceneMaster.instance.stopAudio();
        PlayAudioSource(_winMusic);
        StartCoroutine(WinCoroutine(5));
    }

    public void GameOver()
    {
        _gameOverScore.SetText("Total Score: " + BuildManager.instance.currencyMax);
        SceneMaster.instance.stopAudio();
        StartCoroutine(GameOverCoroutine(5));
    }

    private void PlayAudioSource(AudioResource audioClip)
    {
        if(_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }
        _audioSource.resource = audioClip;
        _audioSource.Play();
    }

    IEnumerator GameOverCoroutine(int waitTime)
    {
        //yield return new WaitForSeconds(2);
        PlayAudioSource(_defeatMusic);
        _continueButton.SetActive(false);
        _restartButton.SetActive(true);
        _gameOverText.SetText("Game Over");
        _gameOverBox.SetActive(true);
        Time.timeScale = 0f;
        //yield return new WaitForSeconds(waitTime);
        //_sceneMasterInstance.GoToMenu();
        yield return null;
    }

    IEnumerator WinCoroutine(int waitTime)
    {
        _winBox.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        _sceneMasterInstance.GoToMenu();
        yield return null;
    }

    public void PauseGame()
    {
        _restartButton.SetActive(false);
        _continueButton.SetActive(true);
        _gameOverText.SetText("Paused");
        _gameOverBox.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
        _gameOverBox.SetActive(false);
    }
}
