using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public abstract class AttackStateManager : MonoBehaviour
    {
        #region Variables

        [Header("Data")]

        [Tooltip("The ammount of damage dealt to the player")]
        [SerializeField] protected float attackDamage;

        [SerializeField] protected float distanceToAttackFrom;

        [SerializeField] protected float attackCooldown;

        protected Coroutine attackCoroutine = null;

        [Header("Scripts")]

        [SerializeField] protected DistanceFromPlayerChecker distanceFromPlayerChecker;

        [SerializeField] protected PlayerApproacher playerApproacher;

        #endregion

        #region Methods

        public abstract void Attack();

        protected void EndAttackCoroutine(Coroutine coroutineToEnd)
        {
            StopCoroutine(coroutineToEnd);
            coroutineToEnd = null;
        }

        #endregion
    }
}