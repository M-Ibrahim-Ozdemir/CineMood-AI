using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMoodAI.Domain.Interface
{
    //bu interface tamammı bunu n gorevi;  bunu impelemente edenlerde ahanda bu alttakıler olacak diyuo bu kdar . nasıl yapıyo falan ilgilenmez. interfaceyi implemente edenler ilgilenir

    public interface IRepository<T> where T : class   //T c# generik kutuphanesi. kullandık cunku tum tablolarda ortak islemler olacak. GettALL(burun datayi getir), getbyıd, add, remove vb burda var n
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Remove(T entity);
        Task SaveChangesAsync();
    }
}
