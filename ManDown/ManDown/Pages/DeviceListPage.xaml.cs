using BluetoothLE.Core;
using BluetoothLE.Core.Events;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ManDown.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeviceListPage : ContentPage
    {
        public ObservableCollection<IDevice> DiscoveredDevices { get; private set; }
        private IAdapter _adapter;

        public DeviceListPage(IAdapter adapter)
        {
            _adapter = adapter;
            DiscoveredDevices = new ObservableCollection<IDevice>();

            RefreshCommand = new Command(() => Scan());

            InitializeComponent();
            deviceListView.ItemSelected += DeviceSelected;
            deviceListView.RefreshCommand = RefreshCommand;

            _adapter.DeviceDiscovered += DeviceDiscovered;
            _adapter.DeviceConnected += DeviceConnected;
            _adapter.StartScanningForDevices();
        }

        void DeviceSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var device = e.SelectedItem as IDevice;
            if (device != null)
            {
                _adapter.ConnectToDevice(device);
            }
        }

        #region BluetoothAdapter callbacks

        void DeviceDiscovered(object sender, DeviceDiscoveredEventArgs e)
        {
            DiscoveredDevices.Add(e.Device);
        }

        void DeviceConnected(object sender, DeviceConnectionEventArgs e)
        {
            Navigation.PushAsync(new DevicePage(e.Device));
        }

        #endregion

        #region RefreshingData
        public bool IsRefreshing = false;

        public ICommand RefreshCommand;

        void Scan()
        {
            DiscoveredDevices.Clear();
            deviceListView.ItemSelected += DeviceSelected;

            _adapter.DeviceDiscovered += DeviceDiscovered;
            _adapter.DeviceConnected += DeviceConnected;
            _adapter.StartScanningForDevices();
            deviceListView.EndRefresh();
            //DisplayAlert("pulldown", "finished", "ok");
            OnPropertyChanged("IsRefreshing");
        }
        #endregion
    }
}