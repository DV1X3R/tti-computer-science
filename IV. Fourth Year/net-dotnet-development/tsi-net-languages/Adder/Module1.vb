Module Module1

    Sub Main()

        Dim cpp = New InputFirst.Class1()
        Dim vb = New InputSecond.Class1()
        Dim cs = New InputThird.Class1()

        Dim a = cpp.GetInput()
        Dim b = vb.GetInput()
        Dim c = cs.GetInput()

        Dim result = a + b + c

        Console.WriteLine($"a + b + c = {result}")
        Console.ReadLine()

    End Sub

End Module
