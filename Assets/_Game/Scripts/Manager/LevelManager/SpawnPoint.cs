using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private Vector2 lastPos;
    public bool CanSpawn(float space)
    {
        if(transform.position.y - lastPos.y == space)
        {
            return true;
        }
        return false;
    }
    public void SetLastPos(Vector2 pos) 
    {
        lastPos = pos;
    }
}
