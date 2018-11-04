using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level : MonoBehaviour
{
    private Vector2 player1Spawn;
    private Vector2 player2Spawn;
    private Vector3 spawnDropValue1;
    private Vector3 spawnDropValue2;
    private Vector3 levelSpawnValue = new Vector3 (0,0,0);
    private string levelName;
    private string attribute;
    private GameObject dropBlock1;
    private GameObject dropBlock2;
    private GameObject dropBlockNull;

    public void Start()
    {
        var children = this.GetComponentsInChildren<GameObject>();
        foreach (var child in children)
        {
            if (child.name == "DropBlock1")
            {
                dropBlock1 = child;
            }
            if (child.name == "DropBlock2")
            {
                dropBlock2 = child;
            }
            if (child.name == "Player1_Spawn")
            {
                player1Spawn = child.transform.position;
            }
            if (child.name == "Player2_Spawn")
            {
                player2Spawn = child.transform.position;
            }
        }
    }

    public string GetName()
    {
        return levelName;
    }

    public void SetAttribute(string newAttribute)
    {
        attribute = newAttribute;
    }

    public string GetAttribute()
    {
        return attribute;
    }

    public Vector3 GetLevelSpawn()
    {
        return levelSpawnValue;
    }

    public GameObject GetDropBlock(int num)
    {
        if (num == 1)
            return dropBlock1;
        if (num == 2)
            return dropBlock2;
        else
            return dropBlockNull;
    }

    public Vector3 GetPlayerSpawn(int num)
    {
        if (num == 1)
            return player1Spawn;
        if (num == 2)
            return player2Spawn;
        else return new Vector3(0, 0, 0);
    }
}