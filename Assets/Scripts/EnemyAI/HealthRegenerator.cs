using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public class HealthRegenerator : MonoBehaviour
    {
        #region Variables

        [Header("Data")]

        [SerializeField] private float healthToRegenerateBy = 5f;

        [SerializeField] private float waitBetweenHealthIncrement = 2.5f;

        //[SerializeField] private float healthToStopRegeneratingAt; // Take away SerializeField after confirming functionality

        private Coroutine healthRegeneratorCoroutine = null;

        [Header("Scripts")]

        [SerializeField] private HealthManager healthManager;

        #endregion

        #region Methods

        public void CallRegenerateHealthCoroutine()
        {
            if (healthRegeneratorCoroutine == null)
            {
                healthRegeneratorCoroutine = StartCoroutine(RegenerateHealth());
            }
        }

        public float GetHealthToRegenerateBy()
        {
            return healthToRegenerateBy;
        }

        private IEnumerator RegenerateHealth()
        {
            while (healthManager.GetHealth() < healthManager.GetMaxHealth())
            {
                healthManager.IncrementHealth(healthToRegenerateBy);
                Debug.Log("The health of the AI is now: " + healthManager.GetHealth());

                // Ensure we don't go over the maximum health of the AI in the case the amount of health needed to get back to max is less than 5
                if (healthManager.GetHealth() > healthManager.GetMaxHealth())
                {
                    healthManager.SetHealth(healthManager.GetMaxHealth());
                    Debug.Log("The health of the AI is now: " + healthManager.GetHealth());
                }

                yield return new WaitForSeconds(waitBetweenHealthIncrement);
            }

            healthRegeneratorCoroutine = null;
        }

        #endregion
    }
}