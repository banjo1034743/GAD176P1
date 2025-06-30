using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public class PlayerApproacher : MonoBehaviour
    {
        #region Variables

        [Header("Data")]

        [Tooltip("The rate of speed the AI will move toward the player at")]
        [SerializeField] private float movementSpeed;

        [Tooltip("How close the AI needs to be to the player before attack them witn a close-ranged attack")]
        [SerializeField] private float distanceToAttackFrom;

        // This bool is what tells this and other scripts when the AI has met or exceeded the specified distance between them and player
        private bool isInAttackDistance;

        [Header("Components")]

        // The referecne to the player we move towards
        private Transform playerTransform;

        [Header("Scripts")]

        [SerializeField] private OnGroundChecker onGroundChecker;

        #endregion

        #region Methods
        public void MoveTowardPlayer()
        {
            Debug.Log("Called MoveTowardPlayer");

            if (Vector3.Distance(transform.position, playerTransform.position) > distanceToAttackFrom)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, movementSpeed * Time.deltaTime);

                if (!onGroundChecker.GetOnGroundValue())
                {
                    transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                }

                RotateTowardsPlayer();
                SetIsInAttackDistanceValue(false);
            }
            else
            {
                SetIsInAttackDistanceValue(true);
            }
        }

        /// <summary>
        /// Called by MeleeEnemyAI to check value before moving into Attacking
        /// </summary>
        /// <returns></returns>
        public bool DistanceFromPlayerCheck()
        {
            return isInAttackDistance;
        }

        public void SetPlayerTransformReference(Transform player)
        {
            playerTransform = player;
        }

        public Transform GetPlayerTransform()
        {
            return playerTransform;
        }

        public void SetIsInAttackDistanceValue(bool value)
        {
            //Debug.Log("SetIsInAttackDistanceValue method has been called");
            isInAttackDistance = value;
        }

        /// <summary>
        /// Rotates toward player while appraoching and not exceeding or meeting the attack distance
        /// </summary>
        private void RotateTowardsPlayer()
        {
            Debug.Log("I'm rotating toward the player");

            if (!onGroundChecker.GetOnGroundValue())
            {
                Debug.Log("We've touched the ground");

                transform.rotation = Quaternion.LookRotation((playerTransform.position - transform.position), Vector3.up);
            }
        }

        #endregion
    }
}