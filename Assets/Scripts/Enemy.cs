using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Stats")]
    [SerializeField] float health = 100;
    [SerializeField] int score = 10;
    GameSession session;

    [Header("Projectile")]
    [SerializeField] bool willShoot = false;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots=0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float projectileSpeed;
    [SerializeField] AudioClip projectileSound;
    [SerializeField] [Range(0,1)] float projectileSoundVolume = 0.5f;
    [SerializeField] GameObject laserPrefab;

    [Header("VFX/SFX")]
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.5f;
    [SerializeField] GameObject explostionPrefab;
    [SerializeField] AudioClip deathSound;
    // Start is called before the first frame update
    void Start()
    {
        session = FindObjectOfType<GameSession>();
        if(willShoot)shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        if(willShoot)CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(projectileSound, Camera.main.transform.position, projectileSoundVolume);
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        DamageDealer damageDealer = collider.gameObject.GetComponent<DamageDealer>();
        DestroyOnHit(damageDealer);
    }

    private void DestroyOnHit(DamageDealer damageDealer)
    {
        if (!damageDealer) return;
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health == 0)
        {
            DeathOnHit();
        }
    }

    private void DeathOnHit()
    {
        session.AddToScore(score);
        Destroy(gameObject);
        GameObject explosionAnimation = Instantiate(explostionPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(explosionAnimation, 1f);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }
}
