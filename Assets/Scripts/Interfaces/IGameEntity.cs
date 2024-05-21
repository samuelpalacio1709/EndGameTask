
using UnityEngine;

public interface IGameEntity

{
    public Vector3 GetPosition();
    public void RecieveDamage(float damage);
    public IEntitySettings GetSettings();

    public void Respawn();
}
