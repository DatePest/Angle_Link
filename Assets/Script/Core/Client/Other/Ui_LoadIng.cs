using EventSystemTool;
using System;
using System.IO;
using UnityEngine;

namespace Client
{
    public class Ui_LoadIng : SingletonTool.SigMono<Ui_LoadIng>
    {
        protected override void Awake()
        {
            base.Awake();
            gameObject.SetActive(false);
        }

        public void Show(bool visible)
        {
            gameObject.SetActive(visible);
        }
    }
   
}
