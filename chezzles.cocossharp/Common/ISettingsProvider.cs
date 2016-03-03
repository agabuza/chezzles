using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chezzles.cocossharp.Common
{
    public interface ISetttingsProvider
    {
        Task Initialize();
        string this[string param] { get; set; }
        Task SaveAsync();
    }
}
