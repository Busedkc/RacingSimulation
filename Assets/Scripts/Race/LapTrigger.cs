using UnityEngine;

public class LapTrigger : MonoBehaviour
{
   [SerializeField] int maxLaps = 0;
   int currentLapsCompleted;

   private void OnTriggerEnter(Collider other)
   {
    if(other.gameObject.tag.Equals("Player"))
    {
        currentLapsCompleted++;
        if(currentLapsCompleted <= maxLaps)
        {
            Events.LapCompleted?.Invoke(currentLapsCompleted);
        } else
        {
            Events.RaceCompleted?.Invoke();
        }
    }
   }
}
