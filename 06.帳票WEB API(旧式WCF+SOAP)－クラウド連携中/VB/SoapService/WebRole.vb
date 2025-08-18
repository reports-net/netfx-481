Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports Microsoft.WindowsAzure
Imports Microsoft.WindowsAzure.ServiceRuntime

Public Class WebRole
    Inherits RoleEntryPoint

    Public Overrides Function OnStart() As Boolean

        ' 讒区・縺ｮ螟画峩繧貞・逅・☆繧区婿豕輔↓縺､縺・※縺ｯ縲・
        ' MSDN 繝医ヴ繝・け (http://go.microsoft.com/fwlink/?LinkId=166357) 繧貞盾辣ｧ縺励※縺上□縺輔＞縲・

        Return MyBase.OnStart()

    End Function

End Class

