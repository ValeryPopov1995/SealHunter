using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovenment : MonoBehaviour
{
    public bool isActive = true;
    public FixedJoystick FixedStick;
    [Space]
    public int MoveSpeed = 10;

    CharacterController character;

    private void Start()
    {
        character = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (isActive)
        {
            Vector3 mov = Vector3.forward * FixedStick.Vertical * MoveSpeed +
            Vector3.right * FixedStick.Horizontal * MoveSpeed - Vector3.up * 3;
            character.Move(mov * Time.deltaTime);
        }
    }
}
