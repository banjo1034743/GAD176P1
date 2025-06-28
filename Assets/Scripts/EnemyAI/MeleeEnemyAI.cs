using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public class MeleeEnemyAI : EnemyAI, IIdleFunctionality, IAttackFunctionality, IFleeFunctionality, IHealthFunctionality, IKillableFunctionality, IPlayerCheckerFunctionality
    {
        #region Variables

        [Header("Scripts")]

        [SerializeField] private PlayerApproacher playerApproacher;

        #endregion

        #region Methods
        public override void AttackState()
        {
            if (!playerApproacher.DistanceFromPlayerCheck())
            {
                playerApproacher.MoveTowardPlayer();
            }
            else
            {
                // When condition reached, stop moving toward player and begin attacking
                Debug.Log("Take that! I, " + transform.name + ", am attacking you!");
            }

        }
        #endregion

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