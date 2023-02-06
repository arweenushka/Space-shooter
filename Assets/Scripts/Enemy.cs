using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Characteristics")]
    [SerializeField] float health = 1;
    private int scoreValue = 1;

    [Header("shooting from the enemy")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject enemyLaserPrefab = null;
    [SerializeField] float enemyLaserPrefabSpeed = 10f;

    [Header("Audios")]
    [SerializeField] AudioClip deathSound = null;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.75f;
    [SerializeField] AudioClip shootSound = null;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;

    [Header("Particle Effects")]
    GameObject explosion;
    [SerializeField] GameObject deathVFX = null;
    [SerializeField] float durationOfExplosion = 1f;


    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Shoot();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Shoot()
    {
        //initiate enemy laser prefab and move it forward with certain speed
        GameObject enemyLaser = Instantiate(enemyLaserPrefab, transform.position, Quaternion.identity);
        PlayShootingSound();
        enemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyLaserPrefabSpeed);
    }

    //when enemy collides with player shoot then take a damage
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        //if object doesn´t have damage dealer component do nothing related to taking damage
        if (!damageDealer)
        {
            return;
        }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        //destroy the laser
        damageDealer.Hit();
        //destroy the enemy when health is 0
        if (health <= 0)
        {
            Die();
        }
    }

    //destroy enemy 
    private void Die()
    {
        //every time when enemy die add value to score 
        gameSession.AddToScore(scoreValue);
        //destroy enemy
        Destroy(gameObject);
        PlayDeathSound();
        ShowDeathExplosion();
    }


    private void PlayDeathSound()
    {
        //play enemy die sound 
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }

    private void PlayShootingSound()
    {
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
    }

    private void ShowDeathExplosion()
    {
        //show explosion
        explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        //destroy explosion particle effects 
        Destroy(explosion, durationOfExplosion);
    }
}
