using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public class RangedEnemyAI : EnemyAI, IIdleFunctionality, IAttackFunctionality, IFleeFunctionality, IHealthFunctionality, IKillableFunctionality, IPlayerCheckerFunctionality
    {
        #region Variables

        [Header("Scripts")]

        [SerializeField] private AttackStateRangedManager attackStateRangedManager;

        #endregion

        #region Methods

        public override void AttackState()
        {
            
        }

        public override void FleeState()
        {
            DisableStates();
            isFleeStateEnabled = true;

            fleeStateManager.CallFleeCycleCoroutine();
        }

        #endregion

        #region Unity Methods

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

            if (isAttackStateEnabled && Vector3.Distance(transform.position, playerApproacher.GetPlayerTransform().position) < attackStateRangedManager.GetDistanceToAttackFrom())
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