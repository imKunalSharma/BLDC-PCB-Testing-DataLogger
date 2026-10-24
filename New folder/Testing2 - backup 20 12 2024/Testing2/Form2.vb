Imports System.IO.Ports

Public Class Form2

    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Label1.Text = PortConnect1("COM8")
        Timer1.Enabled = True
        Timer1.Interval = 1000
        Label2.Text = ""
    End Sub

    Public Function PortConnect1(ByRef Port As String) As String
        Try

            SerialPort1 = New IO.Ports.SerialPort(Port, 9600, IO.Ports.Parity.None, 8, IO.Ports.StopBits.One)
            SerialPort1.Open()

            Return Port & " is Connected, 1"

        Catch ex As Exception
            Return ErrorToString() & ", 0"
        End Try

    End Function


    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim cmmd As String = "$02QR?"
        Form1.SerialPort6.Write(cmmd & vbCr)
        RichTextBox1.AppendText(cmmd & Chr(13))
    End Sub

    Public buff_rx As Byte
    Public Sub SerialPort1_DataReceived(ByVal sender As Object, ByVal e As SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        Form1.SerialPort6.ReadTimeout = 20
        Do While Form1.SerialPort6.BytesToRead > 0
            Try
                buff_rx = Form1.SerialPort6.ReadByte
                If (buff_rx <> 0) Then
                    Me.Invoke(New EventHandler(AddressOf update_dat))
                End If
            Catch ex As Exception

            End Try
            ' Application.DoEvents()
        Loop

    End Sub

    Public Sub update_dat(ByVal sender As Object, ByVal e As System.EventArgs)


        Dim i As Integer = buff_rx
        Dim s As String = ""
        s = i.ToString("X2") 'if wanted to be in hex, use "X2"
        Dim x As String = Convert.ToChar(buff_rx).ToString()

        If x = Chr(13) Then

            Dim RS As String = Label2.Text
            Label2.Text = ""
            RichTextBox1.AppendText(RS & Chr(13))
            RS = ""
            x = ""
        Else

            Label2.Text = Label2.Text + x.Trim
        End If
    End Sub


End Class