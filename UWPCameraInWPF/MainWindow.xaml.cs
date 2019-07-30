using System;
using System.Linq;
using System.Windows;
using Windows.Devices.Enumeration;
using Windows.Media.Capture;
using Windows.UI.Xaml.Controls;
using Microsoft.Toolkit.Wpf.UI.XamlHost;
using Windows.Storage;
using Windows.Media.MediaProperties;
using Windows.Storage.Streams;
using Windows.Graphics.Imaging;
using Windows.Storage.FileProperties;
using Windows.Foundation;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Win10APIInWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window , INotifyPropertyChanged
    {
        private MediaCapture _mediaCapture;
        private CaptureElement _captureElement;

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<string> _imgCollection;

        public ObservableCollection<string> imgCollection
        {
            get { return _imgCollection; }
            set
            {
                _imgCollection = value;
                PropertyChange(nameof(imgCollection));
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            imgCollection = new ObservableCollection<string>();
            DataContext = this;
        }

        public void PropertyChange(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        private void MyNavView_OnChildChanged(object sender, EventArgs e)
        {
            WindowsXamlHost windowsXamlHost = (WindowsXamlHost)sender;

            var tempCaptureElement = (CaptureElement)windowsXamlHost.Child;
            if (tempCaptureElement != null)
            {
                _captureElement = tempCaptureElement;
                PrepareCamera();
            }
        }
        private async void PrepareCamera()
        {
            if (_mediaCapture == null)
            {
                var cameradevice = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);
                var selectDevice = cameradevice.FirstOrDefault();

                if (selectDevice != null)
                {
                    _mediaCapture = new MediaCapture();

                    await _mediaCapture.InitializeAsync(new MediaCaptureInitializationSettings()
                    {
                        VideoDeviceId = selectDevice.Id,
                        StreamingCaptureMode = StreamingCaptureMode.Video
                    });

                    _captureElement.Source = _mediaCapture;

                    await _mediaCapture.StartPreviewAsync();

                }
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var myPictures = await StorageLibrary.GetLibraryAsync(Windows.Storage.KnownLibraryId.Pictures);
            var file = await myPictures.SaveFolder.CreateFileAsync("xaml-islands.jpg", CreationCollisionOption.GenerateUniqueName);
            using (var captureStream = new InMemoryRandomAccessStream())
            {
                await _mediaCapture.CapturePhotoToStreamAsync(ImageEncodingProperties.CreateJpeg(), captureStream);

                using (var fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    var decoder = await BitmapDecoder.CreateAsync(captureStream);
                    var encoder = await BitmapEncoder.CreateForTranscodingAsync(fileStream, decoder);

                    var properties = new BitmapPropertySet {{ "System.Photo.Orientation", new BitmapTypedValue(PhotoOrientation.Normal, PropertyType.UInt16)}};
                    await encoder.BitmapProperties.SetPropertiesAsync(properties);

                    await encoder.FlushAsync();

                    imgCollection.Add(file.Path);
                }
            }
        }
    }
}
