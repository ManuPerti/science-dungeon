using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

/// <summary>
/// Clase que controla la mecánica del minijuego de preguntas QUIZ.
/// </summary>
public class QuizManager : MonoBehaviour {

    public static QuizManager gm;

    // Referencias a los paneles de la interfaz gráfica
    public GameObject infoPanel;
    public GameObject welcomePanel;
    public GameObject mainPanel;
    public GameObject timePanel;
    public GameObject invisiblePanel;
    public GameObject resultsPanel;

    // Variables que determinan las condiciones de cada juego
    public Button[] buttons;
    public float timeResponse;
    public int numberOfQuestions;

    private GameControl gameControl;
    private GameObject player;
    private GameObject[] enemies;

    // Variables que almacenan los resultados del minijuego.
    private int count;
    private int correct;
    private int numberOfGems;

    // Variable que almacena una de las posibles preguntas del minijuego.
    private static string question0 = "¿Cuál de los siguientes poliedros NO es un poliedro de revolución?";
    // Vector que almacena las 4 posibles respuestas asociadas a la pregunta anterior.
    // La respuesta correcta siempre se encuentra en la prima posición, así que posteriormente se mezclará con el resto.
    private static string[] answers0 = { "Prisma", "Cilindro", "Esfera", "Cono" };

    private static string question1 = "¿Cuánto grados suman todos los ángulos interiores de un triángulo?";
    private static string[] answers1 = {"180", "200", "90", "360"};

    private static string question2 = "¿Cuál de los siguientes poliedros NO es necesariamente regular?";
    private static string[] answers2 = {"Pirámide", "Cubo", "Tetraedro", "Icosaedro" };

    private static string question3 = "¿Cómo se denomina al triángulo que tiene dos lados iguales y uno desigual?";
    private static string[] answers3 = { "Isósceles", "Rectángulo", "Escaleno", "Equilátero" };

    private static string question4 = "Si en una función obtenemos los valores de X para los que la función es nula ¿qué estamos calculando realmente?";
    private static string[] answers4 = { "Puntos de corte con el eje X", "Asíntotas", "Puntos de corte con el eje Y", "Vértices" };

    private static string question5 = "¿Cuál es la unidad de medida de fuerza del Sistema Internacional?";
    private static string[] answers5 = { "Newton", "Dina", "Kilopondio", "Libra-fuerza" };

    private static string question6 = "Todos los sistemas materiales tienden a adoptar:";
    private static string[] answers6 = { "Mínimo de energía y máximo desorden", "Mínimo de energía y minimo desorden", "Máximo de energía y mínimo desorden", "Máximo de energía y máximo desorden" };

    private static string question7 = "Una disolución es una mezcla homogénea de al menos dos sustancia que no reaccionan entre sí llamadas:";
    private static string[] answers7 = { "Disolvente y soluto", "Disoluto y solvente", "Líquido y sólido", "Suspensión y coloide" };

    private static string question8 = "Según la trayectoria seguida, ¿qué tipo de movimiento tiene un balón lanzado a una canasta o el disparo de un cañón?";
    private static string[] answers8 = { "Parabólico", "Rectilíneo", "Uniforme", "Acelerado" };

    private static string question9 = "Si lanzamos dos cuerpos de distinta masa desde un edificio despreciando el rozamiento ¿cuál llegará antes al suelo?";
    private static string[] answers9 = { "Los dos a la vez", "El más pesado", "El más ligero", "No se puede saber" };

    private static string question10 = "¿Cuánto grados suman todos los ángulos interiores de un cuadrilátero?";
    private static string[] answers10 = { "360", "200", "90", "180" };

    private static string question11 = "¿Cómo se denomina al triángulo que tiene sus tres lados y sus tres ángulos iguales?";
    private static string[] answers11 = { "Equilátero", "Isósceles", "Rectángulo", "Escaleno" };

    private static string question12 = "¿Cómo se denomina al triángulo que tiene sus tres lados desiguales?";
    private static string[] answers12 = { "Escaleno", "Equilátero", "Isósceles", "Rectángulo" };

    private static string question13 = "¿Cuántas soluciones reales tiene una ecuación de segundo grado? (Elige la opción más correcta)";
    private static string[] answers13 = { "Como máximo dos", "Sólo una", "Exactamente dos", "Al menos una" };

    private static string question14 = "Según la Primera Ley de Newton si sobre un cuerpo no actúa ninguna fuerza externa, o está en reposo o...";
    private static string[] answers14 = { "Se mueve con velocidad uniforme", "No se puede mover", "Su energía cinética es cero", "Su energía es infinita" };

    private static string question15 = "¿A qué forma geométrica se asemeja la órbita de un planeta en torno al Sol?";
    private static string[] answers15 = { "Elipse", "Parábola", "Circunferencia", "Hipérbola" };

    private static string question16 = "¿Qué fuerza se opone a que un objeto se deslice por una superficie que no sea lisa?";
    private static string[] answers16 = { "Fuerza de rozamiento", "Fuerza normal", "Peso", "Fuerza mayor" };

    private static string question17 = "Cuando vamos en el autobús y éste da un giro brusco, tenemos la sensación de salirnos de la curva, ¿cómo se llama la fuerza que produce esa sensación?";
    private static string[] answers17 = { "Fuerza centrífuga", "Fuerza centrípeta", "Fuerza de rozamiento", "Fuerza normal" };

    private static string question18 = "¿Qué poliedro de revolución se genera al girar un triángulo sobre uno de sus catetos?";
    private static string[] answers18 = { "Cono", "Esfera", "Cilindro", "Tronco de cono" };

    private static string question19 = "¿Qué poliedro de revolución se genera al girar un trapecio sobre el lado perpendicular a sus bases?";
    private static string[] answers19 = { "Tronco de cono", "Cono", "Esfera", "Cilindro" };



    // Arrays que almacenan respectivamente todas las preguntas posibles y sus respuestas correspondientes.
    private string[] questionArray = new string[] { question0, question1, question2, question3, question4, question5, question6, question7, question8, question9,
                                                    question10, question11, question12, question13, question14, question15, question16, question17, question18, question19};
    private string[][] answerArray = new string[][] { answers0, answers1, answers2, answers3, answers4, answers5, answers6, answers7, answers8, answers9,
                                                      answers10, answers11, answers12, answers13, answers14, answers15, answers16, answers17, answers18, answers19};

    // Variables que almacenan los datos de esta sesión del minijuego.
    private List<int> selectedQuestions;
    private string currentQuestion;
    private string correctAnswer;
    private string[] currentAnswers;
   
    private float timeLeft;
    private bool gameRunning;

    /// <summary>
    /// Método para inicializar variables que se llama una única vez al iniciar el script
    /// </summary>
    void Start () {
        if (gm == null)
            gm = gameObject.GetComponent<QuizManager>();

        player = GameObject.FindWithTag("Player");
        gameControl = GameObject.FindWithTag("GameController").GetComponent<GameControl>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Welcome();

    }

    /// <summary>
    /// Método para actualizar variables que se llama una vez por frame
    /// </summary>
    void Update () {

        // Sólo si el juego se está ejecutando, actualizamos el contador de tiempo.
        if (gameRunning)
        {
            if (timeLeft >= 0)
            {
                timePanel.GetComponentInChildren<Text>().text = timeLeft.ToString("#");
                timeLeft -= Time.deltaTime;
                //Debug.Log(timeLeft);
            }
            // Si no se responde a tiempo, se mostrará un mensaje al usuario.
            else
            {
                invisiblePanel.GetComponentInChildren<Text>().text = "\n¡Se acabó el tiempo!\n";
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

        this.GetComponent<AudioSource>().enabled = true;
        count = 0;
        correct = 0;
        numberOfGems = 0;
    }

    /// <summary>
    /// Método que se ejecuta con cada pregunta del minijuego.
    /// </summary>
    public void InitGame()
    {
        welcomePanel.SetActive(false);
        invisiblePanel.SetActive(false);
        mainPanel.SetActive(true);
        timePanel.SetActive(true);

        if (!gameRunning)
        {
            gameRunning = true;
            selectedQuestions = SelectQuestions();
        }

        timeLeft = timeResponse;

        // Mostraremos preguntas al jugador hasta que se complete la ronda.
        if (count < numberOfQuestions)
        {
            ShowQuestion();

            count++;

        } else
        {
            gameRunning = false;
            timePanel.SetActive(false);
            resultsPanel.SetActive(true);
            numberOfGems = 5 * correct;

            // Si al menos se ha contestado una pregunta correctamente, se mostrarán las gemas obtenidas y música de éxito.
            if (correct > 0)
            {
                GetComponents<AudioSource>()[0].Stop();
                GetComponents<AudioSource>()[1].Play();
                resultsPanel.GetComponentInChildren<Text>().text = "¡Ronda finalizada!\nHas tenido " + correct + " aciertos y\nganado " + numberOfGems + " gemas verdes";
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
    /// Método que devuelve una lista con una serie de preguntas escogidas al azar.
    /// </summary>
    /// <returns>La lista con las preguntas escogidas al azar</returns>
    public List<int> SelectQuestions()
    {
        List<int> questions = new List<int>();
        System.Random r = new System.Random();

        int firstQuestion = r.Next(0, 20);

        for (int i = 0; i < numberOfQuestions; i++)
        {
            while (questions.Contains(firstQuestion))
            {
                firstQuestion = r.Next(0, 20);
            }

            questions.Add(firstQuestion);

        }

        // Mostrar la lista de preguntas seleccionada al azar
        string questionList = "";
        for(int i = 0; i < questions.Count; i++)
        {
            questionList += questions[i] + " ";
            
        }

        return questions;
    }

    /// <summary>
    /// Método que muestra cada una de las preguntas así como sus posibles respuestas asociadas 
    /// en los campos adecuados de la interfaz gráfica.
    /// </summary>
    public void ShowQuestion()
    {
        int question = selectedQuestions[count];

        currentQuestion = questionArray[question];
        correctAnswer = answerArray[question][0];
        currentAnswers = ShuffleResponse(answerArray[question]);

        mainPanel.GetComponentInChildren<Text>().text = currentQuestion;
        
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponentInChildren<Text>().text = currentAnswers[i];
        }
        
    }

    /// <summary>
    /// Método que mezcla la respuesta correcta de forma aleatoria entre todas las posibles.
    /// </summary>
    /// <param name="responses">El vector con las posibles respuestas a la pregunta</param>
    /// <returns>Un vector con las respuestas mezcladas aleatoriamente</returns>
    public string[] ShuffleResponse(string[] responses)
    {
        string[] shuffled = new string[responses.Length];

        for(int i = 0; i < shuffled.Length; i++)
        {
            shuffled[i] = responses[i];
        }

        System.Random r = new System.Random();
        int correctPosition = r.Next(0, 4);

        // Intercambiamos las posiciones de las respuestas
        string aux = shuffled[correctPosition];
        shuffled[correctPosition] = shuffled[0];
        shuffled[0] = aux;

        return shuffled;
    }

    /// <summary>
    /// Método que comprueba si la solución coincide con la dada por el jugador y muestra al usuario
    /// mensajes acordes a la situación.
    /// </summary>
    /// <param name="buttonClicked">El botón con la opción marcada por el jugador</param>
    public void CheckResponse(Button buttonClicked)
    {
        bool coincidence = false;
        coincidence = buttonClicked.GetComponentInChildren<Text>().text.Equals(correctAnswer);

        if(coincidence)
        {
            correct++;
            invisiblePanel.GetComponentInChildren<Text>().text = "¡Muy bien! Has acertado";
        } else
        {
            invisiblePanel.GetComponentInChildren<Text>().text = "¡Lo siento! La respuesta correcta es:\n" + correctAnswer;
        }

        timePanel.SetActive(false);
        invisiblePanel.SetActive(true);
    }

    /// <summary>
    /// Método que se llama al finalizar la ronda de preguntas y vuelve a la escena principal.
    /// </summary>
    public void EndGame()
    {
        // Se almacenan los resultados obtenidos en el minijuego.
        gameControl.gems[3] += numberOfGems;
        gameControl.games[3]++;
        gameControl.corrects[3] += correct;

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
