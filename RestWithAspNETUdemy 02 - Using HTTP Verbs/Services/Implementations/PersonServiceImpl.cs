using System.Collections.Generic;
using System.Threading;
using RestWithAspNETUdemy.Model;

namespace RestWithAspNETUdemy.Services.Implementations
{
    public class PersonServiceImpl : IPersonService
    {
        private volatile int count;

        public Person Create(Person person)
        {
            return person;
        }

        public void Delete(long id)
        {

        }

        public List<Person> FindAll()
        {
            var persons = new List<Person>();
            for (int i = 0; i < 8; i++)
            {
                var person = MockPerson(i);

                persons.Add(person);
            }

            return persons;
        }

        public Person FindById(long id)
        {
            return new Person
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Address = "New York - New York - US",
                Gender = "Male"
            };
        }

        public Person Update(Person person)
        {
            return person;
        }

        private Person MockPerson(int index)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                FirstName = "Person Name " + index,
                LastName = "Person Last Name " + index,
                Address = "Some Address " + index,
                Gender = "Male " + index
            };
        }

        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }
    }
}