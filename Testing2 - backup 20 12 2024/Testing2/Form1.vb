Imports System.IO
Imports System.IO.Ports
Imports System.Threading

Public Class Form1


    'Timer2 for Date Time
    'Timer1 and Timer3  for JIG 1
    'Timer4 and Timer5  for JIG 2
    'Timer6 and Timer7  for JIG 3
    'Timer8 and Timer9  for JIG 4
    'Timer10 and Timer11  for JIG 5
    '  for Start, Stop, Resume JIG 

    Dim Count1 As Integer = 0
    Dim Count2 As Integer = 0
    Dim Count3 As Integer = 0
    Dim Count4 As Integer = 0
    Dim Count5 As Integer = 0



    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'datetime
        Timer2.Start()
        AxMSFlexGrid()  'MSFlexGrid Function

        'Application.DoEvents()
        ViewFuncation(DataGridView1) ' DataGridview And port
        'Application.DoEvents()

        Button2.Text = "Stop"
        Button2.ForeColor = Color.Red
        RichTextBox1.ScrollToCaret()

        Timer17.Interval = 1000
        Timer17.Enabled = True
        '    'data recived
        '    Timer18.Enabled = True
        '    Timer18.Interval = 1000
        ' Form2.Show()
    End Sub

    Public Function SendQuery(ByVal Serial As SerialPort, ByVal SalveId As Byte, ByVal Address As UShort, ByVal Lenght As UShort)
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

    Dim t1 As Integer
    Dim WaitingTime As Integer = 3000
    Private Sub Timer2_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Label10.Text = "Date : " + System.DateTime.Now.ToString("dd/MM/yyyy") & "  " & "Time : " + System.DateTime.Now.ToString("HH:mm:ss")

        On Error Resume Next

        JIGStatus(DataGridView2)

        If SerialPort1.IsOpen Then
            Dim J As Integer = 1
            If JIG(1, 2) = 1 Then
                If Timer1.Enabled = False Then

                    ToolStripStatusLabel1.Text = SENDCOMMAND(J, "TWF")
                    ToolStripStatusLabel1.Text = SENDCOMMAND(J, "RLN")

                    Timer1.Interval = 500
                    Timer1.Enabled = True


                    Timer12.Start()

                End If
            Else
                If Timer1.Enabled = True Then
                    ToolStripStatusLabel1.Text = SENDCOMMAND(J, "RLF")

                    Timer1.Enabled = False

                    'wait1(WaitingTime)
                    Timer12.Stop()
                End If
            End If
        End If
        If SerialPort2.IsOpen Then
            Dim J As Integer = 2
            If JIG(2, 2) = 1 Then
                If Timer4.Enabled = False Then
                    ToolStripStatusLabel2.Text = SENDCOMMAND(J, "TWF")
                    ToolStripStatusLabel2.Text = SENDCOMMAND(J, "RLN")
                    Timer4.Interval = 500
                    Timer4.Enabled = True
                    Timer13.Start()
                End If
            Else
                If Timer4.Enabled = True Then
                    ToolStripStatusLabel2.Text = SENDCOMMAND(J, "RLF")
                    Timer4.Enabled = False

                    wait2(WaitingTime)
                    Timer13.Stop()
                End If
            End If
        End If
        If SerialPort3.IsOpen Then
            Dim J As Integer = 3
            If JIG(3, 2) = 1 Then
                If Timer6.Enabled = False Then
                    ToolStripStatusLabel3.Text = SENDCOMMAND(J, "TWF")
                    ToolStripStatusLabel3.Text = SENDCOMMAND(J, "RLN")
                    Timer6.Interval = 500
                    Timer6.Enabled = True
                    Timer14.Start()
                End If
            Else
                If Timer6.Enabled = True Then
                    ToolStripStatusLabel3.Text = SENDCOMMAND(J, "RLF")
                    Timer6.Enabled = False
                    wait3(WaitingTime)
                    Timer14.Stop()
                End If
            End If
        End If
        If SerialPort4.IsOpen Then
            Dim J As Integer = 4
            If JIG(4, 2) = 1 Then
                If Timer8.Enabled = False Then
                    ToolStripStatusLabel4.Text = SENDCOMMAND(J, "TWF")
                    ToolStripStatusLabel4.Text = SENDCOMMAND(J, "RLN")
                    Timer8.Interval = 500
                    Timer8.Enabled = True
                    Timer15.Start()
                End If
            Else
                If Timer8.Enabled = True Then
                    ToolStripStatusLabel4.Text = SENDCOMMAND(J, "RLF")
                    Timer8.Enabled = False
                    wait4(WaitingTime)
                    Timer15.Stop()
                End If
            End If
        End If
        If SerialPort5.IsOpen Then
            Dim J As Integer = 5
            If JIG(5, 2) = 1 Then
                If Timer10.Enabled = False Then
                    ToolStripStatusLabel5.Text = SENDCOMMAND(J, "TWF")
                    ToolStripStatusLabel5.Text = SENDCOMMAND(J, "RLN")
                    Timer10.Interval = 500
                    Timer10.Enabled = True
                    Timer16.Start()
                End If
            Else
                If Timer10.Enabled = True Then
                    ToolStripStatusLabel5.Text = SENDCOMMAND(J, "RLF")
                    Timer10.Enabled = False
                    wait5(WaitingTime)
                    Timer16.Stop()
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
            Dim bytes As Integer = SerialPort1.BytesToRead
            Dim buffer As Byte() = New Byte(bytes - 1) {}
            SerialPort1.Read(buffer, 0, bytes)
            'SerialPort1.DiscardInBuffer()
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
            RichTextBox1.AppendText("TX - ")
            RichTextBox1.AppendText(String.Join(" ", MbusQuery))
            RichTextBox1.AppendText(Environment.NewLine)
        End If
    End Sub

    Public Sub RecDate(ByVal buffer() As Byte)
        If InvokeRequired Then
            Invoke(New Action(Of Object)(AddressOf RecDate), buffer)
        Else
            RichTextBox1.AppendText("TX - ")
            RichTextBox1.AppendText(String.Join(" ", buffer))
            RichTextBox1.AppendText(Environment.NewLine)

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

    Private Sub Timer13_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer13.Tick
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

   


    'Public buff As Byte
    Public Sub SerialPort6_DataReceived(ByVal sender As Object, ByVal e As SerialDataReceivedEventArgs) Handles SerialPort6.DataReceived
        SerialPort6.ReadTimeout = 20
        Do While SerialPort6.BytesToRead > 0
            Try
                buff_rx = SerialPort6.ReadByte
                If (buff_rx <> 0) Then
                    Me.Invoke(New EventHandler(AddressOf update_dat))
                End If
            Catch ex As Exception

            End Try
            ' Application.DoEvents()
        Loop

    End Sub







    '$021 - close
    '$020 - open
    Public Sub update_dat(ByVal sender As Object, ByVal e As System.EventArgs)

         On Error Resume Next

        Dim i As Integer = buff_rx
        Dim s As String = ""
        s = i.ToString("X2") 'if wanted to be in hex, use "X2"
        Dim x As String = Convert.ToChar(buff_rx).ToString()

        If x = Chr(13) Then

            Dim RS As String = Label2.Text
            
            Label2.Text = ""
            'If RS.Substring(1, 2) = "02" Then
            If RS.Substring(3, 1) = 0 Then
                Dim dt As DataTable = GetData("SELECT * FROM `modeldetails` WHERE `F_Status` = '1' and Id = '" & RS.Substring(1, 2) & "'")
                If dt.Rows.Count() > 0 Then
                    Dim Id As String = dt.Rows(0).Item("Id").ToString
                    'Dim Currenttime1 As String = System.DateTime.Now.ToString("hh:mm:ss tt")
                    Dim x1 As String = "UPDATE modeldetails SET F_Status = '0' where Id = '" & Id & "'"    ''''Open Flap
                    MyNonQuery(x1)

                    'Dim x1 As String = "UPDATE modeldetails SET status = '0', endtime = '" & Currenttime1 & "' where Id = '" & Id & "'"
                    'MyNonQuery(x1)
                End If
            ElseIf RS.Substring(3, 1) = 1 Then

                Dim dt As DataTable = GetData("SELECT * FROM `modeldetails` WHERE `F_Status` = '0' and Id = '" & RS.Substring(1, 2) & "'")
                If dt.Rows.Count() > 0 Then
                    Dim Id As String = dt.Rows(0).Item("Id").ToString
                    'Dim Currenttime1 As String = System.DateTime.Now.ToString("hh:mm:ss tt")
                    Dim x1 As String = "UPDATE modeldetails SET F_Status = '1'  where Id = '" & Id & "'"    ''''''''Close Flap
                    MyNonQuery(x1)

                    'Dim x1 As String = "UPDATE modeldetails SET status = '1', resumetime  = '" & Currenttime1 & "', endtime = '0'  where Id = '" & Id & "'"
                    'MyNonQuery(x1)

                End If
            End If

            'End If
            RS = ""
            x = ""
        Else

            Label2.Text = Label2.Text + x.Trim
        End If



    End Sub



    Private Sub Timer17_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer17.Tick
        'ToolStripStatusLabel1.Text = SENDCOMMAND("1", "QR?")
        ToolStripStatusLabel1.Text = SENDCOMMAND("2", "QR?")
        'ToolStripStatusLabel1.Text = SENDCOMMAND("3", "QR?")
        'ToolStripStatusLabel1.Text = SENDCOMMAND("4", "QR?")
        'ToolStripStatusLabel1.Text = SENDCOMMAND("5", "QR?")
    End Sub
End Class