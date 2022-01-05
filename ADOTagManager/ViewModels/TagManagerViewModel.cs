using ADOTagManager.Classes;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.Common.TokenStorage;
using Microsoft.VisualStudio.Services.WebApi;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ADOTagManager.ViewModels
{
    public class TagManagerViewModel : BaseViewModel
    {
        #region Fields
        private string _orgName;
        private string _pat;
        private WorkItemTrackingHttpClient _client;

        #endregion

        #region Properties


        public string PAT
        {
            get { return _pat; }
            set
            {
                _pat = value;
                RaisePropertyChanged();
            }
        }


        public string OrgName
        {
            get { return _orgName; }
            set
            {
                _orgName = value;
                RaisePropertyChanged();
            }
        }


        #endregion

        #region Constructor
        public TagManagerViewModel()
        {
            LoginCommand = new DelegateCommand(Login, CanLogin);


        }
        #endregion

        #region Commands

        public DelegateCommand LoginCommand { get; set; }



        #endregion

        #region Methods

        void Login()
        {
            VssBasicCredential cred = new VssBasicCredential(new NetworkCredential("", PAT));

            var conn = new VssConnection(new Uri($"https://dev.azure.com/{OrgName}"), new VssCredentials(cred));
            _client = conn.GetClient<WorkItemTrackingHttpClient>();

        }

        public override void Loaded()
        {


        }

        public bool CanLogin()
        {
            return !string.IsNullOrEmpty(PAT) && !string.IsNullOrEmpty(OrgName);
        }




        #endregion
    }
}
