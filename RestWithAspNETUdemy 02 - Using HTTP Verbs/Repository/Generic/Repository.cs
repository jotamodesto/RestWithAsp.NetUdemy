using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RestWithAspNETUdemy.Model.Base;
using RestWithAspNETUdemy.Model.Context;

namespace RestWithAspNETUdemy.Repository.Generic
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private MySQLContext _context;
        private DbSet<T> _dataset;

        public Repository(MySQLContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }

        public T Create(T item)
        {
            try
            {
                _dataset.Add(item);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return item;
        }

        public void Delete(long id)
        {
            var result = _dataset.SingleOrDefault(i => i.Id == id);

            try
            {
                if (result != null)
                {
                    _dataset.Remove(result);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T Update(T item)
        {
            if (!Exists(item.Id)) return null;

            var result = _dataset.SingleOrDefault(i => i.Id == item.Id);

            try
            {
                _context.Entry(result).CurrentValues.SetValues(item);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return item;
        }

        public bool Exists(long id) => _dataset.Any(i => i.Id == id);

        public List<T> FindAll() => _dataset.ToList();

        public T FindById(long id) => _dataset.SingleOrDefault(i => i.Id == id);
    }
}