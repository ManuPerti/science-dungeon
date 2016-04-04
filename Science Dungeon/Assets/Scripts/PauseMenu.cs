using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// Clase que permite detener o reanudar al juego así como mostrar un
/// menú de pausa donde se pueden realizar ciertas acciones como guardar
/// la partida o modificar el volumen de música y efectos sonoros.
/// </summary>
public class PauseMenu : MonoBehaviour {

    public GameObject pausePanel;
    public GameObject confirmSavePanel;
    public GameObject confirmExitPanel;

    private PlayerController player;

    private int titleScreen = 0;                        //Id del menú principal
    private bool isPaused;                              //Booleano para comprobar si el juego está o no en pausa

    /// <summary>
    /// Método que se llama una única vez al iniciar el script antes de que se ejecute
    /// cualquier clase de lógica.
    /// </summary>
    void Awake()
    {
        isPaused = false;
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }
    /// <summary>
    /// Método para actualizar variables que se llama una vez por frame
    /// </summary>
    void Update()
    {
        // Comprobamos que se han pulsado las teclas de Cancel o la letra P y el juego no estaba ya pausado.
        if (player.enabled)
        {
            if ((Input.GetButtonDown("Cancel") || Input.GetKeyDown(KeyCode.P)) && !isPaused)
            {
                DoPause();
            }
            else if ((Input.GetButtonDown("Cancel") || Input.GetKeyDown(KeyCode.P)) && isPaused)
            {
                UnPause();
            } 
        }

    }

    /// <summary>
    /// Método para pausar el juego haciendo que tanto las animaciones como la física se detengan.
    /// </summary>
    public void DoPause()
    {
        isPaused = true;
        // Asignando time.timescale a 0, provoca que tanto las animaciones como la física dejen de actualizarse
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    /// <summary>
    /// Método para salir del modo de pausa y volver al juego.
    /// </summary>
    public void UnPause()
    {
        isPaused = false;
        // Asignando time.timescale a 1, provoca que tanto las animaciones como la física se acualicen a la velocidad normal
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    /// <summary>
    /// Método que muestra el panel de confirmación para guardar la partida.
    /// </summary>
    public void SaveOption()
    {
        confirmSavePanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    /// <summary>
    /// Método que permite confirmar el guardado del juego y volver a la partida actual.
    /// </summary>
    public void SaveGame()
    {
        confirmSavePanel.SetActive(false);
        UnPause();
    }

    /// <summary>
    /// Método que cancela el guardado de juego y vuelve al menú de pausa.
    /// </summary>
    public void CancelSaveGame()
    {
        confirmSavePanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    /// <summary>
    /// Método que sale del menú de pausa y vuelve a la escena principal.
    /// </summary>
    public void ReturnOption()
    {
        UnPause();
    }

    /// <summary>
    /// Método que muestra el panel de confirmación para salir de la partida.
    /// </summary>
    public void ExitGameOption()
    {
        confirmExitPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    /// <summary>
    /// Método para salir de la partida y volver al menú principal
    /// </summary>
    public void ExitGame()
    {
        confirmExitPanel.SetActive(false);
        UnPause();
        SceneManager.LoadScene(titleScreen);
        Resources.UnloadUnusedAssets();

    }

    /// <summary>
    /// Método para cancelar la opción de salir de la partida y volver al menú de pausa
    /// </summary>
    public void CancelExitGame()
    {
        confirmExitPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

}
