using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public class AttackStateRangedManager : AttackStateManager
    {
        [Header("Scripts (Ranged)")]

        [SerializeField] private EnemyRangedAnimationManager enemyRangedAnimationManager;

        public override void Attack()
        {
            //Debug.Log("Attack() in AttackStateRangedManager has been called");

            if (attackCoroutine == null)
            {
                //Debug.Log("attackCoroutine in AttackStateRangedManager is null");

                enemyRangedAnimationManager.StartAnimation();
                attackCoroutine = StartCoroutine(RangedAttackCoroutine());
            }
        }
        
        public float GetDistanceToAttackFrom()
        {
            return distanceToAttackFrom;
        }

        private IEnumerator RangedAttackCoroutine()
        {
            while (distanceFromPlayerChecker.CheckDistanceFromPlayer() < distanceToAttackFrom)
            {
                // Find player position
                Vector3 playerPosition = playerApproacher.GetPlayerTransform().position;

                // When we have player position, calculate angle for pillow arc


                // Play animation for throwing

                // Wait for length of animation before continuing

                // Instantiate pillow prefab

                // Apply force to move pillow in calculated arc

                // Wait for certain amount of seconds before repeating

                yield return null;
            }
        }
    }
}