using UnityEngine;
using System.Collections;

/// <summary>
/// Clase que hace reaparecer en la escena de juego aquellos objetos que hayan
/// sido ocultados, habiendo pasado un tiempo determinada desde su desaparición.
/// </summary>
public class Spawner : MonoBehaviour {

    // Tiempos mínimos, máximos y distancia de reaparición del objeto. 
    public float minRespawnTime = 120;
    public float maxRespawnTime = 180;
    public float respawnDistance = 20;

    private GameControl gameControl;
    private GameObject player;

    private float distance;
    private float respawnTime;
    private bool respawnTimeRunning = false;
    private float timeLeft = 0;

    /// <summary>
    /// Método para inicializar variables que se llama una única vez al iniciar el script
    /// </summary>
    void Start () {

        gameControl = GameObject.FindWithTag("GameController").GetComponent<GameControl>();
        player = GameObject.FindWithTag("Player");
	}

    /// <summary>
    /// Método para actualizar variables que se llama una vez por frame
    /// </summary>
    void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);

        // No fijamos el tiempo de reaparición del objeto, hasta que éste no haya desaparecido.
        if (!gameObject.GetComponent<MeshRenderer>().enabled)
        {
            if(!respawnTimeRunning)
            {
                respawnTime = Random.Range(minRespawnTime, maxRespawnTime);
                timeLeft = respawnTime;
                respawnTimeRunning = true;
            }

            // Activamos la cuenta atrás para que reaparezca el objeto.
            if(timeLeft >= 0)
            {
                timeLeft -= Time.deltaTime;
            } else
            {
                // Impedimos que el objeto aparezca encima del jugador.
                if(distance >= respawnDistance)
                {
                    gameControl.ActivateBox(gameObject);
                    respawnTimeRunning = false;
                }

            }

        }

    }
}
