Imports System
Imports mysql.Data.MySqlClient

Module Program

    Dim connection

    Sub Main(args As String())
        Dim op As Integer

        Dim nombre As String
        Dim nie As Integer

        Connect()

        Do
            Console.WriteLine("-- MENU --")
            Console.WriteLine("1. Mostrar")
            Console.WriteLine("2. Insertar")
            Console.WriteLine("3. Actualizar")
            Console.WriteLine("4. Eliminar")
            Console.WriteLine("5. Salir")
            Console.Write("Seleccione una opción: ")
            op = Console.ReadLine()

            Select Case op
                Case 1
                    Console.WriteLine("Mostrar")
                    Console.WriteLine(Show())
                Case 2
                    Console.WriteLine("Insertar")
                    Console.Write("Nombre: ")
                    nombre = Console.ReadLine()

                    Console.Write("NIE: ")
                    nie = Console.ReadLine()

                    Console.WriteLine(Insert(nombre, nie))
                Case 3
                    Console.WriteLine("Actualizar")
                Case 4
                    Console.WriteLine("Eliminar")
                Case 5
                    Console.WriteLine("Hasta pronto :)")
                Case Else
                    Console.WriteLine("Opción no válida")
            End Select

            Console.ReadKey()

        Loop While op <> 5
    End Sub

    Sub Connect()
        connection = New MySqlConnection(
            "server=localhost;user=root;password=;database=db_escuela"
        )

        connection.Open()
    End Sub

    Function Show() As String
        Connect()

        Dim q As String = "SELECT * FROM alumno"

        Dim cmd As MySqlCommand = New MySqlCommand(q, connection)
        Dim reader As MySqlDataReader = cmd.ExecuteReader()

        Dim result As String = ""

        While reader.Read()
            result &= reader("id_alumno") 
            result &= " - " 
            result &= reader("nie_alumno")  
            result &= " - "
            result &= reader("nombre_alumno")
            result &= vbCrLf ' Salto de línea
        End While

        return result
    End Function

    Function Insert(nombre As String, nie As Integer) As String
        Connect()
        Dim sql As String = "INSERT INTO alumno(nombre_alumno, nie_alumno) VALUES (@nombre, @nie)"
        Dim cmd As MySqlCommand = New MySqlCommand(sql, connection)

        cmd.Parameters.AddWithValue("@nombre", nombre)
        cmd.Parameters.AddWithValue("@nie", nie)

        cmd.ExecuteNonQuery()
        return "Registro insertado"
    End Function
End Module
