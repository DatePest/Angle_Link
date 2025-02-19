using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.Core.UI
{
    public interface IUi_Switch  : IDisposable
    {
        public Action OnShow { get; set; }
        public Action OnHide { get; set; }
        public GameObject TargetObj { get; }
        public abstract void ActiveSwitch(bool b);
    }
}
