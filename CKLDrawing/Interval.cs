using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using CKLLib;

namespace CKLDrawing
{
    public class Interval : Button // компонент инетрвала истинности индикаторной функции
    {
        public TimeInterval CurrentInterval { get => _interval; }
        public bool IsActive { get => _isActive; }
       
        
        private TimeInterval _interval;
        private bool _isActive;

        new public Chain? Parent { get;  }

        private void SetDefault()
        {
            Background = Constants.DefaultColors.INTERVAL_ITEM_COLOR;
            _isActive = false;
            BorderBrush = Constants.DefaultColors.INTERVAL_ITEM_BORDER_COLOR;
			BorderThickness = new Thickness(0);
			
            Click += (object sender, RoutedEventArgs e) => 
            {
                if (!_isActive)
                {
                    Background = Constants.DefaultColors.INTERVAL_ITEM_ACTIVE_COLOR;
                    BorderThickness = Constants.Dimentions.INTERVAL_BORDER_SIZE;
                }
                else 
                {
					Background = Constants.DefaultColors.INTERVAL_ITEM_COLOR;
                    BorderThickness = new Thickness(0);
				}

                _isActive = !_isActive;
            };
        }

        public void Select() 
        {
			Background = Constants.DefaultColors.INTERVAL_ITEM_ACTIVE_COLOR;
			BorderThickness = Constants.Dimentions.INTERVAL_BORDER_SIZE;
            _isActive = true;
		}

        public void Unselect() 
        {
			Background = Constants.DefaultColors.INTERVAL_ITEM_COLOR;
			BorderThickness = new Thickness(0);
            _isActive = false;
		}

        public Interval(TimeInterval interval) : base() 
        {
            _interval = interval;
            SetDefault();
        }
    }
}
