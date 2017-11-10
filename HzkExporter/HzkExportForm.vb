Imports System.Runtime
Imports System.Runtime.InteropServices
Public Class HzkExporter

    Private Const _ZoneStart = 161
    Private Const _BitStart = 161
    Private Const _AscStart = 32
    Private Const _ZoneMax = 254
    Private Const _BitMax = 254
    Private Const _AscMax = 127

    Private Scene As Bitmap
    Private Cathe As Graphics
    Private MyHzkSize As Integer
    Private MyFont As Font
    Private Offset As Point
    Private MyExportFile As IO.BinaryWriter

#Region "Form Events"
    Private Sub HzkExporter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Offset = Point.Empty
        MyFont = New Font("宋体", 11, FontStyle.Regular)
        HzkSizeCombo.SelectedIndex = 0
        SampleText.Text = "字"
    End Sub

    Private Sub HzkSizeCombo_SselectedIndexChanged(sender As Object, e As EventArgs) Handles HzkSizeCombo.SelectedIndexChanged
        MyHzkSize = Val(HzkSizeCombo.SelectedItem)
        Scene = New Bitmap(MyHzkSize, MyHzkSize, Imaging.PixelFormat.Format32bppArgb)
        Cathe = Graphics.FromImage(Scene)
        Cathe.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        Cathe.TextRenderingHint = Drawing.Text.TextRenderingHint.SystemDefault
        Render()
    End Sub

    Private Sub UpCom_Click(sender As Object, e As EventArgs) Handles UpCom.Click
        Offset.Y -= 1
        Render()
    End Sub

    Private Sub DownCom_Click(sender As Object, e As EventArgs) Handles DownCom.Click
        Offset.Y += 1
        Render()
    End Sub

    Private Sub LeftCom_Click(sender As Object, e As EventArgs) Handles LeftCom.Click
        Offset.X -= 1
        Render()
    End Sub

    Private Sub RightCom_Click(sender As Object, e As EventArgs) Handles RightCom.Click
        Offset.X += 1
        Render()
    End Sub

    Private Sub SampleText_TextChanged(sender As Object, e As EventArgs) Handles SampleText.TextChanged
        Render()
    End Sub

    Private Sub SelectFontCom_Click(sender As Object, e As EventArgs) Handles SelectFontCom.Click
        HzkFontDialog.Font = MyFont
        If HzkFontDialog.ShowDialog = DialogResult.OK Then
            MyFont = HzkFontDialog.Font
            Render()
        End If
    End Sub

    Private Sub ExportAscCom_Click(sender As Object, e As EventArgs) Handles ExportAscCom.Click
        If HzkSaveFileDialog.ShowDialog = DialogResult.OK Then
            ExportAscBGW.RunWorkerAsync()
        End If
    End Sub

    Private Sub ExportHzkCom_Click(sender As Object, e As EventArgs) Handles ExportHzkCom.Click
        If HzkSaveFileDialog.ShowDialog = DialogResult.OK Then
            ExportHzkBGW.RunWorkerAsync()
        End If
    End Sub
#End Region

#Region "BGWs"
    Private Sub ExportHzkBGW_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles ExportHzkBGW.DoWork
        Dim t, tt As Integer
        Using tmpf = New IO.FileStream(HzkSaveFileDialog.FileName, IO.FileMode.Create)
                Using tmpb = New IO.BinaryWriter(tmpf)
                    Using tmpp = New Bitmap(MyHzkSize, MyHzkSize, Imaging.PixelFormat.Format32bppArgb)
                        Using tmpg = Graphics.FromImage(tmpp)
                            For t = _ZoneStart To _ZoneMax
                                For tt = _BitStart To _BitMax
                                    Dim c As String = Chr((t << 8) + tt)
                                    Render(tmpg, c)
                                    tmpb.Write(ScanHzk(tmpp), 0, MyHzkSize * MyHzkSize / 8)
                                Next
                            Next
                        End Using
                    End Using
                End Using
            End Using
            e.Result = $"导出完成。"

    End Sub

    Private Sub ExportAscBGW_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles ExportAscBGW.DoWork
        'Try
        Dim tmpf = New IO.FileStream(HzkSaveFileDialog.FileName, IO.FileMode.Create)
            Dim tmpb = New IO.BinaryWriter(tmpf)
            Dim tmpp = New Bitmap(MyHzkSize / 2, MyHzkSize, Imaging.PixelFormat.Format32bppArgb)
        Dim tmpg = Graphics.FromImage(tmpp)
        Dim t As Integer

        For t = _AscStart To _AscMax
                Dim c As String = Chr(t)
                Render(tmpg, c)
                tmpb.Write(ScanHzk(tmpp), 0, MyHzkSize * MyHzkSize / 16)
            Next
            tmpg.Dispose()
            tmpp.Dispose()
            tmpb.Close()
            tmpf.Close()
            e.Result = $"导出完成。"
        'Catch ex As Exception
        '    e.Result = $"导出时遇到了问题:{ex.Message}"
        'End Try
    End Sub
#End Region

#Region "Render & Scan"
    Private Sub Render()
        If SampleText.Text.Length > 0 Then
            Render(Cathe, SampleText.Text.Substring(0, 1))
            Canvas.Image = Scene
        End If
    End Sub
    Private Sub Render(G As Graphics, C As String)
        Dim tmpa = Asc(C)
        G.Clear(Color.Black)
        G.DrawString(C, MyFont, Brushes.White, Offset)
        If tmpa < 256 AndAlso tmpa > 0 Then
            G.FillRectangle(New SolidBrush(Color.FromArgb(32, 32, 32)), MyHzkSize \ 2, 0, MyHzkSize \ 2, MyHzkSize)
        End If
    End Sub
    Private Function ScanHzk(B As Bitmap) As Byte()
        Dim bd = B.LockBits(New Rectangle(Point.Empty, New Size(MyHzkSize / 2, MyHzkSize)),
                                Imaging.ImageLockMode.ReadOnly,
                                Imaging.PixelFormat.Format32bppArgb)
        Dim bc(MyHzkSize * MyHzkSize * 4) As Byte
        Marshal.Copy(bd.Scan0, bc, 0, bc.Length)
        B.UnlockBits(bd)
        Dim hc(MyHzkSize * MyHzkSize / 8) As Byte
        Dim x, y As Integer
        For y = 0 To MyHzkSize - 1
            For x = 0 To MyHzkSize - 1
                If bc((y * MyHzkSize + x) * 4 + 1) > 128 Then
                    Dim tmpt = x Mod 8
                    hc((y * MyHzkSize + x) \ 8) += 128 / 2 ^ (tmpt)
                End If
            Next
        Next
        Return hc
    End Function

    Private Function ScanAsc(B As Bitmap) As Byte()
        Dim bd = B.LockBits(New Rectangle(Point.Empty, New Size(MyHzkSize / 2, MyHzkSize)),
                                Imaging.ImageLockMode.ReadOnly,
                                Imaging.PixelFormat.Format32bppArgb)
        Dim bc(MyHzkSize * MyHzkSize * 2) As Byte
        Marshal.Copy(bd.Scan0, bc, 0, bc.Length)
        B.UnlockBits(bd)
        Dim hc(MyHzkSize * MyHzkSize / 16) As Byte
        Dim x, y As Integer
        For y = 0 To MyHzkSize - 1
            For x = 0 To MyHzkSize - 1
                If bc((y * MyHzkSize \ 2 + x) * 4 + 1) > 32 Then
                    Dim tmpt = x Mod 8
                    hc((y * MyHzkSize \ 2 + x) \ 8) += 128 / 2 ^ (tmpt)
                End If
            Next
        Next
        Return hc
    End Function
#End Region

End Class
