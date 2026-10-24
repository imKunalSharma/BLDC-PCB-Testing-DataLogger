Imports System.IO
Imports System.IO.Ports
Imports System.Threading
'Imports NModbus
Imports Modbus.Device
Imports Modbus
Imports System.Threading.Tasks

Public Class Form1
    Private lastDataReceived As DateTime

    'Timer2 for Date Time
    'Timer1 and Timer3  for JIG 1
    'Timer4 and Timer5  for JIG 2
    'Timer6 and Timer7  for JIG 3
    'Timer8 and Timer9  for JIG 4
    'Timer10 and Timer11  for JIG 5
    '  for Start, Stop, Resume JIG 

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If MessageBox.Show("Are you sure to close this application?", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            My.Computer.FileSystem.WriteAllText("C:\Reports\log.txt", "Application Close With User Permission - " & "Date : " + System.DateTime.Now.ToString("dd/MM/yyyy") & "  " & "Time : " + System.DateTime.Now.ToString("HH:mm:ss") & Chr(13), True)
            Environment.Exit(0)
        Else
            My.Computer.FileSystem.WriteAllText("C:\Reports\log.txt", "Application Not Close With User Permission - " & "Date : " + System.DateTime.Now.ToString("dd/MM/yyyy") & "  " & "Time : " + System.DateTime.Now.ToString("HH:mm:ss") & Chr(13), True)
            e.Cancel = True
        End If
    End Sub

    Private modbusSerial1 As IModbusSerialMaster
    Private modbusSerial2 As IModbusSerialMaster
    Private modbusSerial3 As IModbusSerialMaster
    Private modbusSerial4 As IModbusSerialMaster
    Private modbusSerial5 As IModbusSerialMaster

    Private FloatArray1(49) As Single ' 100 registers = 50 floats
    Private FloatArray2(49) As Single
    Private FloatArray3(49) As Single
    Private FloatArray4(49) As Single
    Private FloatArray5(49) As Single

    Declare Function RemoveMenu Lib "user32" (ByVal menu As Int32, ByVal Pos As Int32, ByVal u As Int32) As Int32
    Declare Function GetSystemMenu Lib "user32" (ByVal hwnd As Int32, ByVal rseve As Int32) As Int32
    Declare Function WindowFromPoint Lib "user32" (ByVal x As Int64, ByVal y As Int64) As Int32
    Private Const MF_POSITION As Int32 = &H400
    Enum choice
        Disable = 0
        Enable = 1
    End Enum
    Enum Position
        rmclose = 6
    End Enum


    Dim Count1 As Integer = 0
    Dim Count2 As Integer = 0
    Dim Count3 As Integer = 0
    Dim Count4 As Integer = 0
    Dim Count5 As Integer = 0

    Private dataReceived1 As String
    Private dataReceived2 As String
    Private dataReceived3 As String
    Private dataReceived4 As String
    Private dataReceived5 As String

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        Dim menu As Int64
        'disable the close button
        menu = GetSystemMenu(Me.Handle.ToInt32, choice.Disable)
        RemoveMenu(CInt(menu), Position.rmclose, MF_POSITION)


        'datetime
        Timer2.Start()
        AxMSFlexGrid()  'MSFlexGrid Function

        'Application.DoEvents()0
        ViewFuncation(DataGridView1) ' DataGridview And port
        'Application.DoEvents()

        
        RichTextBox1.ScrollToCaret()

        Timer17.Interval = 1000
        Timer17.Enabled = True


        'datagridview 2 view status
        Timer18.Enabled = True
        Timer18.Interval = 200
        ' Form2.Show()

    End Sub

    

    




    
    Dim flag1 As Integer

    Dim t1 As Integer
    Dim WaitingTime As Integer = 3000
    Private Sub Timer2_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Label10.Text = "Date : " + System.DateTime.Now.ToString("dd/MM/yyyy") & "  " & "Time : " + System.DateTime.Now.ToString("HH:mm:ss")

        On Error Resume Next



        If SerialPort1.IsOpen Then
            Dim J As Integer = 1
            If JIG(1, 2) = 1 Then
                If Timer1.Enabled = False Then

                    ToolStripStatusLabel1.Text = SENDCOMMAND(J, "TWF")
                    ToolStripStatusLabel1.Text = SENDCOMMAND(J, "RLN")

                    modbusSerial1 = ModbusSerialMaster.CreateRtu(SerialPort1)
                    modbusSerial1.Transport.ReadTimeout = 35
                    BackgroundWorker1.RunWorkerAsync()

                    Timer12.Start()
                    Timer20.Start()

                End If
            ElseIf JIG(1, 2) = 0 Then
                If Timer1.Enabled = True Then
                    ToolStripStatusLabel1.Text = SENDCOMMAND(J, "RLF")

                    BackgroundWorker1.CancelAsync()

                    wait1(WaitingTime)
                    Timer12.Stop()
                    Timer20.Stop()
                End If
            End If
        End If
        If SerialPort2.IsOpen Then
            Dim J As Integer = 2
            If JIG(2, 2) = 1 Then
                If BackgroundWorker1.IsBusy <> True Then
                    ToolStripStatusLabel2.Text = SENDCOMMAND(J, "TWF")
                    ToolStripStatusLabel2.Text = SENDCOMMAND(J, "RLN")

                    modbusSerial2 = ModbusSerialMaster.CreateRtu(SerialPort2)
                    modbusSerial2.Transport.ReadTimeout = 35
                    BackgroundWorker2.RunWorkerAsync()

                    Timer13.Start()

                    Timer19.Start()

                End If
            ElseIf JIG(2, 2) = 0 Then
                If BackgroundWorker1.IsBusy = True Then
                    ToolStripStatusLabel2.Text = SENDCOMMAND(J, "RLF")
                    BackgroundWorker2.CancelAsync()

                    wait2(WaitingTime)
                    Timer13.Stop()

                    Timer19.Stop()

                End If
            End If
        End If
        If SerialPort3.IsOpen Then
            Dim J As Integer = 3
            If JIG(3, 2) = 1 Then
                If Timer6.Enabled = False Then
                    ToolStripStatusLabel3.Text = SENDCOMMAND(J, "TWF")
                    ToolStripStatusLabel3.Text = SENDCOMMAND(J, "RLN")
                    modbusSerial3 = ModbusSerialMaster.CreateRtu(SerialPort3)
                    modbusSerial3.Transport.ReadTimeout = 35
                    BackgroundWorker3.RunWorkerAsync()
                    Timer14.Start()

                    Timer21.Start()

                End If
            ElseIf JIG(3, 2) = 0 Then
                If Timer6.Enabled = True Then
                    ToolStripStatusLabel3.Text = SENDCOMMAND(J, "RLF")
                    BackgroundWorker3.CancelAsync()
                    wait3(WaitingTime)
                    Timer14.Stop()
                    Timer21.Stop()

                End If
            End If
        End If
        If SerialPort4.IsOpen Then
            Dim J As Integer = 4
            If JIG(4, 2) = 1 Then
                If Timer8.Enabled = False Then
                    ToolStripStatusLabel4.Text = SENDCOMMAND(J, "TWF")
                    ToolStripStatusLabel4.Text = SENDCOMMAND(J, "RLN")
                    modbusSerial4 = ModbusSerialMaster.CreateRtu(SerialPort4)
                    modbusSerial4.Transport.ReadTimeout = 35
                    BackgroundWorker4.RunWorkerAsync()
                    Timer15.Start()
                    Timer22.Start()
                End If
            ElseIf JIG(4, 2) = 0 Then
                If Timer8.Enabled = True Then
                    ToolStripStatusLabel4.Text = SENDCOMMAND(J, "RLF")
                    BackgroundWorker4.CancelAsync()
                    wait4(WaitingTime)
                    Timer15.Stop()
                    Timer22.Stop()
                End If
            End If
        End If
        If SerialPort5.IsOpen Then
            Dim J As Integer = 5
            If JIG(5, 2) = 1 Then
                If Timer10.Enabled = False Then
                    ToolStripStatusLabel5.Text = SENDCOMMAND(J, "TWF")
                    ToolStripStatusLabel5.Text = SENDCOMMAND(J, "RLN")
                    modbusSerial5 = ModbusSerialMaster.CreateRtu(SerialPort5)
                    modbusSerial5.Transport.ReadTimeout = 35
                    BackgroundWorker5.RunWorkerAsync()
                    Timer16.Start()
                    Timer23.Start()
                End If
            ElseIf JIG(5, 2) = 0 Then
                If Timer10.Enabled = True Then
                    ToolStripStatusLabel5.Text = SENDCOMMAND(J, "RLF")
                    BackgroundWorker5.CancelAsync()
                    wait5(WaitingTime)
                    Timer16.Stop()
                    Timer23.Stop()
                End If
            End If
        End If


    End Sub

    Function AxMSFlexGrid() As String

        AxMSFlexGrid1.set_ColWidth(0, 1400)
        AxMSFlexGrid1.set_ColWidth(1, 1400)
        AxMSFlexGrid1.set_ColWidth(2, 1400)
        AxMSFlexGrid1.set_ColWidth(3, 1400)
        AxMSFlexGrid1.set_ColWidth(4, 1400)
        AxMSFlexGrid1.set_ColWidth(5, 1400)

        AxMSFlexGrid1.set_TextMatrix(0, 0, "JIG")
        AxMSFlexGrid1.set_TextMatrix(0, 1, "POSITION")
        AxMSFlexGrid1.set_TextMatrix(0, 2, "VOLTAGE")
        AxMSFlexGrid1.set_TextMatrix(0, 3, "CURRENT")
        AxMSFlexGrid1.set_TextMatrix(0, 4, "WATTAGE")
        AxMSFlexGrid1.set_TextMatrix(0, 5, "PF")

        AxMSFlexGrid1.set_ColAlignment(0, colAlignment:=4)
        AxMSFlexGrid1.set_ColAlignment(1, colAlignment:=4)
        AxMSFlexGrid1.set_ColAlignment(2, colAlignment:=4)
        AxMSFlexGrid1.set_ColAlignment(3, colAlignment:=4)
        AxMSFlexGrid1.set_ColAlignment(4, colAlignment:=4)
        AxMSFlexGrid1.set_ColAlignment(5, colAlignment:=4)

        AxMSFlexGrid2.set_ColWidth(1, 1400)
        AxMSFlexGrid2.set_ColWidth(2, 1400)
        AxMSFlexGrid2.set_ColWidth(3, 1400)
        AxMSFlexGrid2.set_ColWidth(4, 1400)
        AxMSFlexGrid2.set_ColWidth(5, 1400)

        AxMSFlexGrid2.set_TextMatrix(0, 0, "JIG")
        AxMSFlexGrid2.set_TextMatrix(0, 1, "POSITION")
        AxMSFlexGrid2.set_TextMatrix(0, 2, "VOLTAGE")
        AxMSFlexGrid2.set_TextMatrix(0, 3, "CURRENT")
        AxMSFlexGrid2.set_TextMatrix(0, 4, "WATTAGE")
        AxMSFlexGrid2.set_TextMatrix(0, 5, "PF")

        AxMSFlexGrid2.set_ColAlignment(0, colAlignment:=4)
        AxMSFlexGrid2.set_ColAlignment(1, colAlignment:=4)
        AxMSFlexGrid2.set_ColAlignment(2, colAlignment:=4)
        AxMSFlexGrid2.set_ColAlignment(3, colAlignment:=4)
        AxMSFlexGrid2.set_ColAlignment(4, colAlignment:=4)
        AxMSFlexGrid2.set_ColAlignment(5, colAlignment:=4)



        Return Nothing
    End Function
    Public Sub SendDate(ByVal MbusQuery() As Byte)
        If InvokeRequired Then
            Invoke(New Action(Of Object)(AddressOf SendDate), MbusQuery)
        Else
            'RichTextBox1.AppendText("TX - ")
            'RichTextBox1.AppendText(String.Join(" ", MbusQuery))
            'RichTextBox1.AppendText(Environment.NewLine)
        End If
    End Sub

    Public Sub RecDate(ByVal buffer() As Byte)
        If InvokeRequired Then
            Invoke(New Action(Of Object)(AddressOf RecDate), buffer)
        Else
            'RichTextBox1.AppendText("TX - ")
            'RichTextBox1.AppendText(String.Join(" ", buffer))
            'RichTextBox1.AppendText(Environment.NewLine)

        End If
    End Sub
    

    

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)


    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub Timer12_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer12.Tick
        On Error Resume Next

        Dim i As Integer = 1
        Dim joinstr As String
        Dim checkjoinstr As Integer
        joinstr = String.Join("", "0" & CStr(i), JIG(i, 2), JIG(i, 3), JIG(i, 4), JIG(i, 5), JIG(i, 6), JIG(i, 7), JIG(i, 8), JIG(i, 9), JIG(i, 10), JIG(i, 11), JIG(i, 12), JIG(i, 13))
        checkjoinstr = joinstr.Substring(0, 1)
        If strArray(checkjoinstr) <> joinstr Then
            strArray(checkjoinstr) = joinstr

            'Dim x As String = joinstr

            Result(i)

        End If

        ' ToolStripStatusLabel1.Text = SENDCOMMAND(i, "QR?")

    End Sub
    'JIg 2
    Private Sub Timer13_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer13.Tick

        On Error Resume Next

        Dim i As Integer = 2
        Dim joinstr As String
        Dim checkjoinstr As Integer
        joinstr = String.Join("", "0" & CStr(i), JIG(i, 2), JIG(i, 3), JIG(i, 4), JIG(i, 5), JIG(i, 6), JIG(i, 7), JIG(i, 8), JIG(i, 9), JIG(i, 10), JIG(i, 11), JIG(i, 12), JIG(i, 13))
        checkjoinstr = joinstr.Substring(0, 1)
        If strArray(checkjoinstr) <> joinstr Then
            strArray(checkjoinstr) = joinstr

            'Dim x As String = joinstr

            Result(i)

        End If

        ' ToolStripStatusLabel1.Text = SENDCOMMAND(i, "QR?")




    End Sub

    Private Sub Timer14_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer14.Tick

        On Error Resume Next

        Dim i As Integer = 3
        Dim joinstr As String
        Dim checkjoinstr As Integer
        joinstr = String.Join("", "0" & CStr(i), JIG(i, 2), JIG(i, 3), JIG(i, 4), JIG(i, 5), JIG(i, 6), JIG(i, 7), JIG(i, 8), JIG(i, 9), JIG(i, 10), JIG(i, 11), JIG(i, 12), JIG(i, 13))
        checkjoinstr = joinstr.Substring(0, 1)
        If strArray(checkjoinstr) <> joinstr Then
            strArray(checkjoinstr) = joinstr

            'Dim x As String = joinstr

            Result(i)

        End If
        ' ToolStripStatusLabel1.Text = SENDCOMMAND(i, "QR?")


    End Sub

    Private Sub Timer15_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer15.Tick

        On Error Resume Next

        Dim i As Integer = 4
        Dim joinstr As String
        Dim checkjoinstr As Integer
        joinstr = String.Join("", "0" & CStr(i), JIG(i, 2), JIG(i, 3), JIG(i, 4), JIG(i, 5), JIG(i, 6), JIG(i, 7), JIG(i, 8), JIG(i, 9), JIG(i, 10), JIG(i, 11), JIG(i, 12), JIG(i, 13))
        checkjoinstr = joinstr.Substring(0, 1)
        If strArray(checkjoinstr) <> joinstr Then
            strArray(checkjoinstr) = joinstr

            'Dim x As String = joinstr

            Result(i)

        End If
        '
        ' ToolStripStatusLabel1.Text = SENDCOMMAND(i, "QR?")


    End Sub

    Private Sub Timer16_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer16.Tick

        On Error Resume Next

        Dim i As Integer = 5
        Dim joinstr As String
        Dim checkjoinstr As Integer
        joinstr = String.Join("", "0" & CStr(i), JIG(i, 2), JIG(i, 3), JIG(i, 4), JIG(i, 5), JIG(i, 6), JIG(i, 7), JIG(i, 8), JIG(i, 9), JIG(i, 10), JIG(i, 11), JIG(i, 12), JIG(i, 13))
        checkjoinstr = joinstr.Substring(0, 1)
        If strArray(checkjoinstr) <> joinstr Then
            strArray(checkjoinstr) = joinstr

            'Dim x As String = joinstr

            Result(i)

        End If

        ' ToolStripStatusLabel1.Text = SENDCOMMAND(i, "QR?")


    End Sub





    Private Sub Timer17_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer17.Tick

        If JIG(1, 2) = 1 Then
            ToolStripStatusLabel1.Text = SENDCOMMAND("1", "QR?")
        End If
        If JIG(2, 2) = 1 Then
            ToolStripStatusLabel2.Text = SENDCOMMAND("2", "QR?")
        End If
        If JIG(3, 2) = 1 Then
            ToolStripStatusLabel3.Text = SENDCOMMAND("3", "QR?")
        End If
        If JIG(4, 2) = 1 Then
            ToolStripStatusLabel4.Text = SENDCOMMAND("4", "QR?")
        End If
        If JIG(5, 2) = 1 Then
            ToolStripStatusLabel5.Text = SENDCOMMAND("5", "QR?")
        End If

        On Error GoTo exit1
        Label2.Text = ""
        Dim RS As String = ReceiveSerialData()
        If RS <> "" Then
            Label2.Text = RS
            If RS.Substring(3, 1) = 0 Then
                Dim dt As DataTable = GetData("SELECT * FROM `modeldetails` WHERE `F_Status` = '1' and Id = " & CInt(RS.Substring(1, 2)) & "")
                If dt.Rows.Count() > 0 Then
                    Dim Id As String = dt.Rows(0).Item("Id").ToString
                    'Dim Currenttime1 As String = System.DateTime.Now.ToString("hh:mm:ss tt")
                    Dim x1 As String = "UPDATE modeldetails SET F_Status = '0' where Id = '" & Id & "'"    ''''Open Flap
                    MyNonQuery(x1)

                    'Dim x1 As String = "UPDATE modeldetails SET status = '0', endtime = '" & Currenttime1 & "' where Id = '" & Id & "'"
                    'MyNonQuery(x1)
                End If
            ElseIf RS.Substring(3, 1) = 1 Then

                Dim dt As DataTable = GetData("SELECT * FROM `modeldetails` WHERE `F_Status` = '0' and Id = " & CInt(RS.Substring(1, 2)) & "")
                If dt.Rows.Count() > 0 Then
                    Dim Id As String = dt.Rows(0).Item("Id").ToString
                    'Dim Currenttime1 As String = System.DateTime.Now.ToString("hh:mm:ss tt")
                    Dim x1 As String = "UPDATE modeldetails SET F_Status = '1'  where Id = '" & Id & "'"    ''''''''Close Flap
                    MyNonQuery(x1)

                    'Dim x1 As String = "UPDATE modeldetails SET status = '1', resumetime  = '" & Currenttime1 & "', endtime = '0'  where Id = '" & Id & "'"
                    'MyNonQuery(x1)

                End If
            End If
        End If
exit1:

    End Sub

    Private Sub Timer18_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer18.Tick

        'On Error GoTo exit1

        JIGStatus(DataGridView2)



        'exit1:
    End Sub

    'if data not recived then jig 2 relay auto start
    Dim datasend1 As Integer
    Dim datasend2 As Integer
    Dim datasend3 As Integer
    Dim datasend4 As Integer
    Dim datasend5 As Integer
    Private Sub Timer19_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer19.Tick
        'datasend = 0

        datasend2 = datasend2 + 1
        If dataReceived2 <> "" Then
            datasend2 = 0
            ToolStripStatusLabel9.Text = datasend2 & " Data revied"
            dataReceived2 = ""
        Else
            ToolStripStatusLabel9.Text = datasend2 & " Data not revied"
            If datasend2 >= 60 Then
                ToolStripStatusLabel9.Text = datasend2 & " Data not revied"
                ToolStripStatusLabel2.Text = SENDCOMMAND(2, "RLN")
                datasend2 = 0
            End If
        End If

    End Sub


    Function ReceiveSerialData() As String
        ' returns new data from the serial connection
        Dim Incoming As String
        Try
            Incoming = SerialPort6.ReadExisting()
            'If Incoming Is Nothing Then
            '    Return "nothing" & vbCrLf
            'Else
            Return Incoming
            'End If
        Catch ex As TimeoutException
            'Return "Error: Serial Port read timed out."

            My.Computer.FileSystem.WriteAllText("C:\Reports\log.txt", "Data Recived " & ErrorToString() & " " & "Date : " + System.DateTime.Now.ToString("dd/MM/yyyy") & "  " & "Time : " + System.DateTime.Now.ToString("HH:mm:ss") & Chr(13), True)

        End Try
        'Dim datarec As String = ReceiveSerialData()
    End Function

    Private Sub Timer20_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer20.Tick

        datasend1 = datasend1 + 1
        If dataReceived1 <> "" Then
            datasend1 = 0
            dataReceived1 = ""
        Else
            If datasend1 >= 60 Then
                ToolStripStatusLabel1.Text = SENDCOMMAND(1, "RLN")
                datasend1 = 0
            End If
        End If
    End Sub

    Private Sub Timer21_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer21.Tick
        datasend3 = datasend3 + 1
        If dataReceived3 <> "" Then
            datasend3 = 0
            dataReceived3 = ""
        Else
            If datasend3 >= 60 Then
                ToolStripStatusLabel3.Text = SENDCOMMAND(3, "RLN")
                datasend3 = 0
            End If
        End If
    End Sub

    Private Sub Timer22_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer22.Tick
        datasend4 = datasend4 + 1
        If dataReceived4 <> "" Then
            datasend4 = 0
            dataReceived4 = ""
        Else
            If datasend4 >= 60 Then
                ToolStripStatusLabel4.Text = SENDCOMMAND(4, "RLN")
                datasend4 = 0
            End If
        End If
    End Sub

    Private Sub Timer23_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer23.Tick
        datasend5 = datasend5 + 1
        If dataReceived5 <> "" Then
            datasend5 = 0
            dataReceived5 = ""
        Else
            If datasend5 >= 60 Then
                ToolStripStatusLabel5.Text = SENDCOMMAND(5, "RLN")
                datasend5 = 0
            End If
        End If
    End Sub




    'JIG 1
    Private Sub BackgroundWorker1_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Count1 = Count1 + 1
        If Count1 > 10 Then
            Count1 = 1
        End If
        Dim SalveId As Integer
        SalveId = Count1

        AxMSFlexGrid1.set_TextMatrix(SalveId, 0, "JIG1")
        AxMSFlexGrid1.set_TextMatrix(SalveId, 1, SalveId)

        Try


            ' Read holding registers (example: starting at address 0, reading 2 registers)
            Dim registers As UShort() = modbusSerial1.ReadHoldingRegisters(SalveId, Address, Lenght) ' Replace with your slave ID and address ' Replace with your slave ID and address

            ' Convert registers to float values
            For i As Integer = 0 To 49 ' 50 floats (100 registers)
                ' Combine two registers into a single float
                Dim combined As UInteger = (CUInt(registers(i * 2)) << 16) Or CUInt(registers(i * 2 + 1))
                ' Convert to float
                FloatArray1(i) = BitConverter.ToSingle(BitConverter.GetBytes(combined), 0)
            Next


            Voltage = FloatArray1(1).ToString("F4")
            Current = CStr(FloatArray1(7).ToString("F4"))
            Wattage = CStr(FloatArray1(11).ToString("F4"))
            Power_Factor = CStr(FloatArray1(27).ToString("F4"))

            AxMSFlexGrid1.set_TextMatrix(SalveId, 2, Voltage)
            AxMSFlexGrid1.set_TextMatrix(SalveId, 3, Current)
            AxMSFlexGrid1.set_TextMatrix(SalveId, 4, Wattage)
            AxMSFlexGrid1.set_TextMatrix(SalveId, 5, Power_Factor)

            Dim JIGSTR As String = "JIG1"
            Dim FileName As String = JIGSTR & "_P_" & CStr(SalveId)
            Path_RD = "C:\Reports\" + DateTime.Now.ToString("dd MM yyyy") + "\"
            Dim Path_Z As String = Path_RD & JIGSTR & "\"
            CurrentDateTime = System.DateTime.Now.ToString("yyyy-MM-dd") + " " + System.DateTime.Now.ToString("HH:mm:ss")
            Dim data As New List(Of List(Of String)) From {
                New List(Of String) From {"'" & JIG(1, 1) & "'",
                                          "'" & Voltage & "'",
                                          "'" & Current & "'",
                                          "'" & Wattage & "'",
                                          "'" & Power_Factor & "'",
                                          "'" & CurrentDateTime & "'"}
            }
            CreateCsvFile(Path_Z, FileName & ".csv", data)
        Catch ex As TimeoutException
            SalveId = SalveId + 1
        Catch ex As Exception
            ' MsgBox(e.ToString)
        End Try

       

    End Sub
    'JIG 2
    Private Sub BackgroundWorker2_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Count2 = Count2 + 1
        If Count2 > 10 Then
            Count2 = 1
        End If
        Dim SalveId As Integer
        SalveId = Count2
        AxMSFlexGrid1.set_TextMatrix(SalveId + 10, 0, "JIG2")
        AxMSFlexGrid1.set_TextMatrix(SalveId + 10, 1, SalveId)


        Try


            ' Read holding registers (example: starting at address 0, reading 2 registers)
            Dim registers As UShort() = modbusSerial2.ReadHoldingRegisters(SalveId, Address, Lenght) ' Replace with your slave ID and address ' Replace with your slave ID and address

            ' Convert registers to float values
            For i As Integer = 0 To 49 ' 50 floats (100 registers)
                ' Combine two registers into a single float
                Dim combined As UInteger = (CUInt(registers(i * 2)) << 16) Or CUInt(registers(i * 2 + 1))
                ' Convert to float
                FloatArray2(i) = BitConverter.ToSingle(BitConverter.GetBytes(combined), 0)
            Next


            Voltage = FloatArray2(1).ToString("F4")
            Current = CStr(FloatArray2(7).ToString("F4"))
            Wattage = CStr(FloatArray2(11).ToString("F4"))
            Power_Factor = CStr(FloatArray2(27).ToString("F4"))

            AxMSFlexGrid1.set_TextMatrix(SalveId + 10, 2, Voltage)
            AxMSFlexGrid1.set_TextMatrix(SalveId + 10, 3, Current)
            AxMSFlexGrid1.set_TextMatrix(SalveId + 10, 4, Wattage)
            AxMSFlexGrid1.set_TextMatrix(SalveId + 10, 5, Power_Factor)

            Dim JIGSTR As String = "JIG2"
            Dim FileName As String = JIGSTR & "_P_" & CStr(SalveId)
            Path_RD = "C:\Reports\" + DateTime.Now.ToString("dd MM yyyy") + "\"
            Dim Path_Z As String = Path_RD & JIGSTR & "\"
            CurrentDateTime = System.DateTime.Now.ToString("yyyy-MM-dd") + " " + System.DateTime.Now.ToString("HH:mm:ss")
            Dim data As New List(Of List(Of String)) From {
                New List(Of String) From {"'" & JIG(2, 1) & "'",
                                          "'" & Voltage & "'",
                                          "'" & Current & "'",
                                          "'" & Wattage & "'",
                                          "'" & Power_Factor & "'",
                                          "'" & CurrentDateTime & "'"}
            }
            CreateCsvFile(Path_Z, FileName & ".csv", data)
        Catch ex As TimeoutException
            SalveId = SalveId + 1
        Catch ex As Exception
            'MsgBox(e.ToString)
        End Try

    End Sub
    'JIG 3
    Private Sub BackgroundWorker3_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker3.DoWork
        Count3 = Count3 + 1
        If Count3 > 10 Then
            Count3 = 1
        End If

        Dim SalveId As Integer
        'r = 1
        SalveId = Count3
        Dim Row As Integer = CInt(SalveId) + 20
        AxMSFlexGrid1.set_TextMatrix(Row, 0, CStr("JIG3"))
        AxMSFlexGrid1.set_TextMatrix(Row, 1, SalveId)

        Try


            ' Read holding registers (example: starting at address 0, reading 2 registers)
            Dim registers As UShort() = modbusSerial3.ReadHoldingRegisters(SalveId, Address, Lenght) ' Replace with your slave ID and address ' Replace with your slave ID and address

            ' Convert registers to float values
            For i As Integer = 0 To 49 ' 50 floats (100 registers)
                ' Combine two registers into a single float
                Dim combined As UInteger = (CUInt(registers(i * 2)) << 16) Or CUInt(registers(i * 2 + 1))
                ' Convert to float
                FloatArray3(i) = BitConverter.ToSingle(BitConverter.GetBytes(combined), 0)
            Next


            Voltage = FloatArray3(1).ToString("F4")
            Current = CStr(FloatArray3(7).ToString("F4"))
            Wattage = CStr(FloatArray3(11).ToString("F4"))
            Power_Factor = CStr(FloatArray3(27).ToString("F4"))


            AxMSFlexGrid1.set_TextMatrix(CInt(SalveId) + 20, 2, Voltage)
            AxMSFlexGrid1.set_TextMatrix(CInt(SalveId) + 20, 3, Current)
            AxMSFlexGrid1.set_TextMatrix(CInt(SalveId) + 20, 4, Wattage)
            AxMSFlexGrid1.set_TextMatrix(CInt(SalveId) + 20, 5, Power_Factor)

            Dim JIGSTR As String = "JIG3"
            Dim FileName As String = JIGSTR & "_P_" & CStr(SalveId)
            Path_RD = "C:\Reports\" + DateTime.Now.ToString("dd MM yyyy") + "\"
            Dim Path_Z As String = Path_RD & JIGSTR & "\"
            CurrentDateTime = System.DateTime.Now.ToString("yyyy-MM-dd") + " " + System.DateTime.Now.ToString("HH:mm:ss")
            Dim data As New List(Of List(Of String)) From {
                New List(Of String) From {"'" & JIG(3, 1) & "'",
                                          "'" & Voltage & "'",
                                          "'" & Current & "'",
                                          "'" & Wattage & "'",
                                          "'" & Power_Factor & "'",
                                          "'" & CurrentDateTime & "'"}
            }
            CreateCsvFile(Path_Z, FileName & ".csv", data)


        Catch ex As TimeoutException
            SalveId = SalveId + 1
        Catch ex As Exception
            'MsgBox(e.ToString)
        End Try

    End Sub
    'JIG 4
    Private Sub BackgroundWorker4_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker4.DoWork
        Count4 = Count4 + 1
        If Count4 > 10 Then
            Count4 = 1
        End If
        Dim SalveId As Integer
        'r = 1
        SalveId = Count4
        AxMSFlexGrid2.set_TextMatrix(SalveId, 0, "JIG4")
        AxMSFlexGrid2.set_TextMatrix(SalveId, 1, SalveId)

        Try


            ' Read holding registers (example: starting at address 0, reading 2 registers)
            Dim registers As UShort() = modbusSerial4.ReadHoldingRegisters(SalveId, Address, Lenght) ' Replace with your slave ID and address ' Replace with your slave ID and address

            ' Convert registers to float values
            For i As Integer = 0 To 49 ' 50 floats (100 registers)
                ' Combine two registers into a single float
                Dim combined As UInteger = (CUInt(registers(i * 2)) << 16) Or CUInt(registers(i * 2 + 1))
                ' Convert to float
                FloatArray4(i) = BitConverter.ToSingle(BitConverter.GetBytes(combined), 0)
            Next


            Voltage = FloatArray4(1).ToString("F4")
            Current = CStr(FloatArray4(7).ToString("F4"))
            Wattage = CStr(FloatArray4(11).ToString("F4"))
            Power_Factor = CStr(FloatArray4(27).ToString("F4"))

            AxMSFlexGrid2.set_TextMatrix(SalveId, 2, Voltage)
            AxMSFlexGrid2.set_TextMatrix(SalveId, 3, Current)
            AxMSFlexGrid2.set_TextMatrix(SalveId, 4, Wattage)
            AxMSFlexGrid2.set_TextMatrix(SalveId, 5, Power_Factor)

            Dim JIGSTR As String = "JIG4"
            Dim FileName As String = JIGSTR & "_P_" & CStr(SalveId)
            Path_RD = "C:\Reports\" + DateTime.Now.ToString("dd MM yyyy") + "\"
            Dim Path_Z As String = Path_RD & JIGSTR & "\"
            CurrentDateTime = System.DateTime.Now.ToString("yyyy-MM-dd") + " " + System.DateTime.Now.ToString("HH:mm:ss")
            Dim data As New List(Of List(Of String)) From {
                New List(Of String) From {"'" & JIG(4, 1) & "'",
                                          "'" & Voltage & "'",
                                          "'" & Current & "'",
                                          "'" & Wattage & "'",
                                          "'" & Power_Factor & "'",
                                          "'" & CurrentDateTime & "'"}
            }
            CreateCsvFile(Path_Z, FileName & ".csv", data)


        Catch ex As TimeoutException
            SalveId = SalveId + 1
        Catch ex As Exception
            'MsgBox(e.ToString)
        End Try

    End Sub
    'JIG 5
    Private Sub BackgroundWorker5_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker5.DoWork
        Count5 = Count5 + 1
        If Count5 > 10 Then
            Count5 = 1
        End If

        Dim SalveId As Integer
        'r = 1
        SalveId = Count5
        AxMSFlexGrid2.set_TextMatrix(SalveId + 10, 0, "JIG5")
        AxMSFlexGrid2.set_TextMatrix(SalveId + 10, 1, SalveId)


        Try


            ' Read holding registers (example: starting at address 0, reading 2 registers)
            Dim registers As UShort() = modbusSerial5.ReadHoldingRegisters(SalveId, Address, Lenght) ' Replace with your slave ID and address ' Replace with your slave ID and address

            ' Convert registers to float values
            For i As Integer = 0 To 49 ' 50 floats (100 registers)
                ' Combine two registers into a single float
                Dim combined As UInteger = (CUInt(registers(i * 2)) << 16) Or CUInt(registers(i * 2 + 1))
                ' Convert to float
                FloatArray5(i) = BitConverter.ToSingle(BitConverter.GetBytes(combined), 0)
            Next


            Voltage = FloatArray5(1).ToString("F4")
            Current = CStr(FloatArray5(7).ToString("F4"))
            Wattage = CStr(FloatArray5(11).ToString("F4"))
            Power_Factor = CStr(FloatArray5(27).ToString("F4"))

            AxMSFlexGrid2.set_TextMatrix(SalveId + 10, 2, Voltage)
            AxMSFlexGrid2.set_TextMatrix(SalveId + 10, 3, Current)
            AxMSFlexGrid2.set_TextMatrix(SalveId + 10, 4, Wattage)
            AxMSFlexGrid2.set_TextMatrix(SalveId + 10, 5, Power_Factor)

            Dim JIGSTR As String = "JIG5"
            Dim FileName As String = JIGSTR & "_P_" & CStr(SalveId)
            Path_RD = "C:\Reports\" + DateTime.Now.ToString("dd MM yyyy") + "\"
            Dim Path_Z As String = Path_RD & JIGSTR & "\"
            CurrentDateTime = System.DateTime.Now.ToString("yyyy-MM-dd") + " " + System.DateTime.Now.ToString("HH:mm:ss")
            Dim data As New List(Of List(Of String)) From {
                New List(Of String) From {"'" & JIG(5, 1) & "'",
                                          "'" & Voltage & "'",
                                          "'" & Current & "'",
                                          "'" & Wattage & "'",
                                          "'" & Power_Factor & "'",
                                          "'" & CurrentDateTime & "'"}
            }
            CreateCsvFile(Path_Z, FileName & ".csv", data)


        Catch ex As TimeoutException
            SalveId = SalveId + 1
        Catch ex As Exception
            ' MsgBox(e.ToString)
        End Try

    End Sub
End Class