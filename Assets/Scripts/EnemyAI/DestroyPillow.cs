using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public class DestroyPillow : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Collided with: " + other.name);

            if (other.gameObject.CompareTag("PillowRanged"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}