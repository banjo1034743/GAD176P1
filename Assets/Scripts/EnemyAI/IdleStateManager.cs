using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public class IdleStateManager : MonoBehaviour
    {
        #region Variables
        [Header("Data")]

        [Tooltip("This determines how fast the AI moves in the world")]
        [SerializeField] private float movementSpeed;

        [Tooltip("This number determines how fast the AI turns around")]
        [SerializeField] private float turnSpeed;

        // We use this variable to save the current running walk cycle coroutine to then stop whenever regardless of where code is running in it
        private Coroutine currentWalkCycle = null;

        [Header("Components")]

        [SerializeField] private Rigidbody enemyRB; // We use this for moving the AI and accounting for collisions as well

        [Header("Scripts")]

        [SerializeField] private EnemyAI enemyAI;

        [SerializeField] private OnGroundChecker onGroundChecker;
        #endregion

        #region Methods
        public void CallWalkCycleCoroutine()
        {
            currentWalkCycle = StartCoroutine(StartWalkCycle());
        }

        private IEnumerator StartWalkCycle()
        {
            while (enemyAI.GetIdleStateBool())
            {
                //Debug.Log("We're moving at the rate of: " + enemyRB.velocity);

                Vector3 pointToTravelTo = transform.position + new Vector3(0, 0, 5);

                while (transform.position.z < pointToTravelTo.z)
                {
                    transform.position = Vector3.MoveTowards(transform.position, pointToTravelTo, movementSpeed * Time.deltaTime);

                    if (!onGroundChecker.GetOnGroundValue())
                    {
                        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                    }

                    yield return null;
                }

                pointToTravelTo = transform.position - new Vector3(0, 0, 5);

                while (transform.rotation.eulerAngles.y < 180)
                {
                    Debug.Log("We are rotating");

                    transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
                    yield return null;
                }

                //Debug.Log("broke out of while loop");

                while (transform.position.z > pointToTravelTo.z)
                {
                    transform.position = Vector3.MoveTowards(transform.position, pointToTravelTo, movementSpeed * Time.deltaTime);

                    if (!onGroundChecker.GetOnGroundValue())
                    {
                        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                    }

                    yield return null;
                }

                //Debug.Log("Current Y rotation in euler: " + transform.rotation.eulerAngles.y);

                while (transform.rotation.eulerAngles.y > 1)
                {
                    transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
                    //Debug.Log("Current Y rotation in euler: " + transform.rotation.eulerAngles.y);
                    yield return null;
                }
            }
        }
        #endregion

        #region Default Unity Methods
        private void Update()
        {
            // Will run in background to ensure the whole movement is stopped no matter where in walk cycle AI is
            if (!enemyAI.GetIdleStateBool() && currentWalkCycle != null)
            {
                //Debug.Log("Coroutine called to stop!");
                StopCoroutine(currentWalkCycle);
            }
        }
        #endregion
    }
}