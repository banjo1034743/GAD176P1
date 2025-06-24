using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public abstract class EnemyAI : MonoBehaviour, IIdleFunctionality, IAttackFunctionality, IFleeFunctionality, IHealthFunctionality, IKillableFunctionality
    {
        [SerializeField] private IdleStateManager idleState;
        [SerializeField] private PlayerSightedChecker playerSightedChecker;
        [SerializeField] private FleeStateManager fleeState;
        [SerializeField] private HealthManager healthManager;
        
        public void IdleState()
        {
            idleState.BeginWalkCycle();
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
    }
}