using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

// Script para reproducir clips de música y producir efectos especiales de sonido
public class PlayMusic : MonoBehaviour {

    public AudioClip[] musicClips;          //Array de clips para reproducir
    public AudioMixerSnapshot volumeDown;   //Referencia al Audio mixer en el que se baja el volumen master
    public AudioMixerSnapshot volumeUp;     //Referencia al Audio mixer en el que se sube el volumen master


    private AudioSource musicSource;        //Referencia al AudioSource que reproduce la música

    // Se ejecuta una única vez al llamar al método por primera vez
    void Awake()
    {
        // Obtiene una referencia al componente AudioSource añadido a la interfaz
        musicSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Método para inicializar variables que se llama una única vez al iniciar el script
    /// </summary>
    void Start()
    {
        PlaySelectedMusic(0);
    }

    // Método que permite reproducir un clip de música para una escena determinada, 
    // recibiendo como parámetro un entero que indica el clip a reproducir.
    public void PlaySelectedMusic(int musicChoice)
    {
        //Obtiene y reproduce el clip de música pasado como parámetro
        musicSource.clip = musicClips[musicChoice];
        musicSource.Play();
    }

    // Método para elevar el volumen del master mixer
    public void FadeUp(float fadeTime)
    {
        volumeUp.TransitionTo(fadeTime);
    }

    // Método para reducir el volumen del master mixer
    public void FadeDown(float fadeTime)
    {
        volumeDown.TransitionTo(fadeTime);
    }
}
