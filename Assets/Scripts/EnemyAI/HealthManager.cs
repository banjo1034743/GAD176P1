using SAE.GAD176.P1.EnemyAI.PlayerFunctionality;
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

        [SerializeField] private float enemyAIMaxHealth = 0f; // Unserialize when finished resolving issue

        [SerializeField] private float healthRemainingToEnableFleeState;

        [Header("Scripts")]

        [SerializeField] private EnemyAI enemyAI;

        [SerializeField] private HealthRegenerator healthRegenerator;

        [SerializeField] private EnemyMeleeAnimationManager enemyMeleeAnimationManager;

        #endregion

        #region Debug Variables

        [SerializeField] private bool debugMode = false;

        #endregion

        #region Methods

        public void IncrementHealth(float valueToChangeBy)
        {
            enemyAIHealth += valueToChangeBy;
        }

        public void DecrementHealth(float valueToChangeBy)
        {
            enemyAIHealth -= valueToChangeBy;
        }

        public void SetHealth(float valueToChangeBy)
        {
            enemyAIHealth = valueToChangeBy;
        }

        public float GetHealth()
        {
            return enemyAIHealth;
        }

        public float GetMaxHealth()
        {
            return enemyAIMaxHealth;
        }

        public float GetHealthRemainingToEnableFleeState()
        {
            return healthRemainingToEnableFleeState;
        }

        private void CheckIfHealthRemainEnablesFleeState()
        {
            if (enemyAIHealth <= healthRemainingToEnableFleeState && enemyAIHealth > 0 && !enemyAI.GetFleeStateBool())
            {
                Debug.Log("Called FleeState from CheckIfHealthRemainEnablesFleestate in Health Manager");
                enemyMeleeAnimationManager.StopAnimation();
                enemyAI.FleeState();
            }
        } 

        #endregion

        #region Unity Methods

        /// <summary>
        /// We call this code in Awake() to ensure that we set enemyAIMaxHealth to enemyHealth before HealthRegenerator calles GetMaxHealth()
        /// (Remove if keeping as Start ends up working fine with the check added in HealthRegenerator)
        /// </summary>
        private void Start()
        {
            enemyAIMaxHealth = enemyAIHealth;

            Debug.Log("The health of the AI is now: " + enemyAIHealth);
        }

        private void Update()
        {
            CheckIfHealthRemainEnablesFleeState();

            if (debugMode)
            {
                LowerHealthToEnableFleeState();
                LowerHealthToZero();
            }
        }

        #endregion

        #region Debug Methods

        /// <summary>
        /// This method brings the health immeditly down to 5. This was used for testing while implementing the functionality of the Flee State
        /// </summary>
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

        private void LowerHealthToZero()
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                enemyAIHealth = 0;
            }
        }

        #endregion
    }
}