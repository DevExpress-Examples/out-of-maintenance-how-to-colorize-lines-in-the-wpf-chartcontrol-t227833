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

        Private Sub Canvas_SizeChanged(ByVal sender As Object, ByVal e As SizeChangedEventArgs)
            DrawOverChart()
        End Sub
        Private Sub DrawOverChart()
            Canvas.Children.Clear()
            Dim diagram As XYDiagram2D = (CType(chartControl1.Diagram, XYDiagram2D))

            For Each s As Series In chartControl1.Diagram.Series
                For i As Integer = 0 To s.Points.Count - 1
                    If i < s.Points.Count - 1 Then
                        Dim screenPoint1 As Point = diagram.DiagramToPoint(s.Points(i).NumericalArgument, s.Points(i).Value).Point
                        Dim screenPoint2 As Point = diagram.DiagramToPoint(s.Points(i + 1).NumericalArgument, s.Points(i + 1).Value).Point

                        If IsValidPoints(screenPoint1, screenPoint2) Then
                            Canvas.Children.Add(CreateLine(screenPoint1, screenPoint2, GetBrush(s.Points(i), s)))
                        End If
                    End If
                Next i
            Next s
        End Sub

        Private Function GetBrush(ByVal point1 As SeriesPoint, ByVal series As Series) As SolidColorBrush
            Select Case series.DisplayName
                Case "0"
                    If point1.NumericalArgument < 5 Then
                        Return Brushes.Red
                    Else
                        Return Brushes.Pink
                    End If
                Case "1"
                    If point1.NumericalArgument < 5 Then
                        Return Brushes.Blue
                    Else
                        Return Brushes.Aqua
                    End If
            End Select
            Return Brushes.Black
        End Function

        Private Function IsValidPoints(ByVal screenPoint1 As Point, ByVal screenPoint2 As Point) As Boolean
            Return (Not Double.IsInfinity(screenPoint1.X)) AndAlso (Not Double.IsInfinity(screenPoint1.Y)) AndAlso (Not Double.IsInfinity(screenPoint2.X)) AndAlso Not Double.IsInfinity(screenPoint2.Y)
        End Function

        Private Function CreateLine(ByVal screenPoint1 As Point, ByVal screenPoint2 As Point, ByVal brush As SolidColorBrush) As Line
            Dim line = New Line() With {.X1 = screenPoint1.X, .X2 = screenPoint2.X, .Y1 = screenPoint1.Y, .Y2 = screenPoint2.Y, .Stroke = brush, .StrokeThickness = 3}
            Return line
        End Function
    End Class
End Namespace
