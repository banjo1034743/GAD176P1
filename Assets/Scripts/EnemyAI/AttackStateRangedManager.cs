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

        [SerializeField] private float pillowThrowingArcPeak = 10f;

        [Tooltip("This is what we use for calculating the angle of the arc trajectory of the pillow thrown")]
        [SerializeField] private AnimationCurve animationCurve;

        [Header("Objects (Ranged)")]

        [SerializeField] private GameObject pillowProjectilePrefab;

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
                // Play animation for throwing
                enemyRangedAnimationManager.StartAnimation();

                // Wait for length of animation before continuing
                yield return new WaitForSeconds(enemyRangedAnimationManager.GetAttackAnimationLength());

                // Instantiate pillow prefab
                GameObject pillowProjectile = Instantiate(pillowProjectilePrefab, pillowProjectileHoldingPoint.position + new Vector3(0, 0, 2.5f), Quaternion.identity);
                pillowProjectile.transform.position = pillowProjectileHoldingPoint.position;

                //apply force to move pillow in calculated arc
                while (pillowProjectile != null)
                {
                    // when we have player position, calculate angle for pillow arc
                    Debug.Log("while loop in other while loop in RangedAttackCoroutine in AttackStateRangedManager called!");

                    Vector3 pillowThrowingRange = playerApproacher.GetPlayerTransform().position - pillowProjectile.transform.position;

                    if (pillowThrowingRange.z < 0)
                    {
                        pillowProjectileSpeed = -pillowProjectileSpeed;
                    }

                    // adding speed value to forward dir by time.deltatime
                    float zPos = pillowProjectile.transform.position.z + pillowProjectileSpeed * Time.deltaTime;

                    // normalizing the new value above and initialiing a new var with its value
                    float zPosNormalized = (zPos - pillowProjectileHoldingPoint.position.z) / pillowThrowingRange.z;

                    // Normalise value for variable for y pos by calling Evaluate() and inputing normalized z var value
                    float yPosNormalized = animationCurve.Evaluate(zPosNormalized);

                    float yPosAbsolute = yPosNormalized * pillowThrowingRange.y;

                    // Initialise new var for nonnormalised y pos vr with the normalized y var * a var for peak of arc
                    float yPos = pillowProjectileHoldingPoint.position.y + yPosNormalized * pillowThrowingArcPeak + yPosNormalized;

                    // create new vector3 of of the z and y values
                    Vector3 newPos = new Vector3(0, yPos, zPos);

                    // Set position to new vector3
                    pillowProjectile.transform.position = newPos;

                    if (Vector3.Distance(pillowProjectile.transform.position, playerApproacher.GetPlayerTransform().position) < 1f)
                    {
                        Debug.Log("Destroyed pillow");
                        Destroy(pillowProjectile);
                    }

                    yield return null;
                }
                // Wait for certain amount of seconds before repeating

                yield return new WaitForSeconds(attackCooldown);
            }
        }
    }
}