using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private GameObject m_center;
    [SerializeField]
    private GameObject m_camera;
    [SerializeField]
    private GameObject cameraRoot;
    [SerializeField]
    private float rotateSpeed = 30f;

    private Transform _camTransform;
    private Vector3 _center;

    public bool cameraMode = true;

    [SerializeField]
    private GameObject _inputController;
    private InputManager _input;

    public float mouseSensitivity = 1.0f;
    private float Ypitch = 0f;
    private float Xpitch = 0f;

    void Start()
    {
        _input = _inputController.GetComponent<InputManager>();

        _camTransform = m_camera.gameObject.transform;
        _center = m_center.transform.position;
    }

    void Update()
    {
        Look();
    }

    private void Look()
    {
        Vector2 look = _input.look;
        float mouseX = look.x * mouseSensitivity;
        float mouseY = look.y * mouseSensitivity;
        Xpitch = mouseX;
        Ypitch = mouseY;
        

        _camTransform.RotateAround(_center, Vector3.up, Xpitch * rotateSpeed * Time.deltaTime);
        _camTransform.LookAt(_center, Vector3.up);

        /*
        float mouseX = look.x * mouseSensitivity;
        float mouseY = look.y * mouseSensitivity;

        Ypitch -= mouseY;
        Ypitch = Mathf.Clamp(Ypitch, -60f, 80f);
        Xpitch += mouseX;

        
        _camTransform.rotation = Quaternion.Euler(Ypitch, Xpitch, 0f);
        */
     }

    
}