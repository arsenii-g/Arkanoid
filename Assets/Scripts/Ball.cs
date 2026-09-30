using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float velocidad = 8f;
    [SerializeField] private float radio = 0.25f;
    [SerializeField] private float limiteInferior = -5f;
    [SerializeField] private Collider2D paddle;
    [SerializeField] private LayerMask escenario;
    [SerializeField] private GameManager juego;
    [SerializeField] private AudioClip sonidoLanzar;
    [SerializeField] private AudioClip sonidoRebote;
    [SerializeField] private AudioClip sonidoLadrillo;

    private Vector2 direccion;
    private bool lanzada;
    private AudioSource audioBola;

    void Awake()
    {
        audioBola = GetComponent<AudioSource>();
    }

    void LateUpdate()
    {
        if (!lanzada)
        {
            transform.position = new Vector3(paddle.bounds.center.x,
                paddle.bounds.max.y + radio + 0.05f, 0);

            if (Input.GetKeyDown(KeyCode.Space))
                Lanzar();

            return;
        }

        Mover(velocidad * Time.deltaTime);

        if (transform.position.y < limiteInferior)
            juego.PerderNivel();
    }

    public void Lanzar()
    {
        if (lanzada) return;

        lanzada = true;
        direccion = new Vector2(0.35f, 1f).normalized;
        audioBola.PlayOneShot(sonidoLanzar);
    }

    void Mover(float distancia)
    {
        Physics2D.SyncTransforms();
        int rebotes = 0;

        // Comprobamos el recorrido antes de mover la bola.
        while (distancia > 0 && rebotes < 4)
        {
            RaycastHit2D choque = Physics2D.CircleCast(transform.position,
                radio, direccion, distancia, escenario);

            if (choque.collider == null)
            {
                transform.position += (Vector3)(direccion * distancia);
                break;
            }

            transform.position = choque.centroid + choque.normal * 0.01f;
            distancia -= choque.distance;

            if (choque.collider == paddle)
            {
                float zona = (choque.centroid.x - paddle.bounds.center.x)
                    / paddle.bounds.extents.x;
                direccion = new Vector2(Mathf.Clamp(zona, -1f, 1f), 1f).normalized;
            }
            else
            {
                direccion = Vector2.Reflect(direccion, choque.normal).normalized;
            }

            // Evita que la bola se quede viajando casi horizontalmente.
            if (Mathf.Abs(direccion.y) < 0.2f)
            {
                direccion.y = direccion.y >= 0 ? 0.2f : -0.2f;
                direccion.Normalize();
            }

            Brick ladrillo = choque.collider.GetComponent<Brick>();
            if (ladrillo != null)
            {
                ladrillo.RecibirGolpe();
                audioBola.PlayOneShot(sonidoLadrillo);
            }
            else
            {
                audioBola.PlayOneShot(sonidoRebote);
            }

            rebotes++;
        }
    }
}
