Imports System.IO
Imports System.IO.Ports
Imports System.Threading

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


        'datagridview 2 view status
        Timer18.Enabled = True
        Timer18.Interval = 300


        'datetime
        Timer2.Start()
        AxMSFlexGrid()  'MSFlexGrid Function

        'Application.DoEvents()0
        ViewFuncation(DataGridView1) ' DataGridview And port
        'Application.DoEvents()

        Button2.Text = "Stop"
        Button2.ForeColor = Color.Red
        RichTextBox1.ScrollToCaret()

        Timer17.Interval = 500
        Timer17.Enabled = True


        
        ' Form2.Show()

    End Sub

    Public Function SendQuery(ByVal Serial As SerialPort, ByVal SalveId As Byte, ByVal Address As UShort, ByVal Lenght As UShort)

        On Error Resume Next

        Dim MbusQuery(7) As Byte
        StartLow = Val(Address) Mod 256       'Address
        StartHigh = Val(Address) \ 256
        LengthLow = Val(Lenght) Mod 256      'Lenght
        LengthHigh = Val(Lenght) \ 256
        MbusQuery(0) = SalveId
        MbusQuery(1) = "03"
        MbusQuery(2) = StartHigh
        MbusQuery(3) = StartLow
        MbusQuery(4) = LengthHigh
        MbusQuery(5) = LengthLow
        Dim crc As Byte() = Me.CRC(MbusQuery)
        MbusQuery(6) = crc(0)
        MbusQuery(7) = crc(1)
        Serial.Write(MbusQuery, 0, MbusQuery.Length)
        SendDate(MbusQuery)

        Return Nothing
    End Function

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        On Error Resume Next

        Count1 = Count1 + 1
        If Count1 > 10 Then
            Count1 = 1
        End If

        'r = 1
        SalveId = Count1
        AxMSFlexGrid1.set_TextMatrix(SalveId, 0, "JIG1")
        AxMSFlexGrid1.set_TextMatrix(SalveId, 1, SalveId)
        SendQuery(SerialPort1, SalveId, Address, Lenght)
        'Call delay()
        Timer3.Enabled = True
        Timer3.Interval = 250

    End Sub




    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Count1 = 1
        SalveId = Count1
        AxMSFlexGrid1.set_TextMatrix(SalveId, 0, "JIG1")
        AxMSFlexGrid1.set_TextMatrix(SalveId, 1, SalveId)
        SendQuery(SerialPort1, SalveId, Address, Lenght)
        'Call delay()
        Timer3.Enabled = True
        Timer3.Interval = 250

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Timer1.Enabled = True Then
            Button2.Text = "Start"
            Button2.ForeColor = Color.Green
            Timer1.Enabled = False

        Else
            Button2.Text = "Stop"
            Button2.ForeColor = Color.Red
            Timer1.Enabled = True
        End If
    End Sub
    Dim flag1 As Integer
    Dim WaitingTime As Integer = 3000
    Dim t1 As Integer

    Private Sub Timer2_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Label10.Text = "Date : " + System.DateTime.Now.ToString("dd/MM/yyyy") & "  " & "Time : " + System.DateTime.Now.ToString("HH:mm:ss")

        On Error Resume Next



        If SerialPort1.IsOpen Then
            Dim J As Integer = 1
            If JIG(1, 2) = 1 Then
                If Timer1.Enabled = False Then

                    ToolStripStatusLabel1.Text = SENDCOMMAND(J, "TWF")
                    ToolStripStatusLabel1.Text = SENDCOMMAND(J, "RLN")

                    Timer1.Interval = 400
                    Timer1.Enabled = True


                    'Timer12.Start()

                    'Timer20.Start()
                    Timer12.Interval = 500
                    Timer12.Enabled = True

                    Timer20.Interval = 500
                    Timer20.Enabled = True
                End If
            Else
                If Timer1.Enabled = True Then
                    ToolStripStatusLabel1.Text = SENDCOMMAND(J, "RLF")

                    Timer1.Enabled = False

                    wait1(WaitingTime)
                    'Timer12.Stop()
                    'Timer20.Stop()

                    Timer12.Enabled = False
                    Timer20.Enabled = False

                End If
            End If
        End If
        If SerialPort2.IsOpen Then
            Dim J As Integer = 2
            If JIG(2, 2) = 1 Then
                If Timer4.Enabled = False Then
                    ToolStripStatusLabel2.Text = SENDCOMMAND(J, "TWF")
                    ToolStripStatusLabel2.Text = SENDCOMMAND(J, "RLN")
                    Timer4.Interval = 400
                    Timer4.Enabled = True
                    'Timer13.Start()

                    'Timer19.Start()


                    Timer13.Interval = 500
                    Timer13.Enabled = True

                    Timer19.Interval = 500
                    Timer19.Enabled = True


                End If
            Else
                If Timer4.Enabled = True Then
                    ToolStripStatusLabel2.Text = SENDCOMMAND(J, "RLF")
                    Timer4.Enabled = False

                    wait2(WaitingTime)

                    'Timer13.Stop()
                    'Timer19.Stop()
                    Timer13.Enabled = False
                    Timer19.Enabled = False
                End If
            End If
        End If
        If SerialPort3.IsOpen Then
            Dim J As Integer = 3
            If JIG(3, 2) = 1 Then
                If Timer6.Enabled = False Then
                    ToolStripStatusLabel3.Text = SENDCOMMAND(J, "TWF")
                    ToolStripStatusLabel3.Text = SENDCOMMAND(J, "RLN")
                    Timer6.Interval = 400
                    Timer6.Enabled = True
                    'Timer14.Start()
                    'Timer21.Start()

                    Timer14.Interval = 500
                    Timer14.Enabled = True

                    Timer21.Interval = 500
                    Timer21.Enabled = True

                End If
            Else
                If Timer6.Enabled = True Then
                    ToolStripStatusLabel3.Text = SENDCOMMAND(J, "RLF")
                    Timer6.Enabled = False
                    wait3(WaitingTime)
                    'Timer14.Stop()
                    'Timer21.Stop()

                    Timer14.Enabled = False
                    Timer21.Enabled = False

                End If
            End If
        End If
        If SerialPort4.IsOpen Then
            Dim J As Integer = 4
            If JIG(4, 2) = 1 Then
                If Timer8.Enabled = False Then
                    ToolStripStatusLabel4.Text = SENDCOMMAND(J, "TWF")
                    ToolStripStatusLabel4.Text = SENDCOMMAND(J, "RLN")
                    Timer8.Interval = 400
                    Timer8.Enabled = True
                    'Timer15.Start()
                    'Timer22.Start()

                    Timer15.Interval = 500
                    Timer15.Enabled = True

                    Timer22.Interval = 500
                    Timer22.Enabled = True

                End If
            Else
                If Timer8.Enabled = True Then
                    ToolStripStatusLabel4.Text = SENDCOMMAND(J, "RLF")
                    Timer8.Enabled = False
                    wait4(WaitingTime)
                    'Timer15.Stop()
                    'Timer22.Stop()

                    Timer15.Enabled = False
                    Timer22.Enabled = False

                End If
            End If
        End If
        If SerialPort5.IsOpen Then
            Dim J As Integer = 5
            If JIG(5, 2) = 1 Then
                If Timer10.Enabled = False Then
                    ToolStripStatusLabel5.Text = SENDCOMMAND(J, "TWF")
                    ToolStripStatusLabel5.Text = SENDCOMMAND(J, "RLN")
                    Timer10.Interval = 400
                    Timer10.Enabled = True
                    'Timer16.Start()
                    'Timer23.Start()
                    Timer16.Interval = 500
                    Timer16.Enabled = True

                    Timer23.Interval = 500
                    Timer23.Enabled = True
                End If
            Else
                If Timer10.Enabled = True Then
                    ToolStripStatusLabel5.Text = SENDCOMMAND(J, "RLF")
                    Timer10.Enabled = False
                    wait5(WaitingTime)
                    'Timer16.Stop()
                    'Timer23.Stop()
                    Timer16.Enabled = False
                    Timer23.Enabled = False
                End If
            End If
        End If


    End Sub


    Private Sub Timer3_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer3.Tick

        On Error Resume Next

        Dim c1 As Integer
        Dim c2 As Integer

        'RichTextBox2.Clear()
        If SerialPort1.BytesToRead > 0 Then

            dataReceived1 = SerialPort1.BytesToRead

            Dim bytes As Integer = SerialPort1.BytesToRead
            Dim buffer As Byte() = New Byte(bytes - 1) {}
            SerialPort1.Read(buffer, 0, bytes)
            Dim ReceivedMessage As Byte() = New Byte(bytes - 1) {}
            Array.Copy(buffer, ReceivedMessage, bytes)

            RecDate(buffer)

            If buffer.Length = 205 Then

                c1 = 1001
                c2 = 0
                SalveId = buffer(0)
                For v1 = 3 To buffer.Length - 3 Step 4

                    ''If c2 = 45 Then
                    ''    vIN = New Byte() {buffer(v1 + 3), buffer(v1 + 2), buffer(v1 + 1), buffer(v1)}
                    ''End If

                    vIN = New Byte() {buffer(v1 + 3), buffer(v1 + 2), buffer(v1 + 1), buffer(v1)}
                    Dim s1 As Single = BitConverter.ToSingle(vIN, 0)
                    'RichTextBox2.AppendText(c1 & "      " & Math.Round(Convert.ToDouble(s1), 2) & Environment.NewLine)

                    Arr1(c2) = Math.Round(Convert.ToDouble(s1), 2)

                    c1 = c1 + 1
                    c2 = c2 + 1

                Next
                Voltage = CStr(Arr1(1))
                Current = CStr(Arr1(7))
                Wattage = CStr(Arr1(11))
                THD_Voltage = CStr(Arr1(45))
                Power_Factor = CStr(Arr1(27))

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


                'Dim dt As DataTable = GetData("SELECT * FROM `masterjig` WHERE `JIGNO` = '" & JIGSTR & "' and `POS` = '" & CStr(SalveId) & "'  and `Status`= '1'")
                'If dt.Rows.Count() > 0 Then

                'Else
                '    MyNonQuery("UPDATE `masterjig` SET `Model`='" & JIG(1, 1) & "',`Voltage`='" & Voltage & "',`Current`='" & Current & "',`Wattage`='" & Wattage & "',`Power Factor`='" & Power_Factor & "' WHERE `JIGNO`='" & JIGSTR & "' and `POS` = '" & CStr(SalveId) & "'")

                'End If



            End If
        End If

        Timer3.Enabled = False
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
    ' Modbus CRC calculation in VB (Tested with success).
    Public Function CRC(ByVal data As Byte()) As Byte()
        Dim CRCFull As UShort = &HFFFF
        Dim CRCHigh As Byte = &HF, CRCLow As Byte = &HFF
        Dim CRCLSB As Char
        Dim result As Byte() = New Byte(1) {}
        For i As Integer = 0 To (data.Length) - 3
            CRCFull = CUShort(CRCFull Xor data(i))

            For j As Integer = 0 To 7
                CRCLSB = ChrW(CRCFull And &H1)
                CRCFull = CUShort((CRCFull >> 1) And &H7FFF)

                If Convert.ToInt32(CRCLSB) = 1 Then
                    CRCFull = CUShort(CRCFull Xor &HA001)
                End If
            Next
        Next
        CRCHigh = CByte((CRCFull >> 8) And &HFF)
        CRCLow = CByte(CRCFull And &HFF)
        Return New Byte(1) {CRCLow, CRCHigh}
    End Function

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

    'JIG 2 DATA SEND
    Private Sub Timer4_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer4.Tick

        On Error Resume Next

        Count2 = Count2 + 1
        If Count2 > 10 Then
            Count2 = 1
        End If


        'r = 1
        SalveId = Count2
        AxMSFlexGrid1.set_TextMatrix(SalveId + 10, 0, "JIG2")
        AxMSFlexGrid1.set_TextMatrix(SalveId + 10, 1, SalveId)
        SendQuery(SerialPort2, SalveId, Address, Lenght)




        'Call delay()
        Timer5.Enabled = True
        Timer5.Interval = 250



    End Sub
    'JIG 2 DATA RECIVED
    Private Sub Timer5_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer5.Tick

        On Error Resume Next

        

        Dim c1 As Integer
        Dim c2 As Integer

        

        'RichTextBox2.Clear()
        If SerialPort2.BytesToRead > 0 Then

            dataReceived2 = SerialPort2.BytesToRead

            Dim bytes As Integer = SerialPort2.BytesToRead
            Dim buffer As Byte() = New Byte(bytes - 1) {}
            SerialPort2.Read(buffer, 0, bytes)
            'SerialPort1.DiscardInBuffer()
            Dim ReceivedMessage As Byte() = New Byte(bytes - 1) {}
            Array.Copy(buffer, ReceivedMessage, bytes)

            RecDate(buffer)

            If buffer.Length = 205 Then

                c1 = 1001
                c2 = 0
                SalveId = buffer(0)
                For v1 = 3 To buffer.Length - 3 Step 4

                    vIN = New Byte() {buffer(v1 + 3), buffer(v1 + 2), buffer(v1 + 1), buffer(v1)}
                    Dim s1 As Single = BitConverter.ToSingle(vIN, 0)
                    'RichTextBox2.AppendText(c1 & "      " & Math.Round(Convert.ToDouble(s1), 2) & Environment.NewLine)

                    Arr1(c2) = Math.Round(Convert.ToDouble(s1), 2)

                    c1 = c1 + 1
                    c2 = c2 + 1

                Next
                Voltage = CStr(Arr1(1))
                Current = CStr(Arr1(7))
                Wattage = CStr(Arr1(11))
                THD_Voltage = CStr(Arr1(45))
                Power_Factor = CStr(Arr1(27))

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



            End If
        End If

        Timer5.Enabled = False
    End Sub
    'JIG 3 DATA SEND
    Private Sub Timer6_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer6.Tick

        On Error Resume Next

        Count3 = Count3 + 1
        If Count3 > 10 Then
            Count3 = 1
        End If


        'r = 1
        SalveId = Count3
        Dim Row As Integer = CInt(SalveId) + 20
        AxMSFlexGrid1.set_TextMatrix(Row, 0, CStr("JIG3"))
        AxMSFlexGrid1.set_TextMatrix(Row, 1, SalveId)
        SendQuery(SerialPort3, SalveId, Address, Lenght)
        'Call delay()
        Timer7.Enabled = True
        Timer7.Interval = 250
    End Sub
    'JIG 3 DATA Recived
    Private Sub Timer7_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer7.Tick

        On Error Resume Next

        Dim c1 As Integer
        Dim c2 As Integer


        'RichTextBox2.Clear()
        If SerialPort3.BytesToRead > 0 Then

            dataReceived3 = SerialPort3.BytesToRead

            Dim bytes As Integer = SerialPort3.BytesToRead
            Dim buffer As Byte() = New Byte(bytes - 1) {}
            SerialPort3.Read(buffer, 0, bytes)
            'SerialPort1.DiscardInBuffer()
            Dim ReceivedMessage As Byte() = New Byte(bytes - 1) {}
            Array.Copy(buffer, ReceivedMessage, bytes)

            RecDate(buffer)

            If buffer.Length = 205 Then

                c1 = 1001
                c2 = 0
                SalveId = buffer(0)
                For v1 = 3 To buffer.Length - 3 Step 4

                    vIN = New Byte() {buffer(v1 + 3), buffer(v1 + 2), buffer(v1 + 1), buffer(v1)}
                    Dim s1 As Single = BitConverter.ToSingle(vIN, 0)
                    'RichTextBox2.AppendText(c1 & "      " & Math.Round(Convert.ToDouble(s1), 2) & Environment.NewLine)

                    Arr1(c2) = Math.Round(Convert.ToDouble(s1), 2)

                    c1 = c1 + 1
                    c2 = c2 + 1

                Next
                Voltage = CStr(Arr1(1))
                Current = CStr(Arr1(7))
                Wattage = CStr(Arr1(11))
                THD_Voltage = CStr(Arr1(45))
                Power_Factor = CStr(Arr1(27))
                Dim Row As Integer = CInt(SalveId) + 20
                AxMSFlexGrid1.set_TextMatrix(Row, 2, Voltage)
                AxMSFlexGrid1.set_TextMatrix(Row, 3, Current)
                AxMSFlexGrid1.set_TextMatrix(Row, 4, Wattage)
                AxMSFlexGrid1.set_TextMatrix(Row, 5, Power_Factor)

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

            End If
        End If

        Timer7.Enabled = False
    End Sub
    'JIG 4 DATA SEND
    Private Sub Timer8_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer8.Tick

        On Error Resume Next

        Count4 = Count4 + 1
        If Count4 > 10 Then
            Count4 = 1
        End If

        'r = 1
        SalveId = Count4
        AxMSFlexGrid2.set_TextMatrix(SalveId, 0, "JIG4")
        AxMSFlexGrid2.set_TextMatrix(SalveId, 1, SalveId)
        SendQuery(SerialPort4, SalveId, Address, Lenght)
        'Call delay()
        Timer9.Enabled = True
        Timer9.Interval = 250
    End Sub
    'JIG 4 DATA RECV
    Private Sub Timer9_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer9.Tick

        On Error Resume Next

        Dim c1 As Integer
        Dim c2 As Integer


        'RichTextBox2.Clear()
        If SerialPort4.BytesToRead > 0 Then

            dataReceived4 = SerialPort4.BytesToRead

            Dim bytes As Integer = SerialPort4.BytesToRead
            Dim buffer As Byte() = New Byte(bytes - 1) {}
            SerialPort4.Read(buffer, 0, bytes)
            'SerialPort1.DiscardInBuffer()
            Dim ReceivedMessage As Byte() = New Byte(bytes - 1) {}
            Array.Copy(buffer, ReceivedMessage, bytes)

            RecDate(buffer)

            If buffer.Length = 205 Then

                c1 = 1001
                c2 = 0
                SalveId = buffer(0)
                For v1 = 3 To buffer.Length - 3 Step 4

                    vIN = New Byte() {buffer(v1 + 3), buffer(v1 + 2), buffer(v1 + 1), buffer(v1)}
                    Dim s1 As Single = BitConverter.ToSingle(vIN, 0)
                    'RichTextBox2.AppendText(c1 & "      " & Math.Round(Convert.ToDouble(s1), 2) & Environment.NewLine)

                    Arr1(c2) = Math.Round(Convert.ToDouble(s1), 2)

                    c1 = c1 + 1
                    c2 = c2 + 1

                Next
                Voltage = CStr(Arr1(1))
                Current = CStr(Arr1(7))
                Wattage = CStr(Arr1(11))
                THD_Voltage = CStr(Arr1(45))
                Power_Factor = CStr(Arr1(27))

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

            End If
        End If

        Timer9.Enabled = False
    End Sub
    'JIG 5 DATA SEND
    Private Sub Timer10_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer10.Tick

        On Error Resume Next

        Count5 = Count5 + 1
        If Count5 > 10 Then
            Count5 = 1
        End If


        'r = 1
        SalveId = Count5
        AxMSFlexGrid2.set_TextMatrix(SalveId + 10, 0, "JIG5")
        AxMSFlexGrid2.set_TextMatrix(SalveId + 10, 1, SalveId)
        SendQuery(SerialPort5, SalveId, Address, Lenght)
        'Call delay()
        Timer11.Enabled = True
        Timer11.Interval = 250
    End Sub
    'JIG 5 DATA Reciv
    Private Sub Timer11_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer11.Tick

        On Error Resume Next

        Dim c1 As Integer
        Dim c2 As Integer

        'RichTextBox2.Clear()
        If SerialPort5.BytesToRead > 0 Then

            dataReceived5 = SerialPort5.BytesToRead

            Dim bytes As Integer = SerialPort5.BytesToRead
            Dim buffer As Byte() = New Byte(bytes - 1) {}
            SerialPort5.Read(buffer, 0, bytes)
            'SerialPort1.DiscardInBuffer()
            Dim ReceivedMessage As Byte() = New Byte(bytes - 1) {}
            Array.Copy(buffer, ReceivedMessage, bytes)

            RecDate(buffer)

            If buffer.Length = 205 Then

                c1 = 1001
                c2 = 0
                SalveId = buffer(0)
                For v1 = 3 To buffer.Length - 3 Step 4

                    vIN = New Byte() {buffer(v1 + 3), buffer(v1 + 2), buffer(v1 + 1), buffer(v1)}
                    Dim s1 As Single = BitConverter.ToSingle(vIN, 0)
                    'RichTextBox2.AppendText(c1 & "      " & Math.Round(Convert.ToDouble(s1), 2) & Environment.NewLine)

                    Arr1(c2) = Math.Round(Convert.ToDouble(s1), 2)

                    c1 = c1 + 1
                    c2 = c2 + 1

                Next
                Voltage = CStr(Arr1(1))
                Current = CStr(Arr1(7))
                Wattage = CStr(Arr1(11))
                THD_Voltage = CStr(Arr1(45))
                Power_Factor = CStr(Arr1(27))

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

            End If
        End If

        Timer11.Enabled = False
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

        ToolStripStatusLabel1.Text = SENDCOMMAND(i, "QR?")

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

        ToolStripStatusLabel2.Text = SENDCOMMAND(i, "QR?")




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
        ToolStripStatusLabel3.Text = SENDCOMMAND(i, "QR?")


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
        ToolStripStatusLabel4.Text = SENDCOMMAND(i, "QR?")


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

        ToolStripStatusLabel5.Text = SENDCOMMAND(i, "QR?")


    End Sub

   


    '    Public buff_rx As Byte
    '    Public Sub SerialPort6_DataReceived(ByVal sender As Object, ByVal e As SerialDataReceivedEventArgs) Handles SerialPort6.DataReceived

    '        'On Error GoTo exit1

    '        'SerialPort6.ReadTimeout = 20
    '        Application.DoEvents()
    '        Do While SerialPort6.BytesToRead > 0
    '            Try
    '                Application.DoEvents()
    '                buff_rx = SerialPort6.ReadByte
    '                'If (buff_rx <> 0) Then
    '                Me.Invoke(New EventHandler(AddressOf update_dat))
    '                'End If
    '            Catch ex As Exception
    '                My.Computer.FileSystem.WriteAllText("C:\Reports\log.txt", "Data Recived " & ErrorToString() & " " & "Date : " + System.DateTime.Now.ToString("dd/MM/yyyy") & "  " & "Time : " + System.DateTime.Now.ToString("HH:mm:ss") & Chr(13), True)

    '            End Try
    '            ' 
    '        Loop
    '        'exit1:
    '    End Sub

    '    '$021 - close
    '    '$020 - open
    '    Dim StoreStr As String = ""
    '    Public Sub update_dat(ByVal sender As Object, ByVal e As System.EventArgs)

    '        On Error GoTo exit1

    '        Dim x As String = Convert.ToChar(buff_rx).ToString()

    '        If x = Chr(13) Then

    '            Dim RS As String = StoreStr
    '            Label2.Text = StoreStr
    '            StoreStr = ""

    '            'If RS.Substring(1, 2) = "02" Then
    '            If RS.Substring(3, 1) = 0 Then
    '                Dim dt As DataTable = GetData("SELECT * FROM `modeldetails` WHERE `F_Status` = '1' and Id = '" & RS.Substring(1, 2) & "'")
    '                If dt.Rows.Count() > 0 Then
    '                    Dim Id As String = dt.Rows(0).Item("Id").ToString
    '                    'Dim Currenttime1 As String = System.DateTime.Now.ToString("hh:mm:ss tt")
    '                    Dim x1 As String = "UPDATE modeldetails SET F_Status = '0' where Id = '" & Id & "'"    ''''Open Flap
    '                    MyNonQuery(x1)

    '                    'Dim x1 As String = "UPDATE modeldetails SET status = '0', endtime = '" & Currenttime1 & "' where Id = '" & Id & "'"
    '                    'MyNonQuery(x1)
    '                End If
    '            ElseIf RS.Substring(3, 1) = 1 Then

    '                Dim dt As DataTable = GetData("SELECT * FROM `modeldetails` WHERE `F_Status` = '0' and Id = '" & RS.Substring(1, 2) & "'")
    '                If dt.Rows.Count() > 0 Then
    '                    Dim Id As String = dt.Rows(0).Item("Id").ToString
    '                    'Dim Currenttime1 As String = System.DateTime.Now.ToString("hh:mm:ss tt")
    '                    Dim x1 As String = "UPDATE modeldetails SET F_Status = '1'  where Id = '" & Id & "'"    ''''''''Close Flap
    '                    MyNonQuery(x1)

    '                    'Dim x1 As String = "UPDATE modeldetails SET status = '1', resumetime  = '" & Currenttime1 & "', endtime = '0'  where Id = '" & Id & "'"
    '                    'MyNonQuery(x1)

    '                End If
    '            End If

    '            'End If
    '            RS = ""

    '        Else

    '            StoreStr = StoreStr + x
    '        End If
    'exit1:


    '    End Sub



    Private Sub Timer17_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer17.Tick

        'If JIG(1, 2) = 1 Then
        '    ToolStripStatusLabel1.Text = SENDCOMMAND("1", "QR?")
        'End If
        'If JIG(2, 2) = 1 Then
        '    ToolStripStatusLabel2.Text = SENDCOMMAND("2", "QR?")
        'End If
        'If JIG(3, 2) = 1 Then
        '    ToolStripStatusLabel3.Text = SENDCOMMAND("3", "QR?")
        'End If
        'If JIG(4, 2) = 1 Then
        '    ToolStripStatusLabel4.Text = SENDCOMMAND("4", "QR?")
        'End If
        'If JIG(5, 2) = 1 Then
        '    ToolStripStatusLabel5.Text = SENDCOMMAND("5", "QR?")
        'End If






        'On Error GoTo exit1
        'Label2.Text = ""
        'Dim RS As String = ReceiveSerialData()
        'If RS <> "" Then
        '    Label2.Text = RS
        '    If RS.Substring(3, 1) = 0 Then
        '        Dim dt As DataTable = GetData("SELECT * FROM `modeldetails` WHERE `F_Status` = '1' and Id = " & CInt(RS.Substring(1, 2)) & "")
        '        If dt.Rows.Count() > 0 Then
        '            Dim Id As String = dt.Rows(0).Item("Id").ToString
        '            'Dim Currenttime1 As String = System.DateTime.Now.ToString("hh:mm:ss tt")
        '            Dim x1 As String = "UPDATE modeldetails SET F_Status = '0' where Id = '" & Id & "'"    ''''Open Flap
        '            MyNonQuery(x1)

        '            'Dim x1 As String = "UPDATE modeldetails SET status = '0', endtime = '" & Currenttime1 & "' where Id = '" & Id & "'"
        '            'MyNonQuery(x1)
        '        End If
        '    ElseIf RS.Substring(3, 1) = 1 Then

        '        Dim dt As DataTable = GetData("SELECT * FROM `modeldetails` WHERE `F_Status` = '0' and Id = " & CInt(RS.Substring(1, 2)) & "")
        '        If dt.Rows.Count() > 0 Then
        '            Dim Id As String = dt.Rows(0).Item("Id").ToString
        '            'Dim Currenttime1 As String = System.DateTime.Now.ToString("hh:mm:ss tt")
        '            Dim x1 As String = "UPDATE modeldetails SET F_Status = '1'  where Id = '" & Id & "'"    ''''''''Close Flap
        '            MyNonQuery(x1)

        '            'Dim x1 As String = "UPDATE modeldetails SET status = '1', resumetime  = '" & Currenttime1 & "', endtime = '0'  where Id = '" & Id & "'"
        '            'MyNonQuery(x1)

        '        End If
        '    End If
        'End If
        'RS = ""
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
            ToolStripStatusLabel7.Text = datasend2 & " Data revied"
            dataReceived2 = ""
        Else
            ToolStripStatusLabel7.Text = datasend2 & " Data not revied"
            If datasend2 >= 20 Then
                ToolStripStatusLabel7.Text = datasend2 & " Data not revied"
                ToolStripStatusLabel2.Text = SENDCOMMAND(2, "RLN")
                datasend2 = 0
            End If
        End If

    End Sub


    'Function ReceiveSerialData() As String
    '    ' returns new data from the serial connection
    '    Dim Incoming As String
    '    Try
    '        Incoming = SerialPort6.ReadExisting()
    '        'If Incoming Is Nothing Then
    '        '    Return "nothing" & vbCrLf
    '        'Else
    '        Return Incoming
    '        'End If
    '    Catch ex As TimeoutException
    '        'Return "Error: Serial Port read timed out."

    '        My.Computer.FileSystem.WriteAllText("C:\Reports\log.txt", "Data Recived " & ErrorToString() & " " & "Date : " + System.DateTime.Now.ToString("dd/MM/yyyy") & "  " & "Time : " + System.DateTime.Now.ToString("HH:mm:ss") & Chr(13), True)

    '    End Try
    '    'Dim datarec As String = ReceiveSerialData()
    'End Function

    Private Sub Timer20_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer20.Tick

        datasend1 = datasend1 + 1
        If dataReceived1 <> "" Then
            datasend1 = 0
            ToolStripStatusLabel6.Text = datasend1 & " Data revied"
            dataReceived1 = ""
        Else
            ToolStripStatusLabel6.Text = datasend1 & " Data not revied"
            If datasend1 >= 20 Then
                ToolStripStatusLabel1.Text = SENDCOMMAND(1, "RLN")
                datasend1 = 0
            End If
        End If
    End Sub

    Private Sub Timer21_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer21.Tick
        datasend3 = datasend3 + 1
        If dataReceived3 <> "" Then
            datasend3 = 0
            ToolStripStatusLabel8.Text = datasend3 & " Data revied"
            dataReceived3 = ""
        Else
            If datasend3 >= 20 Then
                ToolStripStatusLabel8.Text = datasend3 & " Data not revied"
                ToolStripStatusLabel3.Text = SENDCOMMAND(3, "RLN")
                datasend3 = 0
            End If
        End If
    End Sub

    Private Sub Timer22_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer22.Tick
        datasend4 = datasend4 + 1
        If dataReceived4 <> "" Then
            datasend4 = 0
            ToolStripStatusLabel9.Text = datasend4 & " Data revied"
            dataReceived4 = ""
        Else
            If datasend4 >= 20 Then
                ToolStripStatusLabel9.Text = datasend4 & " Data not revied"
                ToolStripStatusLabel4.Text = SENDCOMMAND(4, "RLN")
                datasend4 = 0
            End If
        End If
    End Sub

    Private Sub Timer23_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer23.Tick
        datasend5 = datasend5 + 1
        If dataReceived5 <> "" Then
            datasend5 = 0
            ToolStripStatusLabel10.Text = datasend5 & " Data not revied"
            dataReceived5 = ""
        Else
            If datasend5 >= 20 Then
                ToolStripStatusLabel10.Text = datasend5 & " Data not revied"
                ToolStripStatusLabel5.Text = SENDCOMMAND(5, "RLN")
                datasend5 = 0
            End If
        End If
    End Sub

    Public buff_rx As Byte
    Public Sub SerialPort6_DataReceived(ByVal sender As Object, ByVal e As SerialDataReceivedEventArgs) Handles SerialPort6.DataReceived
        'SerialPort1.ReadTimeout = 20

        Do While SerialPort6.BytesToRead > 0
            Try
                buff_rx = SerialPort6.ReadByte
                Me.Invoke(New EventHandler(AddressOf update_dat))
            Catch ex As Exception

            End Try
            ' Application.DoEvents
        Loop

    End Sub
    Public Sub update_dat(ByVal sender As Object, ByVal e As System.EventArgs)
        On Error GoTo exit1
        Dim i As Integer = buff_rx
        Dim s As String = ""
        s = i.ToString("X2") 'if wanted to be in hex, use "X2"
        Dim x As String = Convert.ToChar(buff_rx).ToString()

        If x = Chr(13) Then

            Label2.Text = ""
            Dim RS As String = Label3.Text
            Label3.Text = ""
            Label2.Text = RS
            If RS.Substring(3, 1) = 0 Then
                Dim dt As DataTable = GetData("SELECT * FROM `modeldetails` WHERE `F_Status` = '1' and Id = " & CInt(RS.Substring(1, 2)) & "")
                If dt.Rows.Count() > 0 Then
                    Dim Id As String = dt.Rows(0).Item("Id").ToString
                    'Dim Currenttime1 As String = System.DateTime.Now.ToString("hh:mm:ss tt")
                    Dim x1 As String = "UPDATE modeldetails SET F_Status = '0' where Id = '" & Id & "'"    ''''Open Flap
                    MyNonQuery(x1)
                    RS = ""
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
                    RS = ""
                    'Dim x1 As String = "UPDATE modeldetails SET status = '1', resumetime  = '" & Currenttime1 & "', endtime = '0'  where Id = '" & Id & "'"
                    'MyNonQuery(x1)

                End If
            End If

        Else

            Label3.Text = Label3.Text + x.Trim

        End If

exit1:


    End Sub
End Class