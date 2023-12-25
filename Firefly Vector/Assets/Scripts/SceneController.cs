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

    void Start()
    {
        ableToSetDirection = true;
    }

    void Update()
    {
        if (ableToSetDirection)
        {
            SetDirectionVector();
        }
    }

    public void SetDirectionVector()
    {
        if (Input.GetMouseButtonDown(0))
        {
            initialPosition = GetMousePosition();
            playerInScene = Instantiate(playerGameObject);
            playerInScene.transform.position = initialPosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            finalPosition = GetMousePosition();

            direction = (finalPosition - initialPosition).normalized;
            playerInScene.GetComponent<Player>().Initiate(initialPosition, direction);
        }
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
