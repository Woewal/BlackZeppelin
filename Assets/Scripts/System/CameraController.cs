using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [Header("Target following")]
    public GameObject target;
    [SerializeField] float followSpeed;

    [Header("Camera rotating")]
    bool isRotating;
    Vector2 mousePosition;
    [SerializeField] float rotateSpeed;


    // Use this for initialization
    void Start()
    {
        GameController.instance.cameraController = this;
    }

    private void Update()
    {
        if (target != null)
            transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * followSpeed);
    }
}
