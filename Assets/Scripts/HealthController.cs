using UnityEngine;

[RequireComponent(typeof(HealthView))]
public class HealthController : MonoBehaviour
{
    private HealthView healthView;
    private float health = 0;
    private float totalHealth = 0;

    public IEntitySettings CharacterSettings { get => GetComponentInParent<IGameEntity>().GetSettings(); }
    public float Health
    {
        get => health;
        set
        {

            health = value;
            healthView.SetHealth(health, totalHealth);

        }
    }

    private void Awake()
    {
        var settings = CharacterSettings.GetBasicCharacterInfo();
        healthView = GetComponent<HealthView>();
        totalHealth = settings.Item2;
        health = totalHealth;
        healthView.SetCharacter(settings.Item1, CharacterSettings.GetColor());
        healthView.SetHealth(Health, settings.Item2);
    }

    public void HandleDamage(float damage)
    {

        Health -= damage;
    }
}
