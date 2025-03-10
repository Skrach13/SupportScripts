using System;
using System.Collections.Generic;

namespace DI
{
    public class DIContainer
    {
        private readonly DIContainer _parentContainer;
        private readonly Dictionary<(string, Type), DIRegistration> _registration = new();
        private readonly HashSet<(string, Type)> _resolutions = new();

        public DIContainer(DIContainer parentContainer = null)
        {
            _parentContainer = parentContainer;
        }

        public void RegisterSingleton<T>(Func<DIContainer, T> factory)
        {
            RegisterSingleton(null, factory);
        }

        public void RegisterSingleton<T>(string tag, Func<DIContainer, T> factory)
        {
            var key = (tag, typeof(T));

            Register(key, factory, true);
        }
        public void RegisterTransient<T>(Func<DIContainer, T> factory)
        {
            RegisterTransient(null, factory);
        }

        public void RegisterTransient<T>(string tag, Func<DIContainer, T> factory)
        {
            var key = (tag, typeof(T));

            Register(key, factory, false);
        }

        public void RegisterInstance<T>(T instance)
        {
            RegisterInstance(null, instance);
        }

        public void RegisterInstance<T>(string tag, T instance)
        {
            var key = (tag, typeof(T));

            if (_registration.ContainsKey(key))
            {
                throw new Exception(message: $"DI: Dactore whit tag {key.Item1} and type {key.Item2.FullName} has already register");
            }

            _registration[key] = new DIRegistration
            {
                Instance = instance,
                IsSingleton = true
            };
        }

        public T Resolve<T>(string tag = null)
        {
            var key = (tag, typeof(T));


            if (_resolutions.Contains(key))
            {
                throw new Exception(message: $"Cyclic dependency for tag {key.tag} and type {key.Item2.FullName}");
            }

            _resolutions.Add(key);

            try
            {
                if (_registration.TryGetValue(key, out var registration))
                {
                    if (registration.IsSingleton)
                    {
                        if (registration.Instance == null && registration.Factory != null)
                        {
                            registration.Instance = registration.Factory(this);
                        }
                        return (T)registration.Instance;
                    }
                    return (T)registration.Factory(this);
                }

                if (_parentContainer != null)
                {
                    return _parentContainer.Resolve<T>(tag);
                }
            }
            finally 
            {
                _resolutions.Remove(key);
            }

            throw new Exception(message: $"Could't find dependcy for tag{tag} and type {key.Item2.FullName}");
        }

        private void Register<T>((string, Type) key, Func<DIContainer, T> factory, bool isSinglenton)
        {
            if (_registration.ContainsKey(key))
            {
                throw new Exception(message: $"DI: Dactore whit tag {key.Item1} and type {key.Item2.FullName} has already register");
            }

            _registration[key] = new DIRegistration
            {
                Factory = c => factory(c),
                IsSingleton = isSinglenton
            };
        }

    }
}