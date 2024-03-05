using Enemy.Enum;

namespace Enemy
{
    public class SimpleEnemy : BaseEnemy
    {
        public override EnemyType EnemyType => EnemyType.SimpleEnemy;
    }
}