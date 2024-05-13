using CharlotteSchou_Booking.Persistens;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharlotteSchou_Booking.Model.Persistens
{
    //abstract class that other classes inherit from
    public abstract class Repo <T>
    {
        public string? ConnectionString;
        public abstract void InitializeRepository();

        public abstract T Load(int id);

        public abstract void Remove(int id);

        public abstract List <T> LoadAll();

     
        
    }
}
