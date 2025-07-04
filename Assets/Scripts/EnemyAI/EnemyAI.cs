using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public class EnemyAI : MonoBehaviour, IIdleFunctionality, IAttackFunctionality, IFleeFunctionality, IHealthFunctionality, IKillableFunctionality, IPlayerCheckerFunctionality
    {
        [Header("Scripts")]

        [SerializeField] protected IdleStateManager idleState;
        [SerializeField] protected PlayerSightedChecker playerSightedChecker;
        [SerializeField] protected FleeStateManager fleeState;
        [SerializeField] protected HealthManager healthManager;
        [SerializeField] protected HealthRegenerator healthRegenerator;
        [SerializeField] protected EnemyAIDestroyer enemyAIDestroyer;

        [Header("Data")]

        // Remove serialization from these bools after resolving bug
        [SerializeField] protected bool isIdleStateEnabled = false;
        [SerializeField] protected bool isAttackStateEnabled = false;
        [SerializeField] protected bool isFleeStateEnabled = false;

        public void IdleState()
        {
            DisableStates();
            isIdleStateEnabled = true;

            idleState.CallWalkCycleCoroutine();
        }

        // Called by IdleStateManager to check the value to know when to end the walk cycle loop
        public bool GetIdleStateBool()
        {
            return isIdleStateEnabled;
        }

        public void CallPlayerInSightCheck()
        {
            if (playerSightedChecker.PlayerInSightCheck() && !isFleeStateEnabled)
            {
                //Debug.Log("Raycast is true!");
                DisableStates();
                isAttackStateEnabled = true;
            }
        }

        /// <summary>
        /// This is overriden, with content added to it, in the children of this class
        /// </summary>
        public virtual void AttackState()
        {

        }

        public virtual void FleeState()
        {
            DisableStates();
            isFleeStateEnabled = true;

            fleeState.CallFleeCycleCoroutine();
        }

        public bool GetFleeStateBool()
        {
            return isFleeStateEnabled;
        }

        public void StartRegeneratingHealth()
        {
            healthRegenerator.CallRegenerateHealthCoroutine();
        }

        public void CallDestroySelf()
        {
            enemyAIDestroyer.DestroySelf();
        }

        public void EnableFleeState()
        {
            isFleeStateEnabled = true;
        }

        protected void DisableStates()
        {
            isIdleStateEnabled = false;
            isAttackStateEnabled = false;
            isFleeStateEnabled = false;
        }
    }
}