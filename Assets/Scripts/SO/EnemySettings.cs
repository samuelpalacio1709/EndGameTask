using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Settings", menuName = "Data/ new Enemy Settings", order = 2)]

public class EnemySettings : ScriptableObject
{
    public string enemyName;
    public float totalHealth;
    public float attackTime;
    public float movementSpeed;
    public float minAttackTime;
    public float maxAttackTime;
}
