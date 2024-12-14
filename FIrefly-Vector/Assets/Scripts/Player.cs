using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private bool ableToMove;

    [SerializeField]
    private Vector2 attractionObjectPosition;

    [SerializeField]
    private bool inAttractionArea;

    [SerializeField]
    private Vector2 attractVector;

    [SerializeField]
    private float attractSpeed;

    void Start()
    {
        ableToMove = false;
        inAttractionArea = false;
        attractVector = transform.up;
        attractSpeed = 1.0f;
    }

    void Update()
    {
        if (ableToMove)
        {
            Move();
        }

        if (inAttractionArea)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
            attractVector = (attractionObjectPosition - (Vector2) transform.position).normalized;

            Vector2 newDirection = Vector2.Lerp(transform.up, attractVector, Time.deltaTime * attractSpeed);

            SetDirection(newDirection);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
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

    private Vector2 ReflecVector(Vector2 input, Vector2 normal)
    {
        input.Normalize();
        normal.Normalize();

        Vector2 reflection = input - 2 * Vector2.Dot(input, normal) * normal;

        return reflection;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;

        if (tag == "Obstacle")
        {
            Vector2 newDirection = ReflecVector(transform.up, collision.gameObject.transform.up);
            SetDirection(newDirection);
            Destroy(collision.gameObject);
        }
        else if (tag == "CircleObstacle")
        {
            Vector2 newDirection = ReflecVector(transform.up, (Vector2) (collision.gameObject.transform.position - transform.position).normalized);
            SetDirection(newDirection);
            Destroy(collision.gameObject);
        }
        else if (tag == "IndestructibleObstacle")
        {
            Vector2 newDirection = ReflecVector(transform.up, collision.gameObject.transform.up);
            SetDirection(newDirection);
        }
        else if (tag == "IndestructibleCircleObstacle")
        {
            Vector2 newDirection = ReflecVector(transform.up, (Vector2) (collision.gameObject.transform.position - transform.position).normalized);
            SetDirection(newDirection);
        }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        inAttractionArea = true;
        attractionObjectPosition = (Vector2) col.gameObject.transform.position;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        inAttractionArea = false;
        attractVector = transform.up;
    }

    
}
