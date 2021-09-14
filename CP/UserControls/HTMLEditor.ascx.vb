
Partial Class HTMLEditor
    Inherits System.Web.UI.UserControl
    Public Event KeepPopup()
    Public Event SaveEditor()
    Private _TextValue As String
    Public Property TextValue() As String
        Get
            Return txtDesc.InnerHtml
            Return ""
        End Get
        Set(ByVal value As String)
            txtDesc.InnerHtml = value
            txtEd.Content = value
        End Set
    End Property
    Private _LabelValue As String
    Public Property LabelText() As String
        Get
            Return _LabelValue
        End Get
        Set(ByVal value As String)
            _LabelValue = value
        End Set
    End Property
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Me.uniqueKey = Guid.NewGuid.ToString("N")
            ModalPopupExtender2.BehaviorID = "BHID" + uniqueKey
            txtDesc.Attributes.Add("onclick", " $find('" + "BHID" + uniqueKey + "').show()")
        End If
    End Sub

    Protected Sub UpdateDiv() Handles btnSave.Click
        txtDesc.InnerHtml = txtEd.Content
        ModalPopupExtender2.Hide()
        RaiseEvent KeepPopup()
        RaiseEvent SaveEditor()
    End Sub
    Protected Sub Cancel() Handles btnCancel.Click
        txtEd.Content = txtDesc.InnerHtml
        ModalPopupExtender2.Hide()
        RaiseEvent KeepPopup()
    End Sub

    Protected Sub Close() Handles btnClose.Click
        txtEd.Content = txtDesc.InnerHtml
        ModalPopupExtender2.Hide()
        RaiseEvent KeepPopup()
    End Sub

    Protected uniqueKey As String
  
End Class
