using UnityEngine;
using System;

public class Brick : MonoBehaviour
{
    [Header("Comportamiento de los ladrillos")]
    [SerializeField] private int vidaMax = 1;
    [SerializeField] private int puntos = 100;

    [Header("Sprites destruccion")]
    [SerializeField] private Sprite[] EstadoLadrillo;

    private int vidaActual;
    private SpriteRenderer sr;
    private bool destruido;

    public static event Action<int> LadrilloDestruido;

    void Awake()
    {
        vidaActual = vidaMax;
        sr = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ball"))
        {
            return;
        }

        RecibirGolpe();
    }

    public void RecibirGolpe()
    {
        if (destruido) return;

        vidaActual--;

        if (vidaActual <= 0)
            BreakBrick();
        else
            ChangeDamSprite();
    }

    void ChangeDamSprite()
    {
        if (EstadoLadrillo == null || EstadoLadrillo.Length == 0 || sr == null)
            return;

        int dano = Mathf.Clamp(vidaMax - vidaActual - 1, 0, EstadoLadrillo.Length - 1);
        sr.sprite = EstadoLadrillo[dano];
    }

    void BreakBrick()
    {
        destruido = true;
        GetComponent<Collider2D>().enabled = false;
        LadrilloDestruido?.Invoke(puntos);
        Destroy(gameObject);
    }

}
