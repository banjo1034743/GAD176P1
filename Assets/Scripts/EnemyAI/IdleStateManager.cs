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

        [Header("Scripts")]

        [SerializeField] private EnemyAI enemyAI;

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
                Debug.Log("While loop in StartWalkCycle called in IdleStateManager");

                Vector3 pointToTravelTo = transform.position + new Vector3(0, 0, 5);

                while (transform.position.z < pointToTravelTo.z)
                {
                    transform.position = Vector3.MoveTowards(transform.position, pointToTravelTo, movementSpeed * Time.deltaTime);

                    yield return null;
                }

                pointToTravelTo = transform.position - new Vector3(0, 0, 5);

                while (transform.rotation.eulerAngles.y < 180)
                {
                    Debug.Log("We are rotating");

                    transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);

                    //Vector3 newDirection = Vector3.RotateTowards(transform.forward, pointToTravelTo, turnSpeed * Time.deltaTime, 0);

                    //transform.rotation = Quaternion.LookRotation(newDirection, Vector3.up);

                    yield return null;
                }

                //Debug.Log("broke out of while loop");

                while (transform.position.z > pointToTravelTo.z)
                {
                    transform.position = Vector3.MoveTowards(transform.position, pointToTravelTo, movementSpeed * Time.deltaTime);

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

            StopCoroutine(currentWalkCycle);
            currentWalkCycle = null;
        }
        #endregion

        #region Default Unity Methods
        private void Update()
        {
            // Will run in background to ensure the whole movement is stopped no matter where in walk cycle AI is
            if (!enemyAI.GetIdleStateBool() && currentWalkCycle != null)
            {
                Debug.Log("Coroutine called to stop!");
                StopCoroutine(currentWalkCycle);
            }
        }

        #endregion
    }
}