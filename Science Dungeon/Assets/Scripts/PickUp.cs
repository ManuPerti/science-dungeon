using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Clase que modeliza el efecto de recoger determinados objetos del plano al
/// colisionar el jugador con ellos.
/// </summary>
public class PickUp : MonoBehaviour {

    private GameControl gameControl;
    private AudioSource audioSource;
    private int keyId;

    /// <summary>
    /// Método para inicializar variables que se llama una única vez al iniciar el script
    /// </summary>
    void Start () {

        gameControl = GameObject.FindWithTag("GameController").GetComponent<GameControl>();

    }

    /// <summary>
    /// Método para actualizar variables que se llama una vez por frame
    /// </summary>
    void Update () {
	
	}

    /// <summary>
    /// Método que simula el efecto de recoger un objeto al colisionar con el personaje.
    /// </summary>
    /// <param name="other">El objeto de juego que representa al personaje</param>
    void OnTriggerEnter(Collider other)
    {
        // Variable que identifica la llave que se ha recogido.
        keyId = Int32.Parse(gameObject.name.Substring(gameObject.name.Length - 1));

        if (other.tag.Equals("Player"))
        {
            // Se reproduce el sonido asociado a recoger un objeto.
            audioSource = other.GetComponents<AudioSource>()[2];
            audioSource.Play();

            // Se almacena la llave recogida por el personaje.
            gameControl.keys[keyId] = true;

            // Se destruye el objeto.
            Destroy(gameObject);
        }
    }
}
