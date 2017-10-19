using ImporterServiceBase;
using log4net;
using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Training.Importer;
using Training.Importer.Ninject;

namespace ImporterService
{
    public partial class ImporterService : AbstractServiceBase
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(ImporterService));
        private IKernel ninjectKernel;
        private ImportManager importManager;
        private bool isFatalErrorOccured;
        private const int ParallelFilesCount = 200;
        public ImporterService()
        {
            InitializeComponent();
        }

        public override string Name => "ImporterService";

        public override int Interval => 1 * 60 * 1000 / 30;

        public override void Execute()
        {

            logger.Info("starting timer");
            timer.Change(Interval, Timeout.Infinite);
            importManager.RunImportIteration();
           
        }

        protected override void OnStart(string[] args)
        {
            Task.Factory.StartNew(() =>
            {
                logger.Info("Starting " + Name + ".");

                try
                {
                    ninjectKernel = new StandardKernel(new DataAccessModule(),
                new InfrastructureModule(), new DataProcessorModule());


                    importManager = ninjectKernel.Get<ImportManager>();

                    importManager.ParallelFilesCount = ParallelFilesCount;


                    timer.Change(Interval, Timeout.Infinite);
                    logger.InfoFormat("{0} successfully started.", Name);
                }
                catch (Exception exception)
                {
                    logger.Fatal("Error during Kuoni.Services.ImportService start.", exception);

                    isFatalErrorOccured = true;

                    Stop();
                }

            });
            //importManager = ninjectKernel.Get<ImportManager>();
            //importManager.RunImportIteration();
        }
            

        protected override void OnStop()
        {

            base.OnStop();

            if (!isFatalErrorOccured)
            {
                importManager.StopProcessing();
            }

            logger.Info("Kuoni.Services.ImportService successfully stopped.");

        }

        protected override void CallbackMethod(object sender)
        {
            try
            {
                Execute();
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("The exception occured during service {0} execution: {1}.{2}{3}",
                    Name, ex.Message, Environment.NewLine, ex.StackTrace);
            }
        }
    }
}
