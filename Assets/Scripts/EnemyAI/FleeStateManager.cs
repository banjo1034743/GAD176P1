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

        [Header("Components")]

        // These are points in the world where the AI will move toward in rotational pattern one at a time when in the Flee state
        [SerializeField] private Transform[] enemyAIMovePoints;
        
        [Header("Scripts")]

        [SerializeField] private EnemyAI enemyAI;

        #endregion

        public void CallFleeCycleCoroutine()
        {
            Debug.Log("AI is in the flee state!");

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

                Vector3 pointToTravelTo = enemyAIMovePoints[enemyAIMovePointIndex].position;

                while (transform.position != pointToTravelTo)
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
                
                pointToTravelTo = enemyAIMovePoints[enemyAIMovePointIndex].position;      
            }
        }

        private void MoveAndTurn(Vector3 pointToTravelTo)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointToTravelTo, fleeingMovementSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.RotateTowards(transform.position, pointToTravelTo, 1, 0));
        }
    }
}