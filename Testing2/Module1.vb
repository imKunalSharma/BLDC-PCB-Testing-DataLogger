Imports System.IO.Ports
Imports System.IO
Imports MySql.Data.MySqlClient
Module Module1


    'Voltage    - 1003 V
    'Current    - 1015 A
    'Wattage    - 1023 KWH
    'TDH        - 1091 
    'PF         - 1055  PF

   

    Public conn As New MySqlConnection("server=localhost;User Id=root;database=orient")


    
    Public BaudRate As String

   







    Public StartLow As Byte
    Public StartHigh As Byte
    Public LengthLow As Byte
    Public LengthHigh As Byte


    Public SalveId As Byte '= 1

    Public Address As UShort = 1000 'float
    Public Lenght As UShort = 100

    Public vIN() As Byte



    Public Voltage As String
    Public Current As String
    Public Wattage As String
    Public THD_Voltage As String
    Public Power_Factor As String
    Public IR As String


    Public CurrentDateTime As String


    Public strArray(40) As String
    Public strArray2(40) As String

    Public Arr1(49) As String
    Public Arr2(49) As String
    Public Arr3(49) As String
    Public Arr4(49) As String
    Public Arr5(49) As String


    Public ReceivedMessage1 As Byte()
    Public ReceivedMessage2 As Byte()
    Public ReceivedMessage3 As Byte()
    Public ReceivedMessage4 As Byte()
    Public ReceivedMessage5 As Byte()

    Public MInterval As Integer = 300

    Public FlagArr(5) As Integer

    Sub CheckFolder(ByVal folderPath As String)

        If Not Directory.Exists(folderPath) Then
            Directory.CreateDirectory(folderPath)
        End If

    End Sub

    Public Path_RD As String
    Public header As New List(Of String) From {"'Model'", "'Voltage'", "'Current'", "'Wattage'", "'Power Factor'", "'DateTime'"}
    Sub CreateCsvFile(ByVal folderPath As String, ByVal filename As String, ByVal data As List(Of List(Of String)))
        ' Create the folder if it doesn't exist
        If Not Directory.Exists(folderPath) Then
            Directory.CreateDirectory(folderPath)
        End If

        ' Create the CSV file if it doesn't exist
        Dim filePath As String = Path.Combine(folderPath, filename)
        If Not File.Exists(filePath) Then
            Using writer As New StreamWriter(filePath)
                ' Write the header row
                Dim headerLine As String = String.Join(",", header)
                writer.WriteLine(headerLine)
            End Using
        End If

        ' Append data to the CSV file
        Using writer As New StreamWriter(filePath, True)
            For Each row As List(Of String) In data
                Dim line As String = String.Join(",", row)
                writer.WriteLine(line)
            Next
        End Using

    End Sub

    Public Function delay()
        For i = 0 To 500000
            Application.DoEvents()
        Next i
    End Function

    Public Sub wait1(ByVal interval As Integer)
        Dim stopW As New Stopwatch
        stopW.Start()
        Do While stopW.ElapsedMilliseconds < interval
            Application.DoEvents()
        Loop
        stopW.Stop()
    End Sub

    Public Sub wait2(ByVal interval As Integer)
        Dim stopW As New Stopwatch
        stopW.Start()
        Do While stopW.ElapsedMilliseconds < interval
            Application.DoEvents()
        Loop
        stopW.Stop()
    End Sub

    Public Sub wait3(ByVal interval As Integer)
        Dim stopW As New Stopwatch
        stopW.Start()
        Do While stopW.ElapsedMilliseconds < interval
            Application.DoEvents()
        Loop
        stopW.Stop()
    End Sub

    Public Sub wait4(ByVal interval As Integer)
        Dim stopW As New Stopwatch
        stopW.Start()
        Do While stopW.ElapsedMilliseconds < interval
            Application.DoEvents()
        Loop
        stopW.Stop()
    End Sub

    Public Sub wait5(ByVal interval As Integer)
        Dim stopW As New Stopwatch
        stopW.Start()
        Do While stopW.ElapsedMilliseconds < interval
            Application.DoEvents()
        Loop
        stopW.Stop()
    End Sub

    Public Sub wait(ByVal interval As Integer)
        Dim stopW As New Stopwatch
        stopW.Start()
        Do While stopW.ElapsedMilliseconds < interval
            Application.DoEvents()
        Loop
        stopW.Stop()
    End Sub

    'create new DataTable object to hold results
    Public Function GetData(ByVal query As String) As DataTable
        Dim cmd As MySqlCommand = New MySqlCommand(query)
        Using sda As MySqlDataAdapter = New MySqlDataAdapter()
            cmd.Connection = conn
            sda.SelectCommand = cmd
            Using dt As DataTable = New DataTable()
                sda.Fill(dt)
                Return dt
            End Using
        End Using
    End Function


    'insert funcation
    Public Sub MyNonQuery(ByVal SQCommand As String)
        Dim SQLCMD As New MySqlCommand(SQCommand, conn)
        conn.Open()
        SQLCMD.ExecuteNonQuery()
        conn.Close()
    End Sub


    Public JIG(5, 14) As String
    Public Sub JIGStatus(ByRef datagrid As DataGridView)

        'On Error Resume Next


        datagrid.AutoGenerateColumns = False
        datagrid.ColumnCount = 15
        'Add Columns.
        datagrid.Columns(0).Name = "JIG"
        datagrid.Columns(1).Name = "Model"
        datagrid.Columns(2).Name = "Status"
        datagrid.Columns(3).Name = "M TL"
        datagrid.Columns(4).Name = "TL 1"
        datagrid.Columns(5).Name = "TL 2"
        datagrid.Columns(6).Name = "TL 3"
        datagrid.Columns(7).Name = "TL 4"
        datagrid.Columns(8).Name = "TL 5"
        datagrid.Columns(9).Name = "TL 6"
        datagrid.Columns(10).Name = "TL 7"
        datagrid.Columns(11).Name = "TL 8"
        datagrid.Columns(12).Name = "TL 9"
        datagrid.Columns(13).Name = "TL 10"
        datagrid.Columns(14).Name = "F Status"

        datagrid.Columns(0).Width = 40
        datagrid.Columns(1).Width = 60
        datagrid.Columns(2).Width = 40
        datagrid.Columns(3).Width = 40
        datagrid.Columns(4).Width = 40
        datagrid.Columns(5).Width = 40
        datagrid.Columns(6).Width = 40
        datagrid.Columns(7).Width = 40
        datagrid.Columns(8).Width = 40
        datagrid.Columns(9).Width = 40
        datagrid.Columns(10).Width = 40
        datagrid.Columns(11).Width = 40
        datagrid.Columns(12).Width = 40
        datagrid.Columns(13).Width = 50
        datagrid.Columns(14).Width = 80

        datagrid.RowHeadersVisible = False

        datagrid.EnableHeadersVisualStyles = False
        datagrid.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkBlue
        datagrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        datagrid.ColumnHeadersDefaultCellStyle.Font = New Font("Trebuchet MS", 8, FontStyle.Bold)
        datagrid.GridColor = Color.Black

        datagrid.Rows.Clear()

        conn.Open()
        Dim cmd As New MySqlCommand("SELECT * FROM `modeldetails`", conn)
        Dim rd As MySqlDataReader
        Try
            rd = cmd.ExecuteReader
            Dim x As Integer = 1
            While rd.Read


                JIG(x, 0) = rd("Zig").ToString
                JIG(x, 1) = rd("modelname").ToString
                JIG(x, 2) = rd("status").ToString  'for Realy
                JIG(x, 3) = rd("main_status").ToString  'For result

                JIG(x, 4) = rd("P1")
                JIG(x, 5) = rd("P2")
                JIG(x, 6) = rd("P3")
                JIG(x, 7) = rd("P4")
                JIG(x, 8) = rd("P5")
                JIG(x, 9) = rd("P6")
                JIG(x, 10) = rd("P7")
                JIG(x, 11) = rd("P8")
                JIG(x, 12) = rd("P9")
                JIG(x, 13) = rd("P10")
                JIG(x, 14) = rd("F_Status")

                datagrid.Rows.Add(JIG(x, 0), JIG(x, 1), JIG(x, 2), JIG(x, 3), JIG(x, 4), JIG(x, 5), JIG(x, 6), JIG(x, 7), JIG(x, 8), JIG(x, 9), JIG(x, 10), JIG(x, 11), JIG(x, 12), JIG(x, 13), JIG(x, 14))

                If JIG(x, 2) = "1" Then
                    datagrid.Rows(x - 1).Cells(2).Style.BackColor = Color.Green
                    datagrid.Rows(x - 1).Cells(2).Style.ForeColor = Color.White
                ElseIf JIG(x, 2) = "0" Then
                    datagrid.Rows(x - 1).Cells(2).Style.BackColor = Color.White
                    datagrid.Rows(x - 1).Cells(2).Style.ForeColor = Color.Black
                End If

                For i = 3 To 13
                    If JIG(x, i) = 1 Then
                        datagrid.Rows(x - 1).Cells(i).Style.BackColor = Color.Green
                        datagrid.Rows(x - 1).Cells(i).Style.ForeColor = Color.White
                    ElseIf JIG(x, i) = 2 Then
                        datagrid.Rows(x - 1).Cells(i).Style.BackColor = Color.Red
                        datagrid.Rows(x - 1).Cells(i).Style.ForeColor = Color.White
                    ElseIf JIG(x, i) = 3 Then
                        datagrid.Rows(x - 1).Cells(i).Style.BackColor = Color.Yellow
                        datagrid.Rows(x - 1).Cells(i).Style.ForeColor = Color.Black
                    ElseIf JIG(x, i) = 0 Then
                        datagrid.Rows(x - 1).Cells(i).Style.BackColor = Color.White
                        datagrid.Rows(x - 1).Cells(i).Style.ForeColor = Color.Black
                    End If
                Next


                x = x + 1

            End While
            rd.Close()
        Catch ex As Exception
            'MsgBox(ex.Message)
            My.Computer.FileSystem.WriteAllText("C:\Reports\log.txt", "JIG Status " & ex.Message & " " & "Date : " + System.DateTime.Now.ToString("dd/MM/yyyy") & "  " & "Time : " + System.DateTime.Now.ToString("HH:mm:ss") & Chr(13), True)

        End Try
        conn.Close()



    End Sub




End Module
