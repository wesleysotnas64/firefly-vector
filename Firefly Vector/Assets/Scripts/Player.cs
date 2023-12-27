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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;

        if (tag == "Obstacle")
        {
            Vector2 newDirection = ReflecVector(transform.up, collision.gameObject.transform.up);
            SetDirection(newDirection);
        }
    }

    private Vector2 ReflecVector(Vector2 input, Vector2 normal)
    {
        input.Normalize();
        normal.Normalize();

        Vector2 reflection = input - 2 * Vector2.Dot(input, normal) * normal;

        return reflection;
    }
}
