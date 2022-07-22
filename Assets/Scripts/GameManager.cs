using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public int lives = 3;
    public ParticleSystem explosion;
    public float respawnTime = 3.0f;
    public float rit = 3.0f;
    public int score = 0;
    public Text scoreText;
    public Text livesText;

    public void PlayeDead()
    {
        explosion.transform.position = player.transform.position;
        explosion.Play();
        lives--;
        if(lives <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), this.respawnTime);

        }
    }

    private void Update()
    {
        scoreText.text = score.ToString();
        livesText.text = lives.ToString();
    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {

        explosion.transform.position = asteroid.transform.position;
        explosion.Play();

        if(asteroid.size < 0.75f)
        {
            score += 100;
        }else if (asteroid.size < 1.2f)
        {
            score += 50;

        }
        else
        {
            score += 25;

        }
    }

    private void GameOver()
    {
        lives = 3;
        score = 0;
    }

    private void TurnOnCollisions()
    {
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void Respawn()
    {
        player.transform.position = Vector3.zero;
        player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollisions), rit);
    }
}
