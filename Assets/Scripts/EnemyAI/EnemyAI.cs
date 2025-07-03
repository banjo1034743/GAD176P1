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

        [Header("Data")]

        protected bool isIdleStateEnabled = false;
        protected bool isAttackStateEnabled = false;
        protected bool isFleeStateEnabled = false;

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
            if (playerSightedChecker.PlayerInSightCheck())
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

        public void FleeState()
        {
            DisableStates();
            isFleeStateEnabled = true;

            fleeState.CallFleeCycleCoroutine();
            StartRegeneratingHealth();
        }

        public bool GetFleeStateBool()
        {
            return isFleeStateEnabled;
        }

        public void StartRegeneratingHealth()
        {

        }

        public void DestroySelf()
        {

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