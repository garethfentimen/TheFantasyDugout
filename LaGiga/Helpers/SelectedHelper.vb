Public Module SelectedHelper

    <System.Runtime.CompilerServices.Extension> _
	Public Function Selected(Of T)(ByVal helper As HtmlHelper, ByVal value1 As T, ByVal value2 As T) As String
		If value1.Equals(value2) Then
			Return "class=""selected"""
		End If
		Return String.Empty
	End Function

End Module
