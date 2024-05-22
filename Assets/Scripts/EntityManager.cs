using System;
using UnityEngine;

[RequireComponent(typeof(CharacterAnimation))]
[RequireComponent(typeof(PlayerMovement))]
public class EntityManager : MonoBehaviour, IGameEntity
{
    [SerializeField] protected CharacterSettings settings;
    [SerializeField] protected HealthController healthController;
    protected CharacterMovement characterMovement;
    protected CharacterAnimation characterAnimation;
    public static Action<string> onEntityKilled;
    private void Awake() => Init();
    public virtual void Init()
    {
        characterMovement = GetComponent<PlayerMovement>();
        characterAnimation = GetComponent<CharacterAnimation>();
    }

    public void RecieveDamage(IGameEntity entity)
    {
        if (healthController != null)
        {
            var resultedHealth = healthController.HandleDamage(entity.GetSettings().GetDamageValue());
            if (resultedHealth <= 0)
            {
                entity.Kill(this);

            }
        }
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void Respawn()
    {
        healthController.ResetHealth();
        if (SpawnManager.Instance != null)
        {
            healthController.ResetHealth();
            SpawnManager.Instance.SpawnEntity((gameObject.transform));

        }
    }

    public IEntitySettings GetSettings()
    {
        return settings;
    }

    /// <summary>
    /// Called whether a player kills an enemy or vice versa
    /// </summary>
    /// <param name="entity"></param>
    public void Kill(IGameEntity entity)
    {
        string info = this.GetSettings().GetName() + " has killed " + entity.GetSettings().GetName();
        onEntityKilled?.Invoke(info);
        entity.Respawn();

    }
}
