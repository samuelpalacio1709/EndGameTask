
using UnityEngine;

public interface IGameEntity

{
    public Vector3 GetPosition();
    public void RecieveDamage(IGameEntity entity);
    public IEntitySettings GetSettings();

    public void Respawn();
    public void Kill(IGameEntity entity);
}
