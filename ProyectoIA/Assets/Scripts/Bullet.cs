using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    int damage = 10;

     /* 
      Es la lógica para la destrucción de los proyectiles.
      Si los proyectiles tocan a un Enemy o al World, se destruye.
    */
    void OnCollisionStay(Collision collision)
    {        
        foreach (ContactPoint contact in collision.contacts) 
        {
           if(contact.otherCollider.gameObject.tag == "Enemy" || contact.otherCollider.gameObject.tag == "World"){
                Destroy(gameObject);
           } 
        }
    }
    public int GetDamage(){
        return damage;
    }
    
}
