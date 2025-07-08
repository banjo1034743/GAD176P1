using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public class AttackStateRangedManager : AttackStateManager
    {
        #region Variables

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

        [Header("Debug")]

        [SerializeField] private TextMeshProUGUI debugTextPillowPosition;

        [SerializeField] private TextMeshProUGUI debugTextPillowTrajectoryEndPoint;

        #endregion

        #region Methods

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
                // enemyRangedAnimationManager.StartAnimation();

                yield return new WaitForSeconds(enemyRangedAnimationManager.GetAttackAnimationLength());

                pillowProjectile = PreparePillowProjectile();
                pillowProjectile.transform.rotation = Quaternion.LookRotation(pillowThrowingRange, Vector3.up);
                // Less than 0, check for this with if loops in select areas to change direction correctly when looking from behind

                //apply force to move pillow in calculated arc
                while (Mathf.Round(Vector3.Distance(pillowProjectile.transform.position, pillowTrajectoryEndPoint) * 100f) * 0.01f >= 3f && pillowProjectile != null)
                {
                    Debug.Log(Mathf.Round(Vector3.Distance(pillowProjectile.transform.position, pillowTrajectoryEndPoint) * 100f) * 0.01f);
                    Debug.Log("In While Loop for Vector3.Distance in AttackStateRangedManager");


                    if (pillowProjectile.transform.parent != null)
                    {
                        pillowProjectile.transform.parent = null;
                    }

                    if (pillowProjectile.transform.position.z <= pillowTrajectoryEndPoint.z)
                    {
                        Debug.Log("We have met the requirements for the if loop in the while loop");

                        //AdjustArcSizeByDistance(pillowProjectile);
                        Vector3 newPos = CalculatePillowThrow(pillowProjectile);

                        // Set position to new vector3
                        pillowProjectile.transform.position = newPos;
                    }

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
            GameObject pillow = Instantiate(pillowProjectilePrefab, new Vector3(pillowProjectileHoldingPoint.position.x, pillowProjectileHoldingPoint.position.y + 2, pillowProjectileHoldingPoint.position.z), Quaternion.Euler(playerTransform.position - transform.position)); // pillowProjectilePrefab, pillowProjectileHoldingPoint.position, Quaternion.Euler(playerTransform.position - transform.localPosition
            pillowTrajectoryStartPoint = pillow.transform.position;
            pillowTrajectoryEndPoint = Vector3.zero + new Vector3(playerTransform.localPosition.x, 0, playerTransform.localPosition.z);

            pillowThrowingRange = playerTransform.position - pillowTrajectoryStartPoint;

            if (pillowThrowingRange.z < 0)
            {
                pillowProjectileSpeed = -pillowProjectileSpeed;
            }

            pillow.transform.parent = pillowProjectileHoldingPoint;

            return pillow;
        }

        private Vector3 CalculatePillowThrow(GameObject pillow)
        {
            // when we have player position, calculate angle for pillow arc
            Debug.Log("while loop in other while loop in RangedAttackCoroutine in AttackStateRangedManager called!");

            // Declare local float zPosToGoTo. Initialize by setting its value to the local position of the pillow
            /* added by vale of pillowProjectileSpeed multiplied by Time.deltaTime
             */

            float zPosToGoTo = pillow.transform.position.z + pillowProjectileSpeed * Time.deltaTime;
            float xPosToGoTo = pillow.transform.position.x + (playerTransform.position.x - pillow.transform.position.x) / pillowThrowingRange.x * Time.deltaTime;

            /* Declare a new float called normalizedZPosToGoTo. Initliaze it to be the sum of zPosToGoTo subtracted by
             * the z value of pillowTrajectoryStartPoint.z, divided by the value of pillowThrowingRange.z
             */

            float normalizedZPosToGoTo = (zPosToGoTo - pillowTrajectoryStartPoint.z) / pillowThrowingRange.z;
            //Debug.Log(normalizedZPosToGoTo);
            //float normalizedXPosToGoTo = (xPosToGoTo -  pillowTrajectoryStartPoint.x) / pillowThrowingRange.x;

            /* Declare a float called normalizedYPosToGoTo. In animation curve, go to horizontal axis and return number which
             * is placed at the point of axis specified in method. Initialize the var with number returned  
             */

            float normalizedYPosToGoTo = animationCurve.Evaluate(normalizedZPosToGoTo);

            /* Declare new float var called yPosToGoTo. Initialize it to be the normalizedYPosToGoTo value multiplied by the 
             * value in pillowThrowingArcPeak
             */

            float yPosToGoTo = pillowTrajectoryStartPoint.y + normalizedYPosToGoTo * pillowThrowingArcPeak;

            /* Return new Vector3 var consisting of the x value of the end point trying to be reached, the value of
             * yPosToGoTo subtracted by 1 to account for the height added when spawning, to ensure it reaches ground,
             * and the value of zPosToGoTo for the z axis
             */

            return new Vector3(Mathf.Round(xPosToGoTo * 100f) * 0.01f, Mathf.Round(yPosToGoTo * 100f) * 0.01f - 1.5f, Mathf.Round(zPosToGoTo * 100f) * 0.01f);
        }

        private void AdjustArcSizeByDistance(GameObject pillow)
        {
            if (pillow != null)
            {
                float distanceFromPlayerZ = playerTransform.position.z - pillow.transform.position.z;
                pillowThrowingArcPeak = Mathf.Abs(distanceFromPlayerZ) * pillowThrowingArcPeak;
            }
        }

        #endregion

        #region Unity Methods

        private void Awake()
        {

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
                //pillowProjectileHoldingPoint.LookAt(playerApproacher.GetPlayerTransform().localPosition, Vector3.up);
            }

            debugTextPillowPosition.text = "Pillow pos: " + transform.position.ToString();
            debugTextPillowTrajectoryEndPoint.text = "End point: " + new Vector3(pillowTrajectoryEndPoint.x, 0, pillowTrajectoryEndPoint.z).ToString();
        }

        #endregion
    }
}