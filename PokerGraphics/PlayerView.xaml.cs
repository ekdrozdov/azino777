using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PokerCore.ViewModel;
using ReactiveUI;

namespace PokerGraphics
{
    /// <summary>
    /// Логика взаимодействия для PlayerView.xaml
    /// </summary>
    public partial class PlayerView : UserControl, IViewFor<Player>
    {
        Player player;
        public PlayerView(Player _player)
        {
            player = _player;
            DataContext = _player;
            InitializeComponent();
        }

        public static readonly DependencyProperty name_u = DependencyProperty.Register(
   "uName",
   typeof(string),
   typeof(PlayerView));

        public string uName
        {
            get { return (string)GetValue(name_u); }
            set { SetValue(name_u, value); }
        }

        public static readonly DependencyProperty cash_u = DependencyProperty.Register(
    "Cash",
    typeof(string),
    typeof(PlayerView));

        public string Cash
        {
            get { return (string)GetValue(cash_u); }
            set { SetValue(cash_u, value); }
        }

        public Player ViewModel { get => player; set => throw new NotImplementedException(); }
        object IViewFor.ViewModel { get => player; set => throw new NotImplementedException(); }
    }
}
