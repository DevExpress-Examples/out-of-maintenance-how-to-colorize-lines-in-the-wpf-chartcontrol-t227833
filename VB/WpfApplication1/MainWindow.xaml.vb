Imports System
Imports System.Data
Imports System.Windows

Namespace WpfApplication1

    ''' <summary>
    ''' Interaction logic for MainWindow.xaml
    ''' </summary>
    Public Partial Class MainWindow
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
            UpdateDataSource()
            InitializeChartControl()
        End Sub

        Private Function CreateData(ByVal rows As Integer) As DataTable
            Dim r As Random = New Random()
            Dim dt As DataTable = New DataTable()
            dt.Columns.Add("Ser", GetType(Integer))
            dt.Columns.Add("Arg", GetType(Integer))
            dt.Columns.Add("Value", GetType(Integer))
            For i As Integer = 0 To rows - 1
                dt.Rows.Add(0, i, r.Next(100))
                dt.Rows.Add(1, i, r.Next(100))
            Next

            Return dt
        End Function

        Private Sub InitializeChartControl()
            Me.chartControl1.Diagram.Series.Clear()
            Me.chartControl1.Diagram.SeriesDataMember = "Ser"
            Me.chartControl1.Diagram.SeriesTemplate.ValueDataMember = "Value"
            Me.chartControl1.Diagram.SeriesTemplate.ArgumentDataMember = "Arg"
        End Sub

        Private Sub UpdateDataSource()
            Me.chartControl1.DataSource = CreateData(10)
        End Sub
    End Class
End Namespace
