using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultadoPartida : MonoBehaviour
{
    [SerializeField] private Text textoPuntos;

    void Start()
    {
        textoPuntos.text = "PUNTOS: " + GameManager.puntosFinales;
    }

    public void Repetir()
    {
        SceneManager.LoadScene("Nivel 1");
    }

    public void VolverMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
