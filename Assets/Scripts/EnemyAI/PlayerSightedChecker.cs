using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public class PlayerSightedChecker : MonoBehaviour
    {
        [Header("Data")]

        [Tooltip("This is the distance of how far the ray is drawn infront of the AI. It is effectively their view distance.")]
        [SerializeField] private float lineOfViewDistance;

        [Tooltip("The layers which are picked up in collisions with the ray. Effectively what's visible to the AI.")]
        [SerializeField] private LayerMask layerMask;

        public bool PlayerInSightCheck()
        {
            Debug.DrawRay(transform.position, transform.forward);

            return Physics.Raycast(transform.position, transform.forward, lineOfViewDistance, layerMask);
        }
    }
}