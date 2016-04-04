using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

/// <summary>
/// Clase que controla la mecánica del minijuego de lógica boolena LOGIC.
/// </summary>
public class LogicManager : MonoBehaviour {

    public static LogicManager gm;

    // Referencias a los paneles de la interfaz gráfica.
    public GameObject infoPanel;
    public GameObject welcomePanel;
    public GameObject infoLogicPanel;
    public GameObject mainPanel;
    public GameObject invisiblePanel;
    public GameObject resultsPanel;

    // Referencias a otros elementos de la interfaz gráfica.
    public Texture[] images;
    public Toggle[] buttons;
    public GameObject booleanExpression;

    // Número de puzzles por sesión del minijuego
    public float numberOfPuzzles;

    private GameControl gameControl;
    private GameObject player;
    private GameObject[] enemies;

    // Variables que almacenan los posibles valores de las figuras
    private int actualExpression;
    private string[] figuresArray = { "Círculo", "Cuadrado", "Triángulo" };
    private string[] colorsArray = { "Amarillo", "Azul", "Rojo", "Verde"};

    // Variables que gestionan la lógica del minijuego.
    private int firstSolution;
    private int[] logicOptions;
    private int[] logicSolutions;
    private int[] logicAnswers;

    // Resultados del minijuego.
    private int count;
    private int correct;
    private int numberOfGems;

    /// <summary>
    /// Método para inicializar variables que se llama una única vez al iniciar el script
    /// </summary>
    void Start () {
        if (gm == null)
            gm = gameObject.GetComponent<LogicManager>();

        player = GameObject.FindWithTag("Player");
        gameControl = GameObject.FindWithTag("GameController").GetComponent<GameControl>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Welcome();

    }

    /// <summary>
    /// Método para actualizar variables que se llama una vez por frame
    /// </summary>
    void Update () {

        /*if (Input.GetKey(KeyCode.Return))
        {
            Toggle button1 = buttons[1];
            Toggle button2 = buttons[2];
            Toggle button3 = buttons[3];
            // Activamos los botones 2 y 3
            button2.isOn = true;
            button3.isOn = true;

            Toggle[] buttonTest = { button1, button2, button3 };

            // Esperado: False True True
            string entrada = "Datos de entrada: ";
            foreach (Toggle button in buttonTest)
            {
               entrada += button.isOn + " ";
            }
            Debug.Log(entrada);

            UnmarkButtons(buttonTest);
            // Esperado: False False False
            string salida = "Datos de salida: ";
            foreach (Toggle button in buttonTest)
            {
                salida += button.isOn + " ";
            }
            Debug.Log(salida);

        }*/

        /*if (Input.GetKey(KeyCode.Return))
        {
            int solution1 = 5;
            List<int> lista = RandomList(solution1);

            // Salida esperada: Vector de 6 números enteros aleatorios con
            // el valor de solution1 en cualquier posición.
            gameControl.PrintList(lista);

        }*/
	}

    // Pantalla de bienvenida
    /// <summary>
    /// Método que muestra el panel con las instrucciones de juego y resetea todas las variables
    /// a su valor inicial para asegurar cada ejecución independiente del minijuego.
    /// </summary>
    public void Welcome()
    {
        infoPanel.SetActive(false);
        mainPanel.SetActive(false);
        resultsPanel.SetActive(false);
        invisiblePanel.SetActive(false);
        welcomePanel.SetActive(true);

        this.GetComponent<AudioSource>().enabled = true;
        count = 0;
        correct = 0;
        numberOfGems = 0;
    }

    /// <summary>
    /// Método que muestra el panel con información sobre los operadores booleanos.
    /// </summary>
    public void InfoBoolean()
    {
        welcomePanel.SetActive(false);
        infoLogicPanel.SetActive(true);

    }

    /// <summary>
    /// Método que se ejecuta con cada puzzle del minijuego.
    /// </summary>
    public void InitGame()
    {
        welcomePanel.SetActive(false);
        infoLogicPanel.SetActive(false);
        invisiblePanel.SetActive(false);
        mainPanel.SetActive(true);

        // Se ejecuta siempre que no se alcance el número total de puzzles.
        if(count < numberOfPuzzles)
        {
            UnmarkButtons(buttons);

            logicOptions = new int[6];
            logicSolutions = new int[6];
            logicAnswers = new int[6];

            System.Random r = new System.Random();
            actualExpression = r.Next(0, 5);

            RandomLogicExpression(actualExpression);

            FillButtons();

            count++;
        } else
        {
            resultsPanel.SetActive(true);
            numberOfGems = 5 * correct;

            // Si al menos se ha contestado una pregunta correctamente, se mostrarán las gemas obtenidas y música de éxito.
            if (correct > 0)
            {
                GetComponents<AudioSource>()[0].Stop();
                GetComponents<AudioSource>()[1].Play();
                resultsPanel.GetComponentInChildren<Text>().text = "¡Ronda finalizada!\nHas tenido " + correct + " aciertos y\nganado " + numberOfGems + " gemas rojas";
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
    /// Método que recibe un entero indicando el tipo de expresión boolena a calcular.
    /// Posteriormente se obtienen dos operadores booleanos aleatorios que se mostrarán
    /// en forma de expresión booleana al jugador y se calcula y almacena el vector o 
    /// vectores de soluciones para la expresión en cuestión.
    /// </summary>
    /// <param name="actualExpression"></param>
    public void RandomLogicExpression(int actualExpression)
    {
        System.Random r = new System.Random();
        int indexFigure = r.Next(0, 3);
        int indexColor = r.Next(0, 4);
        List<int> allSolutions = new List<int>();

        // Según el entero pasado como parámetro, se escoge una expresión boolena u otra.
        switch (actualExpression)
        {
            // Caso 0: Forma AND Color
            case 0:

                booleanExpression.GetComponent<Text>().text = figuresArray[indexFigure] + " AND " + colorsArray[indexColor];
                firstSolution = indexFigure * 4 + indexColor;
                logicOptions = RandomList(firstSolution).ToArray();
                for(int i = 0; i < logicOptions.Length; i++)
                {
                    if(logicOptions[i] == firstSolution)
                    {
                        logicSolutions[i] = 1;
                    }
                }
                //gameControl.PrintArray(logicSolutions);
                break;

            // Caso 1: Forma AND NOT Color
            case 1:
                booleanExpression.GetComponent<Text>().text = figuresArray[indexFigure] + " AND NOT " + colorsArray[indexColor];

                for (int i = 0; i <= 3; i++)
                {
                    if (i != indexColor)
                    {
                        allSolutions.Add(indexFigure * 4 + i);
                    }

                }

                firstSolution = allSolutions[0];

                logicOptions = RandomList(firstSolution).ToArray();
                for (int i = 0; i < logicOptions.Length; i++)
                {
                    if (logicOptions[i] == allSolutions[0] || logicOptions[i] == allSolutions[1] || logicOptions[i] == allSolutions[2])
                    {
                        logicSolutions[i] = 1;
                    }
                }

                break;

            // Caso 2: NOT Forma AND Color
            case 2:
                booleanExpression.GetComponent<Text>().text = "NOT " + figuresArray[indexFigure] + " AND " + colorsArray[indexColor];

                for (int i = 0; i <= 2; i++)
                {
                    if (i != indexFigure)
                    {
                        allSolutions.Add(i * 4 + indexColor);
                    }

                }

                firstSolution = allSolutions[0];

                logicOptions = RandomList(firstSolution).ToArray();

                for (int i = 0; i < logicOptions.Length; i++)
                {
                    if (logicOptions[i] == allSolutions[0] || logicOptions[i] == allSolutions[1])
                    {
                        logicSolutions[i] = 1;
                    }
                }

                //gameControl.PrintArray(logicSolutions);
                break;

            // Caso 3: Forma OR Color
            case 3:
                booleanExpression.GetComponent<Text>().text = figuresArray[indexFigure] + " OR " + colorsArray[indexColor];

                for (int i = 0; i <= 3; i++)
                {
                    for (int j = 0; j <= 2; j++)
                    {
                        if (i == indexColor || j == indexFigure)
                        {
                            allSolutions.Add(j * 4 + i);
                        }
                    }
                }

                firstSolution = allSolutions[0];

                logicOptions = RandomList(firstSolution).ToArray();

                // Soluciones para ambos operadores lógicos simultánemente (figuras y colores)
                for (int i = 0; i < logicOptions.Length; i++)
                {
                    for (int j = 0; j < allSolutions.Count; j++)
                    {
                        if (logicOptions[i] == allSolutions[j])
                        {
                            logicSolutions[i] = 1;
                        }
                    }

                }

                //gameControl.PrintArray(logicSolutions);

                break;

            // Caso 4: NOT (Forma OR Color)
            case 4:
                booleanExpression.GetComponent<Text>().text = "NOT (" + figuresArray[indexFigure] + " OR " + colorsArray[indexColor] + ")";

                for(int i = 0; i <= 3; i++)
                {
                    for(int j = 0; j <= 2; j++)
                    {
                        if(i != indexColor && j != indexFigure)
                        {
                            allSolutions.Add(j * 4 + i);
                        }
                    }
                }
                //gameControl.PrintList(allSolutions);

                firstSolution = allSolutions[0];

                logicOptions = RandomList(firstSolution).ToArray();

                // Soluciones para ambos operadores lógicos simultánemente (figuras y colores)
                for (int i = 0; i < logicOptions.Length; i++)
                {
                    for (int j = 0; j < allSolutions.Count; j++)
                    {
                        if (logicOptions[i] == allSolutions[j])
                        {
                            logicSolutions[i] = 1;
                        }
                    }

                }

                //gameControl.PrintArray(logicSolutions);

                break;

            default:
                break;

        }
    }


    /// <summary>
    /// Método que permite rellenar los botones con imágenes de manera aleatoria, aunque introduciendo
    /// siempre una solución válida para la expresión lógica presentada.
    /// </summary>
    public void FillButtons()
    {
        //logicOptions = RandomList().ToArray();

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponentsInChildren<RawImage>()[1].texture = images[logicOptions[i]];
        }
    }

    /// <summary>
    /// Método que comprueba si los botones marcados son una respuesta válida que cumple con la
    /// expresión lógica actual.
    /// </summary>
    public void CheckResponse()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].isOn)
            {
                logicAnswers[i] = 1;
            }
        }

        //gameControl.PrintArray(logicAnswers);

        // Comprobamos si coinciden los vectores respuesta y solución.
        if (logicAnswers.SequenceEqual(logicSolutions))
        {
            correct++;
            invisiblePanel.GetComponentInChildren<Text>().text = "¡Correcto!";
        }
        else
        {
            invisiblePanel.GetComponentInChildren<Text>().text = "No es correcto\n¡Sigue practicando!";
        }       

        invisiblePanel.SetActive(true);
    }

    /// <summary>
    /// Método para desmarcar todos los botones marcados en la ejecución anterior del minijuego.
    /// </summary>
    public void UnmarkButtons(Toggle[] buttons)
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].isOn = false;
        }
    }

    /// <summary>
    /// Método que obtiene una lista de números aleatorios como posibles respuestas.
    /// </summary>
    /// <returns>Una lista de números aleatorios que contiene al menos una solución correcta</returns>
    public List<int> RandomList(int firstSolution)
    {
        System.Random r = new System.Random();
        List<int> values = new List<int>();

        int firstNumber = firstSolution;

        for (int i = 0; i < buttons.Length; i++)
        {
            while (values.Contains(firstNumber))
            {
                firstNumber = r.Next(0,12);
            }

            values.Add(firstNumber);

        }

        MathsManager.ShuffleResponse(values);

        return values;
    }

    /// <summary>
    /// Método que se llama al finalizar la ronda de preguntas y vuelve a la escena principal.
    /// </summary>
    public void EndGame()
    {
        // Se almacenan los resultados obtenidos en el minijuego.
        gameControl.gems[2] += numberOfGems;
        gameControl.games[2]++;
        gameControl.corrects[2] += correct;

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
