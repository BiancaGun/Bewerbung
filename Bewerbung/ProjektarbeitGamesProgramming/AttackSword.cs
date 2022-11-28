using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSword : MonoBehaviour
{
    //Complete script from Bianca

    [SerializeField, Tooltip("PlayerStats in ScriptableObject file.")] private PlayerStats playerStats;
    [SerializeField, Tooltip("WeaponrStats in ScriptableObject file.")] private WeaponStats weaponStats;

    private float randoTakeDMG, playerDamageBoost;

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "Enemy" && playerStats.isAttacking == true)
        {
            randoTakeDMG = Random.Range(weaponStats.WeaponMinDMG, weaponStats.WeaponMaxDMG);
            other.gameObject.GetComponent<EnemyDamage>().TakeDamage(randoTakeDMG, playerDamageBoost);
        }

        if (other.gameObject.tag == "Destroyable" && playerStats.isAttacking == true) 
        {
            Destroy(other.gameObject);
        }
    }
}
