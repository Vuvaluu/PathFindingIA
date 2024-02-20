using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckWin : MonoBehaviour
{
    public GameObject enemies;

    /* 
     Es la lógica de la pantalla "Win".
     Si el número de enemigos es 0, se muestra la pantalla "Win".
    */
    void Update(){
        if (enemies.transform.childCount <= 0) {
        SceneManager.LoadScene("Win");
        }

    }
}
