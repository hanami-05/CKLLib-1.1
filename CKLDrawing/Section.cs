using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Diagnostics;

namespace CKLDrawing
{
    public class Section: Button // компонент деления на шкале времени
    {
        public DateTime Date { get => _date; }
        public double Value { get => _value;  }

        private DateTime _date;
        private double _value;

        public Section (double value) : base()
        {
            _value = value;
            SetUp();
        }

        public void SetDate(DateTime date) 
        {
            _date = date;
        } 

        private void SetUp() 
        {
            Background = Constants.DefaultColors.SECTION_COLOR;
            Width = Constants.Dimentions.SECTION_WIDTH;
            BorderThickness = new Thickness(0);
		}
	}
}
