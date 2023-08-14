using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{

    private float moveTimer = 0;

    [SerializeField] private float moveTimerMax;

    [SerializeField] private List<Transform> segments;

    [SerializeField] private Transform segmentsPrefab;

    [SerializeField] private float gap;

    [SerializeField] private BoxCollider2D gridArea;

    private Vector2 direction;
    private Vector2 position;
    private int startSize = 3;
    private int score = 0;



    private void Awake()
    {
        position = new Vector2(0, 0);
        moveTimer = moveTimerMax;
        direction = new Vector2(0, 2);
    }

    private void Start()
    {
        segments = new List<Transform>();
        segments.Add(this.transform);

        for (int i = 1; i <= startSize; i++)
        {
            AddSegments();
        }
    }


    void Update()
    {
        playerInput();

    }

    private void FixedUpdate()
    {
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
            ScreenWrap(ref position);
            moveTimer -= moveTimerMax;
            transform.position = new Vector3(position.x, position.y, 0);
            UpdateSegments();
        }
    }

    public void IncreaseScore(int _score)
    {
        AddSegments();
        score += _score;
        Debug.Log("Score:" + score);
    }

    public void DecreaseScore(int _score)
    {
        if (score > 0)
        {
            if (segments.Count > 3)
            {
                Transform lastSegments = segments[segments.Count - 1];
                segments.Remove(lastSegments);
                Destroy(lastSegments.gameObject);
                score -= _score;
                Debug.Log("Score:" + score);
            }

        }
        else
        {
            score = 0;
        }

    }

    private void UpdateSegments()
    {
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }
        segments[0].position = transform.position;
    }

    private void AddSegments()
    {
        Transform segment = Instantiate(segmentsPrefab);
        segment.position = this.transform.position;
        segments.Add(segment);
    }

    private void ScreenWrap(ref Vector2 position)
    {
        if (position.x < gridArea.bounds.min.x)
        {
            position.x = gridArea.bounds.max.x;
        }
        else if (position.x > gridArea.bounds.max.x)
        {
            position.x = gridArea.bounds.min.x;
        }
        else if (position.y < gridArea.bounds.min.y)
        {
            position.y = gridArea.bounds.max.y;
        }
        else if(position.y > gridArea.bounds.max.y)
        {
            position.y = gridArea.bounds.min.y;
        }
    }


}
