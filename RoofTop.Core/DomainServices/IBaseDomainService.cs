using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoofTop.Core.DomainServices
{
    interface IBaseDomainService<TEntity>
    {
        TEntity Add(TEntity entity);
        TEntity Attach(TEntity entity);
        TEntity Create();
        //TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity;
        //TEntity Find(params object[] keyValues);
        TEntity Remove(TEntity entity);
    }
}
