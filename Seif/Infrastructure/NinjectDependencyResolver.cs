﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Seif.Realize;
using Seif.Repos;

namespace Seif.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            _kernel.Bind<IProducts>().To<DbProducts>().InSingletonScope();
            _kernel.Bind<IAdmin>().To<DbAdmin>().InSingletonScope();
        }
    }
}