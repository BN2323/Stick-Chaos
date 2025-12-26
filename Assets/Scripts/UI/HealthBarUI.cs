using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Image fillImage;
    public Health target;

    void Update()
    {
        if (!target) return;

        fillImage.fillAmount =
            (float)target.GetCurrent() / target.maxHealth;
    }
}
