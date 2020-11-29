using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Engineer : MonoBehaviour
{
    public int MinTimer = 10, AddRandomTimer = 5;
    public GameObject BaseStationPrefab;
    public Transform InstPoint;

    Animator anim;
    EnemyMovenment movenment;

    private void Start()
    {
        anim = GetComponent<Animator>();
        movenment = GetComponent<EnemyMovenment>();
        MinTimer += Random.Range(0, AddRandomTimer);
        StartCoroutine(StartBuild());
    }

    IEnumerator StartBuild()
    {
        yield return new WaitForSeconds(MinTimer);
        // anim.SetTrigger("startbuild");
        Instantiate(BaseStationPrefab, InstPoint.position, InstPoint.rotation);
    }
}
