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
    interface graph
    {
        // Имя игрока. Используется в панели table и menu
        string NamePlayer { get; set; }

        #region ДЛЯ ПАНЕЛИ С ИМЕНЕМ menu

        #region СВОЙСТВА
        Visibility ShowMenu { get; set; }
        #endregion

        #region МЕТОДЫ 
        void StartClick();
        void Exit();
        #endregion

        #endregion

        //-------------------------
        #region grid_sett

        #region СВОЙСТВА
        Visibility ShowMenuSett { get; set; }
        int FirstBank { get; set; }
        #endregion

        #region МЕТОДЫ 
        bool Done();
        #endregion

        #endregion

        //-------------------------
        #region table

        #region СВОЙСТВА
        Visibility ShowTable { get; set; }
        int ShowBigBland { get; set; }
        int ShowLittleBland { get; set; }
        int MoneyInMainBank { get; set; }
        int NumOfMoney { get; set; }
        #endregion

        #region МЕТОДЫ 
        int ShowCash();
        int ShowCash(object o);
        void Equalize();
        int AddMoney();
        int Extract();
        void AddPlayer(object o);
        #endregion

        #endregion
    }
}
