using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_Controller : MonoBehaviour
{

    private float moveTimer;

    [SerializeField] private float moveTimerMax;

    [SerializeField] private List<Transform> segments;

    [SerializeField] private Transform segmentsPrefab;

    [SerializeField] private BoxCollider2D gridArea;

    [SerializeField] private Transform spwanPos;

    [SerializeField] private TMP_Text ScoreText;

    [SerializeField] private TMP_Text ShieldText;

    [SerializeField] private TMP_Text DoubleSpeedText;

    [SerializeField] private TMP_Text DoubleScoreText;

    [SerializeField] private GameObject GameOverPanel;

    [SerializeField] private Pause_Game_Controller PauseGame;

    private Vector2 direction;
    private Vector2 position;
    private int startSize = 3;
    private int score = 0;

    private bool isSpeedUp;
    private float originalSpeed;
    private Coroutine SpeedUpCo;

    private bool isScoreBoost;
    private Coroutine ScoreBoostCo;

    private bool isShieldActive;
    private Coroutine ShieldCo;
    private bool isPaused;

    private void Awake()
    {
        position = new Vector2(0, 0);
        moveTimer = moveTimerMax;
        direction = new Vector2(0, 1);
        GameOverPanel.SetActive(false);
        isPaused = false;
    }

    private void Start()
    {
        segments = new List<Transform>();
        segments.Add(spwanPos);

        for (int i = 1; i <= startSize; i++)
        {
            AddSegments();
        }
        isSpeedUp = false;
        originalSpeed = moveTimerMax;
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
        PauseGameUI();

    }

    private void handleGridMovement()
    {
        moveTimer += Time.deltaTime;

        if (isSpeedUp)
        {
            moveTimerMax = 0.1f;
        }
        else if (!isSpeedUp)
        {
            moveTimerMax = originalSpeed;
        }

        if (moveTimer >= moveTimerMax)
        {
            position += direction;
            ScreenWrap(ref position);
            moveTimer -= moveTimerMax;
            transform.position = new Vector3(position.x, position.y, 0);
            UpdateSegments();
        }

        if (!isShieldActive)
        {
            for (int i = 0; i < segments.Count; i++)
            {
                if (transform.position == segments[i].position)
                {
                    Debug.Log("Game over");
                    GameOverPanel.SetActive(true);
                    Audio_Manager.instance.Play(SoundName.GameOver);
                    this.enabled = false;

                }
            }
        }
    }

    private void PauseGameUI()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame.PauseGame();
                isPaused = true;
            }
            else if (isPaused)
            {
                PauseGame.ResumeGame();
                isPaused = false;
            }
        }

    }

    public void IncreaseScore(int _score)
    {
        if (isScoreBoost)
        {
            score += (_score * 2);
        }
        else
        {
            score += _score;
        }
        Debug.Log("Score:" + score);
        ScoreText.text = "Score: " + score;
        AddSegments();
        Audio_Manager.instance.Play(SoundName.FoodGainer);
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
                ScoreText.text = "Score: " + score;
                Audio_Manager.instance.Play(SoundName.FoodBurner);
            }

        }
        else
        {
            score = 0;
        }

    }

    private void UpdateSegments()
    {
        /*for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }
        segments[0].position = transform.position;*/
        for (int i = segments.Count - 1; i > 0; i--)
        {
            // If the segment is not the head, then set its position to the position of the previous segment.
            if (i != 0)
            {
                segments[i].position = segments[i - 1].position;
            }
        }
    }

    private void AddSegments()
    {
       
        if (segments == null || segments.Count == 0)
        {
            Transform segment = Instantiate(segmentsPrefab, spwanPos.position, spwanPos.rotation);
            segments.Add(segment);
        }
        else
        {
            Transform segment = Instantiate(segmentsPrefab, segments[segments.Count - 1].position, segments[segments.Count - 1].rotation);
            segments.Add(segment);
        }

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
        else if (position.y > gridArea.bounds.max.y)
        {
            position.y = gridArea.bounds.min.y;
        }
    }

    public void ShieldCouroutine()
    {
        ShieldCo = StartCoroutine(Shield());
    }

    private IEnumerator Shield()
    {
        isShieldActive = true;
        Debug.Log("Shield Activated");
        ShieldText.color = Color.red;
        yield return new WaitForSeconds(5f);
        Debug.Log("Shield Deactivated");
        ShieldText.color = Color.gray;
        isShieldActive = false;
    }

    public void ScoreBoostCoroutine()
    {
        ScoreBoostCo = StartCoroutine(ScoreBoost());
    }

    private IEnumerator ScoreBoost()
    {
        isScoreBoost = true;
        DoubleScoreText.color = Color.red;
        Debug.Log("Double Score Activated");
        yield return new WaitForSeconds(8f);
        Debug.Log("Double Score Deactivated");
        DoubleScoreText.color = Color.grey;
        isScoreBoost = false;
    }

    public void SpeedUpCoroutine()
    {
        SpeedUpCo = StartCoroutine(SpeedUp());
    }

    private IEnumerator SpeedUp()
    {
        isSpeedUp = true;
        Debug.Log("Changing Speed");
        DoubleSpeedText.color = Color.red;
        yield return new WaitForSeconds(5f);
        Debug.Log("Original speed");
        DoubleSpeedText.color = Color.white;
        isSpeedUp = false;
    }

    private void OnDisable()
    {
        if (SpeedUpCo != null)
            StopCoroutine(SpeedUpCo);
        if (ScoreBoostCo != null)
            StopCoroutine(ScoreBoostCo);
        if (ShieldCo != null)
            StopCoroutine(ShieldCo);
    }

}
