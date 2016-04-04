using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

/// <summary>
/// Clase que controla la mecánica del juego de percepción espacial SPATIAL.
/// </summary>
public class SpatialManager : MonoBehaviour {

    public static SpatialManager gm;

    // Referencias a los paneles de la interfaz gráfica.
    public GameObject infoPanel;
    public GameObject welcomePanel;
    public GameObject mainPanel;
    public GameObject timePanel;
    public GameObject invisiblePanel;

    // Referencias a otros elementos de la interfaz gráfica.
    public Texture[] images;
    public Button[] buttons;
    public GameObject message;

    public float timeResponse;

    private GameControl gameControl;
    private GameObject player;
    private GameObject[] enemies;

    // Array que almacena las imágenes a mostrar en cada puzzle, está compuesto por números del 0 al 5.
    // Cada número se asocia con una imagen del vector de texturas 'images'.
    private static int[] puzzle0 = new int[] { 2, 1, 2, 0, 2, 2, 5, 1, 0, 1, 2, 4, 3, 5, 2, 3, 2, 3, 2, 3, 2, 5, 2, 1, 2, 5, 2, 2 };
    // Array que almacena la solución al puzzle asociado, está compuesto por números del 0 al 3.
    // Cada número indica el número de giros del recuadro desde la 0 (posición inicial) hasta la 3 (posición tras girar tres veces).
    private static int[] solution0 = new int[] { 3, 0, 2, 2, 3, 2, 0, 1, 2, 1, 0, 0, 2, 0, 0, 2, 0, 3, 1, 0, 2, 0, 0, 0, 1, 0, 0, 1 };

    private static int[] puzzle1 = new int[] { 2, 3, 3, 1, 0, 5, 5, 3, 3, 2, 1, 3, 3, 0, 2, 4, 1, 0, 3, 3, 5, 5, 2, 1, 1, 3, 2, 5 };
    private static int[] solution1 = new int[] { 3, 3, 3, 0, 1, 0, 0, 0, 2, 0, 0, 3, 3, 1, 0, 0, 0, 1, 0, 2, 0, 0, 0, 0, 0, 1, 1, 0 };

    private static int[] puzzle2 = new int[] { 5, 2, 3, 1, 1, 2, 5, 5, 3, 3, 1, 2, 3, 2, 5, 2, 1, 2, 1, 3, 2, 0, 1, 1, 2, 2, 2, 5 };
    private static int[] solution2 = new int[] { 0, 3, 3, 0, 0, 2, 0, 0, 0, 1, 0, 2, 0, 2, 0, 0, 0, 2, 1, 0, 1, 3, 0, 0, 1, 0, 1, 0 };

    private static int[] puzzle3 = new int[] { 0, 1, 1, 2, 5, 2, 2, 2, 1, 1, 4, 1, 3, 1, 2, 2, 5, 3, 2, 2, 3, 5, 2, 1, 3, 3, 1, 2 };
    private static int[] solution3 = new int[] { 3, 0, 0, 2, 0, 3, 2, 3, 0, 0, 0, 0, 2, 1, 0, 2, 0, 0, 2, 0, 2, 0, 0, 0, 1, 1, 0, 1 };

    private static int[] puzzle4 = new int[] { 2, 3, 3, 3, 3, 2, 5, 3, 3, 2, 3, 0, 2, 2, 1, 1, 5, 3, 0, 2, 2, 2, 3, 1, 3, 1, 2, 5 };
    private static int[] solution4 = new int[] { 3, 3, 3, 3, 3, 2, 0, 0, 2, 0, 2, 0, 0, 2, 1, 1, 0, 0, 1, 3, 1, 0, 1, 0, 1, 0, 1, 0 };

    private static int[] puzzle5 = new int[] { 2, 1, 2, 5, 5, 2, 2, 3, 1, 4, 1, 1, 3, 1, 1, 0, 3, 1, 3, 4, 3, 2, 3, 2, 0, 3, 2, 0 };
    private static int[] solution5 = new int[] { 3, 0, 2, 0, 0, 3, 2, 0, 0, 0, 0, 0, 2, 1, 1, 2, 0, 0, 3, 0, 2, 0, 1, 1, 3, 1, 1, 0 };

    private static int[] puzzle6 = new int[] { 0, 2, 3, 3, 2, 2, 2, 3, 3, 1, 1, 1, 0, 1, 1, 1, 2, 2, 1, 2, 3, 2, 2, 5, 5, 2, 3, 2 };
    private static int[] solution6 = new int[] { 2, 3, 3, 3, 2, 3, 2, 0, 2, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 3, 2, 0, 1, 0, 0, 0, 1, 1 };

    private static int[] puzzle7 = new int[] { 5, 0, 2, 3, 1, 2, 5, 2, 4, 3, 3, 3, 4, 2, 2, 4, 3, 2, 3, 3, 2, 5, 2, 3, 1, 3, 2, 5 };
    private static int[] solution7 = new int[] { 0, 2, 3, 3, 0, 2, 0, 3, 0, 2, 0, 3, 0, 2, 0, 0, 2, 0, 2, 0, 1, 0, 0, 1, 0, 1, 1, 0 };

    private static int[] puzzle8 = new int[] { 0, 2, 1, 2, 2, 2, 5, 1, 1, 5, 3, 4, 3, 0, 2, 2, 2, 3, 0, 3, 2, 5, 5, 2, 3, 1, 2, 5 };
    private static int[] solution8 = new int[] { 2, 3, 0, 2, 3, 2, 0, 1, 1, 0, 0, 0, 2, 2, 0, 1, 3, 2, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0 };

    private static int[] puzzle9 = new int[] { 0, 1, 1, 2, 2, 1, 2, 2, 3, 1, 2, 2, 3, 3, 2, 4, 3, 2, 5, 0, 1, 5, 2, 2, 2, 1, 1, 2 };
    private static int[] solution9 = new int[] { 3, 0, 0, 2, 3, 0, 2, 3, 3, 0, 1, 0, 3, 2, 0, 0, 3, 2, 0, 0, 1, 0, 0, 1, 0, 0, 0, 1 };

    // Arrays que almacenan respectivamente todas los puzzles posibles y su solución correspondiente.
    private int[][] puzzlesArray = new int[][] { puzzle0, puzzle1, puzzle2, puzzle3, puzzle4 , puzzle5, puzzle6 , puzzle7, puzzle8 , puzzle9 };
    private int[][] solutionsArray = new int[][] { solution0, solution1, solution2, solution3, solution4, solution5, solution6, solution7, solution8, solution9 };

    // Variables que almacenan los datos de esta sesión del minijuego.
    private int[] actualPuzzle;
    private int[] solutionPuzzle;
    private int[] answer;

    // Variables que controlan la mecánica del minijuego
    private float timeLeft;
    private bool gameRunning;
    private bool soundPlaying = false;

    // Variables que almancenan los resultados del minijuego
    private bool correct;
    private int numberOfGems;
    

    /// <summary>
    /// Método para inicializar variables que se llama una única vez al iniciar el script
    /// </summary>
    void Start () {
        if (gm == null)
            gm = gameObject.GetComponent<SpatialManager>();

        player = GameObject.FindWithTag("Player");
        gameControl = GameObject.FindWithTag("GameController").GetComponent<GameControl>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Welcome();
	}

    /// <summary>
    /// Método para actualizar variables que se llama una vez por frame
    /// </summary>
    void Update () {
        if (Input.GetKey(KeyCode.Return))
        {
            gameControl.PrintArray(answer);
            ResetButtons();
        }
        // Sólo si el juego se está ejecutando, actualizamos el contador de tiempo.
        if (gameRunning)
        {

            if (timeLeft >= 0)
            {
                timePanel.GetComponentInChildren<Text>().text = ShowTimer(timeLeft);
                timeLeft -= Time.deltaTime;
            }
            else
            {
                if (!soundPlaying)
                {
                    GetComponents<AudioSource>()[0].Stop();
                    GetComponents<AudioSource>()[2].Play();
                    soundPlaying = true;
                }

                message.GetComponent<Text>().text = "\n¡Se acabó el tiempo!\nMás suerte la próxima vez";
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
        invisiblePanel.SetActive(false);
        welcomePanel.SetActive(true);
        timePanel.SetActive(false);

        GetComponent<AudioSource>().enabled = true;

        correct = false;
        gameRunning = false;

        actualPuzzle = new int[28];
        solutionPuzzle = new int[28];
        answer = new int[28];

        numberOfGems = 0;

    }

    /// <summary>
    /// Método que se ejecuta al principio del minijuego.
    /// </summary>
    public void InitGame()
    {

        welcomePanel.SetActive(false);
        mainPanel.SetActive(true);
        timePanel.SetActive(true);

        DrawSquares();

        if (!gameRunning)
        {
            gameRunning = true;
        }
        
        timeLeft = timeResponse;

    }

    /// <summary>
    /// Método que aplica a cada botón la textura correspondiente al tipo de tubería que representa.
    /// </summary>
    public void DrawSquares()
    {
        System.Random r = new System.Random();
        int puzzlePos =  r.Next(0, 10);

        for(int i = 0; i < actualPuzzle.Length; i++)
        {
            actualPuzzle[i] = puzzlesArray[puzzlePos][i];
            solutionPuzzle[i] = solutionsArray[puzzlePos][i];
        }

        for (int  i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponentInChildren<RawImage>().texture = images[actualPuzzle[i]];
        }
    }

    /// <summary>
    /// Método que permite girar el botón pulsado en sentido antihorario y almacenar la posición
    /// en la que queda una vez girado.
    /// </summary>
    /// <param name="buttonClick">El botón que se desea girar</param>
    public void RotateOnClick(Button buttonClick)
    {
        // Localizamos la posición del botón en el array
        int numberOfButton = Array.FindIndex(buttons, (x) => (x.name == buttonClick.name));

        // Variable que determina la orientación de la imagen, variará con cada click.
        int position = answer[numberOfButton];

        Texture typeOfSquare = buttonClick.GetComponentInChildren<RawImage>().texture;

        // Casos en que el botón pueda tener 4 posiciones: DeadEnd, Corridor y Corner
        if (typeOfSquare == images[0] || typeOfSquare == images[2] || typeOfSquare == images[3])
        {
            // Rotamos el botón 90 grados en sentido antihorario
            buttonClick.transform.Rotate(0, 0, 90);

            if (position < 3)
            {
                position += 1;
            } else
            {
                position = 0;
            }
        } else if (typeOfSquare == images[1])
        {
            // Rotamos el botón 90 grados en sentido antihorario
            buttonClick.transform.Rotate(0, 0, 90);

            if (position == 0)
            {
                position = 1;
            } else
            {
                position = 0;
            }
        }

        answer[numberOfButton] = position;

        CheckResponse();

    }


    /// <summary>
    /// Método que comprueba si el jugador ha logrado dar con la solución al puzzle y en ese caso,
    /// muestra mensaje al usuario con información adecuada.
    /// </summary>
    public void CheckResponse()
    {
        correct = solutionPuzzle.SequenceEqual(answer);

        if (correct)
        {
            gameRunning = false;
            invisiblePanel.SetActive(true);
            numberOfGems = 25;
            //float solveTime = timeResponse - timeLeft;
            GetComponents<AudioSource>()[0].Stop();
            GetComponents<AudioSource>()[1].Play();
            message.GetComponent<Text>().text = "¡Muy bien! Has conseguido completar el puzzle y\nganar " + numberOfGems + " gemas azules.";
        }

    }

    /// <summary>
    ///  Método que muestra en un formato 0:00 (minutos y segundos) el contador de tiempo.
    /// </summary>
    /// <param name="timer">El tiempo restante en segundos</param>
    /// <returns>Una cadena de texto con el tiempo restante en formato 0:00</returns>
    public string ShowTimer(float timer)
    {
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        return string.Format("{0:0}:{1:00}", minutes, seconds);

    }

    /// <summary>
    /// Método que gira todos los botones hasta que vuelvan a estar en su posición inicial.
    /// </summary>
    public void ResetButtons()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            while (buttons[i].GetComponent<RectTransform>().rotation.z != 0)
            {
                buttons[i].transform.Rotate(0, 0, 90);
            }
           
        }
    }

    /// <summary>
    /// Método que se llama al finalizar el minijuego y vuelve a la escena principal.
    /// </summary>
    public void EndGame()
    {
        // Se vuelven a colocar los botones en su posición inicial.
        ResetButtons();

        // Se almacenan los resultados obtenidos en el minijuego.
        gameControl.gems[1] += numberOfGems;
        gameControl.games[1]++;
        if (correct)
        {
            gameControl.corrects[1]++;
        }

        // Se ocultan los paneles del minijuego y se muestra la escena principal.
        mainPanel.SetActive(false);
        invisiblePanel.SetActive(false);
        timePanel.SetActive(false);
        infoPanel.SetActive(true);

        // Se reactiva el control del personaje
        enabled = false;
        soundPlaying = false;
        GetComponent<AudioSource>().enabled = false;
        player.GetComponent<AudioSource>().enabled = true;
        player.GetComponent<PlayerController>().enabled = true;

        // Se reactivan los enemigos
        foreach (GameObject enemy in enemies)
        {
            gameControl.ActivateEnemy(enemy);
        }
    }

}
