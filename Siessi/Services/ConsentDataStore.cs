using MvvmHelpers;
using Siessi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siessi.Services
{
    public class ConsentDataStore : BaseViewModel, IDataStore<Consent>
    {
        public List<Consent> Consents { get; }

        public ConsentDataStore()
        {
            var a = new DataService();
            Consents = a.GetConsents();
        }

        public Task<bool> AddItemAsync(Consent item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Consent> GetItemAsync(string id)
        {
            return await Task.FromResult(Consents.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Consent>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(Consents);
        }

        public Task<bool> UpdateItemAsync(Consent item)
        {
            throw new NotImplementedException();
        }
    }
}
