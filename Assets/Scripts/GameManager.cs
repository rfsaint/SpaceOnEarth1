using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{ 
private bool isPaused = false;

     void Update()
   {
       if (Input.GetKeyDown(KeyCode.P))
      {
        TogglePause();
      }
   }

     public void TogglePause()
     {
       isPaused = !isPaused;
       Time.timeScale = isPaused ? 0 : 1; // Pausar o tempo do jogo
     }
   }

