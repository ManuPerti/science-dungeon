using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/// <summary>
/// Clase que controla la lógica de la pantalla de título, contiene los métodos
/// asociados a cada botón para mostrar las opciones de nuevo juego, cargar juego
/// guardado, modificar volumen y salir.
/// </summary>
public class StartOptions : MonoBehaviour
{
    // Nombre de la escena que se cargará.
    public string sceneToStart;

    // Referencias a cada uno de los paneles de la interfaz gráfica.
    public GameObject menuPanel;
    public GameObject settingsPanel;
    public GameObject confirmNewPanel;
    public GameObject confirmLoadPanel;
    public GameObject confirmQuitPanel;

    //********* MENÚ PRINCIPAL *********//

    /// <summary>
    /// Método que muestra el panel de confirmación de nuevo juego.
    /// </summary>
    public void NewGameOption()
    {
        confirmNewPanel.SetActive(true);
        menuPanel.SetActive(false);
        Debug.Log("Pregunta al usuario si desea iniciar un nuevo juego");
    }

    /// <summary>
    /// Método que muestra el panel de confirmación de cargar juego.
    /// </summary>
    public void LoadGameOption()
    {
        confirmLoadPanel.SetActive(true);
        menuPanel.SetActive(false);
        Debug.Log("Pregunta al usuario si desea cargar el juego guardado");
    }

    /// <summary>
    /// Método que muestra el panel de ajustes.
    /// </summary>
    public void SettingsOption()
    {
        settingsPanel.SetActive(true);
        menuPanel.SetActive(false);
        Debug.Log("Muestra opciones de sonido");
    }

    /// <summary>
    /// Método que borra los datos de partida almacenada y carga la escena principal. 
    /// </summary>
    public void StartNewGame()
    {
        DeleteGameData();
        SceneManager.LoadScene(sceneToStart);
    }

    /// <summary>
    /// Método que cancela la creación de un nuevo juego y vuelve al menú de título.
    /// </summary>
    public void CancelNewGame()
    {
        confirmNewPanel.SetActive(false);
        menuPanel.SetActive(true);
       
    }

    /// <summary>
    /// Método que permite cargar la siguiente escena.
    /// </summary>
    public void LoadGame()
    {
        SceneManager.LoadScene(sceneToStart);
    }

    /// <summary>
    /// Método que cancela la carga de juego guardado y vuelve al menú de título.
    /// </summary>
    public void CancelLoadGame()
    {
        confirmLoadPanel.SetActive(false);
        menuPanel.SetActive(true);

    }

    /// <summary>
    /// Método que permite cerra el panel de ajustes.
    /// </summary>
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    /// <summary>
    /// Método que abre el panel de confirmación para salir del juego.
    /// </summary>
    public void QuitOption()
    {
        confirmQuitPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    /// <summary>
    /// Método que permite cancelar la opción de salir del juego y vuelve al menú de título.
    /// </summary>
    public void CancelQuitOption()
    {
        confirmQuitPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    /// <summary>
    /// Método que permite borrar los datos de partidas almacenadas, si estos existen.
    /// </summary>
    public void DeleteGameData()
    {
        if (File.Exists("Saves/playerInfo.dat")) {

            File.Delete("Saves/playerInfo.dat");

        }

    }

}
