using System.Collections;
using UnityEngine;

public class EnemyMovenment : MonoBehaviour
{
    public float MoveSpeed = 5;
    public float MaxRightSpeed = 3;

    float setRightSpeed = 0, currentRightSpeed;
    [HideInInspector]
    public bool isActive = true;
    CharacterController character;

    private void Start()
    {
        character = GetComponent<CharacterController>();
        MoveSpeed += Random.Range(0f, 1f);
        MaxRightSpeed += Random.Range(0f, 1f);
        StartCoroutine(setRandomSpeed());
    }

    private void Update()
    {
        if (isActive)
        {
            // smooth move changes
            if (currentRightSpeed != setRightSpeed)
                currentRightSpeed += (setRightSpeed - currentRightSpeed) * Time.deltaTime;
            // mesh rotation
            transform.rotation = Quaternion.Euler( 
                transform.rotation.x, 
                (-currentRightSpeed / MoveSpeed * 45) + 180, 
                transform.rotation.z);

            Vector3 mov = Vector3.back * MoveSpeed + Vector3.right * currentRightSpeed - Vector3.up * 3;
            character.Move(mov * Time.deltaTime);

        }
    }

    IEnumerator setRandomSpeed()
    {
        while (isActive)
        {
            setRightSpeed = Random.Range(-MaxRightSpeed, MaxRightSpeed);
            yield return new WaitForSeconds(Random.Range(.5f, 2f));
        }
    }
}
