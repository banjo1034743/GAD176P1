using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
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

        [Header("Scripts")]

        [SerializeField] private PlayerApproacher playerApproacher;

        /// <summary>
        /// This method is used to check if the Player is in view, to both end the IdleState and being the Attack State
        /// </summary>
        /// <returns></returns>
        public bool PlayerInSightCheck()
        {
            // Draws line visible when running game to see ray's direction
            Debug.DrawRay(transform.position, transform.forward * lineOfViewDistance);

            // Declare for storing reference
            RaycastHit hit;

            // If ray from our position reaching forward as hit something in the specified distance, which is on the Player layer, run steps below:
            if (Physics.Raycast(transform.position, transform.forward, out hit, lineOfViewDistance, layerMask))
            {
                Transform playerTransform;

                if (hit.transform != null)
                {
                    //Debug.Log(hit.transform);

                    playerTransform = hit.transform;

                    if (playerApproacher.GetPlayerTransform() == null)
                    {
                        playerApproacher.SetPlayerTransformReference(playerTransform);
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}