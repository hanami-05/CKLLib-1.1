using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Controls;
using System.Windows;
using CKLLib;
using System.Windows.Input;
using System.IO.Packaging;

namespace CKLDrawing
{
    public class TimeOx : Canvas // компонент шкалы времени
    {
        public List<Section> Sections { get => _sections; }
        public Button Ox { get => _ox; }
        public Canvas SectionsText { get => _sectionsText; }
        public int DelCoast { get => _delCoast; }

        private int _delCoast;
        private TimeInterval _interval;
        private List<Section> _sections;
        private Button _ox;
        private Canvas _sectionsText;

        public TimeOx(TimeInterval globalInterval, int delCoast) : base()
        {
            _interval = globalInterval;
            _delCoast = delCoast;

            SetUp();
        }

        private void SetUp()
        {
            Background = Constants.DefaultColors.TIME_OX_COLOR;
            Height = Constants.Dimentions.TIME_OX_HEIGHT;
            Margin = Constants.Dimentions.TIME_OX_MARGIN;


            _sections = new List<Section>();

            SetUpTextCanvas();

            FillOx();
            DrawOX();
        }

        public void Refresh(TimeInterval globalInterval, int delCoast) 
        {
            Children.Clear();
			
           _interval = globalInterval;
			_delCoast = delCoast;
			
            SetUp();
        }

        private void SetUpTextCanvas()
        {
            _sectionsText = new Canvas();
            _sectionsText.Background = Background;
            _sectionsText.Height = Constants.Dimentions.SECTIONS_TEXT_HEIGHT;
            Canvas.SetBottom(_sectionsText, 0);

            Children.Add(_sectionsText);
        }

        private void DrawOX()
        {
            Button ox = new Button();
            ox.Background = Constants.DefaultColors.SECTION_COLOR;
            ox.Width = Width;
            ox.Height = Constants.Dimentions.SECTION_WIDTH;
            ox.BorderThickness = new Thickness(0);
            Canvas.SetLeft(ox, 0);
            Canvas.SetTop(ox, Constants.Dimentions.TIME_OX_HEIGHT / 2);

            Children.Add(ox);
            _ox = ox;
        }

        private void FillOx()
        {
            double startPos = Constants.Dimentions.FIRST_DEL_START;
            double sectionHeight = Constants.Dimentions.SECTION_HEIGHT;

            double end = _interval.EndTime;

            int i = 0;
            double val = _interval.StartTime;

            Section section;
            while (val <= end)
            {

                section = new Section(val);

                if (i % 5 == 0 && i%10 != 0)
                {
                    sectionHeight *= 2;
                    AddText(startPos, val);
                }
                else if (i % 10 == 0)
                {
                    sectionHeight *= 4;
                    AddText(startPos, val);
                }


                SetUpSection(section, sectionHeight, startPos, val);
                AddSection(section);

                i++;
                val += _delCoast;
				sectionHeight = Constants.Dimentions.SECTION_HEIGHT;
				
                if (val <= end) 
                {
					startPos += Constants.Dimentions.DEL_WIDTH;
				}
            }
            Width = startPos + Constants.Dimentions.OX_FREE_INTERVAL;
        }

        private void SetUpSection(Section section, double height, double startPos, double value)
        {
            section.Height = height;
            Canvas.SetLeft(section, startPos);
            Canvas.SetTop(section, (Constants.Dimentions.TIME_OX_HEIGHT - height) / 2);

            section.MouseEnter += (object sender, MouseEventArgs e) =>
            {
                string s = value.ToString();

                MessageBox.Show(s);
            };
        }

        private void AddSection(Section section)
        {
            Children.Add(section);
            _sections.Add(section);
        }

        private void AddText(double startPos, double value)
        {
            Label label = new Label();
            label.Background = Constants.DefaultColors.TIME_OX_COLOR;
            label.FontSize = Constants.Dimentions.TEXT_SIZE;
            label.Foreground = Constants.DefaultColors.SECTION_COLOR;

            string s = value.ToString();
            string res = s.Contains(',') ? s.Substring(0, s.IndexOf(',') + 1) : s;
            label.Content = res;

            label.HorizontalAlignment = HorizontalAlignment.Left;
            label.HorizontalContentAlignment = HorizontalAlignment.Center;
            label.VerticalContentAlignment = VerticalAlignment.Bottom;
            label.Padding = new Thickness(0);

            Canvas.SetLeft(label, startPos - 2.5);
            Canvas.SetBottom(label, 2.5);
            _sectionsText.Children.Add(label);

            label.Margin = new Thickness(0, 0, 0, 0);
        }

        private void SetDefaultDelCoast() { _delCoast = 1; }
    }
}
