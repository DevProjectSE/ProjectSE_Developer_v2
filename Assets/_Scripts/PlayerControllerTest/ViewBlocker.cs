using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class ViewBlocker : MonoBehaviour
{
    public Transform player;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("OutBounds"))
        {
            Debug.Log("inside");
        }
    }

    private void OnTriggerExit(Collider other)
    {

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        CharacterController cc = player.GetComponent<CharacterController>();
        Vector3 vector3 = cc.transform.position;
        vector3.y += 3;
        Gizmos.DrawWireSphere(vector3, 3);

    }

}
