using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using CKLLib;

namespace CKLDrawing
{
    public class EmptyInterval: Button // компонент интервала, на котором
                                       // индикаторная функция равна 0
    {
        public TimeInterval Duration { get => _duration; private set { } }
		new public Chain? Parent { get; }

        public bool IsActive { get => _isActive;  }
		
        private TimeInterval _duration;
        private bool _isActive;

        private void SetDefault() 
        {
            Background = Constants.DefaultColors.EMPTY_INTERVAL_COLOR;
            Height = Constants.Dimentions.EMPTY_INTERVAL_HEIGHT;
            BorderThickness = new Thickness(0);
			BorderBrush = Constants.DefaultColors.INTERVAL_ITEM_BORDER_COLOR;

			Click += (object sender, RoutedEventArgs e) => 
            {
                if (_isActive)
                {
                    Background = Constants.DefaultColors.EMPTY_INTERVAL_COLOR;
                    BorderThickness = new Thickness(0);
                }
                else 
                {
                    Background = Constants.DefaultColors.EMPRY_INTERVAL_ACTIVE_COLOR;
                    BorderThickness = Constants.Dimentions.EMPTY_INTERVAL_BORDER_SIZE;
                }

                _isActive = !_isActive;
            };  
        }

        public EmptyInterval(TimeInterval duraction) : base() 
        {
            _duration = duraction;
            SetDefault();
        }
	}
}
