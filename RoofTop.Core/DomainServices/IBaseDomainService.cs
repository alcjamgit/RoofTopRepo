using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace RoofTop.Core.DomainServices
{
    /// <summary>
    /// Base repository for Domain Services
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    /// <typeparam name="TKey">Type of TEntity's Primary Key</typeparam>
    public interface IBaseDomainService<T> where T: class
    {
        IQueryable<T> GetAll();
        IQueryable<T> Find(Expression<Func<T ,bool>> predicate);
        T Add(T entity);
        T Delete(T entity);
        void Edit(T entity);
        int Commit();
    }
}
