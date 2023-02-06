using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movements")]
    [SerializeField] float moveSpeed = 10f;
    private float horizontalInput;
    private float verticalInput;

    [Header("Borders")]
    private float xBound = 8.0f;
    private float yBound = 6.0f;

    [Header("Shooting")]
    [SerializeField] GameObject laserPrefab = null;
    [SerializeField] float projectileSpeed = 20f;
    private float shootPeriod = 0.05f;
    Coroutine shootingCoroutine;

    [Header("Audios")]
    [SerializeField] AudioClip shootSound = null;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;
    [SerializeField] AudioClip getDamageSound = null;
    [SerializeField] [Range(0, 1)] float getDamageSoundVolume = 2f;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ConstrainPlayerPosition();
        Move();
        Shoot();
    }

    //player can move in any direction
    private void Move()
    {
        //player can move on the horizontal axis
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * horizontalInput * Time.deltaTime * moveSpeed);

        //player can move on the vertical axis
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector2.up * verticalInput * Time.deltaTime * moveSpeed);
    }

    
    //prevent player go outside the bounds
    void ConstrainPlayerPosition()
    {
        if (transform.position.x < -xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }
        if (transform.position.y < -yBound)
        {
            transform.position = new Vector3(transform.position.x, -yBound, transform.position.z);
        }
        if (transform.position.y > yBound)
        {
            transform.position = new Vector3(transform.position.x, yBound, transform.position.z);
        }
    }

    //shooting after pressing on some button with coroutine to be able to shot with hold button
    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shootingCoroutine = StartCoroutine(ShootContinuously());
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StopCoroutine(shootingCoroutine);
        }
    }

    //coroutine for shooting continously with during the holding button
    IEnumerator ShootContinuously()
    {
        while (true)
        {
            //initiate laser prefab and move it forward with certain speed
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            PlayShootingSound();
            yield return new WaitForSeconds(shootPeriod);
        }
    }

    private void PlayShootingSound()
    {
        //player shooting sound
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        //if object doesn´t have damage dealer component do nothing related to taking damage
        if (!damageDealer)
        {
            return;
        }
        //take damage when player collides with enemy or bullet from enemy
        FindObjectOfType<HealthDisplay>().TakeDamageFromEnemy(damageDealer);
        PlayGetDamageSound();
    }

    private void PlayGetDamageSound()
    {
        //player get damage sound
        AudioSource.PlayClipAtPoint(getDamageSound, Camera.main.transform.position, getDamageSoundVolume);
    }
}
