using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level : MonoBehaviour
{
    
    public Vector3 player1Spawn;
    public Vector3 player2Spawn;
    public Vector3 spawnDropValue1;
    public Vector3 spawnDropValue2;

    private Vector3 levelSpawnValue = new Vector3 (0,0,0);
    //private string levelName;
    private string attribute;
    private GameObject dropBlock1;
    private GameObject dropBlock2;
    private GameObject dropBlockNull;

    private void Update()
    {
        GameObject dropBlockObject = GameObject.Find("/"+ this.name + "/DropBlock1");
        if (dropBlockObject != null)
        {
            dropBlock1 = dropBlockObject;
        }
        if (dropBlock1 == null)
        {
            Debug.Log("Cannot find 'DropBlock1' : " + ("/" + this.name + "/DropBlock2"));
        }

        dropBlockObject = GameObject.Find("/" + this.name + "/DropBlock2");
        if (dropBlockObject != null)
        {
            dropBlock2 = dropBlockObject;
        }
        if (dropBlock2 == null)
        {
            Debug.Log("Cannot find 'DropBlock2' : " + ("/" + this.name + "/DropBlock2"));
        }
    }

    //public string GetName()
    //{
    //    return levelName;
    //}

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

    public void SetDropBlock(int num, GameObject dropTemp)
    {
        if (num == 1)
            dropBlock1 = dropTemp;
        if (num == 2)
            dropBlock2 = dropTemp;
    }

    public GameObject GetDropBlock(int num)
    {
        if (num == 1)
            return dropBlock1;
        if (num == 2)
            return dropBlock2;
        else
            return new GameObject();
    }

    public void SetPlayerSpawn(int num, Vector2 spawnTemp)
    {
        if (num == 1)
            player1Spawn = spawnTemp;
        if (num == 2)
            player2Spawn = spawnTemp;
    }

    public Vector2 GetPlayerSpawn(int num)
    {
        if (num == 1)
            return player1Spawn;
        if (num == 2)
            return player2Spawn;
        else return new Vector3();
    }
}