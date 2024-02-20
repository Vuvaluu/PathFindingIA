using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SteeringBehaviours
{
   
  public static void seek(Enemy agent, Transform target)
  {
    Vector3 desiredVel = target.position - agent.transform.position;
    desiredVel.Normalize();
    desiredVel *= agent.getMaxSpeed();
    desiredVel = arrival(agent, target.position, desiredVel);
    Rigidbody rb = agent.GetComponent<Rigidbody>();
    Vector3 steeringForce = desiredVel - rb.velocity;
    steeringForce = Vector3.ClampMagnitude(steeringForce, agent.getMaxForce());
    steeringForce /= rb.mass;
    rb.velocity = Vector3.ClampMagnitude((rb.velocity + steeringForce) , agent.getMaxForce());   
  }


  public static Vector3 arrival(Enemy agent, Vector3 target, Vector3 desiredVel)
  {
   float distance = Vector3.Distance(agent.transform.position, target);
         if(distance <= agent.getSlowingRad())
      {
          desiredVel.Normalize();
          desiredVel *= agent.getMaxSpeed() * (distance / agent.getSlowingRad());
      }
      return desiredVel;
  }

   /* 
     Este steering behaviour hace que el agente siga un path de transforms.
    */
  public static void PathFollowing(Enemy agent, List<GameObject> pathTransform) 
  {
    // currentTarget es un punto en el path al que el agente le hace seek.
       Transform currentTarget = null;

    // dist es la distancia entre el agente y el currentTarget.
       float dist = 0;
       int i = 0;
       if (currentTarget == null) {
      i = 0;

    // El agente está en el principio del path por lo tanto asignarle el primer transform del path.
      if(i <= pathTransform.Count - 1){
        currentTarget = pathTransform[i].transform;
      }
  }

    // Hacer seek al currentTarget(Algun punto del path).
    if(currentTarget != null){
        seek(agent, currentTarget);
        // Calcular la distancia entre el agente y el currentTarget.
      dist = Vector3.Distance(agent.transform.position, currentTarget.position);
    }
    
    // Si el agente está cerca del nodo, asignar al siguiente nodo del path como el currentNode.
    if(dist <= 3){
      // Checar si llegó al último nodo, si no, asignarle el siguiente.
      if (i == pathTransform.Count - 1) {
        // Detenerte porque llegaste al final del path.
      } else {
        // Asignarl el siguiente nodo en el path.
        i ++;
        if(currentTarget != null){
          currentTarget = pathTransform[i].transform;
        }
        
      }
    }
        
  }
}
