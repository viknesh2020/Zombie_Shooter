using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class ThirdPersonInput : MonoBehaviour
{
    public FixedJoystick fixedJoystick;
    public FixedTouchField touchField;
    public FixedButton fixedButton;
    protected ThirdPersonUserControl control;

    protected float cameraAngle;
    public float cameraAngleSpeed;
    public Vector3 cameraAngleMultiplier;

    protected float cameraAngleY;
    public float cameraAngleSpeedY;

    public GameObject touchFieldObject;
    public bool enableTouchField;

    void Start()
    {
        control = GetComponent<ThirdPersonUserControl>();

        if (enableTouchField)
        {
            touchFieldObject.SetActive(true);
        }
        else
        {
            Camera.main.transform.parent = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        control.m_Jump = fixedButton.Pressed;
        control.hInput = fixedJoystick.Horizontal;
        control.vInput = fixedJoystick.Vertical;

        if (enableTouchField)
        {
            cameraAngle += touchField.TouchDist.x * cameraAngleSpeed;
            cameraAngleY = Mathf.Clamp(cameraAngleY - touchField.TouchDist.y * cameraAngleSpeedY, 0.2f, 5f);
            Camera.main.transform.position = transform.position + Quaternion.AngleAxis(cameraAngle, Vector3.up) * new Vector3(cameraAngleMultiplier.x, cameraAngleY, cameraAngleMultiplier.z);
            Camera.main.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.up * 2f - Camera.main.transform.position, Vector3.up);
        }
    }
}
