using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("In seconds")] [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] GameObject explosion;

    void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
        explosion.SetActive(true);
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(1);
    }
}
