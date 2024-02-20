using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.Entidades;
using Transversal.Interfaces;
using Transversal.LogErrores;
using Transversal.Paginacion;

namespace Transversal.DependencyResolver
{
    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IAdminLogErrores, AdminLogErrores>();
            registerComponent.RegisterType<IPaginacion<Producto_DTO>, Paginacion<Producto_DTO>>();

           
        }
    }
}
