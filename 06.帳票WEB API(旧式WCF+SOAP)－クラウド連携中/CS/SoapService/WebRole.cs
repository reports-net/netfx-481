using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace WCFServiceWebRole1
{
    public class WebRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
            // 讒区・縺ｮ螟画峩繧貞・逅・☆繧区婿豕輔↓縺､縺・※縺ｯ縲・
            // MSDN 繝医ヴ繝・け (http://go.microsoft.com/fwlink/?LinkId=166357) 繧貞盾辣ｧ縺励※縺上□縺輔＞縲・

            return base.OnStart();
        }
    }
}
