using log4net;
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

namespace ImporterServiceBase
{
    public abstract partial class AbstractServiceBase : ServiceBase
    {
        protected readonly Timer timer;
        private readonly ILog logger = LogManager.GetLogger(typeof(AbstractServiceBase));

        protected AbstractServiceBase()
        {
            InitializeComponent();
            timer = new Timer(CallbackMethod, null, Timeout.Infinite, Timeout.Infinite);
        }

        public abstract void Execute();

        public abstract string Name { get; }

        public abstract int Interval { get; }

        protected virtual void CallbackMethod(object sender)
        {
            // pause timer during processing so it
            // wont be run twice if the processing takes longer
            // than the interval for some reason
            timer.Change(Timeout.Infinite, Timeout.Infinite);

            try
            {
                Execute();
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("The exception occured during service {0} execution: {1}.{2}{3}",
                    Name, ex.Message, Environment.NewLine, ex.StackTrace);
            }

            // launch again in 15 seconds
            timer.Change(Interval, Timeout.Infinite);
        }

        protected override void OnStart(string[] args)
        {
            logger.Info("Starting " + Name + ".");
            timer.Change(Interval, Timeout.Infinite);
        }

        protected override void OnStop()
        {
            logger.Info("Stopping " + Name + ".");
            timer.Change(Timeout.Infinite, Timeout.Infinite);
        }
    }
}
