using MamApi.Data;
using System;
using System.Linq;
using System.Collections.Generic;
using MamApi.Models;

namespace MamApi.Services
{
    public class SalesmanRepository : ISalesmanRepository
    {
        private readonly HPPRODb _context;

        public SalesmanRepository(HPPRODb context)
        {
            _context = context;
        }

        public IEnumerable<Salesman> All() {
            var listSalesman = _context.Salesman.ToList();
            
            return listSalesman;
        }

        public Salesman GetSalesman(string salesmanId)
        {
            var salesman = _context.Salesman.
                Where(s => s.MSMN_ID == salesmanId).
                SingleOrDefault();

            return salesman;
        }

        //public IQueryable<T> All() => _context.Set<T>().AsQueryable();
    }
}
