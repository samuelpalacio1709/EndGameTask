using UnityEngine;

/// <summary>
/// Handles references to weapon components
/// </summary>
public class Weapon : MonoBehaviour
{
    [SerializeField] public Transform muzzle;
    [SerializeField] private MeshRenderer weaponRenderer;


    public void SetSkin(Material skin)
    {
        weaponRenderer.material = skin;
    }
}
