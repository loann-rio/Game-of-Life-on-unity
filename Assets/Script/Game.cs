using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    private static int screen_width = 64;
    private static int screen_height = 48;
    private static int sizeSquare = 16;

    Cell[,] grid = new Cell[screen_width, screen_height];

    public float speed = 0.2f;

    private float timer;

    public bool placing = true;

    // Start is called before the first frame update
    void Start()
    {
        initCells();    
    }

    // Update is called once per frame
    void Update()
    {
        if (placing)
        {
            // placing mode:
            placeCell();
        }
        else
        {
            if (timer >= speed)
            {
                timer = 0f;
                countNeighbors();
                getNextStep();
            } 
            else { timer += Time.deltaTime; }
        }
    }

    
    void initCells()
    {
        for (int i=0; i<screen_height; i++) 
        {
            for (int j=0; j<screen_width; j++)
            {
                Cell cell = Instantiate(Resources.Load("Prefab/Cell", typeof(Cell)), new Vector2(j, i), Quaternion.identity) as Cell;
                grid[j, i] = cell;
                grid[j, i].SetAlive(false);
            }
        }
    }

    void placeCell() 
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 mousePos = Input.mousePosition;
            float x =  (mousePos.x/sizeSquare);
            Debug.Log(x);
            grid[ (int) mousePos.x/sizeSquare, (int) mousePos.y/sizeSquare].SetAlive(!grid[ (int) mousePos.x/sizeSquare, (int) mousePos.y/sizeSquare].isAlive);
        }
    }

    void countNeighbors()
    {
        for (int y=0; y<screen_height;y++)
        {
            for (int x=0; x<screen_width; x++)
            {
                int numNeighbourg = 0;

                for (int i=-1; i<2; i++)
                {
                    for (int j=-1; j<2; j++)
                    {
                        if ((x+i>=0) && (y+j>=0) && (x+i<screen_width) && (y+j<screen_height) && ((i!=0)||(j!=0)) && (grid[x+i, y+j].isAlive))
                        {
                            numNeighbourg++;
                        }
                    }
                }

                grid[x, y].numNeighbourg = numNeighbourg;
            }
        }
    }

    void getNextStep()
    {
        for (int y=0; y<screen_height;y++)
        {
            for (int x=0; x<screen_width; x++)
            {
                if (grid[x, y].isAlive)
                {
                    if (grid[x, y].numNeighbourg !=2 && grid[x, y].numNeighbourg !=3)
                    {
                        grid[x, y].SetAlive(false);
                    }
                }
                else
                {
                    if (grid[x, y].numNeighbourg == 3)
                    {
                        grid[x, y].SetAlive(true);
                    }
                }
            }
        }
    }
}
