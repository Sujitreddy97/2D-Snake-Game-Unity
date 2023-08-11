using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food_Controller : MonoBehaviour
{
    [SerializeField] private Food_Type foodType;
    private Coroutine changePositionCoroutine;
    private void Start()
    {
        changePositionCoroutine = StartCoroutine(ChangePosition());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player_Controller playerController = collision.gameObject.GetComponent<Player_Controller>();
        if (playerController != null)
        {

            switch (foodType)
            {
                case Food_Type.MassGainer:
                    Debug.Log("Mass Gainer");
                    playerController.IncreaseScore(5);
                    break;
                case Food_Type.MassBurner:
                    Debug.Log("Mass Burner");
                    playerController.DecreaseScore(2);
                    break;
            }

        }

        StopCoroutine(changePositionCoroutine);
        ChangeFoodPosition();
    }

    IEnumerator ChangePosition()
    {
        yield return new WaitForSeconds(10f);
        ChangeFoodPosition();
        
    }

    void ChangeFoodPosition()
    {
        gameObject.transform.position = Food_Spwan.instance.RandomPosition();
        changePositionCoroutine = StartCoroutine(ChangePosition());
    }

}


