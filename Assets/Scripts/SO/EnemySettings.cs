using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Settings", menuName = "Data/ new Enemy Settings", order = 2)]

public class EnemySettings : CharacterSettings
{
    public float minAttackTime;
    public float maxAttackTime;
}
