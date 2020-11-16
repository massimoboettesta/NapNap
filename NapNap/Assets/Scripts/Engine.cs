using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    public void GetInput(){
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");
    }

    private void Steer(){
        m_steeringAngle = maxSteeringAngle * m_horizontalInput;
        frontDriverW.steerAngle = m_steeringAngle;
        frontPassengerW.steerAngle = m_steeringAngle;
    }

    private void Accelerate(){
        frontDriverW.motorTorque = m_verticalInput * motorForce ;
        frontPassengerW.motorTorque = m_verticalInput * motorForce;
        rearDriverW.motorTorque = m_verticalInput * motorForce;
        rearPassengerW.motorTorque = m_verticalInput * motorForce;
    }
    
    private void UpdateWheelPoses(){
        UpdateWheelPose(frontDriverW,frontDriverT);
        UpdateWheelPose(frontPassengerW,frontPassengerT);
        UpdateWheelPose(rearDriverW,rearDriverT);
        UpdateWheelPose(rearPassengerW,rearPassengerT);
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform){
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;
    }

    private void Start(){
        parentRB = GetComponent<Rigidbody>();
        parentRB.centerOfMass = CenterOfMass;
    }
    private void FixedUpdate(){
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
    }

    private float m_horizontalInput;
    private float m_verticalInput;
    private float m_steeringAngle;

    private Rigidbody parentRB;
    public Vector3 CenterOfMass;
    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;
    public Transform frontDriverT, frontPassengerT;
    public Transform rearDriverT, rearPassengerT;
    public float maxSteeringAngle = 30;
    public float motorForce = 50;
    
}
