using UnityEngine;
using System.Collections;

/// <summary>
/// Clase que permite aplicar a una rotación constante a un objeto de juego
/// en torno a uno de los 3 ejes (X, Y o Z).
/// </summary>
public class Rotate : MonoBehaviour {

    // Velocidad de rotación.
    public float speed = 30.0f;
    // Posibles valores de ejes de rotación.
    public enum whichWayToRotate { AroundX, AroundY, AroundZ }
    // Eje en torno al cual girará el objeto.
    public whichWayToRotate way = whichWayToRotate.AroundY;

    /// <summary>
    /// Método para inicializar variables que se llama una única vez al iniciar el script
    /// </summary>
    void Start () {
	
	}

    /// <summary>
    /// Método para actualizar variables que se llama una vez por frame
    /// </summary>
    void Update () {
        switch (way)
        {
            case whichWayToRotate.AroundX:
                transform.Rotate(Vector3.right * Time.deltaTime * speed);
                break;
            case whichWayToRotate.AroundY:
                transform.Rotate(Vector3.up * Time.deltaTime * speed);
                break;
            case whichWayToRotate.AroundZ:
                transform.Rotate(Vector3.forward * Time.deltaTime * speed);
                break;
        }

    }
}
