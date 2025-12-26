using UnityEngine;

public class BothTrigger : MonoBehaviour
{
    public GameObject healthBar;
    // Start is called before the first frame update
   void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            healthBar.gameObject.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            healthBar.gameObject.SetActive(false);
    }

}
