using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public class EnemyAIDestroyer : MonoBehaviour
    {
        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}