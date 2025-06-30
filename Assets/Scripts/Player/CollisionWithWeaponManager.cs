using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI.PlayerFunctionality
{
    public class CollisionWithWeaponManager : MonoBehaviour
    {
        [SerializeField] private HealthManager healthManager;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Weapon"))
            {
                Debug.Log("I've been hit!");

                healthManager.DecreaseHealth();
            }
        }
    }
}