using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE.GAD176.P1.EnemyAI
{
    public class OnGroundChecker : MonoBehaviour
    {
        #region Variables

        [Header("Data")]

        private bool isOnGround;

        #endregion

        public bool GetOnGroundValue()
        {
            return isOnGround;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Ground")
            {
                isOnGround = true;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.transform.tag == "Ground")
            {
                isOnGround = false;
            }
        }
    }
}