Public Class Class1
    Function GetInput()
        Dim i = 0
        Dim status = False

        Do
            Console.Write("VB: Please enter an integer> ")
            status = Int32.TryParse(Console.ReadLine(), i)
        Loop While Not status

        Return i
    End Function
End Class
