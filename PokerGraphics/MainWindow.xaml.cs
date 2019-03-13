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
using ReactiveUI;
using PokerCore.ViewModel;
using System.ComponentModel;
using System.Reactive.Disposables;

namespace PokerGraphics
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IViewFor<Poker>
    {
        Poker pokerTable;
        public List<PlayerView> views = new List<PlayerView>();

        public Poker ViewModel { get => pokerTable; set => throw new NotImplementedException(); }
        object IViewFor.ViewModel { get => pokerTable; set => throw new NotImplementedException(); }

        public event PropertyChangedEventHandler PropertyChanged;
        public event ReactiveUI.PropertyChangingEventHandler PropertyChanging;

        public void PokerInitialize()
        {
            string realName = "Ilya";
            int smallBlind = 10;
            int bigBlind = 50;
            int realCash = 1000;
            Player real = new Player(realName, realCash);
            //кароч надо создавать объекты типа PlayerView , это UserControl для каждого пользователя
            //у меня не получилось прибиндиться таким образом, мне каж надо как то сказать MainWindow что у него появились ещё контролы
            //но зато получчилось прибиндиться напрямую через ViewModel MainWindow (см. AllBank, SmallBlind, BigBlind)
            PlayerView tmp = new PlayerView(real);
            tmp.OneWayBind(tmp.ViewModel, v => v.MyState.Cash, view => view.Cash);
            tmp.OneWayBind(tmp.ViewModel, v => v.MyState.Name, view => view.uName);
            tmp.Height = 50;
            tmp.Margin = new Thickness(506, 630, 464, 10);
            tmp.Visibility = Visibility.Visible;
            views.Add(tmp);
            AddVisualChild(tmp);
            pokerTable = new Poker(real, smallBlind, bigBlind); 

            tmp = new PlayerView(pokerTable.TryConnect("Bot0", 800));
            tmp.OneWayBind(tmp.ViewModel, v => v.MyState.Name, view => view.uName);
            tmp.OneWayBind(tmp.ViewModel, v => v.MyState.Cash, view => view.Cash);
            tmp.Height = 50;
            tmp.Margin = new Thickness(506, 10, 464, 630);
            tmp.Visibility = Visibility.Visible;
            views.Add(tmp);
        }

        public MainWindow()
        {
            PokerInitialize();
            InitializeComponent();
            
            DataContext = this;
            this.WhenActivated(disposer =>
            {
                this.WhenAnyValue(t => t).Subscribe(v => DataContext = v).DisposeWith(disposer);
            });

            //menu.Visibility = Visibility.Visible;
            //grid_sett.Visibility = Visibility.Collapsed;
            //table.Visibility = Visibility.Collapsed;
        }

        private void button_start_Click(object sender, RoutedEventArgs e)
        {
            menu.Visibility = Visibility.Collapsed;
            grid_sett.Visibility = Visibility.Visible;
        }

        private void button_done_Click(object sender, RoutedEventArgs e)
        {
            grid_sett.Visibility = Visibility.Collapsed;
            table.Visibility = Visibility.Visible;
            try
            {
            }
            catch (Exception ex)
            {
                textbox_number_of_money.Text = "Ты шо дурак, введи целое число";
                grid_sett.Visibility = Visibility.Visible;
                table.Visibility = Visibility.Collapsed;
            }
        }

        private void button_out_to_menu_Click(object sender, RoutedEventArgs e)
        {
            table.Visibility = Visibility.Collapsed;
            grid_sett.Visibility = Visibility.Visible;

        }
    }
}
