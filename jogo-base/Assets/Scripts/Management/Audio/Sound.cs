using UnityEngine;

/* Define um som, a ser usado pela classe AudioManager.
 */
[System.Serializable]
public class Sound
{
    
    /* Ficheiro de áudio */
    public AudioClip clip; 
    private AudioSource source;
    
    public float volume = 1f;
    public float pitch = 1f;

    public Sound(AudioSource source, AudioClip clip)
    {
        this.clip = clip;
        setSource(source);
    }
    public void setSource(AudioSource source)
    {
        this.source = source;
        source.clip = clip;
    }
    
    /* Reproduz o som com o volume e pitch recebidos */
    public void play(float volume, float pitch)
    {
        source.volume = volume;
        source.pitch = pitch;
        source.Play();
   }

    /* Reproduz o som com o volume recebido, pitch pre definido.*/
    public void play(float volume)
    {
        play(volume, pitch);
    }

    /* Reproduz o som com ambos volume e pitch pre definidos. */
    public void play()
    {
        play(volume, pitch);
    }
}
