using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CtrlBox.Domain.Interfaces.Application
{
    public interface IApplicationServiceBase<T> where T : class
    {
        ICollection<T> GetAll();

        Task<ICollection<T>> GetAllAsync();

        T GetById(int id);

        Task<T> GetByIdAsync(int id);

        T GetByUniqueId(string id);

        Task<T> GetByUniqueIdAsync(string id);

        T Add(T entity);

        Task<T> AddAsync(T entity);

        T Update(T updated);

        Task<T> UpdateAsync(T updated);

        void Delete(T t);

        Task<int> DeleteAsync(T t);
    }
}
