using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public abstract class AttackStateManager : MonoBehaviour
    {
        [SerializeField] private float attackDamage;

        [SerializeField] private float distanceToAttackFrom;

        [SerializeField] private float attackCooldown;

        public abstract void Attack();
    }
}