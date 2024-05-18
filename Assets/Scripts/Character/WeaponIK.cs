using UnityEngine;

public class WeaponIK : MonoBehaviour
{
    public Transform leftHand;
    public Transform weapon;

    void LateUpdate()
    {
        AlignWeapon();
    }

    void AlignWeapon()
    {

        Quaternion targetRotation = Quaternion.LookRotation(leftHand.position - weapon.position);
        weapon.rotation = targetRotation;
    }
}
