using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public class FleeStateManager : MonoBehaviour
    {
        #region Variables

        [Header("Data")]

        [SerializeField] private float fleeingMovementSpeed = 5f;

        private int enemyAIMovePointIndex = 0;

        private Coroutine currentFleeCycle = null;

        private Vector3 rotateDirection;

        [Header("Components")]

        // These are points in the world where the AI will move toward in rotational pattern one at a time when in the Flee state
        [SerializeField] private Transform[] enemyAIMovePoints;
        
        [Header("Scripts")]

        [SerializeField] private EnemyAI enemyAI;

        #endregion

        public void CallFleeCycleCoroutine()
        {
            if (currentFleeCycle == null)
            {
                Debug.Log("AI is in the flee state!");
                enemyAIMovePointIndex = 0;
                currentFleeCycle = StartCoroutine(Flee());
            }
        }

        private IEnumerator Flee()
        {
            while (enemyAI.GetFleeStateBool())
            {
                //Debug.Log("We're moving at the rate of: " + enemyRB.velocity);

                Debug.Log("enemyAIMovePointIndex is now = " + enemyAIMovePointIndex);
                Vector3 pointToTravelTo = enemyAIMovePoints[enemyAIMovePointIndex].position;
                rotateDirection = pointToTravelTo - transform.position;

                // enemyAI.GetFleeStateBool added so that we stop properly, so our vector calclation for how far we need to move in IdelStateManager aren't messed with
                while (Vector3.Distance(transform.position, pointToTravelTo) > 0f && enemyAI.GetFleeStateBool())
                {
                    MoveAndTurn(pointToTravelTo);

                    yield return null;
                }

                if (enemyAIMovePointIndex < 3)
                {
                    enemyAIMovePointIndex++;
                }
                else
                {
                    enemyAIMovePointIndex = 0;
                }    

                yield return null;
            }

            // Reset so the Flee Cycle can begin again when the conditions are once again met
            currentFleeCycle = null;
        }

        private void MoveAndTurn(Vector3 pointToTravelTo)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointToTravelTo, fleeingMovementSpeed * Time.deltaTime);

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, rotateDirection, fleeingMovementSpeed * Time.deltaTime, 0);

            transform.rotation = Quaternion.LookRotation(newDirection, Vector3.up);
        }

        #region Unity Methods

        private void Start()
        {
            Debug.Log("enemyAIMovePointIndex is now = " + enemyAIMovePointIndex);
        }

        #endregion
    }
}