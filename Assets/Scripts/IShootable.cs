using UnityEngine;

public interface IShootable
{
    public void Hit(int damage);
    public BodyPart GetBodyPart(Vector3 position);
    public bool IsDead();
    public int GetPointForHit();
    public int GetPointForKill();
}

public enum BodyPart
{
    Head,
    Chest,
    Legs,
}