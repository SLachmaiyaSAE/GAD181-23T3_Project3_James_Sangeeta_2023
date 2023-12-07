using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhaseUIManager : MonoBehaviour
{
    public static PhaseUIManager Instance;

    [SerializeField] private Image imageCooldownPlayer1;
    [SerializeField] private TMP_Text textCooldownPlayer1;
    [SerializeField] private Image imageCooldownPlayer2;
    [SerializeField] private TMP_Text textCooldownPlayer2;

    // variables for cooldown
    private bool isCooldownPlayer1;
    private bool isCooldownPlayer2;
    private float cooldownTime = 5f;
    private float cooldownTimerPlayer1;
    private float cooldownTimerPlayer2;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        textCooldownPlayer1.gameObject.SetActive(false);
        imageCooldownPlayer1.fillAmount = 0.0f;

        textCooldownPlayer2.gameObject.SetActive(false);
        imageCooldownPlayer2.fillAmount = 0.0f;
    }

    void Update()
    {
        if (isCooldownPlayer1)
        {
            ApplyCooldown(ref isCooldownPlayer1, ref cooldownTimerPlayer1, textCooldownPlayer1, imageCooldownPlayer1);
        }

        if (isCooldownPlayer2)
        {
            ApplyCooldown(ref isCooldownPlayer2, ref cooldownTimerPlayer2, textCooldownPlayer2, imageCooldownPlayer2);
        }
    }

    void ApplyCooldown(ref bool isCooldown, ref float cooldownTimer, TMP_Text textCooldown, Image imageCooldown)
    {
        // subtract time since last called
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

    public void UsePhase(int playerNumber)
    {
        if (playerNumber == 1)
        {
            UsePhaseAbility(ref isCooldownPlayer1, ref cooldownTimerPlayer1, textCooldownPlayer1, imageCooldownPlayer1);
        }
        else if (playerNumber == 2)
        {
            UsePhaseAbility(ref isCooldownPlayer2, ref cooldownTimerPlayer2, textCooldownPlayer2, imageCooldownPlayer2);
        }
        else
        {
            Debug.LogError("Invalid player number: " + playerNumber);
        }
    }

    void UsePhaseAbility(ref bool isCooldown, ref float cooldownTimer, TMP_Text textCooldown, Image imageCooldown)
    {
        if (isCooldown) // not available
        {
            // player has used the phase
        }
        else
        {
            isCooldown = true;
            textCooldown.gameObject.SetActive(true);
            cooldownTimer = cooldownTime;

            Debug.Log("Player used the phase ability.");
        }
    }
}