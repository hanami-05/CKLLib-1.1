using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using CKLLib;
using System.Windows.Automation.Provider;
namespace CKLDrawing
{
	public class ValueBox: Button // компонент элемента отношения
	{
		public Chain? CurrentChain { get => _chain; }
		public bool IsActive { get => _isActive; }
		public RelationItem? Item { get => _item; }
		public object Info { get => _info; }

		private Chain _chain;
		private bool _isActive;
		private RelationItem _item;
		private object _info;
		public ValueBox(RelationItem item, Chain chain) : base() 
		{
			_item = item;
			_chain = chain;
			_isActive = false;

			SetUp();
		}

		public ValueBox(object info) : base() 
		{
			_info = info;
			SetUp();
		}

		private void SetUp() 
		{
			if (_item != null) 
			{
				Content = _item.Value.ToString();
				Click += (object sender, RoutedEventArgs e) => 
				{
					if (Background.Equals(Constants.DefaultColors.CKL_BACKGROUND)) 
					{
						if (_item.Info != null) MessageBox.Show(_item.Info.ToString());
						Background = Constants.DefaultColors.TIME_OX_COLOR;
					}
					else Background = Constants.DefaultColors.CKL_BACKGROUND;
					
					_isActive = !_isActive;
				};
			} 
			else Content = _info.ToString();
			
			Width = Constants.Dimentions.VALUE_BOX_WIDTH;
			HorizontalContentAlignment = HorizontalAlignment.Center;
			VerticalContentAlignment = VerticalAlignment.Center;
			Height = Constants.Dimentions.CHAIN_HEIGHT;
			Background = Constants.DefaultColors.VALUE_BOX_DEFAULT_BACKGROUND;
			Foreground = Constants.DefaultColors.VALUE_COLOR;
			BorderThickness = new Thickness(Constants.Dimentions.SECTION_WIDTH);
			BorderBrush = Constants.DefaultColors.INTERVAL_ITEM_BORDER_COLOR;
			Margin = Constants.Dimentions.CHAIN_MARGIN;
		}
	}
}
