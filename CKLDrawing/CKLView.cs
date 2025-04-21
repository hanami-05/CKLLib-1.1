using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CKLLib;

namespace CKLDrawing
{
	public class CKLView : DockPanel // компонент диаграммы Ганта
	{
		public CKL Ckl { get => _ckl; }
		public List<Chain> Chains { get => _chains; }
		public List<Interval> SelectedIntervals { get => _selectedIntervals; }
		public List<EmptyInterval> SelectedEmptyIntervals { get => _selectedEmptyIntervals; }
		public TimeOx TimeScale { get => _timeScale; }
		public TimeInterval CurrentInterval { get => _currentInterval; }
		public StackPanel ListView { get => _listView; }
		public Canvas MainView { get => _mainView; }
		public ScrollViewer ScrollView { get => _scrollView; }
		public TimeDimentions TimeDimention { get => _timeDimention; }
		public int DelCoast { get => _delCoast; }

		private CKL _ckl;
		private List<Chain> _chains;
		private List<Interval> _selectedIntervals;
		private List<EmptyInterval> _selectedEmptyIntervals;
		private TimeOx _timeScale;
		private TimeInterval _currentInterval;
		private StackPanel _listView;
		private Canvas _mainView;
		private ScrollViewer _scrollView;
		private DockPanel _dockPanel;
		private TimeDimentions _timeDimention;
		private int _delCoast;

		public CKLView(CKL ckl) : base()
		{
			_ckl = ckl;
			_delCoast = 1;
			_timeDimention = _ckl.Dimention;
			_currentInterval = new TimeInterval(_ckl.GlobalInterval.StartTime, _ckl.GlobalInterval.EndTime);

			SetUp();
		}

		private void SetUp()
		{
			Background = Constants.DefaultColors.CKL_BACKGROUND;

			_chains = new List<Chain>();
			_selectedIntervals = new List<Interval>();
			_selectedEmptyIntervals = new List<EmptyInterval>();

			_listView = new StackPanel();
			_listView.Background = Background;
			_listView.Orientation = Orientation.Vertical;

			_mainView = new Canvas();
			_mainView.Background = Background;
			_mainView.VerticalAlignment = VerticalAlignment.Top;
			_mainView.HorizontalAlignment = HorizontalAlignment.Left;


			_scrollView = new ScrollViewer();
			_scrollView.Background = Background;
			_scrollView.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
			_scrollView.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;

			_dockPanel = new DockPanel();
			_dockPanel.Background = Background;

			DrawOx();
			DrawChains();

			

			DockPanel.SetDock(_listView, Dock.Left);
			DockPanel.SetDock(_mainView, Dock.Left);

			_dockPanel.Children.Add(_listView);
			_dockPanel.Children.Add(_mainView);

			_scrollView.Content = _dockPanel;

			Children.Add(_scrollView);
		}

		public void ChangeDelCoast(TimeDimentions newDimention, int newDelCoast)
		{
			if (newDelCoast == 0) return;
			
			double intervalMulti = 1;
			double delMulti = (double)_delCoast / newDelCoast;

			if (!newDimention.Equals(_timeDimention)) 
			{
				//if ((int) newDimention > (int) _timeDimention) delMulti = (double)newDelCoast / _delCoast;
				intervalMulti = UpdateInterval(newDimention);
			} 

			double scale = intervalMulti * delMulti;

			_delCoast = newDelCoast;
			_timeDimention = newDimention;

			_timeScale.Refresh(_currentInterval, _delCoast);
			_mainView.Width = _timeScale.Width + Constants.Dimentions.MAIN_VIEW_PADDING_RIGHT;
			ChangeChainsScale(scale);
			ChangeDelCoast();
		}

		private void DrawOx()
		{

			_timeScale = new TimeOx(_currentInterval, _delCoast);

			Canvas.SetLeft(_timeScale, 0);
			Canvas.SetTop(_timeScale, 0);

			_mainView.Children.Add(_timeScale);
			_mainView.Width = _timeScale.Width + Constants.Dimentions.MAIN_VIEW_PADDING_RIGHT;
			_mainView.Height = _timeScale.Height + Constants.Dimentions.TIME_OX_MARGIN.Bottom
				+ Constants.Dimentions.MAIN_VIEW_PADDING_BOTTOM;

			_listView.Children.Add(new ValueBox
				($"{_delCoast} {Constants.TIME_DIMENTIONS_STRINGS[(int)_timeDimention]}\n{_currentInterval}")
			{
				Height = _timeScale.Height,
				Background = _timeScale.Background,
				Foreground = Constants.DefaultColors.SECTION_COLOR,
				Margin = Constants.Dimentions.TIME_OX_MARGIN,
			});

		}

		private void ChangeDelCoast()
		{
			(_listView.Children[0] as ValueBox).Content =
				$"{_delCoast} {Constants.TIME_DIMENTIONS_STRINGS[(int)_timeDimention]}\n{_currentInterval}";
			(_listView.Children[0] as ValueBox).Click += (object sender, RoutedEventArgs e) =>
			{
				MessageBox.Show($"{_currentInterval}");
			};
		}

		private void ChangeChainsScale(double scale) 
		{
			foreach (Chain chain in _chains)
			{
				foreach (Interval interval in chain.Intervals) interval.Width *= scale;
				foreach (EmptyInterval empty in chain.EmptyIntervals) empty.Width *= scale;
				chain.Width *= scale;
			}
		}

		private double UpdateInterval(TimeDimentions newDimention) 
		{
			int oldDim = (int)_timeDimention;
			int newDim = (int)newDimention;
			double intervalMulti = 1;

			if (oldDim > newDim)
			{
				for (int i = 0; i < oldDim - newDim; i++)
				{
					intervalMulti *= Constants.TIME_DIMENTIONS_CONVERT[newDim + i];
				}

			}
			else 
			{
				for (int i = 0; i < newDim - oldDim; i++) 
				{
					intervalMulti /= Constants.TIME_DIMENTIONS_CONVERT[oldDim + i];
				}
			}

			_currentInterval.Scale(intervalMulti);

			return intervalMulti;
		}

		private void DrawChains() 
		{
			double actTop = _timeScale.Height + Constants.Dimentions.TIME_OX_MARGIN.Bottom;

			foreach (RelationItem item in Ckl.Relation) 
			{
				Chain chain = new Chain(item, Ckl.GlobalInterval, 
					_timeScale.Width - Constants.Dimentions.OX_FREE_INTERVAL - Constants.Dimentions.FIRST_DEL_START);
				
				SetUpChainIntervals(chain);
				SetUpChainEmptyIntervals(chain);
				AddChain(chain, actTop);
				
				actTop += chain.Height + Constants.Dimentions.CHAIN_MARGIN.Bottom;
				_mainView.Height += chain.Height + Constants.Dimentions.CHAIN_MARGIN.Bottom;

				ValueBox vb = new ValueBox(item, chain);
				
				vb.Click += (object sender, RoutedEventArgs e) =>
				{
					if (vb.IsActive)
					{
						foreach (Interval interval in chain.Intervals)
						{
							interval.Select();
							_selectedIntervals.Add(interval);
						}
					}
					else 
					{
						foreach (Interval interval in chain.Intervals) 
						{
							interval.Unselect();
							_selectedIntervals.Remove(interval);
						}
					}
				};

				_listView.Children.Add(vb);
			}
		}

		private void SetUpChainIntervals(Chain chain) 
		{
			foreach (Interval interval in chain.Intervals)
			{
				interval.Click += (object sender, RoutedEventArgs e) => 
				{
					if (interval.IsActive) 
					{
						_selectedIntervals.Add(interval);
						MessageBox.Show($"{TimeInterval
							.GetIntervalInAnotherDemention(interval.CurrentInterval, _ckl.Dimention, _timeDimention)}");
					} 
					else _selectedIntervals.Remove(interval);

				};
			}
		}

		private void SetUpChainEmptyIntervals(Chain chain) 
		{
			foreach (EmptyInterval interval in chain.EmptyIntervals) 
			{
				interval.Click += (object sender, RoutedEventArgs e) => 
				{
					if (interval.IsActive)
					{
						_selectedEmptyIntervals.Add(interval);
						MessageBox.Show($"{TimeInterval.GetIntervalInAnotherDemention(interval.Duration, _ckl.Dimention, _timeDimention)}");
					}

					else _selectedEmptyIntervals.Remove(interval);
				};
			}
		}

		private void AddChain(Chain chain, double top) 
		{
			Canvas.SetLeft(chain, 0);
			Canvas.SetTop(chain, top);

			_mainView.Children.Add(chain);
			_chains.Add(chain);
			
		}
	}
}
