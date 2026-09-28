using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    [SerializeField] private float velocidad = 8f;
    [SerializeField] private float limiteX = 7f;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        Vector3 nuevaPosicion = transform.position;

        nuevaPosicion.x += horizontal * velocidad * Time.deltaTime;
        nuevaPosicion.x = Mathf.Clamp(nuevaPosicion.x, -limiteX, limiteX);

        transform.position = nuevaPosicion;
    }
}
