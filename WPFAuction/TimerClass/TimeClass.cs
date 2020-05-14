using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace WPFAuction.TimerClass
{
    public class TimeClass : INotifyPropertyChanged
    {
        public TimeClass()
        {
            MyTime = 20;
            IsEn = false;
        }
        //привязка до лістбоксітема
        private bool _isEn;
        public bool IsEn
        {
            get { return _isEn; }
            set
            {
                _isEn = value;
                OnPropertyChanged("IsEn");
            }
        }

        private int _myTime;

        public int MyTime
        {
            get { return _myTime; }
            set
            {
                _myTime = value;
                OnPropertyChanged("MyTime");
            }
        }
        public void counterOne_Tick(object sender, EventArgs e)
        {
            // code goes here

            if (MyTime > 0)
            {
                MyTime--;
            }
            else
            {
                IsEn = true;
                (sender as DispatcherTimer).Stop();
            }


        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

       
    }
}
