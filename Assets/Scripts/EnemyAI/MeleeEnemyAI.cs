using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public class MeleeEnemyAI : EnemyAI, IIdleFunctionality, IAttackFunctionality, IFleeFunctionality, IHealthFunctionality, IKillableFunctionality, IPlayerCheckerFunctionality
    {
        [Header("Scripts")]

        [SerializeField] private PlayerApproacher playerApproacher; 

        public override void AttackState()
        {
            // Call Player Approaching script here
            playerApproacher.MoveTowardPlayer();

            // When condition reached, stop moving toward player and begin attacking
            
        }

        #region Unity Functions

        private void Start()
        {
            IdleState();
        }

        private void FixedUpdate()
        {
            CallPlayerInSightCheck();
        }

        private void Update()
        {
            if (isAttackStateEnabled)
            {
                AttackState();
            }
        }

        #endregion
    }
}