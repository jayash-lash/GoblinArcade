using Enemy;
using Enemy.Enum;

namespace Factories
{
    public class MiniEnemyFactory : BaseObjectFactory<MiniEnemy>
    {
        protected override EnemyType EnemyType => EnemyType.MiniEnemy;
    }
}