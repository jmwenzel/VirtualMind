using System.Threading.Tasks;

namespace VirtualMind.WebAPI.Infrastructure.Data
{
    /// <summary>
    /// Base DB methods
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : BaseRecordDbEntity
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int?> Insert(T model);
    }
}
