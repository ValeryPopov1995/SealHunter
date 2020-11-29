using System.Collections;
using UnityEngine;

public class EnemyMovenment : MonoBehaviour
{
    public float MoveSpeed = 5;
    public float MaxRightSpeed = 3;

    float randomRightSpeed = 0;
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
            Vector3 mov = Vector3.back * MoveSpeed + Vector3.right * randomRightSpeed - Vector3.up * 3;
            character.Move(mov * Time.deltaTime);
        }
    }

    IEnumerator setRandomSpeed()
    {
        while (isActive)
        {
            float right = MaxRightSpeed;
            randomRightSpeed = Random.Range(-right, right);
            yield return new WaitForSeconds(Random.Range(.5f, 2f));
        }
    }
}
