using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// Clase que permite ajustar el volumen de la música y de los efectos sonoros en el AudioMixer.
/// </summary>
public class SetAudioLevels : MonoBehaviour {

    public AudioMixer mainMixer;

    /// <summary>
    /// Método que permite ajustar el volumen de la música empleando un objeto de tipo AudioMixer.
    /// </summary>
    /// <param name="musicLevel">El nuevo nivel de la música</param>
    public void SetMusicLevel(float musicLevel)
    {
        mainMixer.SetFloat("musicVol", musicLevel);
    }

    /// <summary>
    /// Método que permite ajustar el volumen de los efectos sonoros empleando un objeto de tipo AudioMixer.
    /// </summary>
    /// <param name="sfxLevel">El nuevo nivel de los efectos sonoros</param>
    public void SetSfxLevel(float sfxLevel)
    {
        mainMixer.SetFloat("sfxVol", sfxLevel);
    }
}
