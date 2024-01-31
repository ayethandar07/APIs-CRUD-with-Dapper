using DapperDemoData.Data;
using DapperDemoData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDemoData.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IDataAccess _db;

        public PersonRepository(IDataAccess db)
        {
            _db = db;
        }

        public async Task<bool> AddPerson(Person person)
        {
            try
            {
                string query = "insert into dbo.person(name,email) values(@Name, @Email)";
                await _db.SaveData(query, new { Name = person.Name, Email = person.Email });

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeletePerson(int id)
        {
            try
            {
                string query = "delete from dbo.person where id= @Id";
                await _db.SaveData(query, new { Id = id });

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Person>> GetPeople()
        {
            string query = "select * from dbo.person";
            var peopel = await _db.GetData<Person, dynamic>(query, new { });

            return peopel;
        }

        public async Task<Person> GetPersonById(int id)
        {
            string query = "select * from dbo.person where id = @Id";
            IEnumerable<Person> peopel = await _db.GetData<Person, dynamic>(query, new { Id = id });

            return peopel.FirstOrDefault();
        }

        public async Task<bool> UpdatePerson(Person person)
        {
            try
            {
                string query = "update dbo.person set name=@Name, email=@Email where id=@Id";
                await _db.SaveData(query, person);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
