using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    AudioSource audioSource;
    public AudioClip crashSound;
    public AudioClip finishSound;

    public ParticleSystem crashParticle;
    public ParticleSystem successParticle;

    bool isTransition = false;
    bool disableCollisions = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        HandleDebugKeys();
    }

    private void HandleDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
            LoadNextLevel();
        else if (Input.GetKeyDown(KeyCode.C))
            disableCollisions = !disableCollisions;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransition || disableCollisions)
            return;

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                {
                    break;
                }
            case "Finish":
                {
                    StartSuccessSequence();
                    break;
                }
            default:
                {
                    StartCrashSequence();
                    break;
                }
        }
    }

    private void StartSuccessSequence()
    {
        isTransition = true;
        successParticle.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(finishSound);
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(LoadNextLevel), 1f);
    }

    void StartCrashSequence()
    {
        isTransition = true;
        crashParticle.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadLevel), 1f);
    }

    void ReloadLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    void LoadNextLevel()
    {
        int idx = SceneManager.GetActiveScene().buildIndex + 1;

        if (idx == SceneManager.sceneCountInBuildSettings)
            idx = 0;

        SceneManager.LoadScene(idx);
    }
}
