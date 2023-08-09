using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{

    private float moveTimer = 0;

    [SerializeField] private float moveTimerMax = 0;

    private Vector2 direction;

    private Vector2 position;


    private void Awake()
    {
        position = new Vector2(0, 0);
        moveTimer = moveTimerMax;
        direction = new Vector2(0, 1);
    }

    void Update()
    {
        playerInput();
        handleGridMovement();
    }


    private void playerInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (direction != Vector2.down.normalized)
            {
                direction = Vector2.up.normalized;
                transform.rotation = Quaternion.Euler(Vector3.zero);
                //Debug.Log(direction);
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (direction != Vector2.up.normalized)
            {
                direction = Vector2.down;
                transform.rotation = Quaternion.Euler(0, 0, 180);
                //Debug.Log(direction);
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (direction != Vector2.right.normalized)
            {
                direction = Vector2.left.normalized;
                transform.rotation = Quaternion.Euler(0, 0, 90);
                //Debug.Log(direction);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (direction != Vector2.left.normalized)
            {
                direction = Vector2.right.normalized;
                transform.rotation = Quaternion.Euler(0, 0, -90);
                //Debug.Log(direction);
            }

        }

    }

    private void handleGridMovement()
    {
        moveTimer += Time.deltaTime;

        if (moveTimer >= moveTimerMax)
        {
            position += direction;
            moveTimer -= moveTimerMax;
            transform.position = new Vector3(position.x, position.y, 0);
        }
    }

}
