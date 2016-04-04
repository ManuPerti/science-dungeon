using UnityEngine;
using System.Collections;

/// <summary>
/// Clase que  permite rotar la vista del jugador para observar el entorno.
/// </summary>
public class MouseLooker : MonoBehaviour {

    // Variables para configurar la sensibilidad y límites del movimiento.
    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    public bool clampVerticalRotation = true;
    public float minimumX = -90F;
    public float maximumX = 90F;
    public bool smooth;
    public float smoothTime = 5f;

    // Variables privadas del personaje y la cámara
    private Quaternion m_CharacterTargetRot;
    private Quaternion m_CameraTargetRot;
    private Transform character;
    private Transform cameraTransform;

    /// <summary>
    /// Método para inicializar variables que se llama una única vez al iniciar el script
    /// </summary>
    void Start()
    {
        // Comenzamos el juego con el cursor oculto.
        LockCursor(true);

        // Referencias al personaje y la cámara.
        character = gameObject.transform;
        cameraTransform = Camera.main.transform;

        // Rotaciones locales del personaje y la cámara.
        m_CharacterTargetRot = character.localRotation;
        m_CameraTargetRot = cameraTransform.localRotation;
    }

    /// <summary>
    /// Método para actualizar variables que se llama una vez por frame
    /// </summary>
    void Update()
    {
        // Rotamos la vista siguiendo el movimiento del ratón.
        LookRotation();

        // Al pulsar "Escape" se muestra el cursor.
        if (Input.GetButtonDown("Cancel"))
        {
            LockCursor(false);
        }

        /* Función aún no implementada
        // Si el personaje dispara, volvemos a fijar el cursor.
        if (Input.GetButtonDown("Fire1"))
        {
            LockCursor(true);
        }*/
    }

    /// <summary>
    /// Método que esconde (fija) o muestra el cursor.
    /// </summary>
    /// <param name="isLocked">Booleano que indica si se muestra o esconde el cursor</param>
    private void LockCursor(bool isLocked)
    {
        if (isLocked)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    /// <summary>
    /// Método que realiza la rotación de la vista de la cámara en base a los
    /// movimientos del ratón.
    /// </summary>
    public void LookRotation()
    {
        // Obtenemos las rotaciones locales en los ejes X e Y
        float yRot = Input.GetAxis("Mouse X") * XSensitivity;
        float xRot = Input.GetAxis("Mouse Y") * YSensitivity;
        m_CharacterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
        m_CameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);

        // Si fijamos la rotación vertical
        if (clampVerticalRotation)
            m_CameraTargetRot = ClampRotationAroundXAxis(m_CameraTargetRot);

        // Evitamos un movimiento brusco de la cámara
        if (smooth)
        {
            character.localRotation = Quaternion.Slerp(character.localRotation, m_CharacterTargetRot,
                                                        smoothTime * Time.deltaTime);
            cameraTransform.localRotation = Quaternion.Slerp(cameraTransform.localRotation, m_CameraTargetRot,
                                                     smoothTime * Time.deltaTime);
        }
        else
        {
            character.localRotation = m_CharacterTargetRot;
            cameraTransform.localRotation = m_CameraTargetRot;
        }
    }

    /// <summary>
    /// Método que fija al eje X la rotación aplicada.
    /// </summary>
    /// <param name="q">Un objeto de tipo Quaternion con el ángulo de rotación</param>
    /// <returns>Un objeto de tipo Quaternion vinculado al eje X</returns>
    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, minimumX, maximumX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }
}
