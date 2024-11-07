using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Cinemachine.CinemachineFreeLook;

public class AISnakeMovement : MonoBehaviour
{
    bool isSnakeAlive = true;
    bool isMatchOver = false;
    bool isVertical = true;


    bool isFacingNorth = true;
    bool isFacingSouth = false;
    bool isFacingEast = false;
    bool isFacingWest = false;
    

    int xDirection, yDirection;
    int snakeLength = 2;
    Quaternion rotation;

    List<GameObject> snakeBody = new List<GameObject>();

    [SerializeField] GameObject[] snakeParts;
    [SerializeField] float snakeSpeed = 1f;
    [SerializeField] int offsetX, offsetY;

    private void Start()
    {
        StartCoroutine(FixedRefresh());
        InitialStatus();
    }

    private void InitialStatus()
    {
        isVertical = true;
        xDirection = 0;
        yDirection = 1;
        rotation = Quaternion.Euler(0, 0, 0);
    }

    private void LocateFruit()
    {
        if(GameObject.FindGameObjectWithTag("Fruit") != null)
        {
            GameObject fruit = GameObject.FindGameObjectWithTag("Fruit");            

            if (GameObject.FindGameObjectWithTag("AI Snake Head") != null)
            {
                GameObject snakeHead = GameObject.FindGameObjectWithTag("AI Snake Head");
                int nDistance = (int)Vector2.Distance(snakeHead.transform.position + new Vector3(0, 1, 0), fruit.transform.position);
                int eDistance = (int)Vector2.Distance(snakeHead.transform.position + new Vector3(1, 0, 0), fruit.transform.position);
                int sDistance = (int)Vector2.Distance(snakeHead.transform.position + new Vector3(0, -1, 0), fruit.transform.position);
                int wDistance = (int)Vector2.Distance(snakeHead.transform.position + new Vector3(-1, 0, 0), fruit.transform.position);

                List<int> dList = new List<int>();
                dList.Add(nDistance);
                dList.Add(eDistance);
                dList.Add(sDistance);
                dList.Add(wDistance);

                int lowest = dList.Min();
                int indexOfLowest = dList.IndexOf(lowest);

                switch (indexOfLowest)
                {
                    case 0:
                        if (!isFacingSouth)
                        {
                            GoForward(); break;
                        }
                        RandomHorizontal();
                        break;
                    case 1:
                        if (!isFacingWest)
                        {
                            TurnRight(); break;
                        }
                        RandomVertical();
                        break;
                    case 2:
                        if (!isFacingNorth)
                        {
                            GoBack(); break;
                        }
                        RandomHorizontal();
                        break;
                    case 3:
                        if (!isFacingEast)
                        {
                            TurnLeft(); break;
                        }
                        RandomVertical();
                        break;
                }
            }
        }
    }

    private void RandomHorizontal()
    {
        if (Random.Range(0, 2) == 0)
        {
            TurnLeft();
        }
        else
        {
            TurnRight();
        }
    }

    private void RandomVertical()
    {
        if (Random.Range(0, 2) == 0)
        {
            GoForward();
        }
        else
        {
            GoBack();
        }
    }

    private void TurnLeft()
    {
        isFacingWest = true;
        isFacingEast = false;
        isFacingNorth = false; 
        isFacingSouth = false;

        xDirection = -1;
        yDirection = 0;
        rotation = Quaternion.Euler(0, 0, 90);
    }

    private void GoBack()
    {
        isFacingWest = false;
        isFacingEast = false;
        isFacingNorth = false;
        isFacingSouth = true; 

        xDirection = 0;
        yDirection = -1;
        rotation = Quaternion.Euler(0, 0, 180);
    }

    private void TurnRight()
    {
        isFacingWest = false;
        isFacingEast = true;
        isFacingNorth = false;
        isFacingSouth = false;

        xDirection = 1;
        yDirection = 0;
        rotation = Quaternion.Euler(0, 0, -90);
    }

    private void GoForward()
    {
        isFacingWest = false;
        isFacingEast = false;
        isFacingNorth = true;
        isFacingSouth = false;

        xDirection = 0;
        yDirection = 1;
        rotation = Quaternion.Euler(0, 0, 0);
    }

    IEnumerator FixedRefresh()
    {        
        int posX = (int)transform.position.x - offsetX;
        int posY = (int)transform.position.y - offsetY;

        while (isSnakeAlive && !isMatchOver)
        {
            LocateFruit();
            yield return new WaitForSeconds(snakeSpeed);
            
            CheckObstacles(posX, posY);
            if (isSnakeAlive)
            {
                ClearSnakeHead();
                posX += xDirection;
                posY += yDirection;
                Instantiate(snakeParts[0], new Vector2(posX, posY), rotation);
            }
        }
    }

    private void ClearSnakeHead()
    {
        if (GameObject.FindGameObjectWithTag("AI Snake Head") != null)
        {
            var snakeHead = GameObject.FindGameObjectWithTag("AI Snake Head");
            int posX = (int)snakeHead.transform.position.x;
            int posY = (int)snakeHead.transform.position.y;
            Destroy(snakeHead);
            CreateSnakeBody(posX, posY);
        }
    }

    private void CreateSnakeBody(int posX, int posY)
    {
        var part = Instantiate(snakeParts[1], new Vector2(posX, posY), rotation);
        part.GetComponent<SegmentScript>().SetDuration(snakeSpeed * snakeLength, snakeSpeed);
    }

    public void SetSnakeStatus(bool status)
    {
        isSnakeAlive = status;
    }

    public bool GetSnakeStatus()
    {
        return isSnakeAlive;
    }

    public void FruitEaten()
    {
        snakeLength++;
    }

    void CheckObstacles(int posX, int posY)
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        GameObject[] snakeSegments = GameObject.FindGameObjectsWithTag("Snake Body");
        GameObject[] snakeHead = GameObject.FindGameObjectsWithTag("Snake Head");

        List<GameObject> obstacles = new List<GameObject>();
        foreach(var items in walls)
        {
            obstacles.Add(items);
        }
        foreach (var items in snakeSegments)
        {
            obstacles.Add(items);
        }
        foreach (var items in snakeHead)
        {
            obstacles.Add(items);
        }

        foreach (var obstacle in obstacles)
        {
            if((posX + xDirection) == (int)obstacle.transform.position.x && (posY + yDirection) == (int)obstacle.transform.position.y)
            {
                // Debug.Log((posX + xDirection) + ":" + (posY + yDirection));
                // Debug.Log("Colliding with : " + obstacle.gameObject.name);
                ChangeDirection(posX, posY, obstacles);
                break;
            }
        }
    }

    void ChangeDirection(int posX, int posY, List<GameObject> obstacles)
    {
        // List<int> options = new List<int>();
        // 
        int[] options = new int[4];       

        // NORTH
        foreach (var item in obstacles)
        {
            if ((int)item.transform.position.x == posX && (int)item.transform.position.y == (posY + 1))
            {
                options[0] = 0;
                break;
            }
            options[0] = 1;
        }

        // EAST
        foreach (var item in obstacles)
        {
            if ((int)item.transform.position.x == (posX + 1) && (int)item.transform.position.y == posY)
            {
                options[1] = 0;
                break;
            }
            options[1] = 1;
        }

        // SOUTH
        foreach (var item in obstacles)
        {

            if ((int)item.transform.position.x == posX && (int)item.transform.position.y == (posY - 1))
            {
                options[2] = 0;
                break;
            }
            options[2] = 1;

        }

        // WEST
        foreach (var item in obstacles)
        {
            if ((int)item.transform.position.x == (posX - 1) && (int)item.transform.position.y == posY)
            {
                options[3] = 0;
                break;
            }
            options[3] = 1;
        }

        Debug.Log(options[0] + ":" + options[1] + ":" + options[2] + ":" + options[3]);

        List<int> safeOption = new List<int>();
        for(int i = 0; i < options.Length; i++)
        {
            if (options[i] == 1)
            {
                safeOption.Add(i);
            }    
        }

        
        if(safeOption.Count > 0)
        {
            int finalChoice = safeOption[Random.Range(0, safeOption.Count)];

            Debug.Log("Final Choice : " + finalChoice);

            if (finalChoice != 0)
            {
                switch (finalChoice)
                {
                    case 0:
                        GoForward();
                        break;
                    case 1:
                        TurnRight();
                        break;
                    case 2:
                        GoBack();
                        break;
                    case 3:
                        TurnLeft();
                        break;
                }
            }
        }        
    }

    public void SetMatchStatus(bool status)
    {
        isMatchOver = status;
    }

    public bool GetMatchStatus()
    {
        return isMatchOver;
    }
}
