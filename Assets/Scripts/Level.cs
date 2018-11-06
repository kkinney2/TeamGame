using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level : MonoBehaviour
{
    
    public Vector3 player1Spawn;
    public Vector3 player2Spawn;

    private Vector3 levelSpawnValue = new Vector3 (0,0,0);
    //private string levelName;
    private string attribute;
    private GameController gameController;

    
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
    public void DestroyLevel()
    {
        if(this.gameObject != null)
        {
            Destroy(gameObject);
            Debug.Log("Level Destroyed");
        }
    }
}