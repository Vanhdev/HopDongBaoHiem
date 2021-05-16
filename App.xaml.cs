using System.Windows;

namespace HopDongBaoHiem
{
    public partial class App : Application
    {
        //Đọc dữ liệu khi khởi động
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            InsuranceContractsManager.InsuranceContractsManagement.LoadData(@"E:\WPF_App\HopDongBaoHiem\TextFile1.txt");
        }

        //Lưu dữ liệu khi tắt ứng dụng
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            InsuranceContractsManager.InsuranceContractsManagement.SaveData(@"E:\WPF_App\HopDongBaoHiem\TextFile1.txt");
        }
    }
}
