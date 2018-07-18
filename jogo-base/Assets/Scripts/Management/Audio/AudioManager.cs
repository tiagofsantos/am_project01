using UnityEngine;
using UnityEditor;
using System.Collections;

/* AudioManager é a classe utilizada sempre que seja necessário utilizar um som.
 * 
 * Exemplo de utilização:
 *      AudioManager.instance.playSound("fire_bullet.mp3", tranform.position);
 *      
 * O transform.position está presente porque o sistema de som pode funcionar à base de proximidade,
 * ou seja, o som fica mais alto quanto mais perto estamos do objeto. Assim, é necessário
 * enviar a posição do objeto a emitir o som para que se possa calcular o volume do som.
 * A posição da personagem é desnecessária pois esta classe acede ao ponto central da câmera do jogo.
 * 
 * Em casos de música e outros sons que não dependem da proximidade, pode-se envocar o método
 * playSound sem o transform.position, que irá reproduzir o som com o valor por omissão de volume
 * (1). 
 * Exemplo:
 *      AudioManager.instance.playSound("background_music.mp3");
 * 
 * Caso seja necessário ter, por exemplo, sons de fundo com volumes costumizáveis independentes da distância,
 * pode-se envocar o método playSound que recebe o volume como parâmetro.
 * Exemplo:
 *      AudioManager.instance.playSound("item_pickup.mp3", 0.5);
 * 
 * Atenção: O valor do volume está sempre entre 0 e 1, em que 0 não há som e 1 é o valor máximo do som.
 * 
 * A classe guarda uma string com o caminho geral para a pasta de sons, por isso se o som se encontrar numa
 * sub pasta poderá ser necessário enviar o nome da sub pasta também ("Stalactite/stalactite_drop.mp3")
 *
 */
public class AudioManager : MonoBehaviour { 


    public static AudioManager instance;

    /*String que indica onde os sons estão guardados. 
     * Exemplo: Assets/Sounds/
     */
    private string audioFilePath;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioFilePath = "Assets/Audio/";
    }

    private IEnumerator play(string name, float volume)
    {
        AudioClip audioClip = (AudioClip)AssetDatabase.LoadAssetAtPath(audioFilePath + name, typeof(AudioClip));
        GameObject gameObject = new GameObject("Sound_" + audioFilePath + name);

        Sound s = new Sound(gameObject.AddComponent<AudioSource>(), audioClip);

        if (volume == -1)
        {
            s.play(); //Reproduz o som com valores pre definidos na classe Sound.
        }
        else
        {
            s.play(volume);
        }

        yield return new WaitForSeconds(audioClip.length); //Espera que o som acabe para poder destruir o objeto.

        Destroy(gameObject); // Quando o som acaba, o objeto deixa de ser necessário.
    }

    /* Reproduz o som baseado na proximidade do objeto */
    public void playSound(string name, Vector3 position)
    {
       float distanceToTarget = calculateDistance(position);
        
       StartCoroutine(play(name, calculateVolume(distanceToTarget)));
    }

    /* Reproduz o som com o volume pre-definido (1)*/
    public void playSound(string name)
    {
        StartCoroutine(play(name, -1));
    }

    /* Reproduz o som com um volume recebido */
    public void playSound(string name, float volume)
    {
        if (volume > 1)
        {
            volume = 1;
        }
        else if (volume< 0)
        {
            volume = 0;
        }

        StartCoroutine(play(name, volume));
    }

    /* Calcula a distância entre o objeto a emitir o som e o 
     * centro da câmera de jogo. 
     */
    private float calculateDistance(Vector3 position)
    {
        Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
        return Vector3.Distance(position, target);
    }
    
    /* Calcula o volume do som, a partir de uma determinada distância.
     * O cálculo é feito através de uma função linear onde o volume irá ser
     * 1 quando a distância for 19, e 0 quando a distância for 30.
     * (Valores 19 e 30 podem ser sujeitos a alteração, não houve fórmula para
     * os encontrar, foi afastando a personagem e vendo se ficava bem).
     */
    private float calculateVolume(float distance)
    {
        float volume = (-0.0909090909f * distance) + (2.7272727272f);

        if (volume > 1)
        {
            return 1;
        }
        else if (volume< 0)
        {
            return 0;
        }

        return volume;
    }
}