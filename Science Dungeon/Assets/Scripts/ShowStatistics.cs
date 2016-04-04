using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Clase que permite mostrar al jugador los resultados obtenidos al finalizar
/// la partida como tiempo empleado y porcentajes de aciertos.
/// </summary>
public class ShowStatistics : MonoBehaviour {

    // Referencias a los paneles y textos de la intefaz gráfica.
    public GameObject gamePanel;
    public GameObject statsPanel;
    public Text[] gemsText;
    public Text timeText;

    private GameControl gameControl;
    private PlayerController player;

    /// <summary>
    /// Método para inicializar variables que se llama una única vez al iniciar el script
    /// </summary>
    void Start () {
        gameControl = GameObject.FindWithTag("GameController").GetComponent<GameControl>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    /// <summary>
    /// Método para actualizar variables que se llama una vez por frame
    /// </summary>
    void Update () {
	
	}

    /// <summary>
    /// Método que se activa por el evento de colisión entre el personaje y el cofre.
    /// </summary>
    /// <param name="other">El objeto de juego que representa al personaje</param>
    void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            // Paramos todos los eventos del juego y mostramos las estadísticas.
            player.enabled = false;
            Time.timeScale = 0;
            gamePanel.SetActive(false);
            statsPanel.SetActive(true);

            // Rellenamos los textos con la información que hemos ido almacenando.
            gameControl.FillGems(gemsText);
            timeText.text = FormatTimer(gameControl.globalTime);
        }
    }

    /// <summary>
    /// Método que permite calcular el porcentaje de aciertos en los minijuegos.
    /// </summary>
    /// <param name="gamePosition">Posición que identifica a cada minijuego en el vector
    /// 'games'</param>
    /// <returns>Un entero con el porcentaje de aciertos</returns>
    public int CalculatePercentage(int gamePosition)
    {
        int percentage = 0;
        int totalCorrects = gameControl.corrects[gamePosition];
        int totalQuestions = gameControl.games[gamePosition] * 5;
        
        if(totalQuestions != 0)
        {
            percentage = Mathf.RoundToInt(100 * totalCorrects / totalQuestions);
        } else
        {
            percentage = 0;
        }
        return percentage;
    }
        
    /// <summary>
    /// Método que muestra para cada minijuego el porcentaje de aciertos con un formato adecuado
    /// cada vez que se hace click en el botón correspondiente.
    /// </summary>
    /// <param name="buttonClick">El botón sobre el que se hace click en cada caso</param>
             
    public void ShowPercentage(Button buttonClick)
    {
        string typeGame = buttonClick.name;

        // Para cada juego se llama al método para calcular el porcentaje de aciertos.
        switch (typeGame)
        {
            case "Maths":
                buttonClick.GetComponentInChildren<Text>().text = CalculatePercentage(0) + "%";
                break;

            case "Spatial":
                buttonClick.GetComponentInChildren<Text>().text = CalculatePercentage(1)*5 + "%";
                break;

            case "Logic":
                buttonClick.GetComponentInChildren<Text>().text = CalculatePercentage(2) + "%";
                break;

            case "Quiz":
                buttonClick.GetComponentInChildren<Text>().text = CalculatePercentage(3) + "%";
                break;

        }

        buttonClick.GetComponentInChildren<Text>().fontSize = 36;

    }

    /// <summary>
    /// Método que muestra en un formato 0:00:00 (horas, minutos y segundos) el tiempo empleado en
    /// resolver el juego completo.
    /// </summary>
    /// <param name="timer">El tiempo empleado en segundos</param>
    /// <returns>Una cadena de texto con el tiempo adecuadamente formateado en 0:00:00</returns>
    public string FormatTimer(float timer)
    {
        int hours = Mathf.FloorToInt(timer / 3600F);
        int minutes = Mathf.FloorToInt((timer - hours * 3600) / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);

        return string.Format("{0:0}:{1:00}:{2:00}", hours, minutes, seconds);

    }

}
