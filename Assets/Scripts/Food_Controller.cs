using UnityEngine;

public class Food_Controller : MonoBehaviour
{
    [SerializeField] private Collectable_Type foodType;
    private Coroutine changePositionCoroutine;
    

    private void Start()
    {
        InvokeRepeating(nameof(ChangeFoodPosition), 10f, 10f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player_Controller playerController = collision.gameObject.GetComponent<Player_Controller>();
        if (playerController != null)
        {

            switch (foodType)
            {
                case Collectable_Type.MassGainer:
                    Debug.Log("Mass Gainer");
                    playerController.IncreaseScore(5);
                    break;
                case Collectable_Type.MassBurner:
                    Debug.Log("Mass Burner");
                    playerController.DecreaseScore(2);
                    break;
            }

        }

        this.CancelInvoke();
        ChangeFoodPosition();
    }


    void ChangeFoodPosition()
    {
        gameObject.transform.position = Food_Spwan.instance.RandomPosition();
        InvokeRepeating(nameof(ChangeFoodPosition), 10f, 10f);
    }

}


