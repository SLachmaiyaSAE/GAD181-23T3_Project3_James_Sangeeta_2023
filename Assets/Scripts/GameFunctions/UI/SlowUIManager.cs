using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlowUIManager : MonoBehaviour
{
    public static SlowUIManager Instance;

    [SerializeField] private Image slowImageP1;
    [SerializeField] private TMP_Text slowTextP1;
    [SerializeField] private Image slowImageP2;
    [SerializeField] private TMP_Text slowTextP2;

    private void Awake()
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

    public void UseSlow(int playerNumber)
    {
        if (playerNumber == 1)
        {
            StartCoroutine(StartCooldown(slowImageP1, slowTextP1));
        }
        else if (playerNumber == 2)
        {
            StartCoroutine(StartCooldown(slowImageP2, slowTextP2));
        }
    }

    private IEnumerator StartCooldown(Image imageCooldown, TMP_Text textCooldown)
    {
        bool isCooldown = true;
        float cooldownTime = 5f;
        float cooldownTimer = cooldownTime;

        textCooldown.gameObject.SetActive(true);
        imageCooldown.fillAmount = 1.0f;

        while (isCooldown)
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0.0f)
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

            yield return null;
        }
    }
}
