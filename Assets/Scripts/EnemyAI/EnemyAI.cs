using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public abstract class EnemyAI : MonoBehaviour, IIdleFunctionality, IAttackFunctionality, IFleeFunctionality, IHealthFunctionality, IKillableFunctionality
    {
        [SerializeField] protected IdleStateManager idleState;
        [SerializeField] protected PlayerSightedChecker playerSightedChecker;
        [SerializeField] protected FleeStateManager fleeState;
        [SerializeField] protected HealthManager healthManager;

        protected bool isIdleStateEnabled = false;
        
        public void IdleState()
        {
            DisableStates();
            isIdleStateEnabled = true;

            StartCoroutine(idleState.BeginWalkCycle(isIdleStateEnabled));
        }

        public abstract void AttackState();

        public void FleeState()
        {

        }

        public void CallSetHealth()
        {

        }

        public void DestroySelf()
        {

        }

        protected void DisableStates()
        {
            isIdleStateEnabled = false;
        }
    }
}