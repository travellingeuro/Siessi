using Siessi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Siessi.Services
{
    public class ConsentDataStore : IDataStore<Consent>
    {
        readonly List<Consent> consents;

        public ConsentDataStore()
        {

            consents = new List<Consent>() 
            {new Consent{ Name="Arturo", Model="Rover"} };
        }

        public Task<bool> AddItemAsync(Consent item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Consent> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Consent>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(consents);
        }

        public Task<bool> UpdateItemAsync(Consent item)
        {
            throw new NotImplementedException();
        }
    }
}
