using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject TargetGameObject
    {
        get
        {
            return targetGameObject;
        }
        set
        {
            followTarget = true;
            targetGameObject = value;
        }
    }
    private GameObject targetGameObject;
    public Vector3 TargetPosition
    {
        get
        {
            return targetPosition;
        }
        set
        {
            followTarget = false;
            targetPosition = value;
        }
    }
    private Vector3 targetPosition;
    [SerializeField] float followSpeed;

    [Header("Camera rotating")]
    bool isRotating;
    Vector2 mousePosition;
    [SerializeField] float rotateSpeed;

    bool followTarget = true;


    // Use this for initialization
    void Start()
    {
        GameController.instance.cameraController = this;
    }

    private void Update()
    {
        if (followTarget)
        {
            if (targetGameObject != null)
                transform.position = Vector3.Lerp(transform.position, targetGameObject.transform.position, Time.deltaTime * followSpeed);
        }
        else
        {
            if (targetPosition != null)
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
        }
        
    }
}
