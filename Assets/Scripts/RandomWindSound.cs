using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class RandomWindSound : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] AudioClip wind1;
    [SerializeField] AudioClip wind2;
    [SerializeField] AudioClip wind3;
    [SerializeField] AudioClip wind4;
    [SerializeField] AudioClip wind5;

    [SerializeField] AudioClip currentClip;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlaySoundAfter());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlaySoundAfter()
    {
        int clipNum = Random.Range(1, 5);

        switch (clipNum)
        {
            case 1:
                currentClip = wind1;
                break;
            case 2:
                currentClip = wind2;
                break;
            case 3:
                currentClip = wind3;
                break;
            case 4:
                currentClip = wind4;
                break;
            case 5:
                currentClip = wind5;
                break;
            default:
                
                break;
        }

        audioSource.clip = currentClip;
        audioSource.Play();

        yield return new WaitForSeconds(audioSource.clip.length);

        StartCoroutine(PlaySoundAfter());
    }
}
