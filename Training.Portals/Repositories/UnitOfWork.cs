using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using Training.Portals.Utils.Infrastructure.Config;

namespace Training.Portals.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private CrmServiceClient conn;
        private IOrganizationService _orgService;
        private AccountRepository accountRepository;
        private UserRepository userRepository;
        private bool disposed = false;

        public UnitOfWork()
        {
            conn= new CrmServiceClient(CRMConnectConfiguraion.GetServiceConfiguration());
            _orgService = conn.OrganizationWebProxyClient != null ? (IOrganizationService)conn.OrganizationWebProxyClient :
                (IOrganizationService)conn.OrganizationServiceProxy; 

        }

        public AccountRepository Accounts
        {
            get
            {
                if(accountRepository==null)
                    accountRepository=new AccountRepository(_orgService);
                return accountRepository;
            }
        }

        public UserRepository Users
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository=new UserRepository(_orgService);
                }
                return userRepository;
            }
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    conn.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}