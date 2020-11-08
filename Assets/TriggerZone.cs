using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public enum mode { DefeatZone, NearDefeatZone}
    public mode TriggerMode = mode.NearDefeatZone;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<EnemyMovenment>() != null) // зашел противник
        {
            switch (TriggerMode)
            {
                case mode.DefeatZone:
                    GameManager.Finish(GameManager.finishMode.Defeat);
                    break;
                case mode.NearDefeatZone:
                    GameManager.NearDefeat = true;
                    break;
                default:
                    break;
            }
        }
        else GameManager.NearDefeat = false;
    }
}
