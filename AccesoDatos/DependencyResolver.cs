using AccesoDatos.Login;
using AccesoDatos.Productos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Xml.Linq;
using Transversal.DependencyResolver;
using Transversal.Entidades;
using Transversal.Interfaces;

namespace AccesoDatos
{
    [Export(typeof(Transversal.DependencyResolver.IComponent))]
    public class DependencyResolver : Transversal.DependencyResolver.IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IAccesoDatosProducto, Productos_DA>();
            registerComponent.RegisterType<IAccesoDatosLogin, Login_DA>();
        }
    }
}
