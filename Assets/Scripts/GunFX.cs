using UnityEngine;

public class GunFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem fx;
    [SerializeField] private AudioClip      fireSound;
    [SerializeField] private AudioClip      emptySound;
    [SerializeField] private AudioClip      reloadSound;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayFire()
    {
        fx.Play();
        _audioSource.PlayOneShot(fireSound);
    }

    public void PlayEmpty()
    {
        _audioSource.PlayOneShot(emptySound);
    }

    public void PlayReload()
    {
        _audioSource.PlayOneShot(reloadSound);
    }
}