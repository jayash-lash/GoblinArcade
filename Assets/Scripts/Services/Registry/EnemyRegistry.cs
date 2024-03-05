using System.Collections;
using System.Collections.Generic;
using Enemy;

namespace Services.Registry
{
    public class EnemyRegistry : IEnumerable<BaseEnemy>
    {
        private List<BaseEnemy> _enemies = new ();

        public BaseEnemy this[int index]
        {
            get => _enemies[index];
            set => _enemies[index] = value;
        }
        
        public void Add(BaseEnemy baseEnemy) => _enemies.Add(baseEnemy);

        public void Remove(BaseEnemy baseEnemy) => _enemies.Remove(baseEnemy);
        
        public IEnumerator<BaseEnemy> GetEnumerator()
        {
            foreach (var enemy in _enemies)
            {
                yield return enemy;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}