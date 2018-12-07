using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class for containing basic movement controls and animations as well as camera movements
public class STeevMoveController : MonoBehaviour
{
    Animator Animator;
    public float CameraSpeed = 2.0f;
    public GameObject head;
    private float yaw = 0.0f;
    private float pitch = -90.0f;

    private void Start()
    {
        Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Controls character movement by updating blendtree parameters with horizontal/vertical axis
        float axis = Input.GetAxis("Vertical");
        Animator.SetFloat("Vertical Axis", axis);
        axis = Input.GetAxis("Horizontal");
        Animator.SetFloat("Horizontal Axis", axis);

        //capture values for Yaw and Pitch. Clamp Pitch so the camera doesn't roll. Pitch axis is inverted.
        yaw += CameraSpeed * Input.GetAxis("Mouse X");
        pitch = Mathf.Clamp(pitch + (-CameraSpeed * Input.GetAxis("Mouse Y")), -125f, -60f);

        //apply Yaw to S'Teev so the character can rotate
        transform.eulerAngles = new Vector3(transform.rotation.x, yaw, transform.rotation.z);

        //apply pitch to parent of camera (head) so camera rotates around character in the vertical axis according to mouse movement
        head.transform.eulerAngles = new Vector3(pitch, yaw, head.transform.rotation.z);
    }
}
