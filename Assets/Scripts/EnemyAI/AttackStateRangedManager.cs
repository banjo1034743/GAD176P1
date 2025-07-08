using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
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

        private Vector3 pillowTrajectoryStartPoint;

        [SerializeField] private Vector3 pillowTrajectoryEndPoint;

        [SerializeField] private Vector3 pillowThrowingRange;

        [Header("Objects (Ranged)")]

        [SerializeField] private GameObject pillowProjectilePrefab;

        [Header("Components (Ranged)")]

        [SerializeField] private Transform pillowProjectileHoldingPoint;

        private Transform playerTransform;

        [Header("Scripts (Ranged)")]

        [SerializeField] private EnemyRangedAnimationManager enemyRangedAnimationManager;

        [SerializeField] private RangedEnemyAI rangedEnemyAI;

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
            GameObject pillowProjectile;

            while (distanceFromPlayerChecker.CheckDistanceFromPlayer() < distanceToAttackFrom)
            {
                // Play animation for throwing
                // enemyRangedAnimationManager.StartAnimation();

                // Wait for length of animation before continuing
                yield return new WaitForSeconds(enemyRangedAnimationManager.GetAttackAnimationLength());

                // Instantiate pillow prefab

                pillowProjectile = PreparePillowProjectile();

                //apply force to move pillow in calculated arc
                while (Vector3.Distance(pillowProjectile.transform.position, pillowTrajectoryEndPoint) > 1.5f)
                {
                    Debug.Log("In While Loop for Vector3.Distance in AttackStateRangedManager");

                    if (pillowProjectile.transform.position.z < pillowTrajectoryEndPoint.z) //  && pillowProjectile.transform.position.y > 0.1f
                    {
                        Vector3 newPos = CalculatePillowThrow(pillowProjectile);

                        // Set position to new vector3
                        pillowProjectile.transform.position = newPos;
                    }

                    Debug.Log(Vector3.Distance(pillowProjectile.transform.position, pillowTrajectoryEndPoint));
                    yield return null;
                }

                Debug.Log("Destroyed pillow");
                Destroy(pillowProjectile);
                pillowProjectile = null;

                yield return new WaitForSeconds(attackCooldown);
            }
        }

        private GameObject PreparePillowProjectile()
        {
            GameObject pillow = Instantiate(pillowProjectilePrefab, pillowProjectileHoldingPoint.position + new Vector3(0, 0, 2.5f), Quaternion.identity);
            pillow.transform.parent = null;
            pillowTrajectoryStartPoint = pillow.transform.position;
            //pillowTrajectoryStartPoint = Vector3.zero + new Vector3(pillow.transform.position.x, 0, pillow.transform.position.z);
            pillowTrajectoryEndPoint = Vector3.zero + new Vector3(playerTransform.position.x, 0, playerTransform.position.z);

            pillowThrowingRange = playerTransform.position - pillowTrajectoryStartPoint;

            if (pillowThrowingRange.z < 0)
            {
                pillowProjectileSpeed = -pillowProjectileSpeed;
            }

            pillow.transform.parent = null;

            return pillow;
        }

        private Vector3 CalculatePillowThrow(GameObject pillow)
        {
            // when we have player position, calculate angle for pillow arc
            Debug.Log("while loop in other while loop in RangedAttackCoroutine in AttackStateRangedManager called!");

            // adding speed value to forward dir by time.deltatime
            float zPosToGoTo = pillow.transform.position.z + pillowProjectileSpeed * Time.deltaTime;
            //float xPosToGoTo = (pillowProjectile.transform.position.x + pillowProjectileSpeed) / 2 * Time.deltaTime;

            // normalizing the new value above and initialiing a new var with its value
            float normalizedZPosToGoTo = (zPosToGoTo - pillowTrajectoryStartPoint.z) / pillowThrowingRange.z;
            //float normalizedXPosToGoTo = (xPosToGoTo -  pillowTrajectoryStartPoint.x) / pillowThrowingRange.x;

            // Normalise value for variable for y pos by calling Evaluate() and inputing normalized z var value
            float normalizedYPosToGoTo = animationCurve.Evaluate(normalizedZPosToGoTo);

            float absoluteYPosToGoTo = normalizedYPosToGoTo * pillowThrowingRange.y;

            // Initialise new var for nonnormalised y pos vr with the normalized y var * a var for peak of arc
            float yPosToGoTo = pillowTrajectoryStartPoint.y + normalizedYPosToGoTo * pillowThrowingArcPeak + absoluteYPosToGoTo;

            // create new vector3 of of the z and y values
            return new Vector3(pillowTrajectoryEndPoint.x, yPosToGoTo, zPosToGoTo);
        }

        private void Update()
        {
            if (gameObject.activeSelf && playerTransform == null)
            {
                if (playerApproacher.GetPlayerTransform() != null)
                {
                    playerTransform = playerApproacher.GetPlayerTransform();
                }
            }
            if (playerTransform != null && rangedEnemyAI.GetIsAttackStateEnabled())
            {
                transform.LookAt(playerApproacher.GetPlayerTransform().position, Vector3.up);
            }
        }
    }
}