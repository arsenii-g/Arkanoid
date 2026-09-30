using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [Header("Estado de la partida")]
    public int puntActual;
    public int ladrillosRes;
    public static int puntosFinales;

    [Header("Objetos de la escena")]
    [SerializeField] private Text textoPuntos;
    [SerializeField] private Text textoLadrillos;
    [SerializeField] private Ball bola;
    [SerializeField] private PaddleMovement paddle;

    [Header("Escenas")]
    [SerializeField] private string escVictoria = "Victory";
    [SerializeField] private string escDerrota = "Defeat";

    private bool terminado;

    void Start()
    {
        puntosFinales = 0;
        ladrillosRes = GameObject.FindGameObjectsWithTag("Brick").Length;
        ActualizarTexto();
    }

    void OnEnable()
    {
        Brick.LadrilloDestruido += ProcLadDest;

    }

    void OnDisable()
    {
        Brick.LadrilloDestruido -= ProcLadDest;
    }

    void ProcLadDest(int puntos)
    {
        if (terminado) return;

        puntActual += puntos;
        ladrillosRes--;
        ActualizarTexto();

        if(ladrillosRes <= 0)
        {
            Terminar();
            Invoke(nameof(GanarNivel), 0.3f);
        }
    }

    public void PerderNivel()
    {
        if (terminado) return;

        Terminar();
        SceneManager.LoadScene(escDerrota);
    }

    void GanarNivel()
    {
        SceneManager.LoadScene(escVictoria);
    }

    void Terminar()
    {
        terminado = true;
        puntosFinales = puntActual;
        bola.enabled = false;
        paddle.enabled = false;
    }

    void ActualizarTexto()
    {
        textoPuntos.text = "PUNTOS: " + puntActual;
        textoLadrillos.text = "BLOQUES: " + ladrillosRes;
    }
}
