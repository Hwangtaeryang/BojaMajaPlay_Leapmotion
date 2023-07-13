using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;        // 따라다닐 타겟 오브젝트의 Transform : Player
    private Transform tr;                // 카메라 자신의 Transform
    private Vector3 targetPosition;
    private int index;

    void Start()
    {
        index = 0;
        tr = GetComponent<Transform>();
    }

    void LateUpdate()
    {
        tr.position = new Vector3(target.position.x, tr.position.y, target.position.z - 1.7f);

        if (!GameObject.Find("Interactable").transform.Find("Target" + index).transform.Find("CameraTarget" + index).gameObject.activeSelf)
        {
            index++;
            //Vector3 dirToTarget = GameObject.Find("Interactable").transform.Find("Target" + index).transform.Find("CameraTarget" + index).gameObject.transform.position - this.transform.position;
            //Vector3 look = Vector3.Slerp(this.transform.forward, dirToTarget.normalized, Time.deltaTime);

            //this.transform.rotation = Quaternion.LookRotation(look, Vector3.up);
        }
        // 바라볼 타겟 : 나무
        //targetPosition = new Vector3(tr.position.x, GameObject.Find("Interactable").transform.Find("Target" + index).gameObject.transform.position.y, tr.position.z);
        tr.LookAt(GameObject.Find("Interactable").transform.Find("Target" + index).transform.Find("CameraTarget" + index).gameObject.transform);
        //tr.LookAt(targetPosition);
    }
}