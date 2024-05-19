using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] GameObject gunPrefab;
    [SerializeField] Transform attach;
    [SerializeField] ParticleSystem bulletPS;
    [SerializeField] Transform bulletsParent;
    private Weapon weapon;
    void Awake()
    {
        var weaponGameobject = Instantiate(gunPrefab, attach);
        weaponGameobject.TryGetComponent<Weapon>(out weapon);
    }

    private void OnEnable()
    {
        CharacterInput.onInputAttack += Fire;
    }

    private void Update()
    {
        var newPos = new Vector3(weapon.muzzle.position.x, bulletsParent.transform.position.y, weapon.muzzle.position.z);
        //bulletsParent.transform.position = newPos;
    }

    private void Fire(CharacterAttackState state, Vector3 vector)
    {
        if (state == CharacterAttackState.Attack)
        {
            bulletPS.Play();
        }
    }


}
