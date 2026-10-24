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
        Timer7.Enabled = True
        Timer7.Interval = 300


        'datetime
        Timer6.Start()
        AxMSFlexGrid()  'MSFlexGrid Function

        'Application.DoEvents()0
        ViewFuncation(DataGridView1) ' DataGridview And port
        'Application.DoEvents()

        
        RichTextBox1.ScrollToCaret()
        RichTextBox2.ScrollToCaret()


        Timer13.Interval = 300
        Timer13.Enabled = True
       
       
    End Sub
    Dim flag1 As Integer
    Dim WaitingTime As Integer = 3000
    Dim t1 As Integer

    Private Sub Timer6_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer6.Tick
        Label10.Text = "Date : " + System.DateTime.Now.ToString("dd/MM/yyyy") & "  " & "Time : " + System.DateTime.Now.ToString("HH:mm:ss")

        On Error Resume Next



        If SerialPort1.IsOpen Then
            Dim J As Integer = 1
            If JIG(1, 2) = 1 Then
                If Timer1.Enabled = False Then

                    ToolStripStatusLabel1.Text = SENDCOMMAND(J, "TWF")
                    ToolStripStatusLabel1.Text = SENDCOMMAND(J, "RLN")

                    Timer1.Interval = MInterval
                    Timer1.Enabled = True

                    Timer8.Interval = 1000
                    Timer8.Enabled = True

                End If
            Else
                If Timer1.Enabled = True Then
                    ToolStripStatusLabel1.Text = SENDCOMMAND(J, "RLF")

                    Timer1.Enabled = False

                    wait1(WaitingTime)

                    Timer8.Enabled = False


                End If
            End If
        End If
        If SerialPort2.IsOpen Then
            Dim J As Integer = 2
            If JIG(2, 2) = 1 Then
                If Timer2.Enabled = False Then
                    ToolStripStatusLabel2.Text = SENDCOMMAND(J, "TWF")
                    ToolStripStatusLabel2.Text = SENDCOMMAND(J, "RLN")
                    Timer2.Interval = MInterval
                    Timer2.Enabled = True

                    Timer9.Interval = 1000
                    Timer9.Enabled = True

                End If
            Else
                If Timer2.Enabled = True Then
                    ToolStripStatusLabel2.Text = SENDCOMMAND(J, "RLF")
                    Timer2.Enabled = False

                    wait2(WaitingTime)
                    Timer9.Enabled = False

                End If
            End If
        End If
        If SerialPort3.IsOpen Then
            Dim J As Integer = 3
            If JIG(3, 2) = 1 Then
                If Timer3.Enabled = False Then
                    ToolStripStatusLabel3.Text = SENDCOMMAND(J, "TWF")
                    ToolStripStatusLabel3.Text = SENDCOMMAND(J, "RLN")
                    Timer3.Interval = MInterval
                    Timer3.Enabled = True

                    Timer10.Interval = 1000
                    Timer10.Enabled = True

                End If
            Else
                If Timer3.Enabled = True Then
                    ToolStripStatusLabel3.Text = SENDCOMMAND(J, "RLF")
                    Timer3.Enabled = False

                    wait3(WaitingTime)
                    Timer10.Enabled = False

                End If
            End If
        End If
        If SerialPort4.IsOpen Then
            Dim J As Integer = 4
            If JIG(4, 2) = 1 Then
                If Timer4.Enabled = False Then
                    ToolStripStatusLabel4.Text = SENDCOMMAND(J, "TWF")
                    ToolStripStatusLabel4.Text = SENDCOMMAND(J, "RLN")
                    Timer4.Interval = MInterval
                    Timer4.Enabled = True

                    Timer11.Interval = 1000
                    Timer11.Enabled = True
                End If
            Else
                If Timer4.Enabled = True Then
                    ToolStripStatusLabel4.Text = SENDCOMMAND(J, "RLF")
                    Timer4.Enabled = False

                    wait4(WaitingTime)

                    Timer11.Enabled = False


                End If
            End If
        End If
        If SerialPort5.IsOpen Then
            Dim J As Integer = 5
            If JIG(5, 2) = 1 Then
                If Timer5.Enabled = False Then
                    ToolStripStatusLabel5.Text = SENDCOMMAND(J, "TWF")
                    ToolStripStatusLabel5.Text = SENDCOMMAND(J, "RLN")
                    Timer5.Interval = MInterval
                    Timer5.Enabled = True

                    Timer12.Interval = 1000
                    Timer12.Enabled = True

                End If
            Else
                If Timer5.Enabled = True Then
                    ToolStripStatusLabel5.Text = SENDCOMMAND(J, "RLF")
                    Timer5.Enabled = False

                    wait5(WaitingTime)
                    Timer12.Enabled = False


                End If
            End If
        End If



    End Sub
    

    'JIG 1 DATA SEND AND RECIVED
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

        Dim bytes As Integer = SerialPort1.BytesToRead
        Dim buffer As Byte() = New Byte(bytes - 1) {}
        SerialPort1.Read(buffer, 0, bytes)
        ReceivedMessage1 = New Byte(bytes - 1) {}
        Array.Copy(buffer, ReceivedMessage1, bytes)

        Me.Invoke(New EventHandler(AddressOf update_dat1))



    End Sub


    Public Sub update_dat1(ByVal sender As Object, ByVal e As System.EventArgs)

        On Error Resume Next

        Dim c2 As Integer
        If ReceivedMessage1.Length = 205 Then

            c2 = 0
            SalveId = ReceivedMessage1(0)
            If SalveId = ReceivedMessage1(0) Then

                For v1 = 3 To ReceivedMessage1.Length - 3 Step 4

                    vIN = New Byte() {ReceivedMessage1(v1 + 3), ReceivedMessage1(v1 + 2), ReceivedMessage1(v1 + 1), ReceivedMessage1(v1)}
                    Dim s1 As Single = BitConverter.ToSingle(vIN, 0)
                    'RichTextBox2.AppendText(c1 & "      " & Math.Round(Convert.ToDouble(s1), 2) & Environment.NewLine)

                    Arr1(c2) = Math.Round(Convert.ToDouble(s1), 2)


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
                AxMSFlexGrid1.set_TextMatrix(SalveId, 5, Str(Power_Factor) + " " + Str(THD_Voltage))

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

            End If





        End If

    End Sub


    'JIG 2 DATA SEND an Recived
    Private Sub Timer2_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        On Error Resume Next

        Count2 = Count2 + 1
        If Count2 > 10 Then
            Count2 = 1
        End If

        SalveId = Count2
        AxMSFlexGrid1.set_TextMatrix(SalveId + 10, 0, "JIG2")
        AxMSFlexGrid1.set_TextMatrix(SalveId + 10, 1, SalveId)
        SendQuery(SerialPort2, SalveId, Address, Lenght)


        Dim bytes As Integer = SerialPort2.BytesToRead
        Dim buffer As Byte() = New Byte(bytes - 1) {}
        SerialPort2.Read(buffer, 0, bytes)
        ReceivedMessage2 = New Byte(bytes - 1) {}
        Array.Copy(buffer, ReceivedMessage2, bytes)

        Me.Invoke(New EventHandler(AddressOf update_dat2))



    End Sub
    
    Public Sub update_dat2(ByVal sender As Object, ByVal e As System.EventArgs)

        On Error Resume Next

        Dim c2 As Integer
        If ReceivedMessage2.Length = 205 Then


            c2 = 0
            SalveId = ReceivedMessage2(0)
            For v1 = 3 To ReceivedMessage2.Length - 3 Step 4

                vIN = New Byte() {ReceivedMessage2(v1 + 3), ReceivedMessage2(v1 + 2), ReceivedMessage2(v1 + 1), ReceivedMessage2(v1)}
                Dim s1 As Single = BitConverter.ToSingle(vIN, 0)
                'RichTextBox2.AppendText(c1 & "      " & Math.Round(Convert.ToDouble(s1), 2) & Environment.NewLine)

                Arr1(c2) = Math.Round(Convert.ToDouble(s1), 2)


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

    End Sub
    
    'JIG 3 DATA SEND and RECVIVED
    Private Sub Timer3_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer3.Tick

        On Error Resume Next

        Count3 = Count3 + 1
        If Count3 > 10 Then
            Count3 = 1
        End If

        SalveId = Count3
        Dim Row As Integer = CInt(SalveId) + 20
        AxMSFlexGrid1.set_TextMatrix(Row, 0, CStr("JIG3"))
        AxMSFlexGrid1.set_TextMatrix(Row, 1, SalveId)
        SendQuery(SerialPort3, SalveId, Address, Lenght)

        Dim bytes As Integer = SerialPort3.BytesToRead
        Dim buffer As Byte() = New Byte(bytes - 1) {}
        SerialPort3.Read(buffer, 0, bytes)
        ReceivedMessage3 = New Byte(bytes - 1) {}
        Array.Copy(buffer, ReceivedMessage3, bytes)

        Me.Invoke(New EventHandler(AddressOf update_dat3))
    End Sub
   
    Public Sub update_dat3(ByVal sender As Object, ByVal e As System.EventArgs)

        On Error Resume Next

        Dim c2 As Integer

        If ReceivedMessage3.Length = 205 Then

            c2 = 0
            SalveId = ReceivedMessage3(0)
            For v1 = 3 To ReceivedMessage3.Length - 3 Step 4

                vIN = New Byte() {ReceivedMessage3(v1 + 3), ReceivedMessage3(v1 + 2), ReceivedMessage3(v1 + 1), ReceivedMessage3(v1)}
                Dim s1 As Single = BitConverter.ToSingle(vIN, 0)
                'RichTextBox2.AppendText(c1 & "      " & Math.Round(Convert.ToDouble(s1), 2) & Environment.NewLine)

                Arr1(c2) = Math.Round(Convert.ToDouble(s1), 2)

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
        ElseIf ReceivedMessage3.Length = Nothing Then

        End If

    End Sub


    'JIG 4 DATA SEND AND RECIVED 
    Private Sub Timer4_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer4.Tick

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

        Dim bytes As Integer = SerialPort4.BytesToRead
        Dim buffer As Byte() = New Byte(bytes - 1) {}
        SerialPort4.Read(buffer, 0, bytes)
        ReceivedMessage4 = New Byte(bytes - 1) {}
        Array.Copy(buffer, ReceivedMessage4, bytes)

        Me.Invoke(New EventHandler(AddressOf update_dat4))


    End Sub
    
    Public Sub update_dat4(ByVal sender As Object, ByVal e As System.EventArgs)

        On Error Resume Next

        Dim c2 As Integer
        If ReceivedMessage4.Length = 205 Then

            c2 = 0
            SalveId = ReceivedMessage4(0)
            For v1 = 3 To ReceivedMessage4.Length - 3 Step 4

                vIN = New Byte() {ReceivedMessage4(v1 + 3), ReceivedMessage4(v1 + 2), ReceivedMessage4(v1 + 1), ReceivedMessage4(v1)}
                Dim s1 As Single = BitConverter.ToSingle(vIN, 0)
                'RichTextBox2.AppendText(c1 & "      " & Math.Round(Convert.ToDouble(s1), 2) & Environment.NewLine)

                Arr1(c2) = Math.Round(Convert.ToDouble(s1), 2)

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
    End Sub

    'JIG 5 DATA SEND
    Private Sub Timer5_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer5.Tick
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

        Dim bytes As Integer = SerialPort5.BytesToRead
        Dim buffer As Byte() = New Byte(bytes - 1) {}
        SerialPort5.Read(buffer, 0, bytes)
        ReceivedMessage5 = New Byte(bytes - 1) {}
        Array.Copy(buffer, ReceivedMessage5, bytes)

        Me.Invoke(New EventHandler(AddressOf update_dat5))

    End Sub
    
    Public Sub update_dat5(ByVal sender As Object, ByVal e As System.EventArgs)

        On Error Resume Next

        Dim c2 As Integer
        If ReceivedMessage5.Length = 205 Then

            c2 = 0
            SalveId = ReceivedMessage5(0)
            For v1 = 3 To ReceivedMessage5.Length - 3 Step 4

                vIN = New Byte() {ReceivedMessage5(v1 + 3), ReceivedMessage5(v1 + 2), ReceivedMessage5(v1 + 1), ReceivedMessage5(v1)}
                Dim s1 As Single = BitConverter.ToSingle(vIN, 0)
                'RichTextBox2.AppendText(c1 & "      " & Math.Round(Convert.ToDouble(s1), 2) & Environment.NewLine)

                Arr1(c2) = Math.Round(Convert.ToDouble(s1), 2)

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
    End Sub
    Private Sub Timer7_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer7.Tick
        JIGStatus(DataGridView2)
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
            RichTextBox2.AppendText(RS.Trim & vbCr)
            Dim mID As String = RS.Substring(2, 1)
            If strArray2(mID) <> RS Then
                strArray2(mID) = RS
                If RS.Substring(3, 1) = 0 Then
                    Dim query As String = "SELECT * FROM `modeldetails` WHERE `F_Status`= '1' and `Id`= '" & RS.Substring(2, 1) & "'"
                    Dim dt2 As DataTable = GetData(query)
                    If dt2.Rows.Count() > 0 Then
                        Dim Id As String = dt2.Rows(0).Item("Id").ToString
                        'Dim Currenttime1 As String = System.DateTime.Now.ToString("hh:mm:ss tt")
                        Dim x1 As String = "UPDATE modeldetails SET F_Status = '0' where Id = '" & Id & "'"    ''''Open Flap
                        MyNonQuery(x1)
                        RS = ""
                        'Dim x1 As String = "UPDATE modeldetails SET status = '0', endtime = '" & Currenttime1 & "' where Id = '" & Id & "'"
                        'MyNonQuery(x1)
                    End If
                ElseIf RS.Substring(3, 1) = 1 Then
                    Dim query As String = "SELECT * FROM `modeldetails` WHERE `F_Status` = '0' and `Id`= '" & RS.Substring(2, 1) & "'"
                    Dim dt As DataTable = GetData(query)
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
            End If
            Label3.Text = ""
        Else

            Label3.Text = Label3.Text + x.Trim

        End If

exit1:


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

   
    
    Private Sub RichTextBox2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RichTextBox2.TextChanged
        On Error Resume Next

        Dim lineCount As Integer = RichTextBox2.Lines.Length
        If lineCount >= 60 Then
            RichTextBox2.Clear()
        End If

    End Sub

    
    Private Sub Timer8_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer8.Tick

        On Error Resume Next

        Dim i As Integer = 1
        Dim joinstr As String
        Dim checkjoinstr As Integer
        joinstr = String.Join("", "0" & CStr(i), JIG(i, 2), JIG(i, 3), JIG(i, 4), JIG(i, 5), JIG(i, 6), JIG(i, 7), JIG(i, 8), JIG(i, 9), JIG(i, 10), JIG(i, 11), JIG(i, 12), JIG(i, 13))
        checkjoinstr = joinstr.Substring(1, 1)
        If strArray(checkjoinstr) <> joinstr Then
            strArray(checkjoinstr) = joinstr

            'Dim x As String = joinstr

           
            Result(i)

        End If


        ' ToolStripStatusLabel1.Text = SENDCOMMAND(i, "QR?")



    End Sub

    Private Sub Timer9_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer9.Tick

        On Error Resume Next

        Dim i As Integer = 2
        Dim joinstr As String
        Dim checkjoinstr As Integer
        joinstr = String.Join("", "0" & CStr(i), JIG(i, 2), JIG(i, 3), JIG(i, 4), JIG(i, 5), JIG(i, 6), JIG(i, 7), JIG(i, 8), JIG(i, 9), JIG(i, 10), JIG(i, 11), JIG(i, 12), JIG(i, 13))
        checkjoinstr = joinstr.Substring(1, 1)
        If strArray(checkjoinstr) <> joinstr Then
            strArray(checkjoinstr) = joinstr

            'Dim x As String = joinstr

            Result(i)

        End If


        'ToolStripStatusLabel2.Text = SENDCOMMAND(i, "QR?")



    End Sub

    Private Sub Timer10_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer10.Tick

        On Error Resume Next

        Dim i As Integer = 3
        Dim joinstr As String
        Dim checkjoinstr As Integer
        joinstr = String.Join("", "0" & CStr(i), JIG(i, 2), JIG(i, 3), JIG(i, 4), JIG(i, 5), JIG(i, 6), JIG(i, 7), JIG(i, 8), JIG(i, 9), JIG(i, 10), JIG(i, 11), JIG(i, 12), JIG(i, 13))
        checkjoinstr = joinstr.Substring(1, 1)
        If strArray(checkjoinstr) <> joinstr Then
            strArray(checkjoinstr) = joinstr

            'Dim x As String = joinstr

            Result(i)

        End If


        ' ToolStripStatusLabel3.Text = SENDCOMMAND(i, "QR?")

    End Sub

    Private Sub Timer11_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer11.Tick

        On Error Resume Next

        Dim i As Integer = 4
        Dim joinstr As String
        Dim checkjoinstr As Integer
        joinstr = String.Join("", "0" & CStr(i), JIG(i, 2), JIG(i, 3), JIG(i, 4), JIG(i, 5), JIG(i, 6), JIG(i, 7), JIG(i, 8), JIG(i, 9), JIG(i, 10), JIG(i, 11), JIG(i, 12), JIG(i, 13))
        checkjoinstr = joinstr.Substring(1, 1)
        If strArray(checkjoinstr) <> joinstr Then
            strArray(checkjoinstr) = joinstr

            'Dim x As String = joinstr

            Result(i)

        End If
        '


        'ToolStripStatusLabel4.Text = SENDCOMMAND(i, "QR?")

    End Sub

    Private Sub Timer12_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer12.Tick

        On Error Resume Next

        Dim i As Integer = 5
        Dim joinstr As String
        Dim checkjoinstr As Integer
        joinstr = String.Join("", "0" & CStr(i), JIG(i, 2), JIG(i, 3), JIG(i, 4), JIG(i, 5), JIG(i, 6), JIG(i, 7), JIG(i, 8), JIG(i, 9), JIG(i, 10), JIG(i, 11), JIG(i, 12), JIG(i, 13))
        checkjoinstr = joinstr.Substring(1, 1)
        If strArray(checkjoinstr) <> joinstr Then
            strArray(checkjoinstr) = joinstr

            'Dim x As String = joinstr

            Result(i)

        End If


        'ToolStripStatusLabel5.Text = SENDCOMMAND(i, "QR?")

    End Sub

    Dim dealy_sd As Integer
    Dim counter_sd As Integer

    Private Sub Timer13_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer13.Tick
        On Error Resume Next

        dealy_sd = dealy_sd + 1
        If dealy_sd >= 2 Then
            dealy_sd = 0
            If counter_sd < 5 Then
                counter_sd = counter_sd + 1

                'Dim command_send As String = "$0" + CStr(counter_sd) + "QR?"
                'RichTextBox2.AppendText(command_send.TrimEnd & Chr(13))
                'SerialPort6.Write(command_send & vbCr)
                Dim command_send As String = SENDCOMMAND(counter_sd, "QR?")
            Else
                counter_sd = 0
            End If
        End If

    End Sub
End Class