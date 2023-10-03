using System;
using System.Reflection;

namespace SixtyNamesTest.Helpers
{
    public abstract class Singleton<T> where T : Singleton<T>
    {
        #region Constructor

        protected Singleton()
        {
            if (this != _instance && IsOne)
                throw new Exception("Попытка объявить singletone через оператор NEW!!!");

            IsOne = true;
        }

        #endregion

        #region Fields

        private static bool IsOne;
        private static T _instance;
        public static object _syncRoot = new object();

        #endregion

        #region Methods

        public static T GetInstance()
        {
            lock (_syncRoot)
            {
                if (_instance == null)
                {
                    var constructor = typeof(T).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
                    _instance = (T)constructor.Invoke(null);
                }

                return _instance;
            }
        }

        #endregion
    }
}
