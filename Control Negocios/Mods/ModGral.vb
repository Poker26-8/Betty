﻿Imports System.Net.Mail
Imports System.Net
Imports MySql.Data
Imports System.Threading.Tasks
Imports ThoughtWorks.QRCode.Codec.Util
Imports System.Data.SqlClient

Imports System.Management
Imports System.Security.Cryptography
Imports System.Text
Imports MySql.Data.MySqlClient

Module ModGral

    Public ordetrabajo As Integer = 0
    Public HrTiempo As String = ""
    Public HrEntrega As String = ""

    Public id_usu_log As Integer = 0
    Public ClaveUsuario As String = ""

    'Envio de correos
    Public recibe As String = ""
    Public envia As String = ""
    Public s_smtp As String = ""
    Public port As String = ""
    Public SSL As Boolean = False
    Public Aute As Boolean = False
    Public PassSend As String = ""
    Public body1, body2, body3, body4 As String

    Public mail As New MailMessage
    Public smtp As New SmtpClient

    Public PEDI As Boolean
    Public MyFolio As Double = 0

    Dim nombree As String = ""
    Dim cuentamail As String = ""
    Dim passmail As String = ""
    Dim servidor As String = ""
    Dim puerto As String = ""
    Dim seguridad As Boolean = False

    Sub GenerateAndValidateKey()
        ' Obtén el identificador único
        Dim diskSerial As String = GetDiskSerial()

        If String.IsNullOrEmpty(diskSerial) Then
            'MessageBox.Show("No se pudo obtener el número de serie del disco.")
            'Exit Sub
            Dim fechaCompleta As String = "2024-01-01" 'DateTime.Now.ToString("yyyy-MM-dd")
            Dim random As New Random()
            Dim cadena6Digitos As String = "0897653" ' Genera un número aleatorio de 6 dígitos
            Dim resultado As String = fechaCompleta & " " & cadena6Digitos
            Dim textoSinEspacios As String = resultado.Replace(" ", "")
            diskSerial = textoSinEspacios


            Dim uniqueKey2 As String = GenerateHash(diskSerial)
            frmPagado.lblSerie.Text = uniqueKey2
            frmPagado.txtNumPC.Text = diskSerial
        Else
            Dim uniqueKey As String = GenerateHash(diskSerial)
            frmPagado.lblSerie.Text = uniqueKey
            frmPagado.txtNumPC.Text = diskSerial
        End If

        ' Genera el hash único

        ' Muestra la clave
        'MessageBox.Show("Clave generada para esta máquina: " & uniqueKey)
    End Sub

    Public Function GenerateAndValidateKey2(ByVal ser As String)
        ' Obtén el identificador único
        Dim diskSerial As String = ser
        If String.IsNullOrEmpty(diskSerial) Then
            'MessageBox.Show("No se pudo obtener el número de serie del disco.")
            'Exit Function
            'Exit Sub
            Dim fechaCompleta As String = "2024-01-01" 'DateTime.Now.ToString("yyyy-MM-dd")
            Dim random As New Random()
            Dim cadena6Digitos As String = "0897653" ' Genera un número aleatorio de 6 dígitos
            Dim resultado As String = fechaCompleta & " " & cadena6Digitos
            Dim textoSinEspacios As String = resultado.Replace(" ", "")
            diskSerial = textoSinEspacios
            Dim uniqueKey2 As String = GenerateHash(diskSerial)
            Return uniqueKey2
        Else
            Dim uniqueKey As String = GenerateHash(diskSerial)
            Return uniqueKey
        End If

        ' Genera el hash único

        ' Muestra la clave
        'MessageBox.Show("Clave generada para esta máquina: " & uniqueKey)
    End Function

    Public Function GetDiskSerial() As String
        Try
            Dim searcher As New ManagementObjectSearcher("SELECT SerialNumber FROM Win32_PhysicalMedia")
            For Each wmiObject As ManagementObject In searcher.Get()
                Dim serialNumber As String = wmiObject("SerialNumber").ToString()
                Return serialNumber.Trim()
            Next
        Catch ex As Exception
            MessageBox.Show("Error obteniendo el número de serie: " & ex.Message)
        End Try
        Return String.Empty
    End Function

    Public Function GenerateHash(input As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(input)
            Dim hash As Byte() = sha256.ComputeHash(bytes)
            Return BitConverter.ToString(hash).Replace("-", "").ToUpper()
        End Using
    End Function

    Public Function TraerUsuarioIngresado(ByVal passw As String) As String
        Dim miusuario As String = ""

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Alias,Status FROM usuarios WHERE Clave='" & passw & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    If rd1(1).ToString = 1 Then
                        miusuario = rd1(0).ToString
                    Else
                        MsgBox("El usuario esta inactivo contacte a su administrador.", vbInformation + vbOKOnly, titulocentral)
                    End If
                End If
            Else
                MsgBox("Contraseña incorrecta.", vbInformation + vbOKOnly, titulocentral)
            End If
            rd1.Close()
            cnn1.Close()

            Return miusuario
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Function

    Public Function TraerFormatoImpresion() As String

        Dim respuesta As String = ""
        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "select Formato from RutasImpresion where Equipo='" & ObtenerNombreEquipo() & "' and Tipo='Venta'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    respuesta = rd1(0).ToString
                End If
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try

        Return respuesta
    End Function


    'DatosRecarga
    Public Function DatosRecarga(ByVal valor As String) As String
        Dim respuesta As String = ""
        Dim siono As Integer = 0

        Dim cnn5 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd5 As MySqlDataReader
        Dim cmd5 As MySqlCommand

        Try
            cnn5.Close() : cnn5.Open()

            cmd5 = cnn5.CreateCommand
            cmd5.CommandText =
                "select Facturas, NotasCred, NumPart from Formatos where Facturas='" & valor & "'"
            rd5 = cmd5.ExecuteReader
            If rd5.HasRows Then
                If rd5.Read Then
                    respuesta = rd5("NotasCred").ToString
                    siono = rd5("NumPart").ToString
                End If
            Else
                respuesta = ""
            End If
            'If siono = 0 Then
            '    respuesta = ""
            'End If
            rd5.Close() : cnn5.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
        Return respuesta

    End Function

    Public Function DatosRecarga2(ByVal valor As String) As String
        Dim respuesta As String = ""
        Dim siono As Integer = 0

        Dim cnn5 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd5 As MySqlDataReader
        Dim cmd5 As MySqlCommand

        Try
            cnn5.Close() : cnn5.Open()

            cmd5 = cnn5.CreateCommand
            cmd5.CommandText =
                "select Facturas, NotasCred, NumPart from Formatos where Facturas='" & valor & "'"
            rd5 = cmd5.ExecuteReader
            If rd5.HasRows Then
                If rd5.Read Then
                    respuesta = rd5("NotasCred").ToString
                    siono = rd5("NumPart").ToString
                End If
            Else
                siono = 0
            End If
            rd5.Close() : cnn5.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
        Return siono

    End Function

    Public Function ObtenerNombreEquipo() As String
        Dim nombrePC As String
        nombrePC = Dns.GetHostName

        Return nombrePC
    End Function

    'ProdIEPS
    Public Function ProdsIEPS(ByVal cod As String) As Double

        Dim cnn5 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd5 As MySqlDataReader
        Dim cmd5 As MySqlCommand

        If cod <> "" Then
            cnn5.Close() : cnn5.Open()

            cmd5 = cnn5.CreateCommand
            cmd5.CommandText =
                "select IIEPS from Productos where Codigo='" & cod & "'"
            rd5 = cmd5.ExecuteReader
            If rd5.HasRows Then
                If rd5.Read Then
                    ProdsIEPS = CDbl(rd5(0).ToString) / 100
                Else
                    ProdsIEPS = 0
                End If
            Else
                ProdsIEPS = 0
            End If
            rd5.Close() : cnn5.Close()
        Else
            ProdsIEPS = 0
        End If
    End Function

    Public Function IvaDSC(ByVal cod As String) As Double

        Dim cnn3 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd3 As MySqlDataReader
        Dim cmd3 As MySqlCommand

        Try
            cnn3.Close() : cnn3.Open()

            cmd3 = cnn3.CreateCommand
            cmd3.CommandText =
                "select IVA from Productos where Codigo='" & cod & "'"
            rd3 = cmd3.ExecuteReader
            If rd3.HasRows Then
                If rd3.Read Then
                    IvaDSC = CDbl(IIf(rd3(0).ToString = "", "0", rd3(0).ToString))
                End If
            Else
                IvaDSC = 0
            End If
            rd3.Close()
            cnn3.Close()
            Return IvaDSC
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn3.Close()
        End Try
    End Function

    'BorraLotes
    Public Sub BorraLotes()

        Dim cnn5 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd5 As MySqlDataReader
        Dim cmd5 As MySqlCommand

        Try
            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText =
                "delete from LoteCaducidad where Cantidad<=0"
            cmd5.ExecuteNonQuery()
            cnn5.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            cnn5.Close()
        End Try
    End Sub

    'SFormatos
    Public Function SFormatos(ByVal campo As String, ByVal valor As String)
        Dim existe As Boolean = False

        Dim cnn5 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd5 As MySqlDataReader
        Dim cmd5 As MySqlCommand

        Try
            cnn5.Close() : cnn5.Open()

            cmd5 = cnn5.CreateCommand
            cmd5.CommandText =
                "select NotasCred from Formatos where Facturas='" & campo & "'"
            rd5 = cmd5.ExecuteReader
            If rd5.HasRows Then
                If rd5.Read Then
                    existe = True
                End If
            Else
                existe = False
            End If
            rd5.Close()

            If (existe) Then
                'cmd5 = cnn5.CreateCommand
                'cmd5.CommandText =
                '    "UPDATE Formatos SET NotasCred='" & valor & "' WHERE Facturas='" & campo & "'"
                'cmd5.ExecuteNonQuery()
            Else
                cmd5 = cnn5.CreateCommand
                cmd5.CommandText =
                    "insert into Formatos(Facturas,NotasCred,NumPart) values('" & campo & "','" & valor & "',0)"
                cmd5.ExecuteNonQuery()
            End If

            cnn5.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
        Return Nothing
    End Function

    Public Function SFormatos2(ByVal campo As String, ByVal valor As String)
        Dim existe As Boolean = False

        Dim cnn5 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd5 As MySqlDataReader
        Dim cmd5 As MySqlCommand

        Try
            cnn5.Close() : cnn5.Open()

            cmd5 = cnn5.CreateCommand
            cmd5.CommandText =
                "select NotasCred from Formatos where Facturas='" & campo & "'"
            rd5 = cmd5.ExecuteReader
            If rd5.HasRows Then
                If rd5.Read Then
                    existe = True
                End If
            Else
                existe = False
            End If
            rd5.Close()

            If (existe) Then
                cmd5 = cnn5.CreateCommand
                cmd5.CommandText = "Update Formatos set NotasCred='" & valor & "' where Facturas='" & campo & "'"
                If cmd5.ExecuteNonQuery Then
                Else

                End If
            Else
                cmd5 = cnn5.CreateCommand
                cmd5.CommandText =
                    "insert into Formatos(Facturas,NotasCred,NumPart) values('" & campo & "','" & valor & "',0)"
                cmd5.ExecuteNonQuery()
            End If

            cnn5.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
        Return Nothing
    End Function

    Public Function Dame_Regimen(ByVal pepito As String) As String
        Dim respuesta As String = ""

        Dim cnn5 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd5 As MySqlDataReader
        Dim cmd5 As MySqlCommand

        Try
            cnn5.Close() : cnn5.Open()

            cmd5 = cnn5.CreateCommand
            cmd5.CommandText =
                 "select Descripcion from RegimenFiscalSat where ClaveRegFis='" & pepito & "'"
            rd5 = cmd5.ExecuteReader
            If rd5.HasRows Then
                If rd5.Read Then
                    respuesta = rd5(0).ToString()
                End If
            Else
                respuesta = ""
            End If
            rd5.Close() : cnn5.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString()) : cnn5.Close()
        End Try
        Return respuesta
    End Function

    Public Function Dame_ClaveReg(ByVal pipeto As String) As String
        Dim respuesta As String = ""

        Dim cnn5 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd5 As MySqlDataReader
        Dim cmd5 As MySqlCommand

        Try
            cnn5.Close() : cnn5.Open()

            cmd5 = cnn5.CreateCommand
            cmd5.CommandText =
                 "select ClaveRegFis from RegimenFiscalSat where Descripcion='" & pipeto & "'"
            rd5 = cmd5.ExecuteReader
            If rd5.HasRows Then
                If rd5.Read Then
                    respuesta = rd5(0).ToString()
                End If
            Else
                respuesta = ""
            End If
            rd5.Close() : cnn5.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            cnn5.Close()
        End Try
        Return respuesta
    End Function

    Public Function ValidaPermisos(ByVal user As String, ByVal permiso As String) As Boolean
        Dim id_usu As Integer = 0

        Dim cnn5 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd5 As MySqlDataReader
        Dim cmd5 As MySqlCommand

        Try
            cnn5.Close() : cnn5.Open()

            cmd5 = cnn5.CreateCommand
            cmd5.CommandText =
                      "select IdEmpleado from Usuarios where Alias='" & user & "'"
            rd5 = cmd5.ExecuteReader
            If rd5.HasRows Then
                If rd5.Read Then
                    id_usu = rd5(0).ToString()
                End If
            End If
            rd5.Close()

            cmd5 = cnn5.CreateCommand
            cmd5.CommandText =
                      "select * from Permisos where IdEmpleado=" & id_usu
            rd5 = cmd5.ExecuteReader
            If rd5.HasRows Then
                If rd5.Read Then
                    If rd5(permiso).ToString() = True Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            End If
            rd5.Close()
            cnn5.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString() & cmd5.CommandText =
                      "select * from Permisos where IdEmpleado=" & id_usu)
            cnn5.Close()
        End Try
#Disable Warning BC42353' La función no devuelve un valor
    End Function
#Enable Warning BC42353' La función no devuelve un valor

    'Promos
    Public Function Promos(ByVal codigo As String, ByVal actual As Double) As Double
        Dim FechaI As Date = Nothing, FechaF As Date = Nothing
        Dim DescPromo As Double = 0
        Dim INI As Integer = 0, FIN As Integer = 0

        Dim cnn3 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd3 As MySqlDataReader
        Dim cmd3 As MySqlCommand

        Try
            cnn3.Close() : cnn3.Open()

            cmd3 = cnn3.CreateCommand
            cmd3.CommandText =
                "select Fecha_Inicial, Fecha_Final, Porcentaje_Promo, PrecioVentaIVA from Productos where Codigo='" & codigo & "'"
            rd3 = cmd3.ExecuteReader
            If rd3.HasRows Then
                If rd3.Read Then
                    If CDbl(rd3(2).ToString) <> 0 Then
                        FechaI = rd3(0).ToString
                        FechaF = rd3(1).ToString
                        INI = DateDiff(DateInterval.Day, Date.Now, FechaI)
                        FIN = DateDiff(DateInterval.Day, Date.Now, FechaF)
                        If INI <= 0 And FIN >= 0 Then
                            DescPromo = CDbl(rd3(3).ToString) * (CDbl(rd3(2).ToString) / 100)
                            Promos = actual - DescPromo
                        Else
                            Promos = actual
                        End If
                    Else
                        Promos = actual
                    End If
                End If
            Else
                Promos = actual
            End If
            rd3.Close() : cnn3.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn3.Close()
        End Try
#Disable Warning BC42353' La función no devuelve un valor
    End Function
#Enable Warning BC42353' La función no devuelve un valor

    Public Function Elimina_Especiales(ByVal texto As String) As String
        Elimina_Especiales = texto
        If texto = "*" Then Elimina_Especiales = ""
        If texto = "=" Then Elimina_Especiales = ""
        If texto = "!" Then Elimina_Especiales = ""
        If texto = "#" Then Elimina_Especiales = ""
        If texto = "$" Then Elimina_Especiales = ""
        If texto = "%" Then Elimina_Especiales = ""
        If texto = "&" Then Elimina_Especiales = ""
        If texto = "/" Then Elimina_Especiales = ""
        If texto = "(" Then Elimina_Especiales = ""
        If texto = ")" Then Elimina_Especiales = ""
        If texto = "?" Then Elimina_Especiales = ""
        If texto = "'" Then Elimina_Especiales = ""
        If texto = "¡" Then Elimina_Especiales = ""
        If texto = "¿" Then Elimina_Especiales = ""
        If texto = "." Then Elimina_Especiales = ""
        If texto = "," Then Elimina_Especiales = ""
        If texto = ":" Then Elimina_Especiales = ""
        If texto = ";" Then Elimina_Especiales = ""
        If texto = "_" Then Elimina_Especiales = ""
        If texto = "¨" Then Elimina_Especiales = ""
        If texto = "+" Then Elimina_Especiales = ""
        If texto = "[" Then Elimina_Especiales = ""
        If texto = "]" Then Elimina_Especiales = ""
        If texto = "{" Then Elimina_Especiales = ""
        If texto = "}" Then Elimina_Especiales = ""
        If texto = "´" Then Elimina_Especiales = ""

        Return Elimina_Especiales
    End Function

    Private Sub recupera_campos()
        conexionlocal()
        'cnn1.Close() : cnn1.Open()
        'cmd1 = cnn1.CreateCommand
        'cmd1.CommandText = ""
        'rd1 = cmd1.ExecuteReader
        'If rd1.HasRows Then
        '    If rd1.Read Then

        '    End If
        'End If
        'rd1.Close()
        'cnn1.Close()

        Dim cnn As MySqlClient.MySqlConnection = New MySqlClient.MySqlConnection
        Dim sSQL As String = "SELECT * FROM Formatos"
        Dim sinfo As String = ""
        Dim dr As DataRow
        Dim dt As New DataTable
        Dim oData As New ToolKitSQL.myssql
        With oData
            If .dbOpen(cnn, sTargetlocal, sinfo) Then
                If .getDt(cnn, dt, sSQL, sinfo) Then
                    For Each dr In dt.Rows
                        Select Case dr("Facturas").ToString
                            Case "Mail_Emi"
                                cuentamail = dr("NotasCred").ToString
                            Case "Shibboleth_ML"
                                passmail = dr("NotasCred").ToString
                            Case "Server_SMTP"
                                servidor = dr("NotasCred").ToString
                            Case "Pto_Mail"
                                puerto = dr("NotasCred").ToString
                            Case "SSL"
                                seguridad = dr("NotasCred").ToString
                        End Select
                    Next
                    seguridad = True
                    nombree = varnomemail
                Else
                    MsgBox("Error en la configuracion de correo")
                End If
                cnn.Close()
            Else
                MsgBox(sinfo)
            End If
        End With
    End Sub

    Public Function envio_Corte(ByVal paraf As String, ByVal asuntof As String, ByVal mensajef As String, ByVal archivof As String)
        If archivof <> "" Then
            recupera_campos()

            Dim mail As New MailMessage
            Dim oAttch As Net.Mail.AttachmentBase = New Net.Mail.Attachment(archivof)

            Try
                mail.From = New MailAddress(cuentamail, nombree)
                mail.To.Add(paraf)
                mail.Subject = (asuntof)
                mail.Body = (mensajef)
                mail.Attachments.Add(oAttch)

                Dim servidorx As New SmtpClient(servidor)
                servidorx.Port = puerto
                servidorx.EnableSsl = seguridad
                servidorx.Credentials = New System.Net.NetworkCredential(cuentamail, passmail)
                servidorx.Send(mail)

                MessageBox.Show("Mensaje enviado correctamene", "Exito!", MessageBoxButtons.OK)
                Return True

            Catch ex As Exception
                MessageBox.Show(ex.ToString, "Error!", MessageBoxButtons.OK)
                Return False
            End Try
        End If
#Disable Warning BC42105 ' La función 'envio' no devuelve un valor en todas las rutas de acceso de código. Puede producirse una excepción de referencia NULL en tiempo de ejecución cuando se use el resultado.
    End Function
End Module
