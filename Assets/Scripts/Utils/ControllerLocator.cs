using System;
using System.Collections.Generic;

namespace Utils
{
    public static class ControllerLocator
    {
        private static Dictionary<Type, object> services = new();

        public static void RegisterService(object service)
        {
            services[service.GetType()] = service;
        }

        public static T GetService<T>()
        {
            return (T)services[typeof(T)];
        }

        public static Dictionary<Type, object> GetServices()
        {
            return services;
        }
    }
}