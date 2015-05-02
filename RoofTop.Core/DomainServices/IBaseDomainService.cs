using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoofTop.Core.DomainServices
{
    /// <summary>
    /// Base repository for Domain Services
    /// </summary>
    /// <typeparam name="TEntity">Entity</typeparam>
    /// <typeparam name="TKey">Type of TEntity's Primary Key</typeparam>
    public interface IBaseDomainService<TEntity, TKey>
    {
        TEntity GetById(TKey id);
        IQueryable<TEntity> GetAll();
        int Add(TEntity ad);
        bool Delete(TKey id);
        bool Attach(TEntity ad);
    }
}
