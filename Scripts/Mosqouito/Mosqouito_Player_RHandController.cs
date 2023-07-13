using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mosquito
{
    public class Mosqouito_Player_RHandController : MonoBehaviour
    {
        public GameObject rightHand_currentPos;
        public GameObject leftHand_currentPos;
        public bool is_RColCheck = false;

        private void Start()
        {
            is_RColCheck = false;
        }

        private void Update()
        {
            if (leftHand_currentPos)
            {
                //Debug.Log("Vector3.Distance(Mosqouito_Player_LHandController.Instance.leftHand_currentPos, transform.position) : " + Vector3.Distance(Mosqouito_Player_LHandController.Instance.leftHand_currentPos.transform.position, transform.position));
                if (Vector3.Distance(leftHand_currentPos.transform.position, transform.position) <= 0.4f)
                {
                    //Debug.Log("R_Hand : " + transform.position);
                    is_RColCheck = true;
                }
                else
                {
                    is_RColCheck = false;
                }
            }
            else
            {
                return;
            }
       
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (is_RColCheck)
            {
                if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Insects"))
                {
                    collision.collider.gameObject.transform.GetComponent<Insect>().Perish();
                    SoundManager.Instance.PlaySFX("slap-sound");
                }
            }
        }
    }
}

