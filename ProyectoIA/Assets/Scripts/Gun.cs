using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 100;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public float volume = 0.5f;

     /* 
      Lógica del disparo.
      Si le picas Space, sale el proyectil.
    */
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.PlayOneShot(audioClip, volume);
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        }
    }
}
