using AccesoDatos.Productos;
using LogicaNegocio.Login;
using LogicaNegocio.Productos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Xml.Linq;
using Transversal.DependencyResolver;
using Transversal.Entidades;
using Transversal.Interfaces;

namespace LogicaNegocio
{
    [Export(typeof(Transversal.DependencyResolver.IComponent))]
    public class DependencyResolver : Transversal.DependencyResolver.IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<ILogicaNegocioProducto, Productos_LN>();            
            registerComponent.RegisterType<ILogicaNegocioLogin, Login_LN>();            
        }
    }
}
