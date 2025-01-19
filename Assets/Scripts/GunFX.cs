using UnityEngine;

public class GunFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem fx;
    [SerializeField] private AudioClip      audioClip;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        fx.Play();
        _audioSource.PlayOneShot(audioClip);
    }
}