using System;
using System.Windows.Controls;
using HomeWork_22_2_WPFClient.Pages;

namespace HomeWork_22_2_WPFClient.Services
{
    public class PageService
    {

        public event Action<Page> OnPageChanged;
        public void ChangePage(Page page) => OnPageChanged?.Invoke(page);
    }
}
