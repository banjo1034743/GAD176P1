using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public abstract class AttackStateManager : MonoBehaviour
    {
        [Header("Data")]

        [Tooltip("The ammount of damage dealt to the player")]
        [SerializeField] protected float attackDamage;

        [SerializeField] protected float distanceToAttackFrom;

        [SerializeField] protected float attackCooldown;

        public abstract void Attack();
    }
}