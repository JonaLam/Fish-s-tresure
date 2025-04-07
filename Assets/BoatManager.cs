using UnityEngine;

public class BoatManager : MonoBehaviour
{
    public void DiveIn() 
    {
        GameManager.instance.PrepareDive();
        GameManager.instance.GoDeeper();
    }
}
