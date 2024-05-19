using System.Collections;
using UnityEngine;

[RequireComponent(typeof(HealthView))]
public class HealthController : MonoBehaviour
{
    [SerializeField] private float timeToStartHealing = 4;
    [SerializeField] private float timeSteepHealing = 0.3f;
    private HealthView healthView;
    private float health = 0;
    private float totalHealth = 0;

    IGameEntity gameEntity;
    private Coroutine IncreaseHealthCoroutine;
    public IEntitySettings CharacterSettings { get => gameEntity.GetSettings(); }
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
        gameEntity = GetComponentInParent<IGameEntity>();
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
        if (IncreaseHealthCoroutine == null)
        {
            IncreaseHealthCoroutine = StartCoroutine(IncreaseHealth());
        }


    }

    private IEnumerator IncreaseHealth()
    {
        yield return new WaitForSeconds(timeToStartHealing);

        var increaseFactor = CharacterSettings.GetHealthIncreaseFactor();
        while (Health < CharacterSettings.GetTotalHealth())
        {
            yield return new WaitForSeconds(timeSteepHealing);
            Health += increaseFactor;
        }
        Health = CharacterSettings.GetTotalHealth();
        IncreaseHealthCoroutine = null;
    }

}