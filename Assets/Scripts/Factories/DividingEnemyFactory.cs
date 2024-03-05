using Enemy;
using Enemy.Enum;
using Services.Pool;

namespace Factories
{
    public class DividingEnemyFactory : BaseObjectFactory<DividingEnemy>
    {
        protected override EnemyType EnemyType => EnemyType.DivideEnemy;
    }
}