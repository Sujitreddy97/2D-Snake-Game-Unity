using UnityEngine;
using System.Collections.Generic;

public class Food_Spwan : MonoBehaviour
{
    [SerializeField] private GameObject massGainer;
    [SerializeField] private GameObject massBurner;
    [SerializeField] private BoxCollider2D GridArea;
    List<Food_Controller> foods;

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
        Vector3 massGainerPosition = RandomPosition();
        Vector3 massBurnerPosition = RandomPosition();

        while(massGainerPosition == massBurnerPosition)
        {
            massBurnerPosition = RandomPosition();
        }
        Instantiate(massGainer, massGainerPosition, transform.rotation);
        Instantiate(massBurner, massBurnerPosition, transform.rotation);
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
