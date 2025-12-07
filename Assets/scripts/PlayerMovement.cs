using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Velocidades (editar en Inspector)")]
    [Tooltip("Velocidad de avance/retroceso (unidades por segundo)")]
    public float moveSpeed = 4f;

    [Tooltip("Velocidad de rotación (grados por segundo)")]
    public float rotationSpeed = 120f;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
            Debug.LogError("PlayerMovement requiere CharacterController.");
    }

    void Update()
    {
        // Lectura de inputs 
        float h = Input.GetAxis("Horizontal"); // A/D o flechas izquierda/derecha
        float v = Input.GetAxis("Vertical");   // W/S o flechas arriba/abajo

        // 1) Girar al personaje (sobre el eje Y)
        // Multiplicamos por rotationSpeed y por Time.deltaTime para independencia del FPS
        transform.Rotate(0f, h * rotationSpeed * Time.deltaTime, 0f);

        // 2) Calcular avance en espacio local (z es forward local)
        Vector3 localMove = new Vector3(0f, 0f, v * moveSpeed * Time.deltaTime);

        // 3) Transformar esa dirección local a espacio mundo (para que se mueva hacia donde mira)
        Vector3 worldMove = transform.TransformDirection(localMove);

        // 4) Aplicar el movimiento usando CharacterController.Move
        controller.Move(worldMove);

    }
}
