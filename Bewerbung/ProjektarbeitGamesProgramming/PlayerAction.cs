using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAction : MonoBehaviour
{
    //Complete script from Bianca

    [SerializeField, Tooltip("PlayerStats in ScriptableObject file.")] private PlayerStats playerStats;

    public GameEvent HPChange;

    private void Start() 
    {
        playerStats.CanAttack = true;
        playerStats.isAttacking = false;
    }

    public void TakeDamage(float randoTakeDMG)
    {
        if (playerStats.DefenceBoostActive == true)
        {
            randoTakeDMG -= (randoTakeDMG * (playerStats.DefenceBoost / 100));
        }
        playerStats.RuntimeHP -= randoTakeDMG;
        HPChange.Raise();
        if (playerStats.RuntimeHP <= 0)
        {
            SceneManager.LoadScene("BG_GameOver");
        }
    }

    public void TakePotion(int potionNumber)
    {
        if (potions[potionNumber].PotionInCooldown) return;

        if (potions[potionNumber].HealFloat > 0)
        {
            playerStats.RuntimeHP += potions[potionNumber].HealFloat;
            if (playerStats.RuntimeHP > 100)
            {
                playerStats.RuntimeHP = 100;
            }
        }
        if (potions[potionNumber].StaminaFloat > 0)
        {
            playerStats.RuntimeStamina += potions[potionNumber].StaminaFloat;
            if (playerStats.RuntimeStamina > 100)
            {
                playerStats.RuntimeStamina = 100;
            }
        }
        if (potions[potionNumber].DamageBoostPercent > 0)
        {
            if (playerStats.DamageBoostActive == true)
            {
                StopCoroutine(PotionEffectDamage(0));
            }
            playerStats.DamageBoost = potions[potionNumber].DamageBoostPercent;
            playerStats.DamageBoostActive = true;
            StartCoroutine(PotionEffectDamage(potions[potionNumber].DurationInSeconds));
        }
        if (potions[potionNumber].DefenceBoostPercent > 0)
        {
            if (playerStats.DefenceBoostActive == true)
            {
                StopCoroutine(PotionEffectDamage(0));
            }
            playerStats.DefenceBoost = potions[potionNumber].DefenceBoostPercent;
            playerStats.DefenceBoostActive = true;
            StartCoroutine(PotionEffectDefence(potions[potionNumber].DurationInSeconds));
        }
        StartCoroutine(CooldownPotion(potions[potionNumber].CooldownInSeconds, potions[potionNumber]));
    }
    IEnumerator PotionEffectDamage(float duration)
    {
        yield return new WaitForSeconds(duration);
        playerStats.DamageBoostActive = false;
        yield break;
    }
    IEnumerator PotionEffectDefence(float duration)
    {
        yield return new WaitForSeconds(duration);
        playerStats.DefenceBoostActive = false;
        yield break;
    }
    IEnumerator CooldownPotion(float cooldown, PotionPreset potion)
    {
        potion.PotionInCooldown = true;
        yield return new WaitForSeconds(cooldown);
        potion.PotionInCooldown = false;
        yield break;
    }
}

