using UnityEngine;

public class Segments_Controller : MonoBehaviour
{

    [SerializeField] private Transform spawnPos;

    public Transform SpwanPosition()
    {
        return spawnPos;
    }
}
