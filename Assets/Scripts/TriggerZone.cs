using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TriggerZone : MonoBehaviour
{
    public enum mode { DefeatZone, NearDefeatZone}
    public mode TriggerMode = mode.NearDefeatZone;
    public LayerMask EnemyLayer;

    private void OnTriggerStay(Collider other)
    {
        if (TriggerMode == mode.NearDefeatZone && other.GetComponent<EnemyMovenment>() != null) GameManager.NearDefeat = true;
        else GameManager.NearDefeat = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (TriggerMode == mode.DefeatZone && other.GetComponent<EnemyMovenment>() != null) GameManager.Defeat = true;
    }
}
