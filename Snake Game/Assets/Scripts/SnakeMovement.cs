using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    bool isSnakeAlive = true;
    bool isMatchOver = false;
    bool isVertical = true;

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

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow) && !isVertical)
        {
            isVertical = true;
            GoForeward();
        }

        else if (Input.GetKeyUp(KeyCode.DownArrow) && !isVertical)
        {
            isVertical = true;
            GoBack();
        }

        else if (Input.GetKeyUp(KeyCode.LeftArrow) && isVertical)
        {
            isVertical = false;
            TurnLeft();
        }

        else if (Input.GetKeyUp(KeyCode.RightArrow) && isVertical)
        {
            isVertical = false;
            TurnRight();
        }
    }

    private void TurnRight()
    {
        xDirection = 1;
        yDirection = 0;
        rotation = Quaternion.Euler(0, 0, -90);
    }

    private void TurnLeft()
    {
        xDirection = -1;
        yDirection = 0;
        rotation = Quaternion.Euler(0, 0, 90);
    }

    private void GoBack()
    {
        xDirection = 0;
        yDirection = -1;
        rotation = Quaternion.Euler(0, 0, 180);
    }

    private void GoForeward()
    {
        xDirection = 0;
        yDirection = 1;
        rotation = Quaternion.Euler(0, 0, 0);
    }

    IEnumerator FixedRefresh()
    {
        
        int posX = (int)transform.position.x - offsetX;
        int posY = (int)transform.position.y - offsetY;

        while (isSnakeAlive)
        {
            yield return new WaitForSeconds(snakeSpeed);
            
            if(isSnakeAlive && !isMatchOver)
            {
                ClearSnakeHead();
                Instantiate(snakeParts[0], new Vector2(posX += xDirection, posY += yDirection), rotation);
            }            
        }
    }

    private void ClearSnakeHead()
    {
        if (GameObject.FindGameObjectWithTag("Snake Head") != null)
        {
            var snakeHead = GameObject.FindGameObjectWithTag("Snake Head");
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

    public void SetMatchStatus(bool status)
    {
        isMatchOver = status;
    }

    public bool GetMatchStatus()
    {
        return isMatchOver;
    }
}
