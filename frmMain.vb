Option Strict On
Option Infer On

#Disable Warning IDE1006
Public Class frmMain
    Private WithEvents pnlGrid As New Panel, lblScore As New Label

    Private Const GRID_SIZE As Integer = 4, CELL_SIZE As Integer = 100
    Private Shadows Const PADDING As Integer = 10

    Private ReadOnly Property MaxGridIndex As Integer = GRID_SIZE - 1

    Private board(MaxGridIndex, MaxGridIndex) As Integer, score As New Integer

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim originalWidth As Integer = CELL_SIZE * GRID_SIZE + PADDING * (GRID_SIZE + 3)
        Controls.AddRange(lblScore, pnlGrid)
        Text = "Game of 2048"
        Size = New Size(originalWidth, originalWidth + 70)
        FormBorderStyle = FormBorderStyle.Fixed3D
        MaximizeBox = False
        With lblScore
            .Font = New Font("Arial", 12, FontStyle.Regular)
            .Size = New Size(150, 50)
        End With
        pnlGrid.Dock = DockStyle.Fill
        StartNewGame()
    End Sub

    Private Sub StartNewGame()
        score = 0
        DisplayScore()
        For i As Integer = 0 To MaxGridIndex Step 1
            For j As Integer = 0 To MaxGridIndex Step 1
                board(i, j) = 0
            Next j
        Next i
        GenerateNewNumber()
        GenerateNewNumber()
        pnlGrid.Invalidate()
    End Sub

    Private Sub GenerateNewNumber()
        Dim emptyCells As New List(Of Point), rnd As New Random
        For i As Integer = 0 To MaxGridIndex
            For j As Integer = 0 To MaxGridIndex
                If board(i, j) = 0 Then emptyCells.Add(New Point(i, j))
            Next j
        Next i
        If emptyCells.Count = 0 Then Exit Sub
        Dim randomCell As Point = emptyCells(rnd.Next(emptyCells.Count))
        board(randomCell.X, randomCell.Y) = If(rnd.NextDouble() < 0.9, 2, 4)
    End Sub

    Private Sub DisplayScore()
        With lblScore
            .Text = "Score: " & score
            .Location = New Point(ClientSize.Width \ 3, ClientSize.Height - 30)
            .Width = ClientSize.Width \ 2
        End With
    End Sub

    Private Function Compress(row As Integer()) As Integer()
        Dim newRow As New List(Of Integer)(From x In row Where x <> 0)
        Dim result(MaxGridIndex) As Integer, idx As New Integer

        For i As Integer = 0 To newRow.Count - 1 Step 1
            If i < newRow.Count - 1 AndAlso newRow(i) = newRow(i + 1) Then
                result(idx) = newRow(i) * 2
                score += result(idx)
                i += 1
            Else
                result(idx) = newRow(i)
            End If
            idx += 1
        Next i

        While idx < GRID_SIZE : result(idx) = 0 : idx += 1 : End While
        DisplayScore()
        Return result
    End Function

    Private Sub pnlGrid_Paint(sender As Object, e As PaintEventArgs) Handles pnlGrid.Paint
        Dim g As Graphics = e.Graphics
        For i As Integer = 0 To MaxGridIndex Step 1
            For j As Integer = 0 To MaxGridIndex Step 1
                Dim rect As New Rectangle(j * (CELL_SIZE + PADDING) + PADDING,
                                          i * (CELL_SIZE + PADDING) + PADDING,
                                          CELL_SIZE, CELL_SIZE)
                Dim bgColor As Brush = GridCellColor(board(i, j))
                g.FillRectangle(bgColor, rect)
                If board(i, j) <> 0 Then
                    Dim font As New Font("Arial", 24, FontStyle.Bold)
                    Dim fgColor As Brush = If(board(i, j) <= 8, Brushes.Black, Brushes.White)
                    Dim numText As String = board(i, j).ToString()
                    Dim textSize As SizeF = g.MeasureString(numText, font)
                    Dim textX As Integer = CInt(rect.X + (rect.Width - textSize.Width) / 2)
                    Dim textY As Integer = CInt(rect.Y + (rect.Height - textSize.Height) / 2)
                    g.DrawString(numText, font, fgColor, textX, textY)
                End If
            Next j
        Next i
    End Sub

    Private ReadOnly Property GridCellColor(num As Integer) As Brush
        Get
            Select Case num
                Case 2
                    Return Brushes.LightBlue
                Case 4
                    Return Brushes.LightGreen
                Case 8
                    Return Brushes.Khaki
                Case 16
                    Return Brushes.Tomato
                Case 32
                    Return Brushes.Olive
                Case 64
                    Return Brushes.SteelBlue
                Case 128
                    Return Brushes.CornflowerBlue
                Case 256
                    Return Brushes.Goldenrod
                Case 512
                    Return Brushes.Chocolate
                Case 1024
                    Return Brushes.BlueViolet
                Case 2048
                    Return Brushes.Crimson
                Case Else
                    Return Brushes.LightGray
            End Select
        End Get
    End Property

    Private Sub lblScore_Click(sender As Object, e As EventArgs) Handles lblScore.Click
        If Not (IsGameOver OrElse IsVictory) Then StartNewGame()
    End Sub

    Private Sub frmMain_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Dim oldBoard = CType(board.Clone(), Integer(,)), isBoardMoved As Boolean = False
        If Not (IsGameOver OrElse IsVictory) Then
            Select Case e.KeyCode
                Case Keys.Up
                    For j As Integer = 0 To MaxGridIndex Step 1
                        Dim row(MaxGridIndex) As Integer
                        For i As Integer = 0 To MaxGridIndex Step 1
                            row(i) = board(i, j)
                        Next i
                        For i As Integer = 0 To MaxGridIndex Step 1
                            board(i, j) = Compress(row)(i)
                        Next i
                    Next j
                Case Keys.Down
                    For j As Integer = 0 To MaxGridIndex Step 1
                        Dim row(MaxGridIndex) As Integer
                        For i As Integer = 0 To MaxGridIndex Step 1
                            row(i) = board(MaxGridIndex - i, j)
                        Next i
                        For i As Integer = 0 To MaxGridIndex Step 1
                            board(MaxGridIndex - i, j) = Compress(row)(i)
                        Next i
                    Next j
                Case Keys.Left
                    For i As Integer = 0 To MaxGridIndex Step 1
                        Dim row(MaxGridIndex) As Integer
                        For j As Integer = 0 To MaxGridIndex Step 1
                            row(j) = board(i, j)
                        Next j
                        For j As Integer = 0 To MaxGridIndex Step 1
                            board(i, j) = Compress(row)(j)
                        Next j
                    Next i
                Case Keys.Right
                    For i As Integer = 0 To MaxGridIndex Step 1
                        Dim row(MaxGridIndex) As Integer
                        For j As Integer = 0 To MaxGridIndex Step 1
                            row(j) = board(i, MaxGridIndex - j)
                        Next j
                        For j As Integer = 0 To MaxGridIndex Step 1
                            board(i, MaxGridIndex - j) = Compress(row)(j)
                        Next j
                    Next i
                Case Else
                    Exit Sub
            End Select
            For i As Integer = 0 To MaxGridIndex Step 1
                For j As Integer = 0 To MaxGridIndex Step 1
                    If board(i, j) <> oldBoard(i, j) Then isBoardMoved = True
                Next j
            Next i
            If isBoardMoved Then GenerateNewNumber()
            pnlGrid.Invalidate()
            Exit Sub
        End If
        If e.KeyCode = Keys.Space Then StartNewGame()
        Dim text As String = $"Your final score is {score}. Thanks for playing."
        Dim caption As String = If(IsVictory, "YOU WIN!", "GAME OVER")
        MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        With lblScore
            .Text = "Press SPACE to start a new game."
            .Location = New Point(10, ClientSize.Height - 30)
            .Width = ClientSize.Width
        End With
    End Sub

    Private ReadOnly Property IsGameOver As Boolean
        Get
            For i As Integer = 0 To MaxGridIndex Step 1
                For j As Integer = 0 To MaxGridIndex Step 1
                    Dim isMovableX = i < MaxGridIndex AndAlso board(i, j) = board(i + 1, j)
                    Dim isMovableY = j < MaxGridIndex AndAlso board(i, j) = board(i, j + 1)

                    If board(i, j) = 0 OrElse isMovableX OrElse isMovableY Then Return False
                Next j
            Next i
            Return True
        End Get
    End Property

    Private ReadOnly Property IsVictory As Boolean
        Get
            For i As Integer = 0 To MaxGridIndex Step 1
                For j As Integer = 0 To MaxGridIndex Step 1
                    If board(i, j) = 2048 Then Return True
                Next j
            Next i
            Return False
        End Get
    End Property
#Enable Warning IDE1006

    <STAThread()> Friend Shared Sub Main()
        Application.SetHighDpiMode(HighDpiMode.SystemAware)
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(New frmMain)
    End Sub
End Class