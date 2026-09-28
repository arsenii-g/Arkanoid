using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [Header("Estado de la partida")]
    public int puntActual;
    public int ladrillosRes;

    [Header("Escenas")]
    [SerializeField] private string escVictoria = "Victory";
    [SerializeField] private string escDerrota = "Defeat";
    
    void Start()
    {
        ladrillosRes = GameObject.FindGameObjectsWithTag("Brick").Length;

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
        puntActual += puntos;
        ladrillosRes--;

        if(ladrillosRes <= 0)
        {
            GanarNivel();
        }
    }

    public void PerderNivel()
    {
        SceneManager.LoadScene(escDerrota);
    }

    void GanarNivel()
    {
        SceneManager.LoadScene(escVictoria);
    }
}
