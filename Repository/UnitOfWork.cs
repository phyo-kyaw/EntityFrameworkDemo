﻿using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
        }
        public IUserRepository Users { get; private set; }

        public int Complete()
        {
            if (_context != null)
                return _context.SaveChanges();
            return 0;
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }
    }
}
