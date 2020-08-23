using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    public bool PrefabInStartLevel;

    public AudioSource ownAudioSource;

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && PrefabInStartLevel)
        {
            LoadNextLevel();
        }

    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadNextLevel(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator LoadNextLevel(int levelIndex)
    {

        // Play Animation
        transition.SetTrigger("Start");

        // Play Sound
        ownAudioSource.Play();

        // wait
        yield return new WaitForSeconds(transitionTime);

        // load next scene.
        SceneManager.LoadScene(levelIndex + 1);
    }

    IEnumerator LoadLevel(int levelIndex)
    {

        // Play Animation
        transition.SetTrigger("Start");

        // Play Sound
        //ownAudioSource.Play();

        // wait
        yield return new WaitForSeconds(transitionTime);

        // load next scene.
        SceneManager.LoadScene(levelIndex);
    }



    public void LoadFromStart()
    {
        StartCoroutine(LoadLevel(0));
    }
}
