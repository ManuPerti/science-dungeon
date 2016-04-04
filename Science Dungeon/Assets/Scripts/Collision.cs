using UnityEngine;
using System.Collections;

/// <summary>
/// Clase que modeliza las consecuencias de la colisión entre el jugador y los enemigos.
/// </summary>
public class Collision : MonoBehaviour
{
    private GameControl gameControl;
    private AudioSource audioSource;

    /// <summary>
    /// Método para inicializar variables que se llama una única vez al iniciar el script
    /// </summary>
    void Start()
    {
        gameControl = GameObject.FindWithTag("GameController").GetComponent<GameControl>();
        audioSource = GetComponents<AudioSource>()[1];
    }

    /// <summary>
    /// Método para actualizar variables que se llama una vez por frame
    /// </summary>
    void Update()
    {

    }

    /// <summary>
    /// Método que controla la colisión del personaje con cualquiera de los enemigos,
    /// restándole un número de gemas y reproduciendo el sonido asociado.
    /// </summary>
    /// <param name="other">El objeto de juego que representa al personaje</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            int maxGems = gameControl.MaximumPosition(gameControl.gems);

            // Si el personaje tiene gemas, el fantasma le quitá de las que más tenga.
            if(maxGems != -1)
            {
                gameControl.gems[maxGems] -= 5;
            }
            
            // Se reproduce el sonido asociado a la colisión con el fantasma.
            audioSource.Play();

        }
    }
}
