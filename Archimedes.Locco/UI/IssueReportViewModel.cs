using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archimedes.Locco.UI.Utils;

namespace Archimedes.Locco.UI
{
    public class IssueReportViewModel : ViewModelBase
    {
        private readonly IssueReport _issue;


        public IssueReportViewModel(IssueReport issue)
        {
            _issue = issue;
        }

        /// <summary>
        /// VM Property
        /// </summary>
        public string Title
        {
            get { return _issue.Title; }
            set
            {
                _issue.Title = value;
                OnPropertyChanged("Title");
            }
        }

        /// <summary>
        /// VM Property
        /// </summary>
        public string Description
        {
            get { return _issue.Description; }
            set
            {
                _issue.Description = value;
                OnPropertyChanged("Description");
            }
        }


        /// <summary>
        /// Gets / Sets the issue report
        /// </summary>
        public IssueReport Issue
        {
            get { return _issue; }
        }
    }
}
