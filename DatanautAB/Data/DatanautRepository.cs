using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DatanautAB.Data
{
    public class DatanautRepository
    {
        private readonly DatanautContext _context;

        public DatanautRepository(DatanautContext context)
        {
            _context = context;
        }
    }
}
