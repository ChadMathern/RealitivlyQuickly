using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour { 
 public enum RotationAxes
{
    MouseXAndY = 0,
    MouseX = 1,
    MouseY = 2
}
public Camera _camera;
private CharacterController controller;
public RotationAxes axes = RotationAxes.MouseXAndY;
public float sensitivityHor = 9.0f;
public float sensitivityVert = 9.0f;

private float minimumVert;
private float maximumVert;

private float _rotationX = 0;


    public void OnGUI()
    {
        int size = 20;
        float posX = _camera.pixelWidth / 2 - (size / 4);
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "+");

    }
    void Start()
{
        _camera = GetComponent<Camera>();
        Rigidbody body = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (body != null)
        body.freezeRotation = true;
        minimumVert = -45f;
        maximumVert = 45f;
        
}

    void Update()
{
        float dx = Input.GetAxis("Mouse X");
        dx = dx * Mathf.Abs(dx);
        float dy = Input.GetAxis("Mouse Y");
        dy = dy * Mathf.Abs(dy);

        if (axes == RotationAxes.MouseX)
    {
        transform.Rotate(0, dx * sensitivityHor, 0);
    }
    else if (axes == RotationAxes.MouseY)
    {
        _rotationX -= dy * sensitivityVert;
        _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

        float rotationY = transform.localEulerAngles.y;

        transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
    }
    else
    {
        _rotationX -= dy * sensitivityVert;
        _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

        float delta = dx * sensitivityHor;
        float rotationY = transform.localEulerAngles.y + delta;

        transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
    }

        if (Input.GetMouseButton(1))
        {
            _camera.fieldOfView = 30;
        }
        else
        {
            _camera.fieldOfView = 90;
        }
        //if (axes == RotationAxes.MouseY && !controller.isGrounded) ;
          //{
        //      _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
        //      _rotationX = Mathf.Clamp(360f, 360f, 360f);

        //      float rotationY = transform.localEulerAngles.y;

        //      transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        //  }
        //  else
        //  {
        //      _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
        //      _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

        //      float delta = Input.GetAxis("Mouse X") * sensitivityHor;
        //      float rotationY = transform.localEulerAngles.y + delta;

        //      transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        //  }


    }
}
