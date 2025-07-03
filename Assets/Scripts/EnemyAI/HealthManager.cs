using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public class HealthManager : MonoBehaviour
    {
        #region Variables

        [Header("Data")]

        [SerializeField] private float enemyAIHealth;

        private float enemyAIMaxHealth;

        [SerializeField] private float healthRemainingToEnableFleeState;

        [Header("Scripts")]

        [SerializeField] private EnemyAI enemyAI;

        #endregion

        #region Debug Variables

        [SerializeField] private bool debugMode = false;

        #endregion

        #region Methods

        public void DecrementHealth(float valueToChangeBy)
        {
            enemyAIHealth -= valueToChangeBy;
        }

        private void CheckIfHealthRemainEnablesFleeState()
        {
            if (enemyAIHealth <= healthRemainingToEnableFleeState)
            {
                enemyAI.FleeState();
            }
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
            CheckIfHealthRemainEnablesFleeState();

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