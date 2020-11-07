using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovenment : MonoBehaviour
{
    public bool isActive = true;
    public FixedJoystick FixedStick;
    [Space]
    public float MoveSpeed = 10f;

    CharacterController character;

    private void Start()
    {
        character = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (isActive) moving();
    }

    void moving()
    {
        Vector3 mov = Vector3.forward * Input.GetAxis("Vertical") * MoveSpeed +
            Vector3.right * Input.GetAxis("Horizontal") * MoveSpeed - Vector3.up * 2;
        character.Move(mov * Time.deltaTime);
    }
}
