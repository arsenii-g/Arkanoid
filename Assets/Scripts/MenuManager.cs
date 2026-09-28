using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager:MonoBehaviour {
    public void cargarNivel(){
        SceneManager.LoadScene("Nivel 1");

    }
    public void salirJuego(){
        Application.Quit();
    }   
}