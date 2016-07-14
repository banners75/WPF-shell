using System;
using System.Windows;

namespace Infrastructure.PopupBehaviour
{
    public interface IWindow
    {
        event EventHandler Closed;

        object Content { get; set; }
       
        object Owner { get; set; }

        Style Style { get; set; }

        void Show();

        void Close();
    }
}