'-------------------------------------------------------------------CALCULATOR--------------------------------------------------------------------
'
'-------------------------------------------------------------------------------------------------------------------------------------------------
'
' GROUP 20
' ---------
' 170101013 ARYAN AGRAWAL
' 170101050 PULIKONDA ROOP SAI RAKESH GUPTA
' 160101017 AUTONU KRO
'
'---------------------------------------------------------------------------------------------------------------------------------------------------
'
' Functions Used:
'
'   1. DMAS:
'         It evaluates the expression containing decimal values and operator accoriding to the DMAS rule.
'
'   2. Evaluator:
'           It will parse the expression and divide it into substrings based on the brackets and send it for evaluation to DMAS function.   
'
'   3. validator:
'           It checks the validity of the expression given as input based on the following factors:
'               a. If the expression contains consecutive arithmetic operators
'               b. If the opening bracket is precceded by the numerical value or a dot "."
'               c. If the closing bracket is succeded by the numerical value or a dot "."
'
'----------------------------------------------------------------------------------------------------------------------------------------------------
'
' Algorithm:
'
'   1.	At the start the calculator will show 0 in the display and assign a variable ‘ans’ = 0. User will then enter the 
'       string containing the operand and the operator and brackets (open bracket- ‘(‘and closed bracket- ‘)’). 
'       Example Input: (2+3/2) + 5
'
'   2.  Expression is then send to "validator" function that checks the validity of the expression and sends it to the "Evaluator" 
'       function for evaluation.
'
'   3.  "Evaluator" function then divides the expression based on brackets and stores in the list names "strings"
'       Example : strings = { 2+3/2 , (2+3/2) + 5 }
'
'   4. It then sends each item in the list to DMAS function to get the result for tha expression and replace all the string containg 
'      that bracket expression with its value.
'      Example: strings = { 3.5 , 3.5 + 5 }
'       
'   5. It then sends the last and final expression rid of the brackets to DMAS function and return that value.
'
'   6. DMAS function takes the expression and evaluates by the DMAS rule.
'      Example: 2+3/2 --> 2+1.5 --> 3.5
'
'----------------------------------------------------------------------------------------------------------------------------------------------------
'
'Data Structures Used:
'
'   1. List : To store the bracket expression in "Evaluator" function and to store the double values and operators in DMAS function. 
'
'------------------------------------------------------------------------------------------------------------------------------------------------------



Public Class Calculator
    Dim flag_1 As Int16 = 0
    Dim error_string As String = "-----INPUT ERROR-----"
    Dim ans As Double = 0
    ' Call this routine to compute the resulting value of an expression part
    Function DMAS(expr As String)

        'To confirm that the string input in the dmas doesn't conatain "(" or ")"
        If expr.Contains("(") Or expr.Contains(")") Then
            Return "ERROR"
        End If

        Dim Result As Double = 0
        Dim val As String = ""
        Dim op() As String = {"+", "-", "*", "/"}
        Dim strings As List(Of String) = New List(Of String)
        ' Iterate through each character and for each character
        ' perform a check if it has a numberical value
        Dim count As Int16 = 0
        ' Count stores the number of operations in the string which is useful to print the output not as 0
        For index = 0 To expr.Length() - 1 Step 1
            If IsNumeric(expr(index)) Then
                val = Nothing
                Dim done As Boolean = False
                ' If so, perform iterations stepping forward into each character
                ' until we've found a character which value is not numerical
                While index < expr.Length() And done = False
                    ' For each character perform a similar check if it's numerical value
                    If IsNumeric(expr(index)) Or expr(index) = "." Then
                        ' If so, append it to the resulting string.
                        val += expr(index)
                        ' Increment the value of loop counter variable index by 1
                        index = index + 1
                        ' Otherwise break the loop execution
                    Else : done = True
                    End If
                End While
                index -= 1
                ' Append the numberical value extracted to the array of strings
                strings.Add(val)
                ' Perform a check if the current character equals to '+'

            ElseIf expr(index) = op(0) Then
                ' If so, add the string containg the following character only to the the array of strings
                strings.Add(op(0))
                ' Perform a check if the current character equals to '-'
            ElseIf expr(index) = op(1) Then
                ' If so, add the string containg the following character only to the the array of strings
                strings.Add(op(1))
                ' Perform a check if the current character equals to '*'
            ElseIf expr(index) = op(2) Then
                ' If so, add the string containg the following character only to the the array of strings
                strings.Add(op(2))
                ' Perform a check if the current character equals to '/'
            ElseIf expr(index) = op(3) Then
                ' If so, add the string containg the following character only to the the array of strings
                strings.Add(op(3))
            ElseIf Not IsNumeric(expr(index)) And Not expr(index) = " " Then
                Return "MATH ERROR"
            End If
        Next

        'If there is "-" at the start then we are adding 0 at the start so that the whole operation is done as "0-value" = "-value"
        If strings(0) = "-" Then
            strings.Insert(0, "0")
        End If

        Dim n As Int32 = 0
        ' Execute the following loop until the string no longer contains '/' characters'
        '--------------DIVISION---------------

        While strings.Contains("/")
            Dim found As Boolean = False
            ' Iterate through the array of strings
            While n < strings.Count() And found = False
                ' For each string perform a check if the following string contains only one character - '/'
                If strings(n) = op(3) Then
                    count += 1
                    ' If so, retrieve the first op1 and second op2 operands which are the previous and
                    ' next elements of the following array of strings respectively
                    Dim op1 As Double = Double.Parse(strings(n - 1))
                    '--------CODE TO DEAL WITH NEGATIVE NUMBERS--------------
                    If strings(n + 1) = "-" Then

                        strings.Insert(n - 1, "-")
                        strings.RemoveAt(n + 2)
                        strings.Insert(0, "0")
                        Continue While
                    End If
                    '--------------------------------------------------------
                    Dim op2 As Double = Double.Parse(strings(n + 1))
                    If op2 = 0 Then
                        Return "MATH ERROR"
                    End If
                    ' Perform division and accumulate the result in Res variable
                    Dim Res = CDbl(op1 / op2)
                    ' Remove the previous element from the array of strings
                    strings.RemoveAt(n - 1)
                    ' Assign the resulting value from Res variable to the position n - 1 in the array of strings
                    strings(n - 1) = Res
                    ' Remove the current element from the array of strings
                    strings.RemoveAt(n)
                    ' Assign the Result variable the resulting value so far
                    Result = Res
                    ' If the operator '/' found break the loop execution
                    found = True
                    n = 0
                End If
                n = n + 1
            End While
        End While
        n = 0

        ' Execute the following loop until the string no longer contains '*' characters'
        '--------------MULTIPLICATION---------------------'

        While strings.Contains("*")
            Dim found As Boolean = False
            ' Iterate through the array of strings
            While n < strings.Count() And found = False
                If strings(n) = op(2) Then
                    count += 1
                    ' If so, retrieve the first op1 and second op2 operands which are the previous and
                    ' next elements of the following array of strings respectively
                    Dim op1 As Double = Double.Parse(strings(n - 1))
                    '--------CODE TO DEAL WITH NEGATIVE NUMBERS--------------
                    If strings(n + 1) = "-" Then

                        strings.Insert(n - 1, "-")
                        strings.RemoveAt(n + 2)
                        strings.Insert(0, "0")
                        Continue While
                    End If
                    '-------------------------------------------------------
                    Dim op2 As Double = Double.Parse(strings(n + 1))
                    ' Perform multiplication and accumulate the result in Res variable
                    Dim Res = CDbl(op1 * op2)
                    ' Remove the previous element from the array of strings
                    strings.RemoveAt(n - 1)
                    ' Assign the resulting value from Res variable to the position n - 1 in the array of strings
                    strings(n - 1) = Res
                    ' Remove the current element from the array of strings
                    strings.RemoveAt(n)
                    ' Assign the Result variable the resulting value so far
                    Result = Res
                    ' If the operator '*' found break the loop execution
                    found = True
                    n = 0
                End If
                n = n + 1
            End While
        End While
        n = 0

        ' Execute the following loop until the string no longer contains '+' and '-' characters
        '------------ADDITION AND SUBTRACTION ------------------


        While strings.Contains("+") Or strings.Contains("-")
            Dim found As Boolean = False
            ' Iterate through the array of strings
            While n < strings.Count() And found = False
                ' For each string perform a check if the following string contains only one character - '+'
                If strings(n) = op(0) Then
                    count += 1



                    ' If so, retrieve the first op1 and second op2 operands which are the previous and
                    ' next elements of the following array of strings respectively
                    Dim op1 As Double = Double.Parse(strings(n - 1))
                    '--------CODE TO DEAL WITH NEGATIVE NUMBERS--------------
                    If strings(n + 1) = "-" Then
                        strings.RemoveAt(n)
                        Continue While
                    End If
                    Dim op2 As Double = Double.Parse(strings(n + 1))
                    ' Perform addition and accumulate the result in Res variable
                    Dim Res = CDbl(op1 + op2)
                    ' Remove the previous element from the array of strings
                    strings.RemoveAt(n - 1)
                    ' Assign the resulting value from Res variable to the position n - 1 in the array of strings
                    strings(n - 1) = Res
                    ' Remove the current element from the array of strings
                    strings.RemoveAt(n)
                    ' Assign the Result variable the resulting value so far
                    Result = Res
                    ' If the operator '+' found break the loop execution
                    found = True
                    n = 0
                End If

                ' For each string perform a check if the following string contains only one character - '-'
                If strings(n) = op(1) Then
                    ' If so, retrieve the first op1 and second op2 operands which are the previous and
                    ' next elements of the following array of strings respectively

                    count += 1
                    Dim op1 As Double = Double.Parse(strings(n - 1))
                    '--------CODE TO DEAL WITH NEGATIVE NUMBERS--------------
                    If strings(n + 1) = "-" Then
                        strings(n) = "+"
                        strings.RemoveAt(n + 1)

                        Continue While
                    End If
                    '--------------------------------------------------------
                    Dim op2 As Double = Double.Parse(strings(n + 1))
                    ' Perform subtraction and accumulate the result in Res variable
                    Dim Res = CDbl(op1 - op2)
                    ' Remove the previous element from the array of strings
                    strings.RemoveAt(n - 1)
                    ' Assign the resulting value from Res variable to the position n - 1 in the array of strings
                    strings(n - 1) = Res
                    ' Remove the current element from the array of strings
                    strings.RemoveAt(n)
                    ' Assign the Result variable the resulting value so far
                    Result = Res
                    ' If the operator '-' found break the loop execution
                    found = True
                    n = 0
                End If
                n = n + 1
            End While
        End While

        ' If count = 0 and the input is not 0 then the output will be the corresponding number in the input
        If count = 0 Then
            Result = Double.Parse(strings(0))
        End If

        Return Result
    End Function


    ' Call this routine to perform the actual mathematic expression parsing
    Function Evaluator(input As String)

        Dim t As Double = 0
        Dim oe(0) As Double
        Dim strings As List(Of String) = New List(Of String)
        Dim flag As Int16 = 0
        Dim sb As String = ""
        ' Iterate through the characters of input string starting at the position of final character
        For index = input.Length() - 1 To 0 Step -1
        
            ' For each character perform a check if its value is '('
            If input(index) = "(" Or index = 0 Then

                If flag = 0 Then
                    sb = ""
                End If
                Dim n As Int32 = 0

                ' Perform a check if this is the first character in string
                If index = 0 Then
                    ' If so assign n variable to the value of variable index
                    If input(index) = "(" Then
                        n = 1
                    End If
                    ' Otherwise assign n variable to the value of variable index + 1
                Else : n = index + 1
                End If

                Dim exists As Boolean = False
                Do
                    exists = False
                    Dim bracket As Boolean = False
                    ' Perform the iterations stepping forward into each succeeding character
                    ' starting at the position n = index + 1 until we've found a character equal to ')'
                    While n < input.Length() And bracket = False

                        ' Check if the current character is not ')'.
                        If input(n) <> ")" Then
                            ' If so, append it to the temporary string buffer
                            sb += input(n)
                            ' Otherwise break the loop execution
                        Else
                            bracket = True
                            Continue While
                        End If
                        ' Increment the n loop counter variable by 1
                        n = n + 1
                    End While


                    If exists <> True Then
                        Dim r As Int32 = 0
                        ' Iterate through the array of positions
                        While r < oe.Count() And exists = False
                            ' For each element perform a check if its value
                            ' is equal to the position of the current ')' character
                            If oe(r) = n Then
                                ' If so, append the character ')' to the temporary string buffer and break
                                ' the loop execution assigning the variable exists to the value 'true'
                                exists = True
                                sb += ") "
                                n = n + 1
                            End If
                            r = r + 1
                        End While
                    End If

                    ' Repeat the following loop execution until we've found the character ')' at
                    ' the New position which is not in the array of positions
                Loop While exists = True

                ' If the current character's ')' position has not been previous found,
                ' add the value of position to the array
                If exists = False Then
                    Array.Resize(oe, oe.Length + 1)
                    oe(t) = n
                    t = t + 1
                End If

                ' Add the currently obtained string containing a specific part of the expression to the array
                strings.Add(sb)

            End If
            If index = 0 And input(0) = "(" And flag = 0 Then
                index = 1
                flag = 1
                sb = "("
            End If
        Next


        ' Iterate through the array of the expression parts
        For index = 0 To strings.Count() - 1 Step 1
            ' Compute the result for the current part of the expression

            Dim Result As String = DMAS(strings.Item(index)).ToString()

            ' Iterate through all succeeding parts of the expression
            For n = index To strings.Count() - 1 Step 1

                ' For each part substitute the substring containing the current part of the expression
                ' with its numerical value without parentheses.
                strings(n) = strings.ElementAt(n).Replace("(" + strings.Item(index) + ")", Result)
            Next
        Next

        ' Compute the numerical value of the last part (e.g. the numerical resulting value of the entire expression)
        ' and return this value at the end of the following routine execution.
        Return DMAS(strings.Item(strings.Count() - 1))
    End Function

    'To check the validity of the input expression
    Function validator(expr As String)
        Dim n As Int16 = expr.Length()
        Dim count1 As Int16 = 0
        Dim flag_valid As Int16 = 0
        'To change the other brackets type to the standard brackets "(" and ")"

        For index = 0 To n - 1 Step 1
            If expr(index) = "(" Then
                count1 += 1
            ElseIf expr(index) = ")" Then
                count1 -= 1
            End If
            If count1 < 0 Then
                Return "ERROR"
            End If
            ' To check if space is present in the expression
            If expr(index) = " " Then
                Return "ERROR"
            End If
            'To check if an operator is follwed by an operator
            If expr(index) = "+" Or expr(index) = "-" Or expr(index) = "*" Or expr(index) = "/" Then
                If index = n - 1 Then Return "error"
                If expr(index + 1) = "+" Or expr(index + 1) = "-" Or expr(index + 1) = "*" Or expr(index + 1) = "/" Then
                    Return "error"
                End If
            End If

            'To check if the bracket is preceeded by an operator only 
            If index <> 0 And expr(index) = "(" Then
                If IsNumeric(expr(index - 1)) Or expr(index - 1) = "." Then
                    Return "Error"
                End If
            End If

            'To check if the bracket is suceeded by an operator only
            If index <> n - 1 And expr(index) = ")" Then
                If IsNumeric(expr(index + 1)) Or expr(index + 1) = "." Then
                    Return "Error"
                End If
            End If
        Next
        If count1 <> 0 Then
            flag_valid = 1
        End If
        While count1 <> 0
            If count1 > 0 Then
                expr = expr.Insert(expr.Length(), ")")
                count1 -= 1
            End If
        End While
        If flag_valid = 1 Then
            MessageBox.Show("We are giving the answer for this expression:     " + expr + "     .If not change it!")
        End If
        Return Evaluator(expr)
    End Function

    Private Sub btn_equal_Click(ByVal sender As System.Object, e As System.EventArgs) Handles btn_equal.Click
        'Evaluate the expression

        flag_1 = 1
        Try
            ans = validator(txtbox_display.Text.ToString)
            TextBox1.Text = ans
            If txtbox_display.Text = "0." Then
                txtbox_display.Text = "0"
            End If
        Catch ex As Exception
            TextBox1.Text =error_string

        End Try
    End Sub

    Private Sub Button_Click(ByVal sender As System.Object, e As System.EventArgs) Handles btn_num1.Click, btn_num9.Click, btn_num8.Click, btn_num7.Click, btn_num6.Click, btn_num5.Click, btn_num4.Click, btn_num3.Click, btn_num2.Click, btn_num0.Click
        '0,1 to 9 digit input

        Dim b_num As Button = sender
        Dim b_string As String = b_num.Text
        If TextBox1.Text = error_string Or TextBox1.Text = "0" Then
            TextBox1.Text = ""
            txtbox_display.Text = ""
            flag_1 = 0
        End If
        If txtbox_display.Text <> "0" And TextBox1.Text <> error_string And ans = 0 Then
            txtbox_display.Text += b_string(1)
            flag_1 = 0


        Else
            txtbox_display.Text = b_string(1)
            flag_1 = 0
            ans = 0
        End If
    End Sub

    Private Sub btn_dot_Click(ByVal sender As System.Object, e As System.EventArgs) Handles btn_dot.Click
        'dot '.' input
        If TextBox1.Text = error_string Or TextBox1.Text = "0" Then
            TextBox1.Text = ""
            txtbox_display.Text = "0"
            flag_1 = 0
        End If
        If TextBox1.Text <> error_string And ans = 0 Then
            txtbox_display.Text += "."
        Else
            txtbox_display.Text = "0."
            ans = 0
        End If


    End Sub

    Private Sub Button_Arith_Click(ByVal sender As System.Object, e As System.EventArgs) Handles btn_sub.Click, btn_multiply.Click, btn_divide.Click, btn_add.Click
        'operators (+,-,*,/) input
        If TextBox1.Text = error_string Or TextBox1.Text = "0" Then
            TextBox1.Text = ""
            txtbox_display.Text = "0"
            flag_1 = 0
        End If
        Dim b_arith As Button = sender
        Dim b_string As String = b_arith.Text

        If TextBox1.Text <> "" And flag_1 = 1 And TextBox1.Text <> error_string Then
            txtbox_display.Text = TextBox1.Text
            flag_1 = 0
        End If
        If txtbox_display.Text <> "0" And TextBox1.Text <> error_string Then
            txtbox_display.Text += b_string(1)
            ans = 0
        Else
            If b_string(1) = "-" Then
                txtbox_display.Text = "-"
                ans = 0
            End If

        End If
    End Sub

    Private Sub btn_backspace_Click(ByVal sender As System.Object, e As System.EventArgs) Handles btn_backspace.Click
        'backspace for delete

        If txtbox_display.Text.Length > 1 Then
            txtbox_display.Text = txtbox_display.Text.Remove(txtbox_display.Text.Length - 1, 1)
        ElseIf txtbox_display.Text.Length > 0 Then
            txtbox_display.Text = "0"
        End If
    End Sub

    Private Sub Button_Brac_Click(ByVal sender As System.Object, e As System.EventArgs) Handles btn_openbrac.Click, btn_closebrac.Click
        'brackets opening '(' or closing ')'

        Dim b_brac As Button = sender
        Dim b_string As String = b_brac.Text
        If TextBox1.Text = error_string Or TextBox1.Text = "0" Then
            TextBox1.Text = ""
            txtbox_display.Text = ""
            flag_1 = 0
        End If
        If txtbox_display.Text <> "0" And TextBox1.Text <> error_string And ans = 0 Then
            txtbox_display.Text += b_string(1)
        Else
            txtbox_display.Text = b_string(1)
            ans = 0
        End If
    End Sub



    Private Sub btn_clr_Click(ByVal sender As System.Object, e As System.EventArgs) Handles btn_clr.Click
        'Clear the textbox_display


        ans = 0
        TextBox1.Text = ""
        txtbox_display.Text = "0"
    End Sub

End Class
