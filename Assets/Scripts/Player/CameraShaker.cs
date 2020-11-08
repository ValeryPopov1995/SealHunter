using System.Collections;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Shake(9));
    }

    public IEnumerator Shake(int strength)
    {
        for (int i = strength; i > 0; i--)
        {
            gameObject.transform.localPosition = Vector3.one * Random.Range(0, i) / 10;
            yield return new WaitForSeconds(.05f);
        }
        gameObject.transform.localPosition = Vector3.zero;
    }
}
