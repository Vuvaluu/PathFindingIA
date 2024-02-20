using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] protected float maxSpeed, maxForce, slowingRad;
    protected int maxHP = 100;
    [SerializeField] protected int currentHP;
    [SerializeField] protected int damage = 1;
    public Rigidbody rb;
    public Player player;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public float volume = 0.2f;
    public PathFinding aStar;
    public Dijkstra dijkstra; 

     /* 
      Se utiliza protected para que sea privada y
        pueda ser heredada. Y se utiliza virtual para que el 
        método start pueda ser remplazado.
    */
    protected virtual void Start() 
    {
        currentHP = maxHP;
        
    }

     /* 
        En este método detecta si es impactado por un proyectil y
        calcula su vida restante. También le resta vida la jugador si
        el MeleeAgent está tocandolo.
    */
     void OnCollisionStay(Collision collision)
    {        
        foreach (ContactPoint contact in collision.contacts) 
        {
            if(contact.otherCollider.gameObject.tag == "Bullet"){
                TakeDMG(contact.otherCollider.GetComponent<Bullet>().GetDamage());
            }
            if (contact.otherCollider.gameObject.tag == "Player")
            {
                contact.otherCollider.GetComponent<Player>().TakeDMG(damage);
            }
        }
    }

    void FixedUpdate() 
    { 
        if(aStar != null){
            SteeringBehaviours.PathFollowing(this, aStar.GetPathTransform());
        } else {
            SteeringBehaviours.PathFollowing(this, dijkstra.GetPathTransform());
        }
        
    }

     /* 
        En este método se utiliza audioSource para que reproducir
        un sonido cuando el enemigo recibe daño. Y la lógica de la
        muerte del agente y también se calcula la vida restante cuando
        recibe daño.
    */
    public void TakeDMG(int dmg) 
    {   
        audioSource.PlayOneShot(audioClip, volume);
                   
        currentHP = currentHP - dmg;
        if (currentHP <= 0 ) 
        {
            Destroy(gameObject);
        }
    }

    public int getEnemyDMG() 
    {
        return damage;
    }

    public int getCurrentHP() 
    {
        return currentHP;
    }

    public float getMaxSpeed()
    {
        return maxSpeed;
    }

    public float getMaxForce()
    {
        return maxForce;
    }

    public float getSlowingRad()
    {
        return slowingRad;
    }

}
