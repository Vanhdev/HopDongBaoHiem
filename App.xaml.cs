using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HopDongBaoHiem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            InsuranceContractsManager.InsuranceContractsManagement.SaveData(@"E:\WPF_App\HopDongBaoHiem\TextFile1.txt");
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            InsuranceContractsManager.InsuranceContractsManagement.LoadData(@"E:\WPF_App\HopDongBaoHiem\TextFile1.txt");
        }
    }
}
