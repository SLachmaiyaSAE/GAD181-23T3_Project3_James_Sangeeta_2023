using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhaseUIP2 : MonoBehaviour
{
    [SerializeField] private Image imageCooldown;
    [SerializeField] private TMP_Text textCooldown;

    //variables for cooldown
    private bool isCooldown = false;
    private float cooldownTime = 5f;
    private float cooldownTimer = 0f;

    public PhaseUIManager phaseUIManager;
    void Start()
    {
        textCooldown.gameObject.SetActive(false);
        imageCooldown.fillAmount = 0.0f;

    }


    void Update()
    {
        if (isCooldown)
        {
            ApplyCooldown();
        }
    }

    void ApplyCooldown()
    {
        //subtract time since last called
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer < 0.0f)
        {
            isCooldown = false;
            textCooldown.gameObject.SetActive(false);
            imageCooldown.fillAmount = 0.0f;
        }
        else
        {
            textCooldown.text = Mathf.RoundToInt(cooldownTimer).ToString();
            imageCooldown.fillAmount = cooldownTimer / cooldownTime;
        }
    }

    public void UsePhaseP2()
    {
        phaseUIManager.UsePhase(2);
    }
}
