using UnityEngine;
using UnityEngine.Audio;

public class UIButton : MonoBehaviour
{
    AudioSource _audioSource;

    AudioResource _hoverSound;
    AudioResource _pressedSound;

    protected void Start()
    {
        _hoverSound = Resources.Load("menu_hover") as AudioResource;
        _pressedSound = Resources.Load("menu_click") as AudioResource;

        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
    }
    public void MousePointerEnter()
    {
        _audioSource.resource = _hoverSound;
        PlaySound();
    }

    public virtual void MousePointerDown()
    {
        _audioSource.resource = _pressedSound;
        PlaySound();
    }

    public virtual void MousePointerUp()
    {
        _audioSource.resource = _pressedSound;
        PlaySound();
    }

    private void PlaySound()
    {
        _audioSource.Play();
    }
}
