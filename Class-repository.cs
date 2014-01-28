using System.Data;
using System.Linq;

namespace <DataRepository>
{
    public class Repository : IRepository
    {
        private IStrongDb StrongDb { get; set; }
        private ITransactionResultFactory TransactionResultFactory { get; set; }

        public ProjectsRepository(IStrongDb strongDb)
        {
            StrongDb = strongDb;
            TransactionResultFactory = new TransactionResultFactory();
        }

        public TransactionResult<SOWDetails> GetSOWDetails(string SOWID, string year)
        {
            var transaction = StrongDb.LookupMultipleCollections(<SPNAME>,
                 (databaseWrapper, dbCommand) =>
                 {
                     databaseWrapper.AddInParameter(dbCommand, "param1", DbType.String, SOWID);
                     databaseWrapper.AddInParameter(dbCommand, "param2", DbType.String, year);
                 },
                   
                    (dataReader) => new FPCForecast
                    {
                        SOWID = dataReader["name1"].ParseString(), //use c# extension method here
                        SOWName = dataReader["name2"].ParseString(),
                        SOWStartDate = dataReader["name3"].ParseString(),
                        ...
                        ...
                    }
                     );

            if (transaction.Status == ResultStatus.Error)
                return new TransactionResult<class>(ResultStatus.Error, null, transaction.Message);

            if (transaction.Data == null)
                return new TransactionResult<SOWDetails>(ResultStatus.Error, null,
                    string.Format(""));

            if (transaction.Data.Count() == 0)
                return new TransactionResult<SOWDetails>(ResultStatus.Error, null,
                    string.Format(""));

            var sowLifeCycle = transaction.Data[0].Cast<SOWLifeCycle>().ToList();
            var fpcForecast = transaction.Data[1].Cast<FPCForecast>().ToList();
            ...
            ...
            return new TransactionResult<SOWDetails>(ResultStatus.Success,
                new SOWDetails
                {
                    fpcForecast = fpcForecast,
                    ...
                }, string.Empty);
        }

        
    }

   
}
