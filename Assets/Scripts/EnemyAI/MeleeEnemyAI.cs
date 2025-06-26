using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public class MeleeEnemyAI : EnemyAI, IIdleFunctionality, IAttackFunctionality, IFleeFunctionality, IHealthFunctionality, IKillableFunctionality, IPlayerCheckerFunctionality
    {
        public override void AttackState()
        {
            
        }

        #region Unity Functions

        private void Start()
        {
            IdleState();
        }

        private void Update()
        {
            CallPlayerInSightCheck();
        }

        #endregion
    }
}