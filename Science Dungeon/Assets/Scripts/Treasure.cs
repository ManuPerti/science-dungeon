using UnityEngine;
using System.Collections;

/// <summary>
/// Clase que modeliza el comportamiento del tesoro para mostrar mensajes o abrirse
/// dependiendo de la proximidad del jugador.
/// </summary>
public class Treasure : MonoBehaviour {

    // Distancia de visibilidad y apertura del cofre respecto al personaje.
    public float visibleDistance = 10;
    public float openDistance = 5;

    // Referencia a la tapa del cofre del tesoro
    public GameObject hinge;

    private GameObject player;
    private float distance;

    /// <summary>
    /// Método para inicializar variables que se llama una única vez al iniciar el script
    /// </summary>
    void Start () {
        player = GameObject.FindWithTag("Player");

    }
	
	/// <summary>
    /// Método para actualizar variables que se llama una vez por frame
    /// </summary>
	void Update () {
        distance = Vector3.Distance(player.transform.position, this.transform.position);

        // Mostramos un mensaje al jugador cuando esté a una distancia determinada
        if (distance <= visibleDistance)
        {
            GetComponentInChildren<MeshRenderer>().enabled = true;
        }
        else
        {
            GetComponentInChildren<MeshRenderer>().enabled = false;
        }

        // Abrimos el cofre en cuanto el jugador esté aún más cerca de éste
        if(distance <= openDistance)
        {
            
            Quaternion newRotation = Quaternion.Euler(-120, 180, 0);
            hinge.transform.rotation = newRotation;
            
        }

    }
}
