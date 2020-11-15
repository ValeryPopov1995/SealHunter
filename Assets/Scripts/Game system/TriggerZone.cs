using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TriggerZone : MonoBehaviour
{
    public enum mode { DefeatZone, NearDefeatZone}
    public mode TriggerMode = mode.NearDefeatZone;

    private void OnTriggerStay(Collider other)
    {
        
        if (TriggerMode == mode.NearDefeatZone && other.GetComponent<EnemyMovenment>() != null) 
        {
            GameManager.NearDefeat = true;
            // Debug.Log("near defeat");
        }
        else GameManager.NearDefeat = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (TriggerMode == mode.DefeatZone && other.GetComponent<EnemyMovenment>() != null)
        {
            GameManager.Finish(GameManager.finishMode.Defeat);
            // Debug.Log("defeat");
        }
    }
}
