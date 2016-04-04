using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Just in case so no "duplicate definition" stuff shows up
namespace UnityStandardAssets.Copy._2D
{
    public class Restarter : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                //Application.LoadLevel(Application.loadedLevelName);
            }
        }
    }
}
