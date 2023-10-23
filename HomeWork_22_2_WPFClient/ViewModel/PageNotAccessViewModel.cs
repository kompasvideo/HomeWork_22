using DevExpress.Mvvm;
using HomeWork_22_2_WPFClient.Messages;
using HomeWork_22_2_WPFClient.Pages;
using HomeWork_22_2_WPFClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace HomeWork_22_2_WPFClient.ViewModel
{
    public class PageNotAccessViewModel : ViewModelBase
    {
        private static PageService pageService;
        private static MessageBus messageBus;
        private static Page page;
        public PageNotAccessViewModel(PageService p_pageService, MessageBus p_messageBus)
        {
            pageService = p_pageService;
            messageBus = p_messageBus;
            if (messageBus != null)
            {
                messageBus.Receive<ReturnPageMessage>(this, async message => { page = message.ReturnPage; });
            }
        }
        public PageNotAccessViewModel()
        {
        }

        public ICommand ButtonOkClickCommand
        {
            get
            {
                return new DelegateCommand(() => { pageService.ChangePage(page); });
            }
        }
    }
}