using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    private static AudioSource _audioSource;
    [SerializeField] AudioClip _sheepClip;
    [SerializeField] AudioClip _goatClip;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public static void PlayOneShot(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}
