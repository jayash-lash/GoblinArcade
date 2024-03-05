using Enemy;
using Enemy.Enum;
using Services.Pool;

namespace Factories
{
    public class SimpleEnemyFactory : BaseObjectFactory<SimpleEnemy>
    {
        protected override EnemyType EnemyType => EnemyType.SimpleEnemy;
    }
}