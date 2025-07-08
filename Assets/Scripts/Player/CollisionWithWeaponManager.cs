using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI.PlayerFunctionality
{
    public class CollisionWithWeaponManager : MonoBehaviour
    {
        #region Variables

        [Header("Tags")]

        [SerializeField] private string pillowMeleeTag;

        [SerializeField] private string pillowRangedTag;

        [Header("Scripts")]

        [SerializeField] private HealthManager healthManager;

        #endregion

        #region Unity Methods

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(pillowMeleeTag))
            {
                Debug.Log("I've been hit with a melee pillow attack!");

                healthManager.DecreaseHealth();
            }
            else if (other.gameObject.CompareTag(pillowRangedTag))
            {
                Debug.Log("I've been hit with a ranged pillow projectile!");

                healthManager.DecreaseHealth();
                Destroy(other.gameObject);
            }
        }

        private void Start()
        {
            if (string.IsNullOrEmpty(pillowMeleeTag) || string.IsNullOrEmpty(pillowRangedTag)) // Using IsNullOrEmpty, we check both if the reference is valid, and if there is no content
            {
                Debug.LogError("String variables for pillow tag names are null. Set the value to the coressponding tag!");
            }
        }

        #endregion
    }
}