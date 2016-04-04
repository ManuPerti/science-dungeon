using UnityEngine;
using System.Collections;

/// <summary>
/// Clase que impide que se destruya el objeto asociado al cargar otra escena de juego
/// </summary>
public class DontDestroy : MonoBehaviour {

    /// <summary>
    /// Método que se llama una única vez al inicar el script antes de que se ejecute
    /// cualquier tipo de lógica.
    /// </summary>
	void Awake () {
        // El objeto de juego no se destruirá al cargar otra escena de juego
        DontDestroyOnLoad(gameObject);
    }

}
