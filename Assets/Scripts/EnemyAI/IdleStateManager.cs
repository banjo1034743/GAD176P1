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

        [Tooltip("This number determines how fast the AI turns around")]
        [SerializeField] private float turnSpeed;

        [Header("Components")]

        [SerializeField] private Rigidbody enemyRB; // We use this for moving the AI and accounting for collisions as well

        public IEnumerator BeginWalkCycle(bool stateCheck)
        {
            while (stateCheck)
            {
                //Debug.Log("We're moving at the rate of: " + enemyRB.velocity);

                Vector3 pointToTravelTo = transform.position + new Vector3(0, 0, 5);

                while (transform.position.z < pointToTravelTo.z)
                {
                    enemyRB.velocity = Vector3.forward * Time.fixedDeltaTime * movementSpeed;
                    yield return null;
                }

                enemyRB.velocity = Vector3.zero; // Set velocity to 0 to fully stop motion

                pointToTravelTo = transform.position - new Vector3(0, 0, 5);

                while (transform.rotation.eulerAngles.y < 180)
                {
                    transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
                    yield return null;
                }

                //Debug.Log("broke out of while loop");

                while (transform.position.z > pointToTravelTo.z)
                {
                    enemyRB.velocity = -Vector3.forward * Time.fixedDeltaTime * movementSpeed;
                    yield return null;
                }

                enemyRB.velocity = Vector3.zero;

                //Debug.Log("Current Y rotation in euler: " + transform.rotation.eulerAngles.y);

                while (transform.rotation.eulerAngles.y > 1)
                {
                    transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
                    //Debug.Log("Current Y rotation in euler: " + transform.rotation.eulerAngles.y);
                    yield return null;
                }

                yield return null;
            }
        }

        #region Default Unity Methods

        // This was used to test the functionality of the idle loop
        private void Start()
        {
            //StartCoroutine(BeginWalkCycle());    
        }

        #endregion
    }
}