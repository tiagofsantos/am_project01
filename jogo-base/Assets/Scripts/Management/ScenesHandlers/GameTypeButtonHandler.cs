using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameType { SINGLE, MULTI };

public class GameTypeButtonHandler : MonoBehaviour {

    /* Button single game */
    public void single()
    {
         GameManager.instance.gameType = GameType.SINGLE;
         SceneManager.LoadScene("Scenes/LevelCharacterChoose");
    }

    /* Button multiplayer */
    public void multi()
    {
        GameManager.instance.gameType = GameType.MULTI;
        SceneManager.LoadScene("Scenes/PlayerChoose");

    }

}
