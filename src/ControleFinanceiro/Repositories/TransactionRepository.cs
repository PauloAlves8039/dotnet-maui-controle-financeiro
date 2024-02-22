using ControleFinanceiro.Repositories.Interfaces;
using LiteDB;
using ControleFinanceiro.Models;

namespace ControleFinanceiro.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly LiteDatabase _database;
        private readonly string _collectionName = "transactions";

        public TransactionRepository(LiteDatabase database)
        {
            _database = database;
        }

        public List<Transaction> GetAll()
        {
            return _database
                .GetCollection<Transaction>(_collectionName)
                .Query()
                .OrderByDescending(a => a.Date)
                .ToList();
        }

        public void Add(Transaction transaction)
        {
            var collection = _database.GetCollection<Transaction>(_collectionName);
            collection.Insert(transaction);
            collection.EnsureIndex(a => a.Date);
        }

        public void Update(Transaction transaction)
        {
            var collection = _database.GetCollection<Transaction>(_collectionName);
            collection.Update(transaction);
        }

        public void Delete(Transaction transaction)
        {
            var collection = _database.GetCollection<Transaction>(_collectionName);
            collection.Delete(transaction.Id);
        }
    }
}
