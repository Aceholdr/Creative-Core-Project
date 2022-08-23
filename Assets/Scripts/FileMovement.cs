using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileMovement : MonoBehaviour
{
    public static bool levelPassed;
    private bool isFiling;
    GameObject[] dustParticles;
    GameObject[] despawnParticles;
    ParticleSystem ParticleSystem;
    AudioSource audioSource;
    AudioClip currentClip;

    // Number of bounds only placeholders.
    public float leftBound = -10.0f;
    public float rightBound = 10.0f;
    public float bottomBound = -10.0f;
    public float upperBound = 10.0f;

    private float boundBuffer = 0.00001f;

    [SerializeField] private float horizontalInput;
    [SerializeField] private float forwardInput;
    [SerializeField] private float speed;
    [SerializeField] private AudioClip fileSoundLeft;
    [SerializeField] private AudioClip fileSoundRight;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(ActivateParticle());
    }

    /// <summary>
    /// When a nail spawns the particle will be linked to it.
    /// </summary>
    /// <returns>Waits to prevent error.</returns>
    IEnumerator ActivateParticle()
    {
        yield return new WaitForSeconds(0.01f);

        dustParticles = GameObject.FindGameObjectsWithTag("NailParticles");

        foreach (GameObject particle in dustParticles)
        {
            ParticleSystem = particle.GetComponent<ParticleSystem>();
            ParticleSystem.Stop(false, stopBehavior: ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
        ResetLevel();
        HandleNailParticles();
    }

    /// <summary>
    /// Resets position of character and resets particles.
    /// </summary>
    void ResetLevel()
    {
        if (levelPassed)
        {
            StartCoroutine(ActivateParticle());
            transform.position = new Vector3(-2.0f, 0.5f, -4.0f);
        }
    }

    /// <summary>
    /// Plays the nail particle when filing.
    /// </summary>
    void HandleNailParticles()
    {
        if (ParticleSystem != null)
        {
            if (isFiling)
            {
                ParticleSystem.Play();
            }
            else
            {
                ParticleSystem.Stop();
            }

            isFiling = false;
        }
    }

    /// <summary>
    /// Moves the character and prevents it from going out of bounds.
    /// </summary>
    void MoveCharacter()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        if (transform.position.x > leftBound && transform.position.x < rightBound && transform.position.z > bottomBound && transform.position.z < upperBound)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
            transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
        }
        else if (transform.position.x < leftBound)
        {
            transform.position = new Vector3(leftBound + boundBuffer, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > rightBound)
        {
            transform.position = new Vector3(rightBound - boundBuffer, transform.position.y, transform.position.z);
        }
        else if (transform.position.z < bottomBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, bottomBound + boundBuffer);
        }
        else if (transform.position.z > upperBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, upperBound - boundBuffer);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        CollideWithObject();
        InteractWithNail(other);
    }

    /// <summary>
    /// Pushes the character out of other objects collision box.
    /// </summary>
    void CollideWithObject()
    {
        if (horizontalInput > 0)
        {
            transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
        }
        if (horizontalInput < 0)
        {
            transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
        }
        if (forwardInput < 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.1f);
        }

        if (forwardInput > 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.2f);
        }
    }

    /// <summary>
    /// Handles the shortening and sound of the nail.
    /// </summary>
    /// <param name="other"></param>
    void InteractWithNail(Collider other)
    {
        if (other.gameObject.tag == "Nail" && horizontalInput != 0 && forwardInput > 0.5f)
        {
            other.gameObject.transform.localScale -= new Vector3(0.0f, 0.0f, 0.02f);  // Shortens the nail.
            isFiling = true;

            // Plays specific sound when either filing left or right.

            if (horizontalInput > 0)
            {
                audioSource.clip = fileSoundRight;

                if (currentClip == fileSoundLeft)
                {
                    audioSource.Stop();
                }

                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }

                currentClip = fileSoundRight;
            }

            if (horizontalInput < 0)
            {
                audioSource.clip = fileSoundLeft;

                if (currentClip == fileSoundRight)
                {
                    audioSource.Stop();
                }

                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }

                currentClip = fileSoundLeft;
            }

            if (other.gameObject.transform.localScale.z <= 0.25f)  // When short enough the level resets.
            {
                audioSource.Stop();
                levelPassed = true;
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}
