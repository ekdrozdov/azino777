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
            //кароч надо создавать объекты типа PlayerView , это UserControl для каждого пользователя
            //у меня не получилось прибиндиться таким образом, мне каж надо как то сказать MainWindow что у него появились ещё контролы
            //но зато получчилось прибиндиться напрямую через ViewModel MainWindow (см. AllBank, SmallBlind, BigBlind)
            pokerTable = new Poker(realName, realCash, smallBlind, bigBlind);
        }

        public MainWindow()
        {
            PokerInitialize();
            InitializeComponent();
            DataContext = this;

            menu.Visibility = Visibility.Visible;
            grid_sett.Visibility = Visibility.Collapsed;
            table.Visibility = Visibility.Collapsed;
          
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
                MessageBox.Show("Ты шо дурак, введи целое число");
                grid_sett.Visibility = Visibility.Visible;
                table.Visibility = Visibility.Collapsed;
            }
        }

        private void button_out_to_menu_Click(object sender, RoutedEventArgs e)
        {
            table.Visibility = Visibility.Collapsed;
            grid_sett.Visibility = Visibility.Visible;

        }
        private void button_exit_Click(object sender, RoutedEventArgs e)
        {
        }
        private void button_fold_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.Players[0].Fold();
                pokerTable.EndAction();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void button_bet_click(object sender, RoutedEventArgs e)
        {
            try
            {
                int curBet = Int32.Parse(textbox_number_of_money.Text);
                if (curBet == pokerTable.player0.Cash)
                    ViewModel.Players[0].AllIn();
                else if (curBet < 0)
                    throw new Exception("Ставка меньше нуля! Жулик!");
                else if ((pokerTable.player0.PlayerBet - pokerTable.CurrentBet) == 0 && curBet == 0)
                    ViewModel.Players[0].Check();
                else if (curBet + pokerTable.player0.PlayerBet == pokerTable.CurrentBet)
                    ViewModel.Players[0].Call();
                else if (curBet + pokerTable.player0.PlayerBet > pokerTable.CurrentBet)
                    ViewModel.Players[0].Raise(curBet);
                //pokerTable.EndAction();
                pokerTable.NextCurrentPlayer();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void button_add_1Bot(object sender, RoutedEventArgs e)
        {
            try {
                ViewModel.TryConnectBot(1,1000);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void button_add_2Bot(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.TryConnectBot(2, 1000);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void button_add_3Bot(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.TryConnectBot(3, 1000);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void button_add_4Bot(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.TryConnectBot(4, 1000);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void button_add_5Bot(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.TryConnectBot(5, 1000);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void button_add_6Bot(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.TryConnectBot(6, 1000);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void button_add_7Bot(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.TryConnectBot(7, 1000);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void button_add_8Bot(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.TryConnectBot(8, 1000);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void button_add_9Bot(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.TryConnectBot(9, 1000);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void button_startgame_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.GameStart();
                button_startgame.Visibility = Visibility.Hidden;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}

