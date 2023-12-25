using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private bool ableToMove;

    void Start()
    {
        ableToMove = false;
    }

    void Update()
    {
        if (ableToMove)
        {
            Move();
        }
    }
    
    private void Move()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
    }

    private void SetDirection(Vector2 newDirection)
    {
        transform.up = newDirection;
    }

    public void Initiate(Vector2 initialPosition, Vector2 direction)
    {
        SetDirection(direction);
        ableToMove = true;
    }
}
