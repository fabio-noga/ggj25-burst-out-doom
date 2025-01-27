using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class SceneMaster : MonoBehaviour
{
    public static SceneMaster instance;

    private Scene _currentScene;

    public AudioSource _audioSource;

    public AudioResource _menuMusic;
    public AudioResource _levelMusic;

    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;

        DontDestroyOnLoad(this);
    }

    void Start()
    {
        checkAudioCreation();
        OnStartSceneBehavior();
    }

    private void checkAudioCreation()
    {
        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
            if (_audioSource == null)
            {
                _audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }

    private void OnStartSceneBehavior()
    {
        _audioSource = GetComponent<AudioSource>();
        _currentScene = SceneManager.GetActiveScene();
        if (_currentScene.name == "Level")
        {
            if (_levelMusic != null)
            {
                PlayAudioSource(_levelMusic, true);
            }
        }
        else if (_currentScene.name == "Menu")
        {
            if (_menuMusic != null)
            {
                PlayAudioSource(_menuMusic, true);
            }
        }
        else if(_currentScene.name == "Menu")
        {

        }
    }

    void PlayAudioSource(AudioResource audioClip, bool loop)
    {
        _audioSource.loop = loop;
        _audioSource.resource = audioClip;
        _audioSource.Play();
    }

    public void PlayLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level");
        _currentScene = SceneManager.GetSceneByName("Level");

        PlayAudioSource(_levelMusic, true);
    }

    public void Credits()
    {
        SceneManager.LoadScene("Menu");
        _currentScene = SceneManager.GetSceneByName("Menu");

        //PlayAudioSource( , true);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
        _currentScene = SceneManager.GetSceneByName("Menu");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void stopAudio()
    {
        checkAudioCreation();
        _audioSource.Stop();
    }
}
