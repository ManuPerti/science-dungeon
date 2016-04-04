using UnityEngine;
using System.Collections;

/// <summary>
/// Clase que modeliza el comportamiento de las puertas para que el
/// jugador las atraviesa cuando cumpla una condición determinada.
/// </summary>
public class OpenDoor : MonoBehaviour {

    public float actionDistance = 5;
    public float visibleDistance = 15;
    public int[] gemsToOpen = new int[4];
    public bool[] keysToOpen = new bool[4];

    private GameObject player;
    private GameControl gameControl;
    private AudioSource audioSource;

    private int[] gemsPlayer;
    private bool[] keysPlayer;
    private float distance;
    private bool opened = false;
    private bool soundPlaying = false;

    /// <summary>
    /// Método para inicializar variables que se llama una única vez al iniciar el script
    /// </summary>
    void Start () {
        player = GameObject.FindWithTag("Player");
        gameControl = GameObject.FindWithTag("GameController").GetComponent<GameControl>();
        audioSource = GetComponent<AudioSource>();

    }

    /// <summary>
    /// Método para actualizar variables que se llama una vez por frame
    /// </summary>
    void Update () {
        distance = Vector3.Distance(player.transform.position, this.transform.position);
        gemsPlayer = gameControl.gems;
        keysPlayer = gameControl.keys;

        if (distance <= visibleDistance & !opened)
        {
            GetComponentsInChildren<MeshRenderer>()[1].enabled = true;
        }
        else
        {
            GetComponentsInChildren<MeshRenderer>()[1].enabled = false;
        }

        // Comprobamos si se cumple la condición de la puerta para abrirse.
        if((distance <= actionDistance) && CheckGemsCondition() && CheckKeysCondition())
        {
            GetComponent<BoxCollider>().enabled = false;
            GetComponentsInChildren<MeshRenderer>()[0].enabled = false;
            GetComponentsInChildren<MeshRenderer>()[1].enabled = false;
            opened = true;
            if (!soundPlaying)
            {
                // Si nos encontramos en la última puerta, detenemos la música.
                if(name.Equals("Final Door"))
                {
                    player.GetComponent<AudioSource>().Stop();
                }
                audioSource.Play();
                soundPlaying = true;
            }
            
        }
    }

    /// <summary>
    /// Método que comprueba si el jugador posee suficientes gemas como para abrir la puerta indicada.
    /// </summary>
    /// <returns></returns>
    bool CheckGemsCondition()
    {
        return (gemsPlayer[0] >= gemsToOpen[0]
            && gemsPlayer[1] >= gemsToOpen[1]
            && gemsPlayer[2] >= gemsToOpen[2]
            && gemsPlayer[3] >= gemsToOpen[3]);
    }

    /// <summary>
    /// Método que comprueba si el jugador posee suficientes llaves como para abrir la puerta indicada.
    /// </summary>
    /// <returns></returns>
    bool CheckKeysCondition()
    {
        return (CountKeys(keysPlayer) >= CountKeys(keysToOpen));
        
    }

    /// <summary>
    /// Método que cuenta el número de elementos verdaderos de un vector booleano.
    /// </summary>
    /// <param name="keysArray">Un vector de booleanos</param>
    /// <returns>El número de elementos que son verdaderos</returns>
    int CountKeys(bool[] keysArray)
    {
        int number = 0;

        for(int i = 0; i < keysArray.Length; i++)
        {
            if (keysArray[i])
            {
                number++;
            }
        }

        return number;

    }
}
