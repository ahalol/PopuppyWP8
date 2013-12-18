using System.Collections.Generic;
using System.Windows.Navigation;
using System.Windows.Controls;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Linq;
using System.Net;
using System;

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using PopupManager.Resources;
using PopupManager.Popuppy;

namespace PopupManager
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void ShowPopup(object s, EventArgs e)
        {
            var vars = new TextParams { Message = "Would you like to have some tea and crumpets in a half of an hour?", Ok = "Yes, please", Cancel = "No thanks" };
            Manager.Show(vars, (se, ev) => Debug.WriteLine("OK"), (se, ev) => Debug.WriteLine("Cancel"));
        }
        protected override void OnBackKeyPress(CancelEventArgs ev)
        {
            if (Manager.ActivePopup.IsOpen)
            {
                ev.Cancel = true;
                if (Manager.ActiveDrag != null)
                    Manager.ActiveDrag.OnCancel(this, ev);
            }
        }
    }
}