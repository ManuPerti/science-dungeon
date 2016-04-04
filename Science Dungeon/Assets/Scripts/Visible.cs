using UnityEngine;
using System.Collections;

/// <summary>
/// Clase que permite modelizar la visibilidad de los letreros 3D que se muestran al usuario,
/// de manera que estos se ocultan hasta que el jugador se acerca a una distancia determinada
/// y se ocultan en cuanto aquél se aleja.
/// </summary>
public class Visible : MonoBehaviour {

    // Distancia de visibilidad del letrero.
    public float visibleDistance = 10;

    private GameObject player;
    private float distance;


    /// <summary>
    /// Método para inicializar variables que se llama una única vez al iniciar el script.
    /// </summary>
    void Start () {
        player = GameObject.FindWithTag("Player");
    }

    /// <summary>
    /// Método para actualizar variables que se llama una vez por frame.
    /// </summary>
    void Update () {
        distance = Vector3.Distance(player.transform.position, this.transform.position);

        // Sólo mostraremos el texto si el objeto asociado es visible también.
        if (gameObject.transform.parent.GetComponent<MeshRenderer>().enabled)
        {
            if (distance <= visibleDistance)
            {
                GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                GetComponent<MeshRenderer>().enabled = false;
            }
        } else
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
        
    }
}
