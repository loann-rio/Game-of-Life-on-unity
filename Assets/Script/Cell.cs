using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool isAlive = false;
    public int numNeighbourg = 0;

    public void SetAlive (bool alive) {
        isAlive = alive;
        

        if (alive) 
        {
            Debug.Log("hello");
            GetComponent<SpriteRenderer>().enabled = true;
        } 
        else
        {
            Debug.Log("hello1");
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
