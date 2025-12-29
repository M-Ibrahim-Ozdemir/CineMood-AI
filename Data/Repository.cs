using CineMoodAI.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMoodAI.Infrastructure.Data
{   //nasıl calıstıgı ile ilgili interfaceyi implemente eden ilgilenir yani bu oburu degil
    //IRepository<T>  interfesini impelemente ediuor yani
    public class Repository<T>: IRepository<T> where T : class  //Repository<T> -> T repositoryi cagırırken tipini vercez
    {
        protected readonly CineMoodAIDbContext _context;  //buralar kısa gecti bak sonra
        public Repository(CineMoodAIDbContext context)
        {
            _context = context;
        }
        public async Task<List<T>> GetAllAsync() => await _context.Set<T>().ToListAsync(); //conetexi alır,.set yani bu tabloya set et . mesela T yerine recomandation.Log geldi _context.Set<recomandation.Log> . yani recomandation log tablosunu ayarla .tolist-> tablodaki tum kayıtları getir 
        public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id); //FindAsync(id); id si su olan kaydı getir
        public async Task AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);//AddAsync(entity); ya da su entityyi al ekle
        public void Remove(T entity) =>  _context.Set<T>().Remove(entity);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

//entity framework lasyloding calısır . yani ekle , sil falan diyince veritabanına gitmez. koldarı hazırlar arkada tutar ne zaman sacechange dersen o zman gider. yada 
// yada nezman dataya ihtiyaz duyun bana bu datayı ver dedin trust dedin ozaman veritananına gider datayı alır gelir.
// o yuzden her degisiklikten sonra save change yi cagımak zorundayız

    }
}
