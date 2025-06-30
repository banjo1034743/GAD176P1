using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI.PlayerFunctionality
{
    public class HealthManager : MonoBehaviour
    {
        #region Variables

        [Header("Data")]

        [SerializeField] private float playerHealth;

        [SerializeField] private float healthToIncrementBy;

        [SerializeField] private float healthToDecrementBy;

        #endregion

        #region Methods
        
        public void IncreaseHealth()
        {
            playerHealth += healthToIncrementBy;
            Debug.Log("Player Health is now " +  playerHealth);
        }

        public void DecreaseHealth()
        {
            if (playerHealth > 0)
            {
                playerHealth -= healthToDecrementBy;
                Debug.Log("Player Health is now " + playerHealth);
            }
            else
            {
                Debug.Log("Player has died!");
            }
        }

        #endregion
    }
}