using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] private Image imgTotalEnegy;
    [SerializeField] private Image imgCurrentEnergy;

    [SerializeField] private float curEnergy;
    [SerializeField] private float speedRecoverSkill;

    private float startEnergy = 50f;
    private float maxEnergy = 100f;

    public bool isUsingSkill;

    private void Awake()
    {
        isUsingSkill = false;

        imgCurrentEnergy.fillAmount = startEnergy / maxEnergy;
        curEnergy = startEnergy;
    }
    private void Update()
    {
        curEnergy = Mathf.Clamp(curEnergy,0, maxEnergy);
        curEnergy += speedRecoverSkill * Time.deltaTime;
        imgCurrentEnergy.fillAmount = curEnergy / maxEnergy;
    }

    public void UseSkill()
    {
        isUsingSkill = true;
        StartCoroutine(ResetTimeRecoverySkill());
    }
    private IEnumerator ResetTimeRecoverySkill()
    {
        curEnergy = 0f;
        yield return new WaitForSeconds(3f);
        isUsingSkill=false;
    }
}
