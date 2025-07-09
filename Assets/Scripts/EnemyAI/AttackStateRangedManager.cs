using System.Collections;
using TMPro;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public class AttackStateRangedManager : AttackStateManager
    {
        #region Variables

        [Header("Data (Ranged)")]

        [SerializeField] private float pillowProjectileSpeed = 10f;

        [SerializeField] private float pillowThrowingArcPeak = 10f;

        /* This variable is used for calculating the arc angle for the pillow. It is declared here as it is used for checking if the pillow should be moved further
         * along the X axis
         */
        private float xPosToGoTo; 

        [Tooltip("This is what we use for calculating the angle of the arc trajectory of the pillow thrown")]
        [SerializeField] private AnimationCurve animationCurve;

        private Vector3 pillowTrajectoryStartPoint;

        [SerializeField] private Vector3 pillowTrajectoryEndPoint;

        [SerializeField] private Vector3 pillowThrowingRange;

        [SerializeField] private bool isThrowingForward; // serialized for testing

        [Header("Objects (Ranged)")]

        [SerializeField] private GameObject pillowProjectilePrefab;

        private GameObject pillow;

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
                pillow = pillowProjectile;

                while (pillowProjectile != null)
                {
                    //Debug.Log(Mathf.Round(Vector3.Distance(pillowProjectile.transform.position, pillowTrajectoryEndPoint) * 100f) * 0.01f);
                    Debug.Log("In While Loop for Vector3.Distance in AttackStateRangedManager");

                    if (pillowProjectile.transform.parent != null)
                    {
                        pillowProjectile.transform.parent = null;
                    }

                    // If fire frontward bool, do this
                    if (isThrowingForward)
                    {
                        if ((pillowProjectile.transform.position.y - pillowTrajectoryEndPoint.y) > 0.1f && pillowProjectile.transform.position.z <= pillowTrajectoryEndPoint.z)
                        {
                            Debug.Log("We have met the requirements for the if loop in the while loop");

                            Vector3 newPos = CalculatePillowThrow(pillowProjectile);

                            pillowProjectile.transform.position = newPos;
                        }
                    }
                    else if (!isThrowingForward) // else if fire backward bool, do this
                    {
                        if ((pillowProjectile.transform.position.y - pillowTrajectoryEndPoint.y) > 0.1f && pillowProjectile.transform.position.z > -pillowTrajectoryEndPoint.z)
                        {
                            Debug.Log("(REVERSE) We have met the requirements for the if loop in the while loop");

                            Vector3 newPos = CalculatePillowThrow(pillowProjectile);

                            pillowProjectile.transform.position = newPos;
                        }
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
            GameObject pillow = Instantiate(pillowProjectilePrefab, new Vector3(pillowProjectileHoldingPoint.position.x, pillowProjectileHoldingPoint.position.y + 0.25f, pillowProjectileHoldingPoint.position.z), Quaternion.Euler(playerTransform.position - transform.position));
            pillowTrajectoryStartPoint = pillow.transform.position;
            pillowTrajectoryEndPoint = new Vector3(playerTransform.position.x, 0, playerTransform.position.z);

            pillowThrowingRange = playerTransform.position - pillowTrajectoryStartPoint;

            //switch (isThrowingForward)
            //{
            //    case true:
            //        Mathf.Abs(pillowProjectileSpeed);
            //        break;
            //    case false:
            //        pillowProjectileSpeed = -pillowProjectileSpeed;
            //        break;
            //}
            //if (pillowThrowingRange.z < 0)
            //{
            //    pillowProjectileSpeed = -pillowProjectileSpeed;
            //}

            pillow.transform.parent = pillowProjectileHoldingPoint;

            return pillow;
        }

        private Vector3 CalculatePillowThrow(GameObject pillow)
        {
            Debug.Log("while loop in other while loop in RangedAttackCoroutine in AttackStateRangedManager called!");

            // Declare local float zPosToGoTo. Initialize by setting its value to the local position of the pillow
            /* added by vale of pillowProjectileSpeed multiplied by Time.deltaTime
             */
            float zPosToGoTo = pillow.transform.position.z + pillowProjectileSpeed * Time.deltaTime;

            /* Declare a new float called normalizedZPosToGoTo. Initliaze it to be the sum of zPosToGoTo subtracted by
             * the z value of pillowTrajectoryStartPoint.z, divided by the value of pillowThrowingRange.z
             */
            float normalizedZPosToGoTo = (zPosToGoTo - pillowTrajectoryStartPoint.z) / pillowThrowingRange.z;


            /* Declare a float called normalizedYPosToGoTo. In animation curve, go to horizontal axis and return number which
             * is placed at the point of axis specified in method. Initialize the var with number returned  
             */
            float normalizedYPosToGoTo = animationCurve.Evaluate(normalizedZPosToGoTo);

            /* Declare new float var called yPosToGoTo. Initialize it to be the normalizedYPosToGoTo value multiplied by the 
             * value in pillowThrowingArcPeak
             */
            float yPosToGoTo = pillowTrajectoryStartPoint.y + normalizedYPosToGoTo * pillowThrowingArcPeak;

            if (isThrowingForward)
            {
                if (pillow.transform.position.x <= pillowTrajectoryEndPoint.x && !Mathf.Approximately(pillowTrajectoryEndPoint.x, 0))
                {
                    xPosToGoTo = pillow.transform.position.x + pillowProjectileSpeed / 2 * Time.deltaTime;
                }

                return new Vector3(Mathf.Round(xPosToGoTo * 100f) * 0.01f, Mathf.Round(yPosToGoTo * 100f) * 0.01f, Mathf.Round(zPosToGoTo * 100f) * 0.01f);
            }
            else
            {
                if (pillow.transform.position.x >= pillowTrajectoryEndPoint.x && !Mathf.Approximately(pillowTrajectoryEndPoint.x, 0))
                {                                              // making the line below divide by the distance between the start and end trajectory worth trying
                    xPosToGoTo = pillow.transform.position.x + -pillowProjectileSpeed / 2 * Time.deltaTime; // -pillowProjectileSpeed / 2
                }

                return new Vector3(Mathf.Round(xPosToGoTo * 100f) * 0.01f, Mathf.Round(yPosToGoTo * 100f) * 0.01f, Mathf.Round(zPosToGoTo * 100f) * 0.01f);
            }

            /* Return new Vector3 var consisting of the x value of the end point trying to be reached, the value of
             * yPosToGoTo subtracted by 1 to account for the height added when spawning, to ensure it reaches ground,
             * and the value of zPosToGoTo for the z axis
             */
            //return new Vector3(Mathf.Round(xPosToGoTo * 100f) * 0.01f, Mathf.Round(yPosToGoTo * 100f) * 0.01f, Mathf.Round(zPosToGoTo * 100f) * 0.01f);
        }

        //private void AdjustArcSizeByDistance(GameObject pillow)
        //{
        //    if (pillow != null)
        //    {
        //        float distanceFromPlayerZ = pillowTrajectoryEndPoint.z - pillowTrajectoryStartPoint.z;
        //        pillowThrowingArcPeak = Mathf.Abs(distanceFromPlayerZ) * pillowThrowingArcPeak;
        //    }
        //}

        #endregion

        #region Unity Methods

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

                switch (transform.rotation.y)
                {
                    case > 0:
                        // Enable fire frontward bool
                        isThrowingForward = true;
                        break;
                    case < 0:
                        // Disable fire frontward bool
                        isThrowingForward = false;
                        break;
                    default:
                        break;
                }
            }

            if (pillow != null)
            {
                debugTextPillowPosition.text = "Pillow pos: " + pillow.transform.position;
                debugTextPillowTrajectoryEndPoint.text = "End point: " + new Vector3(pillowTrajectoryEndPoint.x, 0, pillowTrajectoryEndPoint.z).ToString();
            }
            else
            {
                debugTextPillowPosition.text = "Null";
                debugTextPillowTrajectoryEndPoint.text = "Null";
            }
        }

        #endregion
    }
}