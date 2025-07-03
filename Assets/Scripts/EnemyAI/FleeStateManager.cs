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
            //Debug.Log("AI is in the flee state!");

            if (currentFleeCycle == null)
            {
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

                while (Vector3.Distance(transform.position, pointToTravelTo) > 0f)
                {
                    MoveAndTurn(pointToTravelTo);

                    yield return null;
                }

                if (enemyAIMovePointIndex < 3)
                {
                    enemyAIMovePointIndex++;

                    Debug.Log("enemyAIMovePointIndex is now = " + enemyAIMovePointIndex);
                }
                else
                {
                    enemyAIMovePointIndex = 0;
                }    

                yield return null;
            }
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