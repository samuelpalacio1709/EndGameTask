using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject gunPrefab;
    [SerializeField] private Transform attach;
    [SerializeField] private ParticleSystem bulletPS;
    [SerializeField] private ParticleSystem damagePS;
    [SerializeField] private Transform bulletsParent;
    [SerializeField] private LayerMask layer;

    private IGameEntity senderEntity;
    void Awake()
    {
        Instantiate(gunPrefab, attach);
        var collision = bulletPS.collision;
        collision.collidesWith = layer;
        senderEntity = GetComponentInParent<IGameEntity>();
    }

    public void Fire(CharacterAttackState state, Vector3 vector)
    {
        if (state == CharacterAttackState.Attack)
        {
            bulletPS.Play();
        }
    }


    public void SendDamage(GameObject touchedObject)
    {
        IGameEntity reciverEntity;
        if (touchedObject.TryGetComponent(out reciverEntity))
        {
            reciverEntity.RecieveDamage(senderEntity.GetSettings().GetDamageValue());
            ShowDamage(touchedObject);

        }
    }

    private void ShowDamage(GameObject touchedObject)
    {
        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        int totalCollisions = bulletPS.GetCollisionEvents(touchedObject, collisionEvents);

        for (int i = 0; i < totalCollisions; i++)
        {
            Vector3 pointOfContact = collisionEvents[i].intersection;

            Debug.Log(pointOfContact);
        }
    }


}
