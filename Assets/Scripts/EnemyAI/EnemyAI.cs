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
        [SerializeField] protected bool isAttackStateEnabled = false;
        
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

        }

        public void StartRegeneratingHealth()
        {

        }

        public void DestroySelf()
        {

        }

        protected void DisableStates()
        {
            isIdleStateEnabled = false;
            isAttackStateEnabled = false;
        }
    }
}