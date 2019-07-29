using System.Collections.Generic;
using RestWithAspNETUdemy.Model;
using RestWithAspNETUdemy.Repository;
using RestWithAspNETUdemy.Repository.Generic;

namespace RestWithAspNETUdemy.Business.Implementations
{
    public class BookBusinessImpl : IBookBusiness
    {
        private IRepository<Book> _repository;

        public BookBusinessImpl(IRepository<Book> repository) => _repository = repository;

        public Book Create(Book book) => _repository.Create(book);

        public void Delete(long id) => _repository.Delete(id);

        public List<Book> FindAll() => _repository.FindAll();

        public Book FindById(long id) => _repository.FindById(id);

        public Book Update(Book book) => _repository.Update(book);
    }
}