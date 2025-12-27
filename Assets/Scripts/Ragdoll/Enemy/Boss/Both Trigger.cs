using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public BossAI bossAI;
    public GameObject healthbar;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bossAI.SetPlayerInArena(true);
            if (healthbar) healthbar.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bossAI.SetPlayerInArena(false);
            if (healthbar) healthbar.SetActive(false);
        }
    }
}
