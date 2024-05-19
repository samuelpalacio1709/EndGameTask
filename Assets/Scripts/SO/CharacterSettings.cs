using UnityEngine;

[CreateAssetMenu(fileName = "Character Settings", menuName = "Data/ new Character Settings", order = 1)]

public class CharacterSettings : ScriptableObject, IEntitySettings
{
    public string characterName;
    public float totalHealth;
    public float attackTime;
    public float damage;
    public float movementSpeed;
    public float turnSpeed;
    public float increaseHealthFactor;
    public Vector3 rotationOffsetOnAttack = Vector3.zero;
    public Vector3 rotationOffset = Vector3.zero;
    public Color color;
    public (string, float) GetBasicCharacterInfo()
    {
        return (this.characterName, this.totalHealth);
    }
    public Color GetColor()
    {
        return color;
    }

    public float GetDamageValue()
    {
        return damage;
    }

    public float GetHealthIncreaseFactor()
    {
        return increaseHealthFactor;
    }

    public float GetTotalHealth()
    {
        return totalHealth;
    }
}


public interface IEntitySettings
{
    public (string, float) GetBasicCharacterInfo();
    public Color GetColor();

    public float GetDamageValue();
    public float GetHealthIncreaseFactor();
    public float GetTotalHealth();



}