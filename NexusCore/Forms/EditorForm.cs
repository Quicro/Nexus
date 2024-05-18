using NexusCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusCore.Forms
{
    public class EditorForm : IForm
    {
        public IController controller { get; set ; }

        public event EventHandler OnOpen;
        public event EventHandler OnClose;
        public event EventHandler OnDataLoading;
        public event EventHandler OnDataLoaded;
        public event EventHandler OnDataLoadCancelled;

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void End()
        {
            throw new NotImplementedException();
        }

        public void LoadData()
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            throw new NotImplementedException();
        }

        public void Start(List<MenuItem> menu)
        {
            throw new NotImplementedException();
        }
    }
}
