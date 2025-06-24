using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public class IdleStateManager : MonoBehaviour
    {
        [Header("Data")]

        [Tooltip("This determines how fast the AI moves in the world")]
        [SerializeField] private float movementSpeed;

        [Header("Components")]

        [SerializeField] private Rigidbody enemyRB; // We use this for moving the AI and accounting for collisions as well

        public IEnumerator BeginWalkCycle()
        {
            Debug.Log("We're moving at the rate of: " + enemyRB.velocity);

            Vector3 pointToTravelTo = transform.position + new Vector3(0, 0, 5);

            while (transform.position.z < pointToTravelTo.z)
            {
                enemyRB.velocity = Vector3.forward * Time.fixedDeltaTime * movementSpeed;
                yield return null;
            }

            enemyRB.velocity = Vector3.zero;

            pointToTravelTo = transform.position - new Vector3(0, 0, 5);

            transform.rotation = Quaternion.LookRotation(pointToTravelTo);

            while (transform.position.z < pointToTravelTo.z)
            {
                enemyRB.velocity = Vector3.forward * Time.fixedDeltaTime * movementSpeed;
                yield return null;
            }

            enemyRB.velocity = Vector3.zero;
        }

        #region Default Unity Methods
        private void FixedUpdate()
        {
            //BeginWalkCycle();
        }

        private void Start()
        {
            //StartCoroutine(BeginWalkCycle());    
        }

        private void Update()
        {
            Vector3 pointToTravelTo = transform.position - new Vector3(0, 0, 5);

            transform.rotation = Quaternion.LookRotation(pointToTravelTo);
        }
        #endregion
    }
}