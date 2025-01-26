using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance { get; private set; }

    public GameObject _tapioca;

    //[SerializeField]
    private int _tapiocaHp;
    [SerializeField]
    private int _maxTapiocaHp;

    private bool _state1 = false;
    private bool _state2 = false;

    private AudioSource _audioSource;

    private SceneMaster _sceneMasterInstance;

    [SerializeField]
    private GameObject _gameOverBox;
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
        _tapiocaHp = _maxTapiocaHp;
    }

    public void SubTapiocaHp(int valueToSub)
    {
        _tapiocaHp -= valueToSub;
        Debug.Log(_tapiocaHp);
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
    }

    private void SetTapiocaState(int state)
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
    }

    public void Win()
    {
        SceneMaster.instance._audioSource.Stop();
        PlayAudioSource(_winMusic);
        StartCoroutine(WinCoroutine(5));
    }

    private void GameOver()
    {
        SceneMaster.instance._audioSource.Stop();
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
        yield return new WaitForSeconds(2);
        PlayAudioSource(_defeatMusic);
        _gameOverBox.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        _sceneMasterInstance.GoToMenu();
        yield return null;
    }

    IEnumerator WinCoroutine(int waitTime)
    {
        _winBox.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        _sceneMasterInstance.GoToMenu();
        yield return null;
    }
}
