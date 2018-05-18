using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    private const string PREFAB_PATH = "Assets/Prefabs/";

    public GameObject playerPrefab;
    public GameObject shadowPrefab;

    public Transform playerSpawn;

    public EntitySpawner()
    {
        playerSpawn = GameObject.Find("PlayerSpawn").transform;
    }

    public GameObject spawnPlayer(Player player)
    {
        GameObject playerObject = createObject("Player.prefab", playerSpawn.transform);

        playerObject.AddComponent<Player>().init(player.user, player.character);
        playerObject.name = "Player (" + player.user.name + ")";

        return playerObject;
    }


    public GameObject spawnShadow(Player shadow)
    {
        GameObject shadowObject = createObject("Shadow.prefab", playerSpawn.transform);

        shadowObject.AddComponent<Player>().init(shadow.user, shadow.character);
        shadowObject.name = "Shadow (" + shadow.user.name + ")";

        return shadowObject;
    }

    private GameObject createObject(string filename, Transform spawn)
    {
        Object prefab = AssetDatabase.LoadAssetAtPath(PREFAB_PATH + filename, typeof(GameObject));
        return Instantiate(prefab, spawn.position, spawn.rotation) as GameObject;
    }

}
