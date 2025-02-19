using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using static SingletonTool;
namespace Client
{
    public class TopCanvas : SigMono<TopCanvas>
    {
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
