using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public Transform muzzle;
    [SerializeField] private MeshRenderer weaponRenderer;


    public void SetSkin(Material skin)
    {
        weaponRenderer.material = skin;
    }
}
