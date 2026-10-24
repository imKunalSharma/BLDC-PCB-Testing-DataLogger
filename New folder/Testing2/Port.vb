Imports VB = Microsoft.VisualBasic
Imports System.Threading

Module Port
    Public Function PortConnect1(ByRef Port As String) As String
        Try

            Form1.SerialPort1 = New IO.Ports.SerialPort(Port, 9600, IO.Ports.Parity.None, 8, IO.Ports.StopBits.One)
            Form1.SerialPort1.Open()

            Return Port & " is Connected, 1"

        Catch ex As Exception
            Return ErrorToString() & ", 0"
        End Try

    End Function

    Public Function PortConnect2(ByRef Port As String) As String
        Try

            Form1.SerialPort2 = New IO.Ports.SerialPort(Port, 9600, IO.Ports.Parity.None, 8, IO.Ports.StopBits.One)
            Form1.SerialPort2.Open()

            Return Port & " is Connected, 1"

        Catch ex As Exception
            Return ErrorToString() & ", 0"
        End Try

    End Function

    Public Function PortConnect3(ByRef Port As String) As String
        Try

            Form1.SerialPort3 = New IO.Ports.SerialPort(Port, 9600, IO.Ports.Parity.None, 8, IO.Ports.StopBits.One)
            Form1.SerialPort3.Open()

            Return Port & " is Connected, 1"

        Catch ex As Exception
            Return ErrorToString() & ", 0"
        End Try

    End Function

    Public Function PortConnect4(ByRef Port As String) As String
        Try

            Form1.SerialPort4 = New IO.Ports.SerialPort(Port, 9600, IO.Ports.Parity.None, 8, IO.Ports.StopBits.One)
            Form1.SerialPort4.Open()

            Return Port & " is Connected, 1"

        Catch ex As Exception
            Return ErrorToString() & ", 0"
        End Try

    End Function

    Public Function PortConnect5(ByRef Port As String) As String
        Try

            Form1.SerialPort5 = New IO.Ports.SerialPort(Port, 9600, IO.Ports.Parity.None, 8, IO.Ports.StopBits.One)
            Form1.SerialPort5.Open()

            Return Port & " is Connected, 1"

        Catch ex As Exception
            Return ErrorToString() & ", 0"
        End Try

    End Function

    Public Function PortConnect6(ByRef Port As String) As String
        Try

            Form1.SerialPort6 = New IO.Ports.SerialPort(Port, 9600, IO.Ports.Parity.None, 8, IO.Ports.StopBits.One)
            Form1.SerialPort6.Open()

            Return Port & " is Connected, 1"

        Catch ex As Exception
            Return ErrorToString() & ", 0"
        End Try

    End Function

    Public Function ViewFuncation(ByRef Datagrid As DataGridView)

        Datagrid.AutoGenerateColumns = False
        Datagrid.ColumnCount = 2
        'Add Columns.
        Datagrid.Columns(0).Name = "Port"
        Datagrid.Columns(1).Name = "Status"

        Datagrid.Columns(0).Width = 60
        Datagrid.Columns(1).Width = 200

        Datagrid.RowHeadersVisible = False

        Datagrid.EnableHeadersVisualStyles = False
        Datagrid.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkBlue
        Datagrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        Datagrid.ColumnHeadersDefaultCellStyle.Font = New Font("Trebuchet MS", 10, FontStyle.Bold)
        Datagrid.GridColor = Color.Black

        Dim dt As DataTable = GetData("SELECT * FROM `port` WHERE `Id` = 1")
        If dt.Rows.Count() > 0 Then

            Dim Port1 As String() = PortConnect1(dt.Rows(0).Item("Port1")).Split(","c)
            Dim Port2 As String() = PortConnect2(dt.Rows(0).Item("Port2")).Split(","c)
            Dim Port3 As String() = PortConnect3(dt.Rows(0).Item("Port3")).Split(","c)
            Dim Port4 As String() = PortConnect4(dt.Rows(0).Item("Port4")).Split(","c)
            Dim Port5 As String() = PortConnect5(dt.Rows(0).Item("Port5")).Split(","c)
            Dim Port6 As String() = PortConnect6(dt.Rows(0).Item("Port6")).Split(","c)

            Datagrid.Rows.Add(dt.Rows(0).Item("Port1"), Port1(0))
            Datagrid.Rows.Add(dt.Rows(0).Item("Port2"), Port2(0))
            Datagrid.Rows.Add(dt.Rows(0).Item("Port3"), Port3(0))
            Datagrid.Rows.Add(dt.Rows(0).Item("Port4"), Port4(0))
            Datagrid.Rows.Add(dt.Rows(0).Item("Port5"), Port5(0))
            Datagrid.Rows.Add(dt.Rows(0).Item("Port6"), Port6(0))

            If Port1(1) = 1 Then
                Datagrid.Rows(0).DefaultCellStyle.BackColor = Color.Green
                Datagrid.Rows(0).DefaultCellStyle.ForeColor = Color.White

            ElseIf Port1(1) = 0 Then
                Datagrid.Rows(0).DefaultCellStyle.BackColor = Color.Red
                Datagrid.Rows(0).DefaultCellStyle.ForeColor = Color.White
            End If

            If Port2(1) = 1 Then
                Datagrid.Rows(1).DefaultCellStyle.BackColor = Color.Green
                Datagrid.Rows(1).DefaultCellStyle.ForeColor = Color.White
            ElseIf Port2(1) = 0 Then
                Datagrid.Rows(1).DefaultCellStyle.BackColor = Color.Red
                Datagrid.Rows(1).DefaultCellStyle.ForeColor = Color.White
            End If

            If Port3(1) = 1 Then
                Datagrid.Rows(2).DefaultCellStyle.BackColor = Color.Green
                Datagrid.Rows(2).DefaultCellStyle.ForeColor = Color.White
            ElseIf Port3(1) = 0 Then
                Datagrid.Rows(2).DefaultCellStyle.BackColor = Color.Red
                Datagrid.Rows(2).DefaultCellStyle.ForeColor = Color.White
            End If

            If Port4(1) = 1 Then
                Datagrid.Rows(3).DefaultCellStyle.BackColor = Color.Green
                Datagrid.Rows(3).DefaultCellStyle.ForeColor = Color.White
            ElseIf Port4(1) = 0 Then
                Datagrid.Rows(3).DefaultCellStyle.BackColor = Color.Red
                Datagrid.Rows(3).DefaultCellStyle.ForeColor = Color.White
            End If

            If Port5(1) = 1 Then
                Datagrid.Rows(4).DefaultCellStyle.BackColor = Color.Green
                Datagrid.Rows(4).DefaultCellStyle.ForeColor = Color.White
            ElseIf Port5(1) = 0 Then
                Datagrid.Rows(4).DefaultCellStyle.BackColor = Color.Red
                Datagrid.Rows(4).DefaultCellStyle.ForeColor = Color.White
            End If

            If Port6(1) = 1 Then
                Datagrid.Rows(5).DefaultCellStyle.BackColor = Color.Green
                Datagrid.Rows(5).DefaultCellStyle.ForeColor = Color.White
            ElseIf Port6(1) = 0 Then
                Datagrid.Rows(5).DefaultCellStyle.BackColor = Color.Red
                Datagrid.Rows(5).DefaultCellStyle.ForeColor = Color.White
            End If


        End If





        Return Nothing
    End Function



    Public Function SENDCOMMAND(ByRef JIGID As String, ByRef STR As String)

        Try
            'Threading.Thread.Sleep(1000)
            Dim command_send As String = "$0" + CStr(JIGID) + STR

            Form1.RichTextBox1.AppendText(command_send & vbCr)
            'Form1.RichTextBox3.AppendText(command_send & vbCr)

            Form1.SerialPort6.Write(command_send & vbCr)

            MeDelay()
            'Thread.Sleep(200) ' Delay in milliseconds

            'My.Computer.FileSystem.WriteAllText("C:\Reports\log.txt", command_send & " " & "Date : " + System.DateTime.Now.ToString("dd/MM/yyyy") & "  " & "Time : " + System.DateTime.Now.ToString("HH:mm:ss") & Chr(13), True)
            Return command_send
        Catch ex As Exception
            Return ErrorToString()
            My.Computer.FileSystem.WriteAllText("C:\Reports\log.txt", "Write Command " & ErrorToString() & " " & "Date : " + System.DateTime.Now.ToString("dd/MM/yyyy") & "  " & "Time : " + System.DateTime.Now.ToString("HH:mm:ss") & Chr(13), True)
        End Try

    End Function
    Sub MeDelay()
        Dim icount As Integer = 1
        For icount = 1 To 100000000
            icount = icount + 1
        Next
    End Sub


    '$2401RLN$0D  - READLY ON
    '$2401RLF$0D  - REALDY OFF
    '$2401TWR$0D  - TOWER LED RED
    '$2401TWG$0D  - TOWER LED GREEN
    '$2401TWY$0D  - TOWER LED YELLOW
    '$2401TWF$0D  - TOWER LED OFF
    '$2401IN00000000 - JIG POSition WIse Result

    '$2401QR$0D  - JIG Status

    Public Function Result(ByRef SendJIG As Integer)

        'JIGStatus(Form1.DataGridView2)

        If SendJIG = 1 Then
            'Tower light
            If JIG(SendJIG, 3) = 1 Then
                Form1.ToolStripStatusLabel1.Text = SENDCOMMAND(SendJIG, "TWG")
            ElseIf JIG(SendJIG, 3) = 2 Then
                Form1.ToolStripStatusLabel1.Text = SENDCOMMAND(SendJIG, "TWR")
            ElseIf JIG(SendJIG, 3) = 3 Then
                Form1.ToolStripStatusLabel1.Text = SENDCOMMAND(SendJIG, "TWY")
            ElseIf JIG(SendJIG, 3) = 0 Then
                Form1.ToolStripStatusLabel1.Text = SENDCOMMAND(SendJIG, "TWF")
            End If
            Threading.Thread.Sleep(500)
            'JIGStatus(Form1.DataGridView2) '
            Dim str As String = ""

            For i = 4 To 13
                If JIG(SendJIG, i) = 1 Then
                    str = str & JIG(SendJIG, i)
                ElseIf JIG(SendJIG, i) = 2 Then
                    str = str & JIG(SendJIG, i)
                ElseIf JIG(SendJIG, i) = 3 Then
                    str = str & JIG(SendJIG, i)
                ElseIf JIG(SendJIG, i) = 0 Then
                    str = str & JIG(SendJIG, i)
                End If
            Next
            SENDCOMMAND(SendJIG, "IN" & str)

        ElseIf SendJIG = 2 Then
            'Tower light
            If JIG(SendJIG, 3) = 1 Then
                Form1.ToolStripStatusLabel2.Text = SENDCOMMAND(SendJIG, "TWG")
            ElseIf JIG(SendJIG, 3) = 2 Then
                Form1.ToolStripStatusLabel2.Text = SENDCOMMAND(SendJIG, "TWR")
            ElseIf JIG(SendJIG, 3) = 3 Then
                Form1.ToolStripStatusLabel2.Text = SENDCOMMAND(SendJIG, "TWY")
            ElseIf JIG(SendJIG, 3) = 0 Then
                Form1.ToolStripStatusLabel2.Text = SENDCOMMAND(SendJIG, "TWF")
            End If
            Threading.Thread.Sleep(500)
            'JIGStatus(Form1.DataGridView2)
            Dim str As String = ""
            For i = 4 To 13
                If JIG(SendJIG, i) = 1 Then
                    str = str & JIG(SendJIG, i)
                ElseIf JIG(SendJIG, i) = 2 Then
                    str = str & JIG(SendJIG, i)
                ElseIf JIG(SendJIG, i) = 3 Then
                    str = str & JIG(SendJIG, i)
                ElseIf JIG(SendJIG, i) = 0 Then
                    str = str & JIG(SendJIG, i)
                End If
            Next
            SENDCOMMAND(SendJIG, "IN" & str)

        ElseIf SendJIG = 3 Then
            'Tower light
            If JIG(SendJIG, 3) = 1 Then
                Form1.ToolStripStatusLabel3.Text = SENDCOMMAND(SendJIG, "TWG")
            ElseIf JIG(SendJIG, 3) = 2 Then
                Form1.ToolStripStatusLabel3.Text = SENDCOMMAND(SendJIG, "TWR")
            ElseIf JIG(SendJIG, 3) = 3 Then
                Form1.ToolStripStatusLabel3.Text = SENDCOMMAND(SendJIG, "TWY")
            ElseIf JIG(SendJIG, 3) = 0 Then
                Form1.ToolStripStatusLabel3.Text = SENDCOMMAND(SendJIG, "TWF")
            End If
            Threading.Thread.Sleep(500)
            'JIGStatus(Form1.DataGridView2)
            Dim str As String = ""
            For i = 4 To 13
                If JIG(SendJIG, i) = 1 Then
                    str = str & JIG(SendJIG, i)
                ElseIf JIG(SendJIG, i) = 2 Then
                    str = str & JIG(SendJIG, i)
                ElseIf JIG(SendJIG, i) = 3 Then
                    str = str & JIG(SendJIG, i)
                ElseIf JIG(SendJIG, i) = 0 Then
                    str = str & JIG(SendJIG, i)
                End If
            Next
            SENDCOMMAND(SendJIG, "IN" & str)

        ElseIf SendJIG = 4 Then
            'Tower light
            If JIG(SendJIG, 3) = 1 Then
                Form1.ToolStripStatusLabel4.Text = SENDCOMMAND(SendJIG, "TWG")
            ElseIf JIG(SendJIG, 3) = 2 Then
                Form1.ToolStripStatusLabel4.Text = SENDCOMMAND(SendJIG, "TWR")
            ElseIf JIG(SendJIG, 3) = 3 Then
                Form1.ToolStripStatusLabel4.Text = SENDCOMMAND(SendJIG, "TWY")
            ElseIf JIG(SendJIG, 3) = 0 Then
                Form1.ToolStripStatusLabel4.Text = SENDCOMMAND(SendJIG, "TWF")
            End If
            Threading.Thread.Sleep(500)
            'JIGStatus(Form1.DataGridView2)
            Dim str As String = ""
            For i = 4 To 13
                If JIG(SendJIG, i) = 1 Then
                    str = str & JIG(SendJIG, i)
                ElseIf JIG(SendJIG, i) = 2 Then
                    str = str & JIG(SendJIG, i)
                ElseIf JIG(SendJIG, i) = 3 Then
                    str = str & JIG(SendJIG, i)
                ElseIf JIG(SendJIG, i) = 0 Then
                    str = str & JIG(SendJIG, i)
                End If
            Next
            SENDCOMMAND(SendJIG, "IN" & str)
        ElseIf SendJIG = 5 Then
            'Tower light
            If JIG(SendJIG, 3) = 1 Then
                Form1.ToolStripStatusLabel5.Text = SENDCOMMAND(SendJIG, "TWG")
            ElseIf JIG(SendJIG, 3) = 2 Then
                Form1.ToolStripStatusLabel5.Text = SENDCOMMAND(SendJIG, "TWR")
            ElseIf JIG(SendJIG, 3) = 3 Then
                Form1.ToolStripStatusLabel5.Text = SENDCOMMAND(SendJIG, "TWY")
            ElseIf JIG(SendJIG, 3) = 0 Then
                Form1.ToolStripStatusLabel5.Text = SENDCOMMAND(SendJIG, "TWF")
            End If
            Threading.Thread.Sleep(500)
            'JIGStatus(Form1.DataGridView2)
            Dim str As String = ""
            For i = 4 To 13
                If JIG(SendJIG, i) = 1 Then
                    str = str & JIG(SendJIG, i)
                ElseIf JIG(SendJIG, i) = 2 Then
                    str = str & JIG(SendJIG, i)
                ElseIf JIG(SendJIG, i) = 3 Then
                    str = str & JIG(SendJIG, i)
                ElseIf JIG(SendJIG, i) = 0 Then
                    str = str & JIG(SendJIG, i)
                End If
            Next
            SENDCOMMAND(SendJIG, "IN" & str)
        End If


        Return Nothing
    End Function





End Module
