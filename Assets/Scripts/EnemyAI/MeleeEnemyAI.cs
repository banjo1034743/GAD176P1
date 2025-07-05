using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public class MeleeEnemyAI : EnemyAI, IIdleFunctionality, IAttackFunctionality, IFleeFunctionality, IHealthFunctionality, IKillableFunctionality, IPlayerCheckerFunctionality
    {
        #region Variables

        [Header("Scripts")]

        [SerializeField] private AttackStateMeleeManager attackStateMeleeManager;

        #endregion

        #region Methods
        public override void AttackState()
        {
            switch (playerApproacher.DistanceFromPlayerCheck())
            {
                case false:
                    attackStateMeleeManager.ClearCoroutine();
                    playerApproacher.MoveTowardPlayer();
                    break;
                case true:
                    Debug.Log("Take that! I, " + transform.name + ", am attacking you!");
                    attackStateMeleeManager.Attack();
                    break;
            }

        }

        public override void FleeState()
        {
            base.FleeState();
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
            if (healthManager.GetHealth() == healthManager.GetMaxHealth() && !isIdleStateEnabled && !isAttackStateEnabled && !isFleeStateEnabled)
            {
                Debug.Log("Called IdleState from Update in MeleeEnemyAI");
                IdleState();
            }

            if (isAttackStateEnabled)
            {
                AttackState();
            }
            else if (!isAttackStateEnabled && healthManager.GetHealth() <= healthManager.GetHealthRemainingToEnableFleeState() && healthManager.GetHealth() > 0)
            {
                StartRegeneratingHealth();
            }

            if (healthManager.GetHealth() < healthManager.GetMaxHealth() && healthManager.GetHealth() > 0)
            {
                Debug.Log("Called FleeState from Update in MeleeEnemyAI");
                FleeState();
            }
            else if (healthManager.GetHealth() == healthManager.GetMaxHealth())
            {
                isFleeStateEnabled = false;
            }
            else if (healthManager.GetHealth() == 0)
            {
                CallDestroySelf();
            }
        }

        #endregion
    }
}