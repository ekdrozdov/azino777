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

namespace PokerGraphics
{
    /// <summary>
    /// Логика взаимодействия для UserInfo.xaml
    /// </summary>
    public partial class UserInfo : UserControl
    {
        public UserInfo()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty name_u = DependencyProperty.Register(
    "uName",
    typeof(string),
    typeof(UserInfo));

        public string uName
        {
            get { return (string)GetValue(name_u); }
            set { SetValue(name_u, value); }
        }

        public static readonly DependencyProperty cash_u = DependencyProperty.Register(
    "Cash",
    typeof(string),
    typeof(UserInfo));

        public string Cash
        {
            get { return (string)GetValue(cash_u); }
            set { SetValue(cash_u, value); }
        }
    }
}
