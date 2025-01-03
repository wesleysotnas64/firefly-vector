using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public GameObject playerGameObject;
    public GameObject playerInScene;
    public Vector2 direction;
    public Vector2 initialPosition;
    public Vector2 finalPosition;
    public bool ableToSetDirection;
    public bool btnMouseDownIsPressed;
    public GameObject lineGameObject;
    public GameObject lineInScene;

    public int obstacleListCount;

    void Start()
    {
        ableToSetDirection = true;
        btnMouseDownIsPressed = false;
    }

    void Update()
    {
        if (ableToSetDirection)
        {
            SetDirectionVector();
        }

        if (btnMouseDownIsPressed)
        {
            CalcVectorInit();
            lineInScene.GetComponent<Line>().B.position = GetMousePosition();
        }
    }

    public void SetDirectionVector()
    {
        if (Input.GetMouseButtonDown(0))
        {
            btnMouseDownIsPressed = true;
            initialPosition = GetMousePosition();
            playerInScene = Instantiate(playerGameObject);
            playerInScene.transform.position = initialPosition;

            lineInScene = Instantiate(lineGameObject);
            lineInScene.GetComponent<Line>().A.position = initialPosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            btnMouseDownIsPressed = false;
            CalcVectorInit();
            playerInScene.GetComponent<Player>().Initiate(initialPosition, direction);

            Destroy(lineInScene.gameObject);
        }
    }

    private void CalcVectorInit(){
        finalPosition = GetMousePosition();
        direction = (finalPosition - initialPosition).normalized;
    }

    public Vector2 GetMousePosition()
    {
        Vector2 position;

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        position = new Vector2(mousePosition.x, mousePosition.y);

        return position;
    }
}
