using System;
using System.Collections.Generic;

namespace Services.Pool
{
    public class ObjectPool<T>
    {
        private readonly Queue<T> _currentStock;
        private readonly Func<T> _factoryMethod;
        private readonly bool _isDynamic;
        private readonly Action<T> _turnOnCallback;
        private readonly Action<T> _turnOffCallback;

        /// <summary>
        /// <param name="factoryMethod">used in order to be able to create new objects</param>
        /// <param name="turnOnCallback">method to activate our object.</param>
        /// <param name="turnOffCallback">method to deactivate our object</param>
        /// <param name="initialStock">the initial stock using for the Object Pool</param>
        /// <param name="isDynamic">Determines whether the Object Pool is dynamic. If the pool runs out of objects, a dynamic pool will create new objects at runtime. 
        /// If set to false, the pool will return the default value of the type when no objects are available.</param>
        /// </summary>
        public ObjectPool(Func<T> factoryMethod, Action<T> turnOnCallback, Action<T> turnOffCallback,
            int initialStock = 0, bool isDynamic = true)
        {
            _factoryMethod = factoryMethod;
            _isDynamic = isDynamic;

            _turnOffCallback = turnOffCallback;
            _turnOnCallback = turnOnCallback;

            _currentStock = new Queue<T>();

            for (var i = 0; i < initialStock; i++)
            {
                var o = _factoryMethod();
                _turnOffCallback(o);
                _currentStock.Enqueue(o);
            }
        }

        /// <summary>
        /// <param name="factoryMethod">used in order to be able to create new objects</param>
        /// <param name="turnOnCallback">method to activate our object.</param>
        /// <param name="turnOffCallback">method to deactivate our object</param>
        /// <param name="initialStock">the initial stock using for the Object Pool</param>
        /// <param name="isDynamic">Determines whether the Object Pool is dynamic. If the pool runs out of objects, a dynamic pool will create new objects at runtime. 
        /// If set to false, the pool will return the default value of the type when no objects are available.</param>
        /// </summary>
        public ObjectPool(Func<T> factoryMethod, Action<T> turnOnCallback, Action<T> turnOffCallback,
            Queue<T> initialStock, bool isDynamic = true)
        {
            _factoryMethod = factoryMethod;
            _isDynamic = isDynamic;

            _turnOffCallback = turnOffCallback;
            _turnOnCallback = turnOnCallback;

            _currentStock = initialStock;
        }

        /// <summary>
        /// Retrieves an object from the Object Pool. The method requires no parameters and returns an object of type T, which corresponds to the type of objects stored in the pool.
        /// </summary>
        /// <returns>An object of type T retrieved from the Object Pool.</returns>
        public T GetObject()
        {
            T result;
            
            if (_currentStock.Count > 0)
            {
                result = _currentStock.Dequeue();
            }
            else if (_isDynamic)
            {
                result = _factoryMethod();
            }
            else return default;

            _turnOnCallback(result);
            
            return result;
        }

        /// <summary>
        /// Returns an object of type T back to the Object Pool.
        /// </summary>
        /// <param name="o">The object of type T to be returned to the pool.</param>
        public void ReturnObject(T o)
        {
            _turnOffCallback(o);
            _currentStock.Enqueue(o);
        }
    }
}


