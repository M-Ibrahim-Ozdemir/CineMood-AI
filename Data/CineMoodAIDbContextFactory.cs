using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMoodAI.Infrastructure.Data
{
    public class CineMoodAIDbContextFactory: IDesignTimeDbContextFactory<CineMoodAIDbContext>
    {
        public CineMoodAIDbContext CreateDbContext(string[] args) //createcontext cağırıldıgında
        {
            var configuration = new ConfigurationBuilder() //configureationlara erişmek için kullnacagimiz kutuphane
                .SetBasePath(Directory.GetCurrentDirectory()) //git şuan bu uygulamanın calistigi klasoru bul. burayı basepath olarak isaretle, 
                .AddJsonFile("appsettings.json") //buraya bir json dosyasi ekle appsettings 
                .Build(); //ve bunu build et 

            var optionsBuilder = new DbContextOptionsBuilder<CineMoodAIDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new CineMoodAIDbContext(optionsBuilder.Options);


            //simdi bunu web api tarafina tanitmamiz lazim
        } 
    }
}
