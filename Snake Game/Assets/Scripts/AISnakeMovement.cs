using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISnakeMovement : MonoBehaviour
{
    bool isSnakeAlive = true;
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
        // Move();
    }

    private void Move()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow) && !isVertical)
        {
            isVertical = true;
            xDirection = 0;
            yDirection = 1;
            rotation = Quaternion.Euler(0, 0, 0);
        }

        else if (Input.GetKeyUp(KeyCode.DownArrow) && !isVertical)
        {
            isVertical = true;
            xDirection = 0;
            yDirection = -1;
            rotation = Quaternion.Euler(0, 0, 180);
        }

        else if (Input.GetKeyUp(KeyCode.LeftArrow) && isVertical)
        {
            isVertical = false;
            xDirection = -1;
            yDirection = 0;
            rotation = Quaternion.Euler(0, 0, 90);
        }

        else if (Input.GetKeyUp(KeyCode.RightArrow) && isVertical)
        {
            isVertical = false;
            xDirection = 1;
            yDirection = 0;
            rotation = Quaternion.Euler(0, 0, -90);
        }
    }

    IEnumerator FixedRefresh()
    {

        float posX = transform.position.x - offsetX;
        float posY = transform.position.y - offsetY;

        while (isSnakeAlive)
        {
            yield return new WaitForSeconds(snakeSpeed);

            if (isSnakeAlive)
            {
                ClearSnakeHead();
                Instantiate(snakeParts[0], new Vector2(posX += xDirection, posY += yDirection), rotation);
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
}
