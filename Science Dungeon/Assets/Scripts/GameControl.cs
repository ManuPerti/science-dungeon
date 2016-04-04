using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Clase que controla toda la lógica global del juego:
///  - Guardar y cargar estados del juego.
///  - Mostrar información ordenada al usuario.
///  - Activar o desactivar objetos de juego.
///  - Métodos auxiliares para debugging.
/// </summary>
public class GameControl : MonoBehaviour {

    // Referencias a los elementos de la interfaz gráfica
    public static GameControl control;
    public GameObject mainPanel;
    public GameObject helpPanel;
    public GameObject pausePanel;
    public GameObject[] controlPanels;
    public GameObject player;
    public Text[] gemsText;
    public Text keysText;

    // Valores iniciales de posición y rotación del jugador
    public int sceneId = 1;
    public float positionX = 21;
    public float positionY = 1;
    public float positionZ = -21;
    public float rotationY = 0;

    // Arrays que almacenan objetos y datos para estadísticas
    public int[] gems = new int[4];
    public bool[] keys = new bool[4];
    public int[] games = new int[4];
    public int[] corrects = new int[4];

    public List<int> resolutionTime = new List<int>();
    public float globalTime = 0;

    // Lista de llaves recogidas por el jugador
    private List<GameObject> keysPicked;

    /// <summary>
    /// Método que se llama una única vez al iniciar el script antes de que se ejecute
    /// cualquier clase de lógica.
    /// </summary>
    void Awake()
    {
        /*if(control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if(control != this)
        {
            Destroy(gameObject);
        }*/

        control = GameObject.FindWithTag("GameController").GetComponent<GameControl>();

        Load();

    }

    /// <summary>
    /// Método para inicializar variables que se llama una única vez al iniciar el script
    /// </summary>
    void Start()
    {
        player.GetComponent<PlayerController>().enabled = false;
        Time.timeScale = 0;
        mainPanel.SetActive(false);
        helpPanel.SetActive(true);

    }

    /// <summary>
    /// Método para actualizar variables que se llama una vez por frame
    /// </summary>
    void Update()
    {
        // Si el personaje cae por algún agujero del mapa, volvemos a colocarlo en su posición inicial.
        if(player != null && player.transform.position.y < 0.5)
        {
            ResetPlayer();
        }

        if (helpPanel == null || !helpPanel.activeSelf)
        {
            globalTime += Time.deltaTime;

            FillGems(gemsText);
            CountKeys();
        } else
        {
            Time.timeScale = 0;
        }

    }

    /// <summary>
    /// Método público que permite guardar los datos del juego actual en un archivo binario.
    /// </summary>
    public void Save()
    {
        try
        {
            // Si no existe el directorio, se crea
            if (!Directory.Exists("Saves"))
                Directory.CreateDirectory("Saves");

            // Se crea un archivo binario donde guardar los datos
            BinaryFormatter bf = new BinaryFormatter();
            //FileStream file =  File.Create(Application.persistentDataPath + "/playerInfo.dat");
            FileStream saveFile = File.Create("Saves/playerInfo.dat");

            // Referencia a un objeto PlayerData serializado con los datos de la partida
            PlayerData data = new PlayerData();

            data.sceneId = SceneManager.GetActiveScene().buildIndex;
            data.positionX = player.transform.position.x;
            data.positionY = player.transform.position.y;
            data.positionZ = player.transform.position.z;
            data.rotationY = player.transform.eulerAngles.y;

            for (int i = 0; i < data.gems.Length; i++)
            {
               data.gems[i] = gems[i];
               data.keys[i] = keys[i];
               data.games[i] = games[i];
               data.games[i] = games[i];
            }

            data.globalTime = globalTime;

            bf.Serialize(saveFile, data);
            saveFile.Close();

            Debug.Log("Los datos se han guardado correctamente.");

        } catch (IOException e)
        {
            Debug.Log("No se han podido guardar los datos del juego. " + e.ToString());
        }
        

    }

    /// <summary>
    /// Método público que permite cargar los datos de un juego guardado almacenándolos en variables globales
    /// de la escena actual y llamar a los métodos que posicionan.
    /// </summary>
    public void Load()
    {
        try
        {
            // Si el archivo existe, se cargan los datos
            if (File.Exists("Saves/playerInfo.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream loadFile = File.Open("Saves/playerInfo.dat", FileMode.Open);
                PlayerData data = (PlayerData)bf.Deserialize(loadFile);
                loadFile.Close();

                // Cada dato se almacena en una variable para su manipulación durante la partida.
                sceneId = data.sceneId;
                positionX = data.positionX;
                positionY = data.positionY;
                positionZ = data.positionZ;
                rotationY = data.rotationY;

                for(int  i = 0; i < data.gems.Length; i++)
                {
                    gems[i] = data.gems[i];
                    keys[i] = data.keys[i];
                    games[i] = data.games[i];
                    corrects[i] = data.corrects[i];
                }

                globalTime = data.globalTime;
                
                RelocatePlayer();
                DestroyKeys();

                Debug.Log("Se ha cargado correctamente el juego");

            }

        } catch (IOException e)
        {
            Debug.Log("No ha sido posible cargar el juego. " + e.ToString());
        }
       
    }

    /// <summary>
    /// Método para resetear los valores del juego a los iniciales.
    /// </summary>
    public void ResetGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create("Saves/playerInfo.dat");

        PlayerData data = new PlayerData();

        data.sceneId = 1;
        data.positionX = 21;
        data.positionY = 1;
        data.positionZ = -21;
        data.rotationY = 0;

        data.gems = new int[4];
        data.keys = new bool[4];
        data.games = new int[4];

        data.resolutionTime = new List<int>();
        data.globalTime = 0;

        bf.Serialize(file, data);
        file.Close();
    }

    /// <summary>
    /// Método para colocar al jugador en una posición y ángulo de rotación determinados.
    /// </summary>
    public void RelocatePlayer()
    {
        Vector3 newPosition = new Vector3(positionX, positionY, positionZ);
        Quaternion newRotation = Quaternion.Euler(0, rotationY, 0);
        // Le damos al personaje su nueva posición y rotación.
        player.transform.position = newPosition;
        player.transform.rotation = newRotation;

    }

    /// <summary>
    /// Método que simplemente vuelve a colocar al jugador en su posición inicial,
    /// sin modificar nada más de la escena.
    /// </summary>
    public void ResetPlayer()
    {
        Vector3 newPosition = new Vector3(21, 1, -21);
        Quaternion newRotation = Quaternion.Euler(0, 0, 0);
        player.transform.position = newPosition;
        player.transform.rotation = newRotation;
    }

    /// <summary>
    /// Método para mostrar la primera parte de las instrucciones de juego.
    /// </summary>
    public void ShowInstrucionsOne()
    {
        helpPanel.SetActive(false);
        controlPanels[0].SetActive(true);
    }

    /// <summary>
    /// Método para mostrar la segunda parte de las instrucciones de juego.
    /// </summary>
    public void ShowInstrucionsTwo()
    {
        controlPanels[0].SetActive(false);
        controlPanels[1].SetActive(true);
    }

    /// <summary>
    /// Método que se ejecuta al iniciar el juego, tras el panel de bievenida.
    /// </summary>
    public void InitGame()
    {
        // Sólo iniciamos el juego si éste no está en pausa.
        if (!pausePanel.activeSelf)
        {
            controlPanels[1].SetActive(false);
            Time.timeScale = 1;
            helpPanel.SetActive(false);
            mainPanel.SetActive(true);
            player.GetComponent<PlayerController>().enabled = true;
        } else
        {
            controlPanels[1].SetActive(false);
        }
        
    }

    /// <summary>
    /// Método para mostrar en el canvas el número de gemas de cada tipo conseguidas por el jugador.
    /// </summary>
    public void FillGems(Text[] gemsText)
    {
        for (int i = 0; i < gemsText.Length; i++)
        {
            gemsText[i].text = "x " + gems[i].ToString();
        }
    }

    /// <summary>
    /// Método que permite contar el número de elementos verdaderos del vector booleano que almacena
    /// los datos de las llaves recogidas por el jugador.
    /// </summary>
    public void CountKeys()
    {
        int numberOfKeys = 0;

        for (int i = 0; i < keys.Length; i++)
        {
            if (keys[i])
            {
                numberOfKeys++;
            }
        }

        keysText.text = "x " + numberOfKeys.ToString();
    }

    /// <summary>
    /// Método que permite ordenar los elementos de una lista de objetos por orden alfabético ascendente.
    /// </summary>
    /// <param name="array">Una lista de GameObjects genéricos</param>
    public void OrderByName(List<GameObject> array)
    {
        array.Sort(
        delegate (GameObject i1, GameObject i2)
        {
            return i1.name.CompareTo(i2.name);
        }
        );
    }

    /// <summary>
    /// Método que destruye todos los objetos etiquetados como llave que ya posee el jugador para que
    /// no vuelvan a aparecer en la escena.
    /// </summary>
    public void DestroyKeys()
    {
        keysPicked = GameObject.FindGameObjectsWithTag("Key").ToList<GameObject>();
        OrderByName(keysPicked);
        PrintObjects(keysPicked);

        for(int i = 0; i < keysPicked.Count; i++)
        {
            if (keys[i])
            {
                Destroy(keysPicked[i]);
            }

        }
    }

    /// <summary>
    /// Método que devuelve la posición del máximo valor en un vector de enteros.
    /// </summary>
    /// <param name="array">Un vector de números enteros</param>
    /// <returns>La posición del valor máximo</returns>
    public int MaximumPosition(int[] array)
    {
        int maximum = -1;
        int maxValue = array.Max();

        if(maxValue > 0)
        {
            maximum = array.ToList().IndexOf(maxValue);
        }

        return maximum;
    }

    /// <summary>
    /// Método que hace desaparecer temporalmente un objeto de la escena.
    /// </summary>
    /// <param name="box">El objeto de juego a desaparecer</param>
    public void DeactivateBox(GameObject box)
    {
        box.GetComponent<MeshRenderer>().enabled = false;
        box.GetComponent<Collider>().enabled = false;
        box.GetComponent<Rotate>().enabled = false;
    }

    /// <summary>
    /// Método que hace reaparecer un objeto en la escena.
    /// </summary>
    /// <param name="box">El objeto de juego a reaparecer</param>
    public void ActivateBox(GameObject box)
    {
        box.GetComponent<MeshRenderer>().enabled = true;
        box.GetComponent<Collider>().enabled = true;
        box.GetComponent<Rotate>().enabled = true;
    }

    /// <summary>
    /// Método que hace desaparecer temporalmente a un enemigo de la escena.
    /// </summary>
    /// <param name="enemy">El enemigo a desaparecer</param>
    public void DeactivateEnemy(GameObject enemy)
    {
        enemy.GetComponentInChildren<MeshRenderer>().enabled = false;
        enemy.GetComponentInChildren<Collider>().enabled = false;
        enemy.GetComponentInChildren<RotateAround>().enabled = false;
        enemy.GetComponentInChildren<AudioSource>().enabled = false;
    }

    /// <summary>
    /// Método que hace reaparecer un enemigo en la escena.
    /// </summary>
    /// <param name="enemy">El enemigo a reaparecer</param>
    public void ActivateEnemy(GameObject enemy)
    {
        enemy.GetComponentInChildren<MeshRenderer>().enabled = true;
        enemy.GetComponentInChildren<Collider>().enabled = true;
        enemy.GetComponentInChildren<RotateAround>().enabled = true;
        enemy.GetComponentInChildren<AudioSource>().enabled = true;

    }


    /*
    * Métodos auxiliares para facilitar el Debugging y las pruebas de software
    */

    /// <summary>
    /// Método auxiliar para mostrar los elementos de un vector por consola.
    /// </summary>
    /// <param name="array">Un vector de números enteros</param>
    public void PrintArray(int[] array)
    {
        String printedArray = "";
        for (int i = 0; i < array.Length; i++)
        {
            printedArray += array[i] + " ";
        }

        Debug.Log(printedArray);
    }

    /// <summary>
    /// Método auxiliar para mostrar los elementos de un vector booleano por consola.
    /// </summary>
    /// <param name="array">Un vector de booleanos</param>
    public void PrintBoolean(bool[] array)
    {
        string logic = "";
        for (int i = 0; i < array.Length; i++)
        {
            logic += array[i] + " ";
        }

        Debug.Log(logic);
    }

    /// <summary>
    /// Método para mostrar los elementos de un vector de objetos por consola.
    /// </summary>
    /// <param name="list">Lista de GameObjects genéricos</param>
    public void PrintObjects(List<GameObject> list)
    {
        string names = "";
        for (int i = 0; i < list.Count; i++)
        {
            names += list[i].name + " ";
        }

        Debug.Log(names);
    }


    /// <summary>
    /// Método para mostrar los elementos de una lista por consola.
    /// </summary>
    /// <param name="lista">Una lista de números enteros</param>
    public void PrintList(List<int> lista)
    {
        String printedList = "";
        for (int i = 0; i < lista.Count; i++)
        {
            printedList += lista[i] + " ";
        }

        Debug.Log(printedList);
    }

}

/// <summary>
/// Clase que permite serializar todos los datos de la partida que posteriormente almacenaremos
/// en un archivo de guardado.
/// </summary>
[Serializable]
class PlayerData
{
    // Variables de posición del jugador
    public int sceneId;
    public float positionX;
    public float positionY;
    public float positionZ;
    public float rotationY;

    // Vector de gemas: amarillas, azules, rojas y verdes.
    public int[] gems = new int[4];

    // Vector de llaves recogidas
    public bool[] keys = new bool[4];

    // Partidas jugadas de cada minijuego
    public int[] games = new int[4];

    // Preguntas acertadas correctamente
    public int[] corrects = new int[4];

    // Variables que almacenan el tiempo de resolución.
    public List<int> resolutionTime = new List<int>();
    public float globalTime;
}
