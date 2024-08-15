using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour 
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private UIManager uiManager;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        playerHealth = GameObject.FindObjectOfType<Health>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerHealth.TakeDamage(playerHealth.curHealth);
            uiManager.GameOver();
        }
    }
}
