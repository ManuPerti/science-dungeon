using UnityEngine;
using System.Collections;

/// <summary>
/// Clase que permite rotar el objeto asociado en torno a otro proporcionado como pivote.
/// </summary>
public class RotateAround : MonoBehaviour {

    public Transform target; // the object to rotate around
    public int speed = 30; // the speed of rotation

    /// <summary>
    /// Método para inicializar variables que se llama una única vez al iniciar el script
    /// </summary>
    void Start()
    {
        if (target == null)
        {
            target = this.gameObject.transform;
            Debug.Log("Objetivo de rotación no determinado. Por defecto se escoge el padre");
        }
    }

    /// <summary>
    /// Método para actualizar variables que se llama una vez por frame
    /// </summary>
    void Update()
    {
        transform.RotateAround(target.transform.position, target.transform.up, speed * Time.deltaTime);
    }
}
