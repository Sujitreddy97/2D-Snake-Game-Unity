using UnityEngine;

public class Powerup_Controller : MonoBehaviour
{
    [SerializeField] private Collectable_Type powerUpType;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player_Controller player_Controller = collision.gameObject.GetComponent<Player_Controller>();

        if (player_Controller != null)
        {
            switch (powerUpType)
            {
                case Collectable_Type.Shield:
                    Debug.Log("Shield Activated");
                    player_Controller.ShieldCouroutine();
                    break;

                case Collectable_Type.ScoreBoost:
                    Debug.Log("Score Boost");
                    player_Controller.ScoreBoostCoroutine();
                    break;

                case Collectable_Type.SpeedUp:
                    Debug.Log("Speed Up");
                    player_Controller.SpeedUpCoroutine();
                    break;
            }
        }

        CancelInvoke(nameof(Powerup_Spwan.instance.Spawn));
        Destroy(gameObject);
    }
}




