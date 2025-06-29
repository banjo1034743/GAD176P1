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

        [SerializeField] private AttackStateMeleeManager attackStateMeleeManager;

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
                //Debug.Log("Take that! I, " + transform.name + ", am attacking you!");
                attackStateMeleeManager.Attack();
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