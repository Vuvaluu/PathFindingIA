using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

   /* 
     Método que se usa para cargar una escena.
    */
   public void LoadScene(string sceneName)
   {
     SceneManager.LoadScene(sceneName);
   }

   /* 
     Método que se utiliza para salir del juego.
    */
   public void QuitJueguito()
   {
     Application.Quit();
   }

   
}
