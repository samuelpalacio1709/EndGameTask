using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private TMP_Text healthLabel;
    [SerializeField] private TMP_Text characterLabel;
    [SerializeField] private ParticleSystem deathPs;

    private void Awake()
    {
        deathPs.transform.SetParent(null);
    }
    public void SetHealth(float health, float totalHealth)
    {
        fillImage.fillAmount = health / totalHealth;
        healthLabel.text = health.ToString();

    }

    public void SetCharacter(string character, Color color)
    {
        characterLabel.text = character;
        characterLabel.color = color;
        fillImage.color = color;
    }

    public void ShowDeathParticles(Vector3 deathPosition)
    {
        deathPs.gameObject.transform.position = deathPosition;
        deathPs.Play();
    }
}
