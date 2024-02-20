using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    int maxHP = 300;
    int currentHP;
    int damage = 10;
    public Image hpBarSprite;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public float volume = 0.2f;
    private float timer;

    void Start()
    {
        currentHP = maxHP;
        timer = 0;
    }

     /* 
      Actualiza la barra de vida.
    */
    void Update(){
        hpBarSprite.fillAmount = (float)currentHP/(float)maxHP;
    }

     /* 
      Lógica para calcular la vida restante 
      del jugador si recibe daño.
    */
    public void TakeDMG(int dmg){
         timer += Time.deltaTime;
        if (timer > 1.5f)
        {
            audioSource.PlayOneShot(audioClip, volume);
            timer = 0;
        }        
        currentHP = currentHP - dmg;
        if(currentHP <= 0){
           SceneManager.LoadScene("Lost");
        }
    }

    public int GetDamage(){
        return damage;
    }
}
