using UnityEngine;

public class NextArea : MonoBehaviour
{
    [SerializeField] bool goUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        if (goUp)
            GameManager.instance.GoToTop();
        else
            GameManager.instance.GoDeeper();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
