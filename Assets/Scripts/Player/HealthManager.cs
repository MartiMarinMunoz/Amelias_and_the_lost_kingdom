using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public enum characterType { Player, Enemy01, Enemy02 }

    [Header("Health Settings")]
    public characterType currentCharacterType;
    public bool friendlyFire = false;

    [Header("Stats")]
    public int maxHealth;
    int currentHealth;

    //UIController _UIController;

    //public List<Image> life;


    void Start()
    {
        currentHealth = maxHealth;

        //_UIController = GameObject.FindGameObjectWithTag("UI").GetComponent<UIController>();
    }

    public void TakeDamage(int damage, string _tag)
    {
        if (gameObject.tag != _tag && !friendlyFire)
            return;

        currentHealth -= damage;



        if (currentHealth <= 0)
        {
            currentHealth = 0;

            if (currentCharacterType == characterType.Player)
            {
                var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

                if (player != null)
                {
                    player.ResetGame();
                }

                return;
            }
            else if (currentCharacterType == characterType.Enemy01)
            {
                //_UIController.SetScore(100);
                Destroy(gameObject);
            }
            else if (currentCharacterType == characterType.Enemy02)
            {
                //_UIController.SetScore(500);
                Destroy(gameObject);
            }
        }
        try
        {
            //life[currentHealth].gameObject.SetActive(false);
        }
        catch
        {

        }

    }
}
