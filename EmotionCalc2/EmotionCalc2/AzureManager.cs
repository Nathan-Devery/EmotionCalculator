﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using EmotionCalc2.Model;

namespace EmotionCalc2
{
    public class AzureManager
    {

        private static AzureManager instance;
        private MobileServiceClient client;
        private IMobileServiceTable<NotHotDogModel> notHotDogTable;

        private AzureManager()
        {
            this.client = new MobileServiceClient("https://nothotdoginformation.azurewebsites.net");
            this.notHotDogTable = this.client.GetTable<NotHotDogModel>();
        }

        public MobileServiceClient AzureClient
        {
            get { return client; }
        }

        public static AzureManager AzureManagerInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AzureManager();
                }

                return instance;
            }
        }

        public async Task<List<NotHotDogModel>> GetHotDogInformation()
        {
            return await this.notHotDogTable.ToListAsync();
        }


    }
}