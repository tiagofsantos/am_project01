using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EntitySpawner
{
    private const string PREFAB_PATH = "Assets/Prefabs/";

    public GameObject playerPrefab;
    public GameObject shadowPrefab;

    public Transform playerSpawn;

    public EntitySpawner()
    {
        playerSpawn = GameObject.Find("PlayerSpawn").transform;
    }
    
    public GameObject spawnPlayer(User user, Character character)
    {
        GameObject playerObject = createObject("Player.prefab", playerSpawn.transform);
        playerObject.name = "Player (" + user.name + ")";

        Player player = playerObject.GetComponent<Player>();
        player.character = character;
        player.user = user;

        return playerObject;
    }


    public GameObject spawnShadow(User user, Character character)
    {
        GameObject shadowObject = createObject("Shadow.prefab", playerSpawn.transform);
        shadowObject.name = "Shadow (" + user.name + ")";

        Player player = shadowObject.GetComponent<Player>();
        player.character = character;
        player.user = user;

        return shadowObject;
    }

    private GameObject createObject(string filename, Transform spawn)
    {
        Object prefab = AssetDatabase.LoadAssetAtPath(PREFAB_PATH + filename, typeof(GameObject));
        return GameObject.Instantiate(prefab, spawn.position, spawn.rotation) as GameObject;
    }

}
