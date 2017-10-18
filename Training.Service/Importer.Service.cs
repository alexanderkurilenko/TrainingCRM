using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Training.Importer;
using Training.Importer.Ninject;

namespace Training.Service
{
    public partial class ImporterService : ServiceBase
    {
        private ImportManager importManager;
        private IKernel ninjectKernel;
        private bool isFatalErrorOccured;

        public ImporterService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
                 ninjectKernel = new StandardKernel(new DataAccessModule(),
                     new InfrastructureModule(), new DataProcessorModule());

                     importManager = ninjectKernel.Get<ImportManager>();
                    importManager.RunImportIteration();

      
            
           
        }

        protected override void OnStop()
        {
           
          importManager.StopProcessing();
            
        }
    }
}
