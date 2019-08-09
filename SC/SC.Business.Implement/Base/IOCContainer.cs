
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace SC.Business.Implement.Base
{
    public class IOCContainer
    {
        private static volatile IOCContainer instance;

        private static object syncRoot;

        private IUnityContainer container = null;

        public static IOCContainer Instance
        {
            get
            {
                if (IOCContainer.instance == null)
                {
                    lock (IOCContainer.syncRoot)
                    {
                        if (IOCContainer.instance == null)
                        {
                            IOCContainer.instance = new IOCContainer();
                        }
                    }
                }
                return IOCContainer.instance;
            }
        }

        static IOCContainer()
        {
            IOCContainer.syncRoot = new object();
        }

        private IOCContainer()
        {
            UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            this.container = (new UnityContainer()).LoadConfiguration(section);
        }

        public T Resolve<T>()
        {
            return this.container.Resolve<T>(new ResolverOverride[0]);
        }
    }
}