using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public class PlayerApproacher : MonoBehaviour
    {
        [Header("Data")]

        [SerializeField] private float movementSpeed;

        [SerializeField] private float distanceToAttackFrom;

        [Header("Components")]

        // The referecne to the player we move towards
        // Note: only made serialized for seeing work in game, remove after confirming
        [SerializeField] private Transform playerTransform;

        public void MoveTowardPlayer()
        {
            Vector3.MoveTowards(transform.position, playerTransform.position, movementSpeed * Time.deltaTime);
        }

        public bool DistanceFromPlayerCheck()
        {
            return false;
        }

        public void SetPlayerTransformReference(Transform player)
        {
            playerTransform = player;
        }
    }
}