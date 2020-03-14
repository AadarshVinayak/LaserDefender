using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Config params
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0.5f;
    [SerializeField] int health = 500;
    [SerializeField] SceneLoader loader;

    [Header("Projectile")]
    [SerializeField] float projectileSpeed = 12f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    [SerializeField] AudioClip projectileSound;
    [SerializeField] [Range(0, 1)] float projectileSoundVolume = 0.1f;
    //Cached refecences
    [SerializeField] GameObject laserPrefab;

    [Header("SFX/VFX")]
    [SerializeField] GameObject explostionPrefab;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.5f;
    [SerializeField] AudioClip deathSound;


    Coroutine firingCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;




    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        DamageDealer damageDealter = collider.gameObject.GetComponent<DamageDealer>();
        if (!damageDealter) return;
        health -= damageDealter.GetDamage();
        damageDealter.Hit();
        if (health <= 0)
        {
            DeathOnHit();
        }
    }

    private void DeathOnHit()
    {
        GameObject explosionAnimation = Instantiate(explostionPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(explosionAnimation, 1f);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        loader.LoadLoseScene();
        Destroy(gameObject);
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(ContinueFiring());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator ContinueFiring()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(projectileSound, Camera.main.transform.position, projectileSoundVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }

    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    public int GetHealth()
    {
        return health;
    }

}
