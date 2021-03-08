using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/************ CAMERA FOLLOWS AND ROTATES WITH PLAYER ***********/

public class CameraFollowRotate : MonoBehaviour
{

    [SerializeField]
    public Transform target;

    [SerializeField]
    private Vector3 offsetPosition;

    [SerializeField]
    private Space offsetPositionSpace = Space.Self;

    [SerializeField]
    private bool lookAt = true;

    // Lerp smoothness
    [Range(0.01f, 1.0f)]
    public float Smoothness = 0.5f;

    private void Start()
    {

    }

    private void LateUpdate()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (target == null)
        {
            Debug.LogWarning("Missing target reference!", this);
            return;
        }

        // compute position        
        if (offsetPositionSpace == Space.Self)
        {
            transform.position = Vector3.Lerp(transform.position, target.TransformPoint(offsetPosition), 0.05f);
        }
        else
        {
            transform.position = target.position + offsetPosition;
        }

        // compute rotation
        if (lookAt)
        {
            transform.LookAt(target);
        }
        else
        {
            transform.rotation = target.rotation;
        }
    }
}
