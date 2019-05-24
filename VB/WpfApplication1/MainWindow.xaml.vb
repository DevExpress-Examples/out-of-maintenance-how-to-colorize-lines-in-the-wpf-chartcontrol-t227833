Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Linq
Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Shapes
Imports DevExpress.Xpf.Charts

Namespace WpfApplication1
	''' <summary>
	''' Interaction logic for MainWindow.xaml
	''' </summary>
	Partial Public Class MainWindow
		Inherits Window

		Public Sub New()
			InitializeComponent()

			UpdateDataSource()
			InitializeChartControl()
		End Sub
		Private Function CreateData(ByVal rows As Integer) As DataTable
			Dim r As New Random()

			Dim dt As New DataTable()
			dt.Columns.Add("Ser", GetType(Integer))
			dt.Columns.Add("Arg", GetType(Integer))
			dt.Columns.Add("Value", GetType(Integer))
			For i As Integer = 0 To rows - 1
				dt.Rows.Add(0, i, r.Next(100))
				dt.Rows.Add(1, i, r.Next(100))
			Next i
			Return dt
		End Function
		Private Sub InitializeChartControl()
			chartControl1.Diagram.Series.Clear()
			chartControl1.Diagram.SeriesDataMember = "Ser"
			chartControl1.Diagram.SeriesTemplate.ValueDataMember = "Value"
			chartControl1.Diagram.SeriesTemplate.ArgumentDataMember = "Arg"
		End Sub
		Private Sub UpdateDataSource()
			chartControl1.DataSource = CreateData(10)
		End Sub
	End Class
End Namespace
