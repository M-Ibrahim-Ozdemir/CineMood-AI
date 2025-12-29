using CineMoodAI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMoodAI.Aplication.interfaces
{
    public interface ITokenService       //bole bir interfacemiz var ve bu interfaceyi inplemente eden classlarda CreateToken diye metot Olsun
    {
        string CreateToken(AppUser user);
    }
}
