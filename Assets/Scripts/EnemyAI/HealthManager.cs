using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public class HealthManager : MonoBehaviour
    {
        #region Variables

        [SerializeField] private float enemyAIHealth;

        private float enemyAIMaxHealth;

        [SerializeField] private float healthRemainingToEnableFleeState;

        #endregion

        #region Debug Variables

        [SerializeField] private bool debugMode = false;

        #endregion

        #region Methods

        public void DecrementHealth(float valueToChangeBy)
        {
            enemyAIHealth -= valueToChangeBy;
        }

        #endregion

        #region Unity Methods

        private void Start()
        {
            enemyAIMaxHealth = enemyAIHealth;
            Debug.Log("Enemy AI health is " + enemyAIHealth);
        }

        private void Update()
        {
            if (debugMode)
            {
                LowerHealthToEnableFleeState();
            }
        }

        #endregion

        #region Debug Methods

        private void LowerHealthToEnableFleeState()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (enemyAIHealth != healthRemainingToEnableFleeState)
                {
                    enemyAIHealth = healthRemainingToEnableFleeState;
                    Debug.Log("Enemy AI health is " + enemyAIHealth);
                }
            }
        }

        #endregion
    }
}