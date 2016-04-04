using UnityEngine;
using System.Collections;

/// <summary>
/// Clase que permite abandonar completamente el juego cerrando la aplicación.
/// </summary>
public class QuitApplication : MonoBehaviour
{
    /// <summary>
    /// Método para cerrar la aplicación si ésta se está ejecutando.
    /// </summary>
    public void Quit()
    {
        // Si la aplicación se está ejecutando aparte, salimos.
        if (Application.isPlaying)
        {
            Application.Quit();
        }

        // Si la aplicación se está ejecutando en el editor, la cerramos.
       /*if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }*/
            
    }
}

