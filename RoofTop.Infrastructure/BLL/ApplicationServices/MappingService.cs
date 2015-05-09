using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoofTop.Core.ApplicationServices;

namespace RoofTop.Infrastructure.BLL.ApplicationServices
{
    /// <summary>
    /// Concrete implementation using Automapper
    /// </summary>
    public class MappingService: IMappingService
    {
        /// <summary>
        /// This will call Automapper's Mapper.Map method
        /// Config needs to be setup
        /// </summary>
        /// <typeparam name="TSrc"></typeparam>
        /// <typeparam name="TDest"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public virtual TDest Map<TSrc, TDest>(TSrc source) where TDest : class
        {
            return AutoMapper.Mapper.Map<TSrc, TDest>(source);
        }
    }
}
