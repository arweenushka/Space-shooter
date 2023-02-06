using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [Header("Player Characteristics")]
    public GameObject[] hearts;
    private int life;

    [Header("Audios")]
    [SerializeField] AudioClip deathSound = null;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.6f;

    [Header("Particle Effects")]
    GameObject explosion;
    [SerializeField] GameObject deathVFX = null;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] GameObject getDamageVFX = null;


    // Start is called before the first frame update
    void Start()
    {
        life = hearts.Length;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //call this method for taking damage from different objects
    public void TakeDamageFromEnemy(DamageDealer damageDealer)
        {
            ShowGetDamageExplosionEffect();
            //every time when take a damage -1 heart from the player
            if (life >= 1)
            {
                damageDealer.Hit();
            //TODO remove hardcoded value
                life -= 1;
                Destroy(hearts[life].gameObject);


                //when hearts = 0 game over
                if (life < 1)
                {
                    Die();
                }
            }
        }

    //destroy player and load Game Over scene
    public void Die()
    {
        //load Game over scene
        FindObjectOfType<GameManager>().LoadGameOver();
        Destroy(gameObject);
        PlayDeathSound();
        ShowDeathExplosion();
    }

    private void PlayDeathSound()
    {
        //player dieing sound
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }

    private void ShowDeathExplosion()
    {
        //show explosion
        explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        //destroy explosion particle effects 
        Destroy(explosion, durationOfExplosion);
    }

    private void ShowGetDamageExplosionEffect()
    {
        //show explosion
        explosion = Instantiate(getDamageVFX, transform.position, transform.rotation);
        //destroy explosion particle effects 
        Destroy(explosion, durationOfExplosion);
    }
}
