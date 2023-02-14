using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
     const string SCENE_NAME = "Área Inicial";
     public StartingAreaSO sa;

     public void PlayGame(){
          StartNewGame();
          SceneManager.LoadScene(SCENE_NAME);
     }

     public void QuitGame(){
          Application.Quit();
     }

     private void StartNewGame(){
          sa.newGame = true;
     }
     
}
