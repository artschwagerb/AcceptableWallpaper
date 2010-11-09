Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions

Module Scraper

    Public Function ScrapeNewWallpaper()
        ''read images from a url and display them in the console window
        'Dim imageUrls As List(Of String) = GetAllImagesFromUrl("http://wallbase.net/random/all/eqeq/1920x1080/0/110/20")

        'For Each img As String In imageUrls
        '    MsgBox(img)
        'Next

        'read links from Wallbase.net random thumbnail page
        Dim linkUrls As List(Of String) = GetAllLinksFromUrl("http://wallbase.net/random/all/eqeq/1920x1080/0/110/20")

        For Each link As String In linkUrls
            If link.StartsWith("http://wallbase.net/wallpaper/") Then

                ''read images from Wallbase.net download page
                Dim imageUrls As List(Of String) = GetAllImagesFromUrl(link)

                For Each img As String In imageUrls
                    If img.StartsWith("http://wallbase2.net/") Then
                        Return img
                    End If
                Next

            End If

        Next

        Return "http://lucidwebdesigns.com/wallpaper/404error.jpg"
    End Function
    ''' <summary>
    ''' Given a web page url, it will retrieve the Html from that page and parse the image tags in that page
    ''' </summary>
    ''' <param name="url">The Web page url in this format "http;//www.msn.com"</param>
    ''' <returns>Returns a list of image urls as strings based on the url of a Web page</returns>
    Public Function GetAllImagesFromUrl(ByVal url As String) As List(Of String)
        Dim urlList As New List(Of String)()
        Dim rawHtml As String = [String].Empty
        'read the contents of the web page into a string
        Using sr As New StreamReader(New WebClient().OpenRead(url))
            rawHtml = sr.ReadToEnd()
            sr.Dispose()
        End Using
        'regular expression to part out <img> tags from the html
        Dim regExPattern As String = "< \s* img [^\>]* src \s* = \s* [\""\']? ( [^\""\'\s>]* )"
        Dim r As New Regex(regExPattern, RegexOptions.IgnoreCase Or RegexOptions.IgnorePatternWhitespace)
        regExPattern = Nothing
        Dim matches As MatchCollection = r.Matches(rawHtml)
        r = Nothing

        For Each m As Match In matches
            urlList.Add(m.Groups(1).Value)
        Next
        matches = Nothing
        Return urlList
    End Function

    Public Function GetAllLinksFromUrl(ByVal url As String) As List(Of String)
        Dim urlList As New List(Of String)()
        Dim rawHtml As String = [String].Empty
        'read the contents of the web page into a string
        Using sr As New StreamReader(New WebClient().OpenRead(url))
            rawHtml = sr.ReadToEnd()
            sr.Dispose()
        End Using
        'regular expression to part out <img> tags from the html
        Dim regExPattern As String = "< \s* a [^\>]* href \s* = \s* [\""\']? ( [^\""\'\s>]* )"
        Dim r As New Regex(regExPattern, RegexOptions.IgnoreCase Or RegexOptions.IgnorePatternWhitespace)
        regExPattern = Nothing
        Dim matches As MatchCollection = r.Matches(rawHtml)
        r = Nothing

        For Each m As Match In matches
            urlList.Add(m.Groups(1).Value)
        Next
        matches = Nothing
        Return urlList
    End Function

End Module
