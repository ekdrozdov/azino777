using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using System.Reactive;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PokerGraphics
{
    public class example : ReactiveObject
    {
        Visibility _visualMenu = Visibility.Visible;
        public Visibility VisualMenu
        {
            get { return _visualMenu; }
            set { this.RaiseAndSetIfChanged(ref _visualMenu, value); }
        }

        Visibility _visualMenu2 = Visibility.Collapsed;
        public Visibility VisualMenu2
        {
            get { return _visualMenu2; }
            set { this.RaiseAndSetIfChanged(ref _visualMenu2, value); }
        }

        Visibility _visualMenu3 = Visibility.Collapsed;
        public Visibility VisualMenu3
        {
            get { return _visualMenu3; }
            set { this.RaiseAndSetIfChanged(ref _visualMenu3, value); }
        }

        #region CONSTRUCTOR
        public example()
        {
            a = ReactiveCommand.Create(
                () => 
                {
                    _visualMenu = Visibility.Collapsed;
                }
                );
        }
        #endregion

        #region COMMANDS
        public ReactiveCommand<Unit, Unit> a { get; }
        #endregion
    }
}
