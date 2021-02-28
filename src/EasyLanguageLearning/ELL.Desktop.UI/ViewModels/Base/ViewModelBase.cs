using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ELL.Desktop.UI.ViewModels.Base
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void Dispose()
        {
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void IDisposable.Dispose()
        {
            this.Dispose();
        }
    }
}
