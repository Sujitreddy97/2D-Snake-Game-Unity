using UnityEngine;

public class Food_Spwan : MonoBehaviour
{
    [SerializeField] private GameObject massGainer;
    [SerializeField] private GameObject massBurner;
    [SerializeField] private BoxCollider2D GridArea;
    [SerializeField] private float spawnInterwal;
    [SerializeField] private float foodLifeTime;

    public static Food_Spwan instance;

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
        SpawnFood();
    }

    private void SpawnFood()
    {
        Instantiate(massGainer, RandomPosition(), transform.rotation);
        Instantiate(massBurner, RandomPosition(), transform.rotation);
    }

    public Vector3 RandomPosition()
    {
        Bounds bound = GridArea.bounds;
        float x = Random.Range(bound.min.x, bound.max.x);
        float y = Random.Range(bound.min.y, bound.max.y);

        Vector3 position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0);
        return position;
    }

}
