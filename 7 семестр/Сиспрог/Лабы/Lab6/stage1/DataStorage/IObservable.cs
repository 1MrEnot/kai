using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.DataStorage
{
    public interface IObservable<T>
    {
        void Subscribe(IObserver<T> observer);
    }
}
