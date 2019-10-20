Public Class CCategory
    Private _strCatName As String
    Private _dblTotalValue As Double
    Private _intPoorRating As Integer
    Private _intTotalCount As Integer
    Private _intT
    'consturctor 
    Public Sub New(strName As String, dblValue As Double)
        _strCatName = strName
        _dblTotalValue = dblValue
        _intTotalCount = 1
    End Sub
    Public ReadOnly Property CatName() As String ' read only so you can only get it 
        Get
            Return _strCatName
        End Get
    End Property
    Public Property TotalValue() As Double
        Get
            Return _dblTotalValue
        End Get
        Set(dblVal As Double)
            _dblTotalValue = dblVal

        End Set
    End Property
    Public Property TotalCount() As Integer

        Get
            Return _intTotalCount
        End Get
        Set(intVal As Integer)
            _intTotalCount = intVal
        End Set
    End Property
    Public Property intPoorRating() As Integer
        Get
            Return _intPoorRating
        End Get
        Set(intRate As Integer)
            _dblTotalValue = intRate

        End Set
    End Property
    'Public Property TotalCount() As Integer

    '    Get
    '        Return _intTotalCount
    '    End Get
    '    Set(intVal As Integer)
    '        _intTotalCount = intVal
    '    End Set
End Class
