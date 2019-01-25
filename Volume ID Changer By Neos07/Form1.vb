Imports System.IO

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim AllDrives() As DriveInfo = DriveInfo.GetDrives()
        Dim D As DriveInfo
        If Not File.Exists(My.Application.Info.DirectoryPath & "/Volumeid.exe") Then
            System.IO.File.WriteAllBytes(My.Application.Info.DirectoryPath & "/Volumeid.exe", My.Resources.Volumeid)
        End If
        For Each D In AllDrives
            If D.DriveType = 3 Then
                ComboBox1.Items.Add(D.Name.Remove(D.Name.Length - 1, 1))
            End If
        Next
        ComboBox1.SelectedIndex = 0

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If TextBox1.Text Like "[0-9, A, B, C, D, E, F][0-9, A, B, C, D, E, F][0-9, A, B, C, D, E, F][0-9, A, B, C, D, E, F]-[0-9, A, B, C, D, E, F][0-9, A, B, C, D, E, F][0-9, A, B, C, D, E, F][0-9, A, B, C, D, E, F]" Then
            Dim c As ProcessStartInfo = New ProcessStartInfo()
            c.Arguments = "/c Volumeid.exe " & ComboBox1.SelectedItem & " " & TextBox1.Text
            c.WindowStyle = ProcessWindowStyle.Hidden
            c.CreateNoWindow = True
            c.FileName = "cmd.exe"
            Process.Start(c)
            MsgBox("HWID Changed. Please reboot PC to apply it.", MsgBoxStyle.Information, "HWID Changed")
        Else : MsgBox("Invalid HWID. Please enter a valid HWID.", MsgBoxStyle.Critical, "Invalid HWID")
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        TextBox1.Text = GenerateKeysCode()

    End Sub

    Public Function GenerateKeysCode() As Object

        Dim intRnd As String
        Dim strName As String = ""
        Dim HexSymbols As String = "0123456789ABCDEF"
        Dim intLenght As Object = Len(HexSymbols)

        Randomize()
        For i As Integer = 1 To 4
            intRnd = Int((intLenght * Rnd()) + 1)
            strName = strName & HexSymbols(intRnd - 1)
        Next
        strName = strName & "-"
        For i As Integer = 1 To 4
            intRnd = Int((intLenght * Rnd()) + 1)
            strName = strName & HexSymbols(intRnd - 1)
        Next
        Return strName

    End Function

End Class
