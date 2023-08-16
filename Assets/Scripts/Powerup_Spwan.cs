using UnityEngine;

public class Powerup_Spwan : MonoBehaviour
{
    [SerializeField] private GameObject[] PowerUps;
    [SerializeField] private BoxCollider2D GridArea;
    public static Powerup_Spwan instance;
    private GameObject currentPowerUp;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 5f, 5f);
    }

    public void Spawn()
    {
        int index = Random.Range(0, PowerUps.Length);
        currentPowerUp = Instantiate(PowerUps[index], RandomPosition(), transform.rotation);
        Destroy(currentPowerUp, 5f);
    }

    private Vector3 RandomPosition()
    {
        Bounds bounds = GridArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        Vector3 position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0);
        return position;
    }

}
