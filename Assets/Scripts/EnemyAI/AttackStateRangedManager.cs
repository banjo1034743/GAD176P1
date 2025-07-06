using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public class AttackStateRangedManager : AttackStateManager
    {
        [Header("Data (Ranged)")]

        [SerializeField] private float pillowProjectileSpeed = 10f;

        [Header("Objects (Ranged)")]

        [SerializeField] private GameObject pillowProjectile;

        [Header("Components (Ranged)")]

        [SerializeField] private Transform pillowProjectileHoldingPoint;

        [Header("Scripts (Ranged)")]

        [SerializeField] private EnemyRangedAnimationManager enemyRangedAnimationManager;

        public override void Attack()
        {
            //Debug.Log("Attack() in AttackStateRangedManager has been called");

            if (attackCoroutine == null)
            {
                //Debug.Log("attackCoroutine in AttackStateRangedManager is null");

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

                // Play animation for throwing
                enemyRangedAnimationManager.StartAnimation();

                // Wait for length of animation before continuing
                yield return new WaitForSeconds(enemyRangedAnimationManager.GetAttackAnimationLength());

                // Instantiate pillow prefab
                if (pillowProjectile == null)
                {
                    pillowProjectile = Instantiate(pillowProjectile);
                    pillowProjectile.transform.position = pillowProjectileHoldingPoint.position;
                }

                // Apply force to move pillow in calculated arc
                while (Vector3.Distance(pillowProjectile.transform.position, playerPosition) > 0.1f)
                {
                    // When we have player position, calculate angle for pillow arc
                    Vector3 pillowTrajectoryAngle = Vector3.Slerp(transform.position, playerPosition, pillowProjectileSpeed * Time.deltaTime);
                    pillowProjectile.transform.position = pillowTrajectoryAngle;

                    yield return null;
                }

                // Wait for certain amount of seconds before repeating

                yield return new WaitForSeconds(attackCooldown);
            }
        }

        #region Unity Methods

        private void Start()
        {
            pillowProjectile = null;
        }

        #endregion
    }
}