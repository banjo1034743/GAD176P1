using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public abstract class EnemyAI : MonoBehaviour, IIdleFunctionality, IAttackFunctionality, IFleeFunctionality, IHealthFunctionality, IKillableFunctionality, IPlayerCheckerFunctionality
    {
        [Header("Scripts")]

        [SerializeField] protected IdleStateManager idleStateManager;
        [SerializeField] protected PlayerSightedChecker playerSightedChecker;
        [SerializeField] protected FleeStateManager fleeStateManager;
        [SerializeField] protected HealthManager healthManager;
        [SerializeField] protected HealthRegenerator healthRegenerator;
        [SerializeField] protected EnemyAIDestroyer enemyAIDestroyer;
        [SerializeField] protected PlayerApproacher playerApproacher;

        [Header("Data")]

        // Remove serialization from these bools after resolving bug
        [SerializeField] protected bool isIdleStateEnabled = false;
        [SerializeField] protected bool isAttackStateEnabled = false;
        [SerializeField] protected bool isFleeStateEnabled = false;

        public void IdleState()
        {
            DisableStates();
            isIdleStateEnabled = true;

            idleStateManager.CallWalkCycleCoroutine();
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
        /// This is overriden, with content added to it, in the children of this class. This must be overriden by any children inheriting from Enemy AI
        /// </summary>
        public abstract void AttackState();

        /// <summary>
        /// We want to keep a base functionality which will be most generally used, but we also want to override it for the uniqu conditions for fleeing by the RangedEnemyAI class
        /// </summary>
        public virtual void FleeState()
        {
            DisableStates();
            isFleeStateEnabled = true;

            fleeStateManager.CallFleeCycleCoroutine();
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

        protected void DisableStates()
        {
            isIdleStateEnabled = false;
            isAttackStateEnabled = false;
            isFleeStateEnabled = false;
        }
    }
}