using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MAUIImageTintingSample
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand ColorItemClickedCommand { get; set; }
        public ICommand ChangeImageTintColorCommand { get; set; }

        private ColorItem _lastSelectedItem;

        private ObservableCollection<ColorItem> _colors;
        public ObservableCollection<ColorItem> Colors
        {
            get { return _colors; }
            set
            {
                _colors = value;
                OnPropertyChanged(nameof(Colors));
            }
        }

        private Color _tintColor = Color.FromRgba("#000000");
        public Color Color
        {
            get { return _tintColor; }
            set
            {
                _tintColor = value;
                OnPropertyChanged(nameof(Color));
            }
        }

        private bool _isAttached;
        public bool IsAttached
        {
            get { return _isAttached; }
            set
            {
                _isAttached = value;
                OnPropertyChanged(nameof(IsAttached));
            }
        }

        public MainPageViewModel()
        {
            ColorItemClickedCommand = new Command<ColorItem>((i) => OnColorItemClicked(i));
            ChangeImageTintColorCommand = new Command(OnChangeImageTintColor);
            Colors = new ObservableCollection<ColorItem>()
            {
                new ColorItem(){ Color = Color.FromArgb("#12AD2B") },
                new ColorItem(){ Color = Color.FromArgb("#F7DC6F") },
                new ColorItem(){ Color = Color.FromArgb("#01F9C6") },
                new ColorItem(){ Color = Color.FromArgb("#FFA500") },
                new ColorItem(){ Color = Color.FromArgb("#A2AD9C") },
                new ColorItem(){ Color = Color.FromArgb("#728FCE") },
                new ColorItem(){ Color = Color.FromArgb("#EB5406") },
                new ColorItem(){ Color = Color.FromArgb("#000000") },
                new ColorItem(){ Color = Color.FromArgb("#045D5D") },
            };
        }
        private void OnColorItemClicked(ColorItem item)
        {
            if (item != null)
            {
                if (_lastSelectedItem != null)
                    _lastSelectedItem.IsSelected = false;
                _lastSelectedItem = item;
                item.IsSelected = true;
            }
        }
        private void OnChangeImageTintColor()
        {
            if (_lastSelectedItem != null)
            {
                IsAttached = false;
                Color = _lastSelectedItem.Color;
                IsAttached = true;
            }
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class ColorItem : INotifyPropertyChanged
    {
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }
        private Color _color;
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
