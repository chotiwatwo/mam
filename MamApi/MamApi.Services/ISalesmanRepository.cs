using MamApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MamApi.Services
{
    public interface ISalesmanRepository
    {
        IEnumerable<Salesman> All();

        Salesman GetSalesman(string salesmanId);
    }
}
