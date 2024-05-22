using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Controlls the entity weapon
/// </summary>
public class WeaponController : MonoBehaviour
{

    public UnityEvent onFire;
    public UnityEvent onDamage;

    [SerializeField] private GameObject gunPrefab;
    [SerializeField] private Transform attach;
    [SerializeField] private Transform bulletsParent;
    [SerializeField] private LayerMask layer;
    [SerializeField] private Material customMaterial;
    [SerializeField] private bool useCustomMaterial;

    [Header("Visuals")]
    [SerializeField] private ParticleSystem bulletPS;
    [SerializeField] private ParticleSystem damagePS;



    private Weapon weapon;
    private void Start()
    {
        if (customMaterial)
        {
            weapon.SetSkin(customMaterial);
        }
    }

    private IGameEntity senderEntity;
    void Awake()
    {
        Instantiate(gunPrefab, attach).TryGetComponent(out weapon);
        var collision = bulletPS.collision;
        collision.collidesWith = layer;
        senderEntity = GetComponentInParent<IGameEntity>();
        damagePS.gameObject.transform.SetParent(null, false);
    }

    public void Fire(CharacterAttackState state, Vector3 vector)
    {
        if (state == CharacterAttackState.Attack)
        {
            bulletPS.Play();
            onFire?.Invoke();
        }
    }


    public void SendDamage(GameObject touchedObject)
    {
        IGameEntity reciverEntity;
        if (touchedObject.TryGetComponent(out reciverEntity))
        {
            reciverEntity.RecieveDamage(senderEntity);

        }
        ShowDamage(touchedObject);
        onDamage?.Invoke();
    }

    private void ShowDamage(GameObject touchedObject)
    {
        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        int totalCollisions = bulletPS.GetCollisionEvents(touchedObject, collisionEvents);

        for (int i = 0; i < totalCollisions; i++)
        {
            Vector3 pointOfContact = collisionEvents[i].intersection;
            damagePS.transform.position = pointOfContact;
            damagePS.Play();
        }
    }


}
