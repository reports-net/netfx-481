Imports System
Imports System.Windows

Namespace Sample
    Friend NotInheritable Class Program
        <STAThread()>
        Public Shared Sub Main()
            Dim app As New Application()
            Dim window As New Window1()
            app.Run(window)
        End Sub
    End Class
End Namespace