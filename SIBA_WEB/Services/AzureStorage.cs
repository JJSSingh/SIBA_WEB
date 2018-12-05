using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using SIBA_WEB.Models.Comida;
using SIBA_WEB.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIBA_WEB.Services
{
    public class AzureStorage : IAzureStorage
    {
        String StorageAccount;
        String StorageKey;

        public AzureStorage(String StorageAccount, String StorageKey)
        {
            this.StorageAccount = StorageAccount;
            this.StorageKey = StorageKey;
        }
        
        private async Task<CloudTable> GetTableAsync(String TableName)
        {
            //Account
            CloudStorageAccount storageAccount = new CloudStorageAccount(
                new StorageCredentials(this.StorageAccount, this.StorageKey), false);

            //Client
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            //Table
            CloudTable table = tableClient.GetTableReference(TableName);
            await table.CreateIfNotExistsAsync();

            return table;
        }

        //###################### PLATILLO ############3
        //#########################################################################################

        public async Task<PlatilloEntity> GetItem_Platillos(String TableName, string partitionKey, string rowKey)
        {
            //Table
            CloudTable table = await GetTableAsync(TableName);

            //Operation
            TableOperation operation = TableOperation.Retrieve<PlatilloEntity>(partitionKey, rowKey);

            //Execute
            TableResult result = await table.ExecuteAsync(operation);

            return (PlatilloEntity)(dynamic)result.Result;
        }

        public async Task<List<PlatilloEntity>> GetList_Platillos(String TableName)
        {
            //Table
            CloudTable table = await GetTableAsync(TableName);

            //Query
            TableQuery<PlatilloEntity> query = new TableQuery<PlatilloEntity>();

            List<PlatilloEntity> results = new List<PlatilloEntity>();
            TableContinuationToken continuationToken = null;
            do
            {
                TableQuerySegment<PlatilloEntity> queryResults =
                    await table.ExecuteQuerySegmentedAsync(query, continuationToken);

                continuationToken = queryResults.ContinuationToken;
                results.AddRange(queryResults.Results);

            } while (continuationToken != null);

            return results;
        }

        // ############## TOKEN ######################
        // #############################################################################

        public async Task<TokenEntity> GetItem_Token(String TableName, string partitionKey, string rowKey)
        {
            //Table
            CloudTable table = await GetTableAsync(TableName);

            //Operation
            TableOperation operation = TableOperation.Retrieve<TokenEntity>(partitionKey, rowKey);

            //Execute
            TableResult result = await table.ExecuteAsync(operation);

            return (TokenEntity)(dynamic)result.Result;
        }

        public async Task<List<TokenEntity>> GetList_Token(String TableName)
        {
            //Table
            CloudTable table = await GetTableAsync(TableName);

            //Query
            TableQuery<TokenEntity> query = new TableQuery<TokenEntity>();

            List<TokenEntity> results = new List<TokenEntity>();
            TableContinuationToken continuationToken = null;
            do
            {
                TableQuerySegment<TokenEntity> queryResults =
                    await table.ExecuteQuerySegmentedAsync(query, continuationToken);

                continuationToken = queryResults.ContinuationToken;
                results.AddRange(queryResults.Results);

            } while (continuationToken != null);

            return results;
        }

        //##################### ALUMNO ###############
        //#####################################################################

        public async Task<List<AlumnoEntity>> GetItem_Alumno(String TableName, string PartitionKey)
        {
            //Table
            CloudTable table = await GetTableAsync(TableName);

            //Query
            TableQuery<AlumnoEntity> query = new TableQuery<AlumnoEntity>()
                                        .Where(TableQuery.GenerateFilterCondition("PartitionKey",
                                                QueryComparisons.Equal, PartitionKey));

            List<AlumnoEntity> results = new List<AlumnoEntity>();
            TableContinuationToken continuationToken = null;
            do
            {
                TableQuerySegment<AlumnoEntity> queryResults =
                    await table.ExecuteQuerySegmentedAsync(query, continuationToken);

                continuationToken = queryResults.ContinuationToken;

                results.AddRange(queryResults.Results);

            } while (continuationToken != null);

            return results;
        }
        public async Task<List<AlumnoEntity>> GetList_Alumno(String TableName)
        {
            //Table
            CloudTable table = await GetTableAsync(TableName);

            //Query
            TableQuery<AlumnoEntity> query = new TableQuery<AlumnoEntity>();

            List<AlumnoEntity> results = new List<AlumnoEntity>();
            TableContinuationToken continuationToken = null;
            do
            {
                TableQuerySegment<AlumnoEntity> queryResults =
                    await table.ExecuteQuerySegmentedAsync(query, continuationToken);

                continuationToken = queryResults.ContinuationToken;
                results.AddRange(queryResults.Results);

            } while (continuationToken != null);

            return results;
        }

        public async Task<List<AlumnoEntity>> GetItem_Alumno2(String TableName, string rowKey)
        {
            //Table
            CloudTable table = await GetTableAsync(TableName);

            //Query
            TableQuery<AlumnoEntity> query = new TableQuery<AlumnoEntity>()
                                        .Where(TableQuery.GenerateFilterCondition("RowKey",
                                                QueryComparisons.Equal, rowKey));

            List<AlumnoEntity> results = new List<AlumnoEntity>();
            TableContinuationToken continuationToken = null;
            do
            {
                TableQuerySegment<AlumnoEntity> queryResults =
                    await table.ExecuteQuerySegmentedAsync(query, continuationToken);

                continuationToken = queryResults.ContinuationToken;

                results.AddRange(queryResults.Results);

            } while (continuationToken != null);

            return results;
        }

        //#############################
        //Encargada################
        //---

        public async Task<EncargadaEntity> GetItem_Encargada(String TableName, string partitionKey, string rowKey)
        {
            //Table
            CloudTable table = await GetTableAsync(TableName);

            //Operation
            TableOperation operation = TableOperation.Retrieve<EncargadaEntity>(partitionKey, rowKey);

            //Execute
            TableResult result = await table.ExecuteAsync(operation);

            return (EncargadaEntity)(dynamic)result.Result;
        }




        //#####################################################################

        public async Task<String> InsertOrUpdate(String TableName, TableEntity item)
        {
            //Table
            CloudTable table = await GetTableAsync(TableName);

            //Operation
            TableOperation operation = TableOperation.InsertOrReplace(item);

            //Execute
            await table.ExecuteAsync(operation);

            return "Success!";
        }


        //##############################################################################
        //##############################################################################

        public async Task<List<TableEntity>> GetList(String TableName)
        {
            //Table
            CloudTable table = await GetTableAsync(TableName);

            //Query
            TableQuery<TableEntity> query = new TableQuery<TableEntity>();

            List<TableEntity> results = new List<TableEntity>();
            TableContinuationToken continuationToken = null;
            do
            {
                TableQuerySegment<TableEntity> queryResults =
                    await table.ExecuteQuerySegmentedAsync(query, continuationToken);

                continuationToken = queryResults.ContinuationToken;
                results.AddRange(queryResults.Results);

            } while (continuationToken != null);

            return results;
        }

        public async Task<List<TableEntity>> GetList(String TableName, string partitionKey)
        {
            //Table
            CloudTable table = await GetTableAsync(TableName);

            //Query
            TableQuery<TableEntity> query = new TableQuery<TableEntity>()
                                        .Where(TableQuery.GenerateFilterCondition("PartitionKey",
                                                QueryComparisons.Equal, partitionKey));

            List<TableEntity> results = new List<TableEntity>();
            TableContinuationToken continuationToken = null;
            do
            {
                TableQuerySegment<TableEntity> queryResults =
                    await table.ExecuteQuerySegmentedAsync(query, continuationToken);

                continuationToken = queryResults.ContinuationToken;

                results.AddRange(queryResults.Results);

            } while (continuationToken != null);

            return results;
        }

        public async Task<TableEntity> GetItem(String TableName,string partitionKey, string rowKey)
        {
            //Table
            CloudTable table = await GetTableAsync(TableName);

            //Operation
            TableOperation operation = TableOperation.Retrieve<TableEntity>(partitionKey, rowKey);

            //Execute
            TableResult result = await table.ExecuteAsync(operation);

            return (TableEntity)(dynamic)result.Result;
        }

        public async Task Insert(String TableName,TableEntity item)
        {
            //Table
            CloudTable table = await GetTableAsync(TableName);

            //Operation
            TableOperation operation = TableOperation.Insert(item);

            //Execute
            await table.ExecuteAsync(operation);
        }

        public async Task Update(String TableName, TableEntity item)
        {
            //Table
            CloudTable table = await GetTableAsync(TableName);

            //Operation
            TableOperation operation = TableOperation.InsertOrReplace(item);

            //Execute
            await table.ExecuteAsync(operation);
        }

        public async Task Delete(String TableName,string partitionKey, string rowKey)
        {
            //Item
            TableEntity item = await GetItem(TableName, partitionKey, rowKey);

            //Table
            CloudTable table = await GetTableAsync(TableName);

            //Operation
            TableOperation operation = TableOperation.Delete(item);

            //Execute
            await table.ExecuteAsync(operation);
        }
    }






    public interface IAzureStorage
    {
        Task<List<TableEntity>> GetList(string TableName);
        Task<List<TableEntity>> GetList(String TableName, string partitionKey);
        //Task<CloudTable> GetTableAsync(String TableName);
        Task Delete(String TableName, string partitionKey, string rowKey);
        Task Update(String TableName, TableEntity item);
        Task Insert(String TableName, TableEntity item);
        Task<TableEntity> GetItem(String TableName, string partitionKey, string rowKey);
    }
}

