using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 12;
    [SerializeField] int maxHits = 10;

    ScoreBoard scoreBoard;

    void Start()
    {
        AddBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    void AddBoxCollider()
    {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    void OnParticleCollision(GameObject other)
    {
        scoreBoard.ScoreHit(scorePerHit);
        maxHits--;
        if (maxHits <= 0)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        Instantiate(deathFX, transform.position, Quaternion.identity, parent);
        Destroy(gameObject);
    }
}
