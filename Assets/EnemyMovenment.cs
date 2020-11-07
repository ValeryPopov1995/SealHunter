using System.Collections;
using UnityEngine;

public class EnemyMovenment : MonoBehaviour
{
    public int MoveSpeed = 5;
    public int MaxRightSpeed = 3;

    float randomRightSpeed = 0;
    [HideInInspector]
    public bool isActive = true;
    CharacterController character;

    private void Start()
    {
        character = GetComponent<CharacterController>();

        StartCoroutine(setRandomSpeed());
    }

    private void Update()
    {
        Vector3 mov = Vector3.back * MoveSpeed + Vector3.right * randomRightSpeed - Vector3.up * 3;
        character.Move(mov * Time.deltaTime);
    }

    IEnumerator setRandomSpeed()
    {
        while (isActive)
        {
            // if on edge of level !
            float right = MaxRightSpeed;
            randomRightSpeed = Random.Range(-right, right);
            yield return new WaitForSeconds(Random.Range(.5f, 2f));
        }
    }
}
