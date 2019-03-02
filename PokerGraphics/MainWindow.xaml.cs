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

namespace PokerGraphics
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IViewFor<PokerVM>
    {
        PokerVM pokerTable;
        PokerCore.Model.PokerM pokerGame;

        public PokerVM ViewModel { get => pokerTable; set => throw new NotImplementedException(); }
        object IViewFor.ViewModel { get => pokerTable; set => throw new NotImplementedException(); }

        public event PropertyChangedEventHandler PropertyChanged;
        public event ReactiveUI.PropertyChangingEventHandler PropertyChanging;

        public void PokerInitialize(string name, int startbank)
        {
            pokerGame = new PokerCore.Model.PokerM(name, 10, startbank);
            pokerTable = new PokerVM(pokerGame);
            pokerGame.TryConnect("Илья");
        }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            //this.WhenActivated(disposer =>
            //{
            //    //his.WhenAnyValue(t => t).Subscribe(v => DataContext = v).DisposeWith(disposer);
            //});

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
                PokerInitialize(textbox_name.Text, Int32.Parse(textbox_first_bank.Text));
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
