using UnityEngine;

[CreateAssetMenu(fileName = "Character Settings", menuName = "Data/ new Character Settings", order = 1)]

public class CharacterSettings : ScriptableObject
{
    public string characterName;
    public float totalHealth;
    public float attackTime;
    public float movementSpeed;
    public float turnSpeed;
    public Vector3 rotationOffsetOnAttack = Vector3.zero;
    public Vector3 rotationOffset = Vector3.zero;
}
