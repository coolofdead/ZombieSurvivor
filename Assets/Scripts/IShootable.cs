public interface IShootable
{
    public void Hit(int damage);
    public bool IsDead();
    public int GetPointForHit();
    public int GetPointForKill();
}
