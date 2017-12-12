using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [Header("Target following")]
    [SerializeField]
    GameObject target;
    [SerializeField] float followSpeed;

    [Header("Camera rotating")]
    bool isRotating;
    Vector2 mousePosition;
    [SerializeField] float rotateSpeed;


    // Use this for initialization
    void Start()
    {
        StartCoroutine(FollowTarget());
    }

    private void Update()
    {
        if (Input.GetButton("Fire2") && !isRotating)
        {
            StartCoroutine(RotateCamera());
        }
    }



    IEnumerator FollowTarget()
    {
        while (true)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * followSpeed);
            yield return null;
        }
    }

    IEnumerator RotateCamera()
    {
        isRotating = true;

        //mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        while (Input.GetButton("Fire2"))
        {
            //Vector2 mouseOffset = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - mousePosition;

            Vector2 mouseOffset = new Vector2(Input.GetAxis("MouseHorizontal"), Input.GetAxis("MouseVertical"));

            transform.rotation = Quaternion.Euler(Mathf.Clamp(transform.rotation.eulerAngles.x + (mouseOffset.y / 1000) * rotateSpeed, 10, 65), transform.rotation.eulerAngles.y + (mouseOffset.x / 1000) * rotateSpeed, 0);

            

            //Reset mouse position
            //mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            yield return null;
        }

        isRotating = false;
    }
}
