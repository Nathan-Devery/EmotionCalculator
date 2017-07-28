using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace EmotionCalc2
{
    /// <summary>
    /// Connects to specified Azure DB, makes get and post requests
    /// </summary>
    class DatabaseConnecter<T>
    {
        private MobileServiceClient client;
        private IMobileServiceTable<T> table;
        private String serviceUrl;

        public DatabaseConnecter(String serviceUrl)
        {
            this.serviceUrl = serviceUrl;            
            this.client = new MobileServiceClient(serviceUrl);
            this.table = this.client.GetTable<T>();
        }

        public async Task<List<T>> GetTableInformation()
        {
            return await this.table.ToListAsync();
        }

        public async Task PostInformation(T model)
        {
            await this.table.InsertAsync(model);
        }

    }
}
