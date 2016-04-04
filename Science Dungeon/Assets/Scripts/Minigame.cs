using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// Clase que controla el lanzamiento de cada uno de los minijuego dependiendo de
/// con qué objeto colisione el jugador.
/// </summary>
public class Minigame : MonoBehaviour {

    public GameObject miniGameCanvas;
    public GameObject miniGameManager;

    private GameControl gameControl;
    private GameObject[] enemies;
    private string miniGameId;

    /// <summary>
    /// Método para inicializar variables que se llama una única vez al iniciar el script
    /// </summary>
    void Start () {

        gameControl = GameObject.FindWithTag("GameController").GetComponent<GameControl>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

    }

    /// <summary>
    /// Método para actualizar variables que se llama una vez por frame
    /// </summary>
    void Update () {
	
	}


    /// <summary>
    /// Método que gestiona la colisión del jugador con alguno de los cubos de colores
    /// que llaman a los minijuegos.
    /// </summary>
    /// <param name="other">El objeto Player que colisiona con los cubos</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            miniGameId = this.gameObject.name.Substring(0, 7);

            gameControl.DeactivateBox(gameObject);

            foreach(GameObject enemy in enemies)
            {
                gameControl.DeactivateEnemy(enemy);
            }
            
            other.gameObject.GetComponent<PlayerController>().enabled = false;
            other.gameObject.GetComponent<AudioSource>().enabled = false;

            // Activamos el canvas y el Game Manager del minijuego en cuestión y llamamos
            // a la pantalla de bienvenida con las instrucciones.
            miniGameCanvas.SetActive(true);
            miniGameManager.SetActive(true);

            switch (miniGameId)
            {
                case "YellowC":
                    miniGameManager.GetComponent<MathsManager>().enabled = true;
                    miniGameManager.GetComponent<MathsManager>().Welcome();
                    break;

                case "BlueCub":
                    miniGameManager.GetComponent<SpatialManager>().enabled = true;
                    miniGameManager.GetComponent<SpatialManager>().Welcome();
                    break;

                case "RedCube":
                    miniGameManager.GetComponent<LogicManager>().enabled = true;
                    miniGameManager.GetComponent<LogicManager>().Welcome();
                    break;

                case "GreenCu":
                    miniGameManager.GetComponent<QuizManager>().enabled = true;
                    miniGameManager.GetComponent<QuizManager>().Welcome();
                    break;

            } 
        }
        

    }

}
