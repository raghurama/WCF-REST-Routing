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
            var transaction = StrongDb.LookupMultipleCollections(Db.SProcs.GetSOWdetail.Name,
                 (databaseWrapper, dbCommand) =>
                 {
                     databaseWrapper.AddInParameter(dbCommand, Db.SProcs.GetSOWdetail.Params.SOWID, DbType.String, SOWID);
                     databaseWrapper.AddInParameter(dbCommand, Db.SProcs.GetSOWdetail.Params.Year, DbType.String, year);
                 },
                   
                    (dataReader) => new FPCForecast
                    {
                        SOWID = dataReader["SOW_ID"].ParseString(),
                        SOWName = dataReader["SOW_NAME"].ParseString(),
                        SOWStartDate = dataReader["START_DATE"].ParseString(),
                        ...
                        ...
                    }
                     );

            if (transaction.Status == ResultStatus.Error)
                return new TransactionResult<SOWDetails>(ResultStatus.Error, null, transaction.Message);

            if (transaction.Data == null)
                return new TransactionResult<SOWDetails>(ResultStatus.Error, null,
                    string.Format("Get SOW details failed for SOW ID : {0} & Year : {1}", SOWID, year));

            if (transaction.Data.Count() == 0)
                return new TransactionResult<SOWDetails>(ResultStatus.Error, null,
                    string.Format("Get SOW details failed for SOW ID : {0} & Year : {1}", SOWID, year));

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
