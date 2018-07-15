using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    /* when the main scene is loaded inicialize the game depending on game type */
    void OnLevelWasLoaded()
    {
        if(GameManager.instance.gameType == GameType.MULTI)
            GameManager.instance.playMulti();
        else{
            GameManager.instance.playSolo();
        }
    }

}
