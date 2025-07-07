using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public class AttackStateRangedManager : AttackStateManager
    {
        [Header("Data (Ranged)")]

        [SerializeField] private float pillowProjectileSpeed = 10f;

        [SerializeField] private float amplitude = 1.0f;

        [SerializeField] private float frequency = 1.0f;

        [Header("Objects (Ranged)")]

        [SerializeField] private GameObject pillowProjectile;

        [Header("Components (Ranged)")]

        [SerializeField] private Transform pillowProjectileHoldingPoint;

        [Header("Scripts (Ranged)")]

        [SerializeField] private EnemyRangedAnimationManager enemyRangedAnimationManager;

        public override void Attack()
        {
            Debug.Log("Attack() in AttackStateRangedManager has been called");

            if (attackCoroutine == null)
            {
                Debug.Log("attackCoroutine in AttackStateRangedManager is null");

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
                pillowProjectile = Instantiate(pillowProjectile);
                pillowProjectile.transform.position = pillowProjectileHoldingPoint.position;

                //apply force to move pillow in calculated arc
                while (Vector3.Distance(pillowProjectile.transform.position, playerPosition) > 0.1f)
                {
                    // when we have player position, calculate angle for pillow arc
                    float pillowTrajectoryAngle = (transform.position.y + amplitude) * Mathf.Sin(Time.time * frequency);
                    Vector3 pillowTravelDirection = Vector3.MoveTowards(pillowProjectile.transform.position, playerPosition, pillowProjectileSpeed * Time.deltaTime);

                    pillowProjectile.transform.position = new Vector3(pillowTravelDirection.x, pillowTrajectoryAngle, pillowTravelDirection.z);

                    yield return null;
                }

                // Wait for certain amount of seconds before repeating

                yield return new WaitForSeconds(attackCooldown);
            }
        }
    }
}