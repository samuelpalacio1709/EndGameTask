using UnityEngine;

[CreateAssetMenu(fileName = "Character Settings", menuName = "Data/ new Character Settings", order = 1)]

public class CharacterSettings : ScriptableObject
{
    public string characterName;
    public float totalHealth;
    public float attackTime;
}
