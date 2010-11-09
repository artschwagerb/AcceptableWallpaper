Imports System.Xml
Imports Microsoft.Win32
Imports System.Net
Imports System.Threading

Public Class Form1

    Private Const SPI_SETDESKWALLPAPER As Integer = &H14
    Private Const SPIF_UPDATEINIFILE As Integer = &H1
    Private Const SPIF_SENDWININICHANGE As Integer = &H2
    Private Declare Auto Function SystemParametersInfo Lib "user32.dll" (ByVal uAction As Integer, ByVal uParam As Integer, ByVal lpvParam As String, ByVal fuWinIni As Integer) As Integer
    Const WallpaperFile As String = "c:\wallpaper.jpg"

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Timer1.Start()
    End Sub

    Private Sub btnRandom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRandom.Click
        Dim a As New Thread(AddressOf SetWallbaseWallpaper)
        a.IsBackground = True
        a.Start()
    End Sub

    Sub download(ByVal url As String)
        Dim grabber As New WebClient()
        'Dim myUri = New Uri(url)
        grabber.DownloadFile(url, "C:\wallpaper.jpg")
        'grabber.DownloadFile(url, "C:\wallpaper.jpg")
        grabber.Dispose()
    End Sub

    Friend Sub SetWallbaseWallpaper()
        btnRandom.Enabled = False
        btnRandom.Text = "Downloading..."
        Try
            download(Scraper.ScrapeNewWallpaper())
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, "C:\wallpaper.jpg", SPIF_UPDATEINIFILE Or SPIF_SENDWININICHANGE)

        Catch Ex As Exception

            MsgBox("There was an error setting the wallpaper: " & Ex.Message)

        End Try
        btnRandom.Enabled = True
        btnRandom.Text = "Random New Wallpaper"
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        GC.Collect()
    End Sub
End Class
