using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace MRCTest.ViewModels{
    public partial class MainViewModel : ViewModelBase{
        [ObservableProperty] private string webViewSource;

        public MainViewModel(){

            WebViewSource = "http://localhost:8001/";
        }
    }
}
