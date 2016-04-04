using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

/// <summary>
/// Clase que controla la mecánica del minijuego de cálculo mental MATHS.
/// </summary>

public class MathsManager : MonoBehaviour {

    public static MathsManager gm;

    // Referencias a los paneles de la interfaz gráfica.
    public GameObject infoPanel;
    public GameObject welcomePanel;
    public GameObject mainPanel;
    public GameObject invisiblePanel;
    public GameObject resultsPanel;

    // Referencias a otros elementos de la interfaz gráfica.
    public GameObject[] buttons;
    public GameObject nextPuzzle;
    public GameObject operation;
    public GameObject message;
    public GameObject timeout;
    
    // Variables que almacenan condiciones del minijuego.
    public float timeResponse;
    public float numberOfPuzzles;

    private GameControl gameControl;
    private GameObject player;
    private GameObject[] enemies;

    // Variables que gestionan la lógica del minijuego.
    private float timeLeft;
    private int count;
    private int actualOperation;
    private int resultOperation;
    private bool answered;
    private bool gameRunning;

    // Variables que almacenan los resultados del minijuego.
    private int correct;
    private int numberOfGems;


    /// <summary>
    /// Método para inicializar variables que se llama una única vez al iniciar el script.
    /// </summary>
    void Start () {
        if (gm == null)
            gm = gameObject.GetComponent<MathsManager>();

        player = GameObject.FindWithTag("Player");

        gameControl = GameObject.FindWithTag("GameController").GetComponent<GameControl>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Welcome();
	}

    /// <summary>
    /// Método para actualizar variables que se llama una vez por frame.
    /// </summary>
    void Update () {

        if(!answered && gameRunning)
        {
            if(timeLeft >= 0)
            {
                timeout.GetComponent<Text>().text = timeLeft.ToString("#");
                timeLeft -= Time.deltaTime;

            }
            else
            {
                message.GetComponent<Text>().text = "¡Se acabó el tiempo!\n El resultado correcto era: " + resultOperation;
                invisiblePanel.SetActive(true);
            }
        }
        
	}

    /// <summary>
    /// Método que muestra el panel con las instrucciones de juego y resetea todas las variables
    /// a su valor inicial para asegurar cada ejecución independiente del minijuego.
    /// </summary>
    public void Welcome()
    {
        infoPanel.SetActive(false);
        mainPanel.SetActive(false);
        resultsPanel.SetActive(false);
        welcomePanel.SetActive(true);

        GetComponent<AudioSource>().enabled = true;

        count = 0;
        actualOperation = 0;
        answered = false;
        gameRunning = false;
        correct = 0;
        numberOfGems = 0;
    }

    /// <summary>
    /// Método que se ejecuta con cada puzzle del minijuego.
    /// </summary>
    public void InitGame()
    {
        welcomePanel.SetActive(false);
        mainPanel.SetActive(true);

        if (!gameRunning)
        {
            gameRunning = true;
        }

        
        timeLeft = timeResponse;

        // Se ejecuta hasta que se alcance el número de puzzles indicado.
        if (count < numberOfPuzzles)
        {
            System.Random r = new System.Random();
            actualOperation = r.Next(0, 4);

            message.GetComponent<Text>().text = "";
            invisiblePanel.SetActive(false);

            resultOperation = RandomOperation(actualOperation);

            FillButtons();

            count++;

            answered = false;

        }
        else
        {
            gameRunning = false;
            invisiblePanel.SetActive(false);
            resultsPanel.SetActive(true);
            numberOfGems = 5 * correct;

            // Si al menos se ha contestado una pregunta correctamente, se mostrarán las gemas obtenidas y música de éxito.
            if (correct > 0)
            {
                GetComponents<AudioSource>()[0].Stop();
                GetComponents<AudioSource>()[1].Play();
                resultsPanel.GetComponentInChildren<Text>().text = "¡Ronda finalizada!\nHas tenido " + correct + " aciertos y\nganado " + numberOfGems + " gemas amarillas";
            }
            // Si no se ha contestado ninguna pregunta bien, se mostrará mensaje y música acordes a situación de fallo.
            else
            {
                GetComponents<AudioSource>()[0].Stop();
                GetComponents<AudioSource>()[2].Play();
                resultsPanel.GetComponentInChildren<Text>().text = "¡Ronda finalizada!\nLo siento pero no has\nganado ninguna gema";
            }
        }

    }

    /// <summary>
    /// Método que recibe como parámetro un entero que indica el tipo de operación a realizar.
    /// Luego se obtienen los operadores de forma aleatoria y se devuelve el resultado correcto.
    /// </summary>
    /// <param name="actualOperation">Entero que indica el tipo de operación</param>
    /// <returns>Resultado de la operación realizada con operadores aleatorios</returns>
    public int RandomOperation(int actualOperation)
    {
        int result = 0;
        int operator1 = 0, operator2 = 0, operator3 = 0, operator4 = 0;

        System.Random rng = new System.Random();

        switch (actualOperation)
        {
            // Caso 0: A + B x C
            case 0:
                operator1 = rng.Next(2, 10);
                operator2 = rng.Next(2, 10);
                operator3 = rng.Next(2, 10);

                operation.GetComponent<Text>().text = operator1 + " + " + operator2 + " x " + operator3 + " = ?";
                Debug.Log(operator1 + " + " + operator2 + " x " + operator3 + " = ?");

                result = operator1 + (operator2 * operator3);
                break;

            // Caso 1: A + B + C + D
            case 1:
                operator1 = rng.Next(2, 25);
                operator2 = rng.Next(2, 25);
                operator3 = rng.Next(2, 25);
                operator4 = rng.Next(2, 25);

                operation.GetComponent<Text>().text = operator1 + " + " + operator2 + " + " + operator3 + " + " + operator4 + " = ?";
                Debug.Log(operator1 + " + " + operator2 + " + " + operator3 + " + " + operator4 + " = ?");
                result = operator1 + operator2 + operator3 + operator4;
                break;

            // Caso 2: A - B - C + D
            case 2:
                operator1 = rng.Next(2, 25);
                operator2 = rng.Next(2, 25);
                operator3 = rng.Next(2, 25);
                operator4 = rng.Next(2, 25);

                operation.GetComponent<Text>().text = operator1 + " - " + operator2 + " - " + operator3 + " + " + operator4 + " = ?";
                Debug.Log(operator1 + " - " + operator2 + " - " + operator3 + " + " + operator4 + " = ?");
                result = operator1 - operator2 - operator3 + operator4;
                break;

            // Caso 3: Resto de A / B
            case 3:
                operator1 = rng.Next(4, 100);
                operator2 = rng.Next(2, 11);
                operation.GetComponent<Text>().text = "¿Cuál es el resto de " + operator1 + " / " + operator2 + " ?";
                Debug.Log("¿Cuál es el resto de " + operator1 + " / " + operator2 + " ?");
                result = operator1 % operator2;
                break;

            default:
                Debug.Log(result);
                break;
        }
        
        return result;
    }

    /// <summary>
    /// Método que obtiene una lista de números aleatorios como posibles respuestas de la operación,
    /// incluyendo siempre la respuesta correcta mezclada al azar con el resto.
    /// </summary>
    /// <returns></returns>
    public List<int> RandomVector()
    {
        List<int> values = new List<int>();

        int firstNumber = resultOperation;

        for(int i = 0; i < buttons.Length; i++)
        {
            while (values.Contains(firstNumber))
            {
                firstNumber = RandomInt(actualOperation);
            }

            values.Add(firstNumber);
            
        }

        ShuffleResponse(values);
        return values;
    }

    /// <summary>
    /// Método que incluye en una posición aleatoria el resultado correcto de entre
    /// todas las opciones posibles.
    /// </summary>
    /// <param name="list">La lista con todas las opciones posibles</param>
    public static void ShuffleResponse(List<int> list)
    {
        System.Random rng = new System.Random();
        int newPosition = rng.Next(1, list.Count);
        // Intercambiamos la primera posición con la aleatoria que hemos obtenido
        int swap = list[newPosition];
        list[newPosition] = list[0];
        list[0] = swap;

    }

    /// <summary>
    /// Método que permite rellenar los botones con todas las opciones de respuesta
    /// calculadas anteriormente.
    /// </summary>
    public void FillButtons()
    {
        List<int> valuesToButtons = RandomVector();
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponentInChildren<Text>().text = valuesToButtons[i].ToString();
        }
    }

    /// <summary>
    /// Método que permite obtener un número entero en un rango que vendrá determinado por
    /// el parámetro actualOperation. De este modo aseguramos que los números aleatorios
    /// generados estarán dentro de un rango acorde a la operación mostrada.
    /// </summary>
    /// <param name="actualOperation">Un entero que indica el tipo de operación</param>
    /// <returns>Un entero aleatorio en el rango indicado</returns>
    public int RandomInt(int actualOperation)
    {
        int result = 0;
        System.Random r = new System.Random();

        switch (actualOperation)
        {
            case 0:
                result = r.Next(0, 90);
                break;

            case 1:
                result = r.Next(8, 101);
                break;

            case 2:
                result = r.Next(-46, 47);
                break;

            case 3:
                result = r.Next(0, 10);
                break;

        }
        return result;
    }


    /// <summary>
    /// Método que comprueba si el botón pulsado contiene la respuesta correcta o no y actúa en
    /// consecuencia mostrando los mensajes adecuados al usuario.
    /// </summary>
    /// <param name="buttonClick">El botón sobre el que se ha hecho click</param>
    public void CheckResponse(Button buttonClick)
    {
        answered = true;

        string textButton = buttonClick.GetComponentInChildren<Text>().text;
        int value = Int32.Parse(textButton);

        if (value == resultOperation)
        {
            correct++;
            message.GetComponent<Text>().text = "¡Correcto!";
            /*cb.normalColor = Color.green;
            cb.pressedColor = Color.green;
            buttonClick.colors = cb;*/
        } else
        {
            message.GetComponent<Text>().text = "¡Incorrecto! El resultado correcto es " + resultOperation;
            /*cb.normalColor = Color.red;
            cb.pressedColor = Color.red;
            buttonClick.colors = cb;*/
        }

        invisiblePanel.SetActive(true);

    }

    /// <summary>
    /// Método que se llama al finalizar la ronda de preguntas y vuelve a la escena principal.
    /// </summary>
    public void EndGame()
    {
        // Se almacenan los resultados obtenidos en el minijuego.
        gameControl.gems[0] += numberOfGems;
        gameControl.games[0]++;
        gameControl.corrects[0] += correct;

        // Se ocultan los paneles del minijuego y se muestra la escena principal.
        resultsPanel.SetActive(false);
        mainPanel.SetActive(false);
        infoPanel.SetActive(true);

        // Se reactiva el control del personaje.
        GetComponent<AudioSource>().enabled = false;
        player.GetComponent<AudioSource>().enabled = true;
        player.GetComponent<PlayerController>().enabled = true;

        // Se reactivan los enemigos.
        foreach (GameObject enemy in enemies)
        {
            gameControl.ActivateEnemy(enemy);
        }

    }

}
