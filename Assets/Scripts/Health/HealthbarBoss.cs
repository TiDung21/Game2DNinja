using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarBoss : MonoBehaviour
{
    [SerializeField] private Health bossHealth;
    [SerializeField] private Image totalHealthbar;
    [SerializeField] private Image currentHealthbar;
    [SerializeField] private Transform boss;

    private float startingHealth;

    private void Start()
    {
        totalHealthbar.fillAmount = 1;
        startingHealth = bossHealth.curHealth;
    }
    private void Update()
    {
        currentHealthbar.fillAmount = bossHealth.curHealth / startingHealth;
        transform.position = new Vector3(boss.position.x, transform.position.y, transform.position.z);
    }
}
