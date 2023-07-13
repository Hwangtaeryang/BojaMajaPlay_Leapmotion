using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mosquito
{
    public class Mosqouito_Player_LHandController : MonoBehaviour
    {
        public GameObject leftHand_currentPos;
        public GameObject rightHand_currentPos;
        public bool is_LColCheck = false;

        private void Start()
        {
            is_LColCheck = false;
        }

        private void Update()
        {
            
            if (rightHand_currentPos)
            {
                //Debug.Log("Vector3.Distance(Mosqouito_Player_RHandController.Instance.rightHand_currentPos, transform.position) : " + Vector3.Distance(Mosqouito_Player_RHandController.Instance.rightHand_currentPos.transform.position, transform.position));
                if (Vector3.Distance(rightHand_currentPos.transform.position, transform.position) <= 0.4f)
                {
                    //Debug.Log("L_Hand : " + transform.position);
                    is_LColCheck = true;
                }
                else
                {
                    is_LColCheck = false;
                }
            }
            else
            {
                return;
            }

        }
        private void OnCollisionEnter(Collision collision)
        {
            if (is_LColCheck)
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

