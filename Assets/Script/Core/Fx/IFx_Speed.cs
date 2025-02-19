using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Core.Fx
{
    public interface IFx_Speed
    {
        float Time { get; set; }
        float CurrentSp { get; set; }
        public void RegisterSpeed(ref Action<float> action);

        //action += (f) => CurrentSp = f;
    }
}
